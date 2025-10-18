# Allyaria.Theming.Styles.ArySpacing

`ArySpacing` is a readonly record struct that represents logical spacing (margins and paddings) for a themable element.
It provides strongly typed value members and helper methods to generate CSS declarations. Defaults are automatically
sourced from `StyleDefaults`, and the struct is immutable for safe value semantics.

## Constructors

| Signature                                                                                                                                                                                                                                                                                                  | Description                                                                                                                                                                    |
|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `ArySpacing()`                                                                                                                                                                                                                                                                                             | Initializes a new instance using default margin and padding values from `StyleDefaults`.                                                                                       |
| `ArySpacing(AryNumberValue? marginTop = null, AryNumberValue? marginEnd = null, AryNumberValue? marginBottom = null, AryNumberValue? marginStart = null, AryNumberValue? paddingTop = null, AryNumberValue? paddingEnd = null, AryNumberValue? paddingBottom = null, AryNumberValue? paddingStart = null)` | Initializes a new instance with optionally specified margins and paddings for each side. Any unspecified values default to `StyleDefaults.Margin` and `StyleDefaults.Padding`. |

## Properties

| Name            | Type             | Description                                                     |
|-----------------|------------------|-----------------------------------------------------------------|
| `MarginTop`     | `AryNumberValue` | The margin applied to the top side.                             |
| `MarginEnd`     | `AryNumberValue` | The margin applied to the right side (or left in RTL layouts).  |
| `MarginBottom`  | `AryNumberValue` | The margin applied to the bottom side.                          |
| `MarginStart`   | `AryNumberValue` | The margin applied to the left side (or right in RTL layouts).  |
| `PaddingTop`    | `AryNumberValue` | The padding applied to the top side.                            |
| `PaddingEnd`    | `AryNumberValue` | The padding applied to the right side (or left in RTL layouts). |
| `PaddingBottom` | `AryNumberValue` | The padding applied to the bottom side.                         |
| `PaddingStart`  | `AryNumberValue` | The padding applied to the left side (or right in RTL layouts). |

## Methods

| Name            | Signature                                                                                                                                                                                                                                                                                                          | Description                                                                                                                  | Returns      |
|-----------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------|--------------|
| `Cascade`       | `ArySpacing Cascade(AryNumberValue? marginTop = null, AryNumberValue? marginEnd = null, AryNumberValue? marginBottom = null, AryNumberValue? marginStart = null, AryNumberValue? paddingTop = null, AryNumberValue? paddingEnd = null, AryNumberValue? paddingBottom = null, AryNumberValue? paddingStart = null)` | Returns a new `ArySpacing` instance with any specified overrides applied. Unspecified values retain their existing settings. | `ArySpacing` |
| `FromSingle`    | `static ArySpacing FromSingle(AryNumberValue margin, AryNumberValue padding)`                                                                                                                                                                                                                                      | Creates an `ArySpacing` where all sides share the same margin and padding values.                                            | `ArySpacing` |
| `FromSymmetric` | `static ArySpacing FromSymmetric(AryNumberValue marginHorizontal, AryNumberValue marginVertical, AryNumberValue paddingHorizontal, AryNumberValue paddingVertical)`                                                                                                                                                | Creates an `ArySpacing` with symmetric horizontal and vertical margins and paddings.                                         | `ArySpacing` |
| `ToCss`         | `string ToCss(string? varPrefix = "")`                                                                                                                                                                                                                                                                             | Builds a string of CSS declarations for all margin and padding sides using logical properties (`inline`/`block`).            | `string`     |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
// Create a uniform spacing definition
var spacing = ArySpacing.FromSingle(StyleDefaults.Margin, StyleDefaults.Padding);

// Convert to CSS
string css = spacing.ToCss("card");
// Produces:
// --card-margin-top: 1rem;
// --card-padding-top: 0.5rem;
```

---

*Revision Date: 2025-10-17*
