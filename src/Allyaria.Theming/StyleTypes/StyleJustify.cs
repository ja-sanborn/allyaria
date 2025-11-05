namespace Allyaria.Theming.StyleTypes;

public sealed record StyleJustify : StyleValueBase
{
    public StyleJustify(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "center")]
        Center,

        [Description(description: "end")]
        End,

        [Description(description: "flex-end")]
        FlexEnd,

        [Description(description: "flex-start")]
        FlexStart,

        [Description(description: "normal")]
        Normal,

        [Description(description: "safe center")]
        SafeCenter,

        [Description(description: "space-around")]
        SpaceAround,

        [Description(description: "space-between")]
        SpaceBetween,

        [Description(description: "space-evenly")]
        SpaceEvenly,

        [Description(description: "start")]
        Start,

        [Description(description: "stretch")]
        Stretch,

        [Description(description: "unsafe center")]
        UnsafeCenter
    }

    public static StyleJustify Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleJustify(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleJustify? result)
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

    public static implicit operator StyleJustify(string? value) => Parse(value: value);

    public static implicit operator string(StyleJustify? value) => value?.Value ?? string.Empty;
}
