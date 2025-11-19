# Allyaria.Theming.StyleTypes.StyleTextAlign

`StyleTextAlign` is a sealed style value record representing the CSS `text-align` property within the Allyaria theming
system. It inherits from `StyleValueBase` (implementing `IStyleValue`) and provides a strongly typed wrapper for all
valid horizontal text-alignment keywords.

## Summary

`StyleTextAlign` is an immutable, validated CSS style value used to specify horizontal inline-content alignment. It
exposes all officially supported CSS `text-align` options through the nested `Kind` enum, maps each to its CSS keyword
using `[Description]` attributes, and validates the resulting value through `StyleValueBase`. The type supports parsing,
safe parsing (`TryParse`), and implicit conversions for ergonomic usage in theming and component styling.

## Constructors

`StyleTextAlign(Kind kind)` Creates a new instance representing the specified horizontal text-alignment behavior. The
enum value is resolved to the proper CSS keyword and validated through the `StyleValueBase` base constructor.

## Properties

| Name    | Type     | Description                                                                       |
|---------|----------|-----------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `text-align` value (e.g., `"center"`, `"justify"`, `"start"`). |

## Methods

| Name                                                  | Returns          | Description                                                                                               |
|-------------------------------------------------------|------------------|-----------------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                | `StyleTextAlign` | Parses a CSS `text-align` keyword string into a strongly typed instance; throws if the string is invalid. |
| `TryParse(string? value, out StyleTextAlign? result)` | `bool`           | Attempts to parse without throwing; returns `true` and assigns the parsed value on success.               |
| `Equals(object? obj)`                                 | `bool`           | Structural equality check.                                                                                |
| `Equals(StyleTextAlign? other)`                       | `bool`           | Strongly typed structural equality check.                                                                 |
| `GetHashCode()`                                       | `int`            | Returns a structural hash code.                                                                           |

## Nested Types

| Name   | Type   | Description                                            |
|--------|--------|--------------------------------------------------------|
| `Kind` | `enum` | Defines all supported CSS `text-align` keyword values. |

### `StyleTextAlign.Kind` Members

| Name          | Description                                                                              |
|---------------|------------------------------------------------------------------------------------------|
| `Center`      | `"center"` — Centers inline content within the line box.                                 |
| `End`         | `"end"` — Aligns content to the end edge of the line box based on writing direction.     |
| `Justify`     | `"justify"` — Stretches text so each non-final line spans the full container width.      |
| `MatchParent` | `"match-parent"` — Inherits alignment from the parent but resolves directionality.       |
| `Start`       | `"start"` — Aligns content to the start edge of the line box based on writing direction. |

## Operators

| Operator                                                   | Returns          | Description                                                       |
|------------------------------------------------------------|------------------|-------------------------------------------------------------------|
| `implicit operator StyleTextAlign(string? value)`          | `StyleTextAlign` | Parses a string into a `StyleTextAlign` via `Parse`.              |
| `implicit operator string(StyleTextAlign? value)`          | `string`         | Converts the instance to its CSS keyword or returns `""` if null. |
| `operator ==(StyleTextAlign? left, StyleTextAlign? right)` | `bool`           | Tests for structural equality.                                    |
| `operator !=(StyleTextAlign? left, StyleTextAlign? right)` | `bool`           | Tests for structural inequality.                                  |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and the implicit string→`StyleTextAlign` operator when the provided value does not match any valid
  `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the resolved CSS keyword contains invalid characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class TextAlignDemo
{
    public void Demo()
    {
        // Enum-based creation
        var center = new StyleTextAlign(StyleTextAlign.Kind.Center);
        string cssCenter = center.Value;          // "center"

        // Parse from string
        var justify = StyleTextAlign.Parse("justify");
        string cssJustify = justify;              // "justify"

        // TryParse for graceful failure handling
        if (StyleTextAlign.TryParse("start", out var start))
        {
            string cssStart = start!.Value;       // "start"
        }

        // Implicit string conversion
        StyleTextAlign end = "end";
        string cssEnd = end;                      // "end"
    }
}
```

---

*Revision Date: 2025-11-15*
