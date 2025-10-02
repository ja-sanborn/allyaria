# Allyaria.Theming.Styles.AllyariaPalette

`AllyariaPalette` is an immutable, strongly typed palette used by the Allyaria theme engine to compute effective *
*foreground, background, and border styles**, including hover and disabled variants.
It enforces documented precedence rules: background images override region colors; explicit overrides beat defaults;
borders render only when a width is provided.
Intended for inline style generation and CSS custom property emission.

## Constructors

`AllyariaPalette(AllyariaColorValue? backgroundColor = null, AllyariaColorValue? foregroundColor = null, AllyariaImageValue? backgroundImage = null, bool backgroundImageStretch = true, int? borderWidth = 0, AllyariaColorValue? borderColor = null, AllyariaStringValue? borderStyle = null, AllyariaStringValue? borderRadius = null)`
Initializes a new immutable palette with optional colors, borders, and image. Applies default precedence and values.

* Exceptions: *None*

## Properties

| Name              | Type                   | Description                                                                                                                            |
|-------------------|------------------------|----------------------------------------------------------------------------------------------------------------------------------------|
| `BackgroundColor` | `AllyariaColorValue`   | Effective background color; defaults to `Colors.White` when unset.                                                                     |
| `BackgroundImage` | `AllyariaImageValue?`  | Effective background image, or `null` when none.                                                                                       |
| `BorderColor`     | `AllyariaColorValue`   | Effective border color; defaults to `BackgroundColor` if none supplied.                                                                |
| `BorderRadius`    | `AllyariaStringValue?` | Border radius token (e.g., `4px`), or `null` if unset.                                                                                 |
| `BorderStyle`     | `AllyariaStringValue`  | Border style token (e.g., `solid`); defaults to `solid`.                                                                               |
| `BorderWidth`     | `AllyariaNumberValue?` | Border width declaration (e.g., `1px`), or `null` if no border.                                                                        |
| `ForegroundColor` | `AllyariaColorValue`   | Effective foreground: chosen for highest WCAG contrast against background, or adjusted from explicit foreground to meet minimum ratio. |

## Methods

| Name                                                                                                                                      | Returns           | Description                                                                                                      |
|-------------------------------------------------------------------------------------------------------------------------------------------|-------------------|------------------------------------------------------------------------------------------------------------------|
| `Cascade(backgroundColor, foregroundColor, backgroundImage, backgroundImageStretch, borderWidth, borderColor, borderStyle, borderRadius)` | `AllyariaPalette` | Produces a new palette by applying raw overrides, falling back to current values.                                |
| `ToCss()`                                                                                                                                 | `string`          | Builds inline CSS declarations (`color`, `background-color`, border, radius) according to precedence rules.      |
| `ToCssVars(prefix = "")`                                                                                                                  | `string`          | Builds CSS custom property declarations. Normalizes optional prefix to kebab-case.                               |
| `ToDisabledPalette(desaturateBy = 60, valueBlendTowardMid = 0.15, minimumContrast = 3.0)`                                                 | `AllyariaPalette` | Produces a palette for disabled state by desaturating background/border and relaxing contrast.                   |
| `ToHoverPalette(backgroundDeltaV = 6, borderDeltaV = 8, minimumContrast = 4.5)`                                                           | `AllyariaPalette` | Produces a palette for hover state by adjusting Value (V) of background/border and ensuring readable foreground. |

## Operators

| Operator    | Returns | Description          |
|-------------|---------|----------------------|
| `==` / `!=` | `bool`  | Equality comparison. |

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* Background images take precedence over background colors.
* Explicit overrides (foreground, border) beat defaults.
* Borders render only when `BorderWidth > 0`.
* `ForegroundColor` uses **WCAG contrast ratio calculations**:

    * Auto-selects white/black when unset.
    * Adjusts explicit foreground to minimum 4.5:1 against background.
* `ToCssVars` normalizes prefixes: `"My Theme"` → `--my-theme-`. Defaults to `--aa-`.
* Disabled palettes reduce saturation and blend Value toward mid-range (50).
* Hover palettes adjust Value directionally (darken light surfaces, lighten dark surfaces).

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Constants;

var palette = new AllyariaPalette(backgroundColor: Colors.Grey50);
var css = palette.ToCss(); 
// "background-color:#fafafa;color:#000;border-style:solid;..."
```

### Expanded Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Constants;
using Allyaria.Theming.Values;

public class PaletteDemo
{
    public void ApplyPalettes()
    {
        var basePalette = new AllyariaPalette(
            backgroundColor: Colors.Grey100,
            borderWidth: 1,
            borderStyle: new AllyariaStringValue("solid"),
            borderRadius: new AllyariaStringValue("8px")
        );

        var hover = basePalette.ToHoverPalette();
        var disabled = basePalette.ToDisabledPalette();

        Console.WriteLine("Base: " + basePalette.ToCss());
        Console.WriteLine("Hover: " + hover.ToCss());
        Console.WriteLine("Disabled: " + disabled.ToCssVars("btn"));
    }
}
```

> *Rev Date: 2025-10-01*
