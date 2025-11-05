namespace Allyaria.Theming.StyleTypes;

public sealed record StyleLineBreak : StyleValueBase
{
    public StyleLineBreak(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "anywhere")]
        Anywhere,

        [Description(description: "auto")]
        Auto,

        [Description(description: "loose")]
        Loose,

        [Description(description: "normal")]
        Normal,

        [Description(description: "strict")]
        Strict
    }

    public static StyleLineBreak Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleLineBreak(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleLineBreak? result)
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

    public static implicit operator StyleLineBreak(string? value) => Parse(value: value);

    public static implicit operator string(StyleLineBreak? value) => value?.Value ?? string.Empty;
}
