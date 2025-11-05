namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>text-align</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// defining horizontal text alignment behavior in CSS.
/// </summary>
public sealed record StyleTextAlign : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleTextAlign" /> record using the specified <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The text alignment type to represent.</param>
    public StyleTextAlign(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>text-align</c> property values.</summary>
    public enum Kind
    {
        /// <summary>Centers the inline content within the line box.</summary>
        [Description(description: "center")]
        Center,

        /// <summary>
        /// Aligns the inline content to the end edge of the line box based on the document’s writing direction (right for LTR,
        /// left for RTL).
        /// </summary>
        [Description(description: "end")]
        End,

        /// <summary>Justifies text so that each line (except the last) is stretched to fill the container width.</summary>
        [Description(description: "justify")]
        Justify,

        /// <summary>Inherits alignment from the parent element’s text alignment value.</summary>
        [Description(description: "match-parent")]
        MatchParent,

        /// <summary>
        /// Aligns text to the start edge of the line box based on the document’s writing direction (left for LTR, right for RTL).
        /// </summary>
        [Description(description: "start")]
        Start
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>text-align</c> value into a <see cref="StyleTextAlign" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the text-align value.</param>
    /// <returns>A new <see cref="StyleTextAlign" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleTextAlign Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleTextAlign(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleTextAlign" /> instance.</summary>
    /// <param name="value">The string representation of the text-align value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleTextAlign" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleTextAlign? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleTextAlign" /> instance.</summary>
    /// <param name="value">The string representation of the text-align value.</param>
    /// <returns>A <see cref="StyleTextAlign" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleTextAlign(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleTextAlign" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleTextAlign" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>text-align</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleTextAlign? value) => value?.Value ?? string.Empty;
}
