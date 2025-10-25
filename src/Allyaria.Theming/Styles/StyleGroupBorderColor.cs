namespace Allyaria.Theming.Styles;

public readonly record struct StyleGroupBorderColor : IStyleGroup
{
    public StyleGroupBorderColor(StyleValueColor value)
    {
        BlockEnd = value;
        BlockStart = value;
        InlineEnd = value;
        InlineStart = value;
    }

    public StyleGroupBorderColor(StyleValueColor block, StyleValueColor inline)
    {
        BlockEnd = block;
        BlockStart = block;
        InlineEnd = inline;
        InlineStart = inline;
    }

    public StyleGroupBorderColor(StyleValueColor blockStart,
        StyleValueColor blockEnd,
        StyleValueColor inlineStart,
        StyleValueColor inlineEnd)
    {
        BlockEnd = blockEnd;
        BlockStart = blockStart;
        InlineEnd = inlineEnd;
        InlineStart = inlineStart;
    }

    public StyleValueColor BlockEnd { get; init; }

    public StyleValueColor BlockStart { get; init; }

    public StyleValueColor InlineEnd { get; init; }

    public StyleValueColor InlineStart { get; init; }

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = "")
    {
        builder
            .Add<StyleValueColor>(propertyName: "border-block-end-color", value: BlockEnd, varPrefix: varPrefix)
            .Add<StyleValueColor>(propertyName: "border-block-start-color", value: BlockStart, varPrefix: varPrefix)
            .Add<StyleValueColor>(propertyName: "border-inline-end-color", value: InlineEnd, varPrefix: varPrefix)
            .Add<StyleValueColor>(propertyName: "border-inline-start-color", value: InlineStart, varPrefix: varPrefix);

        return builder;
    }

    public StyleGroupBorderColor EnsureContrast(StyleValueColor backgroundColor)
    {
        if (BlockStart.Equals(BlockEnd) && BlockStart.Equals(InlineStart) &&
            BlockStart.Equals(InlineEnd))
        {
            var contrasted = BlockStart.EnsureContrast(backgroundColor);

            return new StyleGroupBorderColor(contrasted);
        }

        return new StyleGroupBorderColor(
            blockStart: BlockStart.EnsureContrast(backgroundColor),
            blockEnd: BlockEnd.EnsureContrast(backgroundColor),
            inlineStart: InlineStart.EnsureContrast(backgroundColor),
            inlineEnd: InlineEnd.EnsureContrast(backgroundColor)
        );
    }

    public StyleGroupBorderColor SetBlockEnd(StyleValueColor value)
        => this with
        {
            BlockEnd = value
        };

    public StyleGroupBorderColor SetBlockStart(StyleValueColor value)
        => this with
        {
            BlockStart = value
        };

    public StyleGroupBorderColor SetInlineEnd(StyleValueColor value)
        => this with
        {
            InlineEnd = value
        };

    public StyleGroupBorderColor SetInlineStart(StyleValueColor value)
        => this with
        {
            InlineStart = value
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();

    public StyleGroupBorderColor ToDisabled()
        => this with
        {
            BlockEnd = BlockEnd.ToDisabled(),
            BlockStart = BlockStart.ToDisabled(),
            InlineEnd = InlineEnd.ToDisabled(),
            InlineStart = InlineStart.ToDisabled()
        };

    public StyleGroupBorderColor ToDragged()
        => this with
        {
            BlockEnd = BlockEnd.ToDragged(),
            BlockStart = BlockStart.ToDragged(),
            InlineEnd = InlineEnd.ToDragged(),
            InlineStart = InlineStart.ToDragged()
        };

    public StyleGroupBorderColor ToFocused()
        => this with
        {
            BlockEnd = BlockEnd.ToFocused(),
            BlockStart = BlockStart.ToFocused(),
            InlineEnd = InlineEnd.ToFocused(),
            InlineStart = InlineStart.ToFocused()
        };

    public StyleGroupBorderColor ToHovered()
        => this with
        {
            BlockEnd = BlockEnd.ToHovered(),
            BlockStart = BlockStart.ToHovered(),
            InlineEnd = InlineEnd.ToHovered(),
            InlineStart = InlineStart.ToHovered()
        };

    public StyleGroupBorderColor ToPressed()
        => this with
        {
            BlockEnd = BlockEnd.ToPressed(),
            BlockStart = BlockStart.ToPressed(),
            InlineEnd = InlineEnd.ToPressed(),
            InlineStart = InlineStart.ToPressed()
        };

    public StyleGroupBorderColor ToVisited()
        => this with
        {
            BlockEnd = BlockEnd.ToVisited(),
            BlockStart = BlockStart.ToVisited(),
            InlineEnd = InlineEnd.ToVisited(),
            InlineStart = InlineStart.ToVisited()
        };
}
