using Allyaria.Theming.Interfaces;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.Services;

/// <summary>
/// Provides a concrete implementation of <see cref="IAryThemeProvider" /> that manages theme configuration, including
/// color palettes, spacing, borders, and typography.
/// </summary>
/// <remarks>
/// This implementation supports runtime updates to theme properties and notifies consumers through the
/// <see cref="ThemeChanged" /> event when the active <see cref="ThemeType" /> or any theme parameter changes. All updates
/// are performed immutably by cascading existing theme data to new instances.
/// </remarks>
public sealed class AryThemeProvider : IAryThemeProvider
{
    /// <summary>Holds the current <see cref="AryTheme" /> instance that defines the active visual design system.</summary>
    /// <remarks>
    /// The field stores the complete theme configuration, including palettes, typography, spacing, and borders. It is updated
    /// immutably using <see cref="AryTheme.Cascade" /> whenever a setter is invoked.
    /// </remarks>
    private AryTheme _theme;

    /// <summary>
    /// Represents the currently active <see cref="ThemeType" /> (e.g., Light, Dark, or System) in use by the provider.
    /// </summary>
    /// <remarks>
    /// This field determines which color palette or variant is selected for CSS and style generation. It is updated when
    /// <see cref="SetThemeType(Enumerations.ThemeType)" /> is called.
    /// </remarks>
    private ThemeType _themeType;

    /// <summary>Initializes a new instance of the <see cref="AryThemeProvider" /> class.</summary>
    /// <param name="theme">The base theme configuration to apply. If <see langword="null" />, the default theme is used.</param>
    /// <param name="themeType">The initial <see cref="ThemeType" /> to apply. Defaults to <see cref="ThemeType.System" />.</param>
    public AryThemeProvider(AryTheme? theme = null, ThemeType themeType = ThemeType.System)
        => (_theme, _themeType) = (theme ?? StyleDefaults.Theme, themeType);

    /// <summary>Occurs when the theme or <see cref="ThemeType" /> has changed.</summary>
    public event EventHandler? ThemeChanged;

    /// <summary>Gets the currently active <see cref="ThemeType" />.</summary>
    public ThemeType ThemeType => _themeType;

    /// <summary>Retrieves a CSS string representing the current visual style for a specific component type.</summary>
    /// <param name="componentType">The component type to generate CSS for.</param>
    /// <param name="elevation">The elevation level to apply. Defaults to <see cref="ComponentElevation.Mid" />.</param>
    /// <param name="state">The component state (e.g., Default, Hovered). Defaults to <see cref="ComponentState.Default" />.</param>
    /// <param name="varPrefix">An optional CSS variable prefix. Defaults to an empty string.</param>
    /// <returns>A string containing CSS declarations for the component’s themed style.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="componentType" /> is not recognized.</exception>
    public string GetCss(ComponentType componentType,
        ComponentElevation elevation = ComponentElevation.Mid,
        ComponentState state = ComponentState.Default,
        string? varPrefix = "")
        => _theme.ToCss(ThemeType, componentType, elevation, state, varPrefix);

    /// <summary>
    /// Retrieves a strongly-typed <see cref="AryStyle" /> representing the themed configuration for a component.
    /// </summary>
    /// <param name="componentType">The component type to retrieve style information for.</param>
    /// <param name="elevation">The elevation level to apply. Defaults to <see cref="ComponentElevation.Mid" />.</param>
    /// <param name="state">The component state (e.g., Default, Hovered). Defaults to <see cref="ComponentState.Default" />.</param>
    /// <returns>An <see cref="AryStyle" /> instance describing the themed style.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="componentType" /> is not recognized.</exception>
    public AryStyle GetStyle(ComponentType componentType,
        ComponentElevation elevation = ComponentElevation.Mid,
        ComponentState state = ComponentState.Default)
        => _theme.ToStyle(ThemeType, componentType, elevation, state);

