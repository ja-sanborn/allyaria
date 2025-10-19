# Allyaria.Theming.Constants.FontWeight

`FontWeight` is a static class of strongly typed constants representing standard CSS font weight values. This type
provides reusable `ThemeString` instances for common CSS `font-weight` settings (e.g., `normal`, `bold`, numeric values,
and relative weights) to promote consistency and readability in Allyaria theming.

## Constructors

*None*

## Properties

| Name      | Type          | Description                                                      |
|-----------|---------------|------------------------------------------------------------------|
| `Bold`    | `ThemeString` | Represents a bold font weight.                                   |
| `Bold1`   | `ThemeString` | Represents a font weight of 100.                                 |
| `Bold2`   | `ThemeString` | Represents a font weight of 200.                                 |
| `Bold3`   | `ThemeString` | Represents a font weight of 300.                                 |
| `Bold4`   | `ThemeString` | Represents a font weight of 400.                                 |
| `Bold5`   | `ThemeString` | Represents a font weight of 500.                                 |
| `Bold6`   | `ThemeString` | Represents a font weight of 600.                                 |
| `Bold7`   | `ThemeString` | Represents a font weight of 700.                                 |
| `Bold8`   | `ThemeString` | Represents a font weight of 800.                                 |
| `Bold9`   | `ThemeString` | Represents a font weight of 900.                                 |
| `Bolder`  | `ThemeString` | Represents a bolder font weight relative to the parent element.  |
| `Lighter` | `ThemeString` | Represents a lighter font weight relative to the parent element. |
| `Normal`  | `ThemeString` | Represents a normal font weight.                                 |

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
ThemeString weight = FontWeight.Bold7; // use a strongly-typed CSS font weight
```

---

*Revision Date: 2025-10-17*
