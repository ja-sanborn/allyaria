# Allyaria.Theming.Enumerations.ComponentState

`ComponentState` is an enumeration defining the interactive and visual states a UI component can occupy. It is used
throughout theming, accessibility, and interaction systems to drive consistent styling, feedback, and behavioral
responses.

## Summary

`ComponentState` is an enum representing component interaction states. UI frameworks use these values to determine
state-specific visuals (e.g., hover, pressed, disabled) and behavioral rules. This allows centralized styling logic and
predictable cross-component behavior.

## Constructors

*None*

## Properties

*None*

## Methods

*None*

## Fields

| Name       | Type             | Description                                                                 |
|------------|------------------|-----------------------------------------------------------------------------|
| `Default`  | `ComponentState` | The neutral state when no interaction is occurring.                         |
| `Dragged`  | `ComponentState` | The component is being dragged, such as during drag-and-drop or reordering. |
| `Focused`  | `ComponentState` | The component currently holds keyboard or programmatic focus.               |
| `Hovered`  | `ComponentState` | The pointer is positioned over the component.                               |
| `Pressed`  | `ComponentState` | The component is actively engaged (e.g., click, tap, key activation).       |
| `Visited`  | `ComponentState` | Indicates a visited element, such as a visited link.                        |
| `ReadOnly` | `ComponentState` | The component is visible and interactive but cannot be modified.            |
| `Disabled` | `ComponentState` | The component cannot receive focus or input but remains visible.            |
| `Hidden`   | `ComponentState` | The component is hidden from layout and assistive technologies.             |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Enumerations;

public class ComponentExample
{
    public ComponentState State { get; private set; } = ComponentState.Default;

    public void OnHover()
    {
        State = ComponentState.Hovered;
    }

    public void OnPress()
    {
        State = ComponentState.Pressed;
    }

    public void OnDisable()
    {
        State = ComponentState.Disabled;
    }
}
```

---

*Revision Date: 2025-11-15*
