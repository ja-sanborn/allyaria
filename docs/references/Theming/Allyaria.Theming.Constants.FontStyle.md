# Allyaria.Theming.Constants.FontStyle

`FontStyle` provides a strongly typed collection of constants representing CSS font-style values.
It allows consistent use of font style definitions across Allyaria’s theming and typography systems.

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

* Each constant is an immutable `AryStringValue` that maps directly to a CSS font-style keyword.
* Enables type-safe assignment and composition of font-related properties within `AllyariaTheme` typography definitions.
* Intended for use in style generation, component theming, and typography presets.

## Members

| Name      | Type             | Description                                    |
|-----------|------------------|------------------------------------------------|
| `Italic`  | `AryStringValue` | Represents the *italic* font style.            |
| `Normal`  | `AryStringValue` | Represents the *normal* (upright) font style.  |
| `Oblique` | `AryStringValue` | Represents the *oblique* (slanted) font style. |

## Examples

### Minimal Example

```csharp
var fontStyle = FontStyle.Italic;
```

### Expanded Example

```csharp
public string BuildFontStyleCss(AryStringValue style)
{
    return $"font-style: {style};";
}

// Example usage:
var css = BuildFontStyleCss(FontStyle.Oblique); 
// "font-style: oblique;"
```

> *Rev Date: 2025-10-06*
