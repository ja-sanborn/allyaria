# Allyaria.Theming.StyleTypes.StyleFontStyle

`StyleFontStyle` is a sealed style value record representing the CSS `font-style` property within the Allyaria theming
system. It inherits from `StyleValueBase` (implementing `IStyleValue`) and provides a strongly typed interface around
the standard CSS typographic style options: `normal`, `italic`, and `oblique`.

## Summary

`StyleFontStyle` is an immutable, fully validated wrapper for expressing CSS `font-style` values. Its nested `Kind` enum
maps directly to official CSS keywords via `[Description]` attributes. The type ensures safe CSS output through
`StyleValueBase` validation and supports parsing, safe parsing (`TryParse`), and implicit string conversions for
ergonomic use in theme and typography definitions.

## Constructors

`StyleFontStyle(Kind kind)` Creates a new `StyleFontStyle` instance using the given `Kind` value. The constructor
resolves the CSS keyword string from the enum and validates it via the `StyleValueBase` base class.

## Properties

| Name    | Type     | Description                                                             |
|---------|----------|-------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `font-style` keyword (e.g., `"italic"`, `"normal"`). |

## Methods

| Name                                                  | Returns          | Description                                                                        |
|-------------------------------------------------------|------------------|------------------------------------------------------------------------------------|
| `Parse(string? value)`                                | `StyleFontStyle` | Parses a CSS `font-style` keyword into a `StyleFontStyle`; throws if invalid.      |
| `TryParse(string? value, out StyleFontStyle? result)` | `bool`           | Attempts to parse the input value; sets `result` and returns success as a boolean. |
| `Equals(object? obj)`                                 | `bool`           | Determines structural equality.                                                    |
| `Equals(StyleFontStyle? other)`                       | `bool`           | Determines equality with another `StyleFontStyle` instance.                        |
| `GetHashCode()`                                       | `int`            | Computes a hash code based on record equality.                                     |

## Nested Types

| Name   | Kind   | Description                                            |
|--------|--------|--------------------------------------------------------|
| `Kind` | `enum` | Defines the supported CSS `font-style` keyword values. |

### `StyleFontStyle.Kind` Members

| Name      | Description                                                                     |
|-----------|---------------------------------------------------------------------------------|
| `Italic`  | `"italic"` — Renders text using an italic font face.                            |
| `Normal`  | `"normal"` — Renders text normally without slanting.                            |
| `Oblique` | `"oblique"` — Renders text slanted, used when no true italic face is available. |

## Operators

| Operator                                                   | Returns          | Description                                                          |
|------------------------------------------------------------|------------------|----------------------------------------------------------------------|
| `implicit operator StyleFontStyle(string? value)`          | `StyleFontStyle` | Parses the input string into a `StyleFontStyle` using `Parse`.       |
| `implicit operator string(StyleFontStyle? value)`          | `string`         | Converts the instance to its CSS keyword or an empty string if null. |
| `operator ==(StyleFontStyle? left, StyleFontStyle? right)` | `bool`           | Compares two instances for equality.                                 |
| `operator !=(StyleFontStyle? left, StyleFontStyle? right)` | `bool`           | Compares two instances for inequality.                               |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and the implicit string→`StyleFontStyle` conversion when the input does not match any valid enum
  member.

* **`AryArgumentException`**  
  Thrown by the `StyleValueBase` constructor if the resolved CSS keyword contains invalid characters.

* **`ArgumentException`**  
  May be surfaced indirectly when invalid color or enum conversions occur within extension utilities.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class FontStyleDemo
{
    public void Demo()
    {
        // Direct enum creation
        var italic = new StyleFontStyle(StyleFontStyle.Kind.Italic);
        string cssItalic = italic.Value; // "italic"

        // Parse from string
        var oblique = StyleFontStyle.Parse("oblique");
        string cssOblique = oblique;     // "oblique"

        // TryParse for safe handling
        if (StyleFontStyle.TryParse("normal", out var normal))
        {
            string cssNormal = normal!.Value; // "normal"
        }

        // Implicit from string
        StyleFontStyle implicitStyle = "italic";
        string cssImplicit = implicitStyle;   // "italic"
    }
}
```

---

*Revision Date: 2025-11-15*
