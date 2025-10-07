# Allyaria.Theming.Styles.AryBorders

`AryBorders` defines a strongly typed structure for representing CSS border definitions within Allyaria theming.
It supports per-side **widths** and **styles** (top/right/bottom/left) and per-corner **radii** (
top-left/top-right/bottom-right/bottom-left).
This struct ensures type safety, composability, and efficient CSS generation for theme-driven borders.

## Constructors

`AryBorders()`
Initializes all border widths, styles, and radii using `StyleDefaults` values.
*(Equivalent to calling `new AryBorders(null)`.)*

`AryBorders(AryNumberValue? topWidth = null, AryNumberValue? rightWidth = null, AryNumberValue? bottomWidth = null, AryNumberValue? leftWidth = null, AryStringValue? topStyle = null, AryStringValue? rightStyle = null, AryStringValue? bottomStyle = null, AryStringValue? leftStyle = null, AryNumberValue? topLeftRadius = null, AryNumberValue? topRightRadius = null, AryNumberValue? bottomRightRadius = null, AryNumberValue? bottomLeftRadius = null)`
Initializes a fully customizable border definition.
Each argument may be left `null` to fall back to `StyleDefaults`.

* Exceptions: *None*

## Properties

| Name                | Type             | Description                                         |
|---------------------|------------------|-----------------------------------------------------|
| `TopWidth`          | `AryNumberValue` | Gets or initializes the top border width.           |
| `RightWidth`        | `AryNumberValue` | Gets or initializes the right border width.         |
| `BottomWidth`       | `AryNumberValue` | Gets or initializes the bottom border width.        |
| `LeftWidth`         | `AryNumberValue` | Gets or initializes the left border width.          |
| `TopStyle`          | `AryStringValue` | Gets or initializes the top border style.           |
| `RightStyle`        | `AryStringValue` | Gets or initializes the right border style.         |
| `BottomStyle`       | `AryStringValue` | Gets or initializes the bottom border style.        |
| `LeftStyle`         | `AryStringValue` | Gets or initializes the left border style.          |
| `TopLeftRadius`     | `AryNumberValue` | Gets or initializes the top-left corner radius.     |
| `TopRightRadius`    | `AryNumberValue` | Gets or initializes the top-right corner radius.    |
| `BottomRightRadius` | `AryNumberValue` | Gets or initializes the bottom-right corner radius. |
| `BottomLeftRadius`  | `AryNumberValue` | Gets or initializes the bottom-left corner radius.  |

## Methods

| Name                                                                                                                                                                                                                                                                                                                                                                                                                                                          | Returns      | Description                                                                                                                                                       |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Cascade(AryNumberValue? topWidth = null, AryNumberValue? rightWidth = null, AryNumberValue? bottomWidth = null, AryNumberValue? leftWidth = null, AryStringValue? topStyle = null, AryStringValue? rightStyle = null, AryStringValue? bottomStyle = null, AryStringValue? leftStyle = null, AryNumberValue? topLeftRadius = null, AryNumberValue? topRightRadius = null, AryNumberValue? bottomRightRadius = null, AryNumberValue? bottomLeftRadius = null)` | `AryBorders` | Returns a new instance with selectively overridden per-side widths, styles, or corner radii. Preserves existing values for all unspecified parameters.            |
| `FromSingle(AryNumberValue width, AryStringValue style, AryNumberValue radius)`                                                                                                                                                                                                                                                                                                                                                                               | `AryBorders` | Creates a uniform border definition with all sides sharing the same width, style, and radius.                                                                     |
| `FromSymmetric(AryNumberValue widthHorizontal, AryNumberValue widthVertical, AryStringValue styleHorizontal, AryStringValue styleVertical, AryNumberValue radiusTop, AryNumberValue radiusBottom)`                                                                                                                                                                                                                                                            | `AryBorders` | Creates a border with symmetric horizontal/vertical widths and radii.                                                                                             |
| `ToCss(string? varPrefix = "")`                                                                                                                                                                                                                                                                                                                                                                                                                               | `string`     | Generates a CSS declaration string for all non-empty members. When `varPrefix` is provided, CSS custom properties are emitted using `--{prefix}-[property-name]`. |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* This struct **excludes color information**; border color is derived from the theme palette.
* Widths and radii use `AryNumberValue` (e.g., `1px`, `.25rem`), while styles use `AryStringValue` (e.g., `solid`,
  `none`).
* Only non-empty values are emitted to CSS (via `StyleHelper.ToCss`).
* Follows Allyaria’s immutability model — all operations (`Cascade`, `FromSingle`, `FromSymmetric`) return new
  instances.
* Designed to support variable-based CSS generation for responsive and theme-driven designs.

## Examples

### Minimal Example

```csharp
var border = new AryBorders();
var css = border.ToCss();
// Uses StyleDefaults (solid 0px border, size2 radius)
```

### Expanded Example

```csharp
var border = AryBorders.FromSingle(
    new AryNumberValue("1px"),
    BorderStyle.Solid,
    new AryNumberValue(".25rem")
);

var hovered = border.Cascade(
    topStyle: BorderStyle.Dashed,
    bottomWidth: new AryNumberValue("2px")
);

Console.WriteLine(hovered.ToCss("btn"));
// Outputs CSS vars like:
// --btn-border-top-style: dashed;
// --btn-border-bottom-width: 2px;
```

> *Rev Date: 2025-10-06*
