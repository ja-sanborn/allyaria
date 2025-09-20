using Allyaria.Theming.Helpers;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>letter-spacing</c> CSS value.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> implements value-based equality and accepts all standard
/// forms:
/// <list type="bullet">
///     <item>
///         <description>The keyword <c>normal</c>.</description>
///     </item>
///     <item>
///         <description>A <em>length</em> or <em>percentage</em> (e.g., <c>0.05em</c>, <c>2px</c>, <c>5%</c>).</description>
///     </item>
///     <item>
///         <description>A supported CSS function such as <c>var(…)</c> or <c>calc(…)</c> (passed through unchanged).</description>
///     </item>
///     <item>
///         <description>
///         A <em>bare number</em> (interpreted as pixels, e.g., <c>2</c> → <c>2px</c>), using
///         <see cref="System.Globalization.CultureInfo.InvariantCulture" />.
///         </description>
///     </item>
/// </list>
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns <c>letter-spacing:value;</c> (no spaces). <see cref="ToString" /> calls
/// <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct AllyariaLetterSpacing : IEquatable<AllyariaLetterSpacing>
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaLetterSpacing" /> struct from a raw CSS value.</summary>
    /// <param name="value">
    /// The raw CSS value (e.g., <c>"normal"</c>, <c>"0.05em"</c>, <c>"2px"</c>, <c>"calc(1px + 0.1em)"</c>).
    /// </param>
    public AllyariaLetterSpacing(string value) => Value = Normalize(value);

    /// <summary>Gets the normalized CSS value represented by this instance.</summary>
    public string Value { get; }

    /// <summary>Determines whether the specified object is equal to the current instance (value equality).</summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is AllyariaLetterSpacing other && Equals(other);

    /// <summary>
    /// Determines whether the specified <see cref="AllyariaLetterSpacing" /> is equal to the current instance (value
    /// equality).
    /// </summary>
    /// <param name="other">The other instance to compare.</param>
    /// <returns>
    /// <see langword="true" /> if both instances have the same normalized value; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(AllyariaLetterSpacing other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

    /// <summary>Returns a hash code for this instance based on the normalized value.</summary>
    /// <returns>A 32-bit signed hash code.</returns>
    public override int GetHashCode()
        => Value is null
            ? 0
            : StringComparer.Ordinal.GetHashCode(Value);

    /// <summary>Normalizes and validates a <c>letter-spacing</c> value using shared helper logic.</summary>
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

        if (lower is "normal")
        {
            return lower;
        }

        // Check if length or percentage.
        if (StyleHelpers.IsLengthOrPercentage(lower))
        {
            return lower;
        }

        // Check if bare number.
        if (StyleHelpers.IsNumeric(lower))
        {
            return string.Concat(lower, "px");
        }

        // Failed normalization.
        return string.Empty;
    }

    /// <summary>Produces a CSS declaration in the form <c>letter-spacing:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration for this value.</returns>
    public string ToCss()
        => string.IsNullOrWhiteSpace(Value)
            ? string.Empty
            : $"letter-spacing:{Value};";

    /// <summary>Returns the CSS declaration produced by <see cref="ToCss" />.</summary>
    /// <returns>The CSS declaration string.</returns>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaLetterSpacing" /> using value equality.</summary>
    /// <param name="left">Left operand.</param>
    /// <param name="right">Right operand.</param>
    /// <returns>
    /// <see langword="true" /> if both operands represent the same normalized value; otherwise, <see langword="false" />.
    /// </returns>
    public static bool operator ==(AllyariaLetterSpacing left, AllyariaLetterSpacing right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaLetterSpacing" />.</summary>
    /// <param name="value">The raw CSS value to convert.</param>
    /// <returns>An <see cref="AllyariaLetterSpacing" /> created from <paramref name="value" />.</returns>
    public static implicit operator AllyariaLetterSpacing(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaLetterSpacing" /> to <see cref="string" />.</summary>
    /// <param name="letterSpacing">The <see cref="AllyariaLetterSpacing" /> instance.</param>
    /// <returns>The normalized CSS value represented by <paramref name="letterSpacing" />.</returns>
    public static implicit operator string(AllyariaLetterSpacing letterSpacing) => letterSpacing.Value;

    /// <summary>Inequality operator for <see cref="AllyariaLetterSpacing" /> using value equality.</summary>
    /// <param name="left">Left operand.</param>
    /// <param name="right">Right operand.</param>
    /// <returns><see langword="true" /> if operands differ; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(AllyariaLetterSpacing left, AllyariaLetterSpacing right) => !left.Equals(right);
}
