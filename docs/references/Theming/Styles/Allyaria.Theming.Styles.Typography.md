# Allyaria.Theming.Styles.Typography

`Typography` is a readonly record struct that defines a complete, strongly typed set of typography tokens for the
Allyaria theming system. It provides immutable values for font family, size, weight, spacing, and decorative attributes,
enabling consistent and accessible text rendering across components. All members default to `StyleDefaults` when not
explicitly provided.

## Constructors

`Typography()` Initializes a new `Typography` instance using the default values from `StyleDefaults`.

`Typography(ThemeString? fontFamily = null, ThemeNumber? fontSize = null, ThemeString? fontStyle = null, ThemeString? fontWeight = null, ThemeNumber? letterSpacing = null, ThemeNumber? lineHeight = null, ThemeString? textAlign = null, ThemeString? textDecorationLine = null, ThemeString? textDecorationStyle = null, ThemeString? textTransform = null, ThemeString? verticalAlign = null)`
Initializes a new instance with optional parameters. Any `null` argument falls back to the corresponding `StyleDefaults`
value.

## Properties

| Name                  | Type          | Description                                              |
|-----------------------|---------------|----------------------------------------------------------|
| `FontFamily`          | `ThemeString` | The font family (e.g., `Inter, "Segoe UI", sans-serif`). |
| `FontSize`            | `ThemeNumber` | The font size (e.g., `14px`, `1rem`).                    |
| `FontStyle`           | `ThemeString` | The font style (e.g., `normal`, `italic`).               |
| `FontWeight`          | `ThemeString` | The font weight (e.g., `400`, `bold`).                   |
| `LetterSpacing`       | `ThemeNumber` | The letter spacing (e.g., `0.02em`).                     |
| `LineHeight`          | `ThemeNumber` | The line height (e.g., `1.5`, `24px`).                   |
| `TextAlign`           | `ThemeString` | The text alignment (e.g., `left`, `center`, `start`).    |
| `TextDecorationLine`  | `ThemeString` | The text decoration line (e.g., `underline`, `none`).    |
| `TextDecorationStyle` | `ThemeString` | The text decoration style (e.g., `solid`, `wavy`).       |
| `TextTransform`       | `ThemeString` | The text transformation (e.g., `uppercase`, `none`).     |
| `VerticalAlign`       | `ThemeString` | The vertical alignment (e.g., `baseline`, `middle`).     |

## Methods

| Name      | Signature                                                                                                                                                                                                                                                                                                                                                                                                  | Description                                                                                                                                | Returns      |
|-----------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------|--------------|
| `Cascade` | `Typography Cascade(ThemeString? fontFamily = null, ThemeNumber? fontSize = null, ThemeString? fontStyle = null, ThemeString? fontWeight = null, ThemeNumber? letterSpacing = null, ThemeNumber? lineHeight = null, ThemeString? textAlign = null, ThemeString? textDecorationLine = null, ThemeString? textDecorationStyle = null, ThemeString? textTransform = null, ThemeString? verticalAlign = null)` | Returns a new `Typography` instance with the specified overrides applied while retaining existing values for unspecified parameters.       | `Typography` |
| `ToCss`   | `string ToCss(string? varPrefix = "")`                                                                                                                                                                                                                                                                                                                                                                     | Builds a string of CSS declarations representing the typography values. Optionally prefixes properties for CSS custom variable generation. | `string`     |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
// Create a custom typography with modified font size and weight
var typo = new Typography(
    fontSize: StyleDefaults.FontSize,
    fontWeight: new ThemeString("600")
);

// Convert typography to CSS declarations
string css = typo.ToCss("heading");
// Example output:
// --heading-font-family: Inter, "Segoe UI", sans-serif;
// --heading-font-weight: 600;
// --heading-font-size: 1rem;
```

---

*Revision Date: 2025-10-17*
