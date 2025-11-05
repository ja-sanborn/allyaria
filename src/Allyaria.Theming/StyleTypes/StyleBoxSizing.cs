namespace Allyaria.Theming.StyleTypes;

public sealed record StyleBoxSizing : StyleValueBase
{
    public StyleBoxSizing(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "border-box")]
        BorderBox,

        [Description(description: "content-box")]
        ContentBox,

        [Description(description: "inherit")]
        Inherit
    }

    public static StyleBoxSizing Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleBoxSizing(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleBoxSizing? result)
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

    public static implicit operator StyleBoxSizing(string? value) => Parse(value: value);

    public static implicit operator string(StyleBoxSizing? value) => value?.Value ?? string.Empty;
}
