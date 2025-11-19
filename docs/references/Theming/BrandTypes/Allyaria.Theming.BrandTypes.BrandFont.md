# Allyaria.Theming.BrandTypes.BrandFont

`BrandFont` is a sealed record representing a brand’s font configuration, providing distinct font family values for
sans-serif, serif, and monospace text rendering. It ensures consistent typography across UI components while allowing
customization and fallback to built-in default font stacks.

## Summary

`BrandFont` is a simple, immutable value type used to supply font-family settings to the Allyaria theming system. It
supports custom font input and automatically falls back to `StyleDefaults` when no value is provided, ensuring safe and
predictable font configuration.

## Constructors

`BrandFont(string? sansSerif = null, string? serif = null, string? monospace = null)` Initializes a new `BrandFont`
instance. Whitespace or null values for any parameter are replaced with default fonts from `StyleDefaults`.

## Properties

| Name        | Type     | Description                                                         |
|-------------|----------|---------------------------------------------------------------------|
| `Monospace` | `string` | Gets the monospace font family name used for fixed-width text.      |
| `SansSerif` | `string` | Gets the sans-serif font family name used for general UI text.      |
| `Serif`     | `string` | Gets the serif font family name used for decorative or formal text. |

## Methods

*None*

## Operators

| Operator                                       | Returns | Description                                                            |
|------------------------------------------------|---------|------------------------------------------------------------------------|
| `operator ==(BrandFont left, BrandFont right)` | `bool`  | Returns `true` if `left` and `right` are equal; otherwise `false`.     |
| `operator !=(BrandFont left, BrandFont right)` | `bool`  | Returns `true` if `left` and `right` are different; otherwise `false`. |

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.BrandTypes;
using Allyaria.Theming.Constants;

public class BrandSetup
{
    public BrandFont Fonts { get; }

    public BrandSetup()
    {
        // Use full custom fonts
        Fonts = new BrandFont(
            sansSerif: "Inter, system-ui, sans-serif",
            serif: "Merriweather, serif",
            monospace: "Fira Code, monospace"
        );

        // Or rely on StyleDefaults for fallbacks
        var defaults = new BrandFont(); // All values pulled from StyleDefaults
    }
}
```

---

*Revision Date: 2025-11-15*
