namespace Allyaria.Theming.Styles;

public readonly record struct StyleGroupBorderStyle : IStyleGroup
{
    public StyleGroupBorderStyle(StyleValueString value)
    {
        BlockEnd = value;
        BlockStart = value;
        InlineEnd = value;
        InlineStart = value;
    }

    public StyleGroupBorderStyle(StyleValueString block, StyleValueString inline)
    {
        BlockEnd = block;
        BlockStart = block;
        InlineEnd = inline;
        InlineStart = inline;
    }

    public StyleGroupBorderStyle(StyleValueString blockStart,
        StyleValueString blockEnd,
        StyleValueString inlineStart,
        StyleValueString inlineEnd)
    {
        BlockEnd = blockEnd;
        BlockStart = blockStart;
        InlineEnd = inlineEnd;
        InlineStart = inlineStart;
    }

    public StyleValueString BlockEnd { get; init; }

    public StyleValueString BlockStart { get; init; }

    public StyleValueString InlineEnd { get; init; }

    public StyleValueString InlineStart { get; init; }

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = "")
    {
        builder
            .Add<StyleValueString>(propertyName: "border-block-end-style", value: BlockEnd, varPrefix: varPrefix)
            .Add<StyleValueString>(propertyName: "border-block-start-style", value: BlockStart, varPrefix: varPrefix)
            .Add<StyleValueString>(propertyName: "border-inline-end-style", value: InlineEnd, varPrefix: varPrefix)
            .Add<StyleValueString>(propertyName: "border-inline-start-style", value: InlineStart, varPrefix: varPrefix);

        return builder;
    }

    public StyleGroupBorderStyle SetBlockEnd(StyleValueString value)
        => this with
        {
            BlockEnd = value
        };

    public StyleGroupBorderStyle SetBlockStart(StyleValueString value)
        => this with
        {
            BlockStart = value
        };

    public StyleGroupBorderStyle SetInlineEnd(StyleValueString value)
        => this with
        {
            InlineEnd = value
        };

    public StyleGroupBorderStyle SetInlineStart(StyleValueString value)
        => this with
        {
            InlineStart = value
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
