namespace Allyaria.Theming.Branding;

public readonly record struct BrandStyle
{
    public BrandStyle(HexColor color)
    {
        var backgroundColor = new StyleValueColor(color: color.SetAlpha(alpha: 255));

        Default = new BrandColor(backgroundColor: backgroundColor);
        Disabled = new BrandColor(backgroundColor: backgroundColor.ToDisabled());
        Dragged = new BrandColor(backgroundColor: backgroundColor.ToDragged());
        Focused = new BrandColor(backgroundColor: backgroundColor.ToFocused());
        Hovered = new BrandColor(backgroundColor: backgroundColor.ToHovered());
        Pressed = new BrandColor(backgroundColor: backgroundColor.ToPressed());
        Visited = new BrandColor(backgroundColor: backgroundColor.ToVisited());
    }

    public BrandStyle(bool isHighContrastDark)
    {
        Default = new BrandColor(isHighContrastDark: isHighContrastDark);
        Disabled = new BrandColor(isHighContrastDark: isHighContrastDark);
        Dragged = new BrandColor(isHighContrastDark: isHighContrastDark);
        Focused = new BrandColor(isHighContrastDark: isHighContrastDark);
        Hovered = new BrandColor(isHighContrastDark: isHighContrastDark);
        Pressed = new BrandColor(isHighContrastDark: isHighContrastDark);
        Visited = new BrandColor(isHighContrastDark: isHighContrastDark);
    }

    public BrandColor Default { get; init; }

    public BrandColor Disabled { get; init; }

    public BrandColor Dragged { get; init; }

    public BrandColor Focused { get; init; }

    public BrandColor Hovered { get; init; }

    public BrandColor Pressed { get; init; }

    public BrandColor Visited { get; init; }

    public ThemeGroupPalette GetPalette(ThemeType themeType, PaletteType paletteType, ComponentState state)
        => state switch
        {
            ComponentState.Disabled => Disabled.GetPalette(themeType: themeType, paletteType: paletteType),
            ComponentState.Dragged => Dragged.GetPalette(themeType: themeType, paletteType: paletteType),
            ComponentState.Focused => Focused.GetPalette(themeType: themeType, paletteType: paletteType),
            ComponentState.Hovered => Hovered.GetPalette(themeType: themeType, paletteType: paletteType),
            ComponentState.Pressed => Pressed.GetPalette(themeType: themeType, paletteType: paletteType),
            ComponentState.Visited => Visited.GetPalette(themeType: themeType, paletteType: paletteType),
            _ => Default.GetPalette(themeType: themeType, paletteType: paletteType)
        };
}
