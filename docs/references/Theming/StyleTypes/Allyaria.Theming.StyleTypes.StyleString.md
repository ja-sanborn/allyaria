# Allyaria.Theming.StyleTypes.StyleString

`StyleString` is a sealed style value record representing a raw CSS-compatible string within the Allyaria theming
system. It inherits from `StyleValueBase` (and therefore implements `IStyleValue`) and provides the simplest possible
strongly typed wrapper for arbitrary CSS string values.

## Summary

`StyleString` acts as a normalized, validated container for any CSS string value—keywords, URLs, identifiers, or
arbitrary constructs (e.g., `"underline"`, `"url('/img.png')"`, `"1fr"`). The type guarantees safe CSS emission by
validating the string via `StyleValueBase`, while offering parsing, safe parsing, and implicit conversions for ease of
use.

## Constructors

`StyleString(string? value = "")` Initializes a new instance using the provided raw string. If `value` is `null`, an
empty string is used. Validation is performed by the `StyleValueBase` constructor.

## Properties

| Name    | Type     | Description                                                                      |
|---------|----------|----------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS string value. Empty if constructed with `null` or whitespace. |

## Methods

| Name                                               | Returns       | Description                                                                             |
|----------------------------------------------------|---------------|-----------------------------------------------------------------------------------------|
| `Parse(string? value)`                             | `StyleString` | Creates a new `StyleString` representing the given value (or empty if `null`).          |
| `TryParse(string? value, out StyleString? result)` | `bool`        | Attempts to parse into a new `StyleString`, returning `true` unless construction fails. |
| `Equals(object? obj)`                              | `bool`        | Structural equality check.                                                              |
| `Equals(StyleString? other)`                       | `bool`        | Structural equality comparison.                                                         |
| `GetHashCode()`                                    | `int`         | Returns a hash code based on record semantics.                                          |

## Operators

| Operator                                             | Returns       | Description                                                               |
|------------------------------------------------------|---------------|---------------------------------------------------------------------------|
| `implicit operator StyleString(string? value)`       | `StyleString` | Converts a raw string to a validated `StyleString` (via `Parse`).         |
| `implicit operator string(StyleString? value)`       | `string`      | Converts the instance back to its underlying CSS string, or `""` if null. |
| `operator ==(StyleString? left, StyleString? right)` | `bool`        | Structural equality.                                                      |
| `operator !=(StyleString? left, StyleString? right)` | `bool`        | Structural inequality.                                                    |

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class StyleStringDemo
{
    public void Demo()
    {
        // Simple creation
        StyleString underline = new StyleString("underline");
        string css = underline.Value;      // "underline"

        // Null becomes empty string
        StyleString emptyFromNull = new StyleString(null);

        // Parsing
        var url = StyleString.Parse("url('/assets/bg.png')");

        // TryParse
        if (StyleString.TryParse("1fr", out var fr))
        {
            string cssFr = fr!.Value;      // "1fr"
        }

        // Implicit conversions
        StyleString implicitValue = "italic";
        string asString = implicitValue;   // "italic"
    }
}
```

---

*Revision Date: 2025-11-15*
