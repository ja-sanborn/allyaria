# Allyaria.Theming.Styles.AllyariaPalette

`AllyariaPalette` is an immutable value type that represents a *resolved color/image palette* used by the Allyaria
theming engine. It applies *precedence rules*, computes *accessible foregrounds*, and can derive *disabled* and *hover*
state variants. CSS fragments can be emitted as inline declarations or CSS variables.

---

## Constructors

`AllyariaPalette(AllyariaColorValue? backgroundColor = null, AllyariaColorValue? foregroundColor = null, AllyariaColorValue? borderColor = null, AllyariaImageValue? backgroundImage = null, bool backgroundImageStretch = true)`
Initializes a new palette with optional colors and background image. Applies default precedence rules.

* Exceptions: *None*

---

## Properties

| Name              | Type                  | Description                                                                                                                                                                                    |
|-------------------|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `BackgroundColor` | `AllyariaColorValue`  | Effective background color. Defaults to `Colors.White` when unset.                                                                                                                             |
| `BackgroundImage` | `AllyariaImageValue?` | Background image if present; otherwise `null`.                                                                                                                                                 |
| `BorderColor`     | `AllyariaColorValue`  | Effective border color. Defaults to `BackgroundColor` when unset.                                                                                                                              |
| `ForegroundColor` | `AllyariaColorValue`  | Foreground color computed for maximum WCAG contrast against background. Uses white/black fallback when not explicitly set; explicit foregrounds are adjusted to at least 4.5:1 contrast ratio. |

---

## Methods

| Name                                                                                                           | Returns            | Description                                                                                                                                  |
|----------------------------------------------------------------------------------------------------------------|--------------------|----------------------------------------------------------------------------------------------------------------------------------------------|
| `Cascade(backgroundColor, foregroundColor, borderColor, backgroundImage, backgroundImageStretch)`              | `AllyariaPalette ` | Returns a new palette with overrides applied. Any `null` values inherit from current effective values.                                       |
| `ToCss()`                                                                                                      | `string`           | Builds a semicolon-terminated CSS declaration string for background, foreground, border, and optional background image.                      |
| `ToCssVars(string prefix = "")`                                                                                | `string`           | Builds CSS custom property declarations for colors and background image. Normalizes prefix (lowercased, hyphenated). Defaults to `"--aa-"`.  |
| `ToDisabledPalette(double desaturateBy = 60, double valueBlendTowardMid = 0.15, double minimumContrast = 3.0)` | `AllyariaPalette`  | Produces a disabled variant: background/border are desaturated and blended toward V=50, foreground adjusted to at least 3:1 contrast.        |
| `ToHoverPalette(double backgroundDeltaV = 6, double borderDeltaV = 8, double minimumContrast = 4.5)`           | `AllyariaPalette`  | Produces a hover variant: background/border Value nudged up or down depending on light/dark, foreground adjusted to at least 4.5:1 contrast. |

---

## Operators

| Operator    | Returns | Description          |
|-------------|---------|----------------------|
| `==` / `!=` | `bool`  | Equality comparison. |

---

## Events

*None*

---

## Exceptions

*None*

---

## Behavior Notes

* Precedence rules:

    * Background image overrides background color.
    * Explicit overrides take priority over defaults.
    * Border color defaults to background color.
* Foreground color resolution:

    * Auto-selects black or white based on higher WCAG ratio when unset.
    * Explicit foregrounds are adjusted using `ColorHelper.EnsureMinimumContrast` with 4.5:1 minimum.
* Disabled palette: desaturates and blends values toward mid-tones.
* Hover palette: increases/decreases Value depending on background brightness.
* CSS emission:

    * `ToCss` → ready-to-use inline style string.
    * `ToCssVars` → variable declarations, e.g. `--aa-background-color:#fafafa;`.

---

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Constants;

var palette = new AllyariaPalette(backgroundColor: Colors.Grey50);
Console.WriteLine(palette.ToCss());
// background-color:#FAFAFA;color:#000000;border-color:#FAFAFA;
```

### Expanded Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Constants;

public class PaletteDemo
{
    public void Demo()
    {
        var basePalette = new AllyariaPalette(
            backgroundColor: Colors.Grey100,
            borderColor: Colors.Grey400
        );

        var hover = basePalette.ToHoverPalette();
        var disabled = basePalette.ToDisabledPalette();

        Console.WriteLine("Base: " + basePalette.ToCss());
        Console.WriteLine("Hover: " + hover.ToCss());
        Console.WriteLine("Disabled: " + disabled.ToCssVars("btn"));
    }
}
```

> *Rev Date: 2025-10-02*
