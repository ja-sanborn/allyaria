# Allyaria.Theming.Helpers.ColorHelper

`ColorHelper` is an internal static class providing **WCAG contrast computation and color adjustment utilities**.
It includes methods for scalar blending, sRGB interpolation, relative luminance, and hue-preserving contrast repair for
opaque colors.
All methods are allocation-free and optimized for theming scenarios—no alpha/canvas blending is performed.

## Constructors

*None*

## Properties

*None*

## Methods

| Name                                              | Returns              | Description                                                                                                              |
|---------------------------------------------------|----------------------|--------------------------------------------------------------------------------------------------------------------------|
| `Blend(start, target, t)`                         | `double`             | Linearly blends a scalar toward a target by factor `t ∈ [0,1]`.                                                          |
| `ContrastRatio(fg, bg)`                           | `double`             | Computes the WCAG contrast ratio between two opaque sRGB colors.                                                         |
| `EnsureMinimumContrast(fg, bg, minimumRatio=3.0)` | `ContrastResult`     | Adjusts the foreground color along HSV value (V) to meet or best-approach a minimum contrast ratio against a background. |
| `MixSrgb(a, b, t)`                                | `AllyariaColorValue` | Linear interpolation (LERP) in sRGB between two opaque colors.                                                           |
| `RelativeLuminance(color)`                        | `double`             | Computes WCAG relative luminance from an opaque sRGB color.                                                              |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* `EnsureMinimumContrast` preserves hue/saturation and adjusts only value (V). If target cannot be met along the HSV
  rail, it mixes toward black/white poles.
* `ContrastRatio` follows WCAG formula: `(Lighter + 0.05) / (Darker + 0.05)`.
* `Blend` and `MixSrgb` both clamp blend factor `t` to `[0,1]`.
* All methods are deterministic and allocation-free.
* Designed for opaque colors (`AllyariaColorValue`); alpha blending is not supported.

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Values;

// Contrast ratio between white and black
var ratio = ColorHelper.ContrastRatio(Colors.White, Colors.Black); // 21.0
```

### Expanded Example

```csharp
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Values;

public class ContrastDemo
{
    public ContrastResult EnsureReadable(AllyariaColorValue foreground, AllyariaColorValue background)
    {
        // Require WCAG AA body text ratio
        return ColorHelper.EnsureMinimumContrast(foreground, background, minimumRatio: 4.5);
    }

    public void ExampleUsage()
    {
        var fg = Colors.Grey500;
        var bg = Colors.White;

        var result = EnsureReadable(fg, bg);

        Console.WriteLine($"Adjusted: {result.Foreground}, Ratio: {result.ContrastRatio}, Met: {result.MeetsMinimum}");
    }
}
```

> *Rev Date: 2025-10-01*
