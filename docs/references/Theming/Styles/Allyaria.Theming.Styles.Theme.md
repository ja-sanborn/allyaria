# Allyaria.Theming.Styles.Theme

`Theme` is the root Allyaria theme struct that composes borders, spacing, palette variants (light/dark/high-contrast),
and component typography. It serves as the primary entry point for generating per-component, per-state styles in a
consistent, immutable, and strongly typed way.

## Constructors

`Theme()` Initializes a new theme instance with default borders, spacing, palettes, and typography.

`Theme(Borders? borders = null, Spacing? spacing = null, Palette? paletteLight = null, Palette? paletteDark = null, Palette? paletteHighContrast = null, Typography? typoSurface = null)`
Initializes a new instance of `Theme` using optional subcomponents. Any parameter left `null` falls back to its default
configuration.

## Properties

*None*

## Methods

| Name      | Signature                                                                                                                                                                                         | Description                                                                                                                                                 | Returns  |
|-----------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| `Cascade` | `Theme Cascade(Borders? borders = null, Spacing? spacing = null, Palette? paletteLight = null, Palette? paletteDark = null, Palette? paletteHighContrast = null, Typography? typoSurface = null)` | Returns a new theme with specified component overrides. Unspecified values are preserved from the existing instance.                                        | `Theme`  |
| `ToCss`   | `string ToCss(ThemeType themeType, ComponentType componentType, ComponentElevation elevation = ComponentElevation.Mid, ComponentState state = ComponentState.Default, string? varPrefix = "")`    | Produces a concatenated CSS string for the resolved style. When the state is `Focused`, the border uses a thicker dashed focus variant.                     | `string` |
| `ToStyle` | `Style ToStyle(ThemeType themeType, ComponentType componentType, ComponentElevation elevation = ComponentElevation.Mid, ComponentState state = ComponentState.Default)`                           | Resolves a concrete `Style` combining the theme’s palette, typography, spacing, and borders for the given theme type, component type, elevation, and state. | `Style`  |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
// Create a theme with defaults
var theme = new Theme();

// Generate CSS for a hovered surface component in light mode
string css = theme.ToCss(
    ThemeType.Light,
    ComponentType.Surface,
    ComponentElevation.Mid,
    ComponentState.Hovered,
    "surface"
);

// Example output:
// --surface-background-color: #ffffff;
// --surface-color: #000000;
// --surface-border-color: #dddddd;
```

---

*Revision Date: 2025-10-17*
