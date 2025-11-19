# Allyaria.Theming.Types.HexColor

`HexColor` is an immutable RGBA color value type with derived HSV components for UI theming. It represents a single
color with red, green, blue, and alpha channels (`HexByte`), automatically computes its HSV (`H`, `S`, `V`)
representation, supports parsing from multiple CSS-like string formats (hex, RGB(A), HSV(A), named colors), and provides
helpers for contrast, luminance, interpolation, and generating common UI state variants (hovered, pressed, disabled,
elevations, etc.).

## Constructors

`HexColor()` Initializes a new `HexColor` with all channels set to `0` (fully transparent black).

`HexColor(HexByte red, HexByte green, HexByte blue, HexByte? alpha = null)` Initializes a new `HexColor` from individual
`HexByte` channels, defaulting `alpha` to `255` (opaque) when `null`, and computes the derived `H`, `S`, and `V`
components.

`HexColor(string value)` Initializes a new `HexColor` by parsing a color string in hex, RGB(A), HSV(A), or named-color
form, normalizing channels and computing `H`, `S`, and `V`.

## Properties

| Name | Type      | Description                                                                                                                     |
|------|-----------|---------------------------------------------------------------------------------------------------------------------------------|
| `A`  | `HexByte` | Gets the alpha (opacity) channel.                                                                                               |
| `B`  | `HexByte` | Gets the blue channel.                                                                                                          |
| `G`  | `HexByte` | Gets the green channel.                                                                                                         |
| `H`  | `double`  | Gets the hue component in degrees in the range `[0, 360)`, where `0` = red, `120` = green, `240` = blue.                        |
| `R`  | `HexByte` | Gets the red channel.                                                                                                           |
| `S`  | `double`  | Gets the saturation component normalized to `[0, 1]`, where `0` is gray and `1` is fully saturated.                             |
| `V`  | `double`  | Gets the value (brightness) component normalized to `[0, 1]`, where `0` is black and `1` is the brightest version of the color. |

## Methods

