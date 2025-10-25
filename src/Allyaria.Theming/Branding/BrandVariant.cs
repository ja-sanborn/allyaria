namespace Allyaria.Theming.Branding;

public struct BrandVariant
{
    public BrandVariant(BrandPalette? light = null, BrandPalette? dark = null)
    {
        HighContrastDark = new BrandPalette(isHighContrastDark: true);
        HighContrastLight = new BrandPalette(isHighContrastDark: false);

        Dark = dark ?? new BrandPalette(
            surface: CssColors.SurfaceColorDark.Color,
            surfaceVariant: CssColors.SurfaceVariantColorDark.Color,
            primary: CssColors.PrimaryColorDark.Color,
            secondary: CssColors.SecondaryColorDark.Color,
            tertiary: CssColors.TertiaryColorDark.Color,
            error: CssColors.ErrorColorDark.Color,
            warning: CssColors.WarningColorDark.Color,
            success: CssColors.SuccessColorDark.Color,
            info: CssColors.InfoColorDark.Color
        );

        Light = light ?? new BrandPalette(
            surface: CssColors.SurfaceColorLight.Color,
            surfaceVariant: CssColors.SurfaceVariantColorLight.Color,
            primary: CssColors.PrimaryColorLight.Color,
            secondary: CssColors.SecondaryColorLight.Color,
            tertiary: CssColors.TertiaryColorLight.Color,
            error: CssColors.ErrorColorLight.Color,
            warning: CssColors.WarningColorLight.Color,
            success: CssColors.SuccessColorLight.Color,
            info: CssColors.InfoColorLight.Color
        );
    }

    public BrandPalette Dark { get; }

    public BrandPalette HighContrastDark { get; }

    public BrandPalette HighContrastLight { get; }

    public BrandPalette Light { get; }

    public ThemeGroupPalette GetPalette(ThemeType themeType, PaletteType paletteType, ComponentState state)
        => themeType switch
        {
            ThemeType.Dark => Dark.GetPalette(themeType: themeType, paletteType: paletteType, state: state),
            ThemeType.HighContrastDark => HighContrastDark.GetPalette(
                themeType: themeType, paletteType: paletteType, state: state
            ),
            ThemeType.HighContrastLight => HighContrastLight.GetPalette(
                themeType: themeType, paletteType: paletteType, state: state
            ),
            _ => Light.GetPalette(themeType: themeType, paletteType: paletteType, state: state)
        };
}
