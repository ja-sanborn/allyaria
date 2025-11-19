# Allyaria.Theming.StyleTypes.StyleFontWeight

`StyleFontWeight` is a sealed style value record representing the CSS `font-weight` property within the Allyaria theming
system. It inherits from `StyleValueBase` (implementing `IStyleValue`) and provides a complete, strongly typed wrapper
over CSS keyword and numeric font-weight values.

## Summary

`StyleFontWeight` is an immutable, validated representation of CSS font weights—including keyword forms (`normal`,
`bold`, `lighter`, `bolder`) and numeric values (`100`–`900`). Numeric inputs such as `"700"` are automatically mapped
to the corresponding enum member (`Weight700`) before validation. It supports full parsing, safe parsing with
`TryParse`, and ergonomic implicit conversions.

## Constructors

`StyleFontWeight(Kind kind)` Creates a new `StyleFontWeight` with the specified `Kind`. The enum value is mapped to its
CSS string via `[Description]`, and the resulting string is validated through the `StyleValueBase` constructor.

## Properties

| Name    | Type     | Description                                                        |
|---------|----------|--------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `font-weight` string (e.g., `"bold"`, `"700"`). |

## Methods

| Name                                                   | Returns           | Description                                                                                    |
|--------------------------------------------------------|-------------------|------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                 | `StyleFontWeight` | Parses CSS font-weight values including `"bold"`, `"normal"`, or numeric strings like `"700"`. |
| `TryParse(string? value, out StyleFontWeight? result)` | `bool`            | Attempts to parse the string into a `StyleFontWeight`.                                         |
| `Equals(object? obj)`                                  | `bool`            | Determines equality with another object.                                                       |
| `Equals(StyleFontWeight? other)`                       | `bool`            | Determines equality with another `StyleFontWeight` instance.                                   |
| `GetHashCode()`                                        | `int`             | Returns a value-based hash code.                                                               |

## Nested Types

| Name   | Kind   | Description                                                                        |
|--------|--------|------------------------------------------------------------------------------------|
| `Kind` | `enum` | Defines all valid CSS `font-weight` options, including keyword and numeric values. |

### `StyleFontWeight.Kind` Members

| Name        | Description                                     |
|-------------|-------------------------------------------------|
| `Bold`      | `"bold"` — Typically equivalent to numeric 700. |
| `Bolder`    | `"bolder"` — Thicker than the parent’s weight.  |
| `Lighter`   | `"lighter"` — Thinner than the parent’s weight. |
| `Normal`    | `"normal"` — Typically numeric 400.             |
| `Weight100` | `"100"` — Thin.                                 |
| `Weight200` | `"200"` — Extra-light.                          |
| `Weight300` | `"300"` — Light.                                |
| `Weight400` | `"400"` — Normal.                               |
| `Weight500` | `"500"` — Medium.                               |
| `Weight600` | `"600"` — Semi-bold.                            |
| `Weight700` | `"700"` — Bold.                                 |
| `Weight800` | `"800"` — Extra-bold.                           |
| `Weight900` | `"900"` — Black.                                |

Numeric strings are automatically mapped to their corresponding enum values (`"700"` → `Weight700`).

## Operators

| Operator                                                     | Returns           | Description                                                |
|--------------------------------------------------------------|-------------------|------------------------------------------------------------|
| `implicit operator StyleFontWeight(string? value)`           | `StyleFontWeight` | Parses the string using `Parse`.                           |
| `implicit operator string(StyleFontWeight? value)`           | `string`          | Converts the instance to its CSS keyword or numeric value. |
| `operator ==(StyleFontWeight? left, StyleFontWeight? right)` | `bool`            | Compares two instances.                                    |
| `operator !=(StyleFontWeight? left, StyleFontWeight? right)` | `bool`            | Compares two instances for inequality.                     |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown when parsing a value that does not correspond to any valid `Kind` member.

* **`AryArgumentException`**  
  Thrown by `StyleValueBase` if the resolved CSS string contains invalid characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class FontWeightDemo
{
    public void Demo()
    {
        // Create from enum
        var bold = new StyleFontWeight(StyleFontWeight.Kind.Bold);
        string cssBold = bold.Value;      // "bold"

        // Parse numeric string
        var weight700 = StyleFontWeight.Parse("700");
        string css700 = weight700;        // "700"

        // Parse keyword
        var normal = StyleFontWeight.Parse("normal");
        string cssNormal = normal.Value;  // "normal"

        // TryParse
        if (StyleFontWeight.TryParse("300", out var light))
        {
            string cssLight = light!.Value; // "300"
        }

        // Implicit
        StyleFontWeight medium = "500";
        string cssMedium = medium;        // "500"
    }
}
```

---

*Revision Date: 2025-11-15*
