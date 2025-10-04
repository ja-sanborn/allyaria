# Allyaria

> *Version 1: 2025-10-03*
>
> [![Tests](https://github.com/ja-sanborn/allyaria/actions/workflows/tests.yml/badge.svg?branch=main)](https://github.com/ja-sanborn/allyaria/actions/workflows/tests.yml)
>
> [![Coverage](https://ja-sanborn.github.io/allyaria/badge_linecoverage.svg)](https://ja-sanborn.github.io/allyaria/)

**Allyaria** is a *Blazor Component Library* for modern .NET apps, built with accessibility and localization at its
core. It is flexible, customizable, and extensible, written entirely in Blazor with minimal JavaScript interop.

## Theming Features

The Allyaria theming system provides a consistent, accessibility-first way to style Blazor components. It is built on
strongly typed value objects (colors, typography, spacing, borders, images) that can be combined into full style bundles
and safely cascaded. This allows developers to adopt predefined, WCAG-compliant defaults or extend them with minimal
effort.

At the core is the `AllyariaStyle` bundle, which groups together a `Palette` (colors and images), `Typography` (fonts,
weight, spacing), `Spacing` (margins and paddings), and `Borders` (widths, radii, styles). Each style can also define
hover and disabled variants. Styles can be emitted as inline CSS or as scoped CSS variables, giving developers
flexibility: use variables for runtime theme switching and component isolation, or direct CSS for static scenarios.

Themes are designed to be **flexible, customizable, and accessible**. Color palettes are validated to ensure proper
contrasts are maintained, with foreground colors automatically adjusted if necessary to meet WCAG requirements.
Background images are layered with contrast overlays to guarantee legibility regardless of the underlying image.

Developers have two main entry points:

* **Predefined defaults** – Safe, accessible presets such as `DefaultThemeLight`, `DefaultThemeDark`, and
  `DefaultThemeHighContrast` are available for immediate use. These are built on the token sets provided in `Colors` and
  `Sizing`, and already meet contrast requirements.
* **Custom cascades** – Any theme can be extended or overridden using the `Cascade` pattern. For example, you can start
  with the light theme and cascade a new accent color or typography setting while inheriting all other defaults.

By design, the theming layer integrates cleanly with Allyaria Blazor components. Every component consumes an
`AllyariaStyle`, either from its own parameters or cascaded from parent/global settings. Because styles emit valid CSS
or CSS variables, developers can easily integrate them into their rendering strategy while still benefiting from
accessibility, flexibility, and predictable styling across the application.

Themes in Allyaria Blazor components respect a clear precedence model: **Component theme → Parent theme → Global theme →
Fallback**. This ensures that a developer can define styles as locally or globally as needed without breaking
accessibility or consistency. Defaults always guarantee readable foreground/background combinations, and overlays are
automatically applied to background images to maintain contrast.

For additional details on available tokens, style objects, and customization options, see the
*[theming API documentation](./docs/references/Theming)*.

## License

**Allyaria** is licensed under the *[Mozilla Public License Version 2.0](./LICENSE)*.

## Installation

*Pending*

## Contributing

Thank you for your interest in contributing to **Allyaria Editor**!

We welcome bug reports, feature requests, documentation improvements, and code contributions.

By contributing to Allyaria Editor, you agree to abide by the [Code of Conduct](./CODE_OF_CONDUCT.md).

Please see [CONTRIBUTING.md](./CONTRIBUTING.md) for details.
