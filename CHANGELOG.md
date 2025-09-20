# Change Log

## [0.0.1-alpha] 2025-09-19

### Added

* Initial repository scaffolding, base solution, core and unit test projects, centralized build and packages, and
  documentation placeholders.
* `Allyaria.Theming` project with:
    * Initial `AllyariaColor` implementation, including:
        * Color parsing from hex, RGB(A), HSV(A), web names, and Material color names.
        * Properties for RGBA, HSVA, HexRgb/HexRgba, ToString, and CSS conversion.
        * Color adjustment methods (`ShiftColor`, `HoverColor`).
        * Implicit conversions, equality, ordering, and operator overloads.
    * New `AllyariaPaletteItem` implementation, including:
        * Constructors for default, background-only, and background+foreground initialization.
        * Properties for background, border, and foreground colors with precedence and fallbacks.
        * Hover color and background image support with overlay handling.
        * `HasBackground` and `HasBorder` flags controlling rendering.
        * Methods `ToCss`, `ToHoverCss`, `ToCssVars`, and `ToString` for dynamic CSS generation.
    * New `AllyariaTypoItem` implementation, including:
        * Constructor with optional parameters for font family, size, style, weight, spacing, line-height, alignment,
          decoration, transform, vertical-align, and word-spacing.
        * Validation and normalization of values (keywords lowercased, bare numbers assumed `px`, `var()` and `calc()`
          passed through).
        * Properties throw `ArgumentException` on invalid inputs.
        * `ToCss` builds single-line CSS declarations in fixed order, skipping null/whitespace.
        * `ToString` returns the same as `ToCss`.

### Updated/Fixed

* none

### Removed

* none

### Breaking

* none
