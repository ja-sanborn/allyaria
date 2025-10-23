namespace Allyaria.Theming.Themes;

public sealed record ThemeComponent
{
    public static readonly ThemeComponent Empty = new(
        ThemeStyle.Empty, ThemeStyle.Empty, ThemeStyle.Empty, ThemeStyle.Empty, ThemeStyle.Empty, ThemeStyle.Empty,
        ThemeStyle.Empty
    );

    public ThemeComponent(ThemeStyle defaultStyle,
        ThemeStyle? disabledStyle = null,
        ThemeStyle? draggedStyle = null,
        ThemeStyle? focusedStyle = null,
        ThemeStyle? hoveredStyle = null,
        ThemeStyle? pressedStyle = null,
        ThemeStyle? visitedStyle = null)
    {
        Default = defaultStyle;
        Disabled = disabledStyle ?? Default.SetPalette(defaultStyle.Palette.ToDisabled());
        Dragged = draggedStyle ?? Default.SetPalette(defaultStyle.Palette.ToDragged());
        Focused = focusedStyle ?? Default.SetPalette(defaultStyle.Palette.ToFocused());
        Hovered = hoveredStyle ?? Default.SetPalette(defaultStyle.Palette.ToHovered());
        Pressed = pressedStyle ?? Default.SetPalette(defaultStyle.Palette.ToPressed());
        Visited = visitedStyle ?? Default.SetPalette(defaultStyle.Palette.ToVisited());
    }

    public ThemeStyle Default { get; init; }

    public ThemeStyle Disabled { get; init; }

    public ThemeStyle Dragged { get; init; }

    public ThemeStyle Focused { get; init; }

    public ThemeStyle Hovered { get; init; }

    public ThemeStyle Pressed { get; init; }

    public ThemeStyle Visited { get; init; }

    public CssBuilder BuildCss(CssBuilder builder, ComponentState state, string? varPrefix = null)
    {
        var prefix = varPrefix.ToCssName();

        if (!string.IsNullOrWhiteSpace(prefix))
        {
            prefix = $"{prefix}-{state}";
        }

        switch (state)
        {
            case ComponentState.Default:
                builder = Default.BuildCss(builder, prefix);

                break;

            case ComponentState.Disabled:
                builder = Disabled.BuildCss(builder, prefix);

                break;

            case ComponentState.Dragged:
                builder = Dragged.BuildCss(builder, prefix);

                break;

            case ComponentState.Focused:
                builder = Focused.BuildCss(builder, prefix);

                break;

            case ComponentState.Hovered:
                builder = Hovered.BuildCss(builder, prefix);

                break;

            case ComponentState.Pressed:
                builder = Pressed.BuildCss(builder, prefix);

                break;

            case ComponentState.Visited:
                builder = Visited.BuildCss(builder, prefix);

                break;
        }

        return builder;
    }

    public static ThemeComponent FromDefault(PaletteColor paletteColor,
        ThemeType themeType,
        PaletteType paletteType,
        string? fontFamily = null)
        => new(ThemeStyle.FromDefault(paletteColor, themeType, paletteType, fontFamily));

    public ThemeComponent Merge(ThemeComponent other)
        => SetDefault(Default.Merge(other.Default))
            .SetDisabled(Disabled.Merge(other.Disabled))
            .SetDragged(Dragged.Merge(other.Dragged))
            .SetFocused(Focused.Merge(other.Focused))
            .SetHovered(Hovered.Merge(other.Hovered))
            .SetPressed(Pressed.Merge(other.Pressed))
            .SetVisited(Visited.Merge(other.Visited));

    public ThemeComponent SetDefault(ThemeStyle value)
        => this with
        {
            Default = value
        };

    public ThemeComponent SetDisabled(ThemeStyle value)
        => this with
        {
            Disabled = value
        };

    public ThemeComponent SetDragged(ThemeStyle value)
        => this with
        {
            Dragged = value
        };

    public ThemeComponent SetFocused(ThemeStyle value)
        => this with
        {
            Focused = value
        };

    public ThemeComponent SetHovered(ThemeStyle value)
        => this with
        {
            Hovered = value
        };

    public ThemeComponent SetPressed(ThemeStyle value)
        => this with
        {
            Pressed = value
        };

    public ThemeComponent SetVisited(ThemeStyle value)
        => this with
        {
            Visited = value
        };

    public string ToCss(ComponentState state, string? varPrefix = "")
        => BuildCss(new CssBuilder(), state, varPrefix).ToString();
}
