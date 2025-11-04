namespace Allyaria.Theming.Types;

public sealed record StyleTextDecorationStyle : StyleValueBase
{
    public StyleTextDecorationStyle(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "dashed")]
        Dashed,

        [Description(description: "dotted")]
        Dotted,

        [Description(description: "double")]
        Double,

        [Description(description: "solid")]
        Solid,

        [Description(description: "wavy")]
        Wavy
    }

    public static StyleTextDecorationStyle Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleTextDecorationStyle(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleTextDecorationStyle? result)
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

    public static implicit operator StyleTextDecorationStyle(string? value) => Parse(value: value);

    public static implicit operator string(StyleTextDecorationStyle? value) => value?.Value ?? string.Empty;
}
