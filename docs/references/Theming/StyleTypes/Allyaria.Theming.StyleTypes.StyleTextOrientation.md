# Allyaria.Theming.StyleTypes.StyleTextOrientation

`StyleTextOrientation` is a sealed style value record representing the CSS `text-orientation` property within the
Allyaria theming system. It inherits from `StyleValueBase` (therefore implementing `IStyleValue`) and provides a
strongly typed wrapper over all valid text-orientation modes used in vertical writing systems.

## Summary

`StyleTextOrientation` is an immutable, validated representation of the CSS `text-orientation` property. Its nested
`Kind` enum exposes all officially supported CSS text-orientation modes—`mixed`, `sideways`, and `upright`—each mapped
to its canonical string form via `[Description]` attributes. The type supports parsing, safe parsing, and implicit
conversions, ensuring ergonomic, type-safe use throughout the theming engine.

## Constructors

`StyleTextOrientation(Kind kind)` Creates a new instance representing the given orientation mode. The enum value is
converted to its CSS keyword via `[Description]` and validated using `StyleValueBase`.

## Properties

| Name    | Type     | Description                                                                               |
|---------|----------|-------------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS text-orientation keyword (e.g., `"mixed"`, `"sideways"`, `"upright"`). |

## Methods

| Name                                                        | Returns                | Description                                                                              |
|-------------------------------------------------------------|------------------------|------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                      | `StyleTextOrientation` | Parses a CSS text-orientation keyword into a strongly typed instance; throws if invalid. |
| `TryParse(string? value, out StyleTextOrientation? result)` | `bool`                 | Attempts to parse the keyword without throwing; returns `true` on success.               |
| `Equals(object? obj)`                                       | `bool`                 | Structural equality check.                                                               |
| `Equals(StyleTextOrientation? other)`                       | `bool`                 | Strongly typed structural equality comparison.                                           |
| `GetHashCode()`                                             | `int`                  | Record-based structural hash code.                                                       |

## Nested Types

| Name   | Type   | Description                                                  |
|--------|--------|--------------------------------------------------------------|
| `Kind` | `enum` | Defines all supported CSS `text-orientation` keyword values. |

### `StyleTextOrientation.Kind` Members

| Name       | Description                                                                                            |
|------------|--------------------------------------------------------------------------------------------------------|
| `Mixed`    | `"mixed"` — Default behavior: ideographic & vertical forms upright, Latin & other characters sideways. |
| `Sideways` | `"sideways"` — Forces all characters to be rotated sideways in vertical writing modes.                 |
| `Upright`  | `"upright"` — Forces all characters to remain upright in vertical writing modes.                       |

## Operators

| Operator                                                               | Returns                | Description                                                  |
|------------------------------------------------------------------------|------------------------|--------------------------------------------------------------|
| `implicit operator StyleTextOrientation(string? value)`                | `StyleTextOrientation` | Converts a CSS string to a strongly typed value via `Parse`. |
| `implicit operator string(StyleTextOrientation? value)`                | `string`               | Converts the instance to its CSS keyword, or `""` if null.   |
| `operator ==(StyleTextOrientation? left, StyleTextOrientation? right)` | `bool`                 | Structural equality comparison.                              |
| `operator !=(StyleTextOrientation? left, StyleTextOrientation? right)` | `bool`                 | Structural inequality comparison.                            |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` (and implicit `string → StyleTextOrientation`) when the input does not correspond to any valid
  `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the resulting CSS string is invalid or unsafe.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class TextOrientationDemo
{
    public void Demo()
    {
        // Construct from enum
        var upright = new StyleTextOrientation(StyleTextOrientation.Kind.Upright);
        string cssUpright = upright.Value;           // "upright"

        // Parse from string
        var sideways = StyleTextOrientation.Parse("sideways");
        string cssSideways = sideways;               // "sideways"

        // TryParse
        if (StyleTextOrientation.TryParse("mixed", out var mixed))
        {
            string cssMixed = mixed!.Value;         // "mixed"
        }

        // Implicit from string
        StyleTextOrientation implicitOrientation = "upright";
        string cssImplicit = implicitOrientation;   // "upright"
    }
}
```

---

*Revision Date: 2025-11-15*
