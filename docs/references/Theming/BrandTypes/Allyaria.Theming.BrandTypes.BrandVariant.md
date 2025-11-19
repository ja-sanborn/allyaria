# Allyaria.Theming.BrandTypes.BrandVariant

`BrandVariant` is a sealed record representing a full set of brand theme variants designed to support adaptive light and
dark modes, plus their derived inverse variants. It provides four complete `BrandTheme` configurations: primary light,
primary dark, and two automatically generated inverse themes based on each mode’s foreground colors.

## Summary

`BrandVariant` enables a brand to define both light and dark theme families with minimal configuration. If no custom
themes are supplied, it automatically builds default Allyaria-compliant light and dark themes using `StyleDefaults`. It
also produces *variant* themes—`LightVariant` and `DarkVariant`—which invert or adapt the foreground colors from the
respective base themes to achieve high contrast and visual harmony.

## Constructors

`BrandVariant(BrandTheme? lightTheme = null, BrandTheme? darkTheme = null)` Initializes all four theme variants:

* **Dark**: Provided or constructed from `StyleDefaults` (dark theme defaults).
* **DarkVariant**: Built by using the foreground colors from `Dark`’s semantic and surface palettes.
* **Light**: Provided or constructed from `StyleDefaults` (light theme defaults).
* **LightVariant**: Built by using the foreground colors from `Light`’s semantic and surface palettes.

## Properties

| Name           | Type         | Description                                                                             |
|----------------|--------------|-----------------------------------------------------------------------------------------|
| `Dark`         | `BrandTheme` | The primary dark theme for the brand.                                                   |
| `DarkVariant`  | `BrandTheme` | The derived inverse of the dark theme, built from the dark theme’s foreground colors.   |
| `Light`        | `BrandTheme` | The primary light theme for the brand.                                                  |
| `LightVariant` | `BrandTheme` | The derived inverse of the light theme, built from the light theme’s foreground colors. |

## Methods

*None*

## Operators

| Operator                                             | Returns | Description                                                            |
|------------------------------------------------------|---------|------------------------------------------------------------------------|
| `operator ==(BrandVariant left, BrandVariant right)` | `bool`  | Returns `true` if `left` and `right` are equal; otherwise `false`.     |
| `operator !=(BrandVariant left, BrandVariant right)` | `bool`  | Returns `true` if `left` and `right` are different; otherwise `false`. |

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.BrandTypes;
using Allyaria.Theming.Types;
using Allyaria.Theming.Constants;

public class BrandThemeManager
{
    public BrandVariant BuildVariants()
    {
        // Create a fully custom light theme and let BrandVariant generate everything else:
        var customLight = new BrandTheme(
            surface: new HexColor("#FFFFFF"),
            primary: new HexColor("#4F46E5"),
            secondary: new HexColor("#3B82F6"),
            tertiary: new HexColor("#A855F7"),
            error: new HexColor("#DC2626"),
            warning: new HexColor("#F59E0B"),
            success: new HexColor("#10B981"),
            info: new HexColor("#0284C7")
        );

        return new BrandVariant(lightTheme: customLight);
    }

    public void UseVariant(BrandVariant variant)
    {
        // Access light theme colors
        var primaryLightFg = variant.Light.Primary.Default.ForegroundColor;

        // Access inverse (variant) theme colors
        var invertedPrimary = variant.LightVariant.Primary.Default.BackgroundColor;

        // Access dark theme surface colors
        var darkSurface = variant.Dark.Surface.Default.BackgroundColor;
    }
}
```

---

*Revision Date: 2025-11-15*
