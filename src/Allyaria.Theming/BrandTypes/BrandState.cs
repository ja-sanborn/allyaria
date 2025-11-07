namespace Allyaria.Theming.BrandTypes;

/// <summary>
/// Represents a collection of brand color palettes corresponding to different UI interaction states. Each state derives
/// its palette from a base color with adjusted tonal variations for accessibility and visual feedback.
/// </summary>
public readonly record struct BrandState
{
    /// <summary>Initializes a new instance of the <see cref="BrandState" /> struct using the specified base color.</summary>
    /// <param name="color">The base <see cref="HexColor" /> used to generate state-specific palettes.</param>
    public BrandState(HexColor color)
    {
        var backgroundColor = color.SetAlpha(alpha: 255);

        Default = new BrandPalette(backgroundColor: backgroundColor);
        Disabled = new BrandPalette(backgroundColor: backgroundColor.ToDisabled());
        Dragged = new BrandPalette(backgroundColor: backgroundColor.ToDragged());
        Focused = new BrandPalette(backgroundColor: backgroundColor.ToFocused());
        Hovered = new BrandPalette(backgroundColor: backgroundColor.ToHovered());
        Pressed = new BrandPalette(backgroundColor: backgroundColor.ToPressed());
        Visited = new BrandPalette(backgroundColor: backgroundColor.ToVisited());
    }

    /// <summary>Gets the palette for the default (idle) visual state.</summary>
    public BrandPalette Default { get; }

    /// <summary>
    /// Gets the palette for the disabled (inactive) state, ensuring reduced contrast and visual de-emphasis.
    /// </summary>
    public BrandPalette Disabled { get; }

    /// <summary>Gets the palette for the dragged state, used when an element is being moved or repositioned.</summary>
    public BrandPalette Dragged { get; }

    /// <summary>Gets the palette for the focused state, emphasizing keyboard or programmatic focus visibility.</summary>
    public BrandPalette Focused { get; }

    /// <summary>Gets the palette for the hovered state, providing feedback when the pointer is over an element.</summary>
    public BrandPalette Hovered { get; }

    /// <summary>Gets the palette for the pressed (active) state, applied during interaction activation.</summary>
    public BrandPalette Pressed { get; }

    /// <summary>
    /// Gets the palette for the visited state, commonly used to indicate previously interacted links or items.
    /// </summary>
    public BrandPalette Visited { get; }
}
