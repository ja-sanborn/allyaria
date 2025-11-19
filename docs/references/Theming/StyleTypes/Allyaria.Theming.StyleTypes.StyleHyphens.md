# Allyaria.Theming.StyleTypes.StyleHyphens

`StyleHyphens` is a sealed style value record representing the CSS `hyphens` property within the Allyaria theming
system. It inherits from `StyleValueBase` (and thus implements `IStyleValue`) and provides a strongly typed wrapper
around the allowed CSS hyphenation behaviors.

## Summary

`StyleHyphens` is an immutable, validated style value used to specify hyphenation rules for text when it wraps. The
nested `Kind` enum reflects all official CSS `hyphens` keywords. Values are converted to CSS strings through
`[Description]` attributes and validated by the `StyleValueBase` base class. The type also supports parsing, safe
parsing (`TryParse`), and implicit conversions.

## Constructors

`StyleHyphens(Kind kind)` Creates a new `StyleHyphens` instance representing the specified hyphenation behavior, mapping
the enum to its CSS string and validating it via `StyleValueBase`.

## Properties

| Name    | Type     | Description                                                                |
|---------|----------|----------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `hyphens` value (e.g., `"auto"`, `"manual"`, `"none"`). |

## Methods

| Name                                                | Returns        | Description                                                                                 |
|-----------------------------------------------------|----------------|---------------------------------------------------------------------------------------------|
| `Parse(string? value)`                              | `StyleHyphens` | Parses a CSS `hyphens` value into a `StyleHyphens` instance; throws if invalid.             |
| `TryParse(string? value, out StyleHyphens? result)` | `bool`         | Attempts to parse the input into a `StyleHyphens` instance, returning `true` if successful. |
| `Equals(object? obj)`                               | `bool`         | Determines equality with another object using record semantics.                             |
| `Equals(StyleHyphens? other)`                       | `bool`         | Compares this instance to another `StyleHyphens` using value equality.                      |
| `GetHashCode()`                                     | `int`          | Retrieves the hash code for this instance.                                                  |

## Nested Types

| Name   | Type   | Description                                          |
|--------|--------|------------------------------------------------------|
| `Kind` | `enum` | Defines all supported CSS `hyphens` property values. |

### `StyleHyphens.Kind` Members

| Name     | Description                                                                                |
|----------|--------------------------------------------------------------------------------------------|
| `Auto`   | `"auto"` — Browser automatically inserts hyphens based on language rules and dictionaries. |
| `Manual` | `"manual"` — Hyphens appear only where soft hyphens (`&shy;`) are manually provided.       |
| `None`   | `"none"` — Hyphenation is disabled; words break only at whitespace.                        |

## Operators

| Operator                                               | Returns        | Description                                                        |
|--------------------------------------------------------|----------------|--------------------------------------------------------------------|
| `implicit operator StyleHyphens(string? value)`        | `StyleHyphens` | Converts the string into a `StyleHyphens` via `Parse`.             |
| `implicit operator string(StyleHyphens? value)`        | `string`       | Converts the instance into a CSS string (or empty string if null). |
| `operator ==(StyleHyphens? left, StyleHyphens? right)` | `bool`         | Compares two `StyleHyphens` instances for equality.                |
| `operator !=(StyleHyphens? left, StyleHyphens? right)` | `bool`         | Determines whether two instances are unequal.                      |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` when the provided string does not match any valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the resolved CSS keyword is invalid or unsafe.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class StyleHyphensDemo
{
    public void Demo()
    {
        // Create from enum
        var auto = new StyleHyphens(StyleHyphens.Kind.Auto);
        string cssAuto = auto.Value;             // "auto"

        // Parse from string
        var manual = StyleHyphens.Parse("manual");
        string cssManual = manual;               // "manual"

        // TryParse for safe handling
        if (StyleHyphens.TryParse("none", out var none))
        {
            string cssNone = none!.Value;        // "none"
        }

        // Implicit string → StyleHyphens
        StyleHyphens implicitAuto = "auto";
        string css = implicitAuto;               // "auto"
    }
}
```

---

*Revision Date: 2025-11-15*
