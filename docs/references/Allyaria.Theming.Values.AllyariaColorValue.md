# Allyaria.Theming.Palette.AllyariaColorValue

`AllyariaColorValue` represents a framework-agnostic color value with CSS-oriented parsing and formatting, immutable
value
semantics, and total ordering based on the canonical uppercase `#RRGGBBAA` form. It supports parsing from CSS-style
strings,
CSS Web color names, and Material Design palette names, and provides conversions between RGBA and HSVA models.

---

## Constructors

* `AllyariaColorValue(string value)`  
  Parses a color from hex (`#RGB`, `#RGBA`, `#RRGGBB`, `#RRGGBBAA`), `rgb()`/`rgba()`, `hsv()`/`hsva()`, CSS Web names,
  or Material Design names. Throws an exception if unrecognized.

* `static AllyariaColorValue FromHsva(double h, double s, double v, double a = 1.0)`  
  Creates a color from HSVA channels. Throws `ArgumentOutOfRangeException` if any component lies outside its valid
  range.

* `static AllyariaColorValue FromRgba(byte r, byte g, byte b, double a = 1.0)`  
  Creates a color from RGBA channels. Throws `ArgumentOutOfRangeException` if alpha is outside `[0..1]`.

---

## Properties

* `byte R` — Red channel (0–255).
* `byte G` — Green channel (0–255).
* `byte B` — Blue channel (0–255).
* `double A` — Alpha channel (0–1).

* `double H` — Hue in degrees (0–360), derived from RGB.
* `double S` — Saturation percentage (0–100), derived from RGB.
* `double V` — Value (brightness) percentage (0–100), derived from RGB.

* `string HexRgb` — Uppercase `#RRGGBB` string.
* `string HexRgba` — Uppercase `#RRGGBBAA` string (alpha included).
* `string Rgb` — `"rgb(r, g, b)"` string.
* `string Rgba` — `"rgba(r, g, b, a)"` string with alpha in `[0–1]`.
* `string Hsv` — `"hsv(H, S%, V%)"` string using invariant culture.
* `string Hsva` — `"hsva(H, S%, V%, A)"` string using invariant culture.
* `override string Value` — The canonical string form (`#RRGGBBAA`).

---

## Events

* *None*

---

## Methods

* `static AllyariaColorValue Parse(string value)` — Parses a color or throws if invalid.
* `static bool TryParse(string value, out AllyariaColorValue? result)` — Attempts to parse a color, returns `true` if
  successful.

* `int CompareTo(ValueBase other)` — Compares two values by their canonical string (from `ValueBase`).
* `bool Equals(ValueBase other)` — Equality check inherited from `ValueBase`.
* `override bool Equals(object? obj)` — Equality against another object.
* `override int GetHashCode()` — Hash code based on canonical string.

* `string ToCss(string? propertyName)` — Converts to CSS declaration string (`"property: #RRGGBBAA;"`) or returns value
  if no property.
* `override string ToString()` — Returns canonical `#RRGGBBAA` form.

---

## Operators

* `==`, `!=` — Equality/inequality by value.
* `<`, `<=`, `>`, `>=` — Ordering by canonical string (from `ValueBase`).
* `implicit operator AllyariaColorValue(string)` — Parses from a color string.
* `implicit operator string(AllyariaColorValue)` — Converts to canonical string.
