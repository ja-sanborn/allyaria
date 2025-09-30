namespace Allyaria.Abstractions.UnitTests.Enumerations;

/// <summary>
/// Specifies the possible positions and behaviors of a label in relation to its associated input or content.
/// </summary>
public enum LabelPosition
{
    /// <summary>
    /// The label is not displayed.
    /// </summary>
    Hidden,

    /// <summary>
    /// The label is displayed to the left of the content.
    /// </summary>
    Left,

    /// <summary>
    /// The label is displayed to the right of the content.
    /// </summary>
    Right,

    /// <summary>
    /// The label is displayed above the content.
    /// </summary>
    Top,

    /// <summary>
    /// The label is displayed below the content.
    /// </summary>
    Bottom,

    /// <summary>
    /// The label floats within the input border at the top (left or right aligned depending on localization).
    /// </summary>
    Float,

    /// <summary>
    /// The label appears as a placeholder initially, then transitions to a floating position when the user enters text.
    /// </summary>
    PlaceholderFloat,

    /// <summary>
    /// The label appears as a placeholder initially, then hides when the user enters text.
    /// </summary>
    PlaceholderHide
}
