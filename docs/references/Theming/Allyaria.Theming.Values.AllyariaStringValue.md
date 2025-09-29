# Allyaria.Theming.Values.AllyariaStringValue

`AllyariaStringValue` is a normalized theming string wrapper with immutable value semantics. It trims input, rejects
`null`/empty/whitespace, and rejects any control characters. It leverages `ValueBase` for ordinal comparison/equality
and CSS
declaration formatting (e.g., `"property:value;"`). It is ideal for safely carrying CSS token values (sizes, keywords,
etc.)
through theming pipelines.

---

## Constructors

* `AllyariaStringValue(string value)`
  Creates a normalized value from `value`. Trims leading/trailing whitespace, rejects control characters, and throws if
  `value` is `null`, empty, or whitespace-only.

---

## Properties

* `string Value` — The underlying normalized string (inherited from `ValueBase`).

---

## Events

* *None*

---

## Methods

* `static AllyariaStringValue Parse(string value)` — Parses and normalizes `value`; throws on invalid input.

* `static bool TryParse(string value, out AllyariaStringValue? result)` — Attempts to parse; returns `true` and sets
  `result` on success, otherwise `false` with `result = null`.

* `string ToCss(string propertyName)` — Formats a CSS declaration using the current value.
  Returns `"propertyName:value;"` (with a lower-cased, trimmed property name). If `propertyName` is `null`/whitespace,
  returns just `Value`. *(inherited from `ValueBase`)*

* `int CompareTo(ValueBase other)` — Ordinal comparison by `Value`. *(inherited)*

* `bool Equals(ValueBase other)` / `override bool Equals(object? obj)` — Ordinal equality by `Value`. *(inherited)*

* `override int GetHashCode()` — Based on `Value`. *(inherited)*

* `override string ToString()` — Returns `Value`. *(inherited)*

---

## Operators

* `==`, `!=`, `<`, `<=`, `>`, `>=` — Ordinal comparison/equality by `Value` (inherited from `ValueBase`). Comparing
  different
  concrete `ValueBase` types throws.
* `implicit operator AllyariaStringValue(string)` — Converts a `string` to a normalized `AllyariaStringValue` (throws on
  invalid input).
* `implicit operator string(AllyariaStringValue)` — Extracts the underlying normalized `Value`.

---

## Exceptions

* `ArgumentException` — Thrown when constructing/parsing with `null`, empty, whitespace-only input, or input containing
  control
  characters.
* `ArgumentException` — Thrown when comparing values of different concrete `ValueBase` types. *(inherited)*
