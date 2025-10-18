# Allyaria.Theming.Constants.StyleDefaults

`StyleDefaults` is a static class providing strongly typed default style values for Allyaria theming. It defines
reusable baseline constants for typography, layout, and color—serving as the foundation for consistent theming across
components. This includes defaults for border styles, spacing, alignment, color palettes, and the global default theme
instance.

## Constructors

*None*

## Properties

| Name                          | Type             | Description                                                                  |
|-------------------------------|------------------|------------------------------------------------------------------------------|
| `BackgroundColorDark`         | `AryColorValue`  | Default background color for dark theme.                                     |
| `BackgroundColorHighContrast` | `AryColorValue`  | Default background color for high-contrast theme.                            |
| `BackgroundColorLight`        | `AryColorValue`  | Default background color for light theme.                                    |
| `BorderRadius`                | `AryNumberValue` | Default border radius.                                                       |
| `BorderStyle`                 | `AryStringValue` | Default border style.                                                        |
| `BorderWidth`                 | `AryNumberValue` | Default border width.                                                        |
| `FontFamily`                  | `AryStringValue` | Default font family.                                                         |
| `FontSize`                    | `AryNumberValue` | Default font size.                                                           |
| `FontStyle`                   | `AryStringValue` | Default font style.                                                          |
| `FontWeight`                  | `AryStringValue` | Default font weight.                                                         |
| `ForegroundColorDark`         | `AryColorValue`  | Default foreground color for dark theme.                                     |
| `ForegroundColorHighContrast` | `AryColorValue`  | Default foreground color for high-contrast theme.                            |
| `ForegroundColorLight`        | `AryColorValue`  | Default foreground color for light theme.                                    |
| `LetterSpacing`               | `AryNumberValue` | Default letter spacing.                                                      |
| `LineHeight`                  | `AryNumberValue` | Default line height.                                                         |
| `Margin`                      | `AryNumberValue` | Default margin.                                                              |
| `Padding`                     | `AryNumberValue` | Default padding.                                                             |
| `PaletteDark`                 | `AryPalette`     | Default dark palette.                                                        |
| `PaletteHighContrast`         | `AryPalette`     | Default high-contrast palette.                                               |
| `PaletteLight`                | `AryPalette`     | Default light palette.                                                       |
| `TextAlign`                   | `AryStringValue` | Default text alignment.                                                      |
| `TextDecorationLine`          | `AryStringValue` | Default text decoration line.                                                |
| `TextDecorationStyle`         | `AryStringValue` | Default text decoration style.                                               |
| `TextTransform`               | `AryStringValue` | Default text transform.                                                      |
| `Theme`                       | `AryTheme`       | Default theme instance combining borders, spacing, palettes, and typography. |
| `Transparent`                 | `AryColorValue`  | Default transparent color.                                                   |
| `VerticalAlign`               | `AryStringValue` | Default vertical alignment.                                                  |

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
AryTheme defaultTheme = StyleDefaults.Theme; // get the global default Allyaria theme
```

---

*Revision Date: 2025-10-17*
