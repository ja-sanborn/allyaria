namespace Allyaria.Theming.StyleTypes;

public sealed record StyleTextAlign : StyleValueBase
{
    public StyleTextAlign(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "center")]
        Center,

        [Description(description: "end")]
        End,

        [Description(description: "justify")]
        Justify,

        [Description(description: "match-parent")]
        MatchParent,

        [Description(description: "start")]
        Start
    }

    public static StyleTextAlign Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleTextAlign(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleTextAlign? result)
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

    public static implicit operator StyleTextAlign(string? value) => Parse(value: value);

    public static implicit operator string(StyleTextAlign? value) => value?.Value ?? string.Empty;
}
