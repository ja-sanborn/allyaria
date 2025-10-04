# Allyaria.Theming.Constants.Styles

`Styles` is a static class that provides **predefined style presets** for Allyaria’s theming system.
These presets combine **palette, typography, and spacing** into WCAG-compliant configurations aligned with Material
Design guidelines.
They serve as **safe defaults** for light, dark, and high-contrast themes, while also exposing individual constants for
direct reuse or composition.

## Constructors

*None*

## Properties

| Name                       | Type                 | Description                                                                               |
|----------------------------|----------------------|-------------------------------------------------------------------------------------------|
| `DefaultPalette`           | `AllyariaPalette`    | The default palette (light surface `Grey50` with dark foreground `Grey900`).              |
| `DefaultSpacing`           | `AllyariaSpacing`    | Default spacing (8px margins, 16px paddings), aligned with Material Design 8dp grid.      |
| `DefaultStyleDark`         | `AllyariaStyle`      | WCAG-compliant dark theme (dark surface `Grey900`, light foreground `Grey50`).            |
| `DefaultStyleHighContrast` | `AllyariaStyle`      | High-contrast grayscale theme (white surface, black text). Useful for maximum legibility. |
| `DefaultStyleLight`        | `AllyariaStyle`      | WCAG-compliant light theme (light surface `Grey50`, dark foreground `Grey900`).           |
| `DefaultTypography`        | `AllyariaTypography` | Default typography: system-first font stack with a base size of `1rem`.                   |

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* Presets are **immutable** (`AllyariaStyle`, `AllyariaPalette`, `AllyariaSpacing`, `AllyariaTypography` instances).
* All presets are **WCAG 2.2 AA compliant** by default.
* `DefaultThemeHighContrast` is designed for accessibility contexts requiring maximum legibility.
* `DefaultTypography` uses a system-first stack:

  ```
  system-ui, -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Helvetica, Arial, sans-serif
  ```
* The spacing values align with `Sizing` constants (`Size2 = 8px`, `Size3 = 16px`).

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Constants;

var darkTheme = Styles.DefaultThemeDark;
```

### Expanded Example

```csharp
using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;

public class ThemeManager
{
    public AllyariaStyle ActiveTheme { get; private set; } = Styles.DefaultThemeLight;

    public void ToggleTheme(bool useDarkMode, bool highContrast)
    {
        if (highContrast)
        {
            ActiveTheme = Styles.DefaultThemeHighContrast;
        }
        else
        {
            ActiveTheme = useDarkMode 
                ? Styles.DefaultThemeDark
                : Styles.DefaultThemeLight;
        }
    }

    public void ApplyToComponent(MyComponent component)
    {
        component.Style = ActiveTheme;
    }
}
```

> *Rev Date: 2025-10-03*
