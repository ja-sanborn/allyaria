# Allyaria.Theming.Styles.ArySpacing

`ArySpacing` defines a strongly typed representation of an element’s logical spacing (margins and paddings).
It provides immutable, theme-aware spacing defaults and methods for generating consistent CSS declarations.

## Constructors

`ArySpacing()`
Initializes all margins and paddings to their default values from `StyleDefaults.Margin` and `StyleDefaults.Padding`.

`ArySpacing(AryNumberValue? marginTop = null, AryNumberValue? marginRight = null, AryNumberValue? marginBottom = null, AryNumberValue? marginLeft = null, AryNumberValue? paddingTop = null, AryNumberValue? paddingRight = null, AryNumberValue? paddingBottom = null, AryNumberValue? paddingLeft = null)`
Initializes a new instance with optional per-side values.
Any parameter left as `null` uses its corresponding default from `StyleDefaults`.

* Exceptions: *None*

## Properties

| Name            | Type             | Description                     |
|-----------------|------------------|---------------------------------|
| `MarginTop`     | `AryNumberValue` | The margin on the top side.     |
| `MarginRight`   | `AryNumberValue` | The margin on the right side.   |
| `MarginBottom`  | `AryNumberValue` | The margin on the bottom side.  |
| `MarginLeft`    | `AryNumberValue` | The margin on the left side.    |
| `PaddingTop`    | `AryNumberValue` | The padding on the top side.    |
| `PaddingRight`  | `AryNumberValue` | The padding on the right side.  |
| `PaddingBottom` | `AryNumberValue` | The padding on the bottom side. |
| `PaddingLeft`   | `AryNumberValue` | The padding on the left side.   |

## Methods

| Name                                                                                                                                                                                                                                                                                                      | Returns      | Description                                                                                                                                     |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------|-------------------------------------------------------------------------------------------------------------------------------------------------|
| `Cascade(AryNumberValue? marginTop = null, AryNumberValue? marginRight = null, AryNumberValue? marginBottom = null, AryNumberValue? marginLeft = null, AryNumberValue? paddingTop = null, AryNumberValue? paddingRight = null, AryNumberValue? paddingBottom = null, AryNumberValue? paddingLeft = null)` | `ArySpacing` | Returns a new instance with selectively overridden margins or paddings, preserving unspecified sides.                                           |
| `FromSingle(AryNumberValue margin, AryNumberValue padding)`                                                                                                                                                                                                                                               | `ArySpacing` | Creates uniform spacing with the same margin and padding applied to all sides.                                                                  |
| `FromSymmetric(AryNumberValue marginHorizontal, AryNumberValue marginVertical, AryNumberValue paddingHorizontal, AryNumberValue paddingVertical)`                                                                                                                                                         | `ArySpacing` | Creates symmetric spacing—horizontal values apply to left/right and vertical values apply to top/bottom.                                        |
| `ToCss(string? varPrefix = "")`                                                                                                                                                                                                                                                                           | `string`     | Generates CSS declarations for all margins and paddings. When a prefix is supplied, variable names are emitted as `--{prefix}-[property-name]`. |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* All instances are **immutable** (`readonly record struct`), ensuring safe value semantics.
* Defaults derive from global theme settings (`StyleDefaults.Margin` and `StyleDefaults.Padding`).
* Only non-empty values are serialized into CSS using `StyleHelper.ToCss`.
* Fully supports variable-based CSS outputs, enabling responsive and themable layouts.
* Ideal for constructing consistent component spacing without inline CSS.

## Examples

### Minimal Example

```csharp
var spacing = new ArySpacing();
var css = spacing.ToCss();
// margin-top: 0.5rem; margin-right: 0.5rem; padding-top: 1rem; ...
```

### Expanded Example

```csharp
var customSpacing = ArySpacing.FromSymmetric(
    marginHorizontal: new AryNumberValue("8px"),
    marginVertical: new AryNumberValue("12px"),
    paddingHorizontal: new AryNumberValue("16px"),
    paddingVertical: new AryNumberValue("20px")
);

var adjusted = customSpacing.Cascade(
    paddingBottom: new AryNumberValue("24px")
);

Console.WriteLine(adjusted.ToCss("card"));
// --card-margin-top: 12px;
// --card-margin-right: 8px;
// --card-padding-bottom: 24px;
```

> *Rev Date: 2025-10-06*
