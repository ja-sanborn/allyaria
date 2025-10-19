# Allyaria.Theming.Constants.TextDecorationLine

`TextDecorationLine` is a static class providing strongly typed constants representing CSS text decoration line values.
It exposes predefined `ThemeString` instances for common text decoration options such as underline, overline,
line-through, and none—helping ensure consistent, error-free styling in Allyaria themes.

## Constructors

*None*

## Properties

| Name          | Type          | Description                                |
|---------------|---------------|--------------------------------------------|
| `LineThrough` | `ThemeString` | Represents a line-through text decoration. |
| `None`        | `ThemeString` | Represents no text decoration line.        |
| `Overline`    | `ThemeString` | Represents an overline text decoration.    |
| `Underline`   | `ThemeString` | Represents an underline text decoration.   |

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
ThemeString decoration = TextDecorationLine.Underline; // apply an underline decoration
```

---

*Revision Date: 2025-10-17*
