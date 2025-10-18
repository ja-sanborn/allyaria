# Allyaria.Theming.Styles.AryPaletteVariant

`AryPaletteVariant` is a readonly record struct that represents a theming variant containing separate `AryPalette`
definitions for light, dark, and high-contrast modes. Each variant includes its own elevation hierarchy to ensure visual
consistency across elevations and interaction states, enabling components to adapt dynamically to user or system theme
preferences.

## Constructors

| Signature                                                                                                                    | Description                                                                                                                                                                                                                   |
|------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `AryPaletteVariant()`                                                                                                        | Initializes a new `AryPaletteVariant` using default palettes for light, dark, and high-contrast modes based on `StyleDefaults`.                                                                                               |
| `AryPaletteVariant(AryPalette? paletteLight = null, AryPalette? paletteDark = null, AryPalette? paletteHighContrast = null)` | Initializes a new instance of `AryPaletteVariant` with optional custom palettes for each mode. Missing values fall back to `StyleDefaults` presets, and elevation hierarchies are constructed automatically for each palette. |

## Properties

| Name                    | Type                  | Description                                                             |
|-------------------------|-----------------------|-------------------------------------------------------------------------|
| `LightPalette`          | `AryPalette`          | The base palette for light mode.                                        |
| `DarkPalette`           | `AryPalette`          | The base palette for dark mode.                                         |
| `HighContrastPalette`   | `AryPalette`          | The base palette for high-contrast mode.                                |
| `LightElevation`        | `AryPaletteElevation` | The elevation hierarchy for light mode, including all elevation layers. |
| `DarkElevation`         | `AryPaletteElevation` | The elevation hierarchy for dark mode.                                  |
| `HighContrastElevation` | `AryPaletteElevation` | The elevation hierarchy for high-contrast mode.                         |

## Methods

| Name        | Signature                                                                                                                            | Description                                                                                                                                                                                                    | Returns             |
|-------------|--------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------|
| `Cascade`   | `AryPaletteVariant Cascade(AryPalette? paletteLight = null, AryPalette? paletteDark = null, AryPalette? paletteHighContrast = null)` | Returns a new `AryPaletteVariant` instance with updated light, dark, or high-contrast palettes. Any unspecified palette inherits its existing value, and corresponding elevation hierarchies are recalculated. | `AryPaletteVariant` |
| `ToPalette` | `AryPalette ToPalette(ThemeType themeType, ComponentElevation elevation, ComponentState state)`                                      | Returns the `AryPalette` appropriate for the given `ThemeType`, `ComponentElevation`, and `ComponentState`. Defaults to the light mode hierarchy if the theme type is unrecognized.                            | `AryPalette`        |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
// Create a palette variant with defaults
var variant = new AryPaletteVariant();

// Get a palette for a focused button in dark mode with medium elevation
AryPalette focusedPalette = variant.ToPalette(
    ThemeType.Dark,
    ComponentElevation.Mid,
    ComponentState.Focused
);
```

---

*Revision Date: 2025-10-17*
