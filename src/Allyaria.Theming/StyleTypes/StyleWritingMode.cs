namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>writing-mode</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// defining the direction in which text and block content flow.
/// </summary>
public sealed record StyleWritingMode : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleWritingMode" /> record using the specified <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The writing mode to represent.</param>
    public StyleWritingMode(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>writing-mode</c> property values.</summary>
    public enum Kind
    {
        /// <summary>
        /// Text and blocks flow horizontally from left to right, with new lines stacked vertically top to bottom (default).
        /// </summary>
        [Description(description: "horizontal-tb")]
        HorizontalTb,

        /// <summary>Text and blocks flow sideways left-to-right, with vertical lines stacked horizontally.</summary>
        [Description(description: "sideways-lr")]
        SidewaysLr,

        /// <summary>Text and blocks flow sideways right-to-left, with vertical lines stacked horizontally.</summary>
        [Description(description: "sideways-rl")]
        SidewaysRl,

        /// <summary>Text and blocks flow vertically from top to bottom, and lines stack from left to right.</summary>
        [Description(description: "vertical-lr")]
        VerticalLr,

        /// <summary>Text and blocks flow vertically from top to bottom, and lines stack from right to left.</summary>
        [Description(description: "vertical-rl")]
        VerticalRl
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>writing-mode</c> value into a <see cref="StyleWritingMode" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the writing-mode value.</param>
    /// <returns>A new <see cref="StyleWritingMode" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleWritingMode Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleWritingMode(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleWritingMode" /> instance.</summary>
    /// <param name="value">The string representation of the writing-mode value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleWritingMode" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleWritingMode? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleWritingMode" /> instance.</summary>
    /// <param name="value">The string representation of the writing-mode value.</param>
    /// <returns>A <see cref="StyleWritingMode" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleWritingMode(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleWritingMode" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleWritingMode" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>writing-mode</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleWritingMode? value) => (value?.Value).OrDefaultIfEmpty();
}
