# Change Log

## [0.0.1-alpha] 2025-10-03

### Added

* Initial repository scaffolding, base solution, core and unit test projects, centralized build and packages, and
  documentation placeholders.
* `Allyaria.Theming` project with:

    * Added `AllyariaColorValue` class in `Allyaria.Theming.Values`

        * Immutable, CSS-oriented color type with parsing, formatting, and conversion support
        * Supports hex, rgb(a), hsv(a), CSS Web names, and Material Design names
        * Provides conversions between RGBA and HSVA
        * Exposes multiple string forms (`HexRgb`, `HexRgba`, `Rgb`, `Rgba`, `Hsv`, `Hsva`)
        * Implements value equality and total ordering by canonical `#RRGGBBAA`

    * Added `AllyariaImageValue` class in `Allyaria.Theming.Values`

        * Immutable, strongly typed CSS image token with canonical `url("…")` wrapping
        * Allows only safe URL schemes (`http`, `https`, `data`, `blob`) and normalizes inputs
        * Provides `Parse` and `TryParse` helpers
        * Provides CSS helpers like `ToCss()` and background emission helpers
        * Implicit conversions: `string → AllyariaImageValue` and `AllyariaImageValue → string`

    * Added `AllyariaNumberValue` class in `Allyaria.Theming.Values`

        * Normalized numeric type with optional CSS length unit (`px`, `rem`, `%`, etc.)
        * Parses signed/unsigned decimals, supports unit mapping from `LengthUnits` enum
        * Provides `Number` (double) and `LengthUnit` properties for numeric value and unit
        * Provides strict validation and canonical string formatting

    * Added `AllyariaStringValue` class in `Allyaria.Theming.Values`

        * Immutable, validated wrapper for theme strings (trimmed; rejects null/whitespace)
        * Provides `Parse` and `TryParse` helpers
        * Implicit conversions: `string → AllyariaStringValue` and `AllyariaStringValue → string`

    * Added `AllyariaBorders` struct in `Allyaria.Theming.Styles`

        * Strongly typed border definition (per-side width/style and per-corner radius)
        * Provides `FromSingle()` and `FromSymmetric()` factories
        * Supports `Cascade()` for non-destructive overrides
        * Generates inline CSS and CSS custom properties via `ToCss()` and `ToCssVars()`

    * Added `AllyariaPalette` struct in `Allyaria.Theming.Styles`

        * Immutable, strongly typed palette for background, foreground, border, hover, and disabled states
        * Enforces theming precedence rules (background images > background colors, explicit overrides > defaults)
        * Computes accessible foreground colors when not explicitly set
        * Supports hover and disabled state derivation while preserving hue
        * Provides `Cascade()` to create derived palettes with overrides
        * Provides `ToCss()` and `ToCssVars()` for inline CSS and custom property generation

    * Added `AllyariaSpacing` struct in `Allyaria.Theming.Styles`

        * Strongly typed spacing (margins and paddings) with per-side customization
        * Provides `FromSingle()` and `FromSymmetric()` factories
        * Supports `Cascade()` for non-destructive overrides
        * Generates inline CSS and CSS custom properties via `ToCss()` and `ToCssVars()`

    * Added `AllyariaTypography` struct in `Allyaria.Theming.Styles`

        * Strongly typed typography definitions (font family, size, weight, style, spacing, alignment, decoration,
          transform, etc.)
        * Provides `Cascade()` for non-destructive overrides
        * Provides `ToCss()` and `ToCssVars()` for clean, minimal CSS output

    * Added `AllyariaStyle` struct in `Allyaria.Theming.Styles`

        * Immutable composition of palette, typography, spacing, and borders into a unified style object
        * Provides base, hover, and disabled variants with automatic derivation when not specified
        * Generates full CSS for base, hover, and disabled states via `ToCss()`, `ToCssHover()`, `ToCssDisabled()`
        * Generates CSS custom properties with hover and disabled variants via `ToCssVars()`

    * Added `ColorHelper` static class in `Allyaria.Theming.Helpers`

        * WCAG-aware contrast ratio calculation and foreground adjustment
        * sRGB color mixing, scalar blending, and relative luminance computation

    * Added `ContrastResult` record struct in `Allyaria.Theming.Primitives`

        * Immutable result describing resolved foreground/background colors and achieved contrast

    * Added `Colors` static class in `Allyaria.Theming.Constants`

        * Strongly typed color definitions for CSS Web and Material Design palettes
        * Alphabetically organized for discoverability

    * Added `Sizing` static class in `Allyaria.Theming.Constants`

        * Material Design–compliant spacing and sizing constants based on a 4px/8px grid
        * Provides standard tokens like `Size0` (0px), `Thin` (1px), `Size2` (8px), `Size3` (16px), up to `Size11` (
          80px)
        * Encourages consistency across margins, paddings, and component sizing

    * Added `Styles` static class in `Allyaria.Theming.Constants`

        * Predefined WCAG-compliant style presets: `DefaultThemeLight`, `DefaultThemeDark`, `DefaultThemeHighContrast`
        * Combines default palette, typography, spacing, and borders into ready-to-use `AllyariaStyle` instances
        * Provides safe defaults for Material Design–inspired UIs and accessibility-focused contexts
        * Exposes `DefaultPalette`, `DefaultTypography`, `DefaultSpacing`, and `DefaultBorders` for customization and
          cascading
