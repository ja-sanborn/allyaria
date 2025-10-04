# Allyaria.Theming.Styles.AllyariaBorders

`AllyariaBorders` is a strongly-typed border descriptor with separate per-side **width**/**style** (
Top/Right/Bottom/Left) and per-corner **radius** (TopLeft/TopRight/BottomRight/BottomLeft). It mirrors the
`AllyariaSpacing` TRBL pattern. Widths and radii use `AllyariaNumberValue`; styles use `AllyariaStringValue`. Color is
intentionally excluded and owned by the palette/theme layer. Unset members are `null` and are omitted from CSS emission.

## Constructors

`AllyariaBorders()` Creates an instance with all members unset (`null`).

* Exceptions: *None*

`AllyariaBorders(AllyariaNumberValue? topWidth = null, AllyariaNumberValue? rightWidth = null, AllyariaNumberValue? bottomWidth = null, AllyariaNumberValue? leftWidth = null, AllyariaStringValue? topStyle = null, AllyariaStringValue? rightStyle = null, AllyariaStringValue? bottomStyle = null, AllyariaStringValue? leftStyle = null, AllyariaNumberValue? topLeftRadius = null, AllyariaNumberValue? topRightRadius = null, AllyariaNumberValue? bottomRightRadius = null, AllyariaNumberValue? bottomLeftRadius = null)`
Creates an instance with optional per-side widths/styles and per-corner radii. Any `null` argument remains unset.

* Exceptions: *None*

## Properties

| Name                | Type                   | Description                                               |
|---------------------|------------------------|-----------------------------------------------------------|
| `TopWidth`          | `AllyariaNumberValue?` | Border top width (e.g., `1px`, `.125rem`, `0`, `50%`).    |
| `RightWidth`        | `AllyariaNumberValue?` | Border right width.                                       |
| `BottomWidth`       | `AllyariaNumberValue?` | Border bottom width.                                      |
| `LeftWidth`         | `AllyariaNumberValue?` | Border left width.                                        |
| `TopStyle`          | `AllyariaStringValue?` | Border top style token (e.g., `solid`, `dashed`, `none`). |
| `RightStyle`        | `AllyariaStringValue?` | Border right style.                                       |
| `BottomStyle`       | `AllyariaStringValue?` | Border bottom style.                                      |
| `LeftStyle`         | `AllyariaStringValue?` | Border left style.                                        |
| `TopLeftRadius`     | `AllyariaNumberValue?` | Top-left border radius.                                   |
| `TopRightRadius`    | `AllyariaNumberValue?` | Top-right border radius.                                  |
| `BottomRightRadius` | `AllyariaNumberValue?` | Bottom-right border radius.                               |
| `BottomLeftRadius`  | `AllyariaNumberValue?` | Bottom-left border radius.                                |

## Methods

| Name                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      | Returns           | Description                                                                                                                                                     |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `static FromSingle(AllyariaNumberValue width, AllyariaStringValue style, AllyariaNumberValue radius)`                                                                                                                                                                                                                                                                                                                                                                                                                     | `AllyariaBorders` | Creates a uniform set: all sides use `width`/`style`; all corners use `radius`.                                                                                 |
| `static FromSymmetric(AllyariaNumberValue widthHorizontal, AllyariaNumberValue widthVertical, AllyariaStringValue styleHorizontal, AllyariaStringValue styleVertical, AllyariaNumberValue radiusTop, AllyariaNumberValue radiusBottom)`                                                                                                                                                                                                                                                                                   | `AllyariaBorders` | Horizontal (L/R) vs Vertical (T/B) widths/styles; top corners use `radiusTop`; bottom corners use `radiusBottom`.                                               |
| `Cascade(AllyariaNumberValue? topWidth = null, AllyariaNumberValue? rightWidth = null, AllyariaNumberValue? bottomWidth = null, AllyariaNumberValue? leftWidth = null, AllyariaStringValue? topStyle = null, AllyariaStringValue? rightStyle = null, AllyariaStringValue? bottomStyle = null, AllyariaStringValue? leftStyle = null, AllyariaNumberValue? topLeftRadius = null, AllyariaNumberValue? topRightRadius = null, AllyariaNumberValue? bottomRightRadius = null, AllyariaNumberValue? bottomLeftRadius = null)` | `AllyariaBorders` | Non-destructively overlays provided members; `null` leaves existing values intact.                                                                              |
| `ToCss()`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 | `string`          | Emits per-member CSS declarations (e.g., `border-top-width:1px;`). Only non-`null` members are emitted.                                                         |
| `ToCssVars(string prefix = "")`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           | `string`          | Emits CSS custom properties. Prefix is normalized (collapse whitespace/dashes, trim, lowercase). Empty prefix → `--aa-`. Example: `--aa-border-top-width:1px;`. |

## Operators

| Operator    | Returns | Description          |
|-------------|---------|----------------------|
| `==` / `!=` | `bool`  | Equality comparison. |

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* **Null omission:** Any `null` member is not included in `ToCss()`/`ToCssVars(...)`.
* **TRBL order:** Members are conceptually Top → Right → Bottom → Left; radii are corner-specific (no shorthands used).
* **Normalization:** `AllyariaNumberValue` and `AllyariaStringValue` control validation/formatting; emitted CSS uses
  their normalized forms.
* **Prefixing:** `ToCssVars(prefix)` normalizes `prefix` and emits variables as `--{prefix}-border-...`; empty prefix
  emits `--aa-...`.
* **No color:** Border color is excluded by design; use palette/theme color tokens on the consumer side.
* **Non-destructive overlay:** `Cascade(...)` returns a new instance, preserving existing non-overridden values.

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

var borders = AllyariaBorders.FromSingle(
    width:  new AllyariaNumberValue("1px"),
    style:  new AllyariaStringValue("solid"),
    radius: new AllyariaNumberValue(".25rem")
);

// Emit direct CSS declarations (only non-null members)
string css = borders.ToCss(); 
// "border-top-width:1px;border-right-width:1px;border-bottom-width:1px;border-left-width:1px;border-top-style:solid;...;border-bottom-left-radius:.25rem;"

// Emit CSS variables with default prefix "--aa-"
string vars = borders.ToCssVars(); 
// "--aa-border-top-width:1px;...--aa-border-bottom-left-radius:.25rem;"
```

### Expanded Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

// Start with symmetric widths/styles, distinct corner radii top vs bottom
var baseBorders = AllyariaBorders.FromSymmetric(
    widthHorizontal: new AllyariaNumberValue("2px"),
    widthVertical:   new AllyariaNumberValue("1px"),
    styleHorizontal: new AllyariaStringValue("solid"),
    styleVertical:   new AllyariaStringValue("dotted"),
    radiusTop:       new AllyariaNumberValue(".5rem"),
    radiusBottom:    new AllyariaNumberValue("4px")
);

// Non-destructively override just top width + bottom-left radius
var finalBorders = baseBorders.Cascade(
    topWidth:        new AllyariaNumberValue("3px"),
    bottomLeftRadius:new AllyariaNumberValue("6px")
);

// Use in a style attribute or stylesheet builder
string decls = finalBorders.ToCss();
// e.g., "border-top-width:3px;border-right-width:2px;...;border-bottom-left-radius:6px;"

string varsPanel = finalBorders.ToCssVars(" Panel  -- Primary ");
// e.g., "--panel-primary-border-top-width:3px;...;--panel-primary-border-bottom-left-radius:6px;"
```

> *Rev Date: 2025-10-03*