| Name                                                                        | Returns    | Description                                                                                                                                                                        |
|-----------------------------------------------------------------------------|------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `CompareTo(HexColor other)`                                                 | `int`      | Compares this color with `other` using RGBA tuple ordering `(R, G, B, A)` and returns the relative sort order.                                                                     |
| `ContrastRatio(HexColor background)`                                        | `double`   | Computes the WCAG 2.2 contrast ratio between this color (foreground) and the given opaque `background` using relative luminance.                                                   |
| `Desaturate(double desaturateBy = 0.5, double valueBlendTowardMid = 0.15)`  | `HexColor` | Returns a new color with reduced saturation by `desaturateBy` (clamped to `[0,1]`) and brightness blended toward mid-tone (`V = 0.5`) by `valueBlendTowardMid`.                    |
| `EnsureContrast(HexColor background, double minimumRatio = 3.0)`            | `HexColor` | Adjusts this color to achieve at least `minimumRatio` contrast against `background`, primarily by moving along its HSV value rail and, if necessary, mixing toward black or white. |
| `Equals(HexColor other)`                                                    | `bool`     | Indicates whether this color has the same RGBA channels as `other`.                                                                                                                |
| `Equals(object? obj)`                                                       | `bool`     | Indicates whether `obj` is a `HexColor` with the same RGBA channels as this instance.                                                                                              |
| `FromHsva(double hue, double saturation, double value, double alpha = 1.0)` | `HexColor` | Creates a `HexColor` from HSVA components (hue in degrees, saturation/value in `[0,1]`, alpha in `[0,1]`, clamped as needed).                                                      |
| `GetHashCode()`                                                             | `int`      | Returns a hash code computed from the RGBA channels.                                                                                                                               |
| `Invert()`                                                                  | `HexColor` | Produces the photographic negative of this color by inverting each RGB channel (`R' = 255 - R`, etc.), preserving alpha.                                                           |
| `IsDark()`                                                                  | `bool`     | Returns `true` if the relative luminance of this color is less than `0.5`; otherwise `false`.                                                                                      |
| `IsLight()`                                                                 | `bool`     | Returns `true` if the relative luminance of this color is greater than or equal to `0.5`; otherwise `false`.                                                                       |
| `IsOpaque()`                                                                | `bool`     | Returns `true` if `A.Value` equals `255` (fully opaque); otherwise `false`.                                                                                                        |
| `IsTransparent()`                                                           | `bool`     | Returns `true` if `A.Value` equals `0` (fully transparent); otherwise `false`.                                                                                                     |
| `Parse(string value)`                                                       | `HexColor` | Parses the specified color string into a new `HexColor`, using the same rules as the `HexColor(string value)` constructor.                                                         |
| `SetAlpha(byte alpha)`                                                      | `HexColor` | Returns a new color with the same RGB channels but the specified `alpha` byte (0–255).                                                                                             |
| `ShiftLightness(double delta = 0.05)`                                       | `HexColor` | Adjusts brightness by shifting `V` up or down by `delta`, automatically lightening dark colors and darkening light colors, and returns the resulting color.                        |
| `ToAccent()`                                                                | `HexColor` | Returns an accent variant of this color by strongly adjusting lightness via `ShiftLightness(0.6)`.                                                                                 |
| `ToDisabled()`                                                              | `HexColor` | Returns a disabled-state variant by desaturating the color (via `Desaturate(0.6)`) and flattening contrast slightly.                                                               |
| `ToDragged()`                                                               | `HexColor` | Returns a dragged-state variant by adjusting lightness with `ShiftLightness(0.18)`.                                                                                                |
| `ToElevation1()`                                                            | `HexColor` | Returns an elevation level 1 variant by slightly shifting lightness with `ShiftLightness(0.02)`.                                                                                   |
| `ToElevation2()`                                                            | `HexColor` | Returns an elevation level 2 variant by shifting lightness with `ShiftLightness(0.04)`.                                                                                            |
| `ToElevation3()`                                                            | `HexColor` | Returns an elevation level 3 variant by shifting lightness with `ShiftLightness(0.06)`.                                                                                            |
| `ToElevation4()`                                                            | `HexColor` | Returns an elevation level 4 variant by shifting lightness with `ShiftLightness(0.08)`.                                                                                            |
| `ToElevation5()`                                                            | `HexColor` | Returns an elevation level 5 variant by shifting lightness with `ShiftLightness(0.1)`.                                                                                             |
| `ToFocused()`                                                               | `HexColor` | Returns a focused-state variant by brightening/darkening with `ShiftLightness(0.1)`, suitable for focus rings or outlines.                                                         |
| `ToForeground()`                                                            | `HexColor` | Returns a foreground variant by strongly adjusting lightness with `ShiftLightness(0.9)`, intended for text or icons on top of this color.                                          |
| `ToHovered()`                                                               | `HexColor` | Returns a hover-state variant by subtly adjusting lightness using `ShiftLightness(0.06)`.                                                                                          |
| `ToLerpLinear(HexColor end, double factor)`                                 | `HexColor` | Performs gamma-correct (linear-light) interpolation between this color and `end` for RGB channels and alpha, with `factor` clamped to `[0,1]`.                                     |
| `ToLerpLinearPreserveAlpha(HexColor end, double factor)`                    | `HexColor` | Performs gamma-correct interpolation between this color and `end` for RGB channels while preserving the current alpha channel.                                                     |
| `ToPressed()`                                                               | `HexColor` | Returns a pressed-state variant by adjusting lightness with `ShiftLightness(0.14)`.                                                                                                |
| `ToRelativeLuminance()`                                                     | `double`   | Computes WCAG relative luminance in `[0,1]` using linearized sRGB: `0.2126·R + 0.7152·G + 0.0722·B`.                                                                               |
| `ToString()`                                                                | `string`   | Returns the hexadecimal string `#RRGGBBAA` representing this color.                                                                                                                |
| `ToVisited()`                                                               | `HexColor` | Returns a visited-state variant by moderately reducing saturation via `Desaturate(0.3)`.                                                                                           |
| `TryParse(string? value, out HexColor result)`                              | `bool`     | Attempts to parse `value` into a `HexColor`, returning `true` and setting `result` on success; on failure, returns `false` and sets `result` to the default color.                 |

