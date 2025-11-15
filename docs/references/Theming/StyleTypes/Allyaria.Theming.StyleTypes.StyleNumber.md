# Allyaria.Theming.StyleTypes.StyleNumber

`StyleNumber` is a sealed style value record representing a validated integer CSS value within the Allyaria theming
system. It inherits from `StyleValueBase` (and therefore implements `IStyleValue`) and provides strict parsing and
normalization for CSS properties that accept integer-only inputs.

## Summary

`StyleNumber` is an immutable, validated wrapper around integer CSS values. It ensures the provided string is a valid
integer (or empty/whitespace) using invariant culture parsing and exposes the parsed value through the `Number`
property. It supports parsing, safe parsing (`TryParse`), and implicit conversions for ease of use throughout the
theming engine.

## Constructors

`StyleNumber(string value)` Creates a new instance from the provided string. If the string is non-empty, the constructor
validates and parses it into an integer using invariant rules. Throws if conversion fails.

## Properties

| Name     | Type     | Description                                                               |
|----------|----------|---------------------------------------------------------------------------|
| `Number` | `int`    | The parsed integer value. Defaults to `0` for empty or whitespace inputs. |
| `Value`  | `string` | The original CSS numeric string as validated by `StyleValueBase`.         |

## Methods

| Name                                               | Returns       | Description                                                                           |
|----------------------------------------------------|---------------|---------------------------------------------------------------------------------------|
| `Parse(string? value)`                             | `StyleNumber` | Parses a CSS integer string (or uses an empty string if `null`); throws when invalid. |
| `TryParse(string? value, out StyleNumber? result)` | `bool`        | Attempts to parse the string into a `StyleNumber`; returns `true` on success.         |
| `Equals(object? obj)`                              | `bool`        | Structural equality comparison.                                                       |
| `Equals(StyleNumber? other)`                       | `bool`        | Compares this instance to another `StyleNumber` for structural equality.              |
| `GetHashCode()`                                    | `int`         | Returns a record-based hash code.                                                     |

## Operators

| Operator                                             | Returns       | Description                                             |
|------------------------------------------------------|---------------|---------------------------------------------------------|
| `implicit operator StyleNumber(string? value)`       | `StyleNumber` | Converts the string into a `StyleNumber` via `Parse`.   |
| `implicit operator string(StyleNumber? value)`       | `string`      | Returns the original numeric string (or empty if null). |
| `operator ==(StyleNumber? left, StyleNumber? right)` | `bool`        | Checks equality.                                        |
| `operator !=(StyleNumber? left, StyleNumber? right)` | `bool`        | Checks inequality.                                      |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown when parsing fails during construction or via `Parse`.

* **`ArgumentException`**  
  May be thrown indirectly if invalid numeric characters appear before `StyleValueBase` validation.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class StyleNumberDemo
{
    public void Demo()
    {
        // Direct creation
        StyleNumber spacing = new StyleNumber("12");
        int val = spacing.Number;            // 12

        // Implicit creation from string
        StyleNumber implicitNum = "42";
        string css = implicitNum;            // "42"

        // TryParse for safe handling
        if (StyleNumber.TryParse("100", out var parsed))
        {
            int parsedValue = parsed!.Number; // 100
        }

        // Empty or whitespace is allowed (treated as 0)
        StyleNumber empty = "";
        int zero = empty.Number;             // 0
    }
}
```

---

*Revision Date: 2025-11-15*
