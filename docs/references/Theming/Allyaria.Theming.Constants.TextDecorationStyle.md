# Allyaria.Theming.Constants.TextDecorationStyle

`TextDecorationStyle` provides a strongly typed collection of constants representing standard CSS text-decoration-style
values.
It ensures consistent decorative styling across Allyaria components that apply underlines, overlines, or
strike-throughs.

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

* Each member is an immutable `AryStringValue` corresponding to a CSS `text-decoration-style` keyword.
* Commonly used in conjunction with `TextDecorationLine` and `TextDecorationColor` for full decoration customization.
* Theming presets can specify decoration styles for hover, focus, or disabled states.
* Allows consistent use of text decorations across typography variants.

## Members

| Name     | Type             | Description                                       |
|----------|------------------|---------------------------------------------------|
| `Dashed` | `AryStringValue` | Represents a *dashed* text decoration style.      |
| `Dotted` | `AryStringValue` | Represents a *dotted* text decoration style.      |
| `Double` | `AryStringValue` | Represents a *double-line* text decoration style. |
| `Solid`  | `AryStringValue` | Represents a *solid* text decoration style.       |
| `Wavy`   | `AryStringValue` | Represents a *wavy* text decoration style.        |

## Examples

### Minimal Example

```csharp
var style = TextDecorationStyle.Wavy;
```

### Expanded Example

```csharp
public string BuildTextDecorationCss(AryStringValue style)
{
    return $"text-decoration-style: {style};";
}

// Example usage:
var css = BuildTextDecorationCss(TextDecorationStyle.Dotted);
// "text-decoration-style: dotted;"
```

> *Rev Date: 2025-10-06*
