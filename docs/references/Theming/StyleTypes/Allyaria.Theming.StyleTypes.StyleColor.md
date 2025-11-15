# Allyaria.Theming.StyleTypes.StyleColor

`StyleColor` is a sealed style value record representing a CSS `color` value within the Allyaria theming system. It
inherits from `StyleValueBase` (which implements `IStyleValue`) and provides strongly typed color handling through
integration with `HexColor`. It supports parsing from CSS color strings, conversion to and from `HexColor`, and safe
serialization to normalized CSS output.

## Summary

`StyleColor` is an immutable, validated representation of any CSS color—including hex codes, RGB(A), and named CSS
colors. It ensures that all colors used in the theming system are validated and normalized through `HexColor` before
becoming part of CSS output. It also supports convenient implicit conversions and resilient parsing via `Parse` and
`TryParse`.

## Constructors

`StyleColor(string value)` Creates a new instance by parsing the provided string into a `HexColor`. Throws if the string
cannot be parsed as a CSS-compatible color.

`StyleColor(HexColor color)` Creates a new instance from an existing `HexColor`. Stores the `HexColor` in `Color` and
passes the normalized string to `StyleValueBase`.

## Properties

| Name    | Type       | Description                                                             |
|---------|------------|-------------------------------------------------------------------------|
| `Value` | `string`   | The normalized CSS color string (typically `#RRGGBBAA`).                |
| `Color` | `HexColor` | The strongly typed underlying color value represented by this instance. |

## Methods

| Name                                              | Returns      | Description                                                                                    |
|---------------------------------------------------|--------------|------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                            | `StyleColor` | Parses a CSS color string into a `StyleColor` by constructing a `HexColor`; throws if invalid. |
| `TryParse(string? value, out StyleColor? result)` | `bool`       | Attempts to parse a CSS color string; assigns `result` or `null` and returns `true`/`false`.   |
| `Equals(object? obj)`                             | `bool`       | Determines equality with another object (record structural equality).                          |
| `Equals(StyleColor? other)`                       | `bool`       | Determines equality with another `StyleColor` instance.                                        |
| `GetHashCode()`                                   | `int`        | Returns a hash code based on the record’s value equality semantics.                            |

## Operators

| Operator                                           | Returns      | Description                                                           |
|----------------------------------------------------|--------------|-----------------------------------------------------------------------|
| `implicit operator StyleColor(string? value)`      | `StyleColor` | Converts a CSS color string into a `StyleColor` via `Parse`.          |
| `implicit operator string(StyleColor? value)`      | `string`     | Converts the style value into a CSS string (or empty string if null). |
| `operator ==(StyleColor? left, StyleColor? right)` | `bool`       | Determines equality between two instances.                            |
| `operator !=(StyleColor? left, StyleColor? right)` | `bool`       | Determines inequality.                                                |

## Events

*None*

## Exceptions

* **`ArgumentException`**  
  Thrown by the constructor and `Parse(string?)` when the input cannot be parsed into a valid `HexColor`.

* **`AryArgumentException`**  
  May be thrown indirectly by `StyleValueBase` if the generated CSS string contains invalid or unsafe characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;
using Allyaria.Theming.Types;

public class StyleColorDemo
{
    public void Demo()
    {
        // From string (hex or CSS named color)
        StyleColor red = "#FF0000";
        string cssRed = red.Value;                   // "#FF0000FF"

        // From HexColor
        HexColor tealHex = new HexColor("#008080");
        StyleColor teal = new StyleColor(tealHex);
        string cssTeal = teal;                       // "#008080FF"

        // Parse dynamically
        var parsed = StyleColor.Parse("rgba(255, 0, 0, 0.5)");
        string cssParsed = parsed.Value;

        // TryParse for safe failure
        if (StyleColor.TryParse("invalid123", out var result))
        {
            // use result
        }
        else
        {
            // gracefully handle invalid color
        }
    }
}
```

---

*Revision Date: 2025-11-15*
