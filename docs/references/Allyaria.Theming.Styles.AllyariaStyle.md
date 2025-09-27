# Allyaria.Theming.Styles.AllyariaStyle

`AllyariaStyle` is an immutable **record-struct** that composes an `AllyariaPalette` (colors, borders, backgrounds) with
`AllyariaTypography` (font family, size, weight, line-height, etc.) and exposes helpers to emit **inline CSS** and **CSS
custom properties** for both its normal and hover states. It concatenates each sub-type’s CSS fragments—so all
precedence rules for colors, images, borders, and hover behavior are inherited from `AllyariaPalette`, while all
font rules are inherited from `AllyariaTypography`.

---

## Constructors

* `AllyariaStyle(AllyariaPalette palette, AllyariaTypography typography, AllyariaPalette? paletteHover = null, AllyariaTypography? typographyHover = null)`
Creates a style by pairing a palette with a typography definition, with optional **hover** overrides.

    * `palette` — The computed color/border/background model. See `AllyariaPalette` docs for precedence and CSS emission
      specifics.
    * `typography` — The text styling model (font stack, sizing, weights, spacing).
    * `paletteHover` — Optional hover palette. If omitted, a derived hover palette is created from `palette`
      (foreground/background shifted to their hover variants) while preserving other palette aspects.
    * `typographyHover` — Optional hover typography. Defaults to `typography` when omitted.

---

## Properties

* `Palette` *(get)* — The color/background/border source whose `ToCss*`/`ToCssVars` outputs are included.
* `PaletteHover` *(get)* — The hover palette used by `ToCssHover()` and hover variable emission.
* `Typography` *(get)* — The typography source whose `ToCss*`/`ToCssVars` outputs are included.
* `TypographyHover` *(get)* — The hover typography used by `ToCssHover()` and hover variable emission.

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
  Emits custom properties (CSS variables) for **both base and hover states** using a normalized prefix.

    * **Prefix normalization:** Input `prefix` is trimmed of leading/trailing hyphens, collapses whitespace/dashes to a
      single hyphen, and is lowercased. If the result is empty/whitespace, the default prefix **`aa`** is used.
    * **Hover namespace:** Hover variables reuse the same normalized prefix with **`-hover`** appended.
    * **Output composition:** Concatenates the variable declarations from `Palette` and `Typography` for the base
      prefix,
      then `PaletteHover` and `TypographyHover` for the hover prefix.

  Use this when exposing **CSS custom properties** (e.g., `--aa-…`) that component-scoped CSS can consume without
  relying
  on global overrides.

---

## Operators

* `==`, `!=` — Value equality provided by `record struct` semantics (compares `Palette`, `PaletteHover`, `Typography`,
  and `TypographyHover`). No ordering operators are defined.

---

## Usage

### 1) Build a style and apply inline CSS

```csharp
using Allyaria.Theming.Styles;

var palette = new AllyariaPalette(
    backgroundColor: Colors.White,
    foregroundColor: Colors.Black,
    backgroundImage: "",  // no image → uses background-color
    borderWidth: 1,
    borderStyle: "solid",
    borderColor: Colors.Gray700);

var typography = new AllyariaTypography(
    fontFamily: "Inter, system-ui, sans-serif",
    fontSize: "14px",
    lineHeight: "1.5",
    fontWeight: 500);

// Hover palette/typography optional: omitted → derived palette hover, same typography by default
var style = new AllyariaStyle(palette, typography);

var css      = style.ToCss();       // non-hover declarations
var cssHover = style.ToCssHover();  // hover declarations
var cssVars  = style.ToCssVars();   // base + hover CSS custom properties
```

### 2) In Razor (inline + vars)

```razor
@code {
    private AllyariaStyle _style = style; // from example above
}

<div style="@_style.ToCss()" class="card">
  <h2>Title</h2>
  <p>Body text…</p>
</div>

<style>
/* Component .razor.css is preferred; shown inline here for illustration only. */
.card:hover { @(_style.ToCssHover()) }
.card { @(_style.ToCssVars()) } /* exposes base + hover custom properties for CSS isolation */
</style>
```

---

## Behavior & Precedence Notes

* **Colors & Backgrounds:** `AllyariaStyle` defers to `AllyariaPalette` for all background/foreground/border logic,
  including image overlays and border emission. If a background image is set, the palette emits `background-image` +
  sizing/positioning helpers; otherwise it emits `background-color`.
* **Hover strategy:** By default, hover uses a derived palette (foreground/background shifted to their hover colors) and
  the same typography. Provide explicit `paletteHover` and/or `typographyHover` to override that behavior.
* **CSS Variables:** `ToCssVars(prefix)` exposes **both base and hover** variables with the same normalized prefix;
  hover
  variables add `-hover`.

---

## Performance

* **Allocation-lean:** Methods use `string.Concat` to join palette and typography fragments, avoiding unnecessary
  intermediates.
* **Value semantics:** As a small `record struct`, instances are cheap to copy and compare; equality comparisons are
  structural across base and hover members.

---

## Examples of Resulting CSS (Representative)

*(Exact output depends on the configured `AllyariaPalette` and `AllyariaTypography`.)*

**Non-hover (`ToCss`)**

```css
color: #000000; background-color: #ffffff; border-color: #4b5563; border-style: solid; border-width: 1px;
font-family: Inter, system-ui, sans-serif; font-size: 14px; line-height: 1.5; font-weight: 500;
```

**Hover (`ToCssHover`)**

```css
color: #111827; background-color: #f9fafb; border-color: #374151; border-style: solid; border-width: 1px;
font-family: Inter, system-ui, sans-serif; font-size: 14px; line-height: 1.5; font-weight: 500;
```

**Variables (`ToCssVars`)**

```css
/* Base (prefix normalized; default "aa" if empty) */
--aa-fg: #000000;
--aa-bg: #ffffff;
--aa-border-color: #4b5563;
--aa-border-style: solid;
--aa-border-width: 1px;
--aa-font-family: Inter, system-ui, sans-serif;
--aa-font-size: 14px;
--aa-line-height: 1.5;
--aa-font-weight: 500;

/* Hover (same prefix with "-hover" appended) */
--aa-hover-fg: #111827;
--aa-hover-bg: #f9fafb;
--aa-hover-border-color: #374151;
/* Typography hover variables mirror base names under the hover prefix */
```

*(Variable names shown for illustration; the exact typography variable names are defined by `AllyariaTypography`.)*

---

## When to Use `AllyariaStyle`

* You want a **single source** that represents both color/border/background and type, to apply consistently across a
  component with **explicit hover handling**.
* You prefer **inline styles** with strong precedence (no reliance on global CSS overrides), while also exporting **CSS
  variables** for isolated CSS to consume.

---

## Related

* **`AllyariaPalette`** — Background/foreground/border logic and CSS emission, including hover and CSS variables.
* **`AllyariaTypography`** — Font stack, size, weight, spacing, and corresponding CSS/variable emission.
