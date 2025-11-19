# Allyaria.Theming.StyleTypes.StylePosition

`StylePosition` is a sealed style value record representing the CSS `position` property within the Allyaria theming
system. It inherits from `StyleValueBase` (thus implementing `IStyleValue`) and provides a strongly typed wrapper over
all standard CSS positioning modes.

## Summary

`StylePosition` is an immutable, validated CSS style value specifying how an element is positioned in the document flow.
It exposes the full set of official CSS `position` keywords through its `Kind` enum, mapping each to its CSS
representation using `[Description]` attributes. It supports parsing, safe parsing with `TryParse`, and implicit
conversions to ensure ergonomic and type-safe usage in theming.

## Constructors

`StylePosition(Kind kind)` Creates a new instance using the provided positioning mode. The enum value is converted to
its CSS keyword and validated by the `StyleValueBase` constructor.

## Properties

| Name    | Type     | Description                                                                         |
|---------|----------|-------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS position keyword (e.g., `"absolute"`, `"relative"`, `"sticky"`). |

## Methods

| Name                                                 | Returns         | Description                                                                        |
|------------------------------------------------------|-----------------|------------------------------------------------------------------------------------|
| `Parse(string? value)`                               | `StylePosition` | Parses a CSS `position` value into a strongly typed instance; throws when invalid. |
| `TryParse(string? value, out StylePosition? result)` | `bool`          | Attempts to parse without throwing; returns `true` if successful.                  |
| `Equals(object? obj)`                                | `bool`          | Structural equality comparison.                                                    |
| `Equals(StylePosition? other)`                       | `bool`          | Equality comparison with another instance.                                         |
| `GetHashCode()`                                      | `int`           | Computes a structural hash code.                                                   |

## Nested Types

| Name   | Type   | Description                                    |
|--------|--------|------------------------------------------------|
| `Kind` | `enum` | Defines all supported CSS `position` keywords. |

### `StylePosition.Kind` Members

| Name       | Description                                                                                  |
|------------|----------------------------------------------------------------------------------------------|
| `Absolute` | `"absolute"` — Removed from normal flow; positioned relative to nearest positioned ancestor. |
| `Fixed`    | `"fixed"` — Positioned relative to the viewport; does not move during scroll.                |
| `Relative` | `"relative"` — Positioned relative to its normal position; offsets apply.                    |
| `Static`   | `"static"` — Default positioning mode; element follows normal flow.                          |
| `Sticky`   | `"sticky"` — Toggles between `relative` and `fixed` based on scroll position.                |

## Operators

| Operator                                                 | Returns         | Description                                                        |
|----------------------------------------------------------|-----------------|--------------------------------------------------------------------|
| `implicit operator StylePosition(string? value)`         | `StylePosition` | Parses the CSS position string into an instance using `Parse`.     |
| `implicit operator string(StylePosition? value)`         | `string`        | Converts the style instance to its CSS keyword or an empty string. |
| `operator ==(StylePosition? left, StylePosition? right)` | `bool`          | Structural equality comparison.                                    |
| `operator !=(StylePosition? left, StylePosition? right)` | `bool`          | Structural inequality.                                             |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and implicit conversion when the provided string does not map to a valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the resolved CSS keyword contains invalid or unsafe characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class PositionDemo
{
    public void Demo()
    {
        // Enum-based creation
        var absolute = new StylePosition(StylePosition.Kind.Absolute);
        string cssAbs = absolute.Value;        // "absolute"

        // Parse from string
        var sticky = StylePosition.Parse("sticky");
        string cssSticky = sticky;             // "sticky"

        // TryParse
        if (StylePosition.TryParse("fixed", out var fixedPos))
        {
            string cssFixed = fixedPos!.Value; // "fixed"
        }

        // Implicit conversion from string
        StylePosition relative = "relative";
        string cssRelative = relative;         // "relative"
    }
}
```

---

*Revision Date: 2025-11-15*
