# Allyaria.Theming.Styles.AryStyle

`AryStyle` represents a **composite theming structure** that combines color palette, typography, spacing, and border
configurations into a unified, serializable style definition.
It serves as the atomic style layer for Allyaria components — encapsulating appearance, layout, and text behavior
consistently across states and themes.

## Constructors

`AryStyle()`
Initializes a style using default theme components:

* `AryPalette` (color scheme)
* `AryTypo` (typography)
* `ArySpacing` (margins and paddings)
* `AryBorders` (border definitions)

`AryStyle(AryPalette? palette = null, AryTypo? typography = null, ArySpacing? spacing = null, AryBorders? border = null)`
Initializes a style with optional overrides for each component.
Any `null` parameter falls back to the default for that component type.

* Exceptions: *None*

## Properties

| Name      | Type         | Description                                                            |
|-----------|--------------|------------------------------------------------------------------------|
| `Palette` | `AryPalette` | The color palette (background, foreground, border) for the style.      |
| `Typo`    | `AryTypo`    | The typography configuration (font, size, weight, spacing, alignment). |
| `Spacing` | `ArySpacing` | The logical spacing configuration (margins and paddings).              |
| `Border`  | `AryBorders` | The border configuration (width, style, and radius per side/corner).   |

## Methods

| Name                                                                                                                     | Returns    | Description                                                                                                                                                      |
|--------------------------------------------------------------------------------------------------------------------------|------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Cascade(AryPalette? palette = null, AryTypo? typography = null, ArySpacing? spacing = null, AryBorders? border = null)` | `AryStyle` | Returns a new `AryStyle` instance with selectively overridden components, preserving any unspecified ones.                                                       |
| `ToCss(string? varPrefix = "")`                                                                                          | `string`   | Converts the entire style into a CSS declaration block. When a prefix is provided, all declarations become scoped CSS custom properties (`--{prefix}-property`). |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* Each subcomponent (`Palette`, `Typo`, `Spacing`, `Border`) is **strongly typed**, immutable, and individually
  serializable.
* All defaults are theme-consistent through `StyleDefaults`.
* Enables consistent **state-based styling** (default, hover, focused, disabled) via composition or cascading.
* `ToCss` concatenates outputs from all subcomponents, producing fully scoped variable-based CSS output for theming
  systems.
* Ideal for representing component-level styles (e.g., buttons, surfaces, text blocks) or exporting design tokens.

## Examples

### Minimal Example

```csharp
var style = new AryStyle();
var css = style.ToCss();
// Includes palette, typography, spacing, and border declarations
```

### Expanded Example

```csharp
var customStyle = new AryStyle(
    palette: new AryPalette(
        backgroundColor: new AryColorValue("#121212"),
        foregroundColor: new AryColorValue("#ffffff")
    ),
    typography: new AryTypo(
        fontFamily: new AryStringValue("Roboto, sans-serif"),
        fontWeight: FontWeight.Bold,
        textAlign: TextAlign.Center
    )
);

var hoverStyle = customStyle.Cascade(
    palette: ColorHelper.DeriveHovered(customStyle.Palette)
);

Console.WriteLine(hoverStyle.ToCss("btn"));
// --btn-color: #fff;
// --btn-background-color: #1e1e1e;
// --btn-font-weight: bold;
// --btn-text-align: center;
```

> *Rev Date: 2025-10-06*
