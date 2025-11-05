namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>text-decoration-style</c> value within the Allyaria theming system. Provides a strongly typed
/// wrapper for defining the visual style of text decorations such as underlines or overlines.
/// </summary>
public sealed record StyleTextDecorationStyle : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleTextDecorationStyle" /> record using the specified
    /// <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The decoration line style to represent.</param>
    public StyleTextDecorationStyle(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>text-decoration-style</c> property values.</summary>
    public enum Kind
    {
        /// <summary>Draws the text decoration line as short dashes.</summary>
        [Description(description: "dashed")]
        Dashed,

        /// <summary>Draws the text decoration line as a series of dots.</summary>
        [Description(description: "dotted")]
        Dotted,

        /// <summary>Draws a pair of parallel lines for the text decoration.</summary>
        [Description(description: "double")]
        Double,

        /// <summary>Draws a single solid line for the text decoration.</summary>
        [Description(description: "solid")]
        Solid,

        /// <summary>Draws a wavy or zigzag line for the text decoration.</summary>
        [Description(description: "wavy")]
        Wavy
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>text-decoration-style</c> value into a
    /// <see cref="StyleTextDecorationStyle" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the text-decoration-style value.</param>
    /// <returns>A new <see cref="StyleTextDecorationStyle" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleTextDecorationStyle Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleTextDecorationStyle(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleTextDecorationStyle" /> instance.</summary>
    /// <param name="value">The string representation of the text-decoration-style value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleTextDecorationStyle" /> instance or
    /// <see langword="null" /> if parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleTextDecorationStyle? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleTextDecorationStyle" /> instance.</summary>
    /// <param name="value">The string representation of the text-decoration-style value.</param>
    /// <returns>A <see cref="StyleTextDecorationStyle" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleTextDecorationStyle(string? value) => Parse(value: value);

    /// <summary>
    /// Implicitly converts a <see cref="StyleTextDecorationStyle" /> instance to its string representation.
    /// </summary>
    /// <param name="value">The <see cref="StyleTextDecorationStyle" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>text-decoration-style</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleTextDecorationStyle? value) => value?.Value ?? string.Empty;
}
