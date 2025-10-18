# Allyaria

> *Version 1: 2025-10-18*
>
> [![Tests](https://github.com/ja-sanborn/allyaria/actions/workflows/tests.yml/badge.svg?branch=main)](https://github.com/ja-sanborn/allyaria/actions/workflows/tests.yml)
>
> [![Coverage](https://ja-sanborn.github.io/allyaria/badge_linecoverage.svg)](https://ja-sanborn.github.io/allyaria/)

**Allyaria** is a *Blazor Component Library* for modern .NET apps, built with accessibility and localization at its
core. It is flexible, customizable, and extensible, written entirely in Blazor with minimal JavaScript interop.

## Abstractions Features

The **Allyaria.Abstractions** project defines the foundational contracts, interfaces, and base types that make all
Allyaria components interoperable and extensible. It establishes a consistent architectural layer for dependency
injection, event handling, and state communication between components without requiring concrete implementations. This
ensures that core behaviors like theming, localization, and accessibility can be customized or replaced by developers
while maintaining a unified API surface.

At its core, the Abstractions layer includes interfaces for component lifecycle coordination, dependency resolution, and
style or resource propagation. These abstractions standardize how Allyaria components exchange data, manage parameters,
and respond to user interactions or accessibility states. By separating contracts from implementation, the library
enables third-party extensions and testing scenarios with minimal coupling.

The abstraction model promotes **clean architecture**, **testability**, and **composition-first design**. Developers can
use the provided base contracts (such as for rendering, configuration, or state binding) to build new
Allyaria-compatible components, services, or themes. Because all higher-level features — including Theming and Editor
integrations — depend on these abstractions, projects can evolve independently while sharing a reliable, versioned
foundation.

For additional details on available types and interfaces, see the
*[Abstractions API documentation](./docs/references/Abstractions)*.

## Theming Features

The **Allyaria.Theming** project provides a consistent, accessibility-first way to style Blazor components. It is built on
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

## License

**Allyaria** is licensed under the *[Mozilla Public License Version 2.0](./LICENSE)*.

## Installation

*Pending*

## Contributing

Thank you for your interest in contributing to the **Allyaria** project!

We welcome bug reports, feature requests, documentation improvements, and code contributions.

By contributing to **Allyaria**, you agree to abide by the *[Code of Conduct](./CODE_OF_CONDUCT.md)*.

Please see *[CONTRIBUTING.md](./CONTRIBUTING.md)* for details.
