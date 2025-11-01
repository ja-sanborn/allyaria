namespace Allyaria.Theming.Types;

public sealed record StyleGroup : IStyleValue
{
    public StyleGroup(StyleGroupType type, IStyleValue value)
    {
        BlockEnd = value;
        BlockStart = value;
        InlineEnd = value;
        InlineStart = value;
    }

    public StyleGroup(StyleGroupType type, IStyleValue block, IStyleValue inline)
    {
        BlockEnd = block;
        BlockStart = block;
        InlineEnd = inline;
        InlineStart = inline;
    }

    public StyleGroup(StyleGroupType type,
        IStyleValue blockStart,
        IStyleValue blockEnd,
        IStyleValue inlineStart,
        IStyleValue inlineEnd)
    {
        BlockEnd = blockEnd;
        BlockStart = blockStart;
        InlineEnd = inlineEnd;
        InlineStart = inlineStart;
    }

    public IStyleValue BlockEnd { get; init; }

    public IStyleValue BlockStart { get; init; }

    public IStyleValue InlineEnd { get; init; }

    public IStyleValue InlineStart { get; init; }

    public StyleGroupType Type { get; init; }

    public string Value => $"{BlockEnd.Value} {BlockStart.Value} {InlineEnd.Value} {InlineStart.Value}";

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = "")
    {
        var typeDescription = Type.GetDescription();
        var typeList = typeDescription.Split(separator: ',');

        builder
            .Add(name: typeList[0], value: BlockEnd.Value, varPrefix: varPrefix)
            .Add(name: typeList[1], value: BlockStart.Value, varPrefix: varPrefix)
            .Add(name: typeList[2], value: InlineEnd.Value, varPrefix: varPrefix)
            .Add(name: typeList[3], value: InlineStart.Value, varPrefix: varPrefix);

        return builder;
    }

    public string ToCss(string? varPrefix = null)
        => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
