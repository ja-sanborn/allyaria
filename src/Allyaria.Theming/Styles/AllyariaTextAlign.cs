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
    /// <summary>Initializes a new instance of the <see cref="AllyariaTextAlign" /> struct from a raw CSS value.</summary>
    /// <param name="value">Raw CSS value (e.g., <c>"left"</c>, <c>"center"</c>, <c>"var(--align)"</c>).</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> is null, whitespace, or not valid for
    /// <c>text-align</c>.
    /// </exception>
    public AllyariaTextAlign(string value) => Value = Normalize(value);

    /// <summary>Gets the normalized CSS value represented by this instance.</summary>
    public string Value { get; }

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is AllyariaTextAlign other && Equals(other);

    /// <inheritdoc />
    public bool Equals(AllyariaTextAlign other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

    /// <inheritdoc />
    public override int GetHashCode()
        => Value is null
            ? 0
            : StringComparer.Ordinal.GetHashCode(Value);

    /// <summary>Normalizes and validates a <c>text-align</c> value.</summary>
    private static string Normalize(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        // Preserve original casing for function identifiers; lower-case only when treating as keywords or unit tokens.
        var trim = value.Trim();

        // Accept common CSS function forms without altering the content.
        if (StyleHelpers.IsCssFunction(trim, "var"))
        {
            return trim;
        }

        // Keyword path (lower-case & validate).
        var lower = trim.ToLowerInvariant();

        if (lower is "left" or "right" or "center" or "justify" or "start" or "end")
        {
            return lower;
        }

        // Failed normalization.
        throw new ArgumentException($"Unable to normalize text-align: {value}.", nameof(value));
    }

    /// <summary>Produces a CSS declaration in the form <c>text-align:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration string.</returns>
    public string ToCss()
        => string.IsNullOrWhiteSpace(Value)
            ? string.Empty
            : $"text-align:{Value};";

    /// <summary>Returns the CSS declaration string produced by <see cref="ToCss" />.</summary>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaTextAlign" />.</summary>
    public static bool operator ==(AllyariaTextAlign left, AllyariaTextAlign right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaTextAlign" />.</summary>
    public static implicit operator AllyariaTextAlign(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaTextAlign" /> to <see cref="string" />.</summary>
    public static implicit operator string(AllyariaTextAlign align) => align.Value;

    /// <summary>Inequality operator for <see cref="AllyariaTextAlign" />.</summary>
    public static bool operator !=(AllyariaTextAlign left, AllyariaTextAlign right) => !left.Equals(right);
}
