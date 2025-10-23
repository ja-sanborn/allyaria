namespace Allyaria.Theming.Themes;

public sealed record ThemeGroupOverflow(
    StyleValueString? OverflowWrap = null,
    StyleValueString? OverflowX = null,
    StyleValueString? OverflowY = null,
    StyleValueString? OverscrollBehaviorX = null,
    StyleValueString? OverscrollBehaviorY = null
) : IThemeGroup
{
    public static readonly ThemeGroupOverflow Empty = new();

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder
            .Add("overflow-wrap", OverflowWrap, varPrefix)
            .Add("overflow-x", OverflowX, varPrefix)
            .Add("overflow-y", OverflowY, varPrefix)
            .Add("overscroll-behavior-x", OverscrollBehaviorX, varPrefix)
            .Add("overscroll-behavior-y", OverscrollBehaviorY, varPrefix);

        return builder;
    }

    public static ThemeGroupOverflow FromDefault() => new();

    public ThemeGroupOverflow Merge(ThemeGroupOverflow other)
        => SetOverflowWrap(other.OverflowWrap ?? OverflowWrap)
            .SetOverflowX(other.OverflowX ?? OverflowX)
            .SetOverflowY(other.OverflowY ?? OverflowY)
            .SetOverscrollBehaviorX(other.OverscrollBehaviorX ?? OverscrollBehaviorX)
            .SetOverscrollBehaviorY(other.OverscrollBehaviorY ?? OverscrollBehaviorY);

    public ThemeGroupOverflow SetOverflowWrap(StyleValueString? value)
        => this with
        {
            OverflowWrap = value
        };

    public ThemeGroupOverflow SetOverflowX(StyleValueString? value)
        => this with
        {
            OverflowX = value
        };

    public ThemeGroupOverflow SetOverflowY(StyleValueString? value)
        => this with
        {
            OverflowY = value
        };

    public ThemeGroupOverflow SetOverscrollBehaviorX(StyleValueString? value)
        => this with
        {
            OverscrollBehaviorX = value
        };

    public ThemeGroupOverflow SetOverscrollBehaviorY(StyleValueString? value)
        => this with
        {
            OverscrollBehaviorY = value
        };
}
