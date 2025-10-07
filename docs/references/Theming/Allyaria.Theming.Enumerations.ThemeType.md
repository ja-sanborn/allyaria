# Allyaria.Theming.Enumerations.ThemeType

`ThemeType` defines the available theme variants supported by Allyaria.
Each type corresponds to a complete color and tonal system, ensuring accessibility compliance and adaptive user
experience across devices and environments.

## Constructors

*Enum — no constructors.*

## Properties

*None*

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* `ThemeType.System` dynamically adapts to OS or browser-level appearance settings (light/dark/high contrast).
* `Light` and `Dark` maintain **WCAG 2.2 AA** contrast compliance.
* `HighContrast` is specifically designed for accessibility and forced-color environments.
* The `ThemeType` is used by `AllyariaTheme` to determine tonal palettes, elevations, and contrast overlays.

## Members

| Name           | Description                                                                                                                                              |
|----------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
| `System`       | Automatically selects a theme based on the system or browser preference. Falls back to `Dark`, `Light`, or `HighContrast` when forced colors are active. |
| `Light`        | Represents a WCAG 2.2 AA–compliant light theme preset.                                                                                                   |
| `Dark`         | Represents a WCAG 2.2 AA–compliant dark theme preset.                                                                                                    |
| `HighContrast` | Represents a high-contrast theme optimized for accessibility and forced-color modes.                                                                     |

## Examples

### Minimal Example

```csharp
var theme = ThemeType.System;
```

### Expanded Example

```csharp
public void ApplyUserTheme(ThemeType themeType)
{
    switch (themeType)
    {
        case ThemeType.Light:
            Console.WriteLine("Applying light theme colors and surfaces.");
            break;
        case ThemeType.Dark:
            Console.WriteLine("Applying dark theme tones for low-light environments.");
            break;
        case ThemeType.HighContrast:
            Console.WriteLine("Applying high-contrast palette for accessibility.");
            break;
        default:
            Console.WriteLine("Detecting system theme preference...");
            break;
    }
}
```

> *Rev Date: 2025-10-06*
