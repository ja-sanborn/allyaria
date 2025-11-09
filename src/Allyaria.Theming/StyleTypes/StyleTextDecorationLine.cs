namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>text-decoration-line</c> value within the Allyaria theming system. Provides a strongly typed
/// wrapper for defining the decoration lines applied to text (e.g., underline, overline, line-through).
/// </summary>
public sealed record StyleTextDecorationLine : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleTextDecorationLine" /> record using the specified <see cref="Kind" />
    /// value.
    /// </summary>
    /// <param name="kind">The text decoration line combination to represent.</param>
    public StyleTextDecorationLine(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>
    /// Defines the supported CSS <c>text-decoration-line</c> property values. Multiple decorations can be represented by
    /// combining values.
    /// </summary>
    public enum Kind
    {
        /// <summary>Applies all standard text decorations: overline, line-through, and underline.</summary>
        [Description(description: "overline line-through underline")]
        All,

        /// <summary>
        /// Draws a line through the middle of the text (commonly used to indicate deleted or discounted content).
        /// </summary>
        [Description(description: "line-through")]
        LineThrough,

        /// <summary>Removes any text decorations from the element.</summary>
        [Description(description: "none")]
        None,

        /// <summary>Draws a line above the text (overline).</summary>
        [Description(description: "overline")]
        Overline,

        /// <summary>Draws both an overline and a line-through on the text.</summary>
        [Description(description: "overline line-through")]
        OverlineLineThrough,

        /// <summary>Draws both an overline and an underline on the text.</summary>
        [Description(description: "overline underline")]
        OverlineUnderline,

        /// <summary>Draws a line beneath the text (underline).</summary>
        [Description(description: "underline")]
        Underline,

        /// <summary>Draws both an underline and a line-through on the text.</summary>
        [Description(description: "underline line-through")]
        UnderlineLineThrough
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>text-decoration-line</c> value into a <see cref="StyleTextDecorationLine" />
    /// instance.
    /// </summary>
    /// <param name="value">The string representation of the text-decoration-line value.</param>
    /// <returns>A new <see cref="StyleTextDecorationLine" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleTextDecorationLine Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleTextDecorationLine(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleTextDecorationLine" /> instance.</summary>
    /// <param name="value">The string representation of the text-decoration-line value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleTextDecorationLine" /> instance or
    /// <see langword="null" /> if parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleTextDecorationLine? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleTextDecorationLine" /> instance.</summary>
    /// <param name="value">The string representation of the text-decoration-line value.</param>
    /// <returns>A <see cref="StyleTextDecorationLine" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleTextDecorationLine(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleTextDecorationLine" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleTextDecorationLine" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>text-decoration-line</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleTextDecorationLine? value) => (value?.Value).OrDefaultIfEmpty();
}
