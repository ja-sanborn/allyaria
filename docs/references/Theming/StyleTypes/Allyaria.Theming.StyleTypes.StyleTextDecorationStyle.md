# Allyaria.Theming.StyleTypes.StyleTextDecorationStyle

`StyleTextDecorationStyle` is a sealed style value record representing the CSS `text-decoration-style` property within
the Allyaria theming system. It inherits from `StyleValueBase` (and thereby implements `IStyleValue`) and provides a
strongly typed wrapper around the visual style of text decoration lines.

## Summary

`StyleTextDecorationStyle` is an immutable, validated CSS style value used to control the visual stroke style applied to
text decorations such as underlines, overlines, or line-throughs. Its nested `Kind` enum exposes all officially
supported CSS `text-decoration-style` keywords, each mapped to its canonical CSS string via `[Description]` attributes.
The type fully supports parsing, safe parsing (`TryParse`), and implicit conversions for ergonomic usage throughout the
theming pipeline.

## Constructors

`StyleTextDecorationStyle(Kind kind)` Creates a new instance using the provided decoration stroke style. The enum value
is resolved to a CSS keyword and validated through the `StyleValueBase` constructor.

## Properties

| Name    | Type     | Description                                                                   |
|---------|----------|-------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `text-decoration-style` value (e.g., `"solid"`, `"wavy"`). |

## Methods

| Name                                                            | Returns                    | Description                                                                                                       |
|-----------------------------------------------------------------|----------------------------|-------------------------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                          | `StyleTextDecorationStyle` | Parses a CSS keyword string into a strongly typed instance; throws if the string does not match any valid `Kind`. |
| `TryParse(string? value, out StyleTextDecorationStyle? result)` | `bool`                     | Attempts to parse without throwing; returns `true` on success and assigns the parsed instance.                    |
| `Equals(object? obj)`                                           | `bool`                     | Structural record equality.                                                                                       |
| `Equals(StyleTextDecorationStyle? other)`                       | `bool`                     | Equality check against another instance.                                                                          |
| `GetHashCode()`                                                 | `int`                      | Returns a hash code derived from record semantics.                                                                |

## Nested Types

| Name   | Type   | Description                                                   |
|--------|--------|---------------------------------------------------------------|
| `Kind` | `enum` | Defines all valid CSS `text-decoration-style` keyword values. |

### `StyleTextDecorationStyle.Kind` Members

| Name     | Description                                             |
|----------|---------------------------------------------------------|
| `Dashed` | `"dashed"` — Draws the decoration line as short dashes. |
| `Dotted` | `"dotted"` — Draws the line as a sequence of dots.      |
| `Double` | `"double"` — Draws a pair of parallel lines.            |
| `Solid`  | `"solid"` — Draws a single continuous line.             |
| `Wavy`   | `"wavy"` — Draws a wavy, zigzag-style decoration line.  |

## Operators

| Operator                                                                       | Returns                    | Description                                                           |
|--------------------------------------------------------------------------------|----------------------------|-----------------------------------------------------------------------|
| `implicit operator StyleTextDecorationStyle(string? value)`                    | `StyleTextDecorationStyle` | Parses the string into a strongly typed instance via `Parse`.         |
| `implicit operator string(StyleTextDecorationStyle? value)`                    | `string`                   | Converts the instance to its CSS keyword or returns `""` when `null`. |
| `operator ==(StyleTextDecorationStyle? left, StyleTextDecorationStyle? right)` | `bool`                     | Structural equality comparison.                                       |
| `operator !=(StyleTextDecorationStyle? left, StyleTextDecorationStyle? right)` | `bool`                     | Structural inequality comparison.                                     |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown when `Parse` or the implicit string→`StyleTextDecorationStyle` operator receives a string that does not
  correspond to a valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the CSS keyword contains invalid or unsafe characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class TextDecorationStyleDemo
{
    public void Demo()
    {
        // Construct from enum
        var solid = new StyleTextDecorationStyle(StyleTextDecorationStyle.Kind.Solid);
        string cssSolid = solid.Value;            // "solid"

        // Parse from string
        var wavy = StyleTextDecorationStyle.Parse("wavy");
        string cssWavy = wavy;                    // "wavy"

        // TryParse with graceful fallback
        if (StyleTextDecorationStyle.TryParse("double", out var dbl))
        {
            string cssDouble = dbl!.Value;        // "double"
        }

        // Implicit from string
        StyleTextDecorationStyle dotted = "dotted";
        string cssDotted = dotted;                // "dotted"
    }
}
```

---

*Revision Date: 2025-11-15*
