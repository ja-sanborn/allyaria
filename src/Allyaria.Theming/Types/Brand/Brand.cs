namespace Allyaria.Theming.Types.Brand;

public readonly record struct Brand
{
    public Brand(BrandFont? font = null, BrandTheme? lightTheme = null, BrandTheme? darkTheme = null)
    {
        Font = font ?? new BrandFont();
        Variant = new BrandVariant(lightTheme: lightTheme, darkTheme: darkTheme);
    }

    public BrandFont Font { get; }

    public BrandVariant Variant { get; }

    public string GetFont(ThemeType themeType, FontType fontType)
        => Font.GetFont(themeType: themeType, fontType: fontType);

    public BrandPalette GetPalette(ThemeType themeType, PaletteType paletteType, ComponentState state)
        => Variant.GetPalette(themeType: themeType, paletteType: paletteType, state: state);
}
