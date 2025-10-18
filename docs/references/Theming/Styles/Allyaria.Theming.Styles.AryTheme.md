# Allyaria.Theming.Styles.AryTheme

`AryTheme` is the root Allyaria theme struct that composes borders, spacing, palette variants (
light/dark/high-contrast), and component typography. It serves as the primary entry point for generating per-component,
per-state styles in a consistent, immutable, and strongly typed way.

## Constructors

| Signature                                                                                                                                                                                                      | Description                                                                                                                               |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------|
| `AryTheme()`                                                                                                                                                                                                   | Initializes a new theme instance with default borders, spacing, palettes, and typography.                                                 |
| `AryTheme(AryBorders? borders = null, ArySpacing? spacing = null, AryPalette? paletteLight = null, AryPalette? paletteDark = null, AryPalette? paletteHighContrast = null, AryTypography? typoSurface = null)` | Initializes a new instance of `AryTheme` using optional subcomponents. Any parameter left `null` falls back to its default configuration. |

## Properties

| Name      | Type                | Description                                                                        |
|-----------|---------------------|------------------------------------------------------------------------------------|
| `Borders` | `AryBorders`        | The border configuration applied by the theme.                                     |
| `Palette` | `AryPaletteVariant` | The palette variant collection (light, dark, and high-contrast) used by the theme. |
| `Spacing` | `ArySpacing`        | The spacing configuration (margins and paddings) used by the theme.                |
| `Typo`    | `AryTypographyArea` | The typography configuration defining component font styles.                       |

## Methods

| Name      | Signature                                                                                                                                                                                                              | Description                                                                                                                                                    | Returns    |
|-----------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------|------------|
| `Cascade` | `AryTheme Cascade(AryBorders? borders = null, ArySpacing? spacing = null, AryPalette? paletteLight = null, AryPalette? paletteDark = null, AryPalette? paletteHighContrast = null, AryTypography? typoSurface = null)` | Returns a new theme with specified component overrides. Unspecified values are preserved from the existing instance.                                           | `AryTheme` |
| `ToCss`   | `string ToCss(ThemeType themeType, ComponentType componentType, ComponentElevation elevation = ComponentElevation.Mid, ComponentState state = ComponentState.Default, string? varPrefix = "")`                         | Produces a concatenated CSS string for the resolved style. When the state is `Focused`, the border uses a thicker dashed focus variant.                        | `string`   |
| `ToStyle` | `AryStyle ToStyle(ThemeType themeType, ComponentType componentType, ComponentElevation elevation = ComponentElevation.Mid, ComponentState state = ComponentState.Default)`                                             | Resolves a concrete `AryStyle` combining the theme’s palette, typography, spacing, and borders for the given theme type, component type, elevation, and state. | `AryStyle` |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
// Create a theme with defaults
var theme = new AryTheme();

// Generate CSS for a hovered surface component in light mode
string css = theme.ToCss(
    ThemeType.Light,
    ComponentType.Surface,
    ComponentElevation.Mid,
    ComponentState.Hovered,
    "surface"
);

// Example output:
// --surface-background-color: #ffffff;
// --surface-color: #000000;
// --surface-border-color: #dddddd;
```

---

*Revision Date: 2025-10-17*
