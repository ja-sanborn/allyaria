# Allyaria.Theming.Styles.AllyariaPalette

`AllyariaPalette` is an immutable record-struct that computes effective foreground, background, and border styles (with
Allyaria precedence rules) and is optimized for inline CSS generation and CSS custom property emission. It supports
optional background images (with a readability overlay), opt-in borders, and sensible defaults for contrast.

---

## Constructors

`AllyariaPalette(AllyariaColorValue? backgroundColor = null, AllyariaColorValue? foregroundColor = null, string? backgroundImage = "", int? borderWidth = 0, AllyariaColorValue? borderColor = null, AllyariaStringValue? borderStyle = null, AllyariaStringValue? borderRadius = null)`
Creates a palette with optional overrides.

* `backgroundColor` — Base background; when omitted defaults to `Colors.White`.
* `foregroundColor` — Explicit foreground; when omitted, computed from effective background lightness (`V < 50 → White`,
  else `Black`).
* `backgroundImage` — Optional URL. If non-empty, becomes a composite value:
  `linear-gradient(rgba(255,255,255,0.5),rgba(255,255,255,0.5)),url("<trimmed-lowercased>")`. Whitespace is trimmed and
  the URL lower-cased.
* `borderWidth` — Pixels. `<= 0` omits the border; `> 0` emits `border-width` (stored as `int?`).
* `borderColor` — When border is present and color omitted, defaults to the **base** background color (or white) for
  cohesion.
* `borderStyle` — Token such as `solid`; defaults to `solid`.
* `borderRadius` — Optional radius (e.g., `4px`); **emitted whenever set**, independent of whether a border exists.

---

## Properties

* `AllyariaColorValue BackgroundColor` — Effective background after precedence.
* `AllyariaStringValue? BackgroundImage` — Composite background-image declaration or `null` when not set.
* `AllyariaColorValue BorderColor` — Effective border color when a border is present; `Transparent` when not. Defaults
  to base background (or white) if not explicitly set.
* `AllyariaStringValue? BorderRadius` — Optional radius token.
* `AllyariaStringValue BorderStyle` — Border style token; defaults to `solid`.
* `AllyariaStringValue? BorderWidth` — Width token like `1px` when `> 0`; otherwise `null`.
* `AllyariaColorValue ForegroundColor` — Effective text color; uses background lightness to choose black/white when not
  explicitly provided.

---

## Events

* *None*

---

## Methods

* `string ToCss()` — Builds inline CSS. Always emits `color`.
  If a background image is set, emits `background-image` plus
  `background-position:center; background-repeat:no-repeat; background-size:cover;`; otherwise emits
  `background-color`.
  If a border is present, emits `border-color`, `border-style`, `border-width`.
  Emits `border-radius` when a radius is set (independent of border).

* `string ToCssVars(string prefix = "")` — Emits CSS custom properties for theme-aware styling. The `prefix` is
  normalized by trimming, lowercasing, replacing spaces with hyphens, and stripping leading/trailing dashes; empty
  resolves to `--aa-`.
  Outputs `--{prefix}color`. If a background image is set, outputs `--{prefix}background-image`; otherwise outputs
  `--{prefix}background-color`.
  When a border is present, outputs `--{prefix}border-color`, `--{prefix}border-style`, `--{prefix}border-width`.
  Emits `--{prefix}border-radius` when a radius is set (independent of border).

* `AllyariaPalette Cascade(AllyariaColorValue? backgroundColor = null, AllyariaColorValue? foregroundColor = null, string? backgroundImage = null, int? borderWidth = null, AllyariaColorValue? borderColor = null, AllyariaStringValue? borderStyle = null, AllyariaStringValue? borderRadius = null)` —
Returns a new palette applying overrides to **base** values (does not re-apply derived precedence). Negative
`borderWidth` is treated as `null` (no border).

---

## Operators

* `==`, `!=` — Value equality/inequality provided by `record struct` semantics. No ordering operators are defined.
