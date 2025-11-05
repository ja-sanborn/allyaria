namespace Allyaria.Theming.StyleTypes;

public sealed record StyleAlign : StyleValueBase
{
    public StyleAlign(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "baseline")]
        Baseline,

        [Description(description: "center")]
        Center,

        [Description(description: "end")]
        End,

        [Description(description: "first baseline")]
        FirstBaseline,

        [Description(description: "flex-end")]
        FlexEnd,

        [Description(description: "flex-start")]
        FlexStart,

        [Description(description: "last baseline")]
        LastBaseline,

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

    public static StyleAlign Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleAlign(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleAlign? result)
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

    public static implicit operator StyleAlign(string? value) => Parse(value: value);

    public static implicit operator string(StyleAlign? value) => value?.Value ?? string.Empty;
}
