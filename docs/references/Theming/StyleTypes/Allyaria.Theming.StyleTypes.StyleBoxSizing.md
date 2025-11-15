# Allyaria.Theming.StyleTypes.StyleBoxSizing

S`StyleBoxSizing` is a sealed style value record representing the CSS `box-sizing` property within the Allyaria theming
system. It inherits from `StyleValueBase` (which implements `IStyleValue`) and provides a strongly typed wrapper around
the official CSS `box-sizing` keywords.

## Summary

`StyleBoxSizing` is an immutable style value used to control how an element’s width and height are calculated—whether
they include padding and border (`border-box`) or are based purely on the content area (`content-box`). It uses a nested
`Kind` enumeration to provide type-safe options, validates the chosen value using `StyleValueBase`, and offers parsing
helpers and implicit conversions for streamlined usage in theme definitions.

## Constructors

`StyleBoxSizing(Kind kind)` Creates a new `StyleBoxSizing` instance for the given `Kind`, converting it to the
appropriate CSS string using the enum’s `[Description]` attribute, and validating via `StyleValueBase`.

## Properties

| Name    | Type     | Description                                                                         |
|---------|----------|-------------------------------------------------------------------------------------|
| `Value` | `string` | Gets the validated CSS `box-sizing` string (e.g., `"border-box"`, `"content-box"`). |

## Methods

| Name                                                  | Returns          | Description                                                                                                     |
|-------------------------------------------------------|------------------|-----------------------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                | `StyleBoxSizing` | Parses a CSS `box-sizing` string into a `StyleBoxSizing` instance by mapping it to a `Kind`; throws if invalid. |
| `TryParse(string? value, out StyleBoxSizing? result)` | `bool`           | Attempts to parse the string into a `StyleBoxSizing`. Outputs the parsed instance or null.                      |
| `Equals(object? obj)`                                 | `bool`           | Determines equality with another object (record structural equality).                                           |
| `Equals(StyleBoxSizing? other)`                       | `bool`           | Determines equality with another `StyleBoxSizing` instance.                                                     |
| `GetHashCode()`                                       | `int`            | Returns a hash code derived from the record’s structural equality semantics.                                    |

## Nested Types

| Name   | Kind   | Description                                         |
|--------|--------|-----------------------------------------------------|
| `Kind` | `enum` | Defines the valid `box-sizing` CSS property values. |

### `StyleBoxSizing.Kind` Members

| Name         | Description                                                                            |
|--------------|----------------------------------------------------------------------------------------|
| `BorderBox`  | `"border-box"` — Width and height include padding and border.                          |
| `ContentBox` | `"content-box"` — Width and height include only content, excluding padding and border. |
| `Inherit`    | `"inherit"` — Inherits `box-sizing` from the parent element.                           |

## Operators

| Operator                                                   | Returns          | Description                                                                |
|------------------------------------------------------------|------------------|----------------------------------------------------------------------------|
| `implicit operator StyleBoxSizing(string? value)`          | `StyleBoxSizing` | Parses the given string into a `StyleBoxSizing` using `Parse`.             |
| `implicit operator string(StyleBoxSizing? value)`          | `string`         | Returns the instance’s underlying CSS keyword, or an empty string if null. |
| `operator ==(StyleBoxSizing? left, StyleBoxSizing? right)` | `bool`           | Determines equality between two instances.                                 |
| `operator !=(StyleBoxSizing? left, StyleBoxSizing? right)` | `bool`           | Determines inequality.                                                     |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and the implicit `string → StyleBoxSizing` operator when the input string does not map to a valid
  `Kind`.

* **`AryArgumentException`**  
  Thrown by the base `StyleValueBase` constructor if the resolved CSS value contains invalid or unsafe characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class BoxSizingDemo
{
    public void Demo()
    {
        // Create via enum
        var borderBox = new StyleBoxSizing(StyleBoxSizing.Kind.BorderBox);
        string cssBorder = borderBox.Value;      // "border-box"

        // Parse from string
        var content = StyleBoxSizing.Parse("content-box");
        string cssContent = content;             // implicit => "content-box"

        // TryParse
        if (StyleBoxSizing.TryParse("inherit", out var inherited))
        {
            string cssInherit = inherited!.Value; // "inherit"
        }

        // Implicit creation
        StyleBoxSizing implicitBox = "border-box";
        string cssImplicit = implicitBox;        // "border-box"
    }
}
```

---

*Revision Date: 2025-11-15*
