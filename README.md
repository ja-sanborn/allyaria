# Allyaria

> *Version 1: 2025-09-27*
>
> [![Tests](https://github.com/ja-sanborn/allyaria/actions/workflows/tests.yml/badge.svg?branch=main)](https://github.com/ja-sanborn/allyaria/actions/workflows/tests.yml)
>
> [![Coverage](https://ja-sanborn.github.io/allyaria/badge_linecoverage.svg)](https://ja-sanborn.github.io/allyaria/)

**Allyaria** is a *Blazor Component Library* for modern .NET apps, built with accessibility and localization at its
core. It is flexible, customizable, and extensible, written entirely in Blazor with minimal JavaScript interop.

## Features

### AllyariaColorValue

`AllyariaColorValue` is an immutable, framework-agnostic color type with CSS-oriented parsing, formatting, and value
semantics.
It supports:

* **Parsing** from multiple formats:

    * Hex: `#RGB`, `#RGBA`, `#RRGGBB`, `#RRGGBBAA`
    * Functional: `rgb()`, `rgba()`, `hsv()`, `hsva()`
    * Named colors: CSS Web color names and Material Design palette names
* **Conversions** between RGBA and HSVA (Hue 0–360°, Saturation/Value 0–100%, Alpha 0–1)
* **Formatting** to string forms:

    * `HexRgb`, `HexRgba`
    * `Rgb`, `Rgba`
    * `Hsv`, `Hsva`
* **Equality & ordering** by canonical `#RRGGBBAA` string

All numeric operations use `InvariantCulture`, ensuring consistent, predictable behavior across cultures.

### AllyariaStringValue

`AllyariaStringValue` is an immutable, normalized wrapper for theme strings.
It guarantees **non-null, non-empty, non-whitespace** input and stores the **trimmed** value.

Key features:

* **Normalization & validation** — throws on `null`/empty/whitespace; value is trimmed.
* **Parsing helpers** — `Parse(string)` and `TryParse(string, out AllyariaStringValue?)`.
* **Ergonomic conversions** — implicit cast **from** `string` and **to** `string` for seamless usage.
* **Immutable value semantics** — safe to pass around and reuse in theming/style composition.

### AllyariaPalette

`AllyariaPalette` is an immutable, strongly typed palette used by the Allyaria theme engine.
It defines background, foreground, border, and hover states with clear **precedence rules**:

* **Background images** take precedence over background colors.
* **Explicit overrides** beat computed defaults.
* **Borders** are opt-in (width > 0) and may include color, style, and radius.
* **Foreground colors** default to contrast-safe values based on background lightness.
* **Hover state adjustment** — when a border is present, the effective background is slightly adjusted for better
  contrast.
* **CSS variable generation** — supports normalized prefixes and consistent naming for theming.
* **Cascade support** — the `Cascade()` method allows creating derived palettes by selectively overriding base values.

Conversion helpers are provided to:

* **Inline CSS styles** via `ToCss()`
* **CSS custom properties (variables)** via `ToCssVars(string prefix = "")`

This ensures consistent, accessible, and theme-driven styling across components.

### AllyariaTypography

`AllyariaTypography` is a strongly typed struct for defining typography within the Allyaria theming system.
It encapsulates font-related properties (family, size, weight, style, spacing, alignment, decoration, transform, etc.)
and provides conversion helpers to:

* **Inline CSS styles** via `ToCss()`
* **CSS custom properties (variables)** via `ToCssVars(string prefix = "")`
* **Cascade support** — the `Cascade()` method allows creating derived typography definitions by selectively overriding
  base values.

Only non-null values are emitted, making it safe to compose flexible, theme-driven typography definitions without
unnecessary noise in the resulting CSS.

### AllyariaStyle

`AllyariaStyle` is an immutable, strongly typed record-struct that **combines a palette and typography** into a single
style definition.

It provides:

* **Composition** of `AllyariaPalette` (colors, backgrounds, borders) and `AllyariaTypography` (fonts, sizes, spacing).
* **Inline CSS styles** via `ToCss()` — for normal state.
* **Inline CSS styles for hover states** via `ToCssHover()`.
* **CSS custom properties (variables)** via `ToCssVars(string prefix = "")` — exportable tokens for isolated CSS.

This ensures consistent, accessible, theme-driven styling across components, with **value semantics** for easy equality
checks and copy-by-value safety.

### Colors

The `Colors` class provides a consolidated, strongly typed library of named colors for Allyaria theming.

* **Includes** both **CSS Web colors** and **Material Design palette colors**.
* Each color is exposed as a static `AllyariaColorValue` property (e.g., `Colors.Red500`, `Colors.BlueA700`,
  `Colors.White`).
* Values are defined in canonical `#RRGGBBAA` form for consistency.
* Properties are alphabetically organized for quick lookup.
* Designed for convenient use in theming, palette composition, and inline style generation.

### Styles

The `Styles` class provides a set of **predefined theme presets** as ready-to-use `AllyariaStyle` instances.

Available presets:

* **Light** — Light UI preset (`Grey50` background, `Grey900` foreground) with sans-serif typography.
* **Dark** — Dark UI preset (`Grey900` background, `Grey50` foreground) with sans-serif typography.
* **HighContrast** — Maximum-contrast preset (`White` background, `Black` foreground) for accessibility.

Each preset combines an `AllyariaPalette` and `AllyariaTypography`, and supports:

* **Inline CSS styles** via `ToCss()`
* **Inline CSS styles for hover states** via `ToCssHover()`
* **CSS custom properties (variables)** via `ToCssVars()`

These serve as accessible defaults while allowing extension or replacement with custom brand themes.

## License

* **Allyaria** is licensed under the *Mozilla Public License Version 2.0*.

## Installation

* Pending

## Contributing

* Pending
