# Allyaria.Theming.Values.AllyariaColorValue

`AllyariaColorValue` is a sealed, immutable theming type that represents a `framework-agnostic color value`.
It *inherits from `ValueBase`* (ordinal, string-based equality/ordering and CSS emission). Parsing supports CSS
hex/functions, CSS Web color names, and Material palette names. It exposes RGBA channels and derived HSVA properties,
provides conversions, and participates in value comparison and CSS formatting via its base semantics.

## Constructors

`AllyariaColorValue(string value)`
Parses a color from one of: `#RGB`, `#RGBA`, `#RRGGBB`, `#RRGGBBAA`, `rgb(r,g,b)`, `rgba(r,g,b,a)`, `hsv(H,S%,V%)`,
`hsva(H,S%,V%,A)`, `CSS Web color name`, or `Material color name`.

* Exceptions:

    * `AllyariaArgumentException` — when parsing fails or input is invalid.

## Properties

| Name      | Type     | Description                                          |
|-----------|----------|------------------------------------------------------|
| `A`       | `double` | Alpha channel in `[0..1]`.                           |
| `R`       | `byte`   | Red channel `[0..255]`.                              |
| `G`       | `byte`   | Green channel `[0..255]`.                            |
| `B`       | `byte`   | Blue channel `[0..255]`.                             |
| `H`       | `double` | Hue (degrees) derived from RGB in `[0..360]`.        |
| `S`       | `double` | Saturation (%) derived from RGB in `[0..100]`.       |
| `V`       | `double` | Value/brightness (%) derived from RGB in `[0..100]`. |
| `HexRgb`  | `string` | Uppercase `#RRGGBB`.                                 |
| `HexRgba` | `string` | Uppercase `canonical` `#RRGGBBAA`.                   |
| `Hsv`     | `string` | `hsv(H, S%, V%)` using invariant culture.            |
| `Hsva`    | `string` | `hsva(H, S%, V%, A)` using invariant culture.        |
| `Rgb`     | `string` | `rgb(r, g, b)`.                                      |
| `Rgba`    | `string` | `rgba(r, g, b, a)` using invariant culture.          |
| `Value`   | `string` | Canonical string for ordering/equality — `HexRgba`.  |

## Methods

| Name                                                     | Returns              | Description                                                                                                            |
|----------------------------------------------------------|----------------------|------------------------------------------------------------------------------------------------------------------------|
| `FromHsva(double h, double s, double v, double a = 1.0)` | `AllyariaColorValue` | Factory: clamps/normalizes HSVA, converts to RGB, returns a new color.                                                 |
| `FromRgba(byte r, byte g, byte b, double a = 1.0)`       | `AllyariaColorValue` | Factory: clamps alpha, returns a new color.                                                                            |
| `Parse(string value)`                                    | `AllyariaColorValue` | Parses or throws `AllyariaArgumentException`.                                                                          |
| `TryParse(string value, out AllyariaColorValue? result)` | `bool`               | Attempts parse; `false` on failure.                                                                                    |
| `Compare(ValueBase? left, ValueBase? right)`             | `int`                | Ordinal comparison by `Value`. Throws if operand runtime types differ when both non-null.                              |
| `CompareTo(ValueBase? other)`                            | `int`                | Ordinal comparison with another value; throws when runtime types differ.                                               |
| `Equals(object? obj)`                                    | `bool`               | Equality by canonical `Value`.                                                                                         |
| `Equals(ValueBase? other)`                               | `bool`               | Equality by canonical `Value`; `true` only if same runtime type and same `Value`.                                      |
| `GetHashCode()`                                          | `int`                | Ordinal hash of `Value`.                                                                                               |
| `ToCss(string propertyName)`                             | `string`             | Formats `"property:value;"` (e.g., `"color:#FF0000FF;"`), or returns raw `Value` if `propertyName` is null/whitespace. |
| `ToString()`                                             | `string`             | Returns `Value` (`#RRGGBBAA`).                                                                                         |

## Operators

| Operator                                | Returns              | Description                                                                          |
|-----------------------------------------|----------------------|--------------------------------------------------------------------------------------|
| `implicit string -> AllyariaColorValue` | `AllyariaColorValue` | Parses string into a color (throws on invalid input).                                |
| `implicit AllyariaColorValue -> string` | `string`             | Returns canonical `#RRGGBBAA`.                                                       |
| `==`, `!=`                              | `bool`               | Ordinal equality by `Value`. Works between two `AllyariaColorValue` instances.       |
| `>`, `<`, `>=`, `<=`                    | `bool`               | Ordinal ordering by `Value`. *Comparing different `ValueBase`-derived types throws.* |

## Events

*None*

## Exceptions

* `AllyariaArgumentException` — invalid/unsupported color input, out-of-range channel, or attempting to compare two
  `different` `ValueBase`-derived types with comparison operators or `Compare*`.
* `ArgumentException` — invalid hex digit when parsing hex strings.

## Behavior Notes

* `Canonical form`: All ordering/equality is based on uppercase `#RRGGBBAA` (`Value`).
* `Parsing sources`: CSS hex forms, `rgb()/rgba()`, `hsv()/hsva()`, `CSS Web color names`, and
  `Material Design palette names` (case-insensitive; whitespace/dashes/underscores ignored for Material names).
* `Normalization`: Hue wraps to `[0..360)`, S/V clamp to `[0..100]`, alpha clamps to `[0..1]`.
* `Culture`: All numeric parsing/formatting uses `InvariantCulture`.
* `CSS emission`: Use `ToCss("color")` to emit inline declarations; property name is lower-cased/trimmed.
* `Type safety`: Comparison with a `different` `ValueBase` subtype (e.g., `AllyariaNumberValue`) throws.

## Examples

### Minimal

```csharp
using Allyaria.Theming.Values;

var red = new AllyariaColorValue("#FF0000");
Console.WriteLine(red.HexRgba);       // "#FF0000FF"
Console.WriteLine(red.ToCss("color")); // "color:#FF0000FF;"
```

### Expanded

```csharp
using Allyaria.Theming.Values;
using Allyaria.Theming.Contracts;

var c1 = AllyariaColorValue.Parse("DeepPurple500");
var c2 = AllyariaColorValue.Parse("rgb(255,0,0)");
var c3 = AllyariaColorValue.FromHsva(200, 100, 50);

// Equality / ordering (ordinal by #RRGGBBAA)
bool same = c1 == c1;                       // true
int cmp  = ValueBase.Compare(c1, c2);       // works with AllyariaColorValue operands
bool gt  = c1 > c2;                         // inherited operator

// CSS emission
string cssColor = c2.ToCss("color");        // "color:#FF0000FF;"
string raw      = c3.ToString();            // "#??????FF" (canonical)

// Safe parsing
if (AllyariaColorValue.TryParse("hsva(210,50%,40%,0.8)", out var parsed))
{
    Console.WriteLine(parsed!.Rgba);        // "rgba(r, g, b, 0.8)"
}
```

> *Rev Date: 2025-10-01*
