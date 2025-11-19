# Allyaria.Theming.Enumerations.PaletteType

`PaletteType` is an enumeration describing the color palette groups used by the Allyaria theming system. These
categories define structured layers of color used for surfaces, semantic feedback, and component elevation, ensuring
visual consistency across UI themes.

## Summary

`PaletteType` is an enum representing theming palette categories such as primary, secondary, error, elevation layers,
and more. It enables predictable organization of theme colors and provides semantic meaning to each palette group.

## Constructors

*None*

## Properties

*None*

## Methods

*None*

## Fields

| Name         | Type          | Description                                                                |
|--------------|---------------|----------------------------------------------------------------------------|
| `Elevation1` | `PaletteType` | Lowest elevation layer for base surfaces or containers.                    |
| `Elevation2` | `PaletteType` | Second elevation layer, lightly raised above `Elevation1`.                 |
| `Elevation3` | `PaletteType` | Third elevation layer, providing deeper visual separation.                 |
| `Elevation4` | `PaletteType` | Fourth elevation layer, used for higher-level containers.                  |
| `Elevation5` | `PaletteType` | Highest elevation layer, typically for dialogs and overlays.               |
| `Error`      | `PaletteType` | Error palette for critical or validation feedback.                         |
| `Info`       | `PaletteType` | Informational palette for neutral hints or messages.                       |
| `Primary`    | `PaletteType` | Primary palette used for major actions, links, and highlights.             |
| `Success`    | `PaletteType` | Success palette indicating completion or positive actions.                 |
| `Secondary`  | `PaletteType` | Secondary palette for additional accents or supporting elements.           |
| `Surface`    | `PaletteType` | Base surface palette controlling general background and container styling. |
| `Tertiary`   | `PaletteType` | Tertiary palette for subtle accents or decorative elements.                |
| `Warning`    | `PaletteType` | Warning palette for cautionary or attention-required states.               |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Enumerations;

public class PaletteUsageExample
{
    public PaletteType CurrentPalette { get; private set; } = PaletteType.Primary;

    public void SetWarningMode()
    {
        CurrentPalette = PaletteType.Warning;
    }
}
```

---

*Revision Date: 2025-11-15*
