namespace Allyaria.Theming.StyleTypes;

public sealed record StyleVerticalAlign : StyleValueBase
{
    public StyleVerticalAlign(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "baseline")]
        Baseline,

        [Description(description: "bottom")]
        Bottom,

        [Description(description: "middle")]
        Middle,

        [Description(description: "sub")]
        Sub,

        [Description(description: "super")]
        Super,

        [Description(description: "text-bottom")]
        TextBottom,

        [Description(description: "text-top")]
        TextTop,

        [Description(description: "top")]
        Top
    }

    public static StyleVerticalAlign Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleVerticalAlign(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleVerticalAlign? result)
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

    public static implicit operator StyleVerticalAlign(string? value) => Parse(value: value);

    public static implicit operator string(StyleVerticalAlign? value) => value?.Value ?? string.Empty;
}
