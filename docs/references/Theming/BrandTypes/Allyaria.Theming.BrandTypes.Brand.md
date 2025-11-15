# Allyaria.Theming.BrandTypes.Brand

`Brand` is a sealed record that encapsulates both the typography and theme configuration used by a brand within the
Allyaria theming system. It defines the complete font stack via `BrandFont`, and houses all theme variants—light, dark,
and their inverse derivatives—through `BrandVariant`.

## Summary

`Brand` consolidates the brand’s identity into two main components:

* A `BrandFont` instance describing sans-serif, serif, and monospace text families.
* A `BrandVariant` instance providing all theme variants (light, dark, light-inverse, dark-inverse).

This type acts as the root configuration object for any brand adopting Allyaria’s theming architecture.

## Constructors

`Brand(BrandFont? font = null, BrandTheme? lightTheme = null, BrandTheme? darkTheme = null)` Initializes a brand
configuration:

* If `font` is null, a new `BrandFont` with default values is used.
* A new `BrandVariant` is created from the provided (or default) `lightTheme` and `darkTheme`.

## Properties

| Name      | Type           | Description                                                                                |
|-----------|----------------|--------------------------------------------------------------------------------------------|
| `Font`    | `BrandFont`    | The brand’s typography configuration, including sans-serif, serif, and monospace families. |
| `Variant` | `BrandVariant` | The complete set of theme variants (light, dark, and their inverses).                      |

## Methods

| Name                        | Returns | Description                                                                                                |
|-----------------------------|---------|------------------------------------------------------------------------------------------------------------|
| `CreateHighContrastBrand()` | `Brand` | Creates a preconfigured high-contrast brand including WCAG-optimized colors for both light and dark modes. |

## Operators

| Operator                               | Returns | Description                                                            |
|----------------------------------------|---------|------------------------------------------------------------------------|
| `operator ==(Brand left, Brand right)` | `bool`  | Returns `true` if `left` and `right` are equal; otherwise `false`.     |
| `operator !=(Brand left, Brand right)` | `bool`  | Returns `true` if `left` and `right` are different; otherwise `false`. |

---

## Events

*None*

---

## Exceptions

*None*

---

## Example

```csharp
using Allyaria.Theming.BrandTypes;
using Allyaria.Theming.Constants;
using Allyaria.Theming.Types;

public class BrandManager
{
    public Brand CreateDefaultBrand()
    {
        // Uses default fonts and StyleDefaults-based themes.
        return new Brand();
    }

    public Brand CreateCustomBrand()
    {
        var font = new BrandFont(
            sansSerif: "Inter, system-ui, sans-serif",
            serif: "Merriweather, serif",
            monospace: "Fira Code, monospace"
        );

        var lightTheme = new BrandTheme(
            surface: new HexColor("#FFFFFF"),
            primary: new HexColor("#4F46E5"),
            secondary: new HexColor("#3B82F6"),
            tertiary: new HexColor("#A855F7"),
            error: new HexColor("#EF4444"),
            warning: new HexColor("#F59E0B"),
            success: new HexColor("#10B981"),
            info: new HexColor("#0EA5E9")
        );

        return new Brand(font: font, lightTheme: lightTheme);
    }

    public Brand CreateHighContrast()
    {
        return Brand.CreateHighContrastBrand();
    }
}
```

---

*Revision Date: 2025-11-15*
