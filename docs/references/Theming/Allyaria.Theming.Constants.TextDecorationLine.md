# Allyaria.Theming.Constants.TextDecorationLine

`TextDecorationLine` provides a strongly typed collection of constants representing standard CSS text-decoration-line
values.
It supports Allyaria’s theming system for typography styling, ensuring consistent text decoration semantics across
components.

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

* Each constant is an immutable `AryStringValue` that maps directly to a valid CSS `text-decoration-line` value.
* Used primarily within typography, links, and stateful text components to manage emphasis or decoration changes (e.g.,
  hover underline).
* Theming and typography presets reference these constants for accessible and consistent text styling.

## Members

| Name          | Type             | Description                                  |
|---------------|------------------|----------------------------------------------|
| `LineThrough` | `AryStringValue` | Represents a *line-through* text decoration. |
| `None`        | `AryStringValue` | Represents no text decoration line.          |
| `Overline`    | `AryStringValue` | Represents an *overline* text decoration.    |
| `Underline`   | `AryStringValue` | Represents an *underline* text decoration.   |

## Examples

### Minimal Example

```csharp
var decoration = TextDecorationLine.Underline;
```

### Expanded Example

```csharp
public string BuildTextDecorationCss(AryStringValue line)
{
    return $"text-decoration-line: {line};";
}

// Example usage:
var css = BuildTextDecorationCss(TextDecorationLine.LineThrough);
// "text-decoration-line: line-through;"
```

> *Rev Date: 2025-10-06*
