# Allyaria.Theming.Constants.BorderStyle

`BorderStyle` is a static class of strongly typed constants representing standard CSS border styles. This type provides
reusable `AryStringValue` instances for common CSS `border-style` values (e.g., `solid`, `dashed`, `none`) to improve
discoverability and reduce typos when composing themes or style objects in code.

## Constructors

*None*

## Properties

| Name     | Type             | Description                            |
|----------|------------------|----------------------------------------|
| `Dashed` | `AryStringValue` | Represents a dashed border style.      |
| `Dotted` | `AryStringValue` | Represents a dotted border style.      |
| `Double` | `AryStringValue` | Represents a double line border style. |
| `Groove` | `AryStringValue` | Represents a groove border style.      |
| `Hidden` | `AryStringValue` | Represents a hidden border style.      |
| `Inset`  | `AryStringValue` | Represents an inset border style.      |
| `None`   | `AryStringValue` | Represents no border.                  |
| `Outset` | `AryStringValue` | Represents an outset border style.     |
| `Ridge`  | `AryStringValue` | Represents a ridge border style.       |
| `Solid`  | `AryStringValue` | Represents a solid border style.       |

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
AryStringValue style = BorderStyle.Solid; // use a strongly-typed CSS border style
```

---

*Revision Date: 2025-10-17*
