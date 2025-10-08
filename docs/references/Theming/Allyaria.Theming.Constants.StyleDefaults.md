# Allyaria.Theming.Constants.StyleDefaults

`StyleDefaults` defines the standard baseline styling values used throughout the Allyaria theming system.
It serves as a centralized source for consistent visual defaults such as spacing, typography, borders, and colors across
all components and themes.

## Constructors

*Static class — no constructors.*

## Properties

*None*

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* Provides **WCAG-compliant** color defaults for light, dark, and high-contrast themes.
* Defines baseline typographic settings including font family, size, weight, line height, and letter spacing.
* Establishes consistent spacing, border, and padding values used by Allyaria surfaces and text components.
* All values are expressed through strongly typed wrappers (`AryColorValue`, `AryNumberValue`, `AryStringValue`) for
  type safety and serialization consistency.
* These constants act as fallbacks for theming systems when no explicit style overrides are applied.

## Members

| Name                          | Type             | Description                                                                                     |
|-------------------------------|------------------|-------------------------------------------------------------------------------------------------|
| `BackgroundColorDark`         | `AryColorValue`  | Default dark theme background color (`Colors.Grey900`).                                         |
| `BackgroundColorHighContrast` | `AryColorValue`  | Default high-contrast theme background color (`Colors.White`).                                  |
| `BackgroundColorLight`        | `AryColorValue`  | Default light theme background color (`Colors.Grey50`).                                         |
| `BorderRadius`                | `AryNumberValue` | Default border radius (`Sizing.Size2`).                                                         |
| `BorderStyle`                 | `AryStringValue` | Default border style (`BorderStyle.Solid`).                                                     |
| `BorderWidth`                 | `AryNumberValue` | Default border width (`Sizing.Size0`).                                                          |
| `FontFamily`                  | `AryStringValue` | Default font stack (`system-ui, Segoe UI, Roboto, Helvetica, Arial, sans-serif`).               |
| `FontSize`                    | `AryNumberValue` | Default font size (`Sizing.Size3`).                                                             |
| `FontStyle`                   | `AryStringValue` | Default font style (`FontStyle.Normal`).                                                        |
| `FontWeight`                  | `AryStringValue` | Default font weight (`FontWeight.Normal`).                                                      |
| `ForegroundColorDark`         | `AryColorValue`  | Default dark theme foreground color (`Colors.Grey50`).                                          |
| `ForegroundColorHighContrast` | `AryColorValue`  | Default high-contrast foreground color (`Colors.Black`).                                        |
| `ForegroundColorLight`        | `AryColorValue`  | Default light theme foreground color (`Colors.Grey900`).                                        |
| `LetterSpacing`               | `AryNumberValue` | Default letter spacing (`0.5px`).                                                               |
| `LineHeight`                  | `AryNumberValue` | Default line height (`1.5`).                                                                    |
| `Margin`                      | `AryNumberValue` | Default margin (`Sizing.Size2`).                                                                |
| `Padding`                     | `AryNumberValue` | Default padding (`Sizing.Size3`).                                                               |
| `TextAlign`                   | `AryStringValue` | Default text alignment (`TextAlign.Left`).                                                      |
| `TextDecorationLine`          | `AryStringValue` | Default text decoration line (`TextDecorationLine.None`).                                       |
| `TextDecorationStyle`         | `AryStringValue` | Default text decoration style (`TextDecorationStyle.Solid`).                                    |
| `TextTransform`               | `AryStringValue` | Default text transform (`TextTransform.None`).                                                  |
| `Theme`                       | `AryTheme`       | Default theme instance. Used as the baseline theme for components unless explicitly overridden. |
| `VerticalAlign`               | `AryStringValue` | Default vertical alignment (`VerticalAlign.Baseline`).                                          |

## Examples

### Minimal Example

```csharp
var font = StyleDefaults.FontFamily;
```

### Expanded Example

```csharp
public void ApplyDefaultTextStyle()
{
    Console.WriteLine($"Font: {StyleDefaults.FontFamily}");
    Console.WriteLine($"Size: {StyleDefaults.FontSize}");
    Console.WriteLine($"Weight: {StyleDefaults.FontWeight}");
    Console.WriteLine($"Color (Light): {StyleDefaults.ForegroundColorLight}");
}
```

> *Rev Date: 2025-10-07*
