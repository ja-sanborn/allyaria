namespace Allyaria.Theming.StyleTypes;

public sealed record StyleOverflowWrap : StyleValueBase
{
    public StyleOverflowWrap(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "anywhere")]
        Anywhere,

        [Description(description: "break-word")]
        BreakWord,

        [Description(description: "normal")]
        Normal
    }

    public static StyleOverflowWrap Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleOverflowWrap(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleOverflowWrap? result)
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

    public static implicit operator StyleOverflowWrap(string? value) => Parse(value: value);

    public static implicit operator string(StyleOverflowWrap? value) => value?.Value ?? string.Empty;
}
