# Allyaria.Theming.BrandTypes.BrandState

`BrandState` is a sealed record representing a full set of brand color palettes corresponding to the various UI
interaction states. Each state-specific palette is automatically derived from a base `HexColor` by applying Allyaria's
color-transformation utilities (hover, pressed, disabled, focus, visited, etc.).

## Summary

`BrandState` generates a complete collection of `BrandPalette` instances for all supported interaction states using a
single base color. This allows brand themes to maintain consistent tonal relationships and contrast safety across hover,
focus, pressed, disabled, and visited states.

## Constructors

`BrandState(HexColor color)` Creates a new `BrandState` using the specified base color. Each property is assigned a new
`BrandPalette` instance representing a tonal/interaction variant:

* `Default` uses the original color.
* `Disabled` uses `color.ToDisabled()`.
* `Dragged` uses `color.ToDragged()`.
* `Focused` uses `color.ToFocused()`.
* `Hovered` uses `color.ToHovered()`.
* `Pressed` uses `color.ToPressed()`.
* `Visited` uses `color.ToVisited()`.

## Properties

| Name       | Type           | Description                                                     |
|------------|----------------|-----------------------------------------------------------------|
| `Default`  | `BrandPalette` | Palette for the default state.                                  |
| `Disabled` | `BrandPalette` | Palette for the disabled/inactive state, with reduced contrast. |
| `Dragged`  | `BrandPalette` | Palette for the dragged/moving state.                           |
| `Focused`  | `BrandPalette` | Palette for focus visibility (e.g., keyboard navigation).       |
| `Hovered`  | `BrandPalette` | Palette shown when the pointer is over an element.              |
| `Pressed`  | `BrandPalette` | Palette used during click/tap/active interaction states.        |
| `Visited`  | `BrandPalette` | Palette representing previously interacted or visited elements. |

## Methods

*None*

## Operators

| Operator                                         | Returns | Description                                                            |
|--------------------------------------------------|---------|------------------------------------------------------------------------|
| `operator ==(BrandState left, BrandState right)` | `bool`  | Returns `true` if `left` and `right` are equal; otherwise `false`.     |
| `operator !=(BrandState left, BrandState right)` | `bool`  | Returns `true` if `left` and `right` are different; otherwise `false`. |

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.BrandTypes;
using Allyaria.Theming.Types;

public class BrandThemeBuilder
{
    public BrandState BuildBrandState()
    {
        HexColor brandBase = new HexColor("#5A31F4"); // Example brand color
        return new BrandState(brandBase);
    }

    public void UseBrandState(BrandState state)
    {
        var defaultFg = state.Default.ForegroundColor;
        var hoveredAccent = state.Hovered.AccentColor;
        var disabledBg = state.Disabled.BackgroundColor;
    }
}
```

---

*Revision Date: 2025-11-15*
