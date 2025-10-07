# Allyaria.Theming.Styles.AryPalette

`AryPalette` represents a strongly typed color palette for Allyaria theming.
It defines **background**, **foreground**, and **border** colors, and automatically enforces WCAG-compliant contrast
ratios.
Palettes form the foundation for component color states (default, hover, pressed, etc.) within Allyaria.

## Constructors

`AryPalette()`
Initializes a palette using the **light theme defaults**:

* `BackgroundColorLight`
* `ForegroundColorLight`
* `BorderColor` equal to `BackgroundColor`.

Enforces a minimum contrast ratio of **4.5:1** between foreground and background colors.

`AryPalette(AryColorValue? backgroundColor = null, AryColorValue? foregroundColor = null, AryColorValue? borderColor = null)`
Initializes a palette with custom colors. Any `null` value defaults to the corresponding `StyleDefaults`.
After construction, the foreground color is automatically adjusted using
`ColorHelper.EnsureMinimumContrast(foreground, background, 4.5)`.

* Exceptions: *None*

## Properties

| Name              | Type            | Description                                                                         |
|-------------------|-----------------|-------------------------------------------------------------------------------------|
| `BackgroundColor` | `AryColorValue` | The palette’s background color.                                                     |
| `ForegroundColor` | `AryColorValue` | The text or content color. Automatically contrast-corrected against the background. |
| `BorderColor`     | `AryColorValue` | The border or outline color, defaulting to the background color.                    |

## Methods

| Name                                                                                                                       | Returns      | Description                                                                                                                                                                            |
|----------------------------------------------------------------------------------------------------------------------------|--------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Cascade(AryColorValue? backgroundColor = null, AryColorValue? foregroundColor = null, AryColorValue? borderColor = null)` | `AryPalette` | Returns a new palette with selectively overridden colors. Automatically enforces a 4.5:1 contrast ratio between foreground and background.                                             |
| `ToCss(string? varPrefix = "")`                                                                                            | `string`     | Generates CSS declarations for `color`, `background-color`, and `border-color`. When `varPrefix` is provided, property names are emitted as CSS custom variables (`--{prefix}-color`). |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* Ensures **minimum 4.5:1 contrast ratio** (WCAG 2.2 AA).
* Works seamlessly with `ColorHelper` methods to derive hover, focus, pressed, and disabled palettes.
* Uses **immutable record struct semantics**—`Cascade` creates a new palette instance without mutating the original.
* Supports integration with Allyaria’s style builders (`StyleHelper.ToCss`) for CSS emission.
* Can be combined with elevation and state helpers for dynamic theming.

## Examples

### Minimal Example

```csharp
var palette = new AryPalette();
var css = palette.ToCss();
// "color: #212121; background-color: #fafafa; border-color: #fafafa;"
```

### Expanded Example

```csharp
var customPalette = new AryPalette(
    backgroundColor: new AryColorValue("#121212"),
    foregroundColor: new AryColorValue("#e0e0e0")
);

var pressed = customPalette.Cascade(
    backgroundColor: new AryColorValue("#1e1e1e")
);

Console.WriteLine(pressed.ToCss("btn"));
// --btn-color: #e0e0e0;
// --btn-background-color: #1e1e1e;
// --btn-border-color: #1e1e1e;
```

> *Rev Date: 2025-10-06*
