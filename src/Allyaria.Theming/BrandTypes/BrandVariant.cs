namespace Allyaria.Theming.BrandTypes;

/// <summary>
/// Represents a set of brand theme variants supporting both light and dark modes, as well as their derived inverse color
/// variants for adaptive contrast and visual harmony.
/// </summary>
public struct BrandVariant
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BrandVariant" /> struct using optional light and dark themes. If none are
    /// provided, default Allyaria color schemes are used.
    /// </summary>
    /// <param name="lightTheme">The light mode <see cref="BrandTheme" /> to apply. Defaults to a standard light theme if null.</param>
    /// <param name="darkTheme">The dark mode <see cref="BrandTheme" /> to apply. Defaults to a standard dark theme if null.</param>
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

    /// <summary>Gets the primary dark theme configuration.</summary>
    public BrandTheme Dark { get; }

    /// <summary>
    /// Gets the dark variant theme, created by inverting or adapting the foreground colors of the dark theme.
    /// </summary>
    public BrandTheme DarkVariant { get; }

    /// <summary>Gets the primary light theme configuration.</summary>
    public BrandTheme Light { get; }

    /// <summary>
    /// Gets the light variant theme, created by inverting or adapting the foreground colors of the light theme.
    /// </summary>
    public BrandTheme LightVariant { get; }
}
