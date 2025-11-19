# Allyaria.Theming.StyleTypes.StyleTextWrapStyle

`StyleTextWrapStyle` is a sealed style value record representing the CSS `text-wrap-style` property within the Allyaria
theming system. It inherits from `StyleValueBase` (and therefore implements `IStyleValue`) and provides a strongly typed
wrapper over all valid CSS text-wrapping strategies used by browsers to determine how lines should wrap within a block
container.

## Summary

`StyleTextWrapStyle` is an immutable, validated CSS style value describing how text should wrap in multi-line contexts.
Its nested `Kind` enum exposes all officially supported `text-wrap-style` modes—`auto`, `balance`, `pretty`, and
`stable`—each mapped to its canonical CSS keyword via `[Description]` attributes. The type supports parsing, safe
parsing (`TryParse`), and implicit conversions, making it simple and reliable to use throughout the Allyaria theming
engine.

## Constructors

`StyleTextWrapStyle(Kind kind)` Creates a new instance representing the specified text wrap behavior. The enum member is
converted to its CSS keyword via `[Description]` and validated by the `StyleValueBase` superclass.

## Properties

| Name    | Type     | Description                                                                   |
|---------|----------|-------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `text-wrap-style` keyword (e.g., `"balance"`, `"pretty"`). |

## Methods

| Name                                                      | Returns              | Description                                                                                                   |
|-----------------------------------------------------------|----------------------|---------------------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                    | `StyleTextWrapStyle` | Parses a CSS `text-wrap-style` string into a strongly typed instance. Throws if invalid.                      |
| `TryParse(string? value, out StyleTextWrapStyle? result)` | `bool`               | Attempts to parse without throwing; outputs a typed instance or `null` and returns whether parsing succeeded. |
| `Equals(object? obj)`                                     | `bool`               | Structural equality comparison (record semantics).                                                            |
| `Equals(StyleTextWrapStyle? other)`                       | `bool`               | Strongly typed equality comparison.                                                                           |
| `GetHashCode()`                                           | `int`                | Returns a structural hash code.                                                                               |

## Nested Types

| Name   | Type   | Description                                             |
|--------|--------|---------------------------------------------------------|
| `Kind` | `enum` | Defines all valid CSS `text-wrap-style` keyword values. |

### `StyleTextWrapStyle.Kind` Members

| Name      | Description                                                                                    |
|-----------|------------------------------------------------------------------------------------------------|
| `Auto`    | `"auto"` — Uses the browser’s default line-breaking and wrapping rules.                        |
| `Balance` | `"balance"` — Attempts to distribute text evenly across wrapped lines.                         |
| `Pretty`  | `"pretty"` — Adjusts wrapping for aesthetic appeal, allowing slight variation in line lengths. |
| `Stable`  | `"stable"` — Preserves line-breaking behavior for consistency across content changes.          |

## Operators

| Operator                                                           | Returns              | Description                                                             |
|--------------------------------------------------------------------|----------------------|-------------------------------------------------------------------------|
| `implicit operator StyleTextWrapStyle(string? value)`              | `StyleTextWrapStyle` | Parses the input string using `Parse`.                                  |
| `implicit operator string(StyleTextWrapStyle? value)`              | `string`             | Returns the CSS keyword represented by the instance, or `""` if `null`. |
| `operator ==(StyleTextWrapStyle? left, StyleTextWrapStyle? right)` | `bool`               | Structural equality comparison.                                         |
| `operator !=(StyleTextWrapStyle? left, StyleTextWrapStyle? right)` | `bool`               | Structural inequality.                                                  |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown when `Parse` (and therefore the implicit string → `StyleTextWrapStyle` conversion) receives a value that does
  not correspond to a valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the CSS keyword contains invalid or unsafe characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class TextWrapStyleDemo
{
    public void Demo()
    {
        // Construct from enum
        var pretty = new StyleTextWrapStyle(StyleTextWrapStyle.Kind.Pretty);
        string cssPretty = pretty.Value;              // "pretty"

        // Parse from string
        var balanced = StyleTextWrapStyle.Parse("balance");
        string cssBalanced = balanced;                // "balance"

        // TryParse safe usage
        if (StyleTextWrapStyle.TryParse("stable", out var stable))
        {
            string cssStable = stable!.Value;         // "stable"
        }

        // Implicit conversion from string
        StyleTextWrapStyle auto = "auto";
        string cssAuto = auto;                        // "auto"
    }
}
```

---

*Revision Date: 2025-11-15*
