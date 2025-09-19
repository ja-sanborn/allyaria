# Allyaria

> *Version 1: 2025-09-19*
>
> [![Tests](https://github.com/ja-sanborn/allyaria/actions/workflows/tests.yml/badge.svg?branch=main)](https://github.com/ja-sanborn/allyaria/actions/workflows/tests.yml)

**Allyaria** is a *Blazor Component Library* for modern .NET apps, built with accessibility and localization at its
core. It is flexible, customizable, and extensible, written entirely in Blazor with minimal JavaScript interop.

## Features

### Theming Foundation

* `AllyariaColor` type for representing and manipulating colors.
    * Supports construction from multiple formats:
        * Hex codes (#RGB, #RGBA, #RRGGBB, #RRGGBBAA).
        * CSS `rgb()` / `rgba()`, `hsv()` / `hsva()`.
        * Standard CSS web color names.
        * Material Design color names.
    * Exposes properties for RGBA, HSVA, `HexRGB`, `HexRGBA`, `rgb(...)`, `rgba(...)`, `hsv(...)`, and `hsva(...)`.
    * Methods for CSS conversion, lightening/darkening (`ShiftColor`), and generating hover-friendly variants (
      `HoverColor`).
    * Implicit conversions to/from `string`.
    * Equality, comparison, and operator overloads (`==`, `!=`, `<`, `<=`, `>`, `>=`).

* `AllyariaPaletteItem` type for encapsulating a palette entry with background, border, and foreground colors.
    * Constructors for default, background-only, and background+foreground initialization.
    * Properties for background, border, and foreground colors with fallback precedence rules.
    * Automatic hover color derivation when not explicitly set.
    * `HasBackground` and `HasBorder` flags to control rendering.
    * Optional `BackgroundImage` property with 50% overlay handling.
    * Methods:
        * `ToCss()` — generates CSS declarations for the normal state.
        * `ToHoverCss()` — generates CSS declarations for the hover state.
        * `ToCssVars()` — emits CSS custom properties (`--aa-*`) for component-scoped theming.
        * `ToString()` — delegates to `ToCss()`.

## License

* **Allyaria** is licensed under the *Mozilla Public License Version 2.0*.

## Installation

* Pending

## Contributing

* Pending
