# Allyaria.Theming.Palette.AllyariaPaletteItem

`AllyariaPaletteItem` represents a theming palette entry that encapsulates background/border/foreground colors with
hover variants and an optional background image. It applies documented precedence so getters always return an effective,
non-null color while setters accept explicit overrides. Hover, border, and background-image behaviors are rendered as
CSS through helper methods.

---

## Constructors

* `AllyariaPaletteItem()`
  Initializes a new palette item with defaults (`HasBackground = true`, `HasBorder = false`). Colors are resolved by
  precedence when not explicitly set.

* `AllyariaPaletteItem(AllyariaColor backgroundColor)`
  Initializes with a base background color (used as the starting point for precedence/hover computation).

* `AllyariaPaletteItem(AllyariaColor backgroundColor, AllyariaColor foregroundColor)`
  Initializes with explicit background and foreground colors.

---

## Properties

* `AllyariaColor BackgroundColor` — Effective background color.
  If unset, resolves to white; when `HasBorder == true`, returns the hover variant of the base background. Setter
  assigns the base background.

* `AllyariaColor BackgroundHoverColor` — Hover background color.
  If unset, resolves to `BackgroundColor.HoverColor()`.

* `string? BackgroundImage` — Optional background image URL.
  When set and `HasBackground == true`, CSS output uses a `linear-gradient` 50% white overlay above the image;
  whitespace-only values are normalized to `null`.

* `AllyariaColor BorderColor` — Border color.
  If unset, falls back to the **base** background (pre `HasBorder` shift).

* `AllyariaColor ForegroundColor` — Foreground (text) color.
  If unset, computed from the **effective** background: `V < 50 → White`, otherwise `Black`.

* `AllyariaColor ForegroundHoverColor` — Hover foreground color.
  If unset, resolves to `ForegroundColor.HoverColor()`.

* `bool HasBackground` — Whether a background (color/image) should be rendered.
  Defaults to `true`. When `false`, background-related CSS is omitted.

* `bool HasBorder` — Whether a border should be rendered.
  Defaults to `false`. When `true`, border color is emitted; also causes `BackgroundColor` to return the hover variant
  of its base value.

---

## Events

* *None*

---

## Methods

* `string ToCss()` — Builds inline CSS declarations for the current state:
  Always emits `color`. If `HasBackground` is true, emits either `background-color` (no image) **or** `background-image`
  with a 50% white overlay (when `BackgroundImage` is set). If `HasBorder` is true, emits `border-color`. Uses
  `AllyariaColor.ToCss(string)` for declarations.

* `string ToCssVars()` — Emits CSS custom properties (`--aa-*`) for per-element theming without per-instance `<style>`
  tags.
  Outputs `--aa-bg`, `--aa-fg`, `--aa-bg-hover`, `--aa-fg-hover`, `--aa-border`, and `--aa-bg-image` (`url("…")` or
  `none`). Intended for use with component-scoped CSS that references these variables (including `:hover`).

* `string ToHoverCss()` — Builds inline CSS declarations for **hover** state mirroring `ToCss()` but using hover colors;
  includes the same image/overlay behavior if applicable.

* `override string ToString()` — Returns the same CSS as `ToCss()`.

---

## Operators

* *None*