## Operators

| Name                                         | Type       | Description                                                                                               |
|----------------------------------------------|------------|-----------------------------------------------------------------------------------------------------------|
| `operator ==(HexColor left, HexColor right)` | `bool`     | Returns `true` if `left` and `right` have equal RGBA channels; otherwise `false`.                         |
| `operator >(HexColor left, HexColor right)`  | `bool`     | Returns `true` if `left` is greater than `right` according to `CompareTo`; otherwise `false`.             |
| `operator >=(HexColor left, HexColor right)` | `bool`     | Returns `true` if `left` is greater than or equal to `right` according to `CompareTo`; otherwise `false`. |
| `implicit operator HexColor(string hex)`     | `HexColor` | Implicitly converts a color string `hex` into a `HexColor` using the parsing constructor.                 |
| `implicit operator string(HexColor value)`   | `string`   | Implicitly converts a `HexColor` to its `#RRGGBBAA` string representation.                                |
| `operator !=(HexColor left, HexColor right)` | `bool`     | Returns `true` if `left` and `right` have different RGBA channels; otherwise `false`.                     |
| `operator <(HexColor left, HexColor right)`  | `bool`     | Returns `true` if `left` is less than `right` according to `CompareTo`; otherwise `false`.                |
| `operator <=(HexColor left, HexColor right)` | `bool`     | Returns `true` if `left` is less than or equal to `right` according to `CompareTo`; otherwise `false`.    |

## Events

*None*

## Exceptions

* `AryArgumentException`
  Thrown by `HexColor(string value)` and `Parse(string value)` when `value` is `null`, empty/whitespace, or cannot be
  parsed as any supported color format or known color name.

* `AryArgumentException`
  Thrown by parsing-related helpers when the input string contains invalid numeric data or out-of-range values, for
  example:

    * Invalid hex format or non-hex digits in hex strings (`HexColor(string)`, `Parse(string)`).
    * Invalid HSV(A) or RGB(A) functional strings (e.g., malformed `hsv(...)`, `rgba(...)`, percentage out of range).
    * Alpha or percentage values that are not finite or fall outside their documented ranges.

* `AryArgumentException`
  Thrown by `FromHsva(double hue, double saturation, double value, double alpha)` when `alpha` is not a finite number (
  e.g., `NaN`).

* `AryArgumentException`
  Thrown by `ContrastRatio(HexColor background)` when the computed contrast ratio is `NaN` or infinity, indicating
  invalid color input.

* `AryArgumentException`
  Thrown by `EnsureContrast(HexColor background, double minimumRatio)` when `minimumRatio` is less than `1.0` or greater
  than `21.0`.

## Example

```csharp
using Allyaria.Theming.Types;

public static class HexColorDemo
{
    public static void Run()
    {
        // Parse from a hex string
        HexColor primary = new("#6200EE");

        // Create a hover-state variant
        HexColor primaryHover = primary.ToHovered();

        // Ensure sufficient contrast for text on top of primary
        HexColor text = Colors.White;                // assumes a Colors registry with named colors
        HexColor accessibleText = text.EnsureContrast(primary, minimumRatio: 4.5);

        // Use implicit conversions to and from string
        HexColor accent = "#03DAC6";                 // string -> HexColor
        string accentHex = accent;                   // HexColor -> string "#RRGGBBAA"

        // Quick luminance checks
        bool isPrimaryDark = primary.IsDark();
        double contrast = accessibleText.ContrastRatio(primary);
    }
}
```

---

*Revision Date: 2025-11-15*
