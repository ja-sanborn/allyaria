# Allyaria.Theming.Styles.AryTheme

`AryTheme` represents the **root theming configuration** in the Allyaria design system.
It composes borders, spacing, color palettes (light/dark/high-contrast), and typography into a unified, immutable theme
definition.
This is the primary entry point for resolving **component-level styles** at runtime.

## Constructors

`AryTheme()`
Initializes a theme using all default subcomponents:

* Borders → `AryBorders`
* Spacing → `ArySpacing`
* Palette → `AryPaletteVariant` (auto-generated light/dark/high-contrast)
* Typography → `AryTypoComponent` (default surface typography)

`AryTheme(AryBorders? borders = null, ArySpacing? spacing = null, AryPalette? paletteLight = null, AryPalette? paletteDark = null, AryPalette? paletteHighContrast = null, AryTypo? typoSurface = null)`
Initializes a theme with optional overrides for any of the above subcomponents.
If a parameter is omitted (`null`), the theme substitutes a default for that section.

* Exceptions: *None*

## Properties

| Name      | Type                | Description                                                                         |
|-----------|---------------------|-------------------------------------------------------------------------------------|
| `Borders` | `AryBorders`        | The border configuration (width, style, and radii) shared across components.        |
| `Spacing` | `ArySpacing`        | The global spacing configuration (margins and paddings).                            |
| `Palette` | `AryPaletteVariant` | A collection of light, dark, and high-contrast palettes.                            |
| `Typo`    | `AryTypoComponent`  | The typography component mappings (per component type, e.g., Surface, Body1, etc.). |

> *All properties are immutable and resolved on initialization.*

## Methods

| Name                                                                                                                                                                                                    | Returns    | Description                                                                                                                                              |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Cascade(AryBorders? borders = null, ArySpacing? spacing = null, AryPalette? paletteLight = null, AryPalette? paletteDark = null, AryPalette? paletteHighContrast = null, AryTypo? typoSurface = null)` | `AryTheme` | Returns a new `AryTheme` with selected subcomponents overridden. Unspecified values are preserved.                                                       |
| `ToStyle(ThemeType themeType, ComponentType componentType, ComponentElevation elevation = ComponentElevation.Mid, ComponentState state = ComponentState.Default)`                                       | `AryStyle` | Resolves a fully composed `AryStyle` (palette + typography + spacing + border) for a given theme type, component type, elevation, and interaction state. |
| `ToCss(ThemeType themeType, ComponentType componentType, ComponentElevation elevation = ComponentElevation.Mid, ComponentState state = ComponentState.Default, string? varPrefix = "")`                 | `string`   | Converts the resolved style into a concatenated CSS variable declaration string (optionally prefixed).                                                   |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* Provides **non-destructive cascading**: overrides apply selectively, preserving existing state.
* Resolves **accessible** color contrast automatically for all palette combinations (via `ColorHelper`).
* Supports **WCAG 2.2 AA** compliant contrast and elevation-based tone differentiation.
* Serves as the primary API entry for applying dynamic theming at component level.
* Combines `AryPaletteVariant`, `AryTypoComponent`, `AryBorders`, and `ArySpacing` to produce complete styles.
* Compatible with both **server-rendered** and **client-side dynamic** theming pipelines.

## Examples

### Minimal Example

```csharp
var theme = new AryTheme();
var css = theme.ToCss(
    ThemeType.Light,
    ComponentType.Surface
);

// Outputs a complete CSS variable block for a light surface
```

### Expanded Example

```csharp
var darkTheme = new AryTheme(
    paletteDark: new AryPalette(
        backgroundColor: new AryColorValue("#121212"),
        foregroundColor: new AryColorValue("#e0e0e0")
    ),
    typoSurface: new AryTypo(
        fontFamily: new AryStringValue("Roboto, sans-serif"),
        fontWeight: FontWeight.Bold
    )
);

var pressedStyle = darkTheme.ToStyle(
    ThemeType.Dark,
    ComponentType.Surface,
    ComponentElevation.High,
    ComponentState.Pressed
);

Console.WriteLine(pressedStyle.ToCss("card"));
// --card-color: #e0e0e0;
// --card-background-color: #1a1a1a;
// --card-border-color: #1a1a1a;
// --card-font-family: Roboto, sans-serif;
// --card-font-weight: bold;
```

> *Rev Date: 2025-10-06*
