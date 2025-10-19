# Allyaria.Theming.Services.ThemeProvider

`ThemeProvider` is a concrete implementation of `IThemeProvider` that manages runtime theme configuration including
color palettes, spacing, borders, and typography. It provides methods to retrieve styled CSS and `Style` representations
and supports immutable updates to theme data while notifying consumers via the `ThemeChanged` event. This class is the
core theming engine for Allyaria UI systems, allowing dynamic adjustments to theme settings while maintaining
consistency and immutability.

## Constructors

`ThemeProvider(Theme? theme = null, ThemeType themeType = ThemeType.System)` Initializes a new instance of

`ThemeProvider` with an optional base theme and theme type. Defaults to `StyleDefaults.Theme` and `ThemeType.System`.

## Properties

| Name        | Type        | Description                                                          |
|-------------|-------------|----------------------------------------------------------------------|
| `ThemeType` | `ThemeType` | Gets the currently active theme type (e.g., Light, Dark, or System). |

## Methods

| Name                                                                                                                                                                | Returns  | Description                                                                                                                                                                 |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `GetCss(ComponentType componentType, ComponentElevation elevation = ComponentElevation.Mid, ComponentState state = ComponentState.Default, string? varPrefix = "")` | `string` | Returns a CSS string representing the themed style for a given component, elevation, and state. Throws `ArgumentOutOfRangeException` if the component type is unrecognized. |
| `GetStyle(ComponentType componentType, ComponentElevation elevation = ComponentElevation.Mid, ComponentState state = ComponentState.Default)`                       | `Style`  | Retrieves a structured `Style` instance describing the component’s themed configuration. Throws `ArgumentOutOfRangeException` if the component type is invalid.             |
| `SetBorders(Borders? borders = null)`                                                                                                                               | `bool`   | Updates the border configuration for the current theme. Returns `true` if updated.                                                                                          |
| `SetDarkPalette(Palette? palette = null)`                                                                                                                           | `bool`   | Updates or replaces the dark palette. Returns `true` if updated.                                                                                                            |
| `SetHighContrastPalette(Palette? palette = null)`                                                                                                                   | `bool`   | Updates or replaces the high-contrast palette. Returns `true` if updated.                                                                                                   |
| `SetLightPalette(Palette? palette = null)`                                                                                                                          | `bool`   | Updates or replaces the light palette. Returns `true` if updated.                                                                                                           |
| `SetSpacing(Spacing? spacing = null)`                                                                                                                               | `bool`   | Updates the spacing configuration for the current theme. Returns `true` if updated.                                                                                         |
| `SetSurfaceTypography(Typography? typoSurface = null)`                                                                                                              | `bool`   | Updates the typography configuration for surface-level components. Returns `true` if updated.                                                                               |
| `SetTheme(Theme theme)`                                                                                                                                             | `bool`   | Applies a complete new `Theme` configuration. Returns `true` if the theme was changed. Throws `ArgumentNullException` if `theme` is `null`.                                 |
| `SetThemeType(ThemeType themeType)`                                                                                                                                 | `bool`   | Changes the current theme type (e.g., Light, Dark, System) and raises `ThemeChanged` if different.                                                                          |

## Operators

*None*

## Events

| Event          | Description                                                       |
|----------------|-------------------------------------------------------------------|
| `ThemeChanged` | Raised when the active `ThemeType` or any theme property changes. |

## Exceptions

*None*

## Example

```csharp
public class ThemeUsageExample
{
    public void ConfigureTheme()
    {
        var provider = new ThemeProvider();

        // Subscribe to theme change events
        provider.ThemeChanged += (_, _) => Console.WriteLine("Theme updated!");

        // Change to dark mode
        provider.SetThemeType(ThemeType.Dark);

        // Get CSS for a Button component
        string css = provider.GetCss(ComponentType.Button);

        // Retrieve structured style for a Card component
        Style cardStyle = provider.GetStyle(ComponentType.Card);

        // Apply a new theme
        var customTheme = new Theme(); // Assume defaults or custom configuration
        provider.SetTheme(customTheme);
    }
}
```

---

*Revision Date: 2025-10-18*
