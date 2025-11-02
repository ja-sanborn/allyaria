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
                surface: StyleDefaults.SurfaceColorLight,
                surfaceVariant: StyleDefaults.SurfaceVariantColorLight,
                primary: StyleDefaults.PrimaryColorLight,
                secondary: StyleDefaults.SecondaryColorLight,
                tertiary: StyleDefaults.TertiaryColorLight,
                error: StyleDefaults.ErrorColorLight,
                warning: StyleDefaults.WarningColorLight,
                success: StyleDefaults.SuccessColorLight,
                info: StyleDefaults.InfoColorLight
            ),
            darkTheme: new BrandTheme(
                surface: StyleDefaults.SurfaceColorDark,
                surfaceVariant: StyleDefaults.SurfaceVariantColorDark,
                primary: StyleDefaults.PrimaryColorDark,
                secondary: StyleDefaults.SecondaryColorDark,
                tertiary: StyleDefaults.TertiaryColorDark,
                error: StyleDefaults.ErrorColorDark,
                warning: StyleDefaults.WarningColorDark,
                success: StyleDefaults.SuccessColorDark,
                info: StyleDefaults.InfoColorDark
            )
        );
}
