namespace Allyaria.Theming.Styles;

public readonly record struct StyleGroupMargin : IStyleGroup
{
    public StyleGroupMargin(StyleValueNumber value)
    {
        BlockEnd = value;
        BlockStart = value;
        InlineEnd = value;
        InlineStart = value;
    }

    public StyleGroupMargin(StyleValueNumber block, StyleValueNumber inline)
    {
        BlockEnd = block;
        BlockStart = block;
        InlineEnd = inline;
        InlineStart = inline;
    }

    public StyleGroupMargin(StyleValueNumber blockStart,
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
            .Add<StyleValueNumber>(propertyName: "margin-block-end", value: BlockEnd, varPrefix: varPrefix)
            .Add<StyleValueNumber>(propertyName: "margin-block-start", value: BlockStart, varPrefix: varPrefix)
            .Add<StyleValueNumber>(propertyName: "margin-inline-end", value: InlineEnd, varPrefix: varPrefix)
            .Add<StyleValueNumber>(propertyName: "margin-inline-start", value: InlineStart, varPrefix: varPrefix);

        return builder;
    }

    public StyleGroupMargin SetBlockEnd(StyleValueNumber value)
        => this with
        {
            BlockEnd = value
        };

    public StyleGroupMargin SetBlockStart(StyleValueNumber value)
        => this with
        {
            BlockStart = value
        };

    public StyleGroupMargin SetInlineEnd(StyleValueNumber value)
        => this with
        {
            InlineEnd = value
        };

    public StyleGroupMargin SetInlineStart(StyleValueNumber value)
        => this with
        {
            InlineStart = value
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
