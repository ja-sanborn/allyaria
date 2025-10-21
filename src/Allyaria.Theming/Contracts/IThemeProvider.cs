namespace Allyaria.Theming.Contracts;

/// <summary>
/// Provides the contract for managing and applying theming data, including palettes, typography, spacing, and borders,
/// across components. Enables retrieval of CSS or <see cref="Style" /> representations of themed components.
/// </summary>
/// <remarks>
/// Implementations must ensure deterministic behavior, raise <see cref="ThemeChanged" /> when the effective
/// <see cref="Theme" /> or <see cref="ThemeType" /> change, and maintain internal immutability of shared style definitions
/// to prevent cross-component side effects.
/// </remarks>
public interface IThemeProvider
{
    /// <summary>Occurs when the active <see cref="ThemeType" /> changes.</summary>
    event EventHandler? ThemeChanged;

    /// <summary>Gets the currently active <see cref="ThemeType" />.</summary>
    ThemeType ThemeType { get; }

    /// <summary>Retrieves a string of CSS var declarations representing the currently active theme.</summary>
    /// <returns>A string containing CSS var declarations for the active theme.</returns>
    string GetCss();

    /// <summary>Retrieves the resolved CSS string for a component based on its type, elevation, and state.</summary>
    /// <param name="componentType">The type of the component for which CSS should be retrieved.</param>
    /// <param name="elevation">The component's visual elevation level. Defaults to <see cref="ComponentElevation.Mid" />.</param>
    /// <param name="state">The component state (e.g., hovered, active). Defaults to <see cref="ComponentState.Default" />.</param>
    /// <param name="varPrefix">An optional prefix applied to generated CSS variables.</param>
    /// <returns>A CSS string representing the component's visual style under the current theme.</returns>
    string GetCss(ComponentType componentType,
        ComponentState state = ComponentState.Default,
        string? varPrefix = "");
}
