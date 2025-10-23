namespace Allyaria.Theming.Styles;

public sealed record StyleGroupBorderWidth : IStyleGroup
{
    public StyleGroupBorderWidth(StyleValueNumber value)
    {
        BlockEnd = value;
        BlockStart = value;
        InlineEnd = value;
        InlineStart = value;
    }

    public StyleGroupBorderWidth(StyleValueNumber block, StyleValueNumber inline)
    {
        BlockEnd = block;
        BlockStart = block;
        InlineEnd = inline;
        InlineStart = inline;
    }

    public StyleGroupBorderWidth(StyleValueNumber blockStart,
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
            .Add<StyleValueNumber>("border-block-end-width", BlockEnd, varPrefix)
            .Add<StyleValueNumber>("border-block-start-width", BlockStart, varPrefix)
            .Add<StyleValueNumber>("border-inline-end-width", InlineEnd, varPrefix)
            .Add<StyleValueNumber>("border-inline-start-width", InlineStart, varPrefix);

        return builder;
    }

    public StyleGroupBorderWidth SetBlockEnd(StyleValueNumber value)
        => this with
        {
            BlockEnd = value
        };

    public StyleGroupBorderWidth SetBlockStart(StyleValueNumber value)
        => this with
        {
            BlockStart = value
        };

    public StyleGroupBorderWidth SetInlineEnd(StyleValueNumber value)
        => this with
        {
            InlineEnd = value
        };

    public StyleGroupBorderWidth SetInlineStart(StyleValueNumber value)
        => this with
        {
            InlineStart = value
        };
}
