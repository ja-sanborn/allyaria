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

    public BrandPalette Default { get; }

    public BrandPalette Disabled { get; }

    public BrandPalette Dragged { get; }

    public BrandPalette Focused { get; }

    public BrandPalette Hovered { get; }

    public BrandPalette Pressed { get; }

    public BrandPalette Visited { get; }
}
