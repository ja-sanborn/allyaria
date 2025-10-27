namespace Allyaria.Theming.Themes;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class ThemeTypography
{
    public static readonly BrandTheme BrandTheme = new();

    public static readonly ThemeGroupTypography Default = new(
        FontSize: CssSize.Size2,
        FontWeight: CssFontWeight.Normal,
        FontStyle: CssFontStyle.Normal,
        TextDecorationLine: CssTextDecorationLine.None,
        TextDecorationStyle: CssTextDecorationStyle.Solid,
        TextDecorationThickness: CssSize.Thin,
        TextTransform: CssTextTransform.None
    );

    public static readonly ThemeGroupTypography Monospace = Default.SetFontFamily(
        value: BrandTheme.GetFont(themeType: ThemeType.Light, fontType: FontType.Monospace)
    );

    public static readonly ThemeGroupTypography SansSerif = Default.SetFontFamily(
        value: BrandTheme.GetFont(themeType: ThemeType.Light, fontType: FontType.SansSerif)
    );

    public static readonly ThemeGroupTypography Serif = Default.SetFontFamily(
        value: BrandTheme.GetFont(themeType: ThemeType.Light, fontType: FontType.Serif)
    );
}
