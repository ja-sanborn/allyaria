# Allyaria.Theming.Enumerations.LengthUnits

`LengthUnits` is an enumeration that defines CSS-compatible units of length measurement for styling and layout. It
enables consistent expression of size, spacing, and dimension values across theming and rendering components.

## Constructors

*None*

## Properties

| Name                | Type          | Description                                                                |
|---------------------|---------------|----------------------------------------------------------------------------|
| `Character`         | `LengthUnits` | Character width unit (`ch`), based on the width of the "0" glyph.          |
| `Centimeter`        | `LengthUnits` | Centimeters (`cm`).                                                        |
| `ContainerBlock`    | `LengthUnits` | Container block size (`cqb`), relative to the container’s block dimension. |
| `ContainerHeight`   | `LengthUnits` | Container height (`cqh`), relative to the container’s height (block size). |
| `ContainerInline`   | `LengthUnits` | Container inline size (`cqi`).                                             |
| `ContainerWidth`    | `LengthUnits` | Container width (`cqw`).                                                   |
| `ContainerMax`      | `LengthUnits` | Maximum container size (`cqmax`).                                          |
| `ContainerMin`      | `LengthUnits` | Minimum container size (`cqmin`).                                          |
| `Em`                | `LengthUnits` | Em size (`em`), relative to the font size of the element.                  |
| `Ex`                | `LengthUnits` | Ex size (`ex`), relative to the x-height of the font.                      |
| `Fraction`          | `LengthUnits` | Fractional unit (`fr`), used in grid layouts.                              |
| `Inch`              | `LengthUnits` | Inches (`in`).                                                             |
| `LineHeight`        | `LengthUnits` | Line height (`lh`) of the element.                                         |
| `Millimeter`        | `LengthUnits` | Millimeters (`mm`).                                                        |
| `Pica`              | `LengthUnits` | Picas (`pc`).                                                              |
| `Point`             | `LengthUnits` | Points (`pt`).                                                             |
| `Percent`           | `LengthUnits` | Percentage (`%`), relative to the parent element or context.               |
| `Pixel`             | `LengthUnits` | Pixels (`px`).                                                             |
| `QuarterMillimeter` | `LengthUnits` | Quarter-millimeters (`Q`).                                                 |
| `RootEm`            | `LengthUnits` | Root em (`rem`), relative to the root element’s font size.                 |
| `RootLineHeight`    | `LengthUnits` | Root line height (`rlh`), relative to the root element’s line height.      |
| `ViewportBlock`     | `LengthUnits` | Viewport block size (`vb`).                                                |
| `ViewportHeight`    | `LengthUnits` | Viewport height (`vh`), relative to 1% of the viewport height.             |
| `ViewportInline`    | `LengthUnits` | Viewport inline size (`vi`).                                               |
| `ViewportMax`       | `LengthUnits` | Larger of the viewport height or width (`vmax`).                           |
| `ViewportMin`       | `LengthUnits` | Smaller of the viewport height or width (`vmin`).                          |
| `ViewportWidth`     | `LengthUnits` | Viewport width (`vw`), relative to 1% of the viewport width.               |

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
LengthUnits unit = LengthUnits.Pixel;
```

---

*Revision Date: 2025-10-17*
