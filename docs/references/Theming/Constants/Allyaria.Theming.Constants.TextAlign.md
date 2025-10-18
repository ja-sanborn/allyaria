# Allyaria.Theming.Constants.TextAlign

`TextAlign` is a static class providing strongly typed constants for CSS text alignment. This type defines reusable
`AryStringValue` instances corresponding to standard `text-align` values, improving clarity and consistency in Allyaria
theming and style definitions.

## Constructors

*None*

## Properties

| Name      | Type             | Description                                                                                        |
|-----------|------------------|----------------------------------------------------------------------------------------------------|
| `Center`  | `AryStringValue` | Represents center-aligned text.                                                                    |
| `End`     | `AryStringValue` | Represents alignment at the end of the inline direction (depends on writing mode and direction).   |
| `Justify` | `AryStringValue` | Represents justified text alignment.                                                               |
| `Left`    | `AryStringValue` | Represents left-aligned text.                                                                      |
| `Match`   | `AryStringValue` | Represents alignment inherited from the parent element (`match-parent`).                           |
| `Right`   | `AryStringValue` | Represents right-aligned text.                                                                     |
| `Start`   | `AryStringValue` | Represents alignment at the start of the inline direction (depends on writing mode and direction). |

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
AryStringValue alignment = TextAlign.Center; // apply center-aligned text styling
```

---

*Revision Date: 2025-10-17*
