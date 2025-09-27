using Allyaria.Theming.Values;
using System.Text;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a strongly typed typography definition for Allyaria theming. Provides conversion to CSS inline styles or CSS
/// variables.
/// </summary>
/// <param name="FontFamily">The font family to use (e.g., <c>"Inter, Segoe UI, sans-serif"</c>).</param>
/// <param name="FontSize">The font size.</param>
/// <param name="FontStyle">The font style (e.g., normal, italic).</param>
/// <param name="FontWeight">The font weight (e.g., bold, 400).</param>
/// <param name="LetterSpacing">The letter spacing.</param>
/// <param name="LineHeight">The line height.</param>
/// <param name="TextAlign">The text alignment.</param>
/// <param name="TextDecoration">The text decoration.</param>
/// <param name="TextTransform">The text transform (e.g., uppercase).</param>
/// <param name="VerticalAlign">The vertical alignment.</param>
/// <param name="WordSpacing">The word spacing.</param>
public readonly record struct AllyariaTypography(
    AllyariaStringValue? FontFamily = null,
    AllyariaStringValue? FontSize = null,
    AllyariaStringValue? FontStyle = null,
    AllyariaStringValue? FontWeight = null,
    AllyariaStringValue? LetterSpacing = null,
    AllyariaStringValue? LineHeight = null,
    AllyariaStringValue? TextAlign = null,
    AllyariaStringValue? TextDecoration = null,
    AllyariaStringValue? TextTransform = null,
    AllyariaStringValue? VerticalAlign = null,
    AllyariaStringValue? WordSpacing = null
)
{
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
    /// Creates a new <see cref="AllyariaTypography" /> instance by cascading the current values with the provided overrides.
    /// Any parameter left <see langword="null" /> will keep the existing value from this instance.
    /// </summary>
    /// <param name="fontFamily">Optional override for <see cref="AllyariaTypography.FontFamily" />.</param>
    /// <param name="fontSize">Optional override for <see cref="AllyariaTypography.FontSize" />.</param>
    /// <param name="fontStyle">Optional override for <see cref="AllyariaTypography.FontStyle" />.</param>
    /// <param name="fontWeight">Optional override for <see cref="AllyariaTypography.FontWeight" />.</param>
    /// <param name="letterSpacing">Optional override for <see cref="AllyariaTypography.LetterSpacing" />.</param>
    /// <param name="lineHeight">Optional override for <see cref="AllyariaTypography.LineHeight" />.</param>
    /// <param name="textAlign">Optional override for <see cref="AllyariaTypography.TextAlign" />.</param>
    /// <param name="textDecoration">Optional override for <see cref="AllyariaTypography.TextDecoration" />.</param>
    /// <param name="textTransform">Optional override for <see cref="AllyariaTypography.TextTransform" />.</param>
    /// <param name="verticalAlign">Optional override for <see cref="AllyariaTypography.VerticalAlign" />.</param>
    /// <param name="wordSpacing">Optional override for <see cref="AllyariaTypography.WordSpacing" />.</param>
    /// <returns>A new <see cref="AllyariaTypography" /> instance with the combined values.</returns>
    public AllyariaTypography Cascade(AllyariaStringValue? fontFamily = null,
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
        var newFontFamily = fontFamily ?? FontFamily;
        var newFontSize = fontSize ?? FontSize;
        var newFontStyle = fontStyle ?? FontStyle;
        var newFontWeight = fontWeight ?? FontWeight;
        var newLetterSpacing = letterSpacing ?? LetterSpacing;
        var newLineHeight = lineHeight ?? LineHeight;
        var newTextAlign = textAlign ?? TextAlign;
        var newTextDecoration = textDecoration ?? TextDecoration;
        var newTextTransform = textTransform ?? TextTransform;
        var newVerticalAlign = verticalAlign ?? VerticalAlign;
        var newWordSpacing = wordSpacing ?? WordSpacing;

        return new AllyariaTypography
        {
            FontFamily = newFontFamily,
            FontSize = newFontSize,
            FontStyle = newFontStyle,
            FontWeight = newFontWeight,
            LetterSpacing = newLetterSpacing,
            LineHeight = newLineHeight,
            TextAlign = newTextAlign,
            TextDecoration = newTextDecoration,
            TextTransform = newTextTransform,
            VerticalAlign = newVerticalAlign,
            WordSpacing = newWordSpacing
        };
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
        var basePrefix = Regex.Replace(prefix, @"[\s-]+", "-").Trim('-').ToLowerInvariant();

        basePrefix = string.IsNullOrWhiteSpace(prefix)
            ? "--aa-"
            : $"--{basePrefix}-";

        var builder = new StringBuilder();

        AppendIfNotNull(builder, FontFamily, $"{basePrefix}font-family");
        AppendIfNotNull(builder, FontSize, $"{basePrefix}font-size");
        AppendIfNotNull(builder, FontStyle, $"{basePrefix}font-style");
        AppendIfNotNull(builder, FontWeight, $"{basePrefix}font-weight");
        AppendIfNotNull(builder, LetterSpacing, $"{basePrefix}letter-spacing");
        AppendIfNotNull(builder, LineHeight, $"{basePrefix}line-height");
        AppendIfNotNull(builder, TextAlign, $"{basePrefix}text-align");
        AppendIfNotNull(builder, TextDecoration, $"{basePrefix}text-decoration");
        AppendIfNotNull(builder, TextTransform, $"{basePrefix}text-transform");
        AppendIfNotNull(builder, VerticalAlign, $"{basePrefix}vertical-align");
        AppendIfNotNull(builder, WordSpacing, $"{basePrefix}word-spacing");

        return builder.ToString();
    }
}
