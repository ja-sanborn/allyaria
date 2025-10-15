using Allyaria.Theming.Constants;
using Allyaria.Theming.Enumerations;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a theming variant that contains separate <see cref="AryPalette" /> definitions for light, dark, and
/// high-contrast modes, each with their own elevation hierarchies.
/// </summary>
/// <remarks>
/// This struct enables components to adapt automatically to system or user-selected theme types while maintaining
/// consistent color relationships across different elevations and states.
/// </remarks>
public readonly record struct AryPaletteVariant
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AryPaletteVariant" /> struct, defining base palettes for light, dark, and
    /// high-contrast modes.
    /// </summary>
    public AryPaletteVariant()
        : this(null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryPaletteVariant" /> struct, defining base palettes for light, dark, and
    /// high-contrast modes.
    /// </summary>
    /// <param name="paletteLight">
    /// Optional custom light mode palette. If <see langword="null" />, defaults to
    /// <see cref="StyleDefaults.BackgroundColorLight" /> and <see cref="StyleDefaults.BackgroundColorDark" />.
    /// </param>
    /// <param name="paletteDark">
    /// Optional custom dark mode palette. If <see langword="null" />, defaults to
    /// <see cref="StyleDefaults.BackgroundColorDark" /> and <see cref="StyleDefaults.ForegroundColorDark" />.
    /// </param>
    /// <param name="paletteHighContrast">
    /// Optional custom high-contrast palette. If <see langword="null" />, defaults to
    /// <see cref="StyleDefaults.BackgroundColorHighContrast" /> and <see cref="StyleDefaults.ForegroundColorHighContrast" />.
    /// </param>
    /// <remarks>
    /// Each palette initializes a corresponding <see cref="AryPaletteElevation" /> hierarchy to support consistent appearance
    /// across elevation layers and component states.
    /// </remarks>
    public AryPaletteVariant(AryPalette? paletteLight = null,
        AryPalette? paletteDark = null,
        AryPalette? paletteHighContrast = null)
    {
        LightPalette = paletteLight ?? StyleDefaults.PaletteLight;
        DarkPalette = paletteDark ?? StyleDefaults.PaletteDark;
        HighContrastPalette = paletteHighContrast ?? StyleDefaults.PaletteHighContrast;

        LightElevation = new AryPaletteElevation(LightPalette);
        DarkElevation = new AryPaletteElevation(DarkPalette);
        HighContrastElevation = new AryPaletteElevation(HighContrastPalette);
    }

    /// <summary>Gets the elevation hierarchy for dark mode.</summary>
    public AryPaletteElevation DarkElevation { get; }

    /// <summary>Gets the base dark mode palette.</summary>
    public AryPalette DarkPalette { get; }

    /// <summary>Gets the elevation hierarchy for high-contrast mode.</summary>
    public AryPaletteElevation HighContrastElevation { get; }

    /// <summary>Gets the base high-contrast mode palette.</summary>
    public AryPalette HighContrastPalette { get; }

    /// <summary>Gets the elevation hierarchy for light mode.</summary>
    public AryPaletteElevation LightElevation { get; }

    /// <summary>Gets the base light mode palette.</summary>
    public AryPalette LightPalette { get; }

    /// <summary>
    /// Returns a new <see cref="AryPaletteVariant" /> with optional overrides for one or more theme palettes.
    /// </summary>
    /// <param name="paletteLight">Optional override for the light mode palette.</param>
    /// <param name="paletteDark">Optional override for the dark mode palette.</param>
    /// <param name="paletteHighContrast">Optional override for the high-contrast palette.</param>
    /// <returns>
    /// A new <see cref="AryPaletteVariant" /> instance with updated palettes and corresponding elevation hierarchies.
    /// </returns>
    public AryPaletteVariant Cascade(AryPalette? paletteLight = null,
        AryPalette? paletteDark = null,
        AryPalette? paletteHighContrast = null)
    {
        var newLight = paletteLight ?? LightPalette;
        var newDark = paletteDark ?? DarkPalette;
        var newHighContrast = paletteHighContrast ?? HighContrastPalette;

        return new AryPaletteVariant(newLight, newDark, newHighContrast);
    }

    /// <summary>
    /// Returns the <see cref="AryPalette" /> appropriate for the given theme type, elevation level, and component state.
    /// </summary>
    /// <param name="themeType">The selected theme type (light, dark, or high-contrast).</param>
    /// <param name="elevation">The component’s elevation level within the UI hierarchy.</param>
    /// <param name="state">The component’s visual state (e.g., default, hovered, focused).</param>
    /// <returns>The <see cref="AryPalette" /> corresponding to the specified theme, elevation, and state.</returns>
    public AryPalette ToPalette(ThemeType themeType, ComponentElevation elevation, ComponentState state)
        => themeType switch
        {
            ThemeType.Dark => DarkElevation.ToPalette(elevation, state),
            ThemeType.HighContrast => HighContrastElevation.ToPalette(elevation, state),
            _ => LightElevation.ToPalette(elevation, state)
        };
}
