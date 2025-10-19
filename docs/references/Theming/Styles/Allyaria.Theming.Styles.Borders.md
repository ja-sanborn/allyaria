# Allyaria.Theming.Styles.Borders

`Borders` is a strongly-typed record struct representing border definitions in the Allyaria theming system. It supports
per-side `width` and `style` (top, right, bottom, left) and per-corner `radius` (top-left, top-right, bottom-right,
bottom-left), providing precise control over border geometry and appearance. Widths and radii use `ThemeNumber` (e.g.,
`1px`, `.125rem`, `0`, `50%`), and styles use `ThemeString` (e.g., `solid`, `dashed`, `none`).

## Constructors

`Borders()` Initializes a new instance using default width, style, and radius values from `StyleDefaults`.

`Borders(ThemeNumber? topWidth = null, ThemeNumber? endWidth = null, ThemeNumber? bottomWidth = null, ThemeNumber? startWidth = null, ThemeString? topStyle = null, ThemeString? endStyle = null, ThemeString? bottomStyle = null, ThemeString? startStyle = null, ThemeNumber? topLeftRadius = null, ThemeNumber? topRightRadius = null, ThemeNumber? bottomRightRadius = null, ThemeNumber? bottomLeftRadius = null)`
Initializes a new instance with optional per-side widths, styles, and per-corner radii. Any `null` parameters fall back
to `StyleDefaults`.

## Properties

| Name                | Type          | Description                                                                                           |
|---------------------|---------------|-------------------------------------------------------------------------------------------------------|
| `BottomLeftRadius`  | `ThemeNumber` | Gets or initializes the `border-bottom-left-radius`.                                                  |
| `BottomRightRadius` | `ThemeNumber` | Gets or initializes the `border-bottom-right-radius`.                                                 |
| `BottomStyle`       | `ThemeString` | Gets or initializes the `border-bottom-style`.                                                        |
| `BottomWidth`       | `ThemeNumber` | Gets or initializes the `border-bottom-width`.                                                        |
| `EndStyle`          | `ThemeString` | Gets or initializes the `border-inline-end-style`.                                                    |
| `EndWidth`          | `ThemeNumber` | Gets or initializes the `border-inline-end-width`.                                                    |
| `FocusWidth`        | `ThemeNumber` | Calculates a focus border width based on the largest side width, ensuring a minimum thickness of 2px. |
| `StartStyle`        | `ThemeString` | Gets or initializes the `border-inline-start-style`.                                                  |
| `StartWidth`        | `ThemeNumber` | Gets or initializes the `border-inline-start-width`.                                                  |
| `TopLeftRadius`     | `ThemeNumber` | Gets or initializes the `border-top-left-radius`.                                                     |
| `TopRightRadius`    | `ThemeNumber` | Gets or initializes the `border-top-right-radius`.                                                    |
| `TopStyle`          | `ThemeString` | Gets or initializes the `border-top-style`.                                                           |
| `TopWidth`          | `ThemeNumber` | Gets or initializes the `border-top-width`.                                                           |

## Methods

| Name            | Signature                                                                                                                                                                                                                                                                                                                                                                                                                       | Description                                                                                                                     | Returns   |
|-----------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------|-----------|
| `Cascade`       | `Borders Cascade(ThemeNumber? topWidth = null, ThemeNumber? endWidth = null, ThemeNumber? bottomWidth = null, ThemeNumber? startWidth = null, ThemeString? topStyle = null, ThemeString? endStyle = null, ThemeString? bottomStyle = null, ThemeString? startStyle = null, ThemeNumber? topLeftRadius = null, ThemeNumber? topRightRadius = null, ThemeNumber? bottomRightRadius = null, ThemeNumber? bottomLeftRadius = null)` | Returns a new `Borders` instance by applying provided overrides, keeping existing values where parameters are `null`.           | `Borders` |
| `FromSingle`    | `static Borders FromSingle(ThemeNumber width, ThemeString style, ThemeNumber radius)`                                                                                                                                                                                                                                                                                                                                           | Creates a uniform `Borders` where all sides share the same width, style, and corner radius.                                     | `Borders` |
| `FromSymmetric` | `static Borders FromSymmetric(ThemeNumber widthHorizontal, ThemeNumber widthVertical, ThemeString styleHorizontal, ThemeString styleVertical, ThemeNumber radiusTop, ThemeNumber radiusBottom)`                                                                                                                                                                                                                                 | Creates a `Borders` with symmetric horizontal and vertical widths, styles, and corner radii.                                    | `Borders` |
| `ToCss`         | `string ToCss(string? varPrefix = "", bool isFocus = false)`                                                                                                                                                                                                                                                                                                                                                                    | Builds CSS declarations for all non-empty border members. Optionally applies focus styling and generates CSS variable prefixes. | `string`  |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
var borders = Borders.FromSingle(
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
