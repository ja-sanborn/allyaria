# Allyaria

> *Version 1: 2025-09-18*
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

## License

* **Allyaria** is licensed under the *Mozilla Public License Version 2.0*.

## Installation

* Pending

## Contributing

* Pending
