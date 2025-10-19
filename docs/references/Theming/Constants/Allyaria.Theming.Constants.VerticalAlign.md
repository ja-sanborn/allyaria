# Allyaria.Theming.Constants.VerticalAlign

`VerticalAlign` is a static class providing strongly typed constants for CSS vertical alignment. This type defines
reusable `ThemeString` instances representing common `vertical-align` values, enabling consistent text and element
alignment behavior in Allyaria theming.

## Constructors

*None*

## Properties

| Name         | Type          | Description                                                     |
|--------------|---------------|-----------------------------------------------------------------|
| `Baseline`   | `ThemeString` | Represents baseline vertical alignment (default).               |
| `Bottom`     | `ThemeString` | Represents aligning to the bottom of the element.               |
| `Middle`     | `ThemeString` | Represents aligning to the middle of the element.               |
| `Sub`        | `ThemeString` | Represents subscript vertical alignment.                        |
| `Super`      | `ThemeString` | Represents superscript vertical alignment.                      |
| `TextBottom` | `ThemeString` | Represents aligning to the bottom of the parent element’s font. |
| `TextTop`    | `ThemeString` | Represents aligning to the top of the parent element’s font.    |
| `Top`        | `ThemeString` | Represents aligning to the top of the element.                  |

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
ThemeString alignment = VerticalAlign.Middle; // vertically center an element
```

---

*Revision Date: 2025-10-17*
