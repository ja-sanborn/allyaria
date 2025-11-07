namespace Allyaria.Theming.Contracts;

/// <summary>
/// Defines a contract for managing theme selection, persistence, and CSS generation within the Allyaria theming system.
/// </summary>
/// <remarks>
/// The <see cref="IThemingService" /> coordinates active and stored theme states, allowing components to generate
/// appropriate CSS based on the current <see cref="ThemeType" />. Implementations should raise <see cref="ThemeChanged" />
/// whenever the effective theme changes, ensuring responsive updates across the UI.
/// </remarks>
public interface IThemingService
{
    /// <summary>Occurs when the currently effective theme changes.</summary>
    event EventHandler? ThemeChanged;

    /// <summary>Gets the theme type that is currently active and applied to the UI.</summary>
    ThemeType EffectiveType { get; }

    /// <summary>Gets the theme type that has been persisted or stored (e.g., user preference).</summary>
    ThemeType StoredType { get; }

    /// <summary>Generates a scoped CSS string for a given component, including state-specific styling.</summary>
    /// <param name="prefix">The CSS prefix or identifier associated with the component.</param>
    /// <param name="componentType">The type of the component for which to generate CSS.</param>
    /// <param name="componentState">The visual or interaction state of the component (e.g., hovered, focused, pressed).</param>
    /// <returns>A CSS string suitable for applying inline or injecting into the component’s style scope.</returns>
    string GetComponentCss(string prefix, ComponentType componentType, ComponentState componentState);

    /// <summary>Generates global or document-level CSS for the current theme.</summary>
    /// <returns>A string containing CSS rules reflecting the active theme’s document-wide styles.</returns>
    string GetDocumentCss();

    /// <summary>Sets the currently active (effective) theme type, applying it immediately to the UI.</summary>
    /// <param name="themeType">The <see cref="ThemeType" /> to activate.</param>
    void SetEffectiveType(ThemeType themeType);

    /// <summary>Sets the stored (persisted) theme type to be remembered across sessions.</summary>
    /// <param name="themeType">The <see cref="ThemeType" /> to store.</param>
    void SetStoredType(ThemeType themeType);
}
