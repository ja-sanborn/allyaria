# Allyaria.Theming.StyleTypes.StyleWordBreak

`StyleWordBreak` is a sealed style value record representing the CSS `word-break` property within the Allyaria theming
system. It inherits from `StyleValueBase` (and therefore implements `IStyleValue`) and provides a strongly typed wrapper
around the valid CSS word-breaking behaviors.

## Summary

`StyleWordBreak` is an immutable, validated CSS value describing how text may break within words when it overflows its
container. Its nested `Kind` enum exposes all officially supported CSS `word-break` modes—`break-all`, `break-word`,
`keep-all`, and `normal`—mapped to their canonical CSS keywords through `[Description]` attributes. The type supports
parsing, safe parsing (`TryParse`), implicit conversions, and safe CSS serialization through `StyleValueBase`.

## Constructors

`StyleWordBreak(Kind kind)` Creates a new instance representing the specified word-break behavior. The enum member is
mapped to its CSS keyword string and validated by the `StyleValueBase` constructor.

## Properties

| Name    | Type     | Description                                                                |
|---------|----------|----------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `word-break` keyword (e.g., `"break-all"`, `"normal"`). |

## Methods

| Name                                                  | Returns          | Description                                                                                                            |
|-------------------------------------------------------|------------------|------------------------------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                | `StyleWordBreak` | Parses a CSS keyword string into a strongly typed instance; throws if the value does not correspond to a valid `Kind`. |
| `TryParse(string? value, out StyleWordBreak? result)` | `bool`           | Attempts to parse without throwing; assigns a typed instance or `null`, and returns success as a boolean.              |
| `Equals(object? obj)`                                 | `bool`           | Structural equality comparison based on record semantics.                                                              |
| `Equals(StyleWordBreak? other)`                       | `bool`           | Strongly typed equality comparison.                                                                                    |
| `GetHashCode()`                                       | `int`            | Returns a hash code derived from structural record semantics.                                                          |

## Nested Types

| Name   | Type   | Description                                        |
|--------|--------|----------------------------------------------------|
| `Kind` | `enum` | Defines all valid CSS `word-break` keyword values. |

### `StyleWordBreak.Kind` Members

| Name        | Description                                                                                                                            |
|-------------|----------------------------------------------------------------------------------------------------------------------------------------|
| `BreakAll`  | `"break-all"` — Allows breaking within any word if necessary to prevent overflow; ideal for long strings without natural break points. |
| `BreakWord` | `"break-word"` — Similar to normal breaking, but permits breaking long unbreakable words to avoid overflow.                            |
| `KeepAll`   | `"keep-all"` — Prevents breaking within words in CJK scripts; keeps words intact unless absolutely required.                           |
| `Normal`    | `"normal"` — Uses the default line-breaking rules for the document’s writing system.                                                   |

## Operators

| Operator                                                   | Returns          | Description                                                |
|------------------------------------------------------------|------------------|------------------------------------------------------------|
| `implicit operator StyleWordBreak(string? value)`          | `StyleWordBreak` | Parses the string using `Parse`.                           |
| `implicit operator string(StyleWordBreak? value)`          | `string`         | Converts the instance to its CSS keyword, or `""` if null. |
| `operator ==(StyleWordBreak? left, StyleWordBreak? right)` | `bool`           | Structural equality comparison.                            |
| `operator !=(StyleWordBreak? left, StyleWordBreak? right)` | `bool`           | Structural inequality comparison.                          |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and implicit string→`StyleWordBreak` conversion when the provided value does not match any valid
  `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the resolved CSS keyword contains invalid or forbidden characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class WordBreakDemo
{
    public void Demo()
    {
        // Enum-based creation
        var breakAll = new StyleWordBreak(StyleWordBreak.Kind.BreakAll);
        string cssBreakAll = breakAll.Value;      // "break-all"

        // Parse from string
        var keepAll = StyleWordBreak.Parse("keep-all");
        string cssKeepAll = keepAll;             // "keep-all"

        // TryParse
        if (StyleWordBreak.TryParse("break-word", out var breakWord))
        {
            string cssBreakWord = breakWord!.Value; // "break-word"
        }

        // Implicit conversion
        StyleWordBreak implicitNormal = "normal";
        string cssNormal = implicitNormal;        // "normal"
    }
}
```

---

*Revision Date: 2025-11-15*
