# Allyaria.Theming.Styles.AryPaletteState

`AryPaletteState` is a readonly record struct that represents a complete set of computed `AryPalette` values mapped to
common UI interaction states such as Default, Disabled, Hovered, Focused, Pressed, and Dragged. Each state variant is
derived from a single baseline `AryPalette` using deterministic transformation methods to ensure consistent theming
behavior across components.

## Constructors

| Signature                             | Description                                                                                                                                                                                                                                         |
|---------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `AryPaletteState(AryPalette palette)` | Initializes a new instance of `AryPaletteState` from the provided baseline `AryPalette`. All derived state palettes (`Disabled`, `Hovered`, `Focused`, `Pressed`, `Dragged`) are computed from the baseline to ensure immutability and consistency. |

## Properties

| Name       | Type         | Description                                                                          |
|------------|--------------|--------------------------------------------------------------------------------------|
| `Default`  | `AryPalette` | The baseline (rest) palette for the component.                                       |
| `Disabled` | `AryPalette` | The palette for disabled state, typically rendered with reduced contrast or opacity. |
| `Dragged`  | `AryPalette` | The palette for dragged state, used during drag interactions.                        |
| `Focused`  | `AryPalette` | The palette for focused state, used when the component receives focus.               |
| `Hovered`  | `AryPalette` | The palette for hovered state, applied when a pointer is over the component.         |
| `Pressed`  | `AryPalette` | The palette for pressed or active state, used during clicks or taps.                 |

## Methods

| Name        | Signature                                    | Description                                                                                                                                       | Returns      |
|-------------|----------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------|--------------|
| `ToPalette` | `AryPalette ToPalette(ComponentState state)` | Returns the appropriate `AryPalette` corresponding to the given `ComponentState`. Defaults to the `Default` palette if the state is unrecognized. | `AryPalette` |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
// Create a base palette and derive its state variants
var basePalette = new AryPalette();
var paletteState = new AryPaletteState(basePalette);

// Retrieve a palette for a hovered state
AryPalette hoverPalette = paletteState.ToPalette(ComponentState.Hovered);
```

---

*Revision Date: 2025-10-17*
