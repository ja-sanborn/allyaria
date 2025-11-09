namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>text-wrap-style</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// defining how lines of text should wrap within a block container.
/// </summary>
public sealed record StyleTextWrapStyle : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleTextWrapStyle" /> record using the specified <see cref="Kind" />
    /// value.
    /// </summary>
    /// <param name="kind">The text wrapping behavior to represent.</param>
    public StyleTextWrapStyle(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>text-wrap-style</c> property values.</summary>
    public enum Kind
    {
        /// <summary>Uses the browser’s default line-breaking and wrapping rules for the text.</summary>
        [Description(description: "auto")]
        Auto,

        /// <summary>
        /// Balances wrapped lines within a block, distributing text evenly across lines when possible. Useful for headings or
        /// short multi-line text.
        /// </summary>
        [Description(description: "balance")]
        Balance,

        /// <summary>
        /// Adjusts wrapping to produce visually pleasing text edges, potentially varying line lengths slightly to improve overall
        /// aesthetics.
        /// </summary>
        [Description(description: "pretty")]
        Pretty,

        /// <summary>
        /// Maintains consistent line-breaking behavior for a stable layout even when content changes or is dynamically updated.
        /// </summary>
        [Description(description: "stable")]
        Stable
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>text-wrap-style</c> value into a <see cref="StyleTextWrapStyle" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the text-wrap-style value.</param>
    /// <returns>A new <see cref="StyleTextWrapStyle" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleTextWrapStyle Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleTextWrapStyle(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleTextWrapStyle" /> instance.</summary>
    /// <param name="value">The string representation of the text-wrap-style value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleTextWrapStyle" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleTextWrapStyle? result)
    {
        try
        {
            result = Parse(value: value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a string into a <see cref="StyleTextWrapStyle" /> instance.</summary>
    /// <param name="value">The string representation of the text-wrap-style value.</param>
    /// <returns>A <see cref="StyleTextWrapStyle" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleTextWrapStyle(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleTextWrapStyle" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleTextWrapStyle" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>text-wrap-style</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleTextWrapStyle? value) => (value?.Value).OrDefaultIfEmpty();
}
