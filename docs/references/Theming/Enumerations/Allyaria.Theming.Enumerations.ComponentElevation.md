# Allyaria.Theming.Enumerations.ComponentElevation

`ComponentElevation` is an enumeration of tonal elevation levels used by Allyaria components. It aligns with the
Material Design 3 elevation model, where elevation represents perceived depth via surface tone and shadow blending;
choose a value to convey relative prominence of a surface.

## Constructors

*None*

## Properties

| Name      | Type                 | Description                                                                 |
|-----------|----------------------|-----------------------------------------------------------------------------|
| `Mid`     | `ComponentElevation` | Represents a mid-level, default tonal palette (e.g. `SurfaceContainer`).    |
| `Lowest`  | `ComponentElevation` | Represents the lowest tonal palette (e.g. `SurfaceContainerLowest`).        |
| `Low`     | `ComponentElevation` | Represents a slightly lowered tonal palette (e.g. `SurfaceContainerLow`).   |
| `High`    | `ComponentElevation` | Represents a slightly elevated tonal palette (e.g. `SurfaceContainerHigh`). |
| `Highest` | `ComponentElevation` | Represents the highest tonal palette (e.g. `SurfaceContainerHighest`).      |

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
ComponentElevation elevation = ComponentElevation.Mid;
```

---

*Revision Date: 2025-10-17*