    /// <summary>Raises the <see cref="ThemeChanged" /> event when theme properties are updated.</summary>
    /// <param name="raiseEvent">Indicates whether the event should be raised.</param>
    private void OnThemeChanged(bool raiseEvent)
    {
        if (!raiseEvent)
        {
            return;
        }

        var handler = ThemeChanged;
        handler?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>Updates the current theme’s border configuration.</summary>
    /// <param name="borders">The new border configuration to apply.</param>
    /// <returns><see langword="true" /> if the borders were updated; otherwise, <see langword="false" />.</returns>
    public bool SetBorders(AryBorders? borders = null) => SetBordersCore(true, borders);

    /// <summary>Core logic for updating border configuration.</summary>
    /// <param name="raiseEvent">Indicates whether to raise <see cref="ThemeChanged" /> after updating.</param>
    /// <param name="borders">The new border configuration.</param>
    /// <returns><see langword="true" /> if the borders were updated; otherwise, <see langword="false" />.</returns>
    private bool SetBordersCore(bool raiseEvent, AryBorders? borders = null)
    {
        if (borders is null || borders.Value == _theme.Borders)
        {
            return false;
        }

        _theme = _theme.Cascade(borders);
        OnThemeChanged(raiseEvent);

        return true;
    }

    /// <summary>Updates or replaces the dark palette used by the theme.</summary>
    /// <param name="palette">The new dark palette to apply.</param>
    /// <returns><see langword="true" /> if the dark palette was updated; otherwise, <see langword="false" />.</returns>
    public bool SetDarkPalette(AryPalette? palette = null) => SetDarkPaletteCore(true, palette);

    /// <summary>Core logic for updating the dark palette configuration.</summary>
    /// <param name="raiseEvent">Indicates whether to raise <see cref="ThemeChanged" /> after updating.</param>
    /// <param name="palette">The new dark palette configuration.</param>
    /// <returns><see langword="true" /> if the palette was updated; otherwise, <see langword="false" />.</returns>
    private bool SetDarkPaletteCore(bool raiseEvent, AryPalette? palette = null)
    {
        if (palette is null || palette.Value == _theme.Palette.DarkPalette)
        {
            return false;
        }

        _theme = _theme.Cascade(paletteDark: palette);
        OnThemeChanged(raiseEvent);

        return true;
    }

    /// <summary>Updates or replaces the high-contrast palette used by the theme.</summary>
    /// <param name="palette">The new high-contrast palette to apply.</param>
    /// <returns>
    /// <see langword="true" /> if the high-contrast palette was updated; otherwise, <see langword="false" />.
    /// </returns>
    public bool SetHighContrastPalette(AryPalette? palette = null) => SetHighContrastPaletteCore(true, palette);

    /// <summary>Core logic for updating the high-contrast palette configuration.</summary>
    /// <param name="raiseEvent">Indicates whether to raise <see cref="ThemeChanged" /> after updating.</param>
    /// <param name="palette">The new high-contrast palette configuration.</param>
    /// <returns><see langword="true" /> if the palette was updated; otherwise, <see langword="false" />.</returns>
    private bool SetHighContrastPaletteCore(bool raiseEvent, AryPalette? palette = null)
    {
        if (palette is null || palette.Value == _theme.Palette.HighContrastPalette)
        {
            return false;
        }

        _theme = _theme.Cascade(paletteHighContrast: palette);
        OnThemeChanged(raiseEvent);

        return true;
    }

    /// <summary>Updates or replaces the light palette used by the theme.</summary>
    /// <param name="palette">The new light palette to apply.</param>
    /// <returns><see langword="true" /> if the light palette was updated; otherwise, <see langword="false" />.</returns>
    public bool SetLightPalette(AryPalette? palette = null) => SetLightPaletteCore(true, palette);

    /// <summary>Core logic for updating the light palette configuration.</summary>
    /// <param name="raiseEvent">Indicates whether to raise <see cref="ThemeChanged" /> after updating.</param>
    /// <param name="palette">The new light palette configuration.</param>
    /// <returns><see langword="true" /> if the palette was updated; otherwise, <see langword="false" />.</returns>
    private bool SetLightPaletteCore(bool raiseEvent, AryPalette? palette = null)
    {
        if (palette is null || palette.Value == _theme.Palette.LightPalette)
        {
            return false;
        }

        _theme = _theme.Cascade(paletteLight: palette);
        OnThemeChanged(raiseEvent);

        return true;
    }

    /// <summary>Updates the current theme’s spacing configuration.</summary>
    /// <param name="spacing">The new spacing configuration to apply.</param>
    /// <returns><see langword="true" /> if the spacing was updated; otherwise, <see langword="false" />.</returns>
    public bool SetSpacing(ArySpacing? spacing = null) => SetSpacingCore(true, spacing);

    /// <summary>Core logic for updating spacing configuration.</summary>
    /// <param name="raiseEvent">Indicates whether to raise <see cref="ThemeChanged" /> after updating.</param>
    /// <param name="spacing">The new spacing configuration.</param>
    /// <returns><see langword="true" /> if the spacing was updated; otherwise, <see langword="false" />.</returns>
    private bool SetSpacingCore(bool raiseEvent, ArySpacing? spacing = null)
    {
        if (spacing is null || spacing.Value == _theme.Spacing)
        {
            return false;
        }

        _theme = _theme.Cascade(spacing: spacing);
        OnThemeChanged(raiseEvent);

        return true;
    }

    /// <summary>Updates the surface typography configuration used by the theme.</summary>
    /// <param name="typoSurface">The new surface typography to apply.</param>
    /// <returns><see langword="true" /> if the typography was updated; otherwise, <see langword="false" />.</returns>
    public bool SetSurfaceTypography(AryTypography? typoSurface = null) => SetSurfaceTypographyCore(true, typoSurface);

    /// <summary>Core logic for updating the surface typography configuration.</summary>
    /// <param name="raiseEvent">Indicates whether to raise <see cref="ThemeChanged" /> after updating.</param>
    /// <param name="typoSurface">The new typography configuration.</param>
    /// <returns><see langword="true" /> if the typography was updated; otherwise, <see langword="false" />.</returns>
    private bool SetSurfaceTypographyCore(bool raiseEvent, AryTypography? typoSurface = null)
    {
        if (typoSurface is null || typoSurface.Value == _theme.Typo.Surface)
        {
            return false;
        }

        _theme = _theme.Cascade(typoSurface: typoSurface);
        OnThemeChanged(raiseEvent);

        return true;
    }

    /// <summary>Applies a complete new <see cref="AryTheme" /> configuration.</summary>
    /// <param name="theme">The new theme to apply.</param>
    /// <returns><see langword="true" /> if the theme was changed; otherwise, <see langword="false" />.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="theme" /> is <see langword="null" />.</exception>
    public bool SetTheme(AryTheme theme)
    {
        if (theme == _theme)
        {
            return false;
        }

        var changed = false;
        changed |= SetBordersCore(false, theme.Borders);
        changed |= SetSpacingCore(false, theme.Spacing);
        changed |= SetLightPaletteCore(false, theme.Palette.LightPalette);
        changed |= SetDarkPaletteCore(false, theme.Palette.DarkPalette);
        changed |= SetHighContrastPaletteCore(false, theme.Palette.HighContrastPalette);
        changed |= SetSurfaceTypographyCore(false, theme.Typo.Surface);

        OnThemeChanged(changed);

        return changed;
    }

    /// <summary>
    /// Sets the current <see cref="ThemeType" /> (e.g., Light, Dark, System) and raises the <see cref="ThemeChanged" /> event
    /// if changed.
    /// </summary>
    /// <param name="themeType">The new theme type to apply.</param>
    /// <returns><see langword="true" /> if the theme type was changed; otherwise, <see langword="false" />.</returns>
    public bool SetThemeType(ThemeType themeType)
    {
        if (themeType == _themeType)
        {
            return false;
        }

        _themeType = themeType;
        OnThemeChanged(true);

        return true;
    }
}
