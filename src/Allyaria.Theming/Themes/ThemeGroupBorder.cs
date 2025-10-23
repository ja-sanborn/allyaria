namespace Allyaria.Theming.Themes;

public sealed record ThemeGroupBorder(
    StyleGroupBorderRadius? BorderRadius = null,
    StyleGroupBorderStyle? BorderStyle = null,
    StyleGroupBorderWidth? BorderWidth = null
) : IThemeGroup
{
    public static readonly ThemeGroupBorder Empty = new();

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder = BorderRadius?.BuildCss(builder, varPrefix) ?? builder;
        builder = BorderStyle?.BuildCss(builder, varPrefix) ?? builder;
        builder = BorderWidth?.BuildCss(builder, varPrefix) ?? builder;

        return builder;
    }

    public static ThemeGroupBorder FromDefault()
        => new(
            new StyleGroupBorderRadius(Sizing.Size2),
            new StyleGroupBorderStyle(Constants.BorderStyle.None),
            new StyleGroupBorderWidth(Sizing.Size0)
        );

    public ThemeGroupBorder Merge(ThemeGroupBorder other)
        => SetBorderRadius(other.BorderRadius ?? BorderRadius)
            .SetBorderStyle(other.BorderStyle ?? BorderStyle)
            .SetBorderWidth(other.BorderWidth ?? BorderWidth);

    public ThemeGroupBorder SetBorderRadius(StyleGroupBorderRadius? value)
        => value is not null
            ? SetBorderRadius(value.StartStart, value.StartEnd, value.EndStart, value.EndEnd)
            : this with
            {
                BorderRadius = null
            };

    public ThemeGroupBorder SetBorderRadius(StyleValueNumber? value)
        => value is not null
            ? SetBorderRadius(value.Value, value.Value, value.Value, value.Value)
            : this with
            {
                BorderRadius = null
            };

    public ThemeGroupBorder SetBorderRadius(StyleValueNumber start, StyleValueNumber end)
        => SetBorderRadius(start, start, end, end);

    public ThemeGroupBorder SetBorderRadius(StyleValueNumber startStart,
        StyleValueNumber startEnd,
        StyleValueNumber endStart,
        StyleValueNumber endEnd)
        => this with
        {
            BorderRadius = BorderRadius is null
                ? new StyleGroupBorderRadius(startStart, startEnd, endStart, endEnd)
                : BorderRadius
                    .SetEndEnd(endEnd)
                    .SetEndStart(endStart)
                    .SetStartEnd(startEnd)
                    .SetStartStart(startStart)
        };

    public ThemeGroupBorder SetBorderStyle(StyleGroupBorderStyle? value)
        => value is not null
            ? SetBorderStyle(value.BlockStart, value.BlockEnd, value.InlineStart, value.InlineEnd)
            : this with
            {
                BorderStyle = null
            };

    public ThemeGroupBorder SetBorderStyle(StyleValueString? value)
        => value is not null
            ? SetBorderStyle(value.Value, value.Value, value.Value, value.Value)
            : this with
            {
                BorderStyle = null
            };

    public ThemeGroupBorder SetBorderStyle(StyleValueString block, StyleValueString inline)
        => SetBorderStyle(block, block, inline, inline);

    public ThemeGroupBorder SetBorderStyle(StyleValueString blockStart,
        StyleValueString blockEnd,
        StyleValueString inlineStart,
        StyleValueString inlineEnd)
        => this with
        {
            BorderStyle = BorderStyle is null
                ? new StyleGroupBorderStyle(blockStart, blockEnd, inlineStart, inlineEnd)
                : BorderStyle
                    .SetBlockEnd(blockEnd)
                    .SetBlockStart(blockStart)
                    .SetInlineEnd(inlineEnd)
                    .SetInlineStart(inlineStart)
        };

    public ThemeGroupBorder SetBorderWidth(StyleGroupBorderWidth? value)
        => value is not null
            ? SetBorderWidth(value.BlockStart, value.BlockEnd, value.InlineStart, value.InlineEnd)
            : this with
            {
                BorderWidth = null
            };

    public ThemeGroupBorder SetBorderWidth(StyleValueNumber? value)
        => value is not null
            ? SetBorderWidth(value.Value, value.Value, value.Value, value.Value)
            : this with
            {
                BorderWidth = null
            };

    public ThemeGroupBorder SetBorderWidth(StyleValueNumber block, StyleValueNumber inline)
        => SetBorderWidth(block, block, inline, inline);

    public ThemeGroupBorder SetBorderWidth(StyleValueNumber blockStart,
        StyleValueNumber blockEnd,
        StyleValueNumber inlineStart,
        StyleValueNumber inlineEnd)
        => this with
        {
            BorderWidth = BorderWidth is null
                ? new StyleGroupBorderWidth(blockStart, blockEnd, inlineStart, inlineEnd)
                : BorderWidth
                    .SetBlockEnd(blockEnd)
                    .SetBlockStart(blockStart)
                    .SetInlineEnd(inlineEnd)
                    .SetInlineStart(inlineStart)
        };
}
