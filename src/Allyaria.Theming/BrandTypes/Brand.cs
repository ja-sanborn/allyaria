namespace Allyaria.Theming.BrandTypes;

/// <summary>
/// Represents a brand configuration that encapsulates the font and theme variants (light and dark) used for visual styling
/// in the Allyaria Theming system.
/// </summary>
public readonly record struct Brand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Brand" /> struct with optional font and light/dark theme parameters.
    /// </summary>
    /// <param name="font">The font configuration for the brand. Defaults to a new <see cref="BrandFont" /> if null.</param>
    /// <param name="lightTheme">The light theme configuration for the brand. Defaults to null if unspecified.</param>
    /// <param name="darkTheme">The dark theme configuration for the brand. Defaults to null if unspecified.</param>
    public Brand(BrandFont? font = null, BrandTheme? lightTheme = null, BrandTheme? darkTheme = null)
    {
        Font = font ?? new BrandFont();
        Variant = new BrandVariant(lightTheme: lightTheme, darkTheme: darkTheme);
    }

    /// <summary>Gets the font configuration associated with this brand.</summary>
    public BrandFont Font { get; }

    /// <summary>Gets the light and dark theme variants for this brand.</summary>
    public BrandVariant Variant { get; }

    /// <summary>Creates a predefined <see cref="Brand" /> instance configured for high contrast accessibility.</summary>
    /// <returns>
    /// A <see cref="Brand" /> instance with high-contrast surface, primary, secondary, tertiary, and status colors for both
    /// light and dark modes.
    /// </returns>
    public static Brand CreateHighContrastBrand()
        => new(
            font: new BrandFont(),
            lightTheme: new BrandTheme(
                surface: StyleDefaults.HighContrastSurfaceColorLight,
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
