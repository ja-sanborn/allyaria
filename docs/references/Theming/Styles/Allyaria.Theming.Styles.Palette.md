# Allyaria.Theming.Styles.Palette

`Palette` is a strongly-typed record struct that represents a computed color palette for Allyaria themes. It
encapsulates `background`, `foreground`, `fill`, and `border` colors with theme-based defaults and intelligent cascading
behavior. The palette ensures accessible contrast ratios, derives border color from the surface context, and supports
high-contrast rendering modes.

## Constructors

`Palette()` Initializes a new `Palette` instance using theme defaults (Light mode) with no fill and derived border.

`Palette(ThemeColor? surfaceColor = null, ThemeColor? backgroundColor = null, ThemeColor? foregroundColor = null, ThemeColor? borderColor = null, bool isHighContrast = false)`
Initializes a new instance with optional overrides for surface, background, foreground, and border colors. Missing
values default to theme rules, and foreground contrast and border derivation are automatically computed.

## Properties

| Name              | Type         | Description                                                                                                                  |
|-------------------|--------------|------------------------------------------------------------------------------------------------------------------------------|
| `BackgroundColor` | `ThemeColor` | Gets or initializes the background fill color of the palette. Transparent fills are treated as absent for border derivation. |
| `BorderColor`     | `ThemeColor` | Gets or initializes the border color of the palette. Derived from other palette values when not explicitly specified.        |
| `ForegroundColor` | `ThemeColor` | Gets or initializes the foreground (text) color, automatically contrast-adjusted against the background.                     |
| `IsHighContrast`  | `bool`       | Indicates whether high-contrast rendering rules are active.                                                                  |
| `SurfaceColor`    | `ThemeColor` | Gets or initializes the surface color used in contrast and border derivation calculations.                                   |

## Methods

| Name           | Signature                                                                                                                                                                               | Description                                                                                                              | Returns   |
|----------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------|-----------|
| `Cascade`      | `Palette Cascade(ThemeColor? surfaceColor = null, ThemeColor? backgroundColor = null, ThemeColor? foregroundColor = null, ThemeColor? borderColor = null, bool? isHighContrast = null)` | Produces a new `Palette` by applying provided overrides while re-computing contrast and derived border color.            | `Palette` |
| `ToCss`        | `string ToCss(string? varPrefix = "")`                                                                                                                                                  | Builds CSS declarations for `background-color`, `color`, and `border-color` with optional variable prefix normalization. | `string`  |
| `ToDisabled`   | `Palette ToDisabled()`                                                                                                                                                                  | Returns a desaturated version of the palette suitable for disabled UI states.                                            | `Palette` |
| `ToDragged`    | `Palette ToDragged()`                                                                                                                                                                   | Returns a brightened version of the palette for dragged UI states.                                                       | `Palette` |
| `ToElevation1` | `Palette ToElevation1()`                                                                                                                                                                | Returns a slightly elevated palette (Elevation 1).                                                                       | `Palette` |
| `ToElevation2` | `Palette ToElevation2()`                                                                                                                                                                | Returns a moderately elevated palette (Elevation 2).                                                                     | `Palette` |
| `ToElevation3` | `Palette ToElevation3()`                                                                                                                                                                | Returns a higher elevated palette (Elevation 3).                                                                         | `Palette` |
| `ToElevation4` | `Palette ToElevation4()`                                                                                                                                                                | Returns a strongly elevated palette (Elevation 4).                                                                       | `Palette` |
| `ToFocused`    | `Palette ToFocused()`                                                                                                                                                                   | Returns a lighter version of the palette for focused states.                                                             | `Palette` |
| `ToHovered`    | `Palette ToHovered()`                                                                                                                                                                   | Returns a slightly lighter palette for hovered states.                                                                   | `Palette` |
| `ToPressed`    | `Palette ToPressed()`                                                                                                                                                                   | Returns a noticeably lighter palette for pressed states.                                                                 | `Palette` |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
var palette = new Palette(
    surfaceColor: StyleDefaults.BackgroundColorLight,
    backgroundColor: StyleDefaults.Transparent
);

string css = palette.ToCss("btn");
// Produces CSS such as:
// --btn-background-color: #ffffff;
// --btn-color: #000000;
// --btn-border-color: #dddddd;
```

---

*Revision Date: 2025-10-17*
