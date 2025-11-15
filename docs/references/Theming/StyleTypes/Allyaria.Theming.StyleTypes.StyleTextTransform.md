# Allyaria.Theming.StyleTypes.StyleTextTransform

`StyleTextTransform` is a sealed style value record representing the CSS `text-transform` property within the Allyaria
theming system. It inherits from `StyleValueBase` (and therefore implements `IStyleValue`) and provides a strongly typed
wrapper over all standard CSS text-casing transformations.

## Summary

`StyleTextTransform` is an immutable, validated style value used to control how text casing is transformed before
rendering. Its nested `Kind` enum exposes the complete set of official CSS text-transform behaviors. Each enum member
maps to its canonical CSS keyword via `[Description]` and is validated through the `StyleValueBase` constructor. The
type provides parsing, safe parsing (`TryParse`), and implicit conversions to ensure developer-friendly usage.

## Constructors

`StyleTextTransform(Kind kind)` Initializes a new instance representing the specified text-transform behavior. The enum
value is resolved to its CSS keyword and validated by `StyleValueBase`.

## Properties

| Name    | Type     | Description                                                                        |
|---------|----------|------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `text-transform` keyword (e.g., `"uppercase"`, `"capitalize"`). |

## Methods

| Name                                                      | Returns              | Description                                                                   |
|-----------------------------------------------------------|----------------------|-------------------------------------------------------------------------------|
| `Parse(string? value)`                                    | `StyleTextTransform` | Parses a CSS text-transform keyword into a typed instance; throws if invalid. |
| `TryParse(string? value, out StyleTextTransform? result)` | `bool`               | Attempts parsing without throwing; returns `true` if successful.              |
| `Equals(object? obj)`                                     | `bool`               | Structural equality comparison.                                               |
| `Equals(StyleTextTransform? other)`                       | `bool`               | Strongly typed structural equality.                                           |
| `GetHashCode()`                                           | `int`                | Returns a structural hash code.                                               |

## Nested Types

| Name   | Type   | Description                                                |
|--------|--------|------------------------------------------------------------|
| `Kind` | `enum` | Defines all supported CSS `text-transform` keyword values. |

### `StyleTextTransform.Kind` Members

| Name         | Description                                                            |
|--------------|------------------------------------------------------------------------|
| `Capitalize` | `"capitalize"` — Capitalizes the first letter of each word.            |
| `Lowercase`  | `"lowercase"` — Converts all characters to lowercase.                  |
| `None`       | `"none"` — Disables case transformation; text is rendered as authored. |
| `Uppercase`  | `"uppercase"` — Converts all characters to uppercase.                  |

## Operators

| Operator                                                           | Returns              | Description                                               |
|--------------------------------------------------------------------|----------------------|-----------------------------------------------------------|
| `implicit operator StyleTextTransform(string? value)`              | `StyleTextTransform` | Converts a string into a typed value via `Parse`.         |
| `implicit operator string(StyleTextTransform? value)`              | `string`             | Converts the instance to its CSS keyword or `""` if null. |
| `operator ==(StyleTextTransform? left, StyleTextTransform? right)` | `bool`               | Structural equality.                                      |
| `operator !=(StyleTextTransform? left, StyleTextTransform? right)` | `bool`               | Structural inequality.                                    |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown when the provided string cannot be parsed into a valid `Kind`.

* **`AryArgumentException`**  
  May be thrown indirectly by `StyleValueBase` if the CSS keyword contains invalid characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class TextTransformDemo
{
    public void Demo()
    {
        // Construct from enum
        var upper = new StyleTextTransform(StyleTextTransform.Kind.Uppercase);
        string cssUpper = upper.Value;          // "uppercase"

        // Parse from string
        var lower = StyleTextTransform.Parse("lowercase");
        string cssLower = lower;               // "lowercase"

        // TryParse
        if (StyleTextTransform.TryParse("capitalize", out var cap))
        {
            string cssCap = cap!.Value;        // "capitalize"
        }

        // Implicit from string
        StyleTextTransform none = "none";
        string cssNone = none;                 // "none"
    }
}
```

---

*Revision Date: 2025-11-15*
