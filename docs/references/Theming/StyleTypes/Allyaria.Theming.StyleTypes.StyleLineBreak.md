# Allyaria.Theming.StyleTypes.StyleLineBreak

`StyleLineBreak` is a sealed style value record representing the CSS `line-break` property within the Allyaria theming
system. It inherits from `StyleValueBase` (implementing `IStyleValue`) and provides a strongly typed wrapper around all
standard CSS line‐breaking behaviors.

## Summary

`StyleLineBreak` is an immutable, validated representation of the CSS `line-break` property. It uses a nested `Kind`
enumeration to expose every allowed CSS keyword, maps each value to its corresponding CSS representation using
`[Description]` attributes, validates it through `StyleValueBase`, and supports parsing, safe parsing (`TryParse`), and
implicit conversions.

## Constructors

`StyleLineBreak(Kind kind)` Creates a new `StyleLineBreak` instance representing the specified `line-break` behavior.
The constructor resolves the enum member’s CSS keyword and validates it using the `StyleValueBase` base class.

## Properties

| Name    | Type     | Description                                                                         |
|---------|----------|-------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `line-break` keyword (e.g., `"auto"`, `"strict"`, `"anywhere"`). |

## Methods

| Name                                                  | Returns          | Description                                                                         |
|-------------------------------------------------------|------------------|-------------------------------------------------------------------------------------|
| `Parse(string? value)`                                | `StyleLineBreak` | Parses a CSS `line-break` string into a strongly typed instance; throws if invalid. |
| `TryParse(string? value, out StyleLineBreak? result)` | `bool`           | Safely attempts to parse the input; outputs a new instance or `null`.               |
| `Equals(object? obj)`                                 | `bool`           | Determines structural equality.                                                     |
| `Equals(StyleLineBreak? other)`                       | `bool`           | Determines equality with another instance.                                          |
| `GetHashCode()`                                       | `int`            | Returns a hash code derived from record semantics.                                  |

## Nested Types

| Name   | Kind   | Description                                    |
|--------|--------|------------------------------------------------|
| `Kind` | `enum` | Defines all supported CSS `line-break` values. |

### `StyleLineBreak.Kind` Members

| Name       | Description                                                                                      |
|------------|--------------------------------------------------------------------------------------------------|
| `Anywhere` | `"anywhere"` — Allows line breaks between any two characters. Useful for long strings like URLs. |
| `Auto`     | `"auto"` — Uses default line‐breaking rules based on language/script.                            |
| `Loose`    | `"loose"` — Looser rules providing more break opportunities; helpful for East Asian typography.  |
| `Normal`   | `"normal"` — Standard line‐breaking behavior.                                                    |
| `Strict`   | `"strict"` — Most restrictive; only breaks where absolutely necessary.                           |

## Operators

| Operator                                                   | Returns          | Description                                                           |
|------------------------------------------------------------|------------------|-----------------------------------------------------------------------|
| `implicit operator StyleLineBreak(string? value)`          | `StyleLineBreak` | Parses the string using `Parse`.                                      |
| `implicit operator string(StyleLineBreak? value)`          | `string`         | Converts the instance to its CSS keyword, or an empty string if null. |
| `operator ==(StyleLineBreak? left, StyleLineBreak? right)` | `bool`           | Structural equality.                                                  |
| `operator !=(StyleLineBreak? left, StyleLineBreak? right)` | `bool`           | Structural inequality.                                                |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and implicit string→`StyleLineBreak` conversion when the string does not match any `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the CSS keyword contains invalid or unsafe characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class LineBreakDemo
{
    public void Demo()
    {
        // Enum-based construction
        var normal = new StyleLineBreak(StyleLineBreak.Kind.Normal);
        string cssNormal = normal.Value;      // "normal"

        // Parse from string
        var strict = StyleLineBreak.Parse("strict");
        string cssStrict = strict;            // "strict"

        // TryParse
        if (StyleLineBreak.TryParse("anywhere", out var anywhere))
        {
            string cssAnywhere = anywhere!.Value;   // "anywhere"
        }

        // Implicit from string
        StyleLineBreak loose = "loose";
        string cssLoose = loose;              // "loose"
    }
}
```

---

*Revision Date: 2025-11-15*
