# CSS Coding Standards (Blazor-focused)

> *Version 1: 2025-09-18*

These standards define how we write styles in this Blazor project using plain CSS. Prefer Blazor component structure and
CSS isolation. Keep styles scoped, minimal, and maintainable. The Allyaria Editor theming is configured via the
AllyariaTheme API rather than CSS variables or overrides.

---

## 1. Core Principles

* **Blazor-first:** Styles belong in `.razor.css` (CSS isolation), not global files.
* **Accessibility-first:** Clear focus indicators, WCAG 2.2 AA contrast, no color-only meaning.
* **Localization-ready:** Use logical properties for RTL/LTR support.
* **Theming-aware:** Theme values are applied via AllyariaTheme, not by CSS overrides.

---

## 2. Formatting

* Encoding: UTF-8
* Line endings: LF
* Final newline: required
* Trim trailing whitespace: yes
* Indentation: 4 spaces (no tabs)

---

## 3. File Organization

* Component styles live in `Component.razor.css`.
* Global styles may exist under `wwwroot/css/` but must remain minimal and purposeful.
* Never inject `<style>` directly in Razor files.

---

## 4. Naming & Class Conventions

* Use **kebab-case** for classes: `.product-card`, `.page-header`.
* Use **BEM** when helpful: `.card`, `.card__title`, `.card--compact`.
* Avoid IDs for styling; prefer classes.

---

## 5. Specificity & Overrides

* Keep selectors shallow.
* Avoid `!important` unless overriding narrow third-party styles.
* Prefer inline styles from components for dynamic theming.

---

## 6. Layout & Spacing

* Use **Flexbox** for one-dimensional layouts, **Grid** for two-dimensional.
* Prefer spacing tokens via CSS custom properties or shared variables (static only).
* Dynamic colors and theming come from `AllyariaTheme`.

---

## 7. States & Accessibility

* Style `:focus-visible` with a clear outline.
* Maintain WCAG 2.2 AA contrast.
* Do not convey information by color alone.

---

## 8. Motion

* Keep transitions small and fast.
* Respect `prefers-reduced-motion`.

---

## 9. RTL & Logical Properties

* Use logical properties (`margin-inline`, `padding-block`) for bidirectional layouts.
* Avoid physical properties (`margin-left`, `padding-right`) unless absolutely required.

---

## 10. Theming

* Colors, borders, and backgrounds are controlled by `AllyariaTheme`.
* Do not rely on CSS overrides for theming.
* Components apply inline styles with strict precedence.

---

## 11. Governance

* **Definition of Done** includes accessibility checks (contrast, focus), RTL readiness, and theme compliance.
* PRs must document deviations (e.g., use of `!important` for third-party overrides).

---

> Following these standards ensures consistent, accessible, and themeable styles in the Allyaria Blazor project.
