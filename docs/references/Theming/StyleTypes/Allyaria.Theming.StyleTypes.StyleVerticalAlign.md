# Allyaria.Theming.StyleTypes.StyleVerticalAlign

`StyleVerticalAlign` is a sealed style value record representing the CSS `vertical-align` property within the Allyaria
theming system. It inherits from `StyleValueBase` (therefore implementing `IStyleValue`) and provides a strongly typed
wrapper over all valid CSS vertical-alignment modes.

## Summary

`StyleVerticalAlign` is an immutable, validated CSS style value used to control the vertical alignment of inline-level
or table-cell elements. Its nested `Kind` enum provides the complete set of standard CSS vertical-align keywords. Each
enum member maps to its canonical CSS string via `[Description]` attributes and is validated by the `StyleValueBase`
constructor. The type includes parsing, safe parsing (`TryParse`), and implicit conversions for developer-friendly use
in theming or layout styling.

## Constructors

`StyleVerticalAlign(Kind kind)` Creates a new instance representing the specified vertical-alignment behavior. The enum
value is converted to its CSS keyword using the `[Description]` attribute and validated via the `StyleValueBase` base
class.

## Properties

| Name    | Type     | Description                                                                                 |
|---------|----------|---------------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `vertical-align` keyword (e.g., `"middle"`, `"text-top"`, `"baseline"`). |

## Methods

| Name                                                      | Returns              | Description                                                                                          |
|-----------------------------------------------------------|----------------------|------------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                    | `StyleVerticalAlign` | Parses a CSS `vertical-align` string into a strongly typed instance; throws if the value is invalid. |
| `TryParse(string? value, out StyleVerticalAlign? result)` | `bool`               | Attempts to parse without throwing; returns `true` on success.                                       |
| `Equals(object? obj)`                                     | `bool`               | Structural equality comparison.                                                                      |
| `Equals(StyleVerticalAlign? other)`                       | `bool`               | Strongly typed structural equality.                                                                  |
| `GetHashCode()`                                           | `int`                | Returns a hash code derived from record semantics.                                                   |

## Nested Types

| Name   | Type   | Description                                            |
|--------|--------|--------------------------------------------------------|
| `Kind` | `enum` | Defines all valid CSS `vertical-align` keyword values. |

### `StyleVerticalAlign.Kind` Members

| Name         | Description                                                                                   |
|--------------|-----------------------------------------------------------------------------------------------|
| `Baseline`   | `"baseline"` — Aligns the element’s baseline with its parent’s baseline.                      |
| `Bottom`     | `"bottom"` — Aligns the bottom of the element with the lowest element on the line.            |
| `Middle`     | `"middle"` — Aligns the middle of the element with the parent’s baseline + half the x-height. |
| `Sub`        | `"sub"` — Lowers the element as subscript text.                                               |
| `Super`      | `"super"` — Raises the element as superscript text.                                           |
| `TextBottom` | `"text-bottom"` — Aligns the element with the bottom of the parent’s text content area.       |
| `TextTop`    | `"text-top"` — Aligns the element with the top of the parent’s text content area.             |
| `Top`        | `"top"` — Aligns the top of the element with the highest element on the line.                 |

## Operators

| Operator                                                           | Returns              | Description                                                        |
|--------------------------------------------------------------------|----------------------|--------------------------------------------------------------------|
| `implicit operator StyleVerticalAlign(string? value)`              | `StyleVerticalAlign` | Parses the CSS keyword into a strongly typed instance.             |
| `implicit operator string(StyleVerticalAlign? value)`              | `string`             | Converts the instance to its CSS keyword string (or `""` if null). |
| `operator ==(StyleVerticalAlign? left, StyleVerticalAlign? right)` | `bool`               | Structural equality comparison.                                    |
| `operator !=(StyleVerticalAlign? left, StyleVerticalAlign? right)` | `bool`               | Structural inequality comparison.                                  |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown when `Parse` (and therefore implicit string→`StyleVerticalAlign` conversion) receives a value that does not
  correspond to a valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` when the final CSS keyword contains invalid or unsafe characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class VerticalAlignDemo
{
    public void Demo()
    {
        // Create from enum
        var middle = new StyleVerticalAlign(StyleVerticalAlign.Kind.Middle);
        string cssMiddle = middle.Value;        // "middle"

        // Parse from string
        var baseline = StyleVerticalAlign.Parse("baseline");
        string cssBaseline = baseline;          // "baseline"

        // TryParse
        if (StyleVerticalAlign.TryParse("text-top", out var textTop))
        {
            string cssTextTop = textTop!.Value; // "text-top"
        }

        // Implicit from string
        StyleVerticalAlign implicitVal = "bottom";
        string cssBottom = implicitVal;         // "bottom"
    }
}
```

---

*Revision Date: 2025-11-15*
