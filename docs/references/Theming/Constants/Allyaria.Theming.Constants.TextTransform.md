# Allyaria.Theming.Constants.TextTransform

`TextTransform` is a static class providing strongly typed constants for CSS text transformation. This type offers
reusable `ThemeString` instances for common `text-transform` values, helping to maintain consistent typography behavior
in Allyaria theming.

## Constructors

*None*

## Properties

| Name         | Type          | Description                                            |
|--------------|---------------|--------------------------------------------------------|
| `Capitalize` | `ThemeString` | Represents capitalizing the first letter of each word. |
| `Lowercase`  | `ThemeString` | Represents transforming all characters to lowercase.   |
| `None`       | `ThemeString` | Represents no text transformation.                     |
| `Uppercase`  | `ThemeString` | Represents transforming all characters to uppercase.   |

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
ThemeString transform = TextTransform.Uppercase; // apply uppercase text transformation
```

---

*Revision Date: 2025-10-17*
