# Allyaria.Theming.Enumerations.LengthUnits

`LengthUnits` is an enumeration defining the supported CSS length units within the Allyaria theming system. Each enum
value corresponds to a physical, relative, or viewport-based measurement used for layout, typography, sizing, and
responsive design.

## Summary

`LengthUnits` is an enum representing a comprehensive set of CSS length measurement units. These values are used
throughout the theming engine to standardize how lengths are stored, converted, serialized, and applied to generated CSS
variables and style rules.

## Constructors

*None*

## Properties

*None*

## Methods

*None*

## Fields

| Name                | Type          | Description                                                                |
|---------------------|---------------|----------------------------------------------------------------------------|
| `Character`         | `LengthUnits` | Character width unit (`ch`), based on the width of the `"0"` glyph.        |
| `Centimeter`        | `LengthUnits` | Centimeters (`cm`).                                                        |
| `ContainerBlock`    | `LengthUnits` | Container block size (`cqb`), relative to the container’s block dimension. |
| `ContainerHeight`   | `LengthUnits` | Container height (`cqh`), relative to the container’s block size.          |
| `ContainerInline`   | `LengthUnits` | Container inline size (`cqi`).                                             |
| `ContainerWidth`    | `LengthUnits` | Container width (`cqw`).                                                   |
| `ContainerMax`      | `LengthUnits` | Maximum container size (`cqmax`).                                          |
| `ContainerMin`      | `LengthUnits` | Minimum container size (`cqmin`).                                          |
| `Em`                | `LengthUnits` | Em size (`em`), relative to the element’s font size.                       |
| `Ex`                | `LengthUnits` | Ex size (`ex`), relative to a font’s x-height.                             |
| `Fraction`          | `LengthUnits` | Fraction unit (`fr`), used in CSS grid layouts.                            |
| `Inch`              | `LengthUnits` | Inches (`in`).                                                             |
| `LineHeight`        | `LengthUnits` | Line height unit (`lh`) of the element.                                    |
| `Millimeter`        | `LengthUnits` | Millimeters (`mm`).                                                        |
| `Pica`              | `LengthUnits` | Picas (`pc`).                                                              |
| `Point`             | `LengthUnits` | Points (`pt`).                                                             |
| `Percent`           | `LengthUnits` | Percentage (`%`), relative to the parent element or context.               |
| `Pixel`             | `LengthUnits` | Pixels (`px`).                                                             |
| `QuarterMillimeter` | `LengthUnits` | Quarter-millimeters (`Q`).                                                 |
| `RootEm`            | `LengthUnits` | Root em (`rem`), relative to the root element’s font size.                 |
| `RootLineHeight`    | `LengthUnits` | Root line height (`rlh`).                                                  |
| `ViewportBlock`     | `LengthUnits` | Viewport block size (`vb`).                                                |
| `ViewportHeight`    | `LengthUnits` | Viewport height (`vh`), equal to 1% of the viewport’s height.              |
| `ViewportInline`    | `LengthUnits` | Viewport inline size (`vi`).                                               |
| `ViewportMax`       | `LengthUnits` | Larger of viewport width or height (`vmax`).                               |
| `ViewportMin`       | `LengthUnits` | Smaller of viewport width or height (`vmin`).                              |
| `ViewportWidth`     | `LengthUnits` | Viewport width (`vw`), equal to 1% of the viewport’s width.                |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Enumerations;

public class SpacingRule
{
    public double Value { get; set; }
    public LengthUnits Unit { get; set; } = LengthUnits.Pixel;

    public override string ToString()
        => $"{Value}{GetCssUnit(Unit)}";

    private static string GetCssUnit(LengthUnits unit)
        => unit switch
        {
            LengthUnits.Character => "ch",
            LengthUnits.Em => "em",
            LengthUnits.Rem => "rem",
            LengthUnits.Percent => "%",
            LengthUnits.Pixel => "px",
            _ => "px"
        };
}
```

---

*Revision Date: 2025-11-15*
