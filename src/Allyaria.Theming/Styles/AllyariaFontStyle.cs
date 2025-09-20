using Allyaria.Theming.Helpers;
using System.Globalization;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>font-style</c> CSS value.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> implements value-based equality and supports all valid CSS
/// forms for <c>font-style</c> defined in modern specifications that are relevant to typography:
/// <list type="bullet">
///     <item>
///         <description>Keywords: <c>normal</c>, <c>italic</c>, <c>oblique</c></description>
///     </item>
///     <item>
///         <description>
///         <c>oblique &lt;angle&gt;</c> where angle may be expressed in <c>deg</c>, <c>rad</c>, <c>grad</c>, or
///         <c>turn</c>
///         </description>
///     </item>
///     <item>
///         <description><c>var(…)</c> for custom properties (passed through unchanged)</description>
///     </item>
/// </list>
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns <c>font-style:value;</c> (no spaces). <see cref="ToString" /> calls
/// <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct AllyariaFontStyle : IEquatable<AllyariaFontStyle>
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaFontStyle" /> struct with a raw CSS value.</summary>
    /// <param name="value">The raw CSS value (e.g., <c>"italic"</c>, <c>"oblique 12deg"</c>, <c>"var(--style)"</c>).</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> is null, whitespace, or not a valid
    /// <c>font-style</c> value.
    /// </exception>
    public AllyariaFontStyle(string value) => Value = Normalize(value);

    /// <summary>
    /// Gets the normalized CSS value represented by this instance (e.g., <c>"italic"</c>, <c>"oblique 10deg"</c>).
    /// </summary>
    public string Value { get; } = string.Empty;

    /// <summary>Determines whether the specified object is equal to the current instance using value equality.</summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is AllyariaFontStyle other && Equals(other);

    /// <summary>
    /// Determines whether the specified <see cref="AllyariaFontStyle" /> is equal to the current instance using value
    /// equality.
    /// </summary>
    /// <param name="other">The other instance.</param>
    /// <returns><see langword="true" /> if the normalized values match; otherwise, <see langword="false" />.</returns>
    public bool Equals(AllyariaFontStyle other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

    /// <summary>Returns a hash code for this instance based on the normalized value.</summary>
    /// <returns>A 32-bit signed hash code.</returns>
    public override int GetHashCode()
        => Value is null
            ? 0
            : StringComparer.Ordinal.GetHashCode(Value);

    /// <summary>
    /// Determines whether a token is a valid CSS angle: number + unit in {deg, rad, grad, turn}, using invariant culture. The
    /// numeric value is not range-restricted here.
    /// </summary>
    /// <param name="token">The candidate angle token.</param>
    /// <returns><see langword="true" /> if the token is a valid angle; otherwise, <see langword="false" />.</returns>
    internal static bool IsAngle(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        static bool HasUnit(string s, string unit, out string number)
        {
            if (s.EndsWith(unit, StringComparison.Ordinal) && s.Length > unit.Length)
            {
                number = s[..^unit.Length];

                return true;
            }

            number = string.Empty;

            return false;
        }

        if (HasUnit(token, "deg", out var n) ||
            HasUnit(token, "rad", out n) ||
            HasUnit(token, "grad", out n) ||
            HasUnit(token, "turn", out n))
        {
            return double.TryParse(n, NumberStyles.Float, CultureInfo.InvariantCulture, out _);
        }

        return false;
    }

    /// <summary>
    /// Normalizes and validates a <c>font-style</c> value. Accepts keywords (<c>normal</c>|<c>italic</c>|<c>oblique</c>),
    /// <c>oblique &lt;angle&gt;</c>, and <c>var()</c>.
    /// </summary>
    /// <param name="value">The raw input string.</param>
    /// <returns>The normalized value.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is invalid for <c>font-style</c>.</exception>
    internal static string Normalize(string value)
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

        if (lower is "normal" or "italic" or "oblique")
        {
            return lower;
        }

        // "oblique <angle>" form.
        if (lower.StartsWith("oblique ", StringComparison.Ordinal))
        {
            // Split into exactly two parts: "oblique" and "<angle>"
            var parts = lower.Split(
                new[]
                {
                    ' '
                }, StringSplitOptions.RemoveEmptyEntries
            );

            if (parts.Length == 2 && IsAngle(parts[1]))
            {
                // Preserve canonical formatting "oblique <angle>" in lowercase.
                return $"oblique {parts[1]}";
            }
        }

        // Failed normalization.
        throw new ArgumentException($"Unable to normalize font-style: {value}.", nameof(value));
    }

    /// <summary>Returns a CSS declaration in the form <c>font-style:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration for this value.</returns>
    public string ToCss()
        => string.IsNullOrWhiteSpace(Value)
            ? string.Empty
            : $"font-style:{Value};";

    /// <summary>Returns the same string as <see cref="ToCss" />.</summary>
    /// <returns>The CSS declaration string.</returns>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaFontStyle" /> using value equality.</summary>
    /// <param name="left">Left operand.</param>
    /// <param name="right">Right operand.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(AllyariaFontStyle left, AllyariaFontStyle right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaFontStyle" />.</summary>
    /// <param name="value">The raw CSS value to convert.</param>
    /// <returns>An <see cref="AllyariaFontStyle" /> instance.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> is empty/whitespace or not a valid
    /// <c>font-style</c>.
    /// </exception>
    public static implicit operator AllyariaFontStyle(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaFontStyle" /> to <see cref="string" />.</summary>
    /// <param name="fontStyle">The <see cref="AllyariaFontStyle" /> instance.</param>
    /// <returns>The normalized CSS value.</returns>
    public static implicit operator string(AllyariaFontStyle fontStyle) => fontStyle.Value;

    /// <summary>Inequality operator for <see cref="AllyariaFontStyle" /> using value equality.</summary>
    /// <param name="left">Left operand.</param>
    /// <param name="right">Right operand.</param>
    /// <returns><see langword="true" /> if not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(AllyariaFontStyle left, AllyariaFontStyle right) => !left.Equals(right);
}
