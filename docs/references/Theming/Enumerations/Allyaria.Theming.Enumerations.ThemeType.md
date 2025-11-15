# Allyaria.Theming.Enumerations.ThemeType

`ThemeType` is an enumeration defining the visual theme modes supported by the Allyaria theming system. These options
represent selectable appearance presets applied at runtime and include adaptive, light, dark, and high-contrast themes
designed for accessibility and WCAG compliance.

## Summary

`ThemeType` is an enum representing available theme modes, including system-adaptive, standard light/dark, and
high-contrast variants. It enables applications to declaratively specify which color scheme should be applied and
supports accessibility requirements such as forced-color modes.

## Constructors

*None*

## Properties

*None*

## Methods

*None*

## Fields

| Name                | Type        | Description                                                                                                                                       |
|---------------------|-------------|---------------------------------------------------------------------------------------------------------------------------------------------------|
| `System`            | `ThemeType` | Automatically selects a theme based on OS/browser preferences, falling back to `Dark`, `Light`, or `HighContrastLight` in forced-color scenarios. |
| `Light`             | `ThemeType` | WCAG 2.2 AA–compliant light theme preset.                                                                                                         |
| `Dark`              | `ThemeType` | WCAG 2.2 AA–compliant dark theme preset.                                                                                                          |
| `HighContrastLight` | `ThemeType` | Accessibility-optimized high-contrast light preset, suitable for forced-color environments.                                                       |
| `HighContrastDark`  | `ThemeType` | Accessibility-optimized high-contrast dark preset, suitable for forced-color environments.                                                        |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Enumerations;

public class ThemeSettings
{
    public ThemeType CurrentTheme { get; private set; } = ThemeType.System;

    public void SetDarkMode()
    {
        CurrentTheme = ThemeType.Dark;
    }

    public void SetHighContrast()
    {
        CurrentTheme = ThemeType.HighContrastLight;
    }
}
```

---

*Revision Date: 2025-11-15*
