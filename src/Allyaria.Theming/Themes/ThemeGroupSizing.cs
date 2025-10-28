namespace Allyaria.Theming.Themes;

public sealed record ThemeGroupSizing(
    StyleValueNumber? Height = null,
    StyleGroupMargin? Margin = null,
    StyleValueNumber? MaxHeight = null,
    StyleValueNumber? MaxWidth = null,
    StyleGroupPadding? Padding = null,
    StyleValueNumber? Width = null
)
{
    public static readonly ThemeGroupSizing Empty = new();

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder
            .Add(propertyName: "height", value: Height, varPrefix: varPrefix)
            .Add(propertyName: "max-height", value: MaxHeight, varPrefix: varPrefix)
            .Add(propertyName: "max-width", value: MaxWidth, varPrefix: varPrefix)
            .Add(propertyName: "width", value: Width, varPrefix: varPrefix);

        builder = Margin?.BuildCss(builder: builder, varPrefix: varPrefix) ?? builder;
        builder = Padding?.BuildCss(builder: builder, varPrefix: varPrefix) ?? builder;

        return builder;
    }

    public ThemeGroupSizing Merge(ThemeGroupSizing other)
        => SetHeight(value: other.Height ?? Height)
            .SetMargin(value: other.Margin ?? Margin)
            .SetMaxHeight(value: other.MaxHeight ?? MaxHeight)
            .SetMaxWidth(value: other.MaxWidth ?? MaxWidth)
            .SetPadding(value: other.Padding ?? Padding)
            .SetWidth(value: other.Width ?? Width);

    public ThemeGroupSizing SetHeight(StyleValueNumber? value)
        => this with
        {
            Height = value
        };

    public ThemeGroupSizing SetMargin(StyleGroupMargin? value)
        => this with
        {
            Margin = value
        };

    public ThemeGroupSizing SetMaxHeight(StyleValueNumber? value)
        => this with
        {
            MaxHeight = value
        };

    public ThemeGroupSizing SetMaxWidth(StyleValueNumber? value)
        => this with
        {
            MaxWidth = value
        };

    public ThemeGroupSizing SetPadding(StyleGroupPadding? value)
        => this with
        {
            Padding = value
        };

    public ThemeGroupSizing SetWidth(StyleValueNumber? value)
        => this with
        {
            Width = value
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
