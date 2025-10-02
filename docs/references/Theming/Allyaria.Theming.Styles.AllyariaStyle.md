# Allyaria.Theming.Styles.AllyariaStyle

`AllyariaStyle` is an immutable record struct representing a combined style configuration.
It encapsulates **palette, typography, and spacing** for the base state, and derived or overridden values for the hover
and disabled states. Provides helpers to generate CSS fragments and CSS custom property declarations.

## Constructors

`AllyariaStyle(AllyariaPalette palette, AllyariaTypography typography, AllyariaSpacing spacing, AllyariaPalette? paletteHover = null, AllyariaTypography? typographyHover = null, AllyariaPalette? paletteDisabled = null, AllyariaTypography? typographyDisabled = null)`
Initializes a new style with palette, typography, and spacing. Hover and disabled values may be provided; if omitted,
they are derived from base palette/typography.

* Exceptions: *None*

## Properties

| Name                 | Type                 | Description                                                       |
|----------------------|----------------------|-------------------------------------------------------------------|
| `Palette`            | `AllyariaPalette`    | Base color palette.                                               |
| `PaletteDisabled`    | `AllyariaPalette`    | Palette for disabled state (derived if not provided).             |
| `PaletteHover`       | `AllyariaPalette`    | Palette for hover state (derived if not provided).                |
| `Spacing`            | `AllyariaSpacing`    | Margins and paddings applied across all states.                   |
| `Typography`         | `AllyariaTypography` | Base typography settings.                                         |
| `TypographyDisabled` | `AllyariaTypography` | Typography for disabled state (defaults to base if not provided). |
| `TypographyHover`    | `AllyariaTypography` | Typography for hover state (defaults to base if not provided).    |

## Methods

| Name                     | Returns  | Description                                                                                     |
|--------------------------|----------|-------------------------------------------------------------------------------------------------|
| `ToCss()`                | `string` | Builds CSS for the base state (palette + typography + spacing).                                 |
| `ToCssDisabled()`        | `string` | Builds CSS for the disabled state (disabled palette + disabled typography + spacing).           |
| `ToCssHover()`           | `string` | Builds CSS for the hover state (hover palette + hover typography + spacing).                    |
| `ToCssVars(prefix = "")` | `string` | Builds CSS variable declarations for base, disabled, and hover states, using normalized prefix. |

## Operators

| Operator    | Returns | Description          |
|-------------|---------|----------------------|
| `==` / `!=` | `bool`  | Equality comparison. |

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* `PaletteHover` defaults to `Palette.ToHoverPalette()` if not specified.
* `PaletteDisabled` defaults to `Palette.ToDisabledPalette()` if not specified.
* Typography hover/disabled default to base `Typography`.
* `ToCssVars` normalizes prefix:

    * `"Editor Theme"` → `--editor-theme-`
    * Empty → `--aa-`
    * Also appends `-disabled` and `-hover` for state-specific sets.
* Concatenated CSS strings are suitable for inline `style` attributes.

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Constants;

var style = new AllyariaStyle(
    Styles.DefaultPalette,
    Styles.DefaultTypography,
    Styles.DefaultSpacing
);

Console.WriteLine(style.ToCss());
// "background-color:#fafafa;color:#212121;font-family:...;margin:...;padding:..."
```

### Expanded Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Constants;

public class StyleDemo
{
    public void ApplyStyles()
    {
        var baseStyle = new AllyariaStyle(
            Styles.DefaultPalette,
            Styles.DefaultTypography,
            Styles.DefaultSpacing
        );

        var hoverCss = baseStyle.ToCssHover();
        var disabledCss = baseStyle.ToCssDisabled();
        var vars = baseStyle.ToCssVars("btn");

        Console.WriteLine("Base: " + baseStyle.ToCss());
        Console.WriteLine("Hover: " + hoverCss);
        Console.WriteLine("Disabled: " + disabledCss);
        Console.WriteLine("CSS Vars: " + vars);
    }
}
```

> *Rev Date: 2025-10-01*
