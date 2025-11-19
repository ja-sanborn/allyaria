# Allyaria.Theming.StyleTypes.StyleBorderOutlineStyle

`StyleBorderOutlineStyle` is a sealed style value record representing a CSS `border-style` or `outline-style` value
within the Allyaria theming system. It inherits from `StyleValueBase` (which implements `IStyleValue`) and provides a
strongly typed wrapper around the full set of standard CSS line-style keywords.

## Summary

`StyleBorderOutlineStyle` is an immutable, validated style value type used to express CSS border and outline styles such
as `solid`, `dashed`, `groove`, and others. By exposing a `Kind` enum—including each official CSS keyword—it enables
fully typed styling, input validation, safe serialization to CSS, and convenient parsing and implicit conversions.

## Constructors

`StyleBorderOutlineStyle(Kind kind)` Initializes a new instance using the specified style `kind`. The constructor
resolves the associated CSS keyword via `[Description]` attributes and validates the resulting string using the
`StyleValueBase` base class.

## Properties

| Name    | Type     | Description                                                                                      |
|---------|----------|--------------------------------------------------------------------------------------------------|
| `Value` | `string` | Gets the validated, normalized CSS style string for this instance (e.g., `"solid"`, `"dotted"`). |

## Methods

| Name                                                           | Returns                   | Description                                                                                                                         |
|----------------------------------------------------------------|---------------------------|-------------------------------------------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                         | `StyleBorderOutlineStyle` | Parses a CSS `border-style` / `outline-style` string into a `StyleBorderOutlineStyle` by mapping to a `Kind`; throws if invalid.    |
| `TryParse(string? value, out StyleBorderOutlineStyle? result)` | `bool`                    | Attempts to parse the string into a `StyleBorderOutlineStyle`. Assigns the parsed value or `null` and returns success as a boolean. |
| `Equals(object? obj)`                                          | `bool`                    | Determines whether the current instance is equal to another object (record structural equality).                                    |
| `Equals(StyleBorderOutlineStyle? other)`                       | `bool`                    | Determines whether the current instance is equal to another `StyleBorderOutlineStyle` instance.                                     |
| `GetHashCode()`                                                | `int`                     | Returns a hash code based on the record’s value equality semantics.                                                                 |

## Nested Types

| Name   | Kind   | Description                                                        |
|--------|--------|--------------------------------------------------------------------|
| `Kind` | `enum` | Defines all supported CSS `border-style` / `outline-style` values. |

### `StyleBorderOutlineStyle.Kind` Members

| Name     | Description                                                 |
|----------|-------------------------------------------------------------|
| `Dashed` | CSS keyword `"dashed"` — A dashed line style.               |
| `Dotted` | CSS keyword `"dotted"` — A dotted line style.               |
| `Double` | CSS keyword `"double"` — A double-line style.               |
| `Groove` | CSS keyword `"groove"` — A carved, 3D grooved border style. |
| `Inset`  | CSS keyword `"inset"` — A sunken, inset 3D border style.    |
| `None`   | CSS keyword `"none"` — Indicates no border or outline.      |
| `Outset` | CSS keyword `"outset"` — A raised, outset 3D border style.  |
| `Ridge`  | CSS keyword `"ridge"` — A raised, ridged 3D border style.   |
| `Solid`  | CSS keyword `"solid"` — A single solid line.                |

## Operators

| Operator                                                                     | Returns                   | Description                                                                      |
|------------------------------------------------------------------------------|---------------------------|----------------------------------------------------------------------------------|
| `implicit operator StyleBorderOutlineStyle(string? value)`                   | `StyleBorderOutlineStyle` | Parses the string into a `StyleBorderOutlineStyle` using `Parse`.                |
| `implicit operator string(StyleBorderOutlineStyle? value)`                   | `string`                  | Returns the CSS keyword represented by the instance, or an empty string if null. |
| `operator ==(StyleBorderOutlineStyle? left, StyleBorderOutlineStyle? right)` | `bool`                    | Determines whether two instances are equal (record structural equality).         |
| `operator !=(StyleBorderOutlineStyle? left, StyleBorderOutlineStyle? right)` | `bool`                    | Determines whether two instances differ.                                         |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by the base `StyleValueBase` constructor when the resolved CSS keyword contains invalid characters.

* **`AryArgumentException`**  
  Thrown by `Parse` (and thus by the implicit string→`StyleBorderOutlineStyle` conversion) when the input does not
  correspond to a valid `Kind` value.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class BorderStyleDemo
{
    public void Demo()
    {
        // Create via enum
        var solid = new StyleBorderOutlineStyle(StyleBorderOutlineStyle.Kind.Solid);
        string cssSolid = solid.Value;            // "solid"

        // Parse from string
        var dotted = StyleBorderOutlineStyle.Parse("dotted");
        string cssDotted = dotted;                // implicit to string => "dotted"

        // TryParse with graceful fallback
        if (StyleBorderOutlineStyle.TryParse("groove", out var groove))
        {
            string cssGroove = groove!.Value;     // "groove"
        }

        // Implicit construction from string
        StyleBorderOutlineStyle dashed = "dashed";
        string cssDashed = dashed;                // "dashed"
    }
}
```

---

*Revision Date: 2025-11-15*
