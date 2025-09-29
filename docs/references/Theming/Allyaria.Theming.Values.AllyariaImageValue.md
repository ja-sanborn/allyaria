# Allyaria.Theming.Values.AllyariaImageValue

`AllyariaImageValue` is a strongly-typed CSS image wrapper with immutable value semantics. It normalizes any provided
input
into a canonical CSS `url("…")` token. If the input already contains a `url(...)` (even inside a longer CSS expression),
only the first `url(...)` is extracted; the rest is discarded. Otherwise, the raw input is validated, stripped of
optional
quotes, safely escaped, and wrapped as `url("…")`. It also supports safe scheme checking and convenience CSS generation
methods.

---

## Constructors

* `AllyariaImageValue(string value)`
  Creates a normalized CSS image token from `value`. Trims leading/trailing whitespace, rejects disallowed control
  characters, ensures safe schemes (`http`, `https`, `data`, `blob`), and blocks dangerous ones (like `javascript:` or
  `vbscript:`). Throws if the input is invalid.

---

## Properties

* `string Value` — The underlying normalized string (a canonical CSS `url("…")` token, inherited from `ValueBase`).

---

## Events

* *None*

---

## Methods

* `static AllyariaImageValue Parse(string value)` — Parses and normalizes `value`; throws on invalid input.

* `static bool TryParse(string value, out AllyariaImageValue? result)` — Attempts to parse; returns `true` and sets
  `result` on success, otherwise `false` with `result = null`.

* `string ToCss(string propertyName)` — Formats a CSS declaration using the current value.
  Returns `"propertyName:value;"` (with a lower-cased, trimmed property name). If `propertyName` is `null`/whitespace,
  returns just `Value`. *(inherited from `ValueBase`)*

* `string ToCssBackground(AllyariaColorValue backgroundColor, bool stretch = true)` — Builds CSS declarations for a
  background image with a contrast-enhancing overlay. Adds a semi-transparent dark or light overlay based on the
  background’s relative luminance and optionally expands into positioning and sizing rules when `stretch = true`.

* `string ToCssVarsBackground(string prefix, AllyariaColorValue backgroundColor, bool stretch = true)` — Same as
  `ToCssBackground` but generates CSS custom properties (`--prefixbackground-image`, etc.) instead of direct
  declarations.

* `int CompareTo(ValueBase other)` — Ordinal comparison by `Value`. *(inherited)*

* `bool Equals(ValueBase other)` / `override bool Equals(object? obj)` — Ordinal equality by `Value`. *(inherited)*

* `override int GetHashCode()` — Based on `Value`. *(inherited)*

* `override string ToString()` — Returns `Value`. *(inherited)*

---

## Operators

* `==`, `!=`, `<`, `<=`, `>`, `>=` — Ordinal comparison/equality by `Value` (inherited from `ValueBase`). Comparing
  different concrete `ValueBase` types throws.
* `implicit operator AllyariaImageValue(string)` — Converts a `string` to a normalized `AllyariaImageValue` (throws on
  invalid input).
* `implicit operator string(AllyariaImageValue)` — Extracts the underlying normalized `Value`.

---

## Exceptions

* `ArgumentException` — Thrown when constructing/parsing with `null`, empty, whitespace-only input, invalid control
  characters, unsupported or dangerous URI schemes (`javascript:`, `vbscript:`), or when comparing values of different
  concrete `ValueBase` types.
* `ArgumentNullException` — Thrown when implicitly converting a `null` `AllyariaImageValue` to string.
