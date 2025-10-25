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
        Disabled = disabledState ?? Default.SetPalette(defaultState.Palette.ToDisabled());
        Dragged = draggedState ?? Default.SetPalette(defaultState.Palette.ToDragged());
        Focused = focusedState ?? Default.SetPalette(defaultState.Palette.ToFocused());
        Hovered = hoveredState ?? Default.SetPalette(defaultState.Palette.ToHovered());
        Pressed = pressedState ?? Default.SetPalette(defaultState.Palette.ToPressed());
        Visited = visitedState ?? Default.SetPalette(defaultState.Palette.ToVisited());
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

        if (!string.IsNullOrWhiteSpace(prefix))
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

    public static ThemeStyle FromDefault(ThemeType themeType, PaletteType paletteType, FontType fontType)
        => new(ThemeState.FromDefault(themeType: themeType, paletteType: paletteType, fontType: fontType));

    public ThemeStyle Merge(ThemeStyle other)
        => SetDefault(Default.Merge(other.Default))
            .SetDisabled(Disabled.Merge(other.Disabled))
            .SetDragged(Dragged.Merge(other.Dragged))
            .SetFocused(Focused.Merge(other.Focused))
            .SetHovered(Hovered.Merge(other.Hovered))
            .SetPressed(Pressed.Merge(other.Pressed))
            .SetVisited(Visited.Merge(other.Visited));

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

    public ThemeStyle UpdateFontFamily(FontDefinition fontDefinition)
    {
        var themeType = Default.Typography.ThemeType;
        var fontType = Default.Typography.FontType;
        var fontFamily = fontDefinition.GetFontFamily(themeType: themeType, fontType: fontType);

        return SetDefault(Default.SetTypography(Default.Typography.SetFontFamily(fontFamily)))
            .SetDisabled(Disabled.SetTypography(Disabled.Typography.SetFontFamily(fontFamily)))
            .SetDragged(Dragged.SetTypography(Dragged.Typography.SetFontFamily(fontFamily)))
            .SetFocused(Focused.SetTypography(Focused.Typography.SetFontFamily(fontFamily)))
            .SetHovered(Hovered.SetTypography(Hovered.Typography.SetFontFamily(fontFamily)))
            .SetPressed(Pressed.SetTypography(Pressed.Typography.SetFontFamily(fontFamily)))
            .SetVisited(Visited.SetTypography(Visited.Typography.SetFontFamily(fontFamily)));
    }

    public ThemeStyle UpdatePalette(ColorPalette colorPalette)
    {
        var themeType = Default.Palette.ThemeType;
        var paletteType = Default.Palette.PaletteType;

        var defaultPalette = ThemeGroupPalette.FromColorPalette(
            colorPalette: colorPalette, themeType: themeType, paletteType: paletteType
        );

        return SetDefault(Default.SetPalette(defaultPalette))
            .SetDisabled(Disabled.SetPalette(defaultPalette.ToDisabled()))
            .SetDragged(Dragged.SetPalette(defaultPalette.ToDragged()))
            .SetFocused(Focused.SetPalette(defaultPalette.ToFocused()))
            .SetHovered(Hovered.SetPalette(defaultPalette.ToHovered()))
            .SetPressed(Pressed.SetPalette(defaultPalette.ToPressed()))
            .SetVisited(Visited.SetPalette(defaultPalette.ToVisited()));
    }
}
