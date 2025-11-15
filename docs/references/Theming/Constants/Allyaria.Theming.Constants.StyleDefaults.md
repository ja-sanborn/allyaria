# Allyaria.Theming.Constants.StyleDefaults

`StyleDefaults` is a static class defining the baseline color and font values used across all Allyaria themes. These
defaults support light, dark, and high-contrast modes and provide consistent, WCAG-aligned initialization for theming
systems when the user does not specify overrides.

## Summary

`StyleDefaults` is a static registry of predefined color and font constants used in theme initialization. It supplies
default semantic colors (primary, secondary, error, warning, etc.) across theme variants, as well as baseline font
stacks and a standard CSS variable prefix used by the theming engine.

## Constructors

*None*

## Properties

| Name                              | Type       | Description                                                          |
|-----------------------------------|------------|----------------------------------------------------------------------|
| `ErrorColorDark`                  | `HexColor` | Default error color for dark mode (`Colors.Red300`).                 |
| `ErrorColorLight`                 | `HexColor` | Default error color for light mode (`Colors.RedA700`).               |
| `HighContrastErrorColorDark`      | `HexColor` | High-contrast error color for dark mode (`Colors.RedA400`).          |
| `HighContrastErrorColorLight`     | `HexColor` | High-contrast error color for light mode (`Colors.RedA700`).         |
| `HighContrastInfoColorDark`       | `HexColor` | High-contrast informational color for dark mode (`Colors.Aqua`).     |
| `HighContrastInfoColorLight`      | `HexColor` | High-contrast informational color for light mode (`Colors.Blue700`). |
| `HighContrastPrimaryColorDark`    | `HexColor` | High-contrast primary color for dark mode (`Colors.Aqua`).           |
| `HighContrastPrimaryColorLight`   | `HexColor` | High-contrast primary color for light mode (`Colors.Black`).         |
| `HighContrastSecondaryColorDark`  | `HexColor` | High-contrast secondary color for dark mode (`Colors.YellowA400`).   |
| `HighContrastSecondaryColorLight` | `HexColor` | High-contrast secondary color for light mode (`Colors.Blue700`).     |
| `HighContrastSuccessColorDark`    | `HexColor` | High-contrast success color for dark mode (`Colors.LimeA200`).       |
| `HighContrastSuccessColorLight`   | `HexColor` | High-contrast success color for light mode (`Colors.Green800`).      |
| `HighContrastSurfaceColorDark`    | `HexColor` | High-contrast surface color for dark mode (`Colors.Black`).          |
| `HighContrastSurfaceColorLight`   | `HexColor` | High-contrast surface color for light mode (`Colors.White`).         |
| `HighContrastTertiaryColorDark`   | `HexColor` | High-contrast tertiary color for dark mode (`Colors.Fuchsia`).       |
| `HighContrastTertiaryColorLight`  | `HexColor` | High-contrast tertiary color for light mode (`Colors.Purple800`).    |
| `HighContrastWarningColorDark`    | `HexColor` | High-contrast warning color for dark mode (`Colors.YellowA400`).     |
| `HighContrastWarningColorLight`   | `HexColor` | High-contrast warning color for light mode (`Colors.Black`).         |
| `InfoColorDark`                   | `HexColor` | Default informational color for dark mode (`Colors.Lightblue300`).   |
| `InfoColorLight`                  | `HexColor` | Default informational color for light mode (`Colors.LightblueA700`). |
| `MonospaceFont`                   | `string`   | Default monospace font stack for code and fixed-width text.          |
| `PrimaryColorDark`                | `HexColor` | Default primary color for dark mode (`Colors.Blue300`).              |
| `PrimaryColorLight`               | `HexColor` | Default primary color for light mode (`Colors.Blue700`).             |
| `SansSerifFont`                   | `string`   | Default sans-serif font stack for UI text.                           |
| `SecondaryColorDark`              | `HexColor` | Default secondary color for dark mode (`Colors.Indigo300`).          |
| `SecondaryColorLight`             | `HexColor` | Default secondary color for light mode (`Colors.Indigo600`).         |
| `SerifFont`                       | `string`   | Default serif font stack for traditional text.                       |
| `SuccessColorDark`                | `HexColor` | Default success color for dark mode (`Colors.Green300`).             |
| `SuccessColorLight`               | `HexColor` | Default success color for light mode (`Colors.Green600`).            |
| `SurfaceColorDark`                | `HexColor` | Default surface background for dark mode (`Colors.Grey900`).         |
| `SurfaceColorLight`               | `HexColor` | Default surface background for light mode (`Colors.Grey50`).         |
| `TertiaryColorDark`               | `HexColor` | Default tertiary color for dark mode (`Colors.Teal300`).             |
| `TertiaryColorLight`              | `HexColor` | Default tertiary color for light mode (`Colors.Teal600`).            |
| `VarPrefix`                       | `string`   | Default CSS custom property prefix (`"ary"`).                        |
| `WarningColorDark`                | `HexColor` | Default warning color for dark mode (`Colors.Amber300`).             |
| `WarningColorLight`               | `HexColor` | Default warning color for light mode (`Colors.Amber700`).            |

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
using Allyaria.Theming.Constants;
using Allyaria.Theming.Types;

public class ThemeInitialization
{
    public HexColor Primary => StyleDefaults.PrimaryColorLight;
    public HexColor Surface => StyleDefaults.SurfaceColorLight;
    public string FontSans => StyleDefaults.SansSerifFont;

    public void ApplyHighContrastDark()
    {
        var warning = StyleDefaults.HighContrastWarningColorDark;
        var success = StyleDefaults.HighContrastSuccessColorDark;
    }
}
```

---

*Revision Date: 2025-11-15*
