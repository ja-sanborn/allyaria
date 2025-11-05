namespace Allyaria.Theming.StyleTypes;

public sealed record StylePosition : StyleValueBase
{
    public StylePosition(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "absolute")]
        Absolute,

        [Description(description: "fixed")]
        Fixed,

        [Description(description: "relative")]
        Relative,

        [Description(description: "static")]
        Static,

        [Description(description: "sticky")]
        Sticky
    }

    public static StylePosition Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StylePosition(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StylePosition? result)
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

    public static implicit operator StylePosition(string? value) => Parse(value: value);

    public static implicit operator string(StylePosition? value) => value?.Value ?? string.Empty;
}
