using Allyaria.Theming.Helpers;
using System.Globalization;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>font-weight</c> CSS value.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> implements value-based equality and accepts the standard CSS
/// keywords <c>normal</c>, <c>bold</c>, <c>lighter</c>, <c>bolder</c>, or a numeric weight that is a multiple of 100
/// between 100 and 900 inclusive. The <c>var(...)</c> function is also accepted and passed through unchanged to support
/// CSS custom properties.
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns a declaration in the exact form <c>font-weight:value;</c> (no spaces).
/// <see cref="ToString" /> calls <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct OldAllyariaFontWeight : IEquatable<OldAllyariaFontWeight>
{
    /// <summary>Initializes a new instance of the <see cref="OldAllyariaFontWeight" /> struct from a raw CSS value.</summary>
    /// <param name="value">
    /// Raw input value (e.g., <c>"normal"</c>, <c>"bold"</c>, <c>"400"</c>, <c>"700"</c>, or <c>"var(--fw)"</c>).
    /// </param>
    public OldAllyariaFontWeight(string value) => Value = Normalize(value);

    /// <summary>Gets the normalized CSS value represented by this instance.</summary>
    public string Value { get; } = string.Empty;

    /// <summary>Determines whether the specified object is equal to the current instance (value equality).</summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="obj" /> is an <see cref="OldAllyariaFontWeight" /> with the same normalized
    /// value; otherwise, <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj) => obj is OldAllyariaFontWeight other && Equals(other);

    /// <summary>
    /// Determines whether the specified <see cref="OldAllyariaFontWeight" /> is equal to the current instance (value
    /// equality).
    /// </summary>
    /// <param name="other">The other <see cref="OldAllyariaFontWeight" /> to compare.</param>
    /// <returns>
    /// <see langword="true" /> if both instances have the same normalized value; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(OldAllyariaFontWeight other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

    /// <summary>Returns a hash code for this instance based on the normalized value.</summary>
    /// <returns>A 32-bit signed hash code.</returns>
    public override int GetHashCode()
        => Value is null
            ? 0
            : StringComparer.Ordinal.GetHashCode(Value);

    /// <summary>
    /// Normalizes and validates a <c>font-weight</c> value according to Allyaria rules.
    /// <list type="bullet">
    ///     <item>
    ///         <description>Keywords are trimmed and lowercased: <c>normal</c>, <c>bold</c>, <c>lighter</c>, <c>bolder</c>.</description>
    ///     </item>
    ///     <item>
    ///         <description>Numeric values must be multiples of 100 within the inclusive range 100..900.</description>
    ///     </item>
    ///     <item>
    ///         <description><c>var(...)</c> is accepted and returned as-is.</description>
    ///     </item>
    /// </list>
    /// </summary>
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
        if (OldStyleHelpers.IsCssFunction(trim, "var"))
        {
            return trim;
        }

        // Keyword path (lower-case & validate).
        var lower = trim.ToLowerInvariant();

        if (lower is "normal" or "bold" or "lighter" or "bolder")
        {
            return lower;
        }

        // Numeric path: multiples of 100 between 100 and 900 inclusive.
        if (int.TryParse(lower, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) &&
            n % 100 == 0 && n >= 100 && n <= 900)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        // Failed normalization.
        return string.Empty;
    }

    /// <summary>Generates a CSS declaration string in the form <c>font-weight:value;</c> with no spaces.</summary>
    /// <returns>The CSS declaration for this instance.</returns>
    public string ToCss()
        => string.IsNullOrWhiteSpace(Value)
            ? string.Empty
            : $"font-weight:{Value};";

    /// <summary>Returns the CSS declaration produced by <see cref="ToCss" />.</summary>
    /// <returns>The CSS declaration string.</returns>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="OldAllyariaFontWeight" /> (value equality).</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if both operands represent the same normalized value; otherwise, <see langword="false" />.
    /// </returns>
    public static bool operator ==(OldAllyariaFontWeight left, OldAllyariaFontWeight right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="OldAllyariaFontWeight" />.</summary>
    /// <param name="value">The raw CSS value to convert.</param>
    /// <returns>An <see cref="OldAllyariaFontWeight" /> created from <paramref name="value" />.</returns>
    public static implicit operator OldAllyariaFontWeight(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="OldAllyariaFontWeight" /> to <see cref="string" />.</summary>
    /// <param name="fontWeight">The <see cref="OldAllyariaFontWeight" /> instance.</param>
    /// <returns>The normalized CSS value contained in <paramref name="fontWeight" />.</returns>
    public static implicit operator string(OldAllyariaFontWeight fontWeight) => fontWeight.Value;

    /// <summary>Inequality operator for <see cref="OldAllyariaFontWeight" /> (value inequality).</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if the operands represent different normalized values; otherwise, <see langword="false" />.
    /// </returns>
    public static bool operator !=(OldAllyariaFontWeight left, OldAllyariaFontWeight right) => !left.Equals(right);
}
