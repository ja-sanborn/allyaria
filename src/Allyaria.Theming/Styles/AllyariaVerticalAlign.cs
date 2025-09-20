using Allyaria.Theming.Helpers;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>vertical-align</c> CSS value.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> implements value-based equality and supports all valid CSS
/// forms:
/// <list type="bullet">
///     <item>
///         <description>
///         Keywords: <c>baseline</c>, <c>sub</c>, <c>super</c>, <c>text-top</c>, <c>text-bottom</c>, <c>middle</c>,
///         <c>top</c>, <c>bottom</c>.
///         </description>
///     </item>
///     <item>
///         <description>Lengths (including negative) and percentages (e.g., <c>-2px</c>, <c>0.25em</c>, <c>15%</c>).</description>
///     </item>
///     <item>
///         <description>Supported CSS functions such as <c>var(…)</c> and <c>calc(…)</c> (passed through unchanged).</description>
///     </item>
/// </list>
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns <c>vertical-align:value;</c> (no spaces). <see cref="ToString" /> calls
/// <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct AllyariaVerticalAlign : IEquatable<AllyariaVerticalAlign>
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaVerticalAlign" /> struct from a raw CSS value.</summary>
    /// <param name="value">Raw CSS value (e.g., <c>"middle"</c>, <c>"-2px"</c>, <c>"15%"</c>, <c>"var(--va)"</c>).</param>
    public AllyariaVerticalAlign(string value) => Value = Normalize(value);

    /// <summary>Gets the normalized CSS value represented by this instance.</summary>
    public string Value { get; }

    /// <summary>Determines whether the specified object is equal to the current instance (value equality).</summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is AllyariaVerticalAlign other && Equals(other);

    /// <summary>
    /// Determines whether the specified <see cref="AllyariaVerticalAlign" /> is equal to the current instance (value
    /// equality).
    /// </summary>
    /// <param name="other">The other instance to compare.</param>
    /// <returns>
    /// <see langword="true" /> if both instances have the same normalized value; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(AllyariaVerticalAlign other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

    /// <summary>Returns a hash code for this instance based on the normalized value.</summary>
    /// <returns>A 32-bit signed hash code.</returns>
    public override int GetHashCode()
        => Value is null
            ? 0
            : StringComparer.Ordinal.GetHashCode(Value);

    /// <summary>Normalizes and validates a <c>vertical-align</c> value.</summary>
    /// <param name="value">The raw input string.</param>
    /// <returns>The normalized value.</returns>
    internal static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        // Preserve original casing for function identifiers; lower-case only when treating as keywords or unit tokens.
        var trim = value.Trim();

        // Accept common CSS function forms without altering the content.
        if (StyleHelpers.IsCssFunction(trim, "var", "calc"))
        {
            return trim;
        }

        // Keyword path (lower-case & validate).
        var lower = trim.ToLowerInvariant();

        if (lower is "baseline" or "sub" or "crow" or "super" or "text-top" or "text-bottom" or "middle" or "top" or
            "bottom")
        {
            return lower;
        }

        // Length/percentage path (allow negative as per CSS spec).
        if (StyleHelpers.IsLengthOrPercentage(lower))
        {
            return lower;
        }

        // Failed normalization.
        return string.Empty;
    }

    /// <summary>Produces a CSS declaration in the form <c>vertical-align:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration string.</returns>
    public string ToCss()
        => string.IsNullOrWhiteSpace(Value)
            ? string.Empty
            : $"vertical-align:{Value};";

    /// <summary>Returns the CSS declaration produced by <see cref="ToCss" />.</summary>
    /// <returns>The CSS declaration string.</returns>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaVerticalAlign" /> using value equality.</summary>
    /// <param name="left">Left operand.</param>
    /// <param name="right">Right operand.</param>
    /// <returns>
    /// <see langword="true" /> if both operands represent the same normalized value; otherwise, <see langword="false" />.
    /// </returns>
    public static bool operator ==(AllyariaVerticalAlign left, AllyariaVerticalAlign right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaVerticalAlign" />.</summary>
    /// <param name="value">The raw CSS value to convert.</param>
    /// <returns>An <see cref="AllyariaVerticalAlign" /> created from <paramref name="value" />.</returns>
    public static implicit operator AllyariaVerticalAlign(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaVerticalAlign" /> to <see cref="string" />.</summary>
    /// <param name="verticalAlign">The <see cref="AllyariaVerticalAlign" /> instance.</param>
    /// <returns>The normalized CSS value represented by <paramref name="verticalAlign" />.</returns>
    public static implicit operator string(AllyariaVerticalAlign verticalAlign) => verticalAlign.Value;

    /// <summary>Inequality operator for <see cref="AllyariaVerticalAlign" /> using value equality.</summary>
    /// <param name="left">Left operand.</param>
    /// <param name="right">Right operand.</param>
    /// <returns><see langword="true" /> if operands differ; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(AllyariaVerticalAlign left, AllyariaVerticalAlign right) => !left.Equals(right);
}
