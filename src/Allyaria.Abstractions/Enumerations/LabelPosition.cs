namespace Allyaria.Abstractions.Enumerations;

/// <summary>
/// Specifies the position or visibility behavior of a label relative to its associated input or control.
/// </summary>
/// <remarks>
/// This enumeration defines how labels are displayed or hidden within Allyaria UI components, particularly for input
/// controls with bordered outlines. Each option determines whether a label appears externally (above/below) or is embedded
/// as a placeholder within the input control. The positioning respects layout direction (LTR/RTL) and accessibility
/// requirements.
/// </remarks>
public enum LabelPosition
{
    /// <summary>
    /// The label is completely hidden from visual layout but remains accessible to assistive technologies such as screen
    /// readers.
    /// </summary>
    Hidden,

    /// <summary>
    /// The label appears at the top-left or top-right corner (depending on text direction) and visually intersects the outline
    /// border of the input control.
    /// </summary>
    Above,

    /// <summary>
    /// The label appears below the outline border of the input control, aligned to the bottom-left or bottom-right corner
    /// depending on text direction.
    /// </summary>
    Below,

    /// <summary>
    /// The label is hidden from view but, if the associated input control is empty, its text appears as a placeholder inside
    /// the input area. When the input has text or focus, the label remains hidden.
    /// </summary>
    PlaceholderHidden,

    /// <summary>
    /// The label text appears as a placeholder inside the input control when it is empty. When the input has text or focus,
    /// the label transitions to the top-left or top-right position (depending on text direction), intersecting the outline
    /// border of the input control.
    /// </summary>
    PlaceholderAbove,

    /// <summary>
    /// The label text appears as a placeholder inside the input control when it is empty. When the input has text or focus,
    /// the label transitions to the bottom-left or bottom-right position (depending on text direction), positioned below the
    /// outline border of the input control.
    /// </summary>
    PlaceholderBelow
}
