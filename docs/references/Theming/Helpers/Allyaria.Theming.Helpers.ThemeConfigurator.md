# Allyaria.Theming.Helpers.ThemeConfigurator

`ThemeConfigurator` is a sealed class that provides a configurable implementation of `IThemeConfigurator`. It allows
consumers to register, customize, and override theming values by supplying a sequence of `ThemeUpdater` instances—while
enforcing rules that preserve immutability, system integrity, and accessibility.

## Summary

`ThemeConfigurator` is used during application startup to configure Allyaria themes. It exposes a fluent API via
`Override(ThemeUpdater)` for applying theme updates. The configurator internally stores each update in order and
prevents illegal modifications such as altering system themes, high-contrast themes, or restricted component states (
e.g., focused outline properties). It is designed to be safe, composable, and enumerable, functioning as a collection of
ordered theme updates.

## Constructors

*None*

## Properties

| Name              | Type           | Description                                                 |
|-------------------|----------------|-------------------------------------------------------------|
| `Count`           | `int`          | Gets the total number of configured `ThemeUpdater` entries. |
| `this[int index]` | `ThemeUpdater` | Gets the `ThemeUpdater` at the specified zero-based index.  |

## Methods

| Name                                      | Returns                     | Description                                                                                                                       |
|-------------------------------------------|-----------------------------|-----------------------------------------------------------------------------------------------------------------------------------|
| `GetEnumerator()`                         | `IEnumerator<ThemeUpdater>` | Returns a generic enumerator for iterating through configured theme updaters.                                                     |
| `Override(ThemeUpdater updater)`          | `IThemeConfigurator`        | Adds or replaces a theme updater in the configuration sequence, enforcing immutability rules for system and high-contrast themes. |
| `IEnumerator IEnumerable.GetEnumerator()` | `IEnumerator`               | Returns a non-generic enumerator for iterating through configured theme updaters.                                                 |

## Operators

*None*

## Events

*None*

## Exceptions

* `AryArgumentException`  
  Thrown when attempting an illegal theme modification, including: modifying the system theme, altering high-contrast
  themes, modifying hidden or read-only component states, or altering restricted focused-outline properties.

## Example

```csharp
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Navigation;
using Allyaria.Theming.Components;
using Allyaria.Theming.Updates;

public class Startup
{
    public IThemeConfigurator ConfigureTheme()
    {
        var configurator = new ThemeConfigurator();

        // Example: Apply a safe theme override
        configurator.Override(
            new ThemeUpdater(
                navigator: new ThemeNavigator(
                    themeTypes: ThemeType.Light,
                    componentStates: ComponentState.Enabled
                ),
                value: new ThemeValue("#6200EE")
            )
        );

        // Enumerator support
        foreach (var update in configurator)
        {
            // process theme updates
        }

        return configurator;
    }
}
```

---

*Revision Date: 2025-11-15*
