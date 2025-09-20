using Allyaria.Theming.Helpers;
using System.Globalization;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>font-size</c> CSS value.
/// <para>
/// This is a <see langword="readonly" /> <see langword="struct" /> with value-based equality. It accepts all valid CSS
/// forms for <c>font-size</c>, including keywords, lengths, percentages, and common CSS functions (e.g., <c>var()</c>,
/// <c>calc()</c>, <c>min()</c>, <c>max()</c>, <c>clamp()</c>).
/// </para>
/// <para>
/// Normalization rules:
/// <list type="bullet">
///     <item>
///         <description>Keywords are lower-cased and validated against a known set.</description>
///     </item>
///     <item>
///         <description>Lengths and percentages are validated; units are normalized to lowercase.</description>
///     </item>
///     <item>
///         <description>
///         Bare numeric values are interpreted as pixel lengths and <c>"px"</c> is appended using
///         <see cref="CultureInfo.InvariantCulture" />.
///         </description>
///     </item>
///     <item>
///         <description>
///         <c>var(...)</c>, <c>calc(...)</c>, <c>min(...)</c>, <c>max(...)</c>, <c>clamp(...)</c> are passed through
///         without alteration.
///         </description>
///     </item>
/// </list>
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns a declaration in the form <c>font-size:value;</c> (no spaces).
/// <see cref="ToString" /> calls <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct AllyariaFontSize : IEquatable<AllyariaFontSize>
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaFontSize" /> struct from a raw CSS value.</summary>
    /// <param name="value">
    /// The raw CSS value (e.g., <c>"16px"</c>, <c>"1rem"</c>, <c>"smaller"</c>, <c>"calc(12px + 1vw)"</c> ).
    /// </param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is not a valid <c>font-size</c> value.</exception>
    public AllyariaFontSize(string value) => Value = Normalize(value);

    /// <summary>
    /// Gets the normalized CSS value (e.g., <c>"16px"</c>, <c>"1rem"</c>, <c>"smaller"</c>, <c>"var(--fs)"</c>).
    /// </summary>
    public string Value { get; } = string.Empty;

    /// <summary>Indicates whether this instance and a specified object are equal (value equality).</summary>
    /// <param name="obj">Another object to compare to.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="obj" /> is an <see cref="AllyariaFontSize" /> with the same normalized
    /// value; otherwise <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj) => obj is AllyariaFontSize other && Equals(other);

    /// <summary>
    /// Indicates whether this instance and another <see cref="AllyariaFontSize" /> are equal (value equality).
    /// </summary>
    /// <param name="other">The other instance.</param>
    /// <returns>
    /// <see langword="true" /> if both instances have the same normalized value; otherwise <see langword="false" />.
    /// </returns>
    public bool Equals(AllyariaFontSize other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

    /// <summary>Returns a hash code for this instance, based on the normalized CSS value using ordinal comparison.</summary>
    /// <returns>A 32-bit signed hash code.</returns>
    public override int GetHashCode()
        => Value is null
            ? 0
            : StringComparer.Ordinal.GetHashCode(Value);

    /// <summary>Normalizes and validates a <c>font-size</c> value.</summary>
    /// <param name="value">The raw input string.</param>
    /// <returns>The normalized value.</returns>
    /// <exception cref="ArgumentException">Thrown when the input does not represent a valid CSS <c>font-size</c>.</exception>
    internal static string Normalize(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        // Preserve original casing for function identifiers; lower-case only when treating as keywords or unit tokens.
        var trim = value.Trim();

        // Accept common CSS function forms without altering the content.
        if (StyleHelpers.IsCssFunction(trim, "var", "calc", "min", "max", "clamp"))
        {
            return trim;
        }

        // Keyword path (lower-case & validate).
        var lower = trim.ToLowerInvariant();

        if (lower is "xx-small" or "x-small" or "small" or "medium" or "" or "large" or "x-large" or "xx-large" or
            "smaller" or "larger")
        {
            return lower;
        }

        // Length/percentage path (accepts px, em, rem, ch, ex, lh, rlh, cm, mm, q, in, pt, pc, vw, vh, vi, vb, vmin, vmax, and %).
        if (StyleHelpers.IsLengthOrPercentage(lower))
        {
            return lower;
        }

        // Bare numeric → px (culture-invariant).
        if (StyleHelpers.IsNumeric(lower))
        {
            return string.Concat(lower, "px");
        }

        // Failed normalization.
        throw new ArgumentException($"Unable to normalize font-size: {value}.", nameof(value));
    }

    /// <summary>Produces a <c>font-size</c> CSS declaration in the form <c>font-size:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration string.</returns>
    public string ToCss()
        => string.IsNullOrWhiteSpace(Value)
            ? string.Empty
            : $"font-size:{Value};";

    /// <summary>Returns the CSS declaration produced by <see cref="ToCss" />.</summary>
    /// <returns>The CSS declaration string.</returns>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaFontSize" /> using value equality.</summary>
    /// <param name="left">Left operand.</param>
    /// <param name="right">Right operand.</param>
    /// <returns>
    /// <see langword="true" /> if both operands have the same normalized value; otherwise <see langword="false" />.
    /// </returns>
    public static bool operator ==(AllyariaFontSize left, AllyariaFontSize right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaFontSize" />.</summary>
    /// <param name="value">The raw CSS value to normalize.</param>
    /// <returns>An <see cref="AllyariaFontSize" /> instance created from <paramref name="value" />.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <see langword="null" /> or whitespace.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is not a valid <c>font-size</c>.</exception>
    public static implicit operator AllyariaFontSize(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaFontSize" /> to <see cref="string" />.</summary>
    /// <param name="fontSize">The <see cref="AllyariaFontSize" /> value.</param>
    /// <returns>The normalized CSS value (e.g., <c>"16px"</c>, <c>"1rem"</c>, <c>"smaller"</c>).</returns>
    public static implicit operator string(AllyariaFontSize fontSize) => fontSize.Value;

    /// <summary>Inequality operator for <see cref="AllyariaFontSize" /> using value equality.</summary>
    /// <param name="left">Left operand.</param>
    /// <param name="right">Right operand.</param>
    /// <returns><see langword="true" /> if operands differ; otherwise <see langword="false" />.</returns>
    public static bool operator !=(AllyariaFontSize left, AllyariaFontSize right) => !left.Equals(right);
}
