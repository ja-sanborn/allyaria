namespace Allyaria.Theming.Themes;

public sealed record ThemeGroupOverflow(
    StyleValueString? OverflowWrap = null,
    StyleValueString? OverflowX = null,
    StyleValueString? OverflowY = null,
    StyleValueString? OverscrollBehaviorX = null,
    StyleValueString? OverscrollBehaviorY = null
)
{
    public static readonly ThemeGroupOverflow Empty = new();

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder
            .Add(propertyName: "overflow-wrap", value: OverflowWrap, varPrefix: varPrefix)
            .Add(propertyName: "overflow-x", value: OverflowX, varPrefix: varPrefix)
            .Add(propertyName: "overflow-y", value: OverflowY, varPrefix: varPrefix)
            .Add(propertyName: "overscroll-behavior-x", value: OverscrollBehaviorX, varPrefix: varPrefix)
            .Add(propertyName: "overscroll-behavior-y", value: OverscrollBehaviorY, varPrefix: varPrefix);

        return builder;
    }

    public ThemeGroupOverflow Merge(ThemeGroupOverflow other)
        => SetOverflowWrap(value: other.OverflowWrap ?? OverflowWrap)
            .SetOverflowX(value: other.OverflowX ?? OverflowX)
            .SetOverflowY(value: other.OverflowY ?? OverflowY)
            .SetOverscrollBehaviorX(value: other.OverscrollBehaviorX ?? OverscrollBehaviorX)
            .SetOverscrollBehaviorY(value: other.OverscrollBehaviorY ?? OverscrollBehaviorY);

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

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
