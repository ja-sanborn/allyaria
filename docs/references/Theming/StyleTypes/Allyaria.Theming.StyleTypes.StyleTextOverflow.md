# Allyaria.Theming.StyleTypes.StyleTextOverflow

`StyleTextOverflow` is a sealed style value record representing the CSS `text-overflow` property within the Allyaria
theming system. It inherits from `StyleValueBase` (and therefore implements `IStyleValue`) and provides a strongly typed
wrapper around the valid text-overflow behaviors (`clip` and `ellipsis`).

## Summary

`StyleTextOverflow` is an immutable, validated CSS value representing how overflowing inline text is visually handled
when it exceeds its container. It exposes the two official CSS keywords through its `Kind` enum, maps each to its
canonical CSS keyword via `[Description]` attributes, and validates the result through the `StyleValueBase` constructor.
It supports parsing, safe parsing (`TryParse`), and implicit conversions to make theme code more ergonomic and less
error-prone.

## Constructors

`StyleTextOverflow(Kind kind)` Initializes a new `StyleTextOverflow` instance for the specified overflow-handling
behavior. The enum value is converted to its CSS keyword using its `[Description]` attribute and validated by the
`StyleValueBase` base class.

## Properties

| Name    | Type     | Description                                                                         |
|---------|----------|-------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `text-overflow` keyword string (e.g., `"ellipsis"` or `"clip"`). |

## Methods

| Name                                                     | Returns             | Description                                                                                                             |
|----------------------------------------------------------|---------------------|-------------------------------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                   | `StyleTextOverflow` | Parses a raw CSS string into a strongly typed `StyleTextOverflow`; throws if the value does not match any valid `Kind`. |
| `TryParse(string? value, out StyleTextOverflow? result)` | `bool`              | Safely attempts to parse a CSS string; assigns the parsed instance or `null`.                                           |
| `Equals(object? obj)`                                    | `bool`              | Structural equality comparison.                                                                                         |
| `Equals(StyleTextOverflow? other)`                       | `bool`              | Strongly typed equality comparison.                                                                                     |
| `GetHashCode()`                                          | `int`               | Returns a structural hash code.                                                                                         |

## Nested Types

| Name   | Type   | Description                                               |
|--------|--------|-----------------------------------------------------------|
| `Kind` | `enum` | Defines the supported CSS `text-overflow` keyword values. |

### `StyleTextOverflow.Kind` Members

| Name       | Description                                                                                   |
|------------|-----------------------------------------------------------------------------------------------|
| `Clip`     | `"clip"` — The overflowed text is clipped silently without any visual indicator.              |
| `Ellipsis` | `"ellipsis"` — The overflowed text is truncated and represented with an ellipsis (“…”) glyph. |

## Operators

| Operator                                                         | Returns             | Description                                                    |
|------------------------------------------------------------------|---------------------|----------------------------------------------------------------|
| `implicit operator StyleTextOverflow(string? value)`             | `StyleTextOverflow` | Parses the given string into a new `StyleTextOverflow`.        |
| `implicit operator string(StyleTextOverflow? value)`             | `string`            | Returns the underlying CSS keyword string (or `""` if `null`). |
| `operator ==(StyleTextOverflow? left, StyleTextOverflow? right)` | `bool`              | Structural equality.                                           |
| `operator !=(StyleTextOverflow? left, StyleTextOverflow? right)` | `bool`              | Structural inequality.                                         |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and implicit string→`StyleTextOverflow` conversion when the input string does not correspond to a
  valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the resolved CSS keyword contains invalid or unsafe characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class TextOverflowDemo
{
    public void Demo()
    {
        // Construct from enum
        var ellipsis = new StyleTextOverflow(StyleTextOverflow.Kind.Ellipsis);
        string cssEllipsis = ellipsis.Value;     // "ellipsis"

        // Parse from string
        var clip = StyleTextOverflow.Parse("clip");
        string cssClip = clip;                   // "clip"

        // TryParse usage
        if (StyleTextOverflow.TryParse("ellipsis", out var parsed))
        {
            string cssParsed = parsed!.Value;    // "ellipsis"
        }

        // Implicit construction from string
        StyleTextOverflow implicitOverflow = "clip";
        string cssImplicit = implicitOverflow;   // "clip"
    }
}
```

---

*Revision Date: 2025-11-15*
