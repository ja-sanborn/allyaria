# Allyaria.Theming.Primitives.ContrastResult

`ContrastResult` is an immutable value type that describes the outcome of a contrast adjustment operation. It provides
the resolved **foreground color**, the background used for evaluation, the **computed contrast ratio**, and whether the
minimum contrast requirement was met.

---

## Constructors

*
`ContrastResult(AllyariaColorValue foregroundColor, AllyariaColorValue backgroundColor, double contrastRatio, bool meetsMinimum)`
Creates a result object representing the contrast computation between two opaque colors.

    * `foregroundColor` — The final, adjusted foreground color (opaque).
    * `backgroundColor` — The background color used to evaluate contrast (opaque).
    * `contrastRatio` — The calculated WCAG contrast ratio `(Llighter + 0.05) / (Ldarker + 0.05)`.
    * `meetsMinimum` — Indicates whether the computed ratio satisfies the requested minimum contrast requirement.

---

## Properties

* `AllyariaColorValue ForegroundColor` — The resolved, final foreground color.
* `AllyariaColorValue BackgroundColor` — The background color used to compute the ratio.
* `double ContrastRatio` — The achieved WCAG contrast ratio.
* `bool MeetsMinimum` — `true` if the ratio meets or exceeds the requested minimum; otherwise `false`.

---

## Events

* *None*

---

## Methods

* Inherits default record struct methods:

    * `bool Equals(object? obj)` — Checks equality by comparing all property values.
    * `int GetHashCode()` — Hash code derived from property values.
    * `string ToString()` — Debug-friendly string representation with property names and values.

---

## Operators

* `==`, `!=` — Value equality and inequality checks across all fields.
