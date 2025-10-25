namespace Allyaria.Theming.Styles;

public readonly record struct StyleGroupBorderWidth : IStyleGroup
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
            .Add<StyleValueNumber>(propertyName: "border-block-end-width", value: BlockEnd, varPrefix: varPrefix)
            .Add<StyleValueNumber>(propertyName: "border-block-start-width", value: BlockStart, varPrefix: varPrefix)
            .Add<StyleValueNumber>(propertyName: "border-inline-end-width", value: InlineEnd, varPrefix: varPrefix)
            .Add<StyleValueNumber>(propertyName: "border-inline-start-width", value: InlineStart, varPrefix: varPrefix);

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

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
