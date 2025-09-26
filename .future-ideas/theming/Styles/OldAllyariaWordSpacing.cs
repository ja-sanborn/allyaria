using Allyaria.Theming.Helpers;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>word-spacing</c> CSS value.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> implements value-based equality and accepts all standard
/// forms:
/// <list type="bullet">
///     <item>
///         <description>The keyword <c>normal</c>.</description>
///     </item>
///     <item>
///         <description>A <em>length</em> or <em>percentage</em> (e.g., <c>1em</c>, <c>4px</c>, <c>10%</c>).</description>
///     </item>
///     <item>
///         <description>A supported CSS function such as <c>var(…)</c> or <c>calc(…)</c> (passed through unchanged).</description>
///     </item>
///     <item>
///         <description>A <em>bare number</em> (interpreted as pixels, e.g., <c>5</c> → <c>5px</c>).</description>
///     </item>
/// </list>
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns <c>word-spacing:value;</c> (no spaces). <see cref="ToString" /> calls
/// <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct OldAllyariaWordSpacing : IEquatable<OldAllyariaWordSpacing>
{
    /// <summary>Initializes a new instance of the <see cref="OldAllyariaWordSpacing" /> struct with a raw CSS value.</summary>
    /// <param name="value">The raw CSS value (e.g., <c>"normal"</c>, <c>"5px"</c>, <c>"10%"</c>, <c>"calc(1px + 0.5em)"</c>).</param>
    public OldAllyariaWordSpacing(string value) => Value = Normalize(value);

    /// <summary>Gets the normalized CSS value represented by this instance.</summary>
    public string Value { get; }

    /// <summary>Determines whether the specified object is equal to the current instance (value equality).</summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is OldAllyariaWordSpacing other && Equals(other);

    /// <summary>
    /// Determines whether the specified <see cref="OldAllyariaWordSpacing" /> is equal to the current instance (value
    /// equality).
    /// </summary>
    /// <param name="other">The other instance to compare.</param>
    /// <returns>
    /// <see langword="true" /> if both instances have the same normalized value; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(OldAllyariaWordSpacing other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

    /// <summary>Returns a hash code for this instance based on the normalized value.</summary>
    /// <returns>A 32-bit signed hash code.</returns>
    public override int GetHashCode()
        => Value is null
            ? 0
            : StringComparer.Ordinal.GetHashCode(Value);

    /// <summary>
    /// Normalizes and validates a <c>word-spacing</c> value. Uses the shared tracking normalization helper for consistency
    /// with <c>letter-spacing</c>.
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
        if (OldStyleHelpers.IsCssFunction(trim, "var", "calc"))
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
        if (OldStyleHelpers.IsLengthOrPercentage(lower))
        {
            return lower;
        }

        // Check if bare number.
        if (OldStyleHelpers.IsNumeric(lower))
        {
            return string.Concat(lower, "px");
        }

        // Failed normalization.
        return string.Empty;
    }

    /// <summary>Produces a CSS declaration in the form <c>word-spacing:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration for this value.</returns>
    public string ToCss()
        => string.IsNullOrWhiteSpace(Value)
            ? string.Empty
            : $"word-spacing:{Value};";

    /// <summary>Returns the CSS declaration string produced by <see cref="ToCss" />.</summary>
    /// <returns>The CSS declaration string.</returns>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="OldAllyariaWordSpacing" /> using value equality.</summary>
    public static bool operator ==(OldAllyariaWordSpacing left, OldAllyariaWordSpacing right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="OldAllyariaWordSpacing" />.</summary>
    /// <param name="value">The raw CSS value to convert.</param>
    /// <returns>An <see cref="OldAllyariaWordSpacing" /> created from <paramref name="value" />.</returns>
    public static implicit operator OldAllyariaWordSpacing(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="OldAllyariaWordSpacing" /> to <see cref="string" />.</summary>
    /// <param name="wordSpacing">The <see cref="OldAllyariaWordSpacing" /> instance.</param>
    /// <returns>The normalized CSS value represented by <paramref name="wordSpacing" />.</returns>
    public static implicit operator string(OldAllyariaWordSpacing wordSpacing) => wordSpacing.Value;

    /// <summary>Inequality operator for <see cref="OldAllyariaWordSpacing" /> using value equality.</summary>
    public static bool operator !=(OldAllyariaWordSpacing left, OldAllyariaWordSpacing right) => !left.Equals(right);
}
