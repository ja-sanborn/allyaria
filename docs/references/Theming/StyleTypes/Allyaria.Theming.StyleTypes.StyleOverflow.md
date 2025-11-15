# Allyaria.Theming.StyleTypes.StyleOverflow

`StyleOverflow` is a sealed style value record representing CSS `overflow`, `overflow-x`, and `overflow-y` values within
the Allyaria theming system. It inherits from `StyleValueBase` (and thus implements `IStyleValue`) and provides a
strongly typed wrapper around all standard CSS overflow behaviors.

## Summary

`StyleOverflow` is an immutable, validated representation of CSS overflow behavior. Using its nested `Kind` enumeration,
it exposes the full set of official `overflow` keywords, mapping each to its exact CSS string through `[Description]`
attributes. It supports parsing from raw strings, safe parsing (`TryParse`), and implicit conversions, ensuring
type-safety and consistency across theming components.

## Constructors

`StyleOverflow(Kind kind)` Creates a new instance representing the specified overflow behavior. The enum value is
converted to the corresponding CSS keyword and validated via the `StyleValueBase` constructor.

## Properties

| Name    | Type     | Description                                                                      |
|---------|----------|----------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `overflow` keyword (e.g., `"auto"`, `"hidden"`, `"visible"`). |

## Methods

| Name                                                 | Returns         | Description                                                                                   |
|------------------------------------------------------|-----------------|-----------------------------------------------------------------------------------------------|
| `Parse(string? value)`                               | `StyleOverflow` | Parses a CSS overflow string into a `StyleOverflow` instance; throws if the value is invalid. |
| `TryParse(string? value, out StyleOverflow? result)` | `bool`          | Attempts to parse a string into a `StyleOverflow`; returns `true` on success.                 |
| `Equals(object? obj)`                                | `bool`          | Structural equality comparison.                                                               |
| `Equals(StyleOverflow? other)`                       | `bool`          | Structural equality with another instance.                                                    |
| `GetHashCode()`                                      | `int`           | Returns a record-derived hash code.                                                           |

## Nested Types

| Name   | Type   | Description                                          |
|--------|--------|------------------------------------------------------|
| `Kind` | `enum` | Defines all supported CSS `overflow` keyword values. |

### `StyleOverflow.Kind` Members

| Name      | Description                                                                                |
|-----------|--------------------------------------------------------------------------------------------|
| `Auto`    | `"auto"` — Browser determines scrollbar visibility automatically.                          |
| `Clip`    | `"clip"` — Content is clipped, no scrollbars.                                              |
| `Hidden`  | `"hidden"` — Clips content; overflow not visible. Scrollbars may appear for compatibility. |
| `Scroll`  | `"scroll"` — Always displays scrollbars; content is scrollable.                            |
| `Visible` | `"visible"` — Overflow is visible outside the element’s box.                               |

## Operators

| Operator                                                 | Returns         | Description                                                          |
|----------------------------------------------------------|-----------------|----------------------------------------------------------------------|
| `implicit operator StyleOverflow(string? value)`         | `StyleOverflow` | Converts a string into a `StyleOverflow` via `Parse`.                |
| `implicit operator string(StyleOverflow? value)`         | `string`        | Converts the instance to its CSS keyword or an empty string if null. |
| `operator ==(StyleOverflow? left, StyleOverflow? right)` | `bool`          | Checks structural equality.                                          |
| `operator !=(StyleOverflow? left, StyleOverflow? right)` | `bool`          | Checks structural inequality.                                        |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and implicit string→`StyleOverflow` conversion when the value does not map to any valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` when the CSS keyword fails base-level validation.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class OverflowDemo
{
    public void Demo()
    {
        // Enum construction
        var hidden = new StyleOverflow(StyleOverflow.Kind.Hidden);
        string cssHidden = hidden.Value;        // "hidden"

        // Parse from string
        var scroll = StyleOverflow.Parse("scroll");
        string cssScroll = scroll;              // "scroll"

        // TryParse pattern
        if (StyleOverflow.TryParse("clip", out var clipped))
        {
            string cssClip = clipped!.Value;    // "clip"
        }

        // Implicit conversion
        StyleOverflow auto = "auto";
        string cssAuto = auto;                  // "auto"
    }
}
```

---

*Revision Date: 2025-11-15*
