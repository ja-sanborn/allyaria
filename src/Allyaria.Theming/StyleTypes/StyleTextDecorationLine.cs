namespace Allyaria.Theming.StyleTypes;

public sealed record StyleTextDecorationLine : StyleValueBase
{
    public StyleTextDecorationLine(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "overline line-through underline")]
        All,

        [Description(description: "line-through")]
        LineThrough,

        [Description(description: "none")]
        None,

        [Description(description: "overline")]
        Overline,

        [Description(description: "overline line-through")]
        OverlineLineThrough,

        [Description(description: "overline underline")]
        OverlineUnderline,

        [Description(description: "underline")]
        Underline,

        [Description(description: "underline line-through")]
        UnderlineLineThrough
    }

    public static StyleTextDecorationLine Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleTextDecorationLine(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleTextDecorationLine? result)
    {
        try
        {
            result = Parse(value: value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    public static implicit operator StyleTextDecorationLine(string? value) => Parse(value: value);

    public static implicit operator string(StyleTextDecorationLine? value) => value?.Value ?? string.Empty;
}
