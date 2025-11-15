# Allyaria.Theming.StyleTypes.StyleTextDecorationLine

`StyleTextDecorationLine` is a sealed style value record representing the CSS `text-decoration-line` property within the
Allyaria theming system. It inherits from `StyleValueBase` (implementing `IStyleValue`) and provides a strongly typed
wrapper around all valid combinations of CSS text-decoration line keywords.

## Summary

`StyleTextDecorationLine` is an immutable, validated style value used to specify line decorations applied to text. Its
nested `Kind` enum exposes every supported decoration combination (including compound values such as
`"overline line-through"`). The type uses `[Description]` attributes to map these enum values to their canonical CSS
string forms, validates the value through `StyleValueBase`, and supports parsing, flexible conversion, and implicit
operators.

## Constructors

`StyleTextDecorationLine(Kind kind)` Creates a new instance using the specified decoration-line combination. The enum
value is mapped to its CSS keyword string via `[Description]` and validated through the `StyleValueBase` constructor.

## Properties

| Name    | Type     | Description                                                                                                  |
|---------|----------|--------------------------------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `text-decoration-line` string (e.g., `"underline"`, `"overline line-through underline"`). |

## Methods

| Name                                                           | Returns                   | Description                                                                                    |
|----------------------------------------------------------------|---------------------------|------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                         | `StyleTextDecorationLine` | Parses a CSS `text-decoration-line` keyword string; throws if invalid.                         |
| `TryParse(string? value, out StyleTextDecorationLine? result)` | `bool`                    | Attempts to parse without throwing; returns `true` on success and assigns the output instance. |
| `Equals(object? obj)`                                          | `bool`                    | Structural equality check.                                                                     |
| `Equals(StyleTextDecorationLine? other)`                       | `bool`                    | Structural equality with another instance.                                                     |
| `GetHashCode()`                                                | `int`                     | Computes a structural hash code.                                                               |

## Nested Types

| Name   | Type   | Description                                                               |
|--------|--------|---------------------------------------------------------------------------|
| `Kind` | `enum` | Defines all supported CSS `text-decoration-line` decoration combinations. |

### `StyleTextDecorationLine.Kind` Members

| Name                   | Description                                                                     |
|------------------------|---------------------------------------------------------------------------------|
| `All`                  | `"overline line-through underline"` — Applies all three major text decorations. |
| `LineThrough`          | `"line-through"` — A line through the middle of text.                           |
| `None`                 | `"none"` — Removes all text decoration.                                         |
| `Overline`             | `"overline"` — A line above the text.                                           |
| `OverlineLineThrough`  | `"overline line-through"` — An overline and a line-through.                     |
| `OverlineUnderline`    | `"overline underline"` — An overline and an underline.                          |
| `Underline`            | `"underline"` — A line beneath the text.                                        |
| `UnderlineLineThrough` | `"underline line-through"` — An underline and a line-through.                   |

## Operators

| Operator                                                                     | Returns                   | Description                                                          |
|------------------------------------------------------------------------------|---------------------------|----------------------------------------------------------------------|
| `implicit operator StyleTextDecorationLine(string? value)`                   | `StyleTextDecorationLine` | Parses the string into a strongly typed value.                       |
| `implicit operator string(StyleTextDecorationLine? value)`                   | `string`                  | Converts the instance to its CSS keyword string (or `""` if `null`). |
| `operator ==(StyleTextDecorationLine? left, StyleTextDecorationLine? right)` | `bool`                    | Structural equality comparison.                                      |
| `operator !=(StyleTextDecorationLine? left, StyleTextDecorationLine? right)` | `bool`                    | Structural inequality.                                               |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` (and therefore by implicit string conversion) when the provided string does not correspond to a
  valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the mapped CSS keyword contains forbidden characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class TextDecorationLineDemo
{
    public void Demo()
    {
        // Create from enum
        var underline = new StyleTextDecorationLine(StyleTextDecorationLine.Kind.Underline);
        string cssUnderline = underline.Value;     // "underline"

        // Parse compound value
        var multi = StyleTextDecorationLine.Parse("overline line-through");
        string cssMulti = multi;                   // "overline line-through"

        // TryParse
        if (StyleTextDecorationLine.TryParse("none", out var none))
        {
            string cssNone = none!.Value;          // "none"
        }

        // Implicit from string
        StyleTextDecorationLine implicitValue = "underline line-through";
        string cssImplicit = implicitValue;        // "underline line-through"
    }
}
```

---

*Revision Date: 2025-11-15*
