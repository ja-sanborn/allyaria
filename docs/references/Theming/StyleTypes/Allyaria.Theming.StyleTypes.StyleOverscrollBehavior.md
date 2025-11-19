# Allyaria.Theming.StyleTypes.StyleOverscrollBehavior

`StyleOverscrollBehavior` is a sealed style value record representing the CSS `overscroll-behavior`,
`overscroll-behavior-x`, and `overscroll-behavior-y` properties within the Allyaria theming system. It inherits from
`StyleValueBase` (and therefore implements `IStyleValue`) and provides a strongly typed wrapper over browser
scroll-chaining and overscroll-control behaviors.

## Summary

`StyleOverscrollBehavior` defines how scrolling behavior is handled when the user reaches scroll boundaries. Its nested
`Kind` enumeration exposes all allowed CSS overscroll behavior modes—`auto`, `contain`, and `none`. Each value maps to
the appropriate CSS keyword via `[Description]` attributes and is validated through the `StyleValueBase` constructor.
This type supports consistent, safe serialization to CSS, plus parsing, safe parsing, and implicit conversions.

## Constructors

`StyleOverscrollBehavior(Kind kind)` Creates a new style value using the provided overscroll behavior. The enum value is
resolved to its CSS keyword and validated by the `StyleValueBase` base class.

## Properties

| Name    | Type     | Description                                                                    |
|---------|----------|--------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS overscroll keyword (e.g., `"auto"`, `"contain"`, `"none"`). |

## Methods

| Name                                                           | Returns                   | Description                                                    |
|----------------------------------------------------------------|---------------------------|----------------------------------------------------------------|
| `Parse(string? value)`                                         | `StyleOverscrollBehavior` | Parses a CSS string into a strongly typed overscroll behavior. |
| `TryParse(string? value, out StyleOverscrollBehavior? result)` | `bool`                    | Attempts to parse without throwing; returns `true` on success. |
| `Equals(object? obj)`                                          | `bool`                    | Structural record equality.                                    |
| `Equals(StyleOverscrollBehavior? other)`                       | `bool`                    | Equality comparison with another instance.                     |
| `GetHashCode()`                                                | `int`                     | Hash code derived from record structure.                       |

## Nested Types

| Name   | Type   | Description                                           |
|--------|--------|-------------------------------------------------------|
| `Kind` | `enum` | Defines all supported CSS overscroll behavior values. |

### `StyleOverscrollBehavior.Kind` Members

| Name      | Description                                                                                       |
|-----------|---------------------------------------------------------------------------------------------------|
| `Auto`    | `"auto"` — Allows normal scroll chaining and browser overscroll gestures (e.g., pull-to-refresh). |
| `Contain` | `"contain"` — Prevents scroll chaining to parent containers but still allows browser gestures.    |
| `None`    | `"none"` — Fully disables scroll chaining and browser overscroll gestures.                        |

## Operators

| Operator                                                                     | Returns                   | Description                                                       |
|------------------------------------------------------------------------------|---------------------------|-------------------------------------------------------------------|
| `implicit operator StyleOverscrollBehavior(string? value)`                   | `StyleOverscrollBehavior` | Converts a CSS string to a `StyleOverscrollBehavior` via `Parse`. |
| `implicit operator string(StyleOverscrollBehavior? value)`                   | `string`                  | Converts the instance to its CSS keyword or an empty string.      |
| `operator ==(StyleOverscrollBehavior? left, StyleOverscrollBehavior? right)` | `bool`                    | Structural equality comparison.                                   |
| `operator !=(StyleOverscrollBehavior? left, StyleOverscrollBehavior? right)` | `bool`                    | Structural inequality.                                            |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and implicit string→`StyleOverscrollBehavior` conversion when the provided value does not match any
  valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` when the keyword contains invalid characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class OverscrollDemo
{
    public void Demo()
    {
        // Enum construction
        var contain = new StyleOverscrollBehavior(StyleOverscrollBehavior.Kind.Contain);
        string cssContain = contain.Value;         // "contain"

        // Parse from string
        var none = StyleOverscrollBehavior.Parse("none");
        string cssNone = none;                     // "none"

        // TryParse
        if (StyleOverscrollBehavior.TryParse("auto", out var auto))
        {
            string cssAuto = auto!.Value;          // "auto"
        }

        // Implicit conversion
        StyleOverscrollBehavior implicitContain = "contain";
        string cssImplicit = implicitContain;      // "contain"
    }
}
```

---

*Revision Date: 2025-11-15*
