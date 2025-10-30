namespace Allyaria.Theming.Types;

public readonly record struct BrandState
{
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

    public BrandState(bool isHighContrastDark)
    {
        Default = new BrandPalette(isHighContrastDark: isHighContrastDark);
        Disabled = new BrandPalette(isHighContrastDark: isHighContrastDark);
        Dragged = new BrandPalette(isHighContrastDark: isHighContrastDark);
        Focused = new BrandPalette(isHighContrastDark: isHighContrastDark);
        Hovered = new BrandPalette(isHighContrastDark: isHighContrastDark);
        Pressed = new BrandPalette(isHighContrastDark: isHighContrastDark);
        Visited = new BrandPalette(isHighContrastDark: isHighContrastDark);
    }

    public BrandPalette Default { get; init; }

    public BrandPalette Disabled { get; init; }

    public BrandPalette Dragged { get; init; }

    public BrandPalette Focused { get; init; }

    public BrandPalette Hovered { get; init; }

    public BrandPalette Pressed { get; init; }

    public BrandPalette Visited { get; init; }

    public BrandPalette GetPalette(ComponentState state)
        => state switch
        {
            ComponentState.Disabled => Disabled,
            ComponentState.Dragged => Dragged,
            ComponentState.Focused => Focused,
            ComponentState.Hovered => Hovered,
            ComponentState.Pressed => Pressed,
            ComponentState.Visited => Visited,
            _ => Default
        };
}
