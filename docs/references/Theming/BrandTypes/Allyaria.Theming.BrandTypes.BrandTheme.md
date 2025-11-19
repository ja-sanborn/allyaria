# Allyaria.Theming.BrandTypes.BrandTheme

`BrandTheme` is a sealed record representing a complete brand-level theme configuration.
It defines color states for multiple semantic categories (primary, secondary, error, etc.) and Material-style elevation
layers. Each state is represented by a `BrandState`, which itself contains palettes for all UI interaction states (
default, hovered, pressed, disabled, etc.).

## Summary

`BrandTheme` creates a fully derived theme from optional base colors for surface, semantic palettes, and feedback
states. It automatically generates per-elevation tonal variations and wraps each color into a `BrandState` so every
state (hover, pressed, focused, etc.) is available for all semantic groups. This allows brands to define a theme with
minimal input while ensuring consistent color behavior across UI states and component elevations.

## Constructors

`BrandTheme(HexColor? surface = null, HexColor? primary = null, HexColor? secondary = null, HexColor? tertiary = null, HexColor? error = null, HexColor? warning = null, HexColor? success = null, HexColor? info = null)`
Creates a new `BrandTheme` using the provided base colors or falls back to `StyleDefaults`:

* Elevation layers (`Elevation1`–`Elevation5`) are derived from `surface` using `ToElevationX()` helpers.
* Semantic palettes (`Primary`, `Secondary`, `Tertiary`, `Error`, `Warning`, `Success`, `Info`) default to corresponding
  values in `StyleDefaults`.
* A `BrandState` is constructed for each category, deriving palettes for all interaction states.

## Properties

| Name         | Type         | Description                                                                |
|--------------|--------------|----------------------------------------------------------------------------|
| `Elevation1` | `BrandState` | Color state for elevation level 1 (lowest raised surfaces).                |
| `Elevation2` | `BrandState` | Color state for elevation level 2 (slightly raised surfaces).              |
| `Elevation3` | `BrandState` | Color state for elevation level 3 (medium-elevation surfaces).             |
| `Elevation4` | `BrandState` | Color state for elevation level 4 (higher-depth regions).                  |
| `Elevation5` | `BrandState` | Color state for elevation level 5 (highest raised surfaces, e.g., modals). |
| `Error`      | `BrandState` | Color state for critical or validation feedback.                           |
| `Info`       | `BrandState` | Color state for informational or neutral alerts.                           |
| `Primary`    | `BrandState` | Color state for key actions and prominent UI elements.                     |
| `Secondary`  | `BrandState` | Color state for secondary or supporting accents.                           |
| `Success`    | `BrandState` | Color state for positive confirmations.                                    |
| `Surface`    | `BrandState` | Base surface color state for background UI regions.                        |
| `Tertiary`   | `BrandState` | Color state for subtle or decorative accents.                              |
| `Warning`    | `BrandState` | Color state for warnings and cautionary alerts.                            |

## Methods

*None*

## Operators

| Operator                                         | Returns | Description                                                            |
|--------------------------------------------------|---------|------------------------------------------------------------------------|
| `operator ==(BrandTheme left, BrandTheme right)` | `bool`  | Returns `true` if `left` and `right` are equal; otherwise `false`.     |
| `operator !=(BrandTheme left, BrandTheme right)` | `bool`  | Returns `true` if `left` and `right` are different; otherwise `false`. |

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.BrandTypes;
using Allyaria.Theming.Types;
using Allyaria.Theming.Constants;

public class ExampleThemeUsage
{
    public BrandTheme BuildCustomTheme()
    {
        // Build a theme with custom primary and surface colors, letting defaults fill the rest.
        return new BrandTheme(
            surface: new HexColor("#FAFAFA"),
            primary: new HexColor("#6200EE"),
            secondary: new HexColor("#03DAC6")
        );
    }

    public void AccessTheme(BrandTheme theme)
    {
        // Access derived palettes
        var primaryHoveredAccent = theme.Primary.Hovered.AccentColor;
        var elevation3Foreground = theme.Elevation3.Default.ForegroundColor;
        var warningPressedBg = theme.Warning.Pressed.BackgroundColor;
    }
}
```

---

*Revision Date: 2025-11-15*
