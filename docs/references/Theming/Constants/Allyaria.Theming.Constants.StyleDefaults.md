# Allyaria.Theming.Constants.StyleDefaults

`StyleDefaults` is a static class providing strongly typed default style values for Allyaria theming. It defines
reusable baseline constants for typography, layout, and color—serving as the foundation for consistent theming across
components. This includes defaults for border styles, spacing, alignment, color palettes, and the global default theme
instance.

## Constructors

*None*

## Properties

| Name                          | Type          | Description                                                                  |
|-------------------------------|---------------|------------------------------------------------------------------------------|
| `BackgroundColorDark`         | `ThemeColor`  | Default background color for dark theme.                                     |
| `BackgroundColorHighContrast` | `ThemeColor`  | Default background color for high-contrast theme.                            |
| `BackgroundColorLight`        | `ThemeColor`  | Default background color for light theme.                                    |
| `BorderRadius`                | `ThemeNumber` | Default border radius.                                                       |
| `BorderStyle`                 | `ThemeString` | Default border style.                                                        |
| `BorderWidth`                 | `ThemeNumber` | Default border width.                                                        |
| `FontFamily`                  | `ThemeString` | Default font family.                                                         |
| `FontSize`                    | `ThemeNumber` | Default font size.                                                           |
| `FontStyle`                   | `ThemeString` | Default font style.                                                          |
| `FontWeight`                  | `ThemeString` | Default font weight.                                                         |
| `ForegroundColorDark`         | `ThemeColor`  | Default foreground color for dark theme.                                     |
| `ForegroundColorHighContrast` | `ThemeColor`  | Default foreground color for high-contrast theme.                            |
| `ForegroundColorLight`        | `ThemeColor`  | Default foreground color for light theme.                                    |
| `LetterSpacing`               | `ThemeNumber` | Default letter spacing.                                                      |
| `LineHeight`                  | `ThemeNumber` | Default line height.                                                         |
| `Margin`                      | `ThemeNumber` | Default margin.                                                              |
| `Padding`                     | `ThemeNumber` | Default padding.                                                             |
| `PaletteDark`                 | `Palette`     | Default dark palette.                                                        |
| `PaletteHighContrast`         | `Palette`     | Default high-contrast palette.                                               |
| `PaletteLight`                | `Palette`     | Default light palette.                                                       |
| `TextAlign`                   | `ThemeString` | Default text alignment.                                                      |
| `TextDecorationLine`          | `ThemeString` | Default text decoration line.                                                |
| `TextDecorationStyle`         | `ThemeString` | Default text decoration style.                                               |
| `TextTransform`               | `ThemeString` | Default text transform.                                                      |
| `Theme`                       | `Theme`       | Default theme instance combining borders, spacing, palettes, and typography. |
| `Transparent`                 | `ThemeColor`  | Default transparent color.                                                   |
| `VerticalAlign`               | `ThemeString` | Default vertical alignment.                                                  |

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
Theme defaultTheme = StyleDefaults.Theme; // get the global default Allyaria theme
```

---

*Revision Date: 2025-10-17*
