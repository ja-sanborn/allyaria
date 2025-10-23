namespace Allyaria.Theming.Themes;

public sealed record ThemeGroupTypography(
    StyleValueString? FontFamily = null,
    StyleValueNumber? FontSize = null,
    StyleValueString? FontStyle = null,
    StyleValueString? FontWeight = null,
    StyleValueNumber? LetterSpacing = null,
    StyleValueNumber? LineHeight = null,
    StyleValueString? TextAlign = null,
    StyleValueString? TextDecorationLine = null,
    StyleValueString? TextDecorationStyle = null,
    StyleValueString? TextTransform = null,
    StyleValueString? VerticalAlign = null,
    StyleValueString? WhiteSpace = null,
    StyleValueString? WritingMode = null
) : IThemeGroup
{
    public static readonly ThemeGroupTypography Empty = new();

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder
            .Add("font-family", FontFamily, varPrefix)
            .Add("font-size", FontSize, varPrefix)
            .Add("font-style", FontStyle, varPrefix)
            .Add("font-weight", FontWeight, varPrefix)
            .Add("letter-spacing", LetterSpacing, varPrefix)
            .Add("line-height", LineHeight, varPrefix)
            .Add("text-align", TextAlign, varPrefix)
            .Add("text-decoration-line", TextDecorationLine, varPrefix)
            .Add("text-decoration-style", TextDecorationStyle, varPrefix)
            .Add("text-transform", TextTransform, varPrefix)
            .Add("vertical-align", VerticalAlign, varPrefix)
            .Add("white-space", WhiteSpace, varPrefix)
            .Add("writing-mode", WritingMode, varPrefix);

        return builder;
    }

    public static ThemeGroupTypography FromDefault(string? fontFamily = "")
        => new(
            string.IsNullOrWhiteSpace(fontFamily)
                ? ThemingDefaults.FontFamily
                : new StyleValueString(fontFamily.Trim()),
            Sizing.Size3,
            Constants.FontStyle.Normal,
            Constants.FontWeight.Normal,
            new StyleValueNumber("0.5px"),
            new StyleValueNumber("1.5"),
            TextDecorationLine: Constants.TextDecorationLine.None,
            TextDecorationStyle: Constants.TextDecorationStyle.Solid,
            TextTransform: TextTransformation.None,
            WhiteSpace: WhiteSpacing.Normal
        );

    public ThemeGroupTypography Merge(ThemeGroupTypography other)
        => SetFontFamily(other.FontFamily ?? FontFamily)
            .SetFontSize(other.FontSize ?? FontSize)
            .SetFontStyle(other.FontStyle ?? FontStyle)
            .SetFontWeight(other.FontWeight ?? FontWeight)
            .SetLetterSpacing(other.LetterSpacing ?? LetterSpacing)
            .SetLineHeight(other.LineHeight ?? LineHeight)
            .SetTextAlign(other.TextAlign ?? TextAlign)
            .SetTextDecorationLine(other.TextDecorationLine ?? TextDecorationLine)
            .SetTextDecorationStyle(other.TextDecorationStyle ?? TextDecorationStyle)
            .SetTextTransform(other.TextTransform ?? TextTransform)
            .SetVerticalAlign(other.VerticalAlign ?? VerticalAlign)
            .SetWhiteSpace(other.WhiteSpace ?? WhiteSpace)
            .SetWritingMode(other.WritingMode ?? WritingMode);

    public ThemeGroupTypography SetFontFamily(StyleValueString? value)
        => this with
        {
            FontFamily = value
        };

    public ThemeGroupTypography SetFontSize(StyleValueNumber? value)
        => this with
        {
            FontSize = value
        };

    public ThemeGroupTypography SetFontStyle(StyleValueString? value)
        => this with
        {
            FontStyle = value
        };

    public ThemeGroupTypography SetFontWeight(StyleValueString? value)
        => this with
        {
            FontWeight = value
        };

    public ThemeGroupTypography SetLetterSpacing(StyleValueNumber? value)
        => this with
        {
            LetterSpacing = value
        };

    public ThemeGroupTypography SetLineHeight(StyleValueNumber? value)
        => this with
        {
            LineHeight = value
        };

    public ThemeGroupTypography SetTextAlign(StyleValueString? value)
        => this with
        {
            TextAlign = value
        };

    public ThemeGroupTypography SetTextDecorationLine(StyleValueString? value)
        => this with
        {
            TextDecorationLine = value
        };

    public ThemeGroupTypography SetTextDecorationStyle(StyleValueString? value)
        => this with
        {
            TextDecorationStyle = value
        };

    public ThemeGroupTypography SetTextTransform(StyleValueString? value)
        => this with
        {
            TextTransform = value
        };

    public ThemeGroupTypography SetVerticalAlign(StyleValueString? value)
        => this with
        {
            VerticalAlign = value
        };

    public ThemeGroupTypography SetWhiteSpace(StyleValueString? value)
        => this with
        {
            WhiteSpace = value
        };

    public ThemeGroupTypography SetWritingMode(StyleValueString? value)
        => this with
        {
            WritingMode = value
        };
}
