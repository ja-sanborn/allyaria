namespace Allyaria.Theming.Themes;

public sealed partial record ThemeGroupBorder(
    StyleGroupBorderRadius? BorderRadius = null,
    StyleGroupBorderStyle? BorderStyle = null,
    StyleGroupBorderWidth? BorderWidth = null,
    StyleValueNumber? OutlineOffset = null,
    StyleValueString? OutlineStyle = null,
    StyleValueNumber? OutlineWidth = null
)
{
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
        => SetBorderRadius(other.BorderRadius ?? BorderRadius)
            .SetBorderStyle(other.BorderStyle ?? BorderStyle)
            .SetBorderWidth(other.BorderWidth ?? BorderWidth)
            .SetOutlineOffset(other.OutlineOffset ?? OutlineOffset)
            .SetOutlineStyle(other.OutlineStyle ?? OutlineStyle)
            .SetOutlineWidth(other.OutlineWidth ?? OutlineWidth);

    public ThemeGroupBorder SetBorderRadius(StyleGroupBorderRadius? value)
        => value is not null
            ? SetBorderRadius(
                startStart: value.Value.StartStart, startEnd: value.Value.StartEnd, endStart: value.Value.EndStart,
                endEnd: value.Value.EndEnd
            )
            : this with
            {
                BorderRadius = null
            };

    public ThemeGroupBorder SetBorderRadius(StyleValueNumber? value)
        => value is not null
            ? SetBorderRadius(
                startStart: value.Value, startEnd: value.Value, endStart: value.Value, endEnd: value.Value
            )
            : this with
            {
                BorderRadius = null
            };

    public ThemeGroupBorder SetBorderRadius(StyleValueNumber start, StyleValueNumber end)
        => SetBorderRadius(startStart: start, startEnd: start, endStart: end, endEnd: end);

    public ThemeGroupBorder SetBorderRadius(StyleValueNumber startStart,
        StyleValueNumber startEnd,
        StyleValueNumber endStart,
        StyleValueNumber endEnd)
        => this with
        {
            BorderRadius = BorderRadius is null
                ? new StyleGroupBorderRadius(
                    startStart: startStart, startEnd: startEnd, endStart: endStart, endEnd: endEnd
                )
                : BorderRadius.Value
                    .SetEndEnd(endEnd)
                    .SetEndStart(endStart)
                    .SetStartEnd(startEnd)
                    .SetStartStart(startStart)
        };

    public ThemeGroupBorder SetBorderStyle(StyleGroupBorderStyle? value)
        => value is not null
            ? SetBorderStyle(
                blockStart: value.Value.BlockStart, blockEnd: value.Value.BlockEnd,
                inlineStart: value.Value.InlineStart, inlineEnd: value.Value.InlineEnd
            )
            : this with
            {
                BorderStyle = null
            };

    public ThemeGroupBorder SetBorderStyle(StyleValueString? value)
        => value is not null
            ? SetBorderStyle(
                blockStart: value.Value, blockEnd: value.Value, inlineStart: value.Value, inlineEnd: value.Value
            )
            : this with
            {
                BorderStyle = null
            };

    public ThemeGroupBorder SetBorderStyle(StyleValueString block, StyleValueString inline)
        => SetBorderStyle(blockStart: block, blockEnd: block, inlineStart: inline, inlineEnd: inline);

    public ThemeGroupBorder SetBorderStyle(StyleValueString blockStart,
        StyleValueString blockEnd,
        StyleValueString inlineStart,
        StyleValueString inlineEnd)
        => this with
        {
            BorderStyle = BorderStyle is null
                ? new StyleGroupBorderStyle(
                    blockStart: blockStart, blockEnd: blockEnd, inlineStart: inlineStart, inlineEnd: inlineEnd
                )
                : BorderStyle.Value
                    .SetBlockEnd(blockEnd)
                    .SetBlockStart(blockStart)
                    .SetInlineEnd(inlineEnd)
                    .SetInlineStart(inlineStart)
        };

    public ThemeGroupBorder SetBorderWidth(StyleGroupBorderWidth? value)
        => value is not null
            ? SetBorderWidth(
                blockStart: value.Value.BlockStart, blockEnd: value.Value.BlockEnd,
                inlineStart: value.Value.InlineStart, inlineEnd: value.Value.InlineEnd
            )
            : this with
            {
                BorderWidth = null
            };

    public ThemeGroupBorder SetBorderWidth(StyleValueNumber? value)
        => value is not null
            ? SetBorderWidth(
                blockStart: value.Value, blockEnd: value.Value, inlineStart: value.Value, inlineEnd: value.Value
            )
            : this with
            {
                BorderWidth = null
            };

    public ThemeGroupBorder SetBorderWidth(StyleValueNumber block, StyleValueNumber inline)
        => SetBorderWidth(blockStart: block, blockEnd: block, inlineStart: inline, inlineEnd: inline);

    public ThemeGroupBorder SetBorderWidth(StyleValueNumber blockStart,
        StyleValueNumber blockEnd,
        StyleValueNumber inlineStart,
        StyleValueNumber inlineEnd)
        => this with
        {
            BorderWidth = BorderWidth is null
                ? new StyleGroupBorderWidth(
                    blockStart: blockStart, blockEnd: blockEnd, inlineStart: inlineStart, inlineEnd: inlineEnd
                )
                : BorderWidth.Value
                    .SetBlockEnd(blockEnd)
                    .SetBlockStart(blockStart)
                    .SetInlineEnd(inlineEnd)
                    .SetInlineStart(inlineStart)
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
