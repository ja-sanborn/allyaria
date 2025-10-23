namespace Allyaria.Theming.Themes;

public sealed record ThemeGroupPosition(
    StyleValueString? AlignContent = null,
    StyleValueString? AlignItems = null,
    StyleValueString? AlignSelf = null,
    StyleValueString? BoxSizing = null,
    StyleValueString? Display = null,
    StyleValueString? JustifyContent = null,
    StyleValueString? JustifyItems = null,
    StyleValueString? JustifySelf = null,
    StyleValueString? Position = null,
    StyleValueNumber? ZIndex = null
) : IThemeGroup
{
    public static readonly ThemeGroupPosition Empty = new();

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder
            .Add("align-content", AlignContent, varPrefix)
            .Add("align-items", AlignItems, varPrefix)
            .Add("align-self", AlignSelf, varPrefix)
            .Add("box-sizing", BoxSizing, varPrefix)
            .Add("display", Display, varPrefix)
            .Add("justify-content", JustifyContent, varPrefix)
            .Add("justify-items", JustifyItems, varPrefix)
            .Add("justify-self", JustifySelf, varPrefix)
            .Add("position", Position, varPrefix)
            .Add("z-index", ZIndex, varPrefix);

        return builder;
    }

    public static ThemeGroupPosition FromDefault() => new();

    public ThemeGroupPosition Merge(ThemeGroupPosition other)
        => SetAlignContent(other.AlignContent ?? AlignContent)
            .SetAlignItems(other.AlignItems ?? AlignItems)
            .SetAlignSelf(other.AlignSelf ?? AlignSelf)
            .SetBoxSizing(other.BoxSizing ?? BoxSizing)
            .SetDisplay(other.Display ?? Display)
            .SetJustifyContent(other.JustifyContent ?? JustifyContent)
            .SetJustifyItems(other.JustifyItems ?? JustifyItems)
            .SetJustifySelf(other.JustifySelf ?? JustifySelf)
            .SetPosition(other.Position ?? Position)
            .SetZIndex(other.ZIndex ?? ZIndex);

    public ThemeGroupPosition SetAlignContent(StyleValueString? value)
        => this with
        {
            AlignContent = value
        };

    public ThemeGroupPosition SetAlignItems(StyleValueString? value)
        => this with
        {
            AlignItems = value
        };

    public ThemeGroupPosition SetAlignSelf(StyleValueString? value)
        => this with
        {
            AlignSelf = value
        };

    public ThemeGroupPosition SetBoxSizing(StyleValueString? value)
        => this with
        {
            BoxSizing = value
        };

    public ThemeGroupPosition SetDisplay(StyleValueString? value)
        => this with
        {
            Display = value
        };

    public ThemeGroupPosition SetJustifyContent(StyleValueString? value)
        => this with
        {
            JustifyContent = value
        };

    public ThemeGroupPosition SetJustifyItems(StyleValueString? value)
        => this with
        {
            JustifyItems = value
        };

    public ThemeGroupPosition SetJustifySelf(StyleValueString? value)
        => this with
        {
            JustifySelf = value
        };

    public ThemeGroupPosition SetPosition(StyleValueString? value)
        => this with
        {
            Position = value
        };

    public ThemeGroupPosition SetZIndex(StyleValueNumber? value)
        => this with
        {
            ZIndex = value
        };
}
