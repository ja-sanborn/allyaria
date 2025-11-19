# Allyaria.Theming.Helpers.ThemeNavigator

`ThemeNavigator` is a readonly record struct used to describe a navigational context for resolving theming relationships
across components, visual states, theme variants, and style types. It is foundational to the Allyaria theming engine and
is primarily consumed by `ThemeUpdater` and `ThemeConfigurator`.

## Summary

`ThemeNavigator` specifies *where* a theming update applies. It holds four lists—`ComponentTypes`, `ThemeTypes`,
`ComponentStates`, and `StyleTypes`—each indicating the targeted areas of the theme system. It provides fluent APIs (
e.g., `SetComponentStates`, `SetThemeType`, `SetAllComponentTypes`, etc.) to construct a fully specified context.

All modification methods return new navigator instances thanks to record struct immutability.

## Constructors

`ThemeNavigator(IReadOnlyList<ComponentType> ComponentTypes, IReadOnlyList<ThemeType> ThemeTypes, IReadOnlyList<ComponentState> ComponentStates, IReadOnlyList<StyleType> StyleTypes)`
Initializes a new navigational context with the provided lists. This constructor is primarily used internally; consumers
typically begin from `ThemeNavigator.Initialize`.

## Properties

| Name              | Type                            | Description                                                      |
|-------------------|---------------------------------|------------------------------------------------------------------|
| `ComponentTypes`  | `IReadOnlyList<ComponentType>`  | The set of targeted component types.                             |
| `ComponentStates` | `IReadOnlyList<ComponentState>` | The targeted component interaction states.                       |
| `StyleTypes`      | `IReadOnlyList<StyleType>`      | The targeted style types.                                        |
| `ThemeTypes`      | `IReadOnlyList<ThemeType>`      | The targeted theme variants.                                     |
| `Initialize`      | `ThemeNavigator` (static)       | A baseline empty navigator with all lists initialized and empty. |

## Methods

| Name                                                | Returns          | Description                                                                                                            |
|-----------------------------------------------------|------------------|------------------------------------------------------------------------------------------------------------------------|
| `SetAllComponentStates()`                           | `ThemeNavigator` | Selects all interactive component states except `Hidden` and `ReadOnly`.                                               |
| `SetAllComponentTypes()`                            | `ThemeNavigator` | Selects all possible component types.                                                                                  |
| `SetAllStyleTypes()`                                | `ThemeNavigator` | Selects all possible style types.                                                                                      |
| `SetComponentStates(params ComponentState[] items)` | `ThemeNavigator` | Sets the targeted component states to the provided list.                                                               |
| `SetComponentTypes(params ComponentType[] items)`   | `ThemeNavigator` | Sets the targeted component types to the provided list.                                                                |
| `SetStyleTypes(params StyleType[] items)`           | `ThemeNavigator` | Sets the targeted style types.                                                                                         |
| `SetThemeType(ThemeType themeType)`                 | `ThemeNavigator` | Sets a *single* theme type after validating that system and high-contrast theme types are not allowed in this context. |
| `SetThemeTypes(params ThemeType[] items)`           | `ThemeNavigator` | *(internal)* Sets the targeted theme types to the provided collection.                                                 |
| `SetContrastThemeTypes(bool isHighContrast)`        | `ThemeNavigator` | *(internal)* Chooses either the high-contrast or standard set of theme types.                                          |

## Operators

| Operator                                                 | Returns | Description                                                            |
|----------------------------------------------------------|---------|------------------------------------------------------------------------|
| `operator ==(ThemeNavigator left, ThemeNavigator right)` | `bool`  | Returns `true` if `left` and `right` are equal; otherwise `false`.     |
| `operator !=(ThemeNavigator left, ThemeNavigator right)` | `bool`  | Returns `true` if `left` and `right` are different; otherwise `false`. |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown when using `SetThemeType` with any of the *invalid* theme types:

    * `ThemeType.System`
    * `ThemeType.HighContrastLight`
    * `ThemeType.HighContrastDark`

## Example

```csharp
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Components;

public class NavigatorExample
{
    public ThemeNavigator Create()
    {
        // Begin from the empty baseline
        var nav = ThemeNavigator.Initialize;

        // Target primary component types and states
        nav = nav
            .SetComponentTypes(ComponentType.Button, ComponentType.Input)
            .SetComponentStates(ComponentState.Enabled, ComponentState.Hovered)
            .SetStyleTypes(StyleType.Color, StyleType.Typography)
            .SetThemeType(ThemeType.Light);

        return nav;
    }

    public ThemeNavigator SelectAll()
    {
        // Fully populate the navigator with all options
        return ThemeNavigator.Initialize
            .SetAllComponentTypes()
            .SetAllComponentStates()
            .SetAllStyleTypes();
    }
}
```

---

*Revision Date: 2025-11-15*
