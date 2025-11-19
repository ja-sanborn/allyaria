# Allyaria.Theming.StyleTypes.StyleScrollBehavior

`StyleScrollBehavior` is a sealed style value record representing the CSS `scroll-behavior` property within the Allyaria
theming system. It inherits from `StyleValueBase` (implementing `IStyleValue`) and provides a strongly typed wrapper
around browser scrolling animation behavior.

## Summary

`StyleScrollBehavior` provides an immutable, validated representation of CSS scroll behavior. Its nested `Kind` enum
exposes the two official CSS keywords—`auto` and `smooth`—mapping each to its CSS string via `[Description]` attributes.
The record supports parsing, safe parsing with `TryParse`, and ergonomic implicit conversions, while benefiting from the
validation provided by `StyleValueBase`.

## Constructors

`StyleScrollBehavior(Kind kind)` Creates a new instance with the specified scroll behavior mode. The enum value is
translated into the official CSS keyword and validated through `StyleValueBase`.

## Properties

| Name    | Type     | Description                                                                |
|---------|----------|----------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `scroll-behavior` keyword (e.g., `"auto"`, `"smooth"`). |

## Methods

| Name                                                       | Returns               | Description                                                                     |
|------------------------------------------------------------|-----------------------|---------------------------------------------------------------------------------|
| `Parse(string? value)`                                     | `StyleScrollBehavior` | Converts a CSS string into a `StyleScrollBehavior` instance; throws if invalid. |
| `TryParse(string? value, out StyleScrollBehavior? result)` | `bool`                | Attempts to parse without throwing; returns `true` if successful.               |
| `Equals(object? obj)`                                      | `bool`                | Structural equality comparison.                                                 |
| `Equals(StyleScrollBehavior? other)`                       | `bool`                | Equality comparison with another instance.                                      |
| `GetHashCode()`                                            | `int`                 | Computes a hash code derived from record structural semantics.                  |

## Nested Types

| Name   | Kind   | Description                                                 |
|--------|--------|-------------------------------------------------------------|
| `Kind` | `enum` | Defines the supported CSS `scroll-behavior` keyword values. |

### `StyleScrollBehavior.Kind` Members

| Name     | Description                                                                            |
|----------|----------------------------------------------------------------------------------------|
| `Auto`   | `"auto"` — Uses instant scrolling with no animation.                                   |
| `Smooth` | `"smooth"` — Enables animated smooth scrolling for programmatic and anchor navigation. |

## Operators

| Operator                                                             | Returns               | Description                                                            |
|----------------------------------------------------------------------|-----------------------|------------------------------------------------------------------------|
| `implicit operator StyleScrollBehavior(string? value)`               | `StyleScrollBehavior` | Parses the provided string into a `StyleScrollBehavior` using `Parse`. |
| `implicit operator string(StyleScrollBehavior? value)`               | `string`              | Converts the value to its CSS string, or `""` if null.                 |
| `operator ==(StyleScrollBehavior? left, StyleScrollBehavior? right)` | `bool`                | Determines equality.                                                   |
| `operator !=(StyleScrollBehavior? left, StyleScrollBehavior? right)` | `bool`                | Determines inequality.                                                 |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and implicit string→`StyleScrollBehavior` conversion when the string does not match any valid
  `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the underlying CSS keyword is malformed or invalid.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class ScrollBehaviorDemo
{
    public void Demo()
    {
        // Enum-based construction
        var smooth = new StyleScrollBehavior(StyleScrollBehavior.Kind.Smooth);
        string cssSmooth = smooth.Value;          // "smooth"

        // Parse from string
        var auto = StyleScrollBehavior.Parse("auto");
        string cssAuto = auto;                    // "auto"

        // TryParse safe usage
        if (StyleScrollBehavior.TryParse("smooth", out var parsed))
        {
            string result = parsed!.Value;        // "smooth"
        }

        // Implicit conversion
        StyleScrollBehavior implicitSmooth = "smooth";
        string cssImplicit = implicitSmooth;      // "smooth"
    }
}
```

---

*Revision Date: 2025-11-15*
