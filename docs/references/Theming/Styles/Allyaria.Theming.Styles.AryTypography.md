# Allyaria.Theming.Styles.AryTypography

`AryTypography` is a readonly record struct that defines a complete, strongly typed set of typography tokens for the
Allyaria theming system. It provides immutable values for font family, size, weight, spacing, and decorative attributes,
enabling consistent and accessible text rendering across components. All members default to `StyleDefaults` when not
explicitly provided.

## Constructors

| Signature                                                                                                                                                                                                                                                                                                                                                                                                                              | Description                                                                                                                     |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------|
| `AryTypography()`                                                                                                                                                                                                                                                                                                                                                                                                                      | Initializes a new `AryTypography` instance using the default values from `StyleDefaults`.                                       |
| `AryTypography(AryStringValue? fontFamily = null, AryNumberValue? fontSize = null, AryStringValue? fontStyle = null, AryStringValue? fontWeight = null, AryNumberValue? letterSpacing = null, AryNumberValue? lineHeight = null, AryStringValue? textAlign = null, AryStringValue? textDecorationLine = null, AryStringValue? textDecorationStyle = null, AryStringValue? textTransform = null, AryStringValue? verticalAlign = null)` | Initializes a new instance with optional parameters. Any `null` argument falls back to the corresponding `StyleDefaults` value. |

## Properties

| Name                  | Type             | Description                                              |
|-----------------------|------------------|----------------------------------------------------------|
| `FontFamily`          | `AryStringValue` | The font family (e.g., `Inter, "Segoe UI", sans-serif`). |
| `FontSize`            | `AryNumberValue` | The font size (e.g., `14px`, `1rem`).                    |
| `FontStyle`           | `AryStringValue` | The font style (e.g., `normal`, `italic`).               |
| `FontWeight`          | `AryStringValue` | The font weight (e.g., `400`, `bold`).                   |
| `LetterSpacing`       | `AryNumberValue` | The letter spacing (e.g., `0.02em`).                     |
| `LineHeight`          | `AryNumberValue` | The line height (e.g., `1.5`, `24px`).                   |
| `TextAlign`           | `AryStringValue` | The text alignment (e.g., `left`, `center`, `start`).    |
| `TextDecorationLine`  | `AryStringValue` | The text decoration line (e.g., `underline`, `none`).    |
| `TextDecorationStyle` | `AryStringValue` | The text decoration style (e.g., `solid`, `wavy`).       |
| `TextTransform`       | `AryStringValue` | The text transformation (e.g., `uppercase`, `none`).     |
| `VerticalAlign`       | `AryStringValue` | The vertical alignment (e.g., `baseline`, `middle`).     |

## Methods

| Name      | Signature                                                                                                                                                                                                                                                                                                                                                                                                                                      | Description                                                                                                                                | Returns         |
|-----------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------|-----------------|
| `Cascade` | `AryTypography Cascade(AryStringValue? fontFamily = null, AryNumberValue? fontSize = null, AryStringValue? fontStyle = null, AryStringValue? fontWeight = null, AryNumberValue? letterSpacing = null, AryNumberValue? lineHeight = null, AryStringValue? textAlign = null, AryStringValue? textDecorationLine = null, AryStringValue? textDecorationStyle = null, AryStringValue? textTransform = null, AryStringValue? verticalAlign = null)` | Returns a new `AryTypography` instance with the specified overrides applied while retaining existing values for unspecified parameters.    | `AryTypography` |
| `ToCss`   | `string ToCss(string? varPrefix = "")`                                                                                                                                                                                                                                                                                                                                                                                                         | Builds a string of CSS declarations representing the typography values. Optionally prefixes properties for CSS custom variable generation. | `string`        |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
// Create a custom typography with modified font size and weight
var typo = new AryTypography(
    fontSize: StyleDefaults.FontSize,
    fontWeight: new AryStringValue("600")
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
