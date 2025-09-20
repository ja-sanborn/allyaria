using Allyaria.Theming.Helpers;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>text-align</c> CSS value.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> implements value-based equality and accepts the standard CSS
/// keywords: <c>left</c>, <c>right</c>, <c>center</c>, <c>justify</c>, <c>start</c>, <c>end</c>. Additionally,
/// <c>var(...)</c> is supported for CSS custom properties and passed through unchanged.
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns <c>text-align:value;</c> (no spaces). <see cref="ToString" /> calls
/// <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct AllyariaTextAlign : IEquatable<AllyariaTextAlign>
{
    /// <summary>
    /// Backing field containing the normalized CSS value (e.g., <c>"left"</c>, <c>"center"</c>, <c>"var(--align)"</c>).
    /// </summary>
    private readonly string _value;

    /// <summary>Allowed keyword set for <c>text-align</c>.</summary>
    private static readonly HashSet<string> AllowedKeywords = new(StringComparer.Ordinal)
    {
        "left",
        "right",
        "center",
        "justify",
        "start",
        "end"
    };

    /// <summary>Initializes a new instance of the <see cref="AllyariaTextAlign" /> struct from a raw CSS value.</summary>
    /// <param name="value">Raw CSS value (e.g., <c>"left"</c>, <c>"center"</c>, <c>"var(--align)"</c>).</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <see langword="null" /> or whitespace.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is not valid for <c>text-align</c>.</exception>
    public AllyariaTextAlign(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value), "text-align value cannot be null or whitespace.");
        }

        _value = Normalize(value);
    }

    /// <summary>Gets the normalized CSS value represented by this instance.</summary>
    public string Value => _value;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is AllyariaTextAlign other && Equals(other);

    /// <inheritdoc />
    public bool Equals(AllyariaTextAlign other) => string.Equals(_value, other._value, StringComparison.Ordinal);

    /// <inheritdoc />
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(_value);

    /// <summary>Normalizes and validates a <c>text-align</c> value.</summary>
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
            "text-align must be one of: left, right, center, justify, start, end, or var(--*).",
            nameof(raw)
        );
    }

    /// <summary>Produces a CSS declaration in the form <c>text-align:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration string.</returns>
    public string ToCss() => $"text-align:{_value};";

    /// <summary>Returns the CSS declaration string produced by <see cref="ToCss" />.</summary>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaTextAlign" />.</summary>
    public static bool operator ==(AllyariaTextAlign left, AllyariaTextAlign right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaTextAlign" />.</summary>
    public static implicit operator AllyariaTextAlign(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaTextAlign" /> to <see cref="string" />.</summary>
    public static implicit operator string(AllyariaTextAlign align) => align._value;

    /// <summary>Inequality operator for <see cref="AllyariaTextAlign" />.</summary>
    public static bool operator !=(AllyariaTextAlign left, AllyariaTextAlign right) => !left.Equals(right);
}
