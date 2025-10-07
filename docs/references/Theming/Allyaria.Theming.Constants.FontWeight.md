# Allyaria.Theming.Constants.FontWeight

`FontWeight` provides a strongly typed set of constants for CSS font-weight values used within Allyaria’s theming and
typography systems.
Each member exposes an immutable `AryStringValue` corresponding to a valid CSS weight keyword or numeric weight value.

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

* `FontWeight` constants provide consistent font weight references for Allyaria typography tokens.
* Both keyword-based (`normal`, `bold`, `bolder`, `lighter`) and numeric (`100`–`900`) weights are available.
* These values can be directly serialized into CSS when generating dynamic styles from the theming engine.
* The naming convention (`Bold1`, `Bold2`, etc.) corresponds to the CSS numeric weights (100–900).

## Members

| Name      | Type             | Description                                                  |
|-----------|------------------|--------------------------------------------------------------|
| `Bold`    | `AryStringValue` | Represents bold font weight (`bold`).                        |
| `Bold1`   | `AryStringValue` | Represents weight `100`.                                     |
| `Bold2`   | `AryStringValue` | Represents weight `200`.                                     |
| `Bold3`   | `AryStringValue` | Represents weight `300`.                                     |
| `Bold4`   | `AryStringValue` | Represents weight `400`.                                     |
| `Bold5`   | `AryStringValue` | Represents weight `500`.                                     |
| `Bold6`   | `AryStringValue` | Represents weight `600`.                                     |
| `Bold7`   | `AryStringValue` | Represents weight `700`.                                     |
| `Bold8`   | `AryStringValue` | Represents weight `800`.                                     |
| `Bold9`   | `AryStringValue` | Represents weight `900`.                                     |
| `Bolder`  | `AryStringValue` | Represents the *bolder* font weight relative to its parent.  |
| `Lighter` | `AryStringValue` | Represents the *lighter* font weight relative to its parent. |
| `Normal`  | `AryStringValue` | Represents the *normal* (regular) font weight.               |

## Examples

### Minimal Example

```csharp
var weight = FontWeight.Bold;
```

### Expanded Example

```csharp
public string BuildFontWeightCss(AryStringValue weight)
{
    return $"font-weight: {weight};";
}

// Example usage:
var css = BuildFontWeightCss(FontWeight.Bold6); 
// "font-weight: 600;"
```

> *Rev Date: 2025-10-06*
