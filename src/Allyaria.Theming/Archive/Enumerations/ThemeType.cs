namespace Allyaria.Theming.Enumerations;

/// <summary>Defines the available theme types supported by Allyaria.</summary>
/// <remarks>
/// The <see cref="ThemeType" /> enumeration represents the visual appearance options that can be applied at runtime
/// through <c>Theme</c>. The <see cref="System" /> option adapts to the user's operating system preference.
/// </remarks>
public enum ThemeType
{
    /// <summary>
    /// Automatically selects a theme based on the system or browser preferences. Falls back to <see cref="Dark" /> or
    /// <see cref="Light" />, or <see cref="HighContrastLight" /> when forced colors are active.
    /// </summary>
    System,

    /// <summary>Represents a WCAG 2.2 AA–compliant light theme preset.</summary>
    Light,

    /// <summary>Represents a WCAG 2.2 AA–compliant dark theme preset.</summary>
    Dark,

    /// <summary>Represents a light high-contrast theme optimized for accessibility and forced-color modes.</summary>
    HighContrastLight,

    /// <summary>Represents a dark high-contrast theme optimized for accessibility and forced-color modes.</summary>
    HighContrastDark
}
