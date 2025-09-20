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
public readonly struct AllyariaWordSpacing : IEquatable<AllyariaWordSpacing>
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaWordSpacing" /> struct with a raw CSS value.</summary>
    /// <param name="value">The raw CSS value (e.g., <c>"normal"</c>, <c>"5px"</c>, <c>"10%"</c>, <c>"calc(1px + 0.5em)"</c>).</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> is null, whitespace, or not a valid
    /// <c>word-spacing</c> value.
    /// </exception>
    public AllyariaWordSpacing(string value) => Value = Normalize(value);

    /// <summary>Gets the normalized CSS value represented by this instance.</summary>
    public string Value { get; }

    /// <summary>Determines whether the specified object is equal to the current instance (value equality).</summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is AllyariaWordSpacing other && Equals(other);

    /// <summary>
    /// Determines whether the specified <see cref="AllyariaWordSpacing" /> is equal to the current instance (value equality).
    /// </summary>
    /// <param name="other">The other instance to compare.</param>
    /// <returns>
    /// <see langword="true" /> if both instances have the same normalized value; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(AllyariaWordSpacing other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

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
    /// <exception cref="ArgumentException">Thrown when the value is not valid for <c>word-spacing</c>.</exception>
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
        throw new ArgumentException($"Unable to normalize word-spacing: {value}.", nameof(value));
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

    /// <summary>Equality operator for <see cref="AllyariaWordSpacing" /> using value equality.</summary>
    public static bool operator ==(AllyariaWordSpacing left, AllyariaWordSpacing right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaWordSpacing" />.</summary>
    /// <param name="value">The raw CSS value to convert.</param>
    /// <returns>An <see cref="AllyariaWordSpacing" /> created from <paramref name="value" />.</returns>
    public static implicit operator AllyariaWordSpacing(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaWordSpacing" /> to <see cref="string" />.</summary>
    /// <param name="wordSpacing">The <see cref="AllyariaWordSpacing" /> instance.</param>
    /// <returns>The normalized CSS value represented by <paramref name="wordSpacing" />.</returns>
    public static implicit operator string(AllyariaWordSpacing wordSpacing) => wordSpacing.Value;

    /// <summary>Inequality operator for <see cref="AllyariaWordSpacing" /> using value equality.</summary>
    public static bool operator !=(AllyariaWordSpacing left, AllyariaWordSpacing right) => !left.Equals(right);
}
