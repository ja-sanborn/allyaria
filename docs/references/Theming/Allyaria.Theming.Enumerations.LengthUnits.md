# Allyaria.Theming.Enumerations.LengthUnits

`LengthUnits` defines the full set of CSS-compatible length measurement units supported by Allyaria theming and layout
systems.
Each enum value maps to a corresponding CSS unit string via the `[Description]` attribute, ensuring consistent
serialization and interop.

## Constructors

*Enum — no constructors.*

## Properties

*None*

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* Each unit is associated with a `[Description]` attribute containing its canonical CSS abbreviation.
* These units can be used in theme definitions, layout computations, and style generation for responsive and
  container-based design.
* The enum includes both **absolute units** (e.g., `cm`, `in`, `pt`) and **relative units** (e.g., `em`, `rem`, `vw`,
  `cqw`).
* Allyaria’s theming API uses `EnumExtensions.GetDescription()` to resolve the proper string representation when
  generating CSS.

## Members

| Name                | Description                                                                |
|---------------------|----------------------------------------------------------------------------|
| `Character`         | Character width unit (`ch`), based on the width of the “0” glyph.          |
| `Centimeter`        | Centimeters (`cm`).                                                        |
| `ContainerBlock`    | Container block size (`cqb`), relative to the container’s block dimension. |
| `ContainerHeight`   | Container height (`cqh`), relative to container’s height (block size).     |
| `ContainerInline`   | Container inline size (`cqi`).                                             |
| `ContainerWidth`    | Container width (`cqw`).                                                   |
| `ContainerMax`      | Maximum container size (`cqmax`).                                          |
| `ContainerMin`      | Minimum container size (`cqmin`).                                          |
| `Em`                | Em size (`em`), relative to the font size of the element.                  |
| `Ex`                | Ex size (`ex`), relative to the x-height of the font.                      |
| `Fraction`          | Fractional unit (`fr`), used in grid layouts.                              |
| `Inch`              | Inches (`in`).                                                             |
| `LineHeight`        | Line height (`lh`) of the element.                                         |
| `Millimeter`        | Millimeters (`mm`).                                                        |
| `Pica`              | Picas (`pc`).                                                              |
| `Point`             | Points (`pt`).                                                             |
| `Percent`           | Percentage (`%`), relative to parent element or context.                   |
| `Pixel`             | Pixels (`px`).                                                             |
| `QuarterMillimeter` | Quarter-millimeters (`Q`).                                                 |
| `RootEm`            | Root em (`rem`), relative to the root element’s font size.                 |
| `RootLineHeight`    | Root line height (`rlh`), relative to the root element’s line height.      |
| `ViewportBlock`     | Viewport block size (`vb`).                                                |
| `ViewportHeight`    | Viewport height (`vh`), 1% of the viewport height.                         |
| `ViewportInline`    | Viewport inline size (`vi`).                                               |
| `ViewportMax`       | Larger of viewport height or width (`vmax`).                               |
| `ViewportMin`       | Smaller of viewport height or width (`vmin`).                              |
| `ViewportWidth`     | Viewport width (`vw`), 1% of the viewport width.                           |

## Examples

### Minimal Example

```csharp
var unit = LengthUnits.Rem;
var css = unit.GetDescription(); // "rem"
```

### Expanded Example

```csharp
public string FormatCssLength(double value, LengthUnits unit)
{
    return $"{value.ToString(CultureInfo.InvariantCulture)}{unit.GetDescription()}";
}

// Example usage:
var result = FormatCssLength(2.5, LengthUnits.Em); // "2.5em"
```

> *Rev Date: 2025-10-06*
