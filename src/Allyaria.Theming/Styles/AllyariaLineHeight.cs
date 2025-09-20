using Allyaria.Theming.Helpers;
using System.Globalization;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>line-height</c> CSS value.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> provides value-based equality and accepts all standard CSS
/// representations for <c>line-height</c>:
/// <list type="bullet">
///     <item>
///         <description>The keyword <c>normal</c>.</description>
///     </item>
///     <item>
///         <description>A <em>unitless positive number</em> (e.g., <c>1.5</c>), which is culture-invariant.</description>
///     </item>
///     <item>
///         <description>A <em>length</em> or <em>percentage</em> (e.g., <c>20px</c>, <c>1.25rem</c>, <c>120%</c>).</description>
///     </item>
///     <item>
///         <description>Supported CSS functions such as <c>var(…)</c> or <c>calc(…)</c> (passed through unchanged).</description>
///     </item>
/// </list>
/// Negative values are rejected.
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns <c>line-height:value;</c> (no spaces). <see cref="ToString" /> calls
/// <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct AllyariaLineHeight : IEquatable<AllyariaLineHeight>
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaLineHeight" /> struct from a raw CSS value.</summary>
    /// <param name="value">The raw CSS value (e.g., <c>"normal"</c>, <c>"1.5"</c>, <c>"20px"</c>, <c>"var(--lh)"</c>).</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> is null, whitespace, or not a valid
    /// <c>line-height</c> value.
    /// </exception>
    public AllyariaLineHeight(string value) => Value = Normalize(value);

    /// <summary>
    /// Gets the normalized CSS value represented by this instance (e.g., <c>"1.5"</c>, <c>"normal"</c>, <c>"20px"</c>,
    /// <c>"120%"</c>).
    /// </summary>
    public string Value { get; }

    /// <summary>Indicates whether the specified object is equal to the current instance (value equality).</summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is AllyariaLineHeight other && Equals(other);

    /// <summary>
    /// Indicates whether the specified <see cref="AllyariaLineHeight" /> is equal to the current instance (value equality).
    /// </summary>
    /// <param name="other">The other instance to compare.</param>
    /// <returns>
    /// <see langword="true" /> if both instances have the same normalized value; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(AllyariaLineHeight other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

    /// <summary>Returns a hash code for this instance based on the normalized value.</summary>
    /// <returns>A 32-bit signed hash code.</returns>
    public override int GetHashCode()
        => Value is null
            ? 0
            : StringComparer.Ordinal.GetHashCode(Value);

    /// <summary>Normalizes and validates a <c>line-height</c> value.</summary>
    /// <param name="value">The raw input string.</param>
    /// <returns>The normalized value.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is invalid or negative.</exception>
    private static string Normalize(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        // Preserve original casing for function identifiers; lower-case only when treating as keywords or unit tokens.
        var trim = value.Trim();

        // Accept common CSS function forms without altering the content.
        if (StyleHelpers.IsCssFunction(trim, "var", "calc"))
        {
            return trim;
        }

        // Keyword path (lower-case & validate).
        var lower = trim.ToLowerInvariant();

        if (lower is "normal")
        {
            return lower;
        }

        // Canonicalize numeric formatting (e.g., "1.0" => "1")
        if (StyleHelpers.IsUnitlessPositive(lower))
        {
            return double.Parse(lower, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
        }

        // Length/percentage path (lower-case & validate), not negative.
        if (StyleHelpers.IsLengthOrPercentage(lower) && !lower.StartsWith("-", StringComparison.Ordinal))
        {
            return lower;
        }

        // Failed normalization.
        throw new ArgumentException($"Unable to normalize line-height: {value}.", nameof(value));
    }

    /// <summary>Produces a CSS declaration in the form <c>line-height:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration for this value.</returns>
    public string ToCss()
        => string.IsNullOrWhiteSpace(Value)
            ? string.Empty
            : $"line-height:{Value};";

    /// <summary>Returns the CSS declaration string produced by <see cref="ToCss" />.</summary>
    /// <returns>The CSS declaration string.</returns>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaLineHeight" /> using value equality.</summary>
    /// <param name="left">Left operand.</param>
    /// <param name="right">Right operand.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(AllyariaLineHeight left, AllyariaLineHeight right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaLineHeight" />.</summary>
    /// <param name="value">The raw CSS value to convert.</param>
    /// <returns>An <see cref="AllyariaLineHeight" /> created from <paramref name="value" />.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <see langword="null" /> or whitespace.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is not a valid <c>line-height</c> value.</exception>
    public static implicit operator AllyariaLineHeight(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaLineHeight" /> to <see cref="string" />.</summary>
    /// <param name="lineHeight">The <see cref="AllyariaLineHeight" /> instance.</param>
    /// <returns>The normalized CSS value represented by <paramref name="lineHeight" />.</returns>
    public static implicit operator string(AllyariaLineHeight lineHeight) => lineHeight.Value;

    /// <summary>Inequality operator for <see cref="AllyariaLineHeight" /> using value equality.</summary>
    /// <param name="left">Left operand.</param>
    /// <param name="right">Right operand.</param>
    /// <returns><see langword="true" /> if not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(AllyariaLineHeight left, AllyariaLineHeight right) => !left.Equals(right);
}
