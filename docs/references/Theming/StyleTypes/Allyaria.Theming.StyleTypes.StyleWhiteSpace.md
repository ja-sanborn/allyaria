# Allyaria.Theming.StyleTypes.StyleWhiteSpace

`StyleWhiteSpace` is a sealed style value record representing the CSS `white-space` property within the Allyaria theming
system. It inherits from `StyleValueBase` (and thus implements `IStyleValue`) and provides a strongly typed wrapper
around all valid whitespace-handling behaviors.

## Summary

`StyleWhiteSpace` is an immutable, validated representation of the CSS `white-space` property. Its nested `Kind` enum
exposes all officially supported whitespace-handling modes—collapsing, wrapping, preserving spaces, preserving line
breaks, and more. Each enum member maps to its canonical CSS value through `[Description]` attributes, validated by the
`StyleValueBase` constructor. The type supports parsing, safe parsing (`TryParse`), and implicit conversions.

## Constructors

`StyleWhiteSpace(Kind kind)` Creates a new instance using the specified whitespace-handling behavior. The enum is mapped
to its CSS string using its `[Description]` attribute and validated by the base class.

## Properties

| Name    | Type     | Description                                                                             |
|---------|----------|-----------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `white-space` keyword (e.g., `"pre"`, `"nowrap"`, `"break-spaces"`). |

## Methods

| Name                                                   | Returns           | Description                                                                          |
|--------------------------------------------------------|-------------------|--------------------------------------------------------------------------------------|
| `Parse(string? value)`                                 | `StyleWhiteSpace` | Parses a CSS `white-space` string into a strongly typed instance; throws if invalid. |
| `TryParse(string? value, out StyleWhiteSpace? result)` | `bool`            | Attempts to parse without throwing; returns `true` on success and assigns `result`.  |
| `Equals(object? obj)`                                  | `bool`            | Structural equality comparison.                                                      |
| `Equals(StyleWhiteSpace? other)`                       | `bool`            | Strongly typed structural equality check.                                            |
| `GetHashCode()`                                        | `int`             | Computes a structural hash code.                                                     |

## Nested Types

| Name   | Type   | Description                                         |
|--------|--------|-----------------------------------------------------|
| `Kind` | `enum` | Defines all valid CSS `white-space` keyword values. |

### `StyleWhiteSpace.Kind` Members

| Name             | Description                                                                                                                  |
|------------------|------------------------------------------------------------------------------------------------------------------------------|
| `BreakSpaces`    | `"break-spaces"` — Preserves spaces, tabs, and line breaks; allows wrapping only at break points; trailing spaces preserved. |
| `Collapse`       | `"collapse"` — Collapses consecutive spaces and tabs into a single space; normal line-breaking rules apply.                  |
| `Normal`         | `"normal"` — Standard behavior: collapse whitespace, allow wrapping at break points.                                         |
| `Nowrap`         | `"nowrap"` — Collapses whitespace but prevents wrapping onto multiple lines.                                                 |
| `Pre`            | `"pre"` — Preserves whitespace exactly; no automatic wrapping.                                                               |
| `PreLine`        | `"pre-line"` — Collapses whitespace but preserves line breaks.                                                               |
| `PreserveNowrap` | `"preserve nowrap"` — Preserves whitespace but prevents wrapping (equivalent to legacy `pre nowrap`).                        |
| `PreWrap`        | `"pre-wrap"` — Preserves whitespace but allows wrapping as needed.                                                           |

## Operators

| Operator                                                     | Returns           | Description                                                       |
|--------------------------------------------------------------|-------------------|-------------------------------------------------------------------|
| `implicit operator StyleWhiteSpace(string? value)`           | `StyleWhiteSpace` | Converts the string into a typed instance via `Parse`.            |
| `implicit operator string(StyleWhiteSpace? value)`           | `string`          | Converts the instance to its CSS keyword or returns `""` if null. |
| `operator ==(StyleWhiteSpace? left, StyleWhiteSpace? right)` | `bool`            | Structural equality comparison.                                   |
| `operator !=(StyleWhiteSpace? left, StyleWhiteSpace? right)` | `bool`            | Structural inequality comparison.                                 |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown when `Parse` (and implicit string→`StyleWhiteSpace`) receives a value that does not correspond to a valid
  `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the resulting CSS keyword contains invalid or unsafe characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class WhiteSpaceDemo
{
    public void Demo()
    {
        // Create from enum
        var ws = new StyleWhiteSpace(StyleWhiteSpace.Kind.PreWrap);
        string css = ws.Value;                 // "pre-wrap"

        // Parse from string
        var nowrap = StyleWhiteSpace.Parse("nowrap");
        string cssNowrap = nowrap;             // "nowrap"

        // TryParse
        if (StyleWhiteSpace.TryParse("break-spaces", out var parsed))
        {
            string cssParsed = parsed!.Value;  // "break-spaces"
        }

        // Implicit
        StyleWhiteSpace implicitValue = "pre";
        string cssImplicit = implicitValue;    // "pre"
    }
}
```

---

*Revision Date: 2025-11-15*
