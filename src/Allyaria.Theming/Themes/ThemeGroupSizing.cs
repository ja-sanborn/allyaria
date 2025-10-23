namespace Allyaria.Theming.Themes;

public sealed record ThemeGroupSizing(
    StyleValueNumber? Height = null,
    StyleGroupMargin? Margin = null,
    StyleValueNumber? MaxHeight = null,
    StyleValueNumber? MaxWidth = null,
    StyleGroupPadding? Padding = null,
    StyleValueNumber? Width = null
) : IThemeGroup
{
    public static readonly ThemeGroupSizing Empty = new();

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder
            .Add("height", Height, varPrefix)
            .Add("max-height", MaxHeight, varPrefix)
            .Add("max-width", MaxWidth, varPrefix)
            .Add("width", Width, varPrefix);

        builder = Margin?.BuildCss(builder, varPrefix) ?? builder;
        builder = Padding?.BuildCss(builder, varPrefix) ?? builder;

        return builder;
    }

    public static ThemeGroupSizing FromDefault()
        => new(
            Margin: new StyleGroupMargin(Sizing.Size2),
            Padding: new StyleGroupPadding(Sizing.Size3)
        );

    public ThemeGroupSizing Merge(ThemeGroupSizing other)
        => SetHeight(other.Height ?? Height)
            .SetMargin(other.Margin ?? Margin)
            .SetMaxHeight(other.MaxHeight ?? MaxHeight)
            .SetMaxWidth(other.MaxWidth ?? MaxWidth)
            .SetPadding(other.Padding ?? Padding)
            .SetWidth(other.Width ?? Width);

    public ThemeGroupSizing SetHeight(StyleValueNumber? value)
        => this with
        {
            Height = value
        };

    public ThemeGroupSizing SetMargin(StyleGroupMargin? value)
        => value is not null
            ? SetMargin(value.BlockStart, value.BlockEnd, value.InlineStart, value.InlineEnd)
            : this with
            {
                Margin = null
            };

    public ThemeGroupSizing SetMargin(StyleValueNumber? value)
        => value is not null
            ? SetMargin(value.Value, value.Value, value.Value, value.Value)
            : this with
            {
                Margin = null
            };

    public ThemeGroupSizing SetMargin(StyleValueNumber block, StyleValueNumber inline)
        => SetMargin(block, block, inline, inline);

    public ThemeGroupSizing SetMargin(StyleValueNumber blockStart,
        StyleValueNumber blockEnd,
        StyleValueNumber inlineStart,
        StyleValueNumber inlineEnd)
        => this with
        {
            Margin = Margin is null
                ? new StyleGroupMargin(blockStart, blockEnd, inlineStart, inlineEnd)
                : Margin
                    .SetBlockEnd(blockEnd)
                    .SetBlockStart(blockStart)
                    .SetInlineEnd(inlineEnd)
                    .SetInlineStart(inlineStart)
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
            ? SetPadding(value.BlockStart, value.BlockEnd, value.InlineStart, value.InlineEnd)
            : this with
            {
                Padding = null
            };

    public ThemeGroupSizing SetPadding(StyleValueNumber? value)
        => value is not null
            ? SetPadding(value.Value, value.Value, value.Value, value.Value)
            : this with
            {
                Padding = null
            };

    public ThemeGroupSizing SetPadding(StyleValueNumber block, StyleValueNumber inline)
        => SetPadding(block, block, inline, inline);

    public ThemeGroupSizing SetPadding(StyleValueNumber blockStart,
        StyleValueNumber blockEnd,
        StyleValueNumber inlineStart,
        StyleValueNumber inlineEnd)
        => this with
        {
            Padding = Padding is null
                ? new StyleGroupPadding(blockStart, blockEnd, inlineStart, inlineEnd)
                : Padding
                    .SetBlockEnd(blockEnd)
                    .SetBlockStart(blockStart)
                    .SetInlineEnd(inlineEnd)
                    .SetInlineStart(inlineStart)
        };

    public ThemeGroupSizing SetWidth(StyleValueNumber? value)
        => this with
        {
            Width = value
        };
}
