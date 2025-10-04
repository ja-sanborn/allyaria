# Allyaria.Theming.Styles.AllyariaStyle

`AllyariaStyle` is a strongly-typed, immutable styling aggregate that composes **Palette**, **Typography**, **Spacing**,
and **Border** into a single unit. It can emit concrete CSS declarations or namespaced CSS custom properties for the *
*base**, **hover**, and **disabled** states. Spacing and Border are applied consistently across all states; Palette and
Typography can vary by state.

> Rev Date: 2025-10-03

---

## Constructors

`AllyariaStyle(AllyariaPalette palette, AllyariaTypography typography, AllyariaSpacing spacing, AllyariaBorders border, AllyariaPalette? paletteHover = null, AllyariaTypography? typographyHover = null, AllyariaPalette? paletteDisabled = null, AllyariaTypography? typographyDisabled = null)`

Creates a new style composed of the provided building blocks.

* If `paletteHover` is `null`, it is derived from `palette.ToHoverPalette()`.
* If `typographyHover` is `null`, it defaults to `typography`.
* If `paletteDisabled` is `null`, it is derived from `palette.ToDisabledPalette()`.
* If `typographyDisabled` is `null`, it defaults to `typography`.

*Exceptions:* None

---

## Properties

| Name                 | Type                 | Description                                                                                 |
|----------------------|----------------------|---------------------------------------------------------------------------------------------|
| `Palette`            | `AllyariaPalette`    | Base palette (e.g., background/foreground colors).                                          |
| `PaletteHover`       | `AllyariaPalette`    | Palette for the `:hover` state. Defaults from `Palette`.                                    |
| `PaletteDisabled`    | `AllyariaPalette`    | Palette for the disabled state. Defaults from `Palette`.                                    |
| `Typography`         | `AllyariaTypography` | Base typography settings (font family, size, etc.).                                         |
| `TypographyHover`    | `AllyariaTypography` | Typography for the `:hover` state. Defaults to `Typography`.                                |
| `TypographyDisabled` | `AllyariaTypography` | Typography for the disabled state. Defaults to `Typography`.                                |
| `Spacing`            | `AllyariaSpacing`    | Spacing (margins/paddings) applied consistently across all states.                          |
| `Border`             | `AllyariaBorders`    | Border (per-side width/style and per-corner radius) applied consistently across all states. |

---

## Methods

| Name                            | Returns  | Description                                                                                                                                                                                                                                                                              |
|---------------------------------|----------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `ToCss()`                       | `string` | Concatenates base state CSS from `Palette`, `Typography`, `Spacing`, and `Border`.                                                                                                                                                                                                       |
| `ToCssDisabled()`               | `string` | Concatenates disabled state CSS from `PaletteDisabled`, `TypographyDisabled`, plus `Spacing` and `Border`.                                                                                                                                                                               |
| `ToCssHover()`                  | `string` | Concatenates hover state CSS from `PaletteHover`, `TypographyHover`, plus `Spacing` and `Border`.                                                                                                                                                                                        |
| `ToCssVars(string prefix = "")` | `string` | Emits CSS custom properties for base/disabled/hover. The `prefix` is normalized (trim/collapse dashes/whitespace, lowercase). Spacing and Border variables are emitted **once** under the base prefix; Palette and Typography variables are emitted for base, `-disabled`, and `-hover`. |

**Variable Prefixing Behavior**

* Input `prefix` → normalize: collapse whitespace/dashes to single `-`, trim leading/trailing `-`, lowercase.
* Empty after normalization → use `"aa"` as the logical base prefix.
* Underlying emitters (`*.ToCssVars`) output variables as `--{prefix}-*`.
  Examples: `--card-color-surface`, `--card-border-top-width`.

---

## Operators

| Operator    | Returns | Description          |
|-------------|---------|----------------------|
| `==` / `!=` | `bool`  | Equality comparison. |

---

## Events

*None*

---

## Exceptions

*None*

---

## Behavior Notes

* **Immutability:** `readonly record struct`; all members are set via the constructor. Methods return strings only.
* **State Model:** Spacing and Border are shared across base/hover/disabled to minimize duplication and ensure layout
  consistency. Palette/typography can vary per state.
* **CSS Emission:** `ToCss*()` concatenate the underlying components in this order: Palette → Typography → Spacing →
  Border.
* **Variables Emission:** `ToCssVars(prefix)` emits:

    * Base variables for Palette, Typography, Spacing, Border under `<prefix>`.
    * Disabled variables for Palette, Typography under `<prefix>-disabled`.
    * Hover variables for Palette, Typography under `<prefix>-hover`.
* **No Side Effects:** No DOM or JS interop; strings are pure and composable.

---

## Examples

### 1) Basic Construction and CSS

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

// Compose primitives (examples)
var palette = new AllyariaPalette(Colors.Grey50, Colors.Grey900);
var type    = new AllyariaTypography(new AllyariaStringValue("system-ui, Segoe UI, Roboto, sans-serif"), new AllyariaNumberValue("1rem"));
var spacing = AllyariaSpacing.FromSingle(Sizing.Size2, Sizing.Size3);
var border  = AllyariaBorders.FromSingle(Sizing.Size0, new AllyariaStringValue("solid"), Sizing.Size1);

// Build the style
var style = new AllyariaStyle(palette, type, spacing, border);

// Emit base CSS declarations
string baseCss = style.ToCss();

// Emit hover CSS declarations
string hoverCss = style.ToCssHover();

// Emit disabled CSS declarations
string disabledCss = style.ToCssDisabled();
```

### 2) Namespaced CSS Variables

```csharp
// Variables for base/hover/disabled (spacing/border only once under base)
string vars = style.ToCssVars(" Card  -- Primary ");

/*
Example fragments (exact names depend on your Palette/Spacing/Borders emitters):
--card-primary-color-surface: #fafafa;
--card-primary-font-family: system-ui, Segoe UI, Roboto, sans-serif;
--card-primary-spacing-margin-top: 8px;
--card-primary-border-top-width: 1px;

--card-primary-disabled-color-surface: #f0f0f0;
--card-primary-hover-color-surface: #f5f5f5;
*/
```

### 3) Deriving State Styles Automatically

```csharp
// Omit hover/disabled parameters; they are derived/defaulted internally
var derived = new AllyariaStyle(palette, type, spacing, border);

// PaletteHover is palette.ToHoverPalette()
// PaletteDisabled is palette.ToDisabledPalette()
// TypographyHover/Disabled default to base typography
string derivedVars = derived.ToCssVars("button");
```

> *Rev Date: 2025-10-03*
