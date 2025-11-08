# Allyaria

> *Version 1: 2025-11-08*
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

## Theming Features

The **Allyaria.Theming** project provides a consistent, accessibility-first way to style Blazor components. It is built
on
strongly typed design tokens (colors, typography, spacing, borders, and imagery) that can be combined into full style
bundles and safely cascaded. This enables developers to adopt predefined, WCAG-compliant defaults or extend them through
lightweight composition.

At the core is the `AllyariaStyle` bundle, which unifies a `Palette` (colors and images), `Typography` (fonts, weight,
spacing), `Spacing` (margins and paddings), and `Borders` (widths, radii, and styles). Each style may define hover,
focus, and disabled variants. Styles can be emitted as inline CSS, CSS variables, or dynamic tokens that support runtime
updates and theme transitions without re-rendering the component tree.

Themes are designed to be **adaptive, composable, and accessible**. Color palettes are validated to maintain proper
contrast ratios, and foregrounds are automatically adjusted when necessary to meet WCAG thresholds. Background imagery
can include layered contrast overlays, ensuring text legibility in all visual modes.

Developers can work with two main entry points:

* **Predefined defaults** – Accessible presets such as `DefaultThemeLight`, `DefaultThemeDark`, and
  `DefaultThemeHighContrast` are available out of the box. These leverage the shared token sets in `Colors` and
  `Sizing` and already meet WCAG contrast requirements.
* **Custom composition** – Any theme can be extended or overridden using the `Cascade` or `Compose` patterns. These
  allow merging or partial overrides (for example, redefining only the accent color or typography) while inheriting all
  other defaults.

By design, the theming layer integrates cleanly with Allyaria Blazor components. Each component consumes an
`AllyariaStyle` either from its own parameters or from a cascaded context. Because styles are emitted as dynamic CSS or
variable definitions, developers can easily integrate them into both static and runtime styling workflows.

Theme resolution follows a clear precedence model: **Component theme → Parent theme → Global theme → Fallback**. This
ensures predictable styling while preserving accessibility guarantees. The system’s validation layer continues to
enforce contrast, legibility, and color safety even under custom overrides.

For additional details on available tokens, styles, and composition patterns,, see the
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
