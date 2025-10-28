namespace Allyaria.Theming.Themes;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class ThemeTypography
{
    public static readonly ThemeGroupTypography Monospace = ThemeGroupTypography.FromBrand(
        brand: new BrandFont(), themeType: ThemeType.Light, fontType: FontType.Monospace
    );

    public static readonly ThemeGroupTypography SansSerif = ThemeGroupTypography.FromBrand(
        brand: new BrandFont(), themeType: ThemeType.Light, fontType: FontType.SansSerif
    );

    public static readonly ThemeGroupTypography Serif = ThemeGroupTypography.FromBrand(
        brand: new BrandFont(), themeType: ThemeType.Light, fontType: FontType.Serif
    );
}
