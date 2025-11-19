# Allyaria.Theming.Contracts.IThemeConfigurator

`IThemeConfigurator` is an interface defining the contract for configuring and extending theme behavior within the
Allyaria theming system. It represents an ordered, immutable sequence of `ThemeUpdater` entries and enables fluent
composition of theme modifications before the theme is finalized.

## Summary

`IThemeConfigurator` is the primary abstraction for building, modifying, and layering theming behavior. Implementations
maintain a read-only list of `ThemeUpdater` instances—each representing a targeted, structured modification to the
theme—and expose a fluent `Override` method for progressively refining the theme. Because the interface inherits from
`IReadOnlyList<ThemeUpdater>`, it naturally supports indexing, enumeration, and count introspection.

## Constructors

*None*

## Properties

*None*

## Methods

| Name                             | Returns              | Description                                                                                                         |
|----------------------------------|----------------------|---------------------------------------------------------------------------------------------------------------------|
| `Override(ThemeUpdater updater)` | `IThemeConfigurator` | Adds a configuration update that refines the theme. Returns a new configurator instance to support fluent chaining. |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Contracts;
using Allyaria.Theming.Updates;

public class ThemeExample
{
    public IThemeConfigurator Configure(IThemeConfigurator configurator)
    {
        return configurator
            .Override(new ThemeUpdater(/* updater details */))
            .Override(new ThemeUpdater(/* more updates */));
    }

    public void Enumerate(IThemeConfigurator configurator)
    {
        foreach (var updater in configurator)
        {
            // Process each ThemeUpdater
        }
    }
}
```

---

*Revision Date: 2025-11-15*
