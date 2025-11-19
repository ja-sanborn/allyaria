# Allyaria

> *Version 1: 2025-11-18*
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

## Allyaria.Theming.Blazor

**Allyaria.Theming.Blazor** provides Blazor-specific integration for the Allyaria theming system. It exposes the
`AryThemeProvider` root component that bridges `IThemingService` to the browser, wiring up system theme detection,
persisted theme preferences, and document `dir`/`lang` attributes so your app reacts correctly to both user and OS-level
theme and culture changes.

### Key Features

* **Root-Level Theme Provider Component**  
  `AryThemeProvider` wraps your application UI and injects the effective theme via a Blazor `CascadingValue`, allowing
  any descendant component to access theme information while keeping all JS interop and state management centralized.

* **System Theme Detection & Sync**  
  Uses a co-located JavaScript module to read system-level preferences (Dark/Light/High Contrast) and map them to
  `ThemeType` values, automatically synchronizing the effective theme when `ThemeType.System` is selected.

* **Browser-Persisted Theme Preference**  
  Persists the stored theme type using safe `localStorage` access, restoring the user’s last choice on reload and
  keeping `IThemingService.StoredType` in sync with the browser.

* **Document Direction & Language Management**  
  Reads the Blazor `CultureInfo` (via `Culture` cascading parameter or `CurrentUICulture`) to set `dir` and `lang` on
  `document.documentElement` and toggle an `rtl` class on `<body>`, ensuring layout and typography respond correctly to
  RTL and LTR cultures.

* **Resilient, Minimal JS Interop**  
  Uses targeted, defensive JS interop calls with graceful fallbacks—failures during prerendering, navigation, or storage
  access are swallowed to avoid breaking the render loop while still keeping theme state consistent where possible.

* **Simple App-Level Integration**  
  Designed to be dropped into your `App.razor` (or another root component) with a minimal setup surface:

```razor
  @using Allyaria.Theming
  @using Allyaria.Theming.Blazor

  <AryThemeProvider>
      <Router AppAssembly="@typeof(App).Assembly">
          <Found Context="routeData">
              <RouteView RouteData="@routeData" />
          </Found>
          <NotFound>
              <p>Sorry, there's nothing at this address.</p>
          </NotFound>
      </Router>
  </AryThemeProvider>
```

### Design Principles

* **Blazor-First Integration**  
  Optimized for Blazor Server and WebAssembly hosting models, with lifecycle hooks and event handling tailored
  specifically to the Blazor render pipeline.

* **Single Source of Theme Truth**  
  Delegates all core theme logic to `IThemingService`, keeping the Blazor layer thin—its main responsibility is
  connecting DI, JS interop, and DOM attributes, not duplicating theme rules.

* **Culture-Aware UX**  
  Treats culture and theme as first-class concerns, ensuring the visual experience (direction, language, contrast)
  naturally aligns with the user’s locale and accessibility settings.

* **Fail-Safe Behavior**  
  Interop failures, storage issues, or teardown races are intentionally absorbed to avoid crashing or corrupting the UI;
  the provider always aims to keep the app usable, even if theme detection is temporarily unavailable.

For additional details on available components and integration patterns, see the
*[Theming.Blazor API documentation](./docs/references/Theming_Blazor)*.

---

## Allyaria.Components.Blazor

**Allyaria.Components.Blazor** provides the core UI component set for building accessible, theme-aware interfaces using
the Allyaria design system. These components integrate directly with the Allyaria theming engine, enabling fully
reactive styling, consistent ARIA semantics, and predictable behavior across all component states and theme modes.

This library contains the foundational component base class as well as a growing collection of concrete UI
components—such as surface containers, layout primitives, and interactive elements—all designed to function seamlessly
with Allyaria’s theming, accessibility, and localization layers.

### Key Features

* **Unified Component Base Class**
  All Allyaria Blazor components derive from a shared base (`AryComponentBase`), providing consistent ARIA attribute
  handling, theming integration, attribute filtering, and lifecycle behavior. This ensures every component behaves
  predictably and adheres to Allyaria accessibility expectations.

* **Theme-Aware Rendering**
  Components automatically integrate with the Allyaria theming pipeline, receiving effective theme values from
  `IThemingService` and rendering CSS variables, component-state styles, and theme-driven class names without manual
  wiring.

* **Accessible by Default**
  ARIA attributes—such as labels, descriptions, visibility hints, and roles—are first-class component parameters.
  Developers can augment these attributes while still benefiting from Allyaria's default accessibility guarantees.

* **Extensible, Framework-Native Components**
  The library includes general-purpose UI elements such as surface containers, wrappers, and layout helpers. These serve
  as building blocks for higher-level elements while remaining easy to extend in application or RCL projects.

* **Predictable Attribute & Style Merging**
  Components merge developer-supplied classes, inline styles, and arbitrary HTML attributes with Allyaria-managed
  values (ensuring required ARIA, class, ID, and style attributes remain consistent).

### Design Principles

* **Blazor-Native Architecture**
  Components are implemented using standard Razor component patterns with minimal or zero JavaScript interop.

* **Accessibility-Centric**
  Every component is built to ensure sensible ARIA defaults and predictable keyboard/tab behavior.

* **Fully Theme-Integrated**
  Style output is driven by theme rules, component state, and resolved theme type—without requiring manual CSS
  definitions.

* **Composable & Extensible**
  Components serve as foundational building blocks for app-level or library-level UI development, easily remixed or
  wrapped into custom controls.

### Example Usage

Below is a minimal example showing how a developer might use a component from this library—such as a themed surface
container—in a Blazor page:

```razor
@page "/example"
@using Allyaria.Components.Blazor

<ArySurface Class="example-surface"
            AriaLabel="Example surface">
    <h2>Hello from Allyaria.Components.Blazor</h2>
    <p>This content is rendered inside a themed Allyaria surface.</p>
</ArySurface>
```

For additional details on available components and integration patterns, see the
*[Components.Blazor API documentation](./docs/references/Components_Blazor)*.

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
