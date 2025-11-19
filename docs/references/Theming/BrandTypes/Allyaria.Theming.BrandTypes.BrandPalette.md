# Allyaria.Theming.BrandTypes.BrandPalette

`BrandPalette` is a sealed record representing a complete derived color palette for a brand theme. Starting from a
single base background color, it generates consistent, contrast-safe colors for foregrounds, accents, outlines, borders,
and decorations using Allyaria’s color utilities.

## Summary

`BrandPalette` creates a cohesive theme palette based on a provided `HexColor` background. It ensures WCAG-aligned
contrast by computing derived colors—including foreground, accent, border, caret, and text-decoration colors—using the
background as the reference point.

## Constructors

`BrandPalette(HexColor color)` Creates a new palette using the given background `color`. The constructor normalizes the
background to full opacity, derives safe foreground and accent colors, and computes consistent stylistic colors such as
caret and outline.

## Properties

| Name                  | Type       | Description                                                         |
|-----------------------|------------|---------------------------------------------------------------------|
| `AccentColor`         | `HexColor` | Highlight or emphasis color derived from the background.            |
| `BackgroundColor`     | `HexColor` | The base background color for the palette, always fully opaque.     |
| `BorderColor`         | `HexColor` | Color used for borders, derived from the background’s accent tone.  |
| `CaretColor`          | `HexColor` | Color for text input cursors; aligns with `ForegroundColor`.        |
| `ForegroundColor`     | `HexColor` | High-contrast foreground color computed from the background.        |
| `OutlineColor`        | `HexColor` | Color used for focus outlines, typically matching the accent color. |
| `TextDecorationColor` | `HexColor` | Color for link underlines and other text decorations.               |

## Methods

*None*

## Operators

| Operator                                             | Returns | Description                                                            |
|------------------------------------------------------|---------|------------------------------------------------------------------------|
| `operator ==(BrandPalette left, BrandPalette right)` | `bool`  | Returns `true` if `left` and `right` are equal; otherwise `false`.     |
| `operator !=(BrandPalette left, BrandPalette right)` | `bool`  | Returns `true` if `left` and `right` are different; otherwise `false`. |

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.BrandTypes;
using Allyaria.Theming.Types;

public class BrandTheme
{
    public BrandPalette Palette { get; }

    public BrandTheme()
    {
        // Build a palette from a brand background color
        HexColor brandBackground = new HexColor("#202124");
        Palette = new BrandPalette(brandBackground);
        
        // Access derived colors
        var fg = Palette.ForegroundColor;
        var accent = Palette.AccentColor;
        var border = Palette.BorderColor;
    }
}
```

---

*Revision Date: 2025-11-15*
