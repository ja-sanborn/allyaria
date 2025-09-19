# Allyaria.Theming.AllyariaColor

`AllyariaColor` represents a framework-agnostic color value with CSS-oriented parsing and formatting, immutable value
semantics, and ordering based on the canonical uppercase `#RRGGBBAA` form. It supports parsing from CSS-style strings,
web color names, and Material Design colors, and provides conversions between RGBA and HSVA models.

---

## Constructors

* `AllyariaColor(string value)`  
  Parses a color from hex (`#RGB`, `#RGBA`, `#RRGGBB`, `#RRGGBBAA`), `rgb()`/`rgba()`, `hsv()`/`hsva()`, CSS web names,
  or Material Design names. Throws an exception if unrecognized.

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
* `string HexRgba` — Uppercase `#RRGGBBAA` string.
* `string Rgb` — `"rgb(r, g, b)"` string.
* `string Rgba` — `"rgba(r, g, b, a)"` string with alpha in [0–1].
* `string Hsv` — `"hsv(H, S%, V%)"` string.
* `string Hsva` — `"hsva(H, S%, V%, A)"` string.

---

## Events

* *None*

---

## Methods

* `int CompareTo(AllyariaColor other)` — Compares two colors by their canonical `#RRGGBBAA` form.
* `bool Equals(AllyariaColor other)` — Checks equality by RGBA values (alpha rounded to byte).
* `override bool Equals(object? obj)` — Checks equality against another object.
* `static AllyariaColor FromHsva(double h, double s, double v, double a = 1.0)` — Creates a color from HSVA channels,
  with clamping on ranges.
* `static AllyariaColor FromRgba(byte r, byte g, byte b, double a = 1.0)` — Creates a color from RGBA channels, with
  clamping on alpha.
* `override int GetHashCode()` — Hash code based on RGBA.
* `AllyariaColor HoverColor()` — Returns a hover-friendly variant (lighten if value < 50, otherwise darken).
* `AllyariaColor ShiftColor(double percent)` — Adjusts brightness (−100 to +100).
* `string ToCss(string? name = null)` — Converts to CSS declaration, default `"color: #RRGGBBAA;"`.
* `override string ToString()` — Returns canonical `#RRGGBBAA` form.

---

## Operators

* `==`, `!=` — Equality/inequality (by RGBA).
* `<`, `<=`, `>`, `>=` — Ordering by `#RRGGBBAA`.
* `implicit operator AllyariaColor(string)` — Parses from a color string.
* `implicit operator string(AllyariaColor)` — Converts to canonical string.
