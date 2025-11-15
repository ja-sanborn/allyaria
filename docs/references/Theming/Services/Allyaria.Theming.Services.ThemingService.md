# Allyaria.Theming.Services.ThemingService

`ThemingService` is the concrete implementation of `IThemingService`, responsible for holding the currently active
theme, the stored (preferred) theme, and for generating CSS for both documents and components. It encapsulates a `Theme`
instance and ensures reactive updates through the `ThemeChanged` event.

## Summary

`ThemingService` coordinates the theme lifecycle inside an Allyaria-themed application. It manages two theme types:

* **StoredType** — The user’s persisted preference (e.g., from settings or local storage).
* **EffectiveType** — The theme type currently being applied to rendered components, derived from `StoredType` unless
  using `System`.

The service delegates CSS generation to the internal `Theme` instance and automatically raises `ThemeChanged` whenever
the effective theme changes. It prevents illegal transitions (e.g., setting the effective theme to `System`) and ensures
the UI updates reactively.

## Constructors

`internal ThemingService(Theme theme, ThemeType themeType = ThemeType.System)` Creates a new theming service using the
provided `Theme` model. If `themeType` is `System`, the initial `EffectiveType` defaults to `Light`; otherwise, both
effective and stored types are set to the provided value.

## Properties

| Name            | Type        | Description                                        |
|-----------------|-------------|----------------------------------------------------|
| `EffectiveType` | `ThemeType` | The currently active theme type applied to the UI. |
| `StoredType`    | `ThemeType` | The user’s persisted theme preference.             |

## Methods

| Name                                                                                         | Returns  | Description                                                                                                              |
|----------------------------------------------------------------------------------------------|----------|--------------------------------------------------------------------------------------------------------------------------|
| `GetComponentCss(string prefix, ComponentType componentType, ComponentState componentState)` | `string` | Generates component-scoped CSS for the given prefix, component type, and state using the current `EffectiveType`.        |
| `GetDocumentCss()`                                                                           | `string` | Generates global document-level CSS for the current theme.                                                               |
| `SetEffectiveType(ThemeType themeType)`                                                      | `void`   | Sets a new active theme type unless it is `System` or already effective; raises `ThemeChanged` when the value changes.   |
| `SetStoredType(ThemeType themeType)`                                                         | `void`   | Updates the stored preference and adjusts the effective theme accordingly; raises `ThemeChanged` for non-system changes. |

## Operators

*None*

## Events

| Event          | Description                                                                                         |
|----------------|-----------------------------------------------------------------------------------------------------|
| `ThemeChanged` | Raised whenever the effective theme changes, allowing UI components to update their CSS reactively. |

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Services;
using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Components;

// Example composition root
public class AppTheming
{
    public void Setup(Theme theme)
    {
        var service = new ThemingService(theme);

        service.ThemeChanged += (_, _) =>
        {
            // Reapply global CSS when theme updates
            string css = service.GetDocumentCss();
        };

        // Change theme to Dark
        service.SetStoredType(ThemeType.Dark);

        // Generate scoped CSS for a hovered card
        string cardCss = service.GetComponentCss("card", ComponentType.Card, ComponentState.Hovered);
    }
}
```

---

*Revision Date: 2025-11-15*
