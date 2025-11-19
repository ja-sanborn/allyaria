namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>font-style</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// defining typographic style such as normal, italic, or oblique.
/// </summary>
public sealed record StyleFontStyle : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleFontStyle" /> record using the specified <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The font style kind that determines how text is rendered.</param>
    public StyleFontStyle(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>font-style</c> property values.</summary>
    public enum Kind
    {
        /// <summary>Renders text in an italicized form, typically using a distinct italic font face.</summary>
        [Description(description: "italic")]
        Italic,

        /// <summary>Renders text normally without slanting or italics.</summary>
        [Description(description: "normal")]
        Normal,

        /// <summary>Renders text slanted, typically when a true italic face is not available.</summary>
        [Description(description: "oblique")]
        Oblique
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>font-style</c> value into a <see cref="StyleFontStyle" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the font-style value.</param>
    /// <returns>A new <see cref="StyleFontStyle" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleFontStyle Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleFontStyle(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleFontStyle" /> instance.</summary>
    /// <param name="value">The string representation of the font-style value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleFontStyle" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleFontStyle? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleFontStyle" /> instance.</summary>
    /// <param name="value">The string representation of the font-style value.</param>
    /// <returns>A <see cref="StyleFontStyle" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleFontStyle(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleFontStyle" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleFontStyle" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>font-style</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleFontStyle? value) => (value?.Value).OrDefaultIfEmpty();
}
