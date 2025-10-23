namespace Allyaria.Theming.Styles;

public sealed record StyleGroupPadding : IStyleGroup
{
    public StyleGroupPadding(StyleValueNumber value)
    {
        BlockEnd = value;
        BlockStart = value;
        InlineEnd = value;
        InlineStart = value;
    }

    public StyleGroupPadding(StyleValueNumber block, StyleValueNumber inline)
    {
        BlockEnd = block;
        BlockStart = block;
        InlineEnd = inline;
        InlineStart = inline;
    }

    public StyleGroupPadding(StyleValueNumber blockStart,
        StyleValueNumber blockEnd,
        StyleValueNumber inlineStart,
        StyleValueNumber inlineEnd)
    {
        BlockEnd = blockEnd;
        BlockStart = blockStart;
        InlineEnd = inlineEnd;
        InlineStart = inlineStart;
    }

    public StyleValueNumber BlockEnd { get; init; }

    public StyleValueNumber BlockStart { get; init; }

    public StyleValueNumber InlineEnd { get; init; }

    public StyleValueNumber InlineStart { get; init; }

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = "")
    {
        builder
            .Add<StyleValueNumber>("padding-block-end", BlockEnd, varPrefix)
            .Add<StyleValueNumber>("padding-block-start", BlockStart, varPrefix)
            .Add<StyleValueNumber>("padding-inline-end", InlineEnd, varPrefix)
            .Add<StyleValueNumber>("padding-inline-start", InlineStart, varPrefix);

        return builder;
    }

    public StyleGroupPadding SetBlockEnd(StyleValueNumber value)
        => this with
        {
            BlockEnd = value
        };

    public StyleGroupPadding SetBlockStart(StyleValueNumber value)
        => this with
        {
            BlockStart = value
        };

    public StyleGroupPadding SetInlineEnd(StyleValueNumber value)
        => this with
        {
            InlineEnd = value
        };

    public StyleGroupPadding SetInlineStart(StyleValueNumber value)
        => this with
        {
            InlineStart = value
        };
}
