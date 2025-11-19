# Allyaria.Theming.Helpers.ThemeUpdater

`ThemeUpdater` is a readonly record struct that represents a single theming update instruction within the Allyaria
theming system. It pairs a navigational context—`ThemeNavigator`—with a concrete style value (`IStyleValue`) to define
*what* style should be applied *where*.

## Summary

`ThemeUpdater` is a lightweight, immutable mapping object used throughout the theme-building pipeline. It connects a
*targeting scope* (components, component states, theme variants, and style types) with an optional `IStyleValue`
representing the new style value to apply. These objects are created by theme appliers, stored by `ThemeConfigurator`,
and consumed by theme builders during final theme generation.

## Constructors

`ThemeUpdater(ThemeNavigator Navigator, IStyleValue? Value)` Creates a new updater with:

* `Navigator` — the selection of components, states, styles, and theme types to target
* `Value` — the style value to apply, or `null` when used as a placeholder/mapping node

## Properties

| Name        | Type             | Description                                                                                             |
|-------------|------------------|---------------------------------------------------------------------------------------------------------|
| `Navigator` | `ThemeNavigator` | The targeting context defining where the update applies.                                                |
| `Value`     | `IStyleValue?`   | The style value to apply. May be `null` when used as a placeholder for hierarchical mapping operations. |

## Methods

*None*

---

## Operators

| Operator                                             | Returns | Description                                                            |
|------------------------------------------------------|---------|------------------------------------------------------------------------|
| `operator ==(ThemeUpdater left, ThemeUpdater right)` | `bool`  | Returns `true` if `left` and `right` are equal; otherwise `false`.     |
| `operator !=(ThemeUpdater left, ThemeUpdater right)` | `bool`  | Returns `true` if `left` and `right` are different; otherwise `false`. |

## Events

*None*

---

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Helpers;
using Allyaria.Theming.StyleTypes;

public class ThemeUpdaterDemo
{
    public ThemeUpdater CreateUpdate()
    {
        var navigator = ThemeNavigator.Initialize
            .SetComponentTypes(ComponentType.Button)
            .SetComponentStates(ComponentState.Hovered)
            .SetStyleTypes(StyleType.Color)
            .SetThemeType(ThemeType.Light);

        var value = new StyleColor("#6200EE");

        return new ThemeUpdater(navigator, value);
    }
}
```

---

*Revision Date: 2025-11-15*
