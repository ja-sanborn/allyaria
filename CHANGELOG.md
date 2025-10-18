# Change Log

## [0.0.1-alpha] 2025-10-18

### Added

* Initial repository scaffolding, base solution, core and unit test projects, centralized build and packages, and
  documentation placeholders.

* `Allyaria.Abstractions` project with:

    * Foundational interfaces and base contracts for component lifecycle, dependency resolution, and resource
      propagation.
    * Core abstractions for theming, localization, and accessibility behaviors shared across Allyaria components.
    * Dependency injection–friendly architecture enabling loose coupling, testability, and extensibility.
    * API documentation for all public abstractions and contracts.

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
