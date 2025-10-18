# Allyaria.Theming.Styles.AryBorders

`AryBorders` is a strongly-typed record struct representing border definitions in the Allyaria theming system. It
supports per-side `width` and `style` (top, right, bottom, left) and per-corner `radius` (top-left, top-right,
bottom-right, bottom-left), providing precise control over border geometry and appearance. Widths and radii use
`AryNumberValue` (e.g., `1px`, `.125rem`, `0`, `50%`), and styles use `AryStringValue` (e.g., `solid`, `dashed`,
`none`).

## Constructors

| Signature                                                                                                                                                                                                                                                                                                                                                                                                                                                      | Description                                                                                                                                 |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------|
| `AryBorders()`                                                                                                                                                                                                                                                                                                                                                                                                                                                 | Initializes a new instance using default width, style, and radius values from `StyleDefaults`.                                              |
| `AryBorders(AryNumberValue? topWidth = null, AryNumberValue? endWidth = null, AryNumberValue? bottomWidth = null, AryNumberValue? startWidth = null, AryStringValue? topStyle = null, AryStringValue? endStyle = null, AryStringValue? bottomStyle = null, AryStringValue? startStyle = null, AryNumberValue? topLeftRadius = null, AryNumberValue? topRightRadius = null, AryNumberValue? bottomRightRadius = null, AryNumberValue? bottomLeftRadius = null)` | Initializes a new instance with optional per-side widths, styles, and per-corner radii. Any `null` parameters fall back to `StyleDefaults`. |

## Properties

| Name                | Type             | Description                                                                                           |
|---------------------|------------------|-------------------------------------------------------------------------------------------------------|
| `BottomLeftRadius`  | `AryNumberValue` | Gets or initializes the `border-bottom-left-radius`.                                                  |
| `BottomRightRadius` | `AryNumberValue` | Gets or initializes the `border-bottom-right-radius`.                                                 |
| `BottomStyle`       | `AryStringValue` | Gets or initializes the `border-bottom-style`.                                                        |
| `BottomWidth`       | `AryNumberValue` | Gets or initializes the `border-bottom-width`.                                                        |
| `EndStyle`          | `AryStringValue` | Gets or initializes the `border-inline-end-style`.                                                    |
| `EndWidth`          | `AryNumberValue` | Gets or initializes the `border-inline-end-width`.                                                    |
| `FocusWidth`        | `AryNumberValue` | Calculates a focus border width based on the largest side width, ensuring a minimum thickness of 2px. |
| `StartStyle`        | `AryStringValue` | Gets or initializes the `border-inline-start-style`.                                                  |
| `StartWidth`        | `AryNumberValue` | Gets or initializes the `border-inline-start-width`.                                                  |
| `TopLeftRadius`     | `AryNumberValue` | Gets or initializes the `border-top-left-radius`.                                                     |
| `TopRightRadius`    | `AryNumberValue` | Gets or initializes the `border-top-right-radius`.                                                    |
| `TopStyle`          | `AryStringValue` | Gets or initializes the `border-top-style`.                                                           |
| `TopWidth`          | `AryNumberValue` | Gets or initializes the `border-top-width`.                                                           |

## Methods

| Name            | Signature                                                                                                                                                                                                                                                                                                                                                                                                                                                              | Description                                                                                                                     | Returns      |
|-----------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------|--------------|
| `Cascade`       | `AryBorders Cascade(AryNumberValue? topWidth = null, AryNumberValue? endWidth = null, AryNumberValue? bottomWidth = null, AryNumberValue? startWidth = null, AryStringValue? topStyle = null, AryStringValue? endStyle = null, AryStringValue? bottomStyle = null, AryStringValue? startStyle = null, AryNumberValue? topLeftRadius = null, AryNumberValue? topRightRadius = null, AryNumberValue? bottomRightRadius = null, AryNumberValue? bottomLeftRadius = null)` | Returns a new `AryBorders` instance by applying provided overrides, keeping existing values where parameters are `null`.        | `AryBorders` |
| `FromSingle`    | `static AryBorders FromSingle(AryNumberValue width, AryStringValue style, AryNumberValue radius)`                                                                                                                                                                                                                                                                                                                                                                      | Creates a uniform `AryBorders` where all sides share the same width, style, and corner radius.                                  | `AryBorders` |
| `FromSymmetric` | `static AryBorders FromSymmetric(AryNumberValue widthHorizontal, AryNumberValue widthVertical, AryStringValue styleHorizontal, AryStringValue styleVertical, AryNumberValue radiusTop, AryNumberValue radiusBottom)`                                                                                                                                                                                                                                                   | Creates a `AryBorders` with symmetric horizontal and vertical widths, styles, and corner radii.                                 | `AryBorders` |
| `ToCss`         | `string ToCss(string? varPrefix = "", bool isFocus = false)`                                                                                                                                                                                                                                                                                                                                                                                                           | Builds CSS declarations for all non-empty border members. Optionally applies focus styling and generates CSS variable prefixes. | `string`     |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
var borders = AryBorders.FromSingle(
    StyleDefaults.BorderWidth,
    StyleDefaults.BorderStyle,
    StyleDefaults.BorderRadius
);

string css = borders.ToCss("btn", isFocus: false);
// Produces CSS variables like:
// --btn-border-top-width: 1px;
// --btn-border-top-style: solid;
// --btn-border-top-left-radius: .25rem;
```

---

*Revision Date: 2025-10-17*
