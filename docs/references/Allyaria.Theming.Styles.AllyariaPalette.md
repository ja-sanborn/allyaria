# Allyaria.Theming.Palette.AllyariaPalette

`AllyariaPalette` is an immutable record-struct that computes effective foreground, background, and border styles
(including hover variants) following Allyaria’s precedence rules. It is optimized for inline CSS generation and
CSS custom property emission, handling optional background images (with overlay), opt-in borders, and sensible
defaults for contrast.

---

## Constructors

*

`AllyariaPalette(AllyariaColorValue? backgroundColor = null, AllyariaColorValue? foregroundColor = null, AllyariaColorValue? backgroundHoverColor = null, AllyariaColorValue? foregroundHoverColor = null, string backgroundImage = "", int borderWidth = 0, AllyariaColorValue? borderColor = null, AllyariaStringValue? borderStyle = null, AllyariaStringValue? borderRadius = null)`  
Creates a palette with optional overrides.

* `backgroundColor` — Base background; defaults to `Colors.White`.
* `foregroundColor` — Explicit foreground; when omitted, computed for contrast from effective background (
  `V < 50 → White`, else `Black`).
* `backgroundHoverColor` — When omitted, defaults to `BackgroundColor.HoverColor()`.
* `foregroundHoverColor` — When omitted, defaults to `ForegroundColor.HoverColor()`.
* `backgroundImage` — Optional URL. If non-empty, the palette uses a composite value
  `linear-gradient(rgba(255,255,255,0.5), rgba(255,255,255,0.5)), url("<lowercased, trimmed>")` for readability;
  otherwise no image.
* `borderWidth` — Pixels; `<= 0` omits the border, `> 0` emits `border-width`.
* `borderColor` — When border is present and color omitted, defaults to effective background for cohesion.
* `borderStyle` — Token such as `solid`; defaults to `solid`.
* `borderRadius` — Optional radius (e.g., `4px`); emitted only when present and a border exists.

---

## Properties

* *None*

---

## Events

* *None*

---

## Methods

* `string ToCss()` — Builds inline CSS declarations for the current (non-hover) state.  
  Always emits `color`. If a background image is set, emits `background-image` plus
  `background-position:center; background-repeat:no-repeat; background-size:cover;`; otherwise emits `background-color`.
  If a border is present, emits `border-color`, `border-style`, `border-width`, and (if specified) `border-radius`.

* `string ToCssVars(string prefix = "")` — Emits CSS custom properties for theme-aware component styling.
  The prefix is `--{prefix}-` and defaults to `--aa-`.
  Outputs `--aa-color`. If a background image is set, outputs `--aa-bg-image`; otherwise outputs
  `--aa-background-color` . If a border is present, outputs `--aa-border-color`, `--aa-border-style`,
  `--aa-border-width`, and (if set) `--aa-border-radius`.

---

## Operators

* `==`, `!=` — Value equality/inequality provided by `record struct` semantics. No ordering operators are defined.
