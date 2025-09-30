using Allyaria.Theming.Values;
using System.Text;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Strongly typed typography definition for Allyaria theming. Provides conversion to inline CSS declarations and CSS
/// custom properties (variables).
/// </summary>
/// <param name="FontFamily">Font family token (e.g., <c>Inter, &quot;Segoe UI&quot;, sans-serif</c>).</param>
/// <param name="FontSize">Font size token (e.g., <c>14px</c>, <c>1rem</c>, <c>clamp(...)</c>).</param>
/// <param name="FontStyle">Font style token (e.g., <c>normal</c>, <c>italic</c>).</param>
/// <param name="FontWeight">Font weight token (e.g., <c>400</c>, <c>bold</c>, <c>600</c>).</param>
/// <param name="LetterSpacing">Letter spacing token (e.g., <c>0.02em</c>).</param>
/// <param name="LineHeight">Line height token (e.g., <c>1.5</c>, <c>24px</c>).</param>
/// <param name="TextAlign">Text align token (e.g., <c>start</c>, <c>left</c>, <c>center</c>).</param>
/// <param name="TextDecoration">Text decoration token (e.g., <c>underline</c>, <c>none</c>).</param>
/// <param name="TextTransform">Text transform token (e.g., <c>uppercase</c>, <c>none</c>).</param>
/// <param name="VerticalAlign">Vertical align token (e.g., <c>baseline</c>, <c>middle</c>).</param>
/// <param name="WordSpacing">Word spacing token (e.g., <c>0.1em</c>).</param>
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
    /// <summary>Appends a CSS declaration to the builder when the provided value is non-null.</summary>
    /// <param name="builder">The <see cref="StringBuilder" /> receiving the CSS declaration.</param>
    /// <param name="value">The optional <see cref="AllyariaStringValue" /> to emit.</param>
    /// <param name="propertyName">The CSS property name to emit.</param>
    private static void AppendIfNotNull(StringBuilder builder, AllyariaStringValue? value, string propertyName)
    {
        if (value is not null)
        {
            builder.Append(value.ToCss(propertyName));
        }
    }

    /// <summary>
    /// Creates a new <see cref="AllyariaTypography" /> by non-destructively applying the provided overrides. Any parameter
    /// left <see langword="null" /> retains the value from the current instance.
    /// </summary>
    /// <param name="fontFamily">Optional override for <see cref="FontFamily" />.</param>
    /// <param name="fontSize">Optional override for <see cref="FontSize" />.</param>
    /// <param name="fontStyle">Optional override for <see cref="FontStyle" />.</param>
    /// <param name="fontWeight">Optional override for <see cref="FontWeight" />.</param>
    /// <param name="letterSpacing">Optional override for <see cref="LetterSpacing" />.</param>
    /// <param name="lineHeight">Optional override for <see cref="LineHeight" />.</param>
    /// <param name="textAlign">Optional override for <see cref="TextAlign" />.</param>
    /// <param name="textDecoration">Optional override for <see cref="TextDecoration" />.</param>
    /// <param name="textTransform">Optional override for <see cref="TextTransform" />.</param>
    /// <param name="verticalAlign">Optional override for <see cref="VerticalAlign" />.</param>
    /// <param name="wordSpacing">Optional override for <see cref="WordSpacing" />.</param>
    /// <returns>A new <see cref="AllyariaTypography" /> instance with combined values.</returns>
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

    /// <summary>Builds a CSS style string representing this typography. Only non-null properties are emitted.</summary>
    /// <returns>A concatenated CSS string of property declarations.</returns>
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
    /// Builds a CSS custom properties (variables) string for this typography. The optional <paramref name="prefix" /> is
    /// normalized by collapsing runs of whitespace/dashes to a single dash, trimming leading/trailing dashes, and converting
    /// to lowercase. If the provided <paramref name="prefix" /> is null or whitespace, variables are emitted using the default
    /// <c>--aa-</c> prefix; otherwise, the normalized value is used and prefixed as <c>--{normalized}-</c>.
    /// </summary>
    /// <param name="prefix">
    /// Optional namespace used for CSS variable names (e.g., <c>editor</c>). It may contain spaces or dashes; normalization is
    /// applied as described above.
    /// </param>
    /// <returns>
    /// A concatenated CSS variables string containing only non-null typography properties (e.g., <c>--{prefix}-font-family</c>
    /// , <c>--{prefix}-line-height</c>).
    /// </returns>
    /// <remarks>
    /// Each variable is appended only when its corresponding property is non-null, keeping the output concise.
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
