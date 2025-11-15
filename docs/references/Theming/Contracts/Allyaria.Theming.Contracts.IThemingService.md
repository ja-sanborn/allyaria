# Allyaria.Theming.Contracts.IThemingService

W`IThemingService` is an interface defining the contract for managing theme selection, persistence, and CSS generation
within the Allyaria theming system. It coordinates the currently active theme, the stored (preferred) theme, and
provides CSS generation for both component-scoped and document-wide styling.

## Constructors

*None*

## Properties

| Name            | Type        | Description                                                          |
|-----------------|-------------|----------------------------------------------------------------------|
| `EffectiveType` | `ThemeType` | Gets the theme type currently active and applied to the UI.          |
| `StoredType`    | `ThemeType` | Gets the theme type that has been persisted (e.g., user preference). |

## Methods

| Name                                                                                         | Returns  | Description                                                                                       |
|----------------------------------------------------------------------------------------------|----------|---------------------------------------------------------------------------------------------------|
| `GetComponentCss(string prefix, ComponentType componentType, ComponentState componentState)` | `string` | Generates scoped CSS for a component based on its type, state, and assigned prefix.               |
| `GetDocumentCss()`                                                                           | `string` | Generates global CSS representing the active theme’s document-wide styling.                       |
| `SetEffectiveType(ThemeType themeType)`                                                      | `void`   | Sets and activates the current effective theme type. Implementations should raise `ThemeChanged`. |
| `SetStoredType(ThemeType themeType)`                                                         | `void`   | Sets the stored (persisted) theme type remembered across sessions.                                |

## Operators

*None*

## Events

| Event          | Description                                                                                               |
|----------------|-----------------------------------------------------------------------------------------------------------|
| `ThemeChanged` | Occurs whenever the effective theme changes, allowing components to reactively update their rendered CSS. |

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Contracts;
using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Components;

public class ThemingExample
{
    private readonly IThemingService theming;

    public ThemingExample(IThemingService service)
    {
        theming = service;

        // Watch for theme changes
        theming.ThemeChanged += (_, _) =>
        {
            string css = theming.GetDocumentCss();
            // Apply CSS to document scope...
        };
    }

    public void UpdateTheme()
    {
        // Switch to dark mode
        theming.SetEffectiveType(ThemeType.Dark);

        // Generate scoped CSS for a hovered button
        string scopedCss = theming.GetComponentCss("btn", ComponentType.Button, ComponentState.Hovered);
    }
}
```

---

*Revision Date: 2025-11-15*
