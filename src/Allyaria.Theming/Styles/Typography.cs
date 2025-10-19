namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a strongly typed set of typography tokens (font family, size, weight, spacing, alignment, and decoration)
/// for the Allyaria theming system. Values are immutable after initialization and default to <see cref="StyleDefaults" />
/// when not explicitly provided.
/// </summary>
public readonly record struct Typography
{
    /// <summary>
    /// Initializes a new <see cref="Typography" /> using the <see cref="StyleDefaults" /> values for all members.
    /// </summary>
    public Typography()
        : this(null) { }

    /// <summary>
    /// Initializes a new <see cref="Typography" /> with optional values. Any parameter left as <see langword="null" /> falls
    /// back to <see cref="StyleDefaults" /> for that member.
    /// </summary>
    /// <param name="fontFamily">Optional font family token.</param>
    /// <param name="fontSize">Optional font size token.</param>
    /// <param name="fontStyle">Optional font style token.</param>
    /// <param name="fontWeight">Optional font weight token.</param>
    /// <param name="letterSpacing">Optional letter spacing token.</param>
    /// <param name="lineHeight">Optional line height token.</param>
    /// <param name="textAlign">Optional text align token.</param>
    /// <param name="textDecorationLine">Optional text decoration line token.</param>
    /// <param name="textDecorationStyle">Optional text decoration style token.</param>
    /// <param name="textTransform">Optional text transform token.</param>
    /// <param name="verticalAlign">Optional vertical align token.</param>
    public Typography(ThemeString? fontFamily = null,
        ThemeNumber? fontSize = null,
        ThemeString? fontStyle = null,
        ThemeString? fontWeight = null,
        ThemeNumber? letterSpacing = null,
        ThemeNumber? lineHeight = null,
        ThemeString? textAlign = null,
        ThemeString? textDecorationLine = null,
        ThemeString? textDecorationStyle = null,
        ThemeString? textTransform = null,
        ThemeString? verticalAlign = null)
    {
        FontFamily = fontFamily ?? StyleDefaults.FontFamily;
        FontSize = fontSize ?? StyleDefaults.FontSize;
        FontStyle = fontStyle ?? StyleDefaults.FontStyle;
        FontWeight = fontWeight ?? StyleDefaults.FontWeight;
        LetterSpacing = letterSpacing ?? StyleDefaults.LetterSpacing;
        LineHeight = lineHeight ?? StyleDefaults.LineHeight;
        TextAlign = textAlign ?? StyleDefaults.TextAlign;
        TextDecorationLine = textDecorationLine ?? StyleDefaults.TextDecorationLine;
        TextDecorationStyle = textDecorationStyle ?? StyleDefaults.TextDecorationStyle;
        TextTransform = textTransform ?? StyleDefaults.TextTransform;
        VerticalAlign = verticalAlign ?? StyleDefaults.VerticalAlign;
    }

    /// <summary>Gets or initializes the font family (e.g., <c>Inter, "Segoe UI", sans-serif</c>).</summary>
    public ThemeString FontFamily { get; init; }

    /// <summary>Gets or initializes the font size (e.g., <c>14px</c>, <c>1rem</c>, <c>clamp(...)</c>).</summary>
    public ThemeNumber FontSize { get; init; }

    /// <summary>Gets or initializes the font style (e.g., <c>normal</c>, <c>italic</c>).</summary>
    public ThemeString FontStyle { get; init; }

    /// <summary>Gets or initializes the font weight (e.g., <c>400</c>, <c>bold</c>, <c>600</c>).</summary>
    public ThemeString FontWeight { get; init; }

    /// <summary>Gets or initializes the letter spacing (e.g., <c>0.02em</c>).</summary>
    public ThemeNumber LetterSpacing { get; init; }

    /// <summary>Gets or initializes the line height (e.g., <c>1.5</c>, <c>24px</c>).</summary>
    public ThemeNumber LineHeight { get; init; }

    /// <summary>Gets or initializes the text alignment (e.g., <c>start</c>, <c>left</c>, <c>center</c>).</summary>
    public ThemeString TextAlign { get; init; }

    /// <summary>Gets or initializes the text decoration line (e.g., <c>underline</c>, <c>none</c>).</summary>
    public ThemeString TextDecorationLine { get; init; }

    /// <summary>Gets or initializes the text decoration style (e.g., <c>solid</c>, <c>wavy</c>).</summary>
    public ThemeString TextDecorationStyle { get; init; }

    /// <summary>Gets or initializes the text transform (e.g., <c>uppercase</c>, <c>none</c>).</summary>
    public ThemeString TextTransform { get; init; }

    /// <summary>Gets or initializes the vertical alignment (e.g., <c>baseline</c>, <c>middle</c>).</summary>
    public ThemeString VerticalAlign { get; init; }

    /// <summary>
    /// Creates a new <see cref="Typography" /> by non-destructively applying the provided overrides. Any parameter left
    /// <see langword="null" /> retains the current theme.
    /// </summary>
    /// <param name="fontFamily">Optional override for <see cref="FontFamily" />.</param>
    /// <param name="fontSize">Optional override for <see cref="FontSize" />.</param>
    /// <param name="fontStyle">Optional override for <see cref="FontStyle" />.</param>
    /// <param name="fontWeight">Optional override for <see cref="FontWeight" />.</param>
    /// <param name="letterSpacing">Optional override for <see cref="LetterSpacing" />.</param>
    /// <param name="lineHeight">Optional override for <see cref="LineHeight" />.</param>
    /// <param name="textAlign">Optional override for <see cref="TextAlign" />.</param>
    /// <param name="textDecorationLine">Optional override for <see cref="TextDecorationLine" />.</param>
    /// <param name="textDecorationStyle">Optional override for <see cref="TextDecorationStyle" />.</param>
    /// <param name="textTransform">Optional override for <see cref="TextTransform" />.</param>
    /// <param name="verticalAlign">Optional override for <see cref="VerticalAlign" />.</param>
    /// <returns>A new <see cref="Typography" /> instance with combined values.</returns>
    public Typography Cascade(ThemeString? fontFamily = null,
        ThemeNumber? fontSize = null,
        ThemeString? fontStyle = null,
        ThemeString? fontWeight = null,
        ThemeNumber? letterSpacing = null,
        ThemeNumber? lineHeight = null,
        ThemeString? textAlign = null,
        ThemeString? textDecorationLine = null,
        ThemeString? textDecorationStyle = null,
        ThemeString? textTransform = null,
        ThemeString? verticalAlign = null)
        => this with
        {
            FontFamily = fontFamily ?? FontFamily,
            FontSize = fontSize ?? FontSize,
            FontStyle = fontStyle ?? FontStyle,
            FontWeight = fontWeight ?? FontWeight,
            LetterSpacing = letterSpacing ?? LetterSpacing,
            LineHeight = lineHeight ?? LineHeight,
            TextAlign = textAlign ?? TextAlign,
            TextDecorationLine = textDecorationLine ?? TextDecorationLine,
            TextDecorationStyle = textDecorationStyle ?? TextDecorationStyle,
            TextTransform = textTransform ?? TextTransform,
            VerticalAlign = verticalAlign ?? VerticalAlign
        };

    /// <summary>Builds a CSS declaration block from the current typography values.</summary>
    /// <param name="varPrefix">
    /// Optional prefix for generating CSS custom properties. When provided, each property name is emitted as
    /// <c>--{varPrefix}-[propertyName]</c>. Hyphens and whitespace in the prefix are normalized; case is lowered.
    /// </param>
    /// <param name="includeSize">Include the font size in the CSS output.</param>
    /// <returns>
    /// A CSS string containing zero or more declarations (each ending as produced by
    /// <see cref="StyleHelper.ToCss(StringBuilder, ThemeBase, string, string)" />).
    /// </returns>
    public string ToCss(string? varPrefix = "", bool includeSize = true)
    {
        var builder = new StringBuilder();

        if (includeSize)
        {
            builder.ToCss(FontSize, "font-size", varPrefix);
        }

        builder.ToCss(FontFamily, "font-family", varPrefix);
        builder.ToCss(FontStyle, "font-style", varPrefix);
        builder.ToCss(FontWeight, "font-weight", varPrefix);
        builder.ToCss(LetterSpacing, "letter-spacing", varPrefix);
        builder.ToCss(LineHeight, "line-height", varPrefix);
        builder.ToCss(TextAlign, "text-align", varPrefix);
        builder.ToCss(TextDecorationLine, "text-decoration-line", varPrefix);
        builder.ToCss(TextDecorationStyle, "text-decoration-style", varPrefix);
        builder.ToCss(TextTransform, "text-transform", varPrefix);
        builder.ToCss(VerticalAlign, "vertical-align", varPrefix);

        return builder.ToString();
    }
}
