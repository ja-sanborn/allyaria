namespace Allyaria.Theming.StyleTypes;

public sealed record StyleTextOrientation : StyleValueBase
{
    public StyleTextOrientation(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "mixed")]
        Mixed,

        [Description(description: "sideways")]
        Sideways,

        [Description(description: "upright")]
        Upright
    }

    public static StyleTextOrientation Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleTextOrientation(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleTextOrientation? result)
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

    public static implicit operator StyleTextOrientation(string? value) => Parse(value: value);

    public static implicit operator string(StyleTextOrientation? value) => value?.Value ?? string.Empty;
}
