# Allyaria.Theming.Styles.AllyariaStyle

`AllyariaStyle` is an immutable **record-struct** that composes an `AllyariaPalette` (colors, borders, backgrounds) with
`AllyariaTypography` (font family, size, weight, line-height, etc.) and exposes helpers to emit **inline CSS** and **CSS
custom properties** for its base, hover, and disabled states. It concatenates each sub-type’s CSS fragments—so all
precedence rules for colors, images, borders, and state-specific behavior are inherited from `AllyariaPalette`, while
all
font rules are inherited from `AllyariaTypography`.

---

## Constructors

`AllyariaStyle(AllyariaPalette palette, AllyariaTypography typography, AllyariaPalette? paletteHover = null, AllyariaTypography? typographyHover = null, AllyariaPalette? paletteDisabled = null, AllyariaTypography? typographyDisabled = null)`
Creates a style by pairing a palette with a typography definition, with optional **hover** and **disabled** overrides.

  * `palette` — The computed color/border/background model.
  * `typography` — The text styling model (font stack, sizing, weights, spacing).
  * `paletteHover` — Optional hover palette. If omitted, a derived hover palette is created from `palette`.
  * `typographyHover` — Optional hover typography. Defaults to `typography` when omitted.
  * `paletteDisabled` — Optional disabled palette. If omitted, a derived disabled palette is created from `palette`.
  * `typographyDisabled` — Optional disabled typography. Defaults to `typography` when omitted.

---

## Properties

* `Palette` — The base color/background/border source.
* `PaletteHover` — The hover palette used by `ToCssHover()` and hover variable emission.
* `PaletteDisabled` — The disabled palette used by variable emission.
* `Typography` — The base typography source.
* `TypographyHover` — The hover typography used by `ToCssHover()` and hover variable emission.
* `TypographyDisabled` — The disabled typography used by variable emission.

> **Immutability:** Being a `record struct` with readonly members, instances are value-type, copyable, and safe to
> pass by value without defensive cloning. Equality and deconstruction follow record semantics.

---

## Events

* *None*

---

## Methods

* `string ToCss()`
  Returns the concatenation of `Palette.ToCss()` and `Typography.ToCss()`.
  Use when emitting **non-hover** inline declarations (e.g., on a root element). Palette emission includes `color` and
  either `background-color` or `background-image` + sizing rules; border declarations are included when configured.
  Typography emission includes font-related declarations.

* `string ToCssHover()`
  Returns the concatenation of `PaletteHover.ToCss()` and `TypographyHover.ToCss()`.
  Use for **`:hover`** inline declarations or when composing style strings for interactive states.

* `string ToCssVars(string prefix = "")`
  Emits custom properties (CSS variables) for **base, disabled, and hover states** using a normalized prefix.

    * **Prefix normalization:** Input `prefix` is trimmed of leading/trailing hyphens, collapses whitespace/dashes to a
      single hyphen, and is lowercased. If the result is empty/whitespace, the default prefix **`aa`** is used.
    * **State namespaces:** Disabled variables reuse the same normalized prefix with **`-disabled`** appended. Hover
      variables use **`-hover`**.
    * **Output composition:** Concatenates the variable declarations from `Palette` and `Typography` for the base
      prefix,
      then `PaletteDisabled` and `TypographyDisabled` for the disabled prefix, and `PaletteHover` and
      `TypographyHover` for the hover prefix.

  Use this when exposing **CSS custom properties** (e.g., `--aa-…`) that component-scoped CSS can consume without
  relying on global overrides.

---

## Operators

* `==`, `!=` — Value equality provided by `record struct` semantics (compares all palette and typography states).
  No ordering operators are defined.

---

## Usage

### Build a style and apply inline CSS

```csharp
using Allyaria.Theming.Styles;

var palette = new AllyariaPalette(
    backgroundColor: Colors.White,
    foregroundColor: Colors.Black,
    borderWidth: 1,
    borderStyle: "solid",
    borderColor: Colors.Gray700);

var typography = new AllyariaTypography(
    fontFamily: "Inter, system-ui, sans-serif",
    fontSize: "14px",
    lineHeight: "1.5",
    fontWeight: 500);

// Hover and disabled states optional — if omitted, derived automatically
var style = new AllyariaStyle(palette, typography);

var css         = style.ToCss();        // base declarations
var cssHover    = style.ToCssHover();   // hover declarations
var cssVars     = style.ToCssVars();    // base + disabled + hover CSS variables
```

---

## Behavior & Precedence Notes

* **Colors & Backgrounds:** `AllyariaStyle` defers to `AllyariaPalette` for all background/foreground/border logic,
  including hover and disabled adjustments.
* **Hover & Disabled Strategies:** By default, hover and disabled states are derived from the base palette and
  typography
  when not explicitly provided.
* **CSS Variables:** `ToCssVars(prefix)` emits **base**, **disabled**, and **hover** variable sets under the normalized
  prefix.

---

## Performance

* **Allocation-lean:** Methods use `string.Concat` to join palette and typography fragments, avoiding unnecessary
  intermediates.
* **Value semantics:** As a small `record struct`, instances are cheap to copy and compare; equality comparisons are
  structural across all states.

---

## Related

* **`AllyariaPalette`** — Background/foreground/border logic and CSS emission, including hover, disabled, and CSS
  variables.
* **`AllyariaTypography`** — Font stack, size, weight, spacing, and corresponding CSS/variable emission.
