# Theming

> *Version 1: 2025-09-18*

Allyaria Editor provides a strongly typed theming API for colors, borders, and backgrounds via `AllyariaTheme`. Themes
are applied by the control at runtime and update immediately when parameters change. **Do not rely on global CSS
overrides**—the component applies **inline styles** with strict precedence, and component CSS remains scoped via
isolation.

---

## 1. Core Principles

* **Blazor-first:** Themable through C# (`AllyariaTheme`), not by ad-hoc CSS variables or global overrides.
* **Accessibility-first:** Built-in presets maintain WCAG 2.2 AA contrast; background images get overlays for
  readability.
* **Localization-ready:** No user-facing text in themes; any textual labels remain localized in components.
* **Extensibility:** Clear precedence rules allow predictable overrides without fragile CSS hacks.

---

## 2. Theme Types

* **System** – Honors OS/browser preferences. If high contrast (forced colors) is active, uses `HighContrast`; otherwise
  selects `Dark` or `Light` based on `prefers-color-scheme`.
* **Light** – WCAG-compliant light preset.
* **Dark** – WCAG-compliant dark preset.
* **HighContrast** – Grayscale preset with maximum clarity and contrast.

---

## 3. Public API (Allyaria.Editor.Theming)

* `enum ThemeType { System, Light, Dark, HighContrast }`
* `class AllyariaTheme` with properties for:

    * **Transparency & Outline**: `Transparent`, `Outlined`
    * **Borders**: `BorderColor`
    * **Background**: `BackgroundImage`, region backgrounds (toolbar/content/status)
    * **Foregrounds**: region foregrounds
    * **Caret**: `CaretColor`

> Components accept an `AllyariaTheme` instance and/or `ThemeType`. Runtime changes are reflected immediately.

---

## 4. Precedence Rules

**Background**

1. `Transparent = true` → No background anywhere; parent background shows through.
2. `BackgroundImage` set & `Transparent = false` →

    * Apply an overlay at 50% opacity (white for Light, black for Dark, base tone for HighContrast).
    * Render the background image with `cover center no-repeat`.
    * Ignore region background colors while image is active.
3. Else → Region background colors or theme defaults.

**Foreground**

* Explicit value > Theme preset > Fallback.

**Caret**

* Explicit value > Theme preset > Fallback.

**Border**

* Explicit value > Theme preset > Fallback. `Outlined = false` removes border entirely.

---

## 5. Accessibility

* Built-in presets meet **WCAG 2.2 AA** contrast.
* Placeholder text uses **50% opacity** of the content foreground color for readability.
* Background images always receive a **50%** theme-appropriate overlay.
* ARIA roles/labels remain localized and unaffected by theme.

---

## 6. Runtime Behavior

* Theme changes at runtime are **immediately applied**—no reload required.
* With `ThemeType.System`, detection runs on first render:

    * Forced colors (high contrast) take precedence over dark/light.
    * Subsequent system theme changes should be reflected on next render cycle or via a lightweight interop listener (if
      enabled).

---

## 7. Integration with Razor/CSS/JS

* **Razor**: Components expose parameters for `Theme` / `ThemeType`; no inline `<style>`—styles come from the theme
  engine.
* **CSS (isolation)**: Keep selectors shallow; do **not** attempt to re-theme via CSS variables. The theme engine sets
  inline styles to ensure precedence.
* **JS (optional)**: Minimal interop may listen for system theme changes; provide `init/dispose` and respect reduced
  motion.

---

## 8. Example (Usage)

```razor
<AllyariaEditor ThemeType="ThemeType.System"
               Theme="new AllyariaTheme { Outlined = true, CaretColor = "#5b9bd5" }" />
```

**Effective behavior**

* If system is High Contrast → use `HighContrast` preset regardless of Dark/Light.
* Caret color overrides preset.
* If later `BackgroundImage` is set and `Transparent` is false → overlay + image take precedence over region
  backgrounds.

---

## 9. Out of Scope

* Per-user theme persistence.
* Animated transitions between themes.
* Non-color theming (typography, spacing, etc.).

---

## 10. Governance

* **Definition of Done** includes:

    * Theme respects precedence rules and updates at runtime.
    * Contrast checks pass for Light/Dark/HighContrast.
    * No reliance on global CSS overrides or ad-hoc CSS variables.
    * Localized UI remains unaffected by theme changes.
* PRs must document any deviations from the precedence rules.
