namespace Allyaria.Theming.Types;

public struct BrandVariant
{
    public BrandVariant(BrandTheme? lightTheme = null, BrandTheme? darkTheme = null)
    {
        Dark = darkTheme ?? new BrandTheme(
            surface: StyleDefaults.SurfaceColorDark,
            primary: StyleDefaults.PrimaryColorDark,
            secondary: StyleDefaults.SecondaryColorDark,
            tertiary: StyleDefaults.TertiaryColorDark,
            error: StyleDefaults.ErrorColorDark,
            warning: StyleDefaults.WarningColorDark,
            success: StyleDefaults.SuccessColorDark,
            info: StyleDefaults.InfoColorDark
        );

        DarkVariant = new BrandTheme(
            surface: Dark.Surface.Default.ForegroundColor,
            primary: Dark.Primary.Default.ForegroundColor,
            secondary: Dark.Secondary.Default.ForegroundColor,
            tertiary: Dark.Tertiary.Default.ForegroundColor,
            error: Dark.Error.Default.ForegroundColor,
            warning: Dark.Warning.Default.ForegroundColor,
            success: Dark.Success.Default.ForegroundColor,
            info: Dark.Info.Default.ForegroundColor
        );

        Light = lightTheme ?? new BrandTheme(
            surface: StyleDefaults.SurfaceColorLight,
            primary: StyleDefaults.PrimaryColorLight,
            secondary: StyleDefaults.SecondaryColorLight,
            tertiary: StyleDefaults.TertiaryColorLight,
            error: StyleDefaults.ErrorColorLight,
            warning: StyleDefaults.WarningColorLight,
            success: StyleDefaults.SuccessColorLight,
            info: StyleDefaults.InfoColorLight
        );

        LightVariant = new BrandTheme(
            surface: Light.Surface.Default.ForegroundColor,
            primary: Light.Primary.Default.ForegroundColor,
            secondary: Light.Secondary.Default.ForegroundColor,
            tertiary: Light.Tertiary.Default.ForegroundColor,
            error: Light.Error.Default.ForegroundColor,
            warning: Light.Warning.Default.ForegroundColor,
            success: Light.Success.Default.ForegroundColor,
            info: Light.Info.Default.ForegroundColor
        );
    }

    public BrandTheme Dark { get; }

    public BrandTheme DarkVariant { get; }

    public BrandTheme Light { get; }

    public BrandTheme LightVariant { get; }
}
