# Allyaria.Theming.Styles.AllyariaStyle

`AllyariaStyle` is an immutable **record-struct** that composes an `AllyariaPalette` (colors, borders, backgrounds) with
`AllyariaTypography` (font family, size, weight, line-height, etc.) and exposes helpers to emit **inline CSS** and **CSS
custom properties** for both its normal and hover states. It simply **concatenates** each sub-type’s CSS fragments—so
all precedence rules for colors, images, borders, and hover behavior are inherited from `AllyariaPalette`, while all
font rules are inherited from `AllyariaTypography`.

---

## Constructors

* `AllyariaStyle(AllyariaPalette palette, AllyariaTypography typography)`
  Creates a style by pairing a palette with a typography definition.

    * `palette` — The computed color/border/background model. See `AllyariaPalette` docs for precedence and CSS emission
      specifics.
    * `typography` — The text styling model (font stack, sizing, weights, spacing). *(See `AllyariaTypography` API for
      details.)*

---

## Properties

* `Palette` *(get; init)* — The color/background/border source whose `ToCss*`/`ToCssVars` outputs are included.
* `Typography` *(get; init)* — The typography source whose `ToCss*`/`ToCssVars` outputs are included.

> **Immutability:** Being a `record struct` with `readonly` members, instances are value-type, copyable, and safe to
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
  Returns the concatenation of `Palette.ToCssHover()` and `Typography.ToCss()`.
  Use for **hover** state styling. Palette hover behavior mirrors non-hover with hover color variants; background image
  behavior is identical to non-hover. Typography typically does not change on hover and is reused.

* `string ToCssVars()`
  Returns the concatenation of `Palette.ToCssVars()` and `Typography.ToCssVars()`.
  Use to expose **CSS custom properties** (e.g., `--aa-fg`, `--aa-bg`, `--aa-border-width`) that components can consume
  in isolated CSS without relying on global overrides.

---

## Operators

* `==`, `!=` — Value equality provided by `record struct` semantics (compares `Palette` and `Typography`). No ordering
  operators are defined.

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

var style = new AllyariaStyle(palette, typography);

var css      = style.ToCss();      // non-hover declarations
var cssHover = style.ToCssHover(); // hover declarations
var cssVars  = style.ToCssVars();  // CSS custom properties for isolated CSS
```

*`ToCss()` and `ToCssHover()` are concatenations of the palette and typography outputs; palette emission rules are
described in `AllyariaPalette`. Typography emission covers font-related declarations only.*

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
.card { @(_style.ToCssVars()) } /* expose custom properties for CSS isolation */
</style>
```

---

## Behavior & Precedence Notes

* **Colors & Backgrounds:** `AllyariaStyle` defers to `AllyariaPalette` for all background/foreground/border logic,
  including image overlays, hover variants, and border emission (width/style/color/radius). If a background image is
  set, the palette emits `background-image` + sizing/positioning helpers; otherwise it emits `background-color`.
* **Typography:** `AllyariaStyle` includes typography unchanged across normal/hover; use a different `AllyariaStyle` if
  hover should adjust font weight/decoration.
* **CSS Variables:** When you prefer styling via CSS isolation, call `ToCssVars()` and consume the emitted custom
  properties from your component styles instead of overriding with global CSS.

---

## Performance

* **Allocation-lean:** Methods use `string.Concat` to join the palette and typography fragments, avoiding intermediate
  builders where unnecessary. This is suitable for per-render inline style emission.
* **Value semantics:** As a small `record struct`, instances are cheap to copy and compare; equality comparisons are
  structural (`Palette`, `Typography`).

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
--aa-fg: #000000;
--aa-fg-hover: #111827;
--aa-bg: #ffffff;
--aa-bg-hover: #f9fafb;
--aa-border-color: #4b5563;
--aa-border-style: solid;
--aa-border-width: 1px;
--aa-font-family: Inter, system-ui, sans-serif;
--aa-font-size: 14px;
--aa-line-height: 1.5;
--aa-font-weight: 500;
```

*(Variable names shown for illustration; the exact typography variable names are defined by `AllyariaTypography`.)*

---

## When to Use `AllyariaStyle`

* You want a **single source** that represents both color/border/background and type, to apply consistently across a
  component.
* You prefer **inline styles** with strong precedence (no reliance on global CSS overrides), but still want to **export
  CSS variables** for isolated CSS to consume.

---

## Related

* **`AllyariaPalette`** — Background/foreground/border logic and CSS emission, including hover and CSS variables.
* **`AllyariaTypography`** — Font stack, size, weight, spacing, and corresponding CSS/variable emission. *(See its API
  doc.)*
