# Allyaria.Theming.Constants.Sizing

`Sizing` is a static class providing Material Design–compliant sizing constants based on a 4px/8px grid system. This
type offers reusable `AryNumberValue` instances for consistent margins, paddings, and component dimensions, ensuring
visual uniformity across Allyaria themes and layouts.

## Constructors

*None*

## Properties

| Name     | Type             | Description                                                        |
|----------|------------------|--------------------------------------------------------------------|
| `Size0`  | `AryNumberValue` | 0px — no size.                                                     |
| `Size1`  | `AryNumberValue` | 4px — micro spacing, used sparingly for fine alignment.            |
| `Size2`  | `AryNumberValue` | 8px — base spacing unit (1 step on Material grid).                 |
| `Size3`  | `AryNumberValue` | 16px — default internal padding for many components.               |
| `Size4`  | `AryNumberValue` | 24px — common margin/gutter size on larger layouts.                |
| `Size5`  | `AryNumberValue` | 32px — larger spacing for layout separation.                       |
| `Size6`  | `AryNumberValue` | 40px — large step in the spacing scale.                            |
| `Size7`  | `AryNumberValue` | 48px — minimum touch target size per accessibility guidance.       |
| `Size8`  | `AryNumberValue` | 56px — commonly used for component heights (e.g., toolbars).       |
| `Size9`  | `AryNumberValue` | 64px — extra large spacing step.                                   |
| `Size10` | `AryNumberValue` | 72px — extra large spacing step.                                   |
| `Size11` | `AryNumberValue` | 80px — extra large spacing step, upper bound of the default scale. |
| `Thick`  | `AryNumberValue` | 2px — double pixel spacing, usually used for borders.              |
| `Thin`   | `AryNumberValue` | 1px — single pixel spacing, usually used for borders.              |

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
AryNumberValue padding = Sizing.Size3; // apply 16px padding using Allyaria sizing constants
```

---

*Revision Date: 2025-10-17*
