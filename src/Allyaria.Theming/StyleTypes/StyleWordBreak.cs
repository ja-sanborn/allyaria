namespace Allyaria.Theming.StyleTypes;

public sealed record StyleWordBreak : StyleValueBase
{
    public StyleWordBreak(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "break-all")]
        BreakAll,

        [Description(description: "break-word")]
        BreakWord,

        [Description(description: "keep-all")]
        KeepAll,

        [Description(description: "normal")]
        Normal
    }

    public static StyleWordBreak Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleWordBreak(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleWordBreak? result)
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

    public static implicit operator StyleWordBreak(string? value) => Parse(value: value);

    public static implicit operator string(StyleWordBreak? value) => value?.Value ?? string.Empty;
}
