# Allyaria.Theming.Styles.AryTypo

`AryTypo` defines a strongly typed typography token set for the Allyaria theming system.
It represents a complete text style configuration, including font, spacing, alignment, and decoration settings — all
with default, theme-aware fallbacks.

## Constructors

`AryTypo()`
Initializes all typography members with values from `StyleDefaults` (font family, size, weight, spacing, and alignment).

`AryTypo(AryStringValue? fontFamily = null, AryNumberValue? fontSize = null, AryStringValue? fontStyle = null, AryStringValue? fontWeight = null, AryNumberValue? letterSpacing = null, AryNumberValue? lineHeight = null, AryStringValue? textAlign = null, AryStringValue? textDecorationLine = null, AryStringValue? textDecorationStyle = null, AryStringValue? textTransform = null, AryStringValue? verticalAlign = null)`
Initializes a new instance with explicit values.
All unspecified parameters fall back to `StyleDefaults`.

* Exceptions: *None*

## Properties

| Name                  | Type             | Description                                                  |
|-----------------------|------------------|--------------------------------------------------------------|
| `FontFamily`          | `AryStringValue` | Font family stack (e.g., `"Inter, 'Segoe UI', sans-serif"`). |
| `FontSize`            | `AryNumberValue` | Font size (e.g., `16px`, `1rem`).                            |
| `FontStyle`           | `AryStringValue` | Font style (e.g., `normal`, `italic`).                       |
| `FontWeight`          | `AryStringValue` | Font weight (e.g., `400`, `bold`).                           |
| `LetterSpacing`       | `AryNumberValue` | Letter spacing (e.g., `0.5px`).                              |
| `LineHeight`          | `AryNumberValue` | Line height (e.g., `1.5`, `24px`).                           |
| `TextAlign`           | `AryStringValue` | Text alignment (e.g., `start`, `center`, `justify`).         |
| `TextDecorationLine`  | `AryStringValue` | Text decoration line (e.g., `underline`, `none`).            |
| `TextDecorationStyle` | `AryStringValue` | Text decoration style (e.g., `solid`, `wavy`).               |
| `TextTransform`       | `AryStringValue` | Text transform (e.g., `uppercase`, `capitalize`).            |
| `VerticalAlign`       | `AryStringValue` | Vertical alignment (e.g., `baseline`, `middle`).             |

## Methods

| Name                                                                                                                                                                                                                                                                                                                                                                                                                             | Returns   | Description                                                                                                                                                          |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Cascade(AryStringValue? fontFamily = null, AryNumberValue? fontSize = null, AryStringValue? fontStyle = null, AryStringValue? fontWeight = null, AryNumberValue? letterSpacing = null, AryNumberValue? lineHeight = null, AryStringValue? textAlign = null, AryStringValue? textDecorationLine = null, AryStringValue? textDecorationStyle = null, AryStringValue? textTransform = null, AryStringValue? verticalAlign = null)` | `AryTypo` | Creates a new `AryTypo` with selective overrides for one or more members. Unspecified values are preserved.                                                          |
| `ToCss(string? varPrefix = "")`                                                                                                                                                                                                                                                                                                                                                                                                  | `string`  | Generates a CSS declaration block for all typography properties. When a `varPrefix` is provided, values are emitted as CSS variables (e.g., `--{prefix}-font-size`). |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* All instances are **immutable** (`readonly record struct`), supporting value-based theming.
* Fallbacks come from `StyleDefaults`, ensuring consistent baseline typography across components.
* Fully compatible with Allyaria’s `StyleHelper.ToCss` for CSS output and variable mapping.
* Designed for accessibility — readable, adaptable, and themable font configurations.
* Theming layers may dynamically override these properties for surface or component-level typography variants.

## Examples

### Minimal Example

```csharp
var typo = new AryTypo();
var css = typo.ToCss();
// "font-family: system-ui, ...; font-size: 1rem; line-height: 1.5; ..."
```

### Expanded Example

```csharp
var heading = new AryTypo(
    fontFamily: new AryStringValue("Roboto, sans-serif"),
    fontSize: new AryNumberValue("2rem"),
    fontWeight: FontWeight.Bold7,
    textAlign: TextAlign.Center,
    textTransform: TextTransform.Uppercase
);

var emphasized = heading.Cascade(
    fontStyle: FontStyle.Italic,
    textDecorationLine: TextDecorationLine.Underline
);

Console.WriteLine(emphasized.ToCss("h1"));
// --h1-font-family: Roboto, sans-serif;
// --h1-font-weight: 700;
// --h1-text-transform: uppercase;
```

> *Rev Date: 2025-10-06*
