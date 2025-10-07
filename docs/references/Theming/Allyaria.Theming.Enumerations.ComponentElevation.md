# Allyaria.Theming.Enumerations.ComponentElevation

`ComponentElevation` defines the tonal elevation levels used throughout Allyaria components, aligning with the *
*Material Design 3** elevation model.
Elevation represents perceived depth through surface tone and shadow blending, helping differentiate UI surfaces.

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

* Elevation levels affect tonal blending and shadow intensity rather than physical z-index values.
* The `Mid` level serves as the default for most surfaces (e.g., `SurfaceContainer`).
* Components dynamically adjust their tone when changing states (hovered, pressed, disabled) based on their elevation.
* These values are thematically mapped to color tokens within the `AllyariaTheme` API.

## Members

| Name      | Description                                                                  |
|-----------|------------------------------------------------------------------------------|
| `Mid`     | Represents a mid-level, default tonal palette (e.g., `SurfaceContainer`).    |
| `Lowest`  | Represents the lowest tonal palette (e.g., `SurfaceContainerLowest`).        |
| `Low`     | Represents a slightly lowered tonal palette (e.g., `SurfaceContainerLow`).   |
| `High`    | Represents a slightly elevated tonal palette (e.g., `SurfaceContainerHigh`). |
| `Highest` | Represents the highest tonal palette (e.g., `SurfaceContainerHighest`).      |

## Examples

### Minimal Example

```csharp
var elevation = ComponentElevation.High;
```

### Expanded Example

```csharp
public void ApplySurfaceStyle(ComponentElevation elevation)
{
    switch (elevation)
    {
        case ComponentElevation.Lowest:
            Console.WriteLine("Applying base background tone for deep surfaces.");
            break;
        case ComponentElevation.Highest:
            Console.WriteLine("Applying bright elevated tone for floating elements.");
            break;
        default:
            Console.WriteLine("Using default mid-level tone.");
            break;
    }
}
```

> *Rev Date: 2025-10-06*
