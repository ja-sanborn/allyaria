# Allyaria.Theming.Styles.AryPaletteElevation

`AryPaletteElevation` is a readonly record struct representing a hierarchy of `AryPaletteState` values that correspond
to distinct elevation levels within the Allyaria theming system. Each elevation level (Lowest → Highest) encapsulates a
complete set of component states (Default, Hovered, Focused, etc.), derived from a base `AryPalette` using progressive
elevation transformations.

## Constructors

| Signature                                 | Description                                                                                                                                                                                                                                                                    |
|-------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `AryPaletteElevation(AryPalette palette)` | Initializes a new instance of `AryPaletteElevation` using the provided base palette. Each elevation layer (`Lowest`, `Low`, `Mid`, `High`, `Highest`) is constructed by applying successive elevation transformations (`ToElevation1()`–`ToElevation4()`) to the base palette. |

## Properties

| Name      | Type              | Description                                                                                              |
|-----------|-------------------|----------------------------------------------------------------------------------------------------------|
| `Lowest`  | `AryPaletteState` | Represents the lowest elevation layer, typically used for background or deeply recessed elements.        |
| `Low`     | `AryPaletteState` | Represents the low elevation layer, used for slightly raised or inset elements.                          |
| `Mid`     | `AryPaletteState` | Represents the mid-level elevation layer, commonly used for primary surfaces and content regions.        |
| `High`    | `AryPaletteState` | Represents the high elevation layer, typically used for elevated elements such as popovers or dropdowns. |
| `Highest` | `AryPaletteState` | Represents the highest elevation layer, used for topmost components such as dialogs or modals.           |

## Methods

| Name        | Signature                                                                  | Description                                                                                                                                                            | Returns      |
|-------------|----------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------|
| `ToPalette` | `AryPalette ToPalette(ComponentElevation elevation, ComponentState state)` | Returns the `AryPalette` corresponding to the specified `ComponentElevation` and `ComponentState`. Defaults to the `Mid` layer if the elevation value is unrecognized. | `AryPalette` |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
// Create a base palette
var basePalette = new AryPalette();

// Construct elevation layers
var elevation = new AryPaletteElevation(basePalette);

// Retrieve a palette for a hovered component at high elevation
AryPalette hoveredPalette = elevation.ToPalette(ComponentElevation.High, ComponentState.Hovered);
```

---

*Revision Date: 2025-10-17*
