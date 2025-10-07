# Allyaria.Theming.Constants.BorderStyle

`BorderStyle` provides a strongly typed collection of constants representing standard CSS border styles.
It serves as a centralized, type-safe source for defining border patterns in Allyaria’s theming and style generation
systems.

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

* Each property exposes an `AryStringValue` instance containing the canonical CSS keyword.
* The constants are immutable and may be used directly in theme definitions or style builders.
* Ensures consistency between C#-defined themes and rendered CSS border styles.
* Typically consumed by the `AllyariaTheme` engine when constructing component outlines or borders.

## Members

| Name     | Type             | Description                            |
|----------|------------------|----------------------------------------|
| `Dashed` | `AryStringValue` | Represents a dashed border style.      |
| `Dotted` | `AryStringValue` | Represents a dotted border style.      |
| `Double` | `AryStringValue` | Represents a double-line border style. |
| `Groove` | `AryStringValue` | Represents a groove border style.      |
| `Hidden` | `AryStringValue` | Represents a hidden border style.      |
| `Inset`  | `AryStringValue` | Represents an inset border style.      |
| `None`   | `AryStringValue` | Represents no border.                  |
| `Outset` | `AryStringValue` | Represents an outset border style.     |
| `Ridge`  | `AryStringValue` | Represents a ridge border style.       |
| `Solid`  | `AryStringValue` | Represents a solid border style.       |

## Examples

### Minimal Example

```csharp
var border = BorderStyle.Solid;
```

### Expanded Example

```csharp
public string BuildBorderCss(AryStringValue style)
{
    return $"border: 1px {style} #000;";
}

// Example usage
var css = BuildBorderCss(BorderStyle.Dashed); // "border: 1px dashed #000;"
```

> *Rev Date: 2025-10-06*
