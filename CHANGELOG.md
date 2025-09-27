# Change Log

## [0.0.1-alpha] 2025-09-26

### Added

* Initial repository scaffolding, base solution, core and unit test projects, centralized build and packages, and
  documentation placeholders.
* `Allyaria.Theming` project with:
    * Added `AllyariaColorValue` class in `Allyaria.Theming.Values`
        * Immutable, CSS-oriented color type with parsing, formatting, and conversion support
        * Supports hex, rgb(a), hsv(a), CSS Web names, and Material Design names
        * Provides conversions between RGBA and HSVA
        * Exposes multiple string forms (`HexRgb`, `HexRgba`, `Rgb`, `Rgba`, `Hsv`, `Hsva`)
        * Includes helpers: `HoverColor()` (20% lighten/darken) and `ShiftColor(percent)`
        * Implements value equality and total ordering by canonical `#RRGGBBAA`
    * Added `AllyariaStringValue` in `Allyaria.Theming.Values`
        * Immutable, validated wrapper for theme strings (trimmed; rejects null/whitespace)
        * Provides `Parse` and `TryParse` helpers
        * Implicit conversions: `string → AllyariaStringValue` and `AllyariaStringValue → string`
    * Added `AllyariaPalette` struct in `Allyaria.Theming.Styles`
        * Provides immutable, strongly typed palette for background, foreground, border, and hover states
        * Enforces theming precedence rules (background images > background colors, explicit overrides > defaults,
          borders opt-in)
        * Computes accessible foreground colors when not explicitly set
        * Supports `ToCss()` for inline CSS generation
        * Supports `ToCssVars(string prefix = "")` for emitting CSS custom properties
    * Added `AllyariaTypography` struct in `Allyaria.Theming.Styles`
        * Supports strongly typed typography definitions (font family, size, weight, style, spacing, alignment, etc.)
        * Provides `ToCss()` for inline CSS style strings
        * Provides `ToCssVars(string prefix = "")` for generating CSS custom properties
        * Emits only non-null properties for clean, minimal output
    * Added `AllyariaStyle` record struct in `Allyaria.Theming.Styles`
        * Immutable composition of `AllyariaPalette` (colors, backgrounds, borders) and `AllyariaTypography` (fonts,
          sizes, spacing)
        * Provides `ToCss()` for combined inline CSS output
        * Provides `ToCssVars(string prefix = "")` for exporting combined CSS custom properties
        * Implements value semantics via record struct equality
    * Added `Colors` static class in `Allyaria.Theming.Constants`
        * Provides strongly typed `AllyariaColorValue` properties for CSS Web and Material Design colors
        * Alphabetically organized for discoverability
        * Covers standard named colors (e.g., `Colors.Red`, `Colors.White`) and full Material tone sets (e.g.,
          `Colors.Blue500`, `Colors.GreenA400`)
    * Added `Styles` static class in `Allyaria.Theming.Constants`
        * Provides predefined `AllyariaStyle` presets: `Light`, `Dark`, and `HighContrast`
        * Each preset combines an `AllyariaPalette` (foreground/background/borders) with `AllyariaTypography` (font
          family/size)
        * Supports `ToCss()`, and `ToCssVars(string prefix = "")` through the underlying `AllyariaStyle`
        * Serves as accessible defaults for light, dark, and high-contrast theming scenarios

### Updated/Fixed

* none

### Removed

* none

### Breaking

* none
