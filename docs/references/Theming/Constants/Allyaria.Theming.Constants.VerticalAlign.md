# Allyaria.Theming.Constants.VerticalAlign

`VerticalAlign` is a static class providing strongly typed constants for CSS vertical alignment. This type defines
reusable `AryStringValue` instances representing common `vertical-align` values, enabling consistent text and element
alignment behavior in Allyaria theming.

## Constructors

*None*

## Properties

| Name         | Type             | Description                                                     |
|--------------|------------------|-----------------------------------------------------------------|
| `Baseline`   | `AryStringValue` | Represents baseline vertical alignment (default).               |
| `Bottom`     | `AryStringValue` | Represents aligning to the bottom of the element.               |
| `Middle`     | `AryStringValue` | Represents aligning to the middle of the element.               |
| `Sub`        | `AryStringValue` | Represents subscript vertical alignment.                        |
| `Super`      | `AryStringValue` | Represents superscript vertical alignment.                      |
| `TextBottom` | `AryStringValue` | Represents aligning to the bottom of the parent element’s font. |
| `TextTop`    | `AryStringValue` | Represents aligning to the top of the parent element’s font.    |
| `Top`        | `AryStringValue` | Represents aligning to the top of the element.                  |

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
AryStringValue alignment = VerticalAlign.Middle; // vertically center an element
```

---

*Revision Date: 2025-10-17*
