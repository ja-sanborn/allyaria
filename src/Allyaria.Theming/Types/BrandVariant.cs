namespace Allyaria.Theming.Types;

public struct BrandVariant
{
    public BrandVariant(BrandTheme? lightTheme = null, BrandTheme? darkTheme = null)
    {
        HighContrastDark = new BrandTheme(isHighContrastDark: true);
        HighContrastLight = new BrandTheme(isHighContrastDark: false);

        Dark = darkTheme ?? new BrandTheme(
            surface: StyleDefaults.SurfaceColorDark,
            surfaceVariant: StyleDefaults.SurfaceVariantColorDark,
            primary: StyleDefaults.PrimaryColorDark,
            secondary: StyleDefaults.SecondaryColorDark,
            tertiary: StyleDefaults.TertiaryColorDark,
            error: StyleDefaults.ErrorColorDark,
            warning: StyleDefaults.WarningColorDark,
            success: StyleDefaults.SuccessColorDark,
            info: StyleDefaults.InfoColorDark
        );

        Light = lightTheme ?? new BrandTheme(
            surface: StyleDefaults.SurfaceColorLight,
            surfaceVariant: StyleDefaults.SurfaceVariantColorLight,
            primary: StyleDefaults.PrimaryColorLight,
            secondary: StyleDefaults.SecondaryColorLight,
            tertiary: StyleDefaults.TertiaryColorLight,
            error: StyleDefaults.ErrorColorLight,
            warning: StyleDefaults.WarningColorLight,
            success: StyleDefaults.SuccessColorLight,
            info: StyleDefaults.InfoColorLight
        );
    }

    public BrandTheme Dark { get; }

    public BrandTheme HighContrastDark { get; }

    public BrandTheme HighContrastLight { get; }

    public BrandTheme Light { get; }

    public BrandPalette GetPalette(ThemeType themeType, PaletteType paletteType, ComponentState state)
        => themeType switch
        {
            ThemeType.Dark => Dark.GetPalette(paletteType: paletteType, state: state),
            ThemeType.HighContrastDark => HighContrastDark.GetPalette(paletteType: paletteType, state: state),
            ThemeType.HighContrastLight => HighContrastLight.GetPalette(paletteType: paletteType, state: state),
            _ => Light.GetPalette(paletteType: paletteType, state: state)
        };
}
