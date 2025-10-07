# Allyaria.Theming.Enumerations.ComponentState

`ComponentState` defines the various interactive and visual states a UI component may occupy within the Allyaria
framework.
These values are central to theming, accessibility, and interaction logic—ensuring consistent visual feedback and state
handling across components.

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

* States inform the Allyaria theming system which tonal, border, or typography variant to apply.
* Accessibility logic uses these states to ensure screen reader consistency (e.g., disabled vs. read-only).
* Multiple states may be transiently applied (e.g., `Hovered` + `Focused`) depending on user interaction.
* Visual effects such as elevation, tone shift, and outline intensity vary according to component state.

## Members

| Name       | Description                                                                  |
|------------|------------------------------------------------------------------------------|
| `Default`  | The neutral baseline state — neither hovered, focused, nor pressed.          |
| `Dragged`  | Indicates that the component is actively being moved or reordered.           |
| `Focused`  | The component currently holds keyboard or programmatic focus.                |
| `Hovered`  | The pointer or similar input device is positioned over the component.        |
| `Pressed`  | The component is being clicked, tapped, or otherwise activated.              |
| `ReadOnly` | The component is visible but not editable, used for locked or static fields. |
| `Disabled` | The component is visible but non-interactive and unfocusable.                |
| `Hidden`   | The component is hidden visually and from assistive technologies.            |

## Examples

### Minimal Example

```csharp
var state = ComponentState.Hovered;
```

### Expanded Example

```csharp
public void ApplyVisualStyle(ComponentState state)
{
    switch (state)
    {
        case ComponentState.Focused:
            Console.WriteLine("Apply focus ring and highlight color.");
            break;
        case ComponentState.Disabled:
            Console.WriteLine("Dim color and suppress hover interactions.");
            break;
        case ComponentState.Pressed:
            Console.WriteLine("Apply pressed tone and lowered elevation.");
            break;
        default:
            Console.WriteLine("Use default theme style.");
            break;
    }
}
```

> *Rev Date: 2025-10-06*
