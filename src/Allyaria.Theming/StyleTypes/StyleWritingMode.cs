namespace Allyaria.Theming.StyleTypes;

public sealed record StyleWritingMode : StyleValueBase
{
    public StyleWritingMode(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "horizontal-tb")]
        HorizontalTb,

        [Description(description: "sideways-lr")]
        SidewaysLr,

        [Description(description: "sideways-rl")]
        SidewaysRl,

        [Description(description: "vertical-lr")]
        VerticalLr,

        [Description(description: "vertical-rl")]
        VerticalRl
    }

    public static StyleWritingMode Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleWritingMode(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleWritingMode? result)
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

    public static implicit operator StyleWritingMode(string? value) => Parse(value: value);

    public static implicit operator string(StyleWritingMode? value) => value?.Value ?? string.Empty;
}
