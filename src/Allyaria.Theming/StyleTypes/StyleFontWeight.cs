namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>font-weight</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// defining typographic weight (thickness or boldness) of text.
/// </summary>
public sealed record StyleFontWeight : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleFontWeight" /> record using the specified <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The font weight kind that determines how bold or light the text is rendered.</param>
    public StyleFontWeight(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>font-weight</c> property values.</summary>
    public enum Kind
    {
        /// <summary>Specifies bold text, typically equivalent to a numeric weight of 700.</summary>
        [Description(description: "bold")]
        Bold,

        /// <summary>Specifies a font weight bolder than the parent element’s weight.</summary>
        [Description(description: "bolder")]
        Bolder,

        /// <summary>Specifies a font weight lighter than the parent element’s weight.</summary>
        [Description(description: "lighter")]
        Lighter,

        /// <summary>Specifies a normal font weight, typically equivalent to a numeric weight of 400.</summary>
        [Description(description: "normal")]
        Normal,

        /// <summary>Specifies a thin font weight (100).</summary>
        [Description(description: "100")]
        Weight100,

        /// <summary>Specifies an extra-light font weight (200).</summary>
        [Description(description: "200")]
        Weight200,

        /// <summary>Specifies a light font weight (300).</summary>
        [Description(description: "300")]
        Weight300,

        /// <summary>Specifies a normal font weight (400).</summary>
        [Description(description: "400")]
        Weight400,

        /// <summary>Specifies a medium font weight (500).</summary>
        [Description(description: "500")]
        Weight500,

        /// <summary>Specifies a semi-bold font weight (600).</summary>
        [Description(description: "600")]
        Weight600,

        /// <summary>Specifies a bold font weight (700).</summary>
        [Description(description: "700")]
        Weight700,

        /// <summary>Specifies an extra-bold font weight (800).</summary>
        [Description(description: "800")]
        Weight800,

        /// <summary>Specifies a black (heaviest) font weight (900).</summary>
        [Description(description: "900")]
        Weight900
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>font-weight</c> value into a <see cref="StyleFontWeight" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the font-weight value.</param>
    /// <returns>A new <see cref="StyleFontWeight" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleFontWeight Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleFontWeight(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleFontWeight" /> instance.</summary>
    /// <param name="value">The string representation of the font-weight value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleFontWeight" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleFontWeight? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleFontWeight" /> instance.</summary>
    /// <param name="value">The string representation of the font-weight value.</param>
    /// <returns>A <see cref="StyleFontWeight" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleFontWeight(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleFontWeight" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleFontWeight" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>font-weight</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleFontWeight? value) => (value?.Value).OrDefaultIfEmpty();
}
