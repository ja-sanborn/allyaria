namespace Allyaria.Theming.Themes;

public sealed record ThemeStyle
{
    public static readonly ThemeStyle Empty = new(
        defaultState: ThemeState.Empty,
        disabledState: ThemeState.Empty,
        draggedState: ThemeState.Empty,
        focusedState: ThemeState.Empty,
        hoveredState: ThemeState.Empty,
        pressedState: ThemeState.Empty,
        visitedState: ThemeState.Empty
    );

    public ThemeStyle(ThemeState defaultState,
        ThemeState? disabledState = null,
        ThemeState? draggedState = null,
        ThemeState? focusedState = null,
        ThemeState? hoveredState = null,
        ThemeState? pressedState = null,
        ThemeState? visitedState = null)
    {
        Default = defaultState;
        Disabled = disabledState ?? Default.SetPalette(value: defaultState.Palette.ToDisabled());
        Dragged = draggedState ?? Default.SetPalette(value: defaultState.Palette.ToDragged());
        Focused = focusedState ?? Default.SetPalette(value: defaultState.Palette.ToFocused());
        Hovered = hoveredState ?? Default.SetPalette(value: defaultState.Palette.ToHovered());
        Pressed = pressedState ?? Default.SetPalette(value: defaultState.Palette.ToPressed());
        Visited = visitedState ?? Default.SetPalette(value: defaultState.Palette.ToVisited());
    }

    public ThemeState Default { get; init; }

    public ThemeState Disabled { get; init; }

    public ThemeState Dragged { get; init; }

    public ThemeState Focused { get; init; }

    public ThemeState Hovered { get; init; }

    public ThemeState Pressed { get; init; }

    public ThemeState Visited { get; init; }

    public CssBuilder BuildCss(CssBuilder builder, ComponentState state, string? varPrefix = null)
    {
        var prefix = varPrefix.ToCssName();

        if (!string.IsNullOrWhiteSpace(value: prefix))
        {
            prefix = $"{prefix}-{state}";
        }

        switch (state)
        {
            case ComponentState.Default:
                builder = Default.BuildCss(builder: builder, varPrefix: prefix);

                break;

            case ComponentState.Disabled:
                builder = Disabled.BuildCss(builder: builder, varPrefix: prefix);

                break;

            case ComponentState.Dragged:
                builder = Dragged.BuildCss(builder: builder, varPrefix: prefix);

                break;

            case ComponentState.Focused:
                builder = Focused.BuildCss(builder: builder, varPrefix: prefix);

                break;

            case ComponentState.Hovered:
                builder = Hovered.BuildCss(builder: builder, varPrefix: prefix);

                break;

            case ComponentState.Pressed:
                builder = Pressed.BuildCss(builder: builder, varPrefix: prefix);

                break;

            case ComponentState.Visited:
                builder = Visited.BuildCss(builder: builder, varPrefix: prefix);

                break;
        }

        return builder;
    }

    public static ThemeStyle FromBrand(BrandTheme brand,
        ThemeType themeType,
        FontType fontType,
        PaletteType paletteType)
    {
        var defaultState = ThemeState.FromBrand(
            brand: brand, themeType: themeType, fontType: fontType, paletteType: paletteType,
            state: ComponentState.Default
        );

        var disabledState = ThemeState.FromBrand(
            brand: brand, themeType: themeType, fontType: fontType, paletteType: paletteType,
            state: ComponentState.Disabled
        );

        var draggedState = ThemeState.FromBrand(
            brand: brand, themeType: themeType, fontType: fontType, paletteType: paletteType,
            state: ComponentState.Dragged
        );

        var focusedState = ThemeState.FromBrand(
            brand: brand, themeType: themeType, fontType: fontType, paletteType: paletteType,
            state: ComponentState.Focused
        );

        var hoveredState = ThemeState.FromBrand(
            brand: brand, themeType: themeType, fontType: fontType, paletteType: paletteType,
            state: ComponentState.Hovered
        );

        var pressedState = ThemeState.FromBrand(
            brand: brand, themeType: themeType, fontType: fontType, paletteType: paletteType,
            state: ComponentState.Pressed
        );

        var visitedState = ThemeState.FromBrand(
            brand: brand, themeType: themeType, fontType: fontType, paletteType: paletteType,
            state: ComponentState.Visited
        );

        return new ThemeStyle(
            defaultState: defaultState,
            disabledState: disabledState,
            draggedState: draggedState,
            focusedState: focusedState,
            hoveredState: hoveredState,
            pressedState: pressedState,
            visitedState: visitedState
        );
    }

    public ThemeStyle Merge(ThemeStyle other)
        => SetDefault(value: Default.Merge(other: other.Default))
            .SetDisabled(value: Disabled.Merge(other: other.Disabled))
            .SetDragged(value: Dragged.Merge(other: other.Dragged))
            .SetFocused(value: Focused.Merge(other: other.Focused))
            .SetHovered(value: Hovered.Merge(other: other.Hovered))
            .SetPressed(value: Pressed.Merge(other: other.Pressed))
            .SetVisited(value: Visited.Merge(other: other.Visited));

    public ThemeStyle SetDefault(ThemeState value)
        => this with
        {
            Default = value
        };

    public ThemeStyle SetDisabled(ThemeState value)
        => this with
        {
            Disabled = value
        };

    public ThemeStyle SetDragged(ThemeState value)
        => this with
        {
            Dragged = value
        };

    public ThemeStyle SetFocused(ThemeState value)
        => this with
        {
            Focused = value
        };

    public ThemeStyle SetHovered(ThemeState value)
        => this with
        {
            Hovered = value
        };

    public ThemeStyle SetPressed(ThemeState value)
        => this with
        {
            Pressed = value
        };

    public ThemeStyle SetVisited(ThemeState value)
        => this with
        {
            Visited = value
        };

    public string ToCss(ComponentState state, string? varPrefix = "")
        => BuildCss(builder: new CssBuilder(), state: state, varPrefix: varPrefix).ToString();
}
