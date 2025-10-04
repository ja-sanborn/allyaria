# Allyaria.Theming.Constants.Sizing

`Sizing` is a static class providing **Material Design–compliant sizing constants** based on a 4px/8px grid.
These values ensure consistency in margins, paddings, and component dimensions across the Allyaria design system.

## Constructors

*None*

## Properties

| Name     | Type                  | Description                                                          |
|----------|-----------------------|----------------------------------------------------------------------|
| `Size0`  | `AllyariaNumberValue` | `0px` — no size.                                                     |
| `Size1`  | `AllyariaNumberValue` | `4px` — micro spacing, used sparingly for fine alignment.            |
| `Size2`  | `AllyariaNumberValue` | `8px` — base spacing unit (1 step on Material grid).                 |
| `Size3`  | `AllyariaNumberValue` | `16px` — default internal padding for many components.               |
| `Size4`  | `AllyariaNumberValue` | `24px` — common margin/gutter size on larger layouts.                |
| `Size5`  | `AllyariaNumberValue` | `32px` — larger spacing for layout separation.                       |
| `Size6`  | `AllyariaNumberValue` | `40px` — large step in the spacing scale.                            |
| `Size7`  | `AllyariaNumberValue` | `48px` — minimum touch target size per accessibility guidance.       |
| `Size8`  | `AllyariaNumberValue` | `56px` — commonly used for component heights (e.g., toolbars).       |
| `Size9`  | `AllyariaNumberValue` | `64px` — extra large spacing step.                                   |
| `Size10` | `AllyariaNumberValue` | `72px` — extra large spacing step.                                   |
| `Size11` | `AllyariaNumberValue` | `80px` — extra large spacing step, upper bound of the default scale. |
| `Thick`  | `AllyariaNumberValue` | `2px` — double pixel spacing, usually used for borders.              |
| `Thin`   | `AllyariaNumberValue` | `1px` — single pixel spacing, usually used for borders.              |

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* Values are immutable instances of `AllyariaNumberValue`, constructed with pixel-based string literals (e.g.,
  `"16px"`).
* Scale follows Material Design’s 4px/8px grid for consistent rhythm.
* `Size7` (`48px`) is aligned with accessibility minimum touch target guidance.

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Constants;

var margin = Sizing.Size3; // 16px
```

### Expanded Example

```csharp
using Allyaria.Theming.Constants;
using Allyaria.Theming.Values;

public class LayoutExample
{
    public AllyariaNumberValue SmallPadding { get; } = Sizing.Size2;  // 8px
    public AllyariaNumberValue DefaultPadding { get; } = Sizing.Size3; // 16px
    public AllyariaNumberValue TouchTarget { get; } = Sizing.Size7; // 48px

    public void ApplyToComponent(MyComponent component)
    {
        component.Padding = DefaultPadding;
        component.MinHeight = TouchTarget; // ensures accessibility compliance
    }
}
```

> *Rev Date: 2025-10-03*
