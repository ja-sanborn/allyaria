# Allyaria.Theming.Styles.Spacing

`Spacing` is a readonly record struct that represents logical spacing (margins and paddings) for a themable element. It
provides strongly typed value members and helper methods to generate CSS declarations. Defaults are automatically
sourced from `StyleDefaults`, and the struct is immutable for safe value semantics.

## Constructors

`Spacing()` Initializes a new instance using default margin and padding values from `StyleDefaults`.

`Spacing(ThemeNumber? marginTop = null, ThemeNumber? marginEnd = null, ThemeNumber? marginBottom = null, ThemeNumber? marginStart = null, ThemeNumber? paddingTop = null, ThemeNumber? paddingEnd = null, ThemeNumber? paddingBottom = null, ThemeNumber? paddingStart = null)`
Initializes a new instance with optionally specified margins and paddings for each side. Any unspecified values default
to `StyleDefaults.Margin` and `StyleDefaults.Padding`.

## Properties

| Name            | Type          | Description                                                     |
|-----------------|---------------|-----------------------------------------------------------------|
| `MarginTop`     | `ThemeNumber` | The margin applied to the top side.                             |
| `MarginEnd`     | `ThemeNumber` | The margin applied to the right side (or left in RTL layouts).  |
| `MarginBottom`  | `ThemeNumber` | The margin applied to the bottom side.                          |
| `MarginStart`   | `ThemeNumber` | The margin applied to the left side (or right in RTL layouts).  |
| `PaddingTop`    | `ThemeNumber` | The padding applied to the top side.                            |
| `PaddingEnd`    | `ThemeNumber` | The padding applied to the right side (or left in RTL layouts). |
| `PaddingBottom` | `ThemeNumber` | The padding applied to the bottom side.                         |
| `PaddingStart`  | `ThemeNumber` | The padding applied to the left side (or right in RTL layouts). |

## Methods

| Name            | Signature                                                                                                                                                                                                                                                                               | Description                                                                                                               | Returns   |
|-----------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------|-----------|
| `Cascade`       | `Spacing Cascade(ThemeNumber? marginTop = null, ThemeNumber? marginEnd = null, ThemeNumber? marginBottom = null, ThemeNumber? marginStart = null, ThemeNumber? paddingTop = null, ThemeNumber? paddingEnd = null, ThemeNumber? paddingBottom = null, ThemeNumber? paddingStart = null)` | Returns a new `Spacing` instance with any specified overrides applied. Unspecified values retain their existing settings. | `Spacing` |
| `FromSingle`    | `static Spacing FromSingle(ThemeNumber margin, ThemeNumber padding)`                                                                                                                                                                                                                    | Creates an `Spacing` where all sides share the same margin and padding values.                                            | `Spacing` |
| `FromSymmetric` | `static Spacing FromSymmetric(ThemeNumber marginHorizontal, ThemeNumber marginVertical, ThemeNumber paddingHorizontal, ThemeNumber paddingVertical)`                                                                                                                                    | Creates an `Spacing` with symmetric horizontal and vertical margins and paddings.                                         | `Spacing` |
| `ToCss`         | `string ToCss(string? varPrefix = "")`                                                                                                                                                                                                                                                  | Builds a string of CSS declarations for all margin and padding sides using logical properties (`inline`/`block`).         | `string`  |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
// Create a uniform spacing definition
var spacing = Spacing.FromSingle(StyleDefaults.Margin, StyleDefaults.Padding);

// Convert to CSS
string css = spacing.ToCss("card");
// Produces:
// --card-margin-top: 1rem;
// --card-padding-top: 0.5rem;
```

---

*Revision Date: 2025-10-17*
