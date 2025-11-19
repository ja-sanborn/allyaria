# Allyaria.Theming.StyleTypes.StyleOverflowWrap

`StyleOverflowWrap` is a sealed style value record representing the CSS `overflow-wrap` property within the Allyaria
theming system. It inherits from `StyleValueBase` (and thus implements `IStyleValue`) and provides a strongly typed
wrapper around the allowed CSS keywords for controlling how words break when they exceed their container.

## Summary

`StyleOverflowWrap` is an immutable, validated style value that determines how aggressively the browser may break words
to prevent overflow. It exposes all official CSS `overflow-wrap` values via its nested `Kind` enum, maps each value to
its proper CSS keyword using `[Description]` attributes, validates it through `StyleValueBase`, and supports parsing,
safe parsing, and implicit conversions.

## Constructors

`StyleOverflowWrap(Kind kind)` Creates a new instance representing the specified word-breaking mode. Maps the enum to
its CSS keyword and validates it using `StyleValueBase`.

## Properties

| Name    | Type     | Description                                                                                |
|---------|----------|--------------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `overflow-wrap` value (e.g., `"normal"`, `"anywhere"`, `"break-word"`). |

## Methods

| Name                                                     | Returns             | Description                                                         |
|----------------------------------------------------------|---------------------|---------------------------------------------------------------------|
| `Parse(string? value)`                                   | `StyleOverflowWrap` | Parses a CSS string into a strongly typed value; throws if invalid. |
| `TryParse(string? value, out StyleOverflowWrap? result)` | `bool`              | Attempts to parse without throwing, returning `true` if successful. |
| `Equals(object? obj)`                                    | `bool`              | Structural record equality.                                         |
| `Equals(StyleOverflowWrap? other)`                       | `bool`              | Equality comparison with another instance.                          |
| `GetHashCode()`                                          | `int`               | Hash code derived from record structure.                            |

## Nested Types

| Name   | Type   | Description                                     |
|--------|--------|-------------------------------------------------|
| `Kind` | `enum` | Lists all supported CSS `overflow-wrap` values. |

### `StyleOverflowWrap.Kind` Members

| Name        | Description                                                                                                                   |
|-------------|-------------------------------------------------------------------------------------------------------------------------------|
| `Anywhere`  | `"anywhere"` — Browser may break words at arbitrary points to avoid overflow.                                                 |
| `BreakWord` | `"break-word"` — Breaks within words only when essential to prevent overflow; mostly similar to `normal` but more permissive. |
| `Normal`    | `"normal"` — Standard word-wrapping behavior; breaks occur at normal breakpoints.                                             |

## Operators

| Operator                                                         | Returns             | Description                                         |
|------------------------------------------------------------------|---------------------|-----------------------------------------------------|
| `implicit operator StyleOverflowWrap(string? value)`             | `StyleOverflowWrap` | Parses the string to an instance using `Parse`.     |
| `implicit operator string(StyleOverflowWrap? value)`             | `string`            | Returns the CSS keyword or an empty string if null. |
| `operator ==(StyleOverflowWrap? left, StyleOverflowWrap? right)` | `bool`              | Structural equality comparison.                     |
| `operator !=(StyleOverflowWrap? left, StyleOverflowWrap? right)` | `bool`              | Structural inequality.                              |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown when `Parse` or implicit conversion receives a string that does not correspond to a valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the CSS keyword contains invalid characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class OverflowWrapDemo
{
    public void Demo()
    {
        // Enum construction
        var wrapAnywhere = new StyleOverflowWrap(StyleOverflowWrap.Kind.Anywhere);
        string cssAnywhere = wrapAnywhere.Value;  // "anywhere"

        // Parse from string
        var breakWord = StyleOverflowWrap.Parse("break-word");
        string cssBreakWord = breakWord;          // "break-word"

        // TryParse
        if (StyleOverflowWrap.TryParse("normal", out var normal))
        {
            string cssNormal = normal!.Value;     // "normal"
        }

        // Implicit conversion
        StyleOverflowWrap implicitWrap = "anywhere";
        string css = implicitWrap;                // "anywhere"
    }
}
```

---

*Revision Date: 2025-11-15*
