# Allyaria.Theming.Constants.TextTransform

`TextTransform` is a static class providing strongly typed constants for CSS text transformation. This type offers
reusable `AryStringValue` instances for common `text-transform` values, helping to maintain consistent typography
behavior in Allyaria theming.

## Constructors

*None*

## Properties

| Name         | Type             | Description                                            |
|--------------|------------------|--------------------------------------------------------|
| `Capitalize` | `AryStringValue` | Represents capitalizing the first letter of each word. |
| `Lowercase`  | `AryStringValue` | Represents transforming all characters to lowercase.   |
| `None`       | `AryStringValue` | Represents no text transformation.                     |
| `Uppercase`  | `AryStringValue` | Represents transforming all characters to uppercase.   |

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
AryStringValue transform = TextTransform.Uppercase; // apply uppercase text transformation
```

---

*Revision Date: 2025-10-17*
