# Allyaria.Theming.Constants.TextAlign

`TextAlign` is a static class providing strongly typed constants for CSS text alignment. This type defines reusable
`ThemeString` instances corresponding to standard `text-align` values, improving clarity and consistency in Allyaria
theming and style definitions.

## Constructors

*None*

## Properties

| Name      | Type          | Description                                                                                        |
|-----------|---------------|----------------------------------------------------------------------------------------------------|
| `Center`  | `ThemeString` | Represents center-aligned text.                                                                    |
| `End`     | `ThemeString` | Represents alignment at the end of the inline direction (depends on writing mode and direction).   |
| `Justify` | `ThemeString` | Represents justified text alignment.                                                               |
| `Left`    | `ThemeString` | Represents left-aligned text.                                                                      |
| `Match`   | `ThemeString` | Represents alignment inherited from the parent element (`match-parent`).                           |
| `Right`   | `ThemeString` | Represents right-aligned text.                                                                     |
| `Start`   | `ThemeString` | Represents alignment at the start of the inline direction (depends on writing mode and direction). |

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
ThemeString alignment = TextAlign.Center; // apply center-aligned text styling
```

---

*Revision Date: 2025-10-17*
