# Allyaria.Theming.Types.HexColor

`HexColor` is an immutable color value type that represents an RGBA color with full parsing, conversion, interpolation,
and contrast-analysis capabilities. It supports multiple color formats including hexadecimal, RGB(A), HSV(A), and named
colors from the Allyaria color registry. The struct is immutable and thread-safe, with derived HSV components computed
automatically for each instance.

## Constructors

`HexColor()`
Initializes a new instance of `HexColor` with all color channels set to `0` (transparent black).

`HexColor(HexByte red, HexByte green, HexByte blue, HexByte? alpha = null)`
Initializes a new instance of `HexColor` with explicit RGBA channel values. If `alpha` is `null`, it defaults to
opaque (255).

`HexColor(string value)`
Initializes a new instance of `HexColor` by parsing a color string in any supported format (`#RRGGBB`, `rgb()`, `hsv()`,
or named color).

## Properties

| Name | Type      | Description                                                            |
|------|-----------|------------------------------------------------------------------------|
| `R`  | `HexByte` | Gets the red channel component.                                        |
| `G`  | `HexByte` | Gets the green channel component.                                      |
| `B`  | `HexByte` | Gets the blue channel component.                                       |
| `A`  | `HexByte` | Gets the alpha (opacity) channel.                                      |
| `H`  | `double`  | Gets the hue component in degrees `[0–360)`.                           |
| `S`  | `double`  | Gets the saturation component in the normalized range `[0–1]`.         |
| `V`  | `double`  | Gets the brightness (value) component in the normalized range `[0–1]`. |

## Methods

| Name                                                                                                                                    | Returns    | Description                                                                               |
|-----------------------------------------------------------------------------------------------------------------------------------------|------------|-------------------------------------------------------------------------------------------|
| `CompareTo(HexColor other)`                                                                                                             | `int`      | Compares this color with another for relative ordering.                                   |
| `ContrastRatio(HexColor background)`                                                                                                    | `double`   | Calculates the WCAG contrast ratio between this color and a background.                   |
| `Desaturate(double desaturateBy = 0.5, double valueBlendTowardMid = 0.15)`                                                              | `HexColor` | Reduces saturation by the given fraction and optionally blends brightness toward midtone. |
| `EnsureMinimumContrast(HexColor background, double minimumRatio = 3.0)`                                                                 | `HexColor` | Adjusts the brightness to meet or best-approximate a required WCAG contrast ratio.        |
| `Equals(HexColor other)`                                                                                                                | `bool`     | Determines whether this color equals another `HexColor`.                                  |
| `FromHsva(double hue, double saturation, double value, double alpha = 1.0)`                                                             | `HexColor` | Creates a color from HSVA component values.                                               |
| `Invert()`                                                                                                                              | `HexColor` | Returns the photographic negative of the color (inverts each RGB channel).                |
| `IsDark()`                                                                                                                              | `bool`     | Returns `true` if the color’s relative luminance is below 0.5.                            |
| `IsLight()`                                                                                                                             | `bool`     | Returns `true` if the color’s relative luminance is 0.5 or greater.                       |
| `IsOpaque()`                                                                                                                            | `bool`     | Returns `true` if the alpha channel equals 255.                                           |
| `IsTransparent()`                                                                                                                       | `bool`     | Returns `true` if the alpha channel equals 0.                                             |
| `Parse(string value)`                                                                                                                   | `HexColor` | Parses a color string into a `HexColor`.                                                  |
| `SetAlpha(byte alpha)`                                                                                                                  | `HexColor` | Returns a new color with the same RGB values but a modified alpha.                        |
| `ShiftLightness(double delta = 0.05)`                                                                                                   | `HexColor` | Adjusts brightness automatically (lightens dark colors, darkens light ones).              |
| `ToComponentBorderColor(HexColor outerBackground, HexColor? componentFill = null, double minContrast = 3.0, bool highContrast = false)` | `HexColor` | Derives a suitable border color that maintains contrast hierarchy within UI components.   |
| `ToDividerBorderColor(HexColor surface, double minContrast = 3.0, bool highContrast = false)`                                           | `HexColor` | Generates a subtle divider color that meets the given contrast ratio.                     |
| `ToLerpLinear(HexColor end, double factor)`                                                                                             | `HexColor` | Performs gamma-correct (linear-light) interpolation between two colors, including alpha.  |
| `ToLerpLinearPreserveAlpha(HexColor end, double factor)`                                                                                | `HexColor` | Performs gamma-correct interpolation while preserving alpha.                              |
| `ToRelativeLuminance()`                                                                                                                 | `double`   | Computes WCAG relative luminance using the sRGB formula.                                  |
| `ToString()`                                                                                                                            | `string`   | Returns the color in `#RRGGBBAA` format.                                                  |
| `TryParse(string? value, out HexColor result)`                                                                                          | `bool`     | Attempts to parse a color string, returning `true` if successful.                         |

## Operators

| Operator                                   | Returns    | Description                                                  |
|--------------------------------------------|------------|--------------------------------------------------------------|
| `==`                                       | `bool`     | Determines equality between two colors.                      |
| `!=`                                       | `bool`     | Determines inequality between two colors.                    |
| `>`                                        | `bool`     | Determines if one color is greater than another.             |
| `<`                                        | `bool`     | Determines if one color is less than another.                |
| `>=`                                       | `bool`     | Determines if one color is greater than or equal to another. |
| `<=`                                       | `bool`     | Determines if one color is less than or equal to another.    |
| `implicit operator HexColor(string value)` | `HexColor` | Converts a color string to a `HexColor`.                     |
| `implicit operator string(HexColor value)` | `string`   | Converts a `HexColor` to its `#RRGGBBAA` string form.        |

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Abstractions.Types;

// Create colors from strings and bytes
var red = new HexColor("#FF0000");
var blue = new HexColor("rgb(0, 0, 255)");
var semiTransparent = new HexColor(new HexByte(0), new HexByte(128), new HexByte(255), new HexByte(128));

// Compute contrast
double ratio = red.ContrastRatio(blue);

// Ensure accessible contrast
var accessibleRed = red.EnsureMinimumContrast(blue, 4.5);

// Interpolation
var gradientMid = red.ToLerpLinear(blue, 0.5);

// Adjust brightness
var lighter = red.ShiftLightness(0.1);

// Parse named colors
var sky = new HexColor("Sky500");

Console.WriteLine(sky); // e.g., "#87CEEBFF"
```

---

*Revision Date: 2025-10-17*
