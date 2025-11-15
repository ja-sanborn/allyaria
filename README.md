# Allyaria

> *Version 1: 2025-11-15*
>
> [![Tests](https://github.com/ja-sanborn/allyaria/actions/workflows/tests.yml/badge.svg?branch=main)](https://github.com/ja-sanborn/allyaria/actions/workflows/tests.yml)
>
> [![Coverage](https://ja-sanborn.github.io/allyaria/badge_linecoverage.svg)](https://ja-sanborn.github.io/allyaria/)

**Allyaria** is a *Blazor Component Library* for modern .NET apps, built with accessibility and localization at its
core. It is flexible, customizable, and extensible, written entirely in Blazor with minimal JavaScript interop.

---

## Allyaria.Abstractions

**Allyaria.Abstractions** defines the foundational contracts, validation utilities, and result-handling primitives for
the Allyaria framework.
It provides lightweight, dependency-free base components intended for consistent error management, input validation, and
functional-style operation results across the entire Allyaria ecosystem.

### Key Features

* **Structured Exception Model**  
  Includes core exception types like `AryException`, `AryArgumentException`, and `AryInvalidOperationException` that
  introduce structured error codes and standardized error handling semantics.

* **Result-Oriented Operations**  
  Implements the `AryResult` and `AryResult<T>` types for modeling success and failure outcomes without relying on
  exceptions for control flow.

* **Validation Framework**  
  Offers fluent and guard-based validation through `AryGuard`, `AryChecks`, `AryValidation<T>`, and
  `AryValidationExtensions`, enabling both declarative and imperative argument validation patterns.

* **Enum and String Extensions**  
  Provides `EnumExtensions` and `StringExtensions` for transforming identifiers, formatting names, and retrieving
  user-friendly descriptions from enums and text.

* **Generic Helpers**  
  Includes concise extension methods like `GenericExtensions.OrDefault()` for simplifying nullable value handling.

### Design Principles

* **Framework-Agnostic**  
  Works seamlessly across .NET applications, libraries, and services.

* **Zero External Dependencies**  
  Fully self-contained to ensure minimal footprint.

* **Consistency & Safety:**  
  Promotes predictable validation, consistent exception handling, and readable naming conventions.

* **Performance-Oriented:**  
  Uses caching and compiled expressions to reduce reflection and regex overhead.

> For additional details on available types and interfaces, see:
*[Abstractions API documentation](./docs/references/Abstractions)*.

---

## Allyaria.Theming

**Allyaria.Theming** provides the full styling, branding, and runtime theme-management system for the Allyaria UI
framework. It enables strongly typed, CSS-safe style values; brand-driven theme generation; composition of custom theme
overrides; and fully reactive theme-switching behavior at runtime.

### Key Features

* **Strongly Typed CSS Value System**  
  Includes a comprehensive set of style value types (`StyleColor`, `StyleLength`, `StyleFontWeight`, `StyleOverflow`,
  `StyleTextAlign`, `StyleTextTransform`, etc.)—all inheriting from `StyleValueBase` and implementing `IStyleValue`.
  Every value is validated, normalized, and guaranteed safe for CSS serialization.

* **Complete Theme Modeling & Composition**  
  Themes are built from structured domain types (`Brand`, `BrandVariant`, `BrandPalette`, `BrandFont`, `BrandTheme`).
  The system ensures consistent color tokens, spacing rules, component groupings, and variant families across each theme
  type.

* **Navigator-Driven Customization**  
  The `ThemeNavigator` API allows targeting specific theme slices—component types, component states, style types, and
  theme variants. Combined with `ThemeUpdater`, this enables precise and safe theme customization with full
  immutability.

* **Configurable Theme Overrides**  
  Consumers can extend or replace parts of a theme using `IThemeConfigurator` and `ThemeConfigurator`, supporting fluent
  `Override()` chaining. Built-in protections prevent modification of system themes, high-contrast themes, and readonly
  component states.

* **Reactive Runtime Theme Service**  
  The `IThemingService` and its implementation `ThemingService` manage the effective and stored theme types, generate
  global and per-component CSS, and raise `ThemeChanged` events when the active theme switches.

* **Automatic Dependency Injection Integration**  
  `ServiceCollectionExtensions.AddAllyariaTheming()` integrates theme creation, builder execution, brand loading,
  overrides, and service registration in a single DI-friendly extension method.

* **Safe, Declarative CSS Generation**  
  Every component and document-level style rule is generated through the theme engine using strongly typed style values,
  component states, and theme variants—eliminating raw CSS strings and preventing invalid output.

### Design Principles

* **Immutable & Deterministic**  
  Themes, navigators, and updaters follow immutability rules to ensure stable, predictable theme generation.

* **Strong Validation Guarantees**  
  Powered by the Abstractions package (`AryArgumentException`, guard utilities, enum metadata), preventing invalid CSS,
  illegal theme operations, and unsafe brand configurations.

* **Separation of Concerns**  
  Style values, theme navigation, theme generation, and DI registration are cleanly separated into dedicated namespaces.

* **Brand-Driven Theming**  
  Themes inherit their identity from `Brand` objects—enabling custom palettes, typography, and component groups to be
  reused or replaced per application.

* **Framework-Agnostic**  
  Works across Blazor, ASP.NET, MAUI, desktop, and console-hosted applications—anywhere .NET DI and CSS generation
  apply.

For additional details on available tokens, styles, and composition patterns, see the
*[Theming API documentation](./docs/references/Theming)*.

---

## License

**Allyaria** is licensed under the *[Mozilla Public License Version 2.0](./LICENSE)*.

---

## Installation

*Pending*

---

## Contributing

Thank you for your interest in contributing to the **Allyaria** project!

We welcome bug reports, feature requests, documentation improvements, and code contributions.

By contributing to **Allyaria**, you agree to abide by the *[Code of Conduct](./CODE_OF_CONDUCT.md)*.

Please see *[CONTRIBUTING.md](./CONTRIBUTING.md)* for details.
