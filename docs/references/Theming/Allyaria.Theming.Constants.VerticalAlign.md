# Allyaria.Theming.Constants.VerticalAlign

`VerticalAlign` provides a strongly typed collection of constants representing standard CSS vertical alignment values.
It is primarily used in Allyaria typography, inline layout components, and theming systems to ensure alignment
consistency across different rendering contexts.

## Constructors

*Static class — no constructors.*

## Properties

*None*

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* Each constant is an immutable `AryStringValue` representing a CSS `vertical-align` keyword.
* Used for aligning inline or inline-block elements relative to their line box or parent element.
* Supports typographic alignment options like `sub`, `super`, and text-relative positions.
* Helps maintain accessibility and visual harmony within mixed-content layouts (e.g., icons with text).

## Members

| Name         | Type             | Description                                                         |
|--------------|------------------|---------------------------------------------------------------------|
| `Baseline`   | `AryStringValue` | Aligns the element’s baseline with the parent’s baseline (default). |
| `Bottom`     | `AryStringValue` | Aligns to the bottom of the element.                                |
| `Middle`     | `AryStringValue` | Aligns the element vertically in the middle of the line.            |
| `Sub`        | `AryStringValue` | Aligns the element as a subscript.                                  |
| `Super`      | `AryStringValue` | Aligns the element as a superscript.                                |
| `TextBottom` | `AryStringValue` | Aligns to the bottom of the parent element’s font.                  |
| `TextTop`    | `AryStringValue` | Aligns to the top of the parent element’s font.                     |
| `Top`        | `AryStringValue` | Aligns to the top of the element.                                   |

## Examples

### Minimal Example

```csharp
var align = VerticalAlign.Middle;
```

### Expanded Example

```csharp
public string BuildVerticalAlignCss(AryStringValue align)
{
    return $"vertical-align: {align};";
}

// Example usage:
var css = BuildVerticalAlignCss(VerticalAlign.Super);
// "vertical-align: super;"
```

> *Rev Date: 2025-10-06*
