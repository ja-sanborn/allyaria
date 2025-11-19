# Allyaria.Theming.StyleTypes.StyleJustify

`StyleJustify` is a sealed style value record representing CSS `justify-content`, `justify-items`, and `justify-self`
values within the Allyaria theming system. It inherits from `StyleValueBase` (implementing `IStyleValue`) and provides a
strongly typed wrapper around all official CSS justification keywords.

## Summary

`StyleJustify` is an immutable, validated style value used to control how items are positioned and distributed along the
main axis in flexbox and grid layouts. Each justification option is represented by the nested `Kind` enum, which maps to
the correct CSS keyword via `[Description]` attributes. The type supports safe CSS serialization, parsing, safe parsing
with `TryParse`, and ergonomic implicit conversions.

## Constructors

`StyleJustify(Kind kind)` Creates a new `StyleJustify` instance using the provided justification `kind`. The constructor
resolves the enum member to its CSS keyword string and validates it through `StyleValueBase`.

## Properties

| Name    | Type     | Description                                                                     |
|---------|----------|---------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS justification keyword (e.g., `"center"`, `"space-between"`). |

## Methods

| Name                                                | Returns        | Description                                                                          |
|-----------------------------------------------------|----------------|--------------------------------------------------------------------------------------|
| `Parse(string? value)`                              | `StyleJustify` | Parses a CSS justification string into a `StyleJustify` instance; throws if invalid. |
| `TryParse(string? value, out StyleJustify? result)` | `bool`         | Safely attempts to parse a justification string. Returns `true` on success.          |
| `Equals(object? obj)`                               | `bool`         | Determines structural equality.                                                      |
| `Equals(StyleJustify? other)`                       | `bool`         | Determines equality with another `StyleJustify` instance.                            |
| `GetHashCode()`                                     | `int`          | Returns a hash code based on structural equality.                                    |

## Nested Types

| Name   | Kind   | Description                                                                            |
|--------|--------|----------------------------------------------------------------------------------------|
| `Kind` | `enum` | Contains all officially supported CSS justification values for `justify-*` properties. |

### `StyleJustify.Kind` Members

| Name           | Description                                                                |
|----------------|----------------------------------------------------------------------------|
| `Center`       | `"center"` — Centers items along the main axis.                            |
| `End`          | `"end"` — Aligns items toward the end of the main axis.                    |
| `FlexEnd`      | `"flex-end"` — Aligns flex items to the main-axis end.                     |
| `FlexStart`    | `"flex-start"` — Aligns flex items to the main-axis start.                 |
| `Normal`       | `"normal"` — Applies standard alignment behavior.                          |
| `SafeCenter`   | `"safe center"` — Centers items but prevents overflow where possible.      |
| `SpaceAround`  | `"space-around"` — Distributes items with space around each.               |
| `SpaceBetween` | `"space-between"` — Items at start and end with even distribution between. |
| `SpaceEvenly`  | `"space-evenly"` — Equal spacing between and around all items.             |
| `Start`        | `"start"` — Aligns items toward the start of the main axis.                |
| `Stretch`      | `"stretch"` — Items stretch to fill available space.                       |
| `UnsafeCenter` | `"unsafe center"` — Centers items even if overflow may occur.              |

## Operators

| Operator                                               | Returns        | Description                                                    |
|--------------------------------------------------------|----------------|----------------------------------------------------------------|
| `implicit operator StyleJustify(string? value)`        | `StyleJustify` | Parses a justification keyword into a `StyleJustify` instance. |
| `implicit operator string(StyleJustify? value)`        | `string`       | Converts the instance to a CSS keyword (or an empty string).   |
| `operator ==(StyleJustify? left, StyleJustify? right)` | `bool`         | Tests two instances for equality.                              |
| `operator !=(StyleJustify? left, StyleJustify? right)` | `bool`         | Tests two instances for inequality.                            |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and the implicit conversion when the provided string does not correspond to a valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` when the resolved CSS keyword is invalid for output.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class JustifyDemo
{
    public void Demo()
    {
        // Create via enum
        var justifyCenter = new StyleJustify(StyleJustify.Kind.Center);
        string cssCenter = justifyCenter.Value; // "center"

        // Parse from string
        var justifyBetween = StyleJustify.Parse("space-between");
        string cssBetween = justifyBetween;     // "space-between"

        // TryParse
        if (StyleJustify.TryParse("flex-end", out var justifyFlexEnd))
        {
            string css = justifyFlexEnd!.Value; // "flex-end"
        }

        // Implicit
        StyleJustify implicitStyle = "safe center";
        string cssImplicit = implicitStyle;     // "safe center"
    }
}
```

---

*Revision Date: 2025-11-15*
