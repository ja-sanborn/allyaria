using Allyaria.Theming.Styles;

namespace Allyaria.Theming.Interfaces;

/// <summary>
/// Provides the contract for managing and applying theming data, including palettes, typography, spacing, and borders,
/// across components. Enables retrieval of CSS or <see cref="AryStyle" /> representations of themed components.
/// </summary>
/// <remarks>
/// Implementations must ensure deterministic behavior, raise <see cref="ThemeChanged" /> when the effective
/// <see cref="AryTheme" /> or <see cref="ThemeType" /> change, and maintain internal immutability of shared style
/// definitions to prevent cross-component side effects.
/// </remarks>
public interface IThemeProvider
{
    /// <summary>Occurs when the active <see cref="ThemeType" /> changes.</summary>
    event EventHandler? ThemeChanged;

    /// <summary>Gets the currently active <see cref="ThemeType" />.</summary>
    ThemeType ThemeType { get; }

    /// <summary>Retrieves the resolved CSS string for a component based on its type, elevation, and state.</summary>
    /// <param name="componentType">The type of the component for which CSS should be retrieved.</param>
    /// <param name="elevation">The component's visual elevation level. Defaults to <see cref="ComponentElevation.Mid" />.</param>
    /// <param name="state">The component state (e.g., hovered, active). Defaults to <see cref="ComponentState.Default" />.</param>
    /// <param name="varPrefix">An optional prefix applied to generated CSS variables.</param>
    /// <returns>A CSS string representing the component's visual style under the current theme.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the specified <paramref name="componentType" /> is not
    /// recognized.
    /// </exception>
    string GetCss(ComponentType componentType,
        ComponentElevation elevation = ComponentElevation.Mid,
        ComponentState state = ComponentState.Default,
        string? varPrefix = "");

    /// <summary>
    /// Retrieves the structured <see cref="AryStyle" /> for a component based on its type, elevation, and state.
    /// </summary>
    /// <param name="componentType">The type of the component for which a style object should be retrieved.</param>
    /// <param name="elevation">The component's visual elevation level. Defaults to <see cref="ComponentElevation.Mid" />.</param>
    /// <param name="state">The component's interaction state. Defaults to <see cref="ComponentState.Default" />.</param>
    /// <returns>An <see cref="AryStyle" /> object representing the component's themed style data.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the specified <paramref name="componentType" /> is not
    /// recognized.
    /// </exception>
    AryStyle GetStyle(ComponentType componentType,
        ComponentElevation elevation = ComponentElevation.Mid,
        ComponentState state = ComponentState.Default);

    /// <summary>Updates the current theme's border configuration.</summary>
    /// <param name="borders">The border configuration to apply. If <see langword="null" />, the default borders are restored.</param>
    /// <returns><see langword="true" /> if the operation succeeded; otherwise, <see langword="false" />.</returns>
    bool SetBorders(AryBorders? borders = null);

    /// <summary>Updates or replaces the dark theme palette.</summary>
    /// <param name="palette">The dark theme palette to apply. If <see langword="null" />, defaults are used.</param>
    /// <returns><see langword="true" /> if the operation succeeded; otherwise, <see langword="false" />.</returns>
    bool SetDarkPalette(AryPalette? palette = null);

    /// <summary>Updates or replaces the high-contrast theme palette.</summary>
    /// <param name="palette">The high-contrast palette to apply. If <see langword="null" />, defaults are used.</param>
    /// <returns><see langword="true" /> if the operation succeeded; otherwise, <see langword="false" />.</returns>
    bool SetHighContrastPalette(AryPalette? palette = null);

    /// <summary>Updates or replaces the light theme palette.</summary>
    /// <param name="palette">The light theme palette to apply. If <see langword="null" />, defaults are used.</param>
    /// <returns><see langword="true" /> if the operation succeeded; otherwise, <see langword="false" />.</returns>
    bool SetLightPalette(AryPalette? palette = null);

    /// <summary>Updates the current theme's spacing configuration.</summary>
    /// <param name="spacing">The spacing configuration to apply. If <see langword="null" />, default spacing is restored.</param>
    /// <returns><see langword="true" /> if the operation succeeded; otherwise, <see langword="false" />.</returns>
    bool SetSpacing(ArySpacing? spacing = null);

    /// <summary>Updates the typography applied to surface-level components.</summary>
    /// <param name="typoSurface">The typography configuration to apply. If <see langword="null" />, defaults are used.</param>
    /// <returns><see langword="true" /> if the operation succeeded; otherwise, <see langword="false" />.</returns>
    bool SetSurfaceTypography(AryTypography? typoSurface = null);

    /// <summary>Replaces the entire active <see cref="AryTheme" /> configuration.</summary>
    /// <param name="theme">The theme to apply.</param>
    /// <returns><see langword="true" /> if the theme was successfully applied; otherwise, <see langword="false" />.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="theme" /> is <see langword="null" />.</exception>
    bool SetTheme(AryTheme theme);

    /// <summary>
    /// Changes the current <see cref="ThemeType" /> and raises <see cref="ThemeChanged" /> if the type differs.
    /// </summary>
    /// <param name="themeType">The new theme type to activate.</param>
    /// <returns><see langword="true" /> if the theme type was changed; otherwise, <see langword="false" />.</returns>
    bool SetThemeType(ThemeType themeType);
}
