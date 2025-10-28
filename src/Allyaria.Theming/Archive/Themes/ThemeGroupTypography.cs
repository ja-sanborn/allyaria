namespace Allyaria.Theming.Themes;

public sealed record ThemeGroupTypography(
    StyleValueString? FontFamily = null,
    StyleValueNumber? FontSize = null,
    StyleValueString? FontStyle = null,
    StyleValueString? FontWeight = null,
    StyleValueString? TextDecorationLine = null,
    StyleValueString? TextDecorationStyle = null,
    StyleValueNumber? TextDecorationThickness = null,
    StyleValueString? TextTransform = null
)
{
    public static readonly ThemeGroupTypography Empty = new();

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder
            .Add(propertyName: "font-family", value: FontFamily, varPrefix: varPrefix)
            .Add(propertyName: "font-size", value: FontSize, varPrefix: varPrefix)
            .Add(propertyName: "font-style", value: FontStyle, varPrefix: varPrefix)
            .Add(propertyName: "font-weight", value: FontWeight, varPrefix: varPrefix)
            .Add(propertyName: "text-decoration-line", value: TextDecorationLine, varPrefix: varPrefix)
            .Add(propertyName: "text-decoration-style", value: TextDecorationStyle, varPrefix: varPrefix)
            .Add(propertyName: "text-decoration-thickness", value: TextDecorationThickness, varPrefix: varPrefix)
            .Add(propertyName: "text-transform", value: TextTransform, varPrefix: varPrefix);

        return builder;
    }

    public static ThemeGroupTypography FromBrand(BrandFont brand, ThemeType themeType, FontType fontType)
        => new(
            FontFamily: brand.GetFont(themeType: themeType, fontType: fontType),
            FontSize: CssSize.Size2,
            FontWeight: CssFontWeight.Normal,
            FontStyle: CssFontStyle.Normal,
            TextDecorationLine: CssTextDecorationLine.None,
            TextDecorationStyle: CssTextDecorationStyle.Solid,
            TextDecorationThickness: CssSize.Thin,
            TextTransform: CssTextTransform.None
        );

    public ThemeGroupTypography Merge(ThemeGroupTypography other)
        => SetFontFamily(value: other.FontFamily ?? FontFamily)
            .SetFontSize(value: other.FontSize ?? FontSize)
            .SetFontStyle(value: other.FontStyle ?? FontStyle)
            .SetFontWeight(value: other.FontWeight ?? FontWeight)
            .SetTextDecorationLine(value: other.TextDecorationLine ?? TextDecorationLine)
            .SetTextDecorationStyle(value: other.TextDecorationStyle ?? TextDecorationStyle)
            .SetTextDecorationThickness(value: other.TextDecorationThickness ?? TextDecorationThickness)
            .SetTextTransform(value: other.TextTransform ?? TextTransform);

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

    public ThemeGroupTypography SetTextDecorationThickness(StyleValueNumber? value)
        => this with
        {
            TextDecorationThickness = value
        };

    public ThemeGroupTypography SetTextTransform(StyleValueString? value)
        => this with
        {
            TextTransform = value
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
