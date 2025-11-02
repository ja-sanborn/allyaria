namespace Allyaria.Theming.Types;

public readonly record struct Brand
{
    public Brand(BrandFont? font = null, BrandTheme? lightTheme = null, BrandTheme? darkTheme = null)
    {
        Font = font ?? new BrandFont();
        Variant = new BrandVariant(lightTheme: lightTheme, darkTheme: darkTheme);
    }

    public BrandFont Font { get; }

    public BrandVariant Variant { get; }

    public static Brand CreateHighContrastBrand()
        => new(
            font: new BrandFont(),
            lightTheme: new BrandTheme(
                surface: StyleDefaults.HighContrastSurfaceColorLight,
                surfaceVariant: StyleDefaults.HighContrastSurfaceVariantColorLight,
                primary: StyleDefaults.HighContrastPrimaryColorLight,
                secondary: StyleDefaults.HighContrastSecondaryColorLight,
                tertiary: StyleDefaults.HighContrastTertiaryColorLight,
                error: StyleDefaults.HighContrastErrorColorLight,
                warning: StyleDefaults.HighContrastWarningColorLight,
                success: StyleDefaults.HighContrastSuccessColorLight,
                info: StyleDefaults.HighContrastInfoColorLight
            ),
            darkTheme: new BrandTheme(
                surface: StyleDefaults.HighContrastSurfaceColorDark,
                surfaceVariant: StyleDefaults.HighContrastSurfaceVariantColorDark,
                primary: StyleDefaults.HighContrastPrimaryColorDark,
                secondary: StyleDefaults.HighContrastSecondaryColorDark,
                tertiary: StyleDefaults.HighContrastTertiaryColorDark,
                error: StyleDefaults.HighContrastErrorColorDark,
                warning: StyleDefaults.HighContrastWarningColorDark,
                success: StyleDefaults.HighContrastSuccessColorDark,
                info: StyleDefaults.HighContrastInfoColorDark
            )
        );
}
