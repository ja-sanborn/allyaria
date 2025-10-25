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
        => value is not null
            ? SetMargin(
                blockStart: value.Value.BlockStart, blockEnd: value.Value.BlockEnd,
                inlineStart: value.Value.InlineStart, inlineEnd: value.Value.InlineEnd
            )
            : this with
            {
                Margin = null
            };

    public ThemeGroupSizing SetMargin(StyleValueNumber? value)
        => value is not null
            ? SetMargin(
                blockStart: value.Value, blockEnd: value.Value, inlineStart: value.Value, inlineEnd: value.Value
            )
            : this with
            {
                Margin = null
            };

    public ThemeGroupSizing SetMargin(StyleValueNumber block, StyleValueNumber inline)
        => SetMargin(blockStart: block, blockEnd: block, inlineStart: inline, inlineEnd: inline);

    public ThemeGroupSizing SetMargin(StyleValueNumber blockStart,
        StyleValueNumber blockEnd,
        StyleValueNumber inlineStart,
        StyleValueNumber inlineEnd)
        => this with
        {
            Margin = Margin is null
                ? new StyleGroupMargin(
                    blockStart: blockStart, blockEnd: blockEnd, inlineStart: inlineStart, inlineEnd: inlineEnd
                )
                : Margin.Value
                    .SetBlockEnd(value: blockEnd)
                    .SetBlockStart(value: blockStart)
                    .SetInlineEnd(value: inlineEnd)
                    .SetInlineStart(value: inlineStart)
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
        => value is not null
            ? SetPadding(
                blockStart: value.Value.BlockStart, blockEnd: value.Value.BlockEnd,
                inlineStart: value.Value.InlineStart, inlineEnd: value.Value.InlineEnd
            )
            : this with
            {
                Padding = null
            };

    public ThemeGroupSizing SetPadding(StyleValueNumber? value)
        => value is not null
            ? SetPadding(
                blockStart: value.Value, blockEnd: value.Value, inlineStart: value.Value, inlineEnd: value.Value
            )
            : this with
            {
                Padding = null
            };

    public ThemeGroupSizing SetPadding(StyleValueNumber block, StyleValueNumber inline)
        => SetPadding(blockStart: block, blockEnd: block, inlineStart: inline, inlineEnd: inline);

    public ThemeGroupSizing SetPadding(StyleValueNumber blockStart,
        StyleValueNumber blockEnd,
        StyleValueNumber inlineStart,
        StyleValueNumber inlineEnd)
        => this with
        {
            Padding = Padding is null
                ? new StyleGroupPadding(
                    blockStart: blockStart, blockEnd: blockEnd, inlineStart: inlineStart, inlineEnd: inlineEnd
                )
                : Padding.Value
                    .SetBlockEnd(value: blockEnd)
                    .SetBlockStart(value: blockStart)
                    .SetInlineEnd(value: inlineEnd)
                    .SetInlineStart(value: inlineStart)
        };

    public ThemeGroupSizing SetWidth(StyleValueNumber? value)
        => this with
        {
            Width = value
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
