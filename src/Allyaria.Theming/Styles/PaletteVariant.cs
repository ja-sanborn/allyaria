namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a theming variant that contains separate <see cref="Palette" /> definitions for light, dark, and
/// high-contrast modes, each with their own elevation hierarchies.
/// </summary>
/// <remarks>
/// This struct enables components to adapt automatically to system or user-selected theme types while maintaining
/// consistent color relationships across different elevations and states.
/// </remarks>
internal readonly record struct PaletteVariant
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaletteVariant" /> struct, defining base palettes for light, dark, and
    /// high-contrast modes.
    /// </summary>
    public PaletteVariant()
        : this(null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaletteVariant" /> struct, defining base palettes for light, dark, and
    /// high-contrast modes.
    /// </summary>
    /// <param name="paletteLight">
    /// Optional custom light mode palette. If <see langword="null" />, defaults to
    /// <see cref="StyleDefaults.BackgroundThemeColorLight" /> and <see cref="StyleDefaults.BackgroundThemeColorDark" />.
    /// </param>
    /// <param name="paletteDark">
    /// Optional custom dark mode palette. If <see langword="null" />, defaults to
    /// <see cref="StyleDefaults.BackgroundThemeColorDark" /> and <see cref="StyleDefaults.ForegroundThemeColorDark" />.
    /// </param>
    /// <param name="paletteHighContrast">
    /// Optional custom high-contrast palette. If <see langword="null" />, defaults to
    /// <see cref="StyleDefaults.BackgroundThemeColorHighContrast" /> and
    /// <see cref="StyleDefaults.ForegroundThemeColorHighContrast" />.
    /// </param>
    /// <remarks>
    /// Each palette initializes a corresponding <see cref="PaletteElevation" /> hierarchy to support consistent appearance
    /// across elevation layers and component states.
    /// </remarks>
    public PaletteVariant(Palette? paletteLight = null,
        Palette? paletteDark = null,
        Palette? paletteHighContrast = null)
    {
        LightPalette = paletteLight ?? StyleDefaults.PaletteLight;
        DarkPalette = paletteDark ?? StyleDefaults.PaletteDark;
        HighContrastPalette = paletteHighContrast ?? StyleDefaults.PaletteHighContrast;

        LightElevation = new PaletteElevation(LightPalette);
        DarkElevation = new PaletteElevation(DarkPalette);
        HighContrastElevation = new PaletteElevation(HighContrastPalette);
    }

    /// <summary>Gets the elevation hierarchy for dark mode.</summary>
    public PaletteElevation DarkElevation { get; }

    /// <summary>Gets the base dark mode palette.</summary>
    public Palette DarkPalette { get; }

    /// <summary>Gets the elevation hierarchy for high-contrast mode.</summary>
    public PaletteElevation HighContrastElevation { get; }

    /// <summary>Gets the base high-contrast mode palette.</summary>
    public Palette HighContrastPalette { get; }

    /// <summary>Gets the elevation hierarchy for light mode.</summary>
    public PaletteElevation LightElevation { get; }

    /// <summary>Gets the base light mode palette.</summary>
    public Palette LightPalette { get; }

    /// <summary>
    /// Returns a new <see cref="PaletteVariant" /> with optional overrides for one or more theme palettes.
    /// </summary>
    /// <param name="paletteLight">Optional override for the light mode palette.</param>
    /// <param name="paletteDark">Optional override for the dark mode palette.</param>
    /// <param name="paletteHighContrast">Optional override for the high-contrast palette.</param>
    /// <returns>
    /// A new <see cref="PaletteVariant" /> instance with updated palettes and corresponding elevation hierarchies.
    /// </returns>
    public PaletteVariant Cascade(Palette? paletteLight = null,
        Palette? paletteDark = null,
        Palette? paletteHighContrast = null)
    {
        var newLight = paletteLight ?? LightPalette;
        var newDark = paletteDark ?? DarkPalette;
        var newHighContrast = paletteHighContrast ?? HighContrastPalette;

        return new PaletteVariant(newLight, newDark, newHighContrast);
    }

    /// <summary>
    /// Returns the <see cref="Palette" /> appropriate for the given theme type, elevation level, and component state.
    /// </summary>
    /// <param name="themeType">The selected theme type (light, dark, or high-contrast).</param>
    /// <param name="elevation">The component’s elevation level within the UI hierarchy.</param>
    /// <param name="state">The component’s visual state (e.g., default, hovered, focused).</param>
    /// <returns>The <see cref="Palette" /> corresponding to the specified theme, elevation, and state.</returns>
    public Palette ToPalette(ThemeType themeType, ComponentElevation elevation, ComponentState state)
        => themeType switch
        {
            ThemeType.Dark => DarkElevation.ToPalette(elevation, state),
            ThemeType.HighContrast => HighContrastElevation.ToPalette(elevation, state),
            _ => LightElevation.ToPalette(elevation, state)
        };
}
