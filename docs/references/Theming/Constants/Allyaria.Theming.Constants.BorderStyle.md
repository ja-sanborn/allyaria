# Allyaria.Theming.Constants.BorderStyle

`BorderStyle` is a static class of strongly typed constants representing standard CSS border styles. This type provides
reusable `ThemeString` instances for common CSS `border-style` values (e.g., `solid`, `dashed`, `none`) to improve
discoverability and reduce typos when composing themes or style objects in code.

## Constructors

*None*

## Properties

| Name     | Type          | Description                            |
|----------|---------------|----------------------------------------|
| `Dashed` | `ThemeString` | Represents a dashed border style.      |
| `Dotted` | `ThemeString` | Represents a dotted border style.      |
| `Double` | `ThemeString` | Represents a double line border style. |
| `Groove` | `ThemeString` | Represents a groove border style.      |
| `Hidden` | `ThemeString` | Represents a hidden border style.      |
| `Inset`  | `ThemeString` | Represents an inset border style.      |
| `None`   | `ThemeString` | Represents no border.                  |
| `Outset` | `ThemeString` | Represents an outset border style.     |
| `Ridge`  | `ThemeString` | Represents a ridge border style.       |
| `Solid`  | `ThemeString` | Represents a solid border style.       |

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
ThemeString style = BorderStyle.Solid; // use a strongly-typed CSS border style
```

---

*Revision Date: 2025-10-17*
