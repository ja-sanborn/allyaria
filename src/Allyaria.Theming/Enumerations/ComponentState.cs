namespace Allyaria.Theming.Enumerations;

/// <summary>
/// Specifies the various interactive and visual states a UI component may occupy within the Allyaria framework. These
/// values are used by theming, accessibility, and interaction logic to ensure consistent behavior and visual feedback.
/// </summary>
public enum ComponentState
{
    /// <summary>
    /// Represents the default, neutral state when the component is neither focused, hovered, pressed, nor otherwise modified.
    /// </summary>
    Default,

    /// <summary>
    /// Indicates that the component is currently being dragged, typically as part of a drag-and-drop operation or interactive
    /// reordering gesture.
    /// </summary>
    Dragged,

    /// <summary>
    /// Indicates that the component currently holds keyboard or programmatic focus, and should display an appropriate focus
    /// indicator.
    /// </summary>
    Focused,

    /// <summary>
    /// Indicates that the user's pointer or equivalent input device is currently positioned over the component.
    /// </summary>
    Hovered,

    /// <summary>
    /// Indicates that the component is in an active, pressed, or engaged state— for example, during a click, tap, or key
    /// activation.
    /// </summary>
    Pressed,

    /// <summary>
    /// Indicates that the component is visible and interactive but does not allow modification of its theme or contents.
    /// Typically used for static displays or locked fields.
    /// </summary>
    ReadOnly,

    /// <summary>
    /// Indicates that the component is not interactive and cannot receive focus or user input, but remains visible for
    /// contextual purposes.
    /// </summary>
    Disabled,

    /// <summary>
    /// Indicates that the component is intentionally hidden from both visual layout and assistive technologies such as screen
    /// readers.
    /// </summary>
    Hidden
}
