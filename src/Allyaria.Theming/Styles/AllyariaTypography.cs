using Allyaria.Theming.Contracts;
using Allyaria.Theming.Values;
using System.Text;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Strongly typed typography definition for Allyaria theming. Provides conversion to inline CSS declarations and CSS
/// custom properties (variables). All members are optional; when a value is <see langword="null" />, it is treated as
/// unset and omitted from <see cref="ToCss" /> and <see cref="ToCssVars(string)" />.
/// </summary>
public readonly record struct AllyariaTypography
{
    /// <summary>
    /// Initializes a new <see cref="AllyariaTypography" /> with optional values. Pass <see langword="null" /> for any member
    /// to leave it unset.
    /// </summary>
    /// <param name="fontFamily">Optional font family token.</param>
    /// <param name="fontSize">Optional font size token.</param>
    /// <param name="fontStyle">Optional font style token.</param>
    /// <param name="fontWeight">Optional font weight token.</param>
    /// <param name="letterSpacing">Optional letter spacing token.</param>
    /// <param name="lineHeight">Optional line height token.</param>
    /// <param name="textAlign">Optional text align token.</param>
    /// <param name="textDecoration">Optional text decoration token.</param>
    /// <param name="textTransform">Optional text transform token.</param>
    /// <param name="verticalAlign">Optional vertical align token.</param>
    /// <param name="wordSpacing">Optional word spacing token.</param>
    public AllyariaTypography(AllyariaStringValue? fontFamily = null,
        AllyariaNumberValue? fontSize = null,
        AllyariaStringValue? fontStyle = null,
        AllyariaStringValue? fontWeight = null,
        AllyariaNumberValue? letterSpacing = null,
        AllyariaNumberValue? lineHeight = null,
        AllyariaStringValue? textAlign = null,
        AllyariaStringValue? textDecoration = null,
        AllyariaStringValue? textTransform = null,
        AllyariaStringValue? verticalAlign = null,
        AllyariaNumberValue? wordSpacing = null)
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

    /// <summary>
    /// Gets or initializes the font family (e.g., <c>Inter, "Segoe UI", sans-serif</c>), or <see langword="null" /> when
    /// unset.
    /// </summary>
    public AllyariaStringValue? FontFamily { get; init; }

    /// <summary>
    /// Gets or initializes the font size (e.g., <c>14px</c>, <c>1rem</c>, <c>clamp(...)</c>), or <see langword="null" /> when
    /// unset.
    /// </summary>
    public AllyariaNumberValue? FontSize { get; init; }

    /// <summary>
    /// Gets or initializes the font style (e.g., <c>normal</c>, <c>italic</c>), or <see langword="null" /> when unset.
    /// </summary>
    public AllyariaStringValue? FontStyle { get; init; }

    /// <summary>
    /// Gets or initializes the font weight (e.g., <c>400</c>, <c>bold</c>, <c>600</c>), or <see langword="null" /> when unset.
    /// </summary>
    public AllyariaStringValue? FontWeight { get; init; }

    /// <summary>Gets or initializes the letter spacing (e.g., <c>0.02em</c>), or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? LetterSpacing { get; init; }

    /// <summary>
    /// Gets or initializes the line height (e.g., <c>1.5</c>, <c>24px</c>), or <see langword="null" /> when unset.
    /// </summary>
    public AllyariaNumberValue? LineHeight { get; init; }

    /// <summary>
    /// Gets or initializes the text alignment (e.g., <c>start</c>, <c>left</c>, <c>center</c>), or <see langword="null" />
    /// when unset.
    /// </summary>
    public AllyariaStringValue? TextAlign { get; init; }

    /// <summary>
    /// Gets or initializes the text decoration (e.g., <c>underline</c>, <c>none</c>), or <see langword="null" /> when unset.
    /// </summary>
    public AllyariaStringValue? TextDecoration { get; init; }

    /// <summary>
    /// Gets or initializes the text transform (e.g., <c>uppercase</c>, <c>none</c>), or <see langword="null" /> when unset.
    /// </summary>
    public AllyariaStringValue? TextTransform { get; init; }

    /// <summary>
    /// Gets or initializes the vertical alignment (e.g., <c>baseline</c>, <c>middle</c>), or <see langword="null" /> when
    /// unset.
    /// </summary>
    public AllyariaStringValue? VerticalAlign { get; init; }

    /// <summary>Gets or initializes the word spacing (e.g., <c>0.1em</c>), or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? WordSpacing { get; init; }

    /// <summary>Appends a CSS declaration to the builder when the provided value is non-null.</summary>
    /// <param name="builder">The <see cref="StringBuilder" /> receiving the CSS declaration.</param>
    /// <param name="value">The optional <see cref="AllyariaStringValue" /> to emit.</param>
    /// <param name="propertyName">The CSS property name to emit.</param>
    private static void AppendIfNotNull(StringBuilder builder, ValueBase? value, string propertyName)
    {
        if (value is not null)
        {
            builder.Append(value.ToCss(propertyName));
        }
    }

    /// <summary>
    /// Creates a new <see cref="AllyariaTypography" /> by non-destructively applying the provided overrides. Any parameter
    /// left <see langword="null" /> retains the current value (which may also be <see langword="null" />).
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
        AllyariaNumberValue? fontSize = null,
        AllyariaStringValue? fontStyle = null,
        AllyariaStringValue? fontWeight = null,
        AllyariaNumberValue? letterSpacing = null,
        AllyariaNumberValue? lineHeight = null,
        AllyariaStringValue? textAlign = null,
        AllyariaStringValue? textDecoration = null,
        AllyariaStringValue? textTransform = null,
        AllyariaStringValue? verticalAlign = null,
        AllyariaNumberValue? wordSpacing = null)
        => new()
        {
            FontFamily = fontFamily ?? FontFamily,
            FontSize = fontSize ?? FontSize,
            FontStyle = fontStyle ?? FontStyle,
            FontWeight = fontWeight ?? FontWeight,
            LetterSpacing = letterSpacing ?? LetterSpacing,
            LineHeight = lineHeight ?? LineHeight,
            TextAlign = textAlign ?? TextAlign,
            TextDecoration = textDecoration ?? TextDecoration,
            TextTransform = textTransform ?? TextTransform,
            VerticalAlign = verticalAlign ?? VerticalAlign,
            WordSpacing = wordSpacing ?? WordSpacing
        };

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
    /// normalized by collapsing runs of whitespace/dashes, trimming leading/trailing dashes, and converting to lowercase. If
    /// empty after normalization, the default <c>--aa-</c> prefix is used; otherwise variables emit as <c>--{normalized}-*</c>
    /// . Only non-null members are emitted.
    /// </summary>
    /// <param name="prefix">Optional namespace used for CSS variable names (e.g., <c>editor</c>).</param>
    /// <returns>A concatenated CSS variables string containing only non-null typography properties.</returns>
    public string ToCssVars(string prefix = "")
    {
        var basePrefix = Regex.Replace(prefix, @"[\s-]+", "-").Trim('-').ToLowerInvariant();

        basePrefix = string.IsNullOrWhiteSpace(basePrefix)
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
