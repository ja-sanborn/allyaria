namespace Allyaria.Theming.Branding;

public readonly record struct BrandTheme
{
    public BrandTheme(BrandFont? font = null, BrandPalette? lightPalette = null, BrandPalette? darkPalette = null)
    {
        Font = font ?? new BrandFont();
        Variant = new BrandVariant(light: lightPalette, dark: darkPalette);
    }

    public BrandFont Font { get; }

    public BrandVariant Variant { get; }

    public StyleValueString GetFont(ThemeType themeType, FontType fontType)
        => Font.GetFont(themeType: themeType, fontType: fontType);

    public ThemeGroupPalette GetPalette(ThemeType themeType, PaletteType paletteType, ComponentState state)
        => Variant.GetPalette(themeType: themeType, paletteType: paletteType, state: state);
}
