# Allyaria.Theming.Enumerations.ThemeType

`ThemeType` is an enumeration that defines the available theme modes supported by Allyaria. It represents the visual
appearance options that can be applied at runtime through `AryTheme`, enabling dynamic adaptation to user or system
preferences.

## Constructors

*None*

## Properties

| Name           | Type        | Description                                                                                                                                                 |
|----------------|-------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `System`       | `ThemeType` | Automatically selects a theme based on the system or browser preferences, falling back to `Dark`, `Light`, or `HighContrast` when forced colors are active. |
| `Light`        | `ThemeType` | Represents a WCAG 2.2 AA–compliant light theme preset.                                                                                                      |
| `Dark`         | `ThemeType` | Represents a WCAG 2.2 AA–compliant dark theme preset.                                                                                                       |
| `HighContrast` | `ThemeType` | Represents a high-contrast theme optimized for accessibility and forced-color modes.                                                                        |

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
ThemeType theme = ThemeType.System;
```

---

*Revision Date: 2025-10-17*
