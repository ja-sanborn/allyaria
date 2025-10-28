namespace Allyaria.Theming.Themes;

public sealed record ThemeGroupBorder(
    StyleGroupBorderRadius? BorderRadius = null,
    StyleGroupBorderStyle? BorderStyle = null,
    StyleGroupBorderWidth? BorderWidth = null,
    StyleValueNumber? OutlineOffset = null,
    StyleValueString? OutlineStyle = null,
    StyleValueNumber? OutlineWidth = null
)
{
    public static readonly ThemeGroupBorder Empty = new();

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder.Add(propertyName: "outline-offset", value: OutlineOffset, varPrefix: varPrefix);
        builder.Add(propertyName: "outline-style", value: OutlineStyle, varPrefix: varPrefix);
        builder.Add(propertyName: "outline-width", value: OutlineWidth, varPrefix: varPrefix);

        builder = BorderRadius?.BuildCss(builder: builder, varPrefix: varPrefix) ?? builder;
        builder = BorderStyle?.BuildCss(builder: builder, varPrefix: varPrefix) ?? builder;
        builder = BorderWidth?.BuildCss(builder: builder, varPrefix: varPrefix) ?? builder;

        return builder;
    }

    public ThemeGroupBorder Merge(ThemeGroupBorder other)
        => SetBorderRadius(value: other.BorderRadius ?? BorderRadius)
            .SetBorderStyle(value: other.BorderStyle ?? BorderStyle)
            .SetBorderWidth(value: other.BorderWidth ?? BorderWidth)
            .SetOutlineOffset(value: other.OutlineOffset ?? OutlineOffset)
            .SetOutlineStyle(value: other.OutlineStyle ?? OutlineStyle)
            .SetOutlineWidth(value: other.OutlineWidth ?? OutlineWidth);

    public ThemeGroupBorder SetBorderRadius(StyleGroupBorderRadius? value)
        => this with
        {
            BorderRadius = value
        };

    public ThemeGroupBorder SetBorderStyle(StyleGroupBorderStyle? value)
        => this with
        {
            BorderStyle = value
        };

    public ThemeGroupBorder SetBorderWidth(StyleGroupBorderWidth? value)
        => this with
        {
            BorderWidth = value
        };

    public ThemeGroupBorder SetOutlineOffset(StyleValueNumber? value)
        => this with
        {
            OutlineOffset = value
        };

    public ThemeGroupBorder SetOutlineStyle(StyleValueString? value)
        => this with
        {
            OutlineStyle = value
        };

    public ThemeGroupBorder SetOutlineWidth(StyleValueNumber? value)
        => this with
        {
            OutlineWidth = value
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
