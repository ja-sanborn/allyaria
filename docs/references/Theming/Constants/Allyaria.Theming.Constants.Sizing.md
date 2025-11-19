# Allyaria.Theming.Constants.Sizing

`Sizing` is a static class providing Material Design–aligned spacing and sizing constants built on a 4px/8px grid. These
values enable consistent application of margins, paddings, sizing tokens, and component dimensions across the Allyaria
theming system.

## Summary

`Sizing` is a static registry of CSS sizing values expressed as strings. It contains percentages, rem-based relative
sizes, and pixel-based Material Design spacing steps, all intended for uniform layout and spacing decisions within UI
components.

## Constructors

*None*

## Properties

| Name             | Type     | Description                                             |
|------------------|----------|---------------------------------------------------------|
| `Full`           | `string` | `"100%"` — full size.                                   |
| `Half`           | `string` | `"50%"` — half size.                                    |
| `Quarter`        | `string` | `"25%"` — quarter size.                                 |
| `Relative`       | `string` | `"1rem"` — same relative size.                          |
| `RelativeLarge1` | `string` | `"1.25rem"` — large relative size.                      |
| `RelativeLarge2` | `string` | `"1.5rem"` — larger relative size.                      |
| `RelativeLarge3` | `string` | `"1.75rem"` — extra large relative size.                |
| `RelativeLarge4` | `string` | `"2rem"` — largest relative size.                       |
| `RelativeSmall1` | `string` | `"0.875rem"` — small relative size.                     |
| `RelativeSmall2` | `string` | `"0.75rem"` — smaller relative size.                    |
| `RelativeSmall3` | `string` | `"0.625rem"` — extra small relative size.               |
| `RelativeSmall4` | `string` | `"0.5rem"` — smallest relative size.                    |
| `Size0`          | `string` | `"0px"` — no size.                                      |
| `Size1`          | `string` | `"4px"` — micro spacing for fine alignment.             |
| `Size10`         | `string` | `"72px"` — extra large spacing step.                    |
| `Size11`         | `string` | `"80px"` — large spacing, upper bound of default scale. |
| `Size2`          | `string` | `"8px"` — base spacing unit (Material 1-step).          |
| `Size3`          | `string` | `"16px"` — default internal padding.                    |
| `Size4`          | `string` | `"24px"` — common margin/gutter for larger layouts.     |
| `Size5`          | `string` | `"32px"` — larger layout spacing.                       |
| `Size6`          | `string` | `"40px"` — large spacing step.                          |
| `Size7`          | `string` | `"48px"` — minimum accessibility touch target.          |
| `Size8`          | `string` | `"56px"` — used for component heights (e.g., toolbars). |
| `Size9`          | `string` | `"64px"` — extra large spacing step.                    |
| `Thick`          | `string` | `"2px"` — double-pixel thickness (borders).             |
| `Thin`           | `string` | `"1px"` — single-pixel thickness (borders).             |
| `ThreeQuarter`   | `string` | `"75%"` — three-quarter size.                           |

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
using Allyaria.Theming.Constants;

public class ExampleComponent
{
    public string Padding => Sizing.Size3;     // "16px"
    public string Width   => Sizing.Half;      // "50%"
    public string Border  => Sizing.Thin;      // "1px"
}
```

---

*Revision Date: 2025-11-15*
