# Change Log

## [0.0.1-alpha] 2025-11-15

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

    * Strongly typed CSS value system (`StyleColor`, `StyleLength`, `StyleFontWeight`, `StyleOverflow`,
      `StyleTextAlign`, `StyleTextTransform`, and all other `Style…` types) implementing `IStyleValue` and validated
      through `StyleValueBase`.
    * Comprehensive theme domain model including `Brand`, `BrandVariant`, `BrandPalette`, `BrandFont`, `BrandTheme`, and
      related structures enabling fully brand-driven theme generation.
    * Navigator-based update targeting via `ThemeNavigator`, supporting selection of component types, component states,
      theme variants, and style categories.
    * Immutable theme customization pipeline using `ThemeUpdater`, `IThemeConfigurator`, and `ThemeConfigurator`, with
      built-in safeguards for restricted or system-defined theme values.
    * Reactive runtime theming support through `IThemingService` and its implementation `ThemingService`, enabling
      document-wide and component-scoped CSS generation as well as theme switching with the `ThemeChanged` event.
    * Dependency injection integration using `ServiceCollectionExtensions.AddAllyariaTheming()` for automatic theme
      building, brand initialization, override application, and service registration.
    * Full API documentation for all public types, including every style value, theme navigation type, theme
      configuration interface, theming service, and DI extension.

* Updated documentation:

    * Added API documentation for Abstractions and Theming.
    * Updated README with Abstractions summary, revised Theming section, and corrected documentation links.
