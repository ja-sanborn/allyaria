# Allyaria.Theming.Helpers.ColorHelper

`ColorHelper` provides WCAG-aware color utilities used by theming: contrast ratio computation, hue-preserving contrast
repair for opaque colors, sRGB mixing, scalar blending, and luminance. It’s a static, allocation-free helper (no canvas
alpha blending), designed to support palette derivations.

---

## Constructors

* *None* (static class)

---

## Properties

* *None*

---

## Events

* *None*

---

## Methods

* `static double Blend(double start, double target, double t)`
  Linearly blends a scalar toward a `target` by factor `t ∈ [0..1]`. `0 → start`, `1 → target`.

* `static double ContrastRatio(AllyariaColorValue foreground, AllyariaColorValue background)`
  Computes WCAG contrast between two **opaque sRGB** colors as `(Llighter + 0.05) / (Ldarker + 0.05)` using relative
  luminance.

* `static ContrastResult EnsureMinimumContrast(AllyariaColorValue foreground, AllyariaColorValue background, double minimumRatio = 3.0)`
Resolves a foreground that **meets or best-approaches** `minimumRatio` over `background`. Strategy:

    1. keep **H**/**S** and binary-search **V** in the locally better direction;
    2. try the opposite **V** direction if needed;
    3. if still unmet, **mix toward white** in sRGB;
       returns the first solution that meets the target, otherwise the best-approaching result. The returned
       `ContrastResult` includes the final color, background, achieved ratio, and a flag indicating whether the minimum
       was
       met.

* `static AllyariaColorValue MixSrgb(AllyariaColorValue a, AllyariaColorValue b, double t)`
  Linear interpolation between two **opaque** colors in sRGB (`t ∈ [0..1]`). Note: not perceptually uniform.

* `static double RelativeLuminance(AllyariaColorValue color)`
  Computes WCAG relative luminance from sRGB bytes via linearized channels.

---

## Usage

### Ensure accessible text color

```csharp
var bg = new AllyariaColorValue("#0F172AFF");   // slate-900
var fg = new AllyariaColorValue("#334155FF");   // slate-700 (may be too low contrast)

var result = ColorHelper.EnsureMinimumContrast(fg, bg, minimumRatio: 4.5);
var accessibleFg = result.ForegroundColor;      // hue-preserving adjustment when possible
var ratio = result.ContrastRatio;               // ≥ 4.5 if `MeetsMinimum` is true
```

### Simple sRGB mix

```csharp
var a = new AllyariaColorValue("#FF0000FF");
var b = new AllyariaColorValue("#0000FFFF");
var mid = ColorHelper.MixSrgb(a, b, 0.5);  // #800080FF (approximate, sRGB-linear mix)
```

---

## Notes

* All inputs/outputs are **opaque** (`#RRGGBBAA`, `A = 1`), and operations are tuned for UI theme work.
* `ColorHelper` is internal; it’s consumed by higher-level types (e.g., palettes) to derive hover/disabled states and to
  enforce readable foregrounds.
