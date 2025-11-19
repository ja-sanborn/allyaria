# Allyaria.Theming.StyleTypes.StyleLength

`StyleLength` is a sealed style value record representing a CSS length within the Allyaria theming system. It inherits
from `StyleValueBase` (implementing `IStyleValue`) and provides full parsing, normalization, validation, and unit
extraction for CSS length syntax such as `12px`, `1.5rem`, `50%`, `0`, or `.25em`.

## Summary

`StyleLength` converts CSS length strings into a normalized, validated representation. It supports:

* Numeric values with or without units
* Nearly all CSS length units via `LengthUnits`
* Automatic extraction of numeric value and unit
* Normalization through regex parsing and unit-token mapping
* Implicit conversion to and from strings
* Safe parsing via `TryParse`

This makes it a foundational component for margin, padding, border, and layout-related styling in the theming engine.

## Constructors

`StyleLength(string value)` Creates a new instance from a raw CSS length string. If the string is non-empty, the
constructor validates and parses it into a numeric component and optional `LengthUnits` value. Throws if parsing fails.

## Properties

| Name         | Type           | Description                                                                       |
|--------------|----------------|-----------------------------------------------------------------------------------|
| `LengthUnit` | `LengthUnits?` | The parsed unit (e.g., `LengthUnits.Pixel`), or `null` if the value was unitless. |
| `Number`     | `double`       | The parsed numeric value (default `0.0` if parsing failed or value was empty).    |
| `Value`      | `string`       | Inherited from `StyleValueBase`. The raw, normalized CSS length string.           |

## Methods

| Name                                               | Returns       | Description                                                                  |
|----------------------------------------------------|---------------|------------------------------------------------------------------------------|
| `Parse(string? value)`                             | `StyleLength` | Parses a CSS length string (or empty string if `null`). Throws if not valid. |
| `TryParse(string? value, out StyleLength? result)` | `bool`        | Attempts to parse; returns `true` if valid and assigns the result.           |
| `Equals(object? obj)`                              | `bool`        | Structural equality comparison.                                              |
| `Equals(StyleLength? other)`                       | `bool`        | Compares this instance to another `StyleLength`.                             |
| `GetHashCode()`                                    | `int`         | Returns a hash code derived from record structure.                           |

## Operators

| Operator                                             | Returns       | Description                                                  |
|------------------------------------------------------|---------------|--------------------------------------------------------------|
| `implicit operator StyleLength(string? value)`       | `StyleLength` | Converts the string into a validated `StyleLength`.          |
| `implicit operator string(StyleLength? value)`       | `string`      | Converts the instance into its CSS string (or empty string). |
| `operator ==(StyleLength? left, StyleLength? right)` | `bool`        | Structural equality.                                         |
| `operator !=(StyleLength? left, StyleLength? right)` | `bool`        | Structural inequality.                                       |

## Events

*None*

## Exceptions

* **`ArgumentException`**  
  Thrown when the supplied length string cannot be parsed into a numeric/unit combination.

* **`AryArgumentException`**  
  May be thrown indirectly by `StyleValueBase` if the final CSS value contains forbidden characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;
using Allyaria.Theming.Enumerations;

public class StyleLengthDemo
{
    public void Demo()
    {
        // Basic usage
        StyleLength px = "16px";
        double numberPx = px.Number;              // 16
        LengthUnits? unitPx = px.LengthUnit;      // LengthUnits.Pixel

        // Unitless value
        StyleLength unitless = "42";
        double number = unitless.Number;          // 42
        var unit = unitless.LengthUnit;           // null

        // Percentage
        StyleLength pct = "50%";
        double pctNum = pct.Number;               // 50
        var pctUnit = pct.LengthUnit;             // LengthUnits.Percent

        // Parse method
        var rem = StyleLength.Parse("1.5rem");

        // TryParse gracefully handles invalid values
        if (StyleLength.TryParse("invalid!", out var result))
        {
            // valid
        }
        else
        {
            // invalid
        }

        // Convert back to CSS
        string css = px;                           // "16px"
    }
}
```

---

*Revision Date: 2025-11-15*
