using Allyaria.Theming.Helpers;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>text-transform</c> CSS value.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> implements value-based equality and accepts the standard
/// keywords: <c>none</c>, <c>capitalize</c>, <c>uppercase</c>, and <c>lowercase</c>. Additionally, <c>var(...)</c> is
/// supported for CSS custom properties and passed through unchanged.
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns <c>text-transform:value;</c> (no spaces). <see cref="ToString" /> calls
/// <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct AllyariaTextTransform : IEquatable<AllyariaTextTransform>
{
    /// <summary>
    /// Backing field containing the normalized CSS value (e.g., <c>"uppercase"</c>, <c>"none"</c>, <c>"var(--tt)"</c>).
    /// </summary>
    private readonly string _value;

    /// <summary>Allowed keyword set for <c>text-transform</c>.</summary>
    private static readonly HashSet<string> AllowedKeywords = new(StringComparer.Ordinal)
    {
        "none",
        "capitalize",
        "uppercase",
        "lowercase"
    };

    /// <summary>Initializes a new instance of the <see cref="AllyariaTextTransform" /> struct from a raw CSS value.</summary>
    /// <param name="value">Raw CSS value (e.g., <c>"uppercase"</c>, <c>"capitalize"</c>, <c>"var(--tt)"</c>).</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <see langword="null" /> or whitespace.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is not valid for <c>text-transform</c>.</exception>
    public AllyariaTextTransform(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value), "text-transform value cannot be null or whitespace.");
        }

        _value = Normalize(value);
    }

    /// <summary>Gets the normalized CSS value represented by this instance.</summary>
    public string Value => _value;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is AllyariaTextTransform other && Equals(other);

    /// <inheritdoc />
    public bool Equals(AllyariaTextTransform other) => string.Equals(_value, other._value, StringComparison.Ordinal);

    /// <inheritdoc />
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(_value);

    /// <summary>Normalizes and validates a <c>text-transform</c> value.</summary>
    private static string Normalize(string raw)
    {
        var v = raw.Trim();

        // Allow CSS custom properties
        if (StyleHelpers.IsCssFunction(v, "var"))
        {
            return v;
        }

        var lower = v.ToLowerInvariant();

        if (AllowedKeywords.Contains(lower))
        {
            return lower;
        }

        throw new ArgumentException(
            "text-transform must be one of: none, capitalize, uppercase, lowercase, or var(--*).",
            nameof(raw)
        );
    }

    /// <summary>Produces a CSS declaration in the form <c>text-transform:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration string.</returns>
    public string ToCss() => $"text-transform:{_value};";

    /// <summary>Returns the CSS declaration string produced by <see cref="ToCss" />.</summary>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaTextTransform" />.</summary>
    public static bool operator ==(AllyariaTextTransform left, AllyariaTextTransform right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaTextTransform" />.</summary>
    public static implicit operator AllyariaTextTransform(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaTextTransform" /> to <see cref="string" />.</summary>
    public static implicit operator string(AllyariaTextTransform transform) => transform._value;

    /// <summary>Inequality operator for <see cref="AllyariaTextTransform" />.</summary>
    public static bool operator !=(AllyariaTextTransform left, AllyariaTextTransform right) => !left.Equals(right);
}
