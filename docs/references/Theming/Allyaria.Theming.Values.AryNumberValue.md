# Allyaria.Theming.Values.AryNumberValue

`AryNumberValue` represents a **normalized numeric value** with an optional CSS-compatible length unit.
It validates, parses, and canonicalizes input strings such as `"12px"`, `"-3.5 rem"`, `"100%"`, or `"auto"`, while
exposing both numeric and unit components in a strongly typed, culture-invariant way.
All comparison, equality, and CSS serialization behaviors are inherited from `ValueBase`.

## Constructors

`AryNumberValue(string value)`
Parses and normalizes the input, producing a canonical CSS-safe numeric representation.
For `"auto"`, the numeric value is set to `0` and the unit to `null`.

* Exceptions:

    * `AryArgumentException` — if `value` is null, whitespace, non-numeric, infinite, or uses an unsupported unit token.

## Properties

| Name         | Type           | Description                                                                                         |
|--------------|----------------|-----------------------------------------------------------------------------------------------------|
| `Value`      | `string`       | The canonical normalized value (e.g., `"12px"`, `"100%"`, `"auto"`).                                |
| `Number`     | `double`       | The parsed numeric value, or `0` if input was `"auto"`.                                             |
| `LengthUnit` | `LengthUnits?` | The parsed unit (e.g., `LengthUnits.Px`, `LengthUnits.Percent`), or `null` if unitless or `"auto"`. |

## Methods

| Name                                                        | Returns          | Description                                                                              |
|-------------------------------------------------------------|------------------|------------------------------------------------------------------------------------------|
| `static Parse(string value)`                                | `AryNumberValue` | Parses and normalizes the input string, throwing on failure.                             |
| `static TryParse(string value, out AryNumberValue? result)` | `bool`           | Safely attempts to parse the input. Returns `false` when invalid.                        |
| `ToCss(string propertyName)`                                | `string`         | Returns a CSS declaration (e.g., `"width:12px;"`), or raw value if no property is given. |
| `CompareTo(ValueBase? other)`                               | `int`            | Ordinal comparison with another `ValueBase` of the same type.                            |
| `Equals(ValueBase? other)`                                  | `bool`           | Ordinal equality comparison (type-safe).                                                 |

## Operators

| Operator                           | Returns          | Description                                      |
|------------------------------------|------------------|--------------------------------------------------|
| `==`, `!=`                         | `bool`           | Ordinal equality comparison (same type only).    |
| `>`, `<`, `>=`, `<=`               | `bool`           | Lexicographical ordering (same type only).       |
| `implicit string → AryNumberValue` | `AryNumberValue` | Converts a string to a normalized numeric value. |
| `implicit AryNumberValue → string` | `string`         | Extracts the canonical normalized value.         |

## Exceptions

* `AryArgumentException` —

    * Input is null, whitespace, or contains control characters.
    * Invalid numeric or unit format.
    * Infinite or `NaN` numeric values.
    * Unsupported or unrecognized CSS unit.

## Behavior Notes

* Parsing is **locale-independent** (uses `CultureInfo.InvariantCulture`).
* Recognized units include all entries from `LengthUnits` (e.g., `px`, `em`, `rem`, `%`, `vh`, `vw`, `cqw`, etc.).
* Unit lookup is case-insensitive; canonicalized units retain lowercase as per enum description.
* `"auto"` is treated as a valid special keyword — results in `Value = "auto"`, `Number = 0`, `LengthUnit = null`.
* Numbers are formatted with precision rules avoiding scientific notation for typical ranges (`0.0001–1e6`).
* Supports sign and fractional forms: `"-.5em"`, `"+10px"`, `"0.25rem"`, etc.

## Examples

### Minimal Example

```csharp
var num = new AryNumberValue("12px");
Console.WriteLine(num.Value);   // "12px"
Console.WriteLine(num.Number);  // 12
Console.WriteLine(num.LengthUnit); // LengthUnits.Pixel
```

### Expanded Example

```csharp
var percentage = new AryNumberValue("  50 % ");
Console.WriteLine(percentage.Value); // "50%"
Console.WriteLine(percentage.Number); // 50
Console.WriteLine(percentage.LengthUnit); // LengthUnits.Percent

var unitless = new AryNumberValue("42");
Console.WriteLine(unitless.Value); // "42"

var css = num.ToCss("margin-top"); 
// "margin-top:12px;"
```

> *Rev Date: 2025-10-06*
