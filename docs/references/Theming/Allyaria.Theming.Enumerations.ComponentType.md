# Allyaria.Theming.Enumerations.ComponentType

`ComponentType` defines the Material Design 3–compatible component categories used in Allyaria for theming, layout, and
typography alignment.
Each type informs how a component’s tonal surface, typography, and elevation behave within the overall theme structure.

## Constructors

*Enum — no constructors.*

## Properties

*None*

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* `ComponentType` provides a semantic anchor for theming engines and typography mappers.
* The enumeration allows components to reference theme-specific palettes and typography sets consistently.
* Additional types (e.g., `Card`, `Dialog`, `Button`, `TextField`) may be added as Allyaria expands support for MD3
  components.

## Members

| Name      | Description                                                                                                                                                         |
|-----------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Surface` | Represents a Material Design **Surface** component, serving as the base layer for content. Defines elevation, background color, and tonal mapping within the theme. |

## Examples

### Minimal Example

```csharp
var type = ComponentType.Surface;
```

### Expanded Example

```csharp
public void ApplyTheme(ComponentType type)
{
    if (type == ComponentType.Surface)
    {
        Console.WriteLine("Applying surface background tone and elevation.");
    }
}
```

> *Rev Date: 2025-10-06*
