# Allyaria.Theming.Contracts.IThemeProvider

`IThemeProvider` is an interface that defines a contract for managing and applying theming data—palettes, typography,
spacing, and borders—across components and for producing CSS or structured `Style` representations under the active
theme.

## Constructors

*None*

## Properties

| Name        | Type        | Description                            |
|-------------|-------------|----------------------------------------|
| `ThemeType` | `ThemeType` | Gets the currently active `ThemeType`. |

## Methods

| Name                                                                                                                                                                | Returns  | Description                                                                                                              |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------|--------------------------------------------------------------------------------------------------------------------------|
| `GetCss(ComponentType componentType, ComponentElevation elevation = ComponentElevation.Mid, ComponentState state = ComponentState.Default, string? varPrefix = "")` | `string` | Retrieves the resolved CSS string for a component given its type, elevation, state, and an optional CSS variable prefix. |
| `GetStyle(ComponentType componentType, ComponentElevation elevation = ComponentElevation.Mid, ComponentState state = ComponentState.Default)`                       | `Style`  | Retrieves the structured `Style` for a component given its type, elevation, and state.                                   |
| `SetBorders(Borders? borders = null)`                                                                                                                               | `bool`   | Updates the current theme’s border configuration; `null` restores default borders.                                       |
| `SetDarkPalette(Palette? palette = null)`                                                                                                                           | `bool`   | Updates or replaces the dark theme palette; `null` applies defaults.                                                     |
| `SetHighContrastPalette(Palette? palette = null)`                                                                                                                   | `bool`   | Updates or replaces the high-contrast palette; `null` applies defaults.                                                  |
| `SetLightPalette(Palette? palette = null)`                                                                                                                          | `bool`   | Updates or replaces the light theme palette; `null` applies defaults.                                                    |
| `SetSpacing(Spacing? spacing = null)`                                                                                                                               | `bool`   | Updates the current theme’s spacing configuration; `null` restores default spacing.                                      |
| `SetSurfaceTypography(Typography? typoSurface = null)`                                                                                                              | `bool`   | Updates typography applied to surface-level components; `null` applies defaults.                                         |
| `SetTheme(Theme theme)`                                                                                                                                             | `bool`   | Replaces the entire active `Theme` configuration.                                                                        |
| `SetThemeType(ThemeType themeType)`                                                                                                                                 | `bool`   | Changes the current `ThemeType` and raises `ThemeChanged` if the type differs.                                           |

## Operators

*None*

## Events

| Event          | Description                                 |
|----------------|---------------------------------------------|
| `ThemeChanged` | Occurs when the active `ThemeType` changes. |

## Exceptions

*None*

## Example

```csharp
public class Usage
{
    private readonly IThemeProvider provider;

    public Usage(IThemeProvider provider)
    {
        this.provider = provider;

        // Switch to Dark theme
        provider.SetThemeType(ThemeType.Dark);

        // Get CSS for a Button at default elevation/state
        string buttonCss = provider.GetCss(ComponentType.Button);

        // Update spacing (or pass null to restore defaults)
        provider.SetSpacing(new Spacing());

        // Retrieve a structured style for a Card
        Style cardStyle = provider.GetStyle(ComponentType.Card, ComponentElevation.Mid, ComponentState.Default);
    }
}
```

---

*Revision Date: 2025-10-18*
