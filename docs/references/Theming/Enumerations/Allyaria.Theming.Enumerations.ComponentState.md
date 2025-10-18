# Allyaria.Theming.Enumerations.ComponentState

`ComponentState` is an enumeration that defines the interactive and visual states a UI component may occupy within the
Allyaria framework. It supports theming, accessibility, and interaction logic to ensure consistent feedback and user
experience across components.

## Constructors

*None*

## Properties

| Name       | Type             | Description                                                                                         |
|------------|------------------|-----------------------------------------------------------------------------------------------------|
| `Default`  | `ComponentState` | Represents the default, neutral state when the component is not focused, hovered, or pressed.       |
| `Dragged`  | `ComponentState` | Indicates the component is currently being dragged during a drag-and-drop or reordering action.     |
| `Focused`  | `ComponentState` | Indicates the component has keyboard or programmatic focus and should display a focus indicator.    |
| `Hovered`  | `ComponentState` | Indicates the pointer or equivalent input device is positioned over the component.                  |
| `Pressed`  | `ComponentState` | Indicates the component is in an active or pressed state, such as during a click or tap action.     |
| `ReadOnly` | `ComponentState` | Indicates the component is visible and interactive but does not allow modification of its content.  |
| `Disabled` | `ComponentState` | Indicates the component is non-interactive and cannot receive focus or input but remains visible.   |
| `Hidden`   | `ComponentState` | Indicates the component is intentionally hidden from both visual layout and assistive technologies. |

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
ComponentState state = ComponentState.Focused;
```

---

*Revision Date: 2025-10-17*
