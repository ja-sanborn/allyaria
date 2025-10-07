# Allyaria.Theming.Values.AryColorValue

`AryColorValue` represents a **framework-agnostic color type** with full CSS parsing, validation, and formatting
support.
It supports hex (`#RGB`, `#RGBA`, `#RRGGBB`, `#RRGGBBAA`), RGB(A), HSV(A), CSS Web color names, and Material Design
color tokens — producing a canonical uppercase `#RRGGBBAA` value.
All comparison, equality, and CSS serialization behaviors are inherited from `ValueBase`.

## Constructors

`AryColorValue(string value)`
Parses a color from a hex, `rgb()`, `rgba()`, `hsv()`, `hsva()`, CSS Web color name, or Material color name.

* Exceptions:

    * `AryArgumentException` — Invalid or unrecognized color string.

`private AryColorValue(byte r, byte g, byte b, double a = 1.0)`
Initializes a color from validated RGB(A) channels.

`private AryColorValue(double h, double s, double v, double a = 1.0)`
Initializes a color from HSV(A) channels, converting internally to RGB(A).

## Properties

| Name      | Type     | Description                                                                     |
|-----------|----------|---------------------------------------------------------------------------------|
| `Value`   | `string` | Canonical uppercase `#RRGGBBAA` string used for equality and CSS serialization. |
| `R`       | `byte`   | Red channel (0–255).                                                            |
| `G`       | `byte`   | Green channel (0–255).                                                          |
| `B`       | `byte`   | Blue channel (0–255).                                                           |
| `A`       | `double` | Alpha (opacity) value in range `[0–1]`.                                         |
| `HexRgb`  | `string` | Uppercase `#RRGGBB` representation.                                             |
| `HexRgba` | `string` | Uppercase `#RRGGBBAA` representation.                                           |
| `Rgb`     | `string` | CSS `rgb(r, g, b)` form.                                                        |
| `Rgba`    | `string` | CSS `rgba(r, g, b, a)` form (alpha shown in [0–1]).                             |
| `H`       | `double` | Hue in degrees `[0–360]` (computed from RGB).                                   |
| `S`       | `double` | Saturation in percent `[0–100]` (computed from RGB).                            |
| `V`       | `double` | Brightness/value in percent `[0–100]` (computed from RGB).                      |
| `Hsv`     | `string` | CSS `hsv(H, S%, V%)` form.                                                      |
| `Hsva`    | `string` | CSS `hsva(H, S%, V%, A)` form.                                                  |

## Methods

| Name                                                                          | Returns         | Description                                                   |
|-------------------------------------------------------------------------------|-----------------|---------------------------------------------------------------|
| `static AryColorValue Parse(string value)`                                    | `AryColorValue` | Parses a color string (throws on invalid input).              |
| `static bool TryParse(string value, out AryColorValue? result)`               | `bool`          | Attempts to parse a color safely; returns `false` on failure. |
| `static AryColorValue FromRgba(byte r, byte g, byte b, double a = 1.0)`       | `AryColorValue` | Constructs directly from RGBA channels.                       |
| `static AryColorValue FromHsva(double h, double s, double v, double a = 1.0)` | `AryColorValue` | Constructs from HSVA channels, converting internally to RGB.  |
| `ToCss(string propertyName)`                                                  | `string`        | Formats into a CSS declaration, e.g. `"color:#FF00FF;"`.      |
| `CompareTo(ValueBase? other)`                                                 | `int`           | Ordinal comparison with another `ValueBase` of the same type. |
| `Equals(ValueBase? other)`                                                    | `bool`          | Ordinal equality comparison (type-safe).                      |

## Operators

| Operator                          | Returns         | Description                                   |
|-----------------------------------|-----------------|-----------------------------------------------|
| `==`, `!=`                        | `bool`          | Ordinal equality comparison (same type only). |
| `>`, `<`, `>=`, `<=`              | `bool`          | Lexicographical ordering (same type only).    |
| `implicit string → AryColorValue` | `AryColorValue` | Converts from a color string to an instance.  |
| `implicit AryColorValue → string` | `string`        | Extracts the canonical `#RRGGBBAA` form.      |

## Exceptions

* `AryArgumentException` — Thrown for invalid input format, unsupported color name, or channel out of range.

## Behavior Notes

* Uses **culture-invariant parsing** for numeric values.
* Automatically supports all CSS and Material color naming conventions.
* Equality and comparison use the canonical uppercase `#RRGGBBAA` string.
* Alpha (`A`) is always normalized and clamped to `[0,1]`.
* All conversions preserve color fidelity and deterministic formatting.
* The class is immutable — all operations produce new instances.

## Examples

### Minimal Example

```csharp
var c1 = new AryColorValue("#FF00FF");
Console.WriteLine(c1.Value);  // #FF00FFFF
Console.WriteLine(c1.Rgba);   // rgba(255, 0, 255, 1)
```

### Expanded Example

```csharp
var c2 = AryColorValue.Parse("hsv(300, 100%, 100%)");
Console.WriteLine(c2.HexRgb);  // #FF00FF
Console.WriteLine(c2.Hsva);    // hsva(300, 100%, 100%, 1)

var named = new AryColorValue("Deep Purple 200");
Console.WriteLine(named.Value); // e.g. #B39DDBFF

var css = c2.ToCss("background-color");
// "background-color:#FF00FFFF;"
```

> *Rev Date: 2025-10-06*
