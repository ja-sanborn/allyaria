# Allyaria.Theming.Constants.Styles

`Styles` provides a small, curated set of **predefined theme presets**—ready-to-use `AllyariaStyle` instances that
combine an `AllyariaPalette` (foreground/background, borders, hover) with an `AllyariaTypography` (font family/size,
etc.). These presets are intended as sane defaults for Light, Dark, and High-Contrast experiences and are WCAG-minded
out of the box.

> This page follows the structure used by `Allyaria.Theming.Constants.Colors`, but instead of listing many color tokens,
> it catalogs a few **opinionated style bundles**.

---

## Presets

| Property       | Palette (Bg → Fg)    | Typography (Family, Size)                                | Intent / Notes                                              |
|----------------|----------------------|----------------------------------------------------------|-------------------------------------------------------------|
| `Dark`         | `Grey900` → `Grey50` | `Segoe UI, Roboto, Helvetica, Arial, sans-serif`, `1rem` | High contrast on dark background; suitable for dark UIs.    |
| `HighContrast` | `White` → `Black`    | `Segoe UI, Roboto, Helvetica, Arial, sans-serif`, `1rem` | Maximum contrast grayscale; useful for accessibility modes. |
| `Light`        | `Grey50` → `Grey900` | `Segoe UI, Roboto, Helvetica, Arial, sans-serif`, `1rem` | Comfortable default for light UIs.                          |

> Palette colors reference the centralized named color library documented in **`Allyaria.Theming.Constants.Colors`**.

---

## API

### Static Class

```csharp
public static class Styles
```

### Members

* `public static AllyariaStyle DarkStyle { get; }` — WCAG-friendly dark preset (Grey900 background, Grey50 text).
* `public static AllyariaStyle HighContrast { get; }` — High-contrast white/black preset.
* `public static AllyariaStyle LightStyle { get; }` — WCAG-friendly light preset (Grey50 background, Grey900 text).

All presets use a neutral, widely available sans-serif stack and a base size of `1rem`, encouraging user-controlled
scaling.

---

## Usage

### Inline styles

```csharp
using Allyaria.Theming.Constants;

var style = Styles.DarkStyle;
var css       = style.ToCss();      // normal state
var cssHover  = style.ToCssHover(); // hover state
var cssTokens = style.ToCssVars();  // CSS custom properties
```

### In Razor

```razor
@using Allyaria.Theming.Constants

<div class="card" style="@Styles.LightStyle.ToCss()">
  <h2>Title</h2>
  <p>Body text…</p>
</div>

<style>
.card:hover { @Styles.LightStyle.ToCssHover() }   /* hover colors */
.card { @Styles.LightStyle.ToCssVars() }          /* expose tokens for isolated CSS */
</style>
```

---

## Accessibility & Design Notes

* **Contrast:** Each preset pairs foreground/background to meet common contrast targets for body text; use
  component-level checks for specific sizes/weights as needed.
* **Typography scaling:** `1rem` respects user zoom and OS text settings; avoid hard-coding pixel sizes downstream.
* **Extensibility:** Treat these presets as starting points—compose new `AllyariaStyle` values from `Colors` and custom
  type ramps for brand alignment.

---

## Related

* **`AllyariaStyle`** — Combined palette + typography with `ToCss()`, `ToCssHover()`, `ToCssVars()`.
* **`AllyariaPalette`** — Colors/backgrounds/borders + hover logic.
* **`AllyariaTypography`** — Font family/size/weight/spacing.
* **`Colors`** — Named CSS & Material color tokens used by these presets. 
