using Allyaria.Theming.Values;
using System.Text;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a strongly typed typography definition for Allyaria theming. Provides conversion to CSS inline styles or CSS
/// variables.
/// </summary>
public readonly record struct AllyariaTypography
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaTypography" /> struct.</summary>
    /// <param name="fontFamily">The font family to use (e.g., <c>"Inter, Segoe UI, sans-serif"</c>).</param>
    /// <param name="fontSize">The font size.</param>
    /// <param name="fontStyle">The font style (e.g., normal, italic).</param>
    /// <param name="fontWeight">The font weight (e.g., bold, 400).</param>
    /// <param name="letterSpacing">The letter spacing.</param>
    /// <param name="lineHeight">The line height.</param>
    /// <param name="textAlign">The text alignment.</param>
    /// <param name="textDecoration">The text decoration.</param>
    /// <param name="textTransform">The text transform (e.g., uppercase).</param>
    /// <param name="verticalAlign">The vertical alignment.</param>
    /// <param name="wordSpacing">The word spacing.</param>
    public AllyariaTypography(AllyariaStringValue? fontFamily = null,
        AllyariaStringValue? fontSize = null,
        AllyariaStringValue? fontStyle = null,
        AllyariaStringValue? fontWeight = null,
        AllyariaStringValue? letterSpacing = null,
        AllyariaStringValue? lineHeight = null,
        AllyariaStringValue? textAlign = null,
        AllyariaStringValue? textDecoration = null,
        AllyariaStringValue? textTransform = null,
        AllyariaStringValue? verticalAlign = null,
        AllyariaStringValue? wordSpacing = null)
    {
        FontFamily = fontFamily;
        FontSize = fontSize;
        FontStyle = fontStyle;
        FontWeight = fontWeight;
        LetterSpacing = letterSpacing;
        LineHeight = lineHeight;
        TextAlign = textAlign;
        TextDecoration = textDecoration;
        TextTransform = textTransform;
        VerticalAlign = verticalAlign;
        WordSpacing = wordSpacing;
    }

    /// <summary>Gets the font family.</summary>
    private AllyariaStringValue? FontFamily { get; }

    /// <summary>Gets the font size.</summary>
    private AllyariaStringValue? FontSize { get; }

    /// <summary>Gets the font style.</summary>
    private AllyariaStringValue? FontStyle { get; }

    /// <summary>Gets the font weight.</summary>
    private AllyariaStringValue? FontWeight { get; }

    /// <summary>Gets the letter spacing.</summary>
    private AllyariaStringValue? LetterSpacing { get; }

    /// <summary>Gets the line height.</summary>
    private AllyariaStringValue? LineHeight { get; }

    /// <summary>Gets the text alignment.</summary>
    private AllyariaStringValue? TextAlign { get; }

    /// <summary>Gets the text decoration.</summary>
    private AllyariaStringValue? TextDecoration { get; }

    /// <summary>Gets the text transform.</summary>
    private AllyariaStringValue? TextTransform { get; }

    /// <summary>Gets the vertical alignment.</summary>
    private AllyariaStringValue? VerticalAlign { get; }

    /// <summary>Gets the word spacing.</summary>
    private AllyariaStringValue? WordSpacing { get; }

    /// <summary>Appends a CSS declaration to the builder if the value is not null.</summary>
    /// <param name="builder">The string builder to append to.</param>
    /// <param name="value">The optional string value.</param>
    /// <param name="propertyName">The CSS property name.</param>
    private static void AppendIfNotNull(StringBuilder builder, AllyariaStringValue? value, string propertyName)
    {
        if (value is not null)
        {
            builder.Append(value.ToCss(propertyName));
        }
    }

    /// <summary>
    /// Builds a CSS style string representing this typography. Only appends declarations for non-null properties.
    /// </summary>
    /// <returns>A concatenated CSS style string.</returns>
    public string ToCss()
    {
        var builder = new StringBuilder();

        AppendIfNotNull(builder, FontFamily, "font-family");
        AppendIfNotNull(builder, FontSize, "font-size");
        AppendIfNotNull(builder, FontStyle, "font-style");
        AppendIfNotNull(builder, FontWeight, "font-weight");
        AppendIfNotNull(builder, LetterSpacing, "letter-spacing");
        AppendIfNotNull(builder, LineHeight, "line-height");
        AppendIfNotNull(builder, TextAlign, "text-align");
        AppendIfNotNull(builder, TextDecoration, "text-decoration");
        AppendIfNotNull(builder, TextTransform, "text-transform");
        AppendIfNotNull(builder, VerticalAlign, "vertical-align");
        AppendIfNotNull(builder, WordSpacing, "word-spacing");

        return builder.ToString();
    }

    /// <summary>
    /// Builds a CSS custom properties (variables) string representing this typography. The method normalizes the optional
    /// <paramref name="prefix" /> by trimming whitespace and dashes, converting to lowercase, and replacing spaces with
    /// hyphens. If no usable prefix remains, variables are emitted with the default <c>--aa-</c> prefix; otherwise, the
    /// computed prefix is applied (e.g., <c>--mytheme-font-size</c>).
    /// </summary>
    /// <param name="prefix">
    /// An optional string used to namespace the CSS variables. May contain spaces or leading/trailing dashes, which are
    /// normalized before use. If empty or whitespace, defaults to <c>--aa-</c>.
    /// </param>
    /// <returns>
    /// A concatenated CSS variables string containing only non-null typography properties (e.g., <c>--{prefix}-font-family</c>
    /// , <c>--{prefix}-line-height</c>).
    /// </returns>
    /// <remarks>
    /// Each variable is only appended if its corresponding property is non-null. This keeps the resulting CSS concise and
    /// avoids redundant declarations.
    /// </remarks>
    public string ToCssVars(string prefix = "")
    {
        prefix = prefix.Trim().Trim('-').ToLowerInvariant().Replace(" ", "-");

        prefix = string.IsNullOrWhiteSpace(prefix)
            ? "--aa-"
            : $"--{prefix}-";

        var builder = new StringBuilder();

        AppendIfNotNull(builder, FontFamily, $"{prefix}font-family");
        AppendIfNotNull(builder, FontSize, $"{prefix}font-size");
        AppendIfNotNull(builder, FontStyle, $"{prefix}font-style");
        AppendIfNotNull(builder, FontWeight, $"{prefix}font-weight");
        AppendIfNotNull(builder, LetterSpacing, $"{prefix}letter-spacing");
        AppendIfNotNull(builder, LineHeight, $"{prefix}line-height");
        AppendIfNotNull(builder, TextAlign, $"{prefix}text-align");
        AppendIfNotNull(builder, TextDecoration, $"{prefix}text-decoration");
        AppendIfNotNull(builder, TextTransform, $"{prefix}text-transform");
        AppendIfNotNull(builder, VerticalAlign, $"{prefix}vertical-align");
        AppendIfNotNull(builder, WordSpacing, $"{prefix}word-spacing");

        return builder.ToString();
    }
}
