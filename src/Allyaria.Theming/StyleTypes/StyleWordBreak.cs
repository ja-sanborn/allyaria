namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>word-break</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// controlling how lines should break within words when text overflows its container.
/// </summary>
public sealed record StyleWordBreak : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleWordBreak" /> record using the specified <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The word-breaking behavior to represent.</param>
    public StyleWordBreak(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>word-break</c> property values.</summary>
    public enum Kind
    {
        /// <summary>
        /// Breaks words at arbitrary points if necessary to prevent overflow. Useful for non-language-based content or very long
        /// strings.
        /// </summary>
        [Description(description: "break-all")]
        BreakAll,

        /// <summary>
        /// Allows unbreakable words to be broken if necessary to prevent overflow, but otherwise uses normal line-breaking rules.
        /// </summary>
        [Description(description: "break-word")]
        BreakWord,

        /// <summary>
        /// Prevents line breaks within words in East Asian text (CJK), maintaining whole-word wrapping where possible.
        /// </summary>
        [Description(description: "keep-all")]
        KeepAll,

        /// <summary>Uses the default line-breaking rules for the document’s language and writing system.</summary>
        [Description(description: "normal")]
        Normal
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>word-break</c> value into a <see cref="StyleWordBreak" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the word-break value.</param>
    /// <returns>A new <see cref="StyleWordBreak" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleWordBreak Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleWordBreak(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleWordBreak" /> instance.</summary>
    /// <param name="value">The string representation of the word-break value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleWordBreak" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleWordBreak? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleWordBreak" /> instance.</summary>
    /// <param name="value">The string representation of the word-break value.</param>
    /// <returns>A <see cref="StyleWordBreak" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleWordBreak(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleWordBreak" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleWordBreak" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>word-break</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleWordBreak? value) => value?.Value ?? string.Empty;
}
