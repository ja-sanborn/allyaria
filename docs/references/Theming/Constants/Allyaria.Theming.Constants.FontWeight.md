# Allyaria.Theming.Constants.FontWeight

`FontWeight` is a static class of strongly typed constants representing standard CSS font weight values. This type
provides reusable `AryStringValue` instances for common CSS `font-weight` settings (e.g., `normal`, `bold`, numeric
values, and relative weights) to promote consistency and readability in Allyaria theming.

## Constructors

*None*

## Properties

| Name      | Type             | Description                                                      |
|-----------|------------------|------------------------------------------------------------------|
| `Bold`    | `AryStringValue` | Represents a bold font weight.                                   |
| `Bold1`   | `AryStringValue` | Represents a font weight of 100.                                 |
| `Bold2`   | `AryStringValue` | Represents a font weight of 200.                                 |
| `Bold3`   | `AryStringValue` | Represents a font weight of 300.                                 |
| `Bold4`   | `AryStringValue` | Represents a font weight of 400.                                 |
| `Bold5`   | `AryStringValue` | Represents a font weight of 500.                                 |
| `Bold6`   | `AryStringValue` | Represents a font weight of 600.                                 |
| `Bold7`   | `AryStringValue` | Represents a font weight of 700.                                 |
| `Bold8`   | `AryStringValue` | Represents a font weight of 800.                                 |
| `Bold9`   | `AryStringValue` | Represents a font weight of 900.                                 |
| `Bolder`  | `AryStringValue` | Represents a bolder font weight relative to the parent element.  |
| `Lighter` | `AryStringValue` | Represents a lighter font weight relative to the parent element. |
| `Normal`  | `AryStringValue` | Represents a normal font weight.                                 |

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
AryStringValue weight = FontWeight.Bold7; // use a strongly-typed CSS font weight
```

---

*Revision Date: 2025-10-17*
