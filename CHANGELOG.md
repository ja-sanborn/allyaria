# Change Log

## [0.0.1-alpha] 2025-11-08

### Added

* Initial repository scaffolding, base solution, core and unit test projects, centralized build and packages, and
  documentation placeholders.

* `Allyaria.Abstractions` project with:

    * Core exception hierarchy (`AryException`, `AryArgumentException`, `AryInvalidOperationException`) for structured
      error handling.
    * `AryResult` and `AryResult<T>` types for functional-style success/failure result modeling.
    * Validation utilities including `AryGuard`, `AryChecks`, `AryValidation<T>`, and fluent `AryValidationExtensions`.
    * `EnumExtensions` for description and `[Flags]` enum formatting with caching.
    * `StringExtensions` for case conversion, normalization, and human-readable formatting.
    * `GenericExtensions` for simplified nullable value handling (`OrDefault`).
    * Full API documentation templates for all public types.

* `Allyaria.Theming` project with:

    * Strongly typed **design token system** covering color, typography, spacing, borders, and imagery.
    * Unified `AllyariaStyle` bundle combining palette, typography, spacing, and borders into composable style sets.
    * Support for runtime composition and cascading via `Cascade()` and `Compose()` patterns.
    * Dynamic token emission through inline CSS, CSS variables, and runtime theme updates without re-rendering.
    * Adaptive color and contrast validation with automatic WCAG foreground adjustments.
    * Predefined accessible presets: `DefaultThemeLight`, `DefaultThemeDark`, and `DefaultThemeHighContrast`.
    * Shared constants (`Colors`, `Sizing`) and helpers for consistent cross-component styling.
    * Expanded validation layer for ensuring accessible and predictable color behavior across custom overrides.

* Updated documentation:

    * Added API documentation for Abstractions and Theming.
    * Updated README with Abstractions summary, revised Theming section, and corrected documentation links.
