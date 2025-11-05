namespace Allyaria.Theming.StyleTypes;

public sealed record StyleOverscrollBehavior : StyleValueBase
{
    public StyleOverscrollBehavior(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "auto")]
        Auto,

        [Description(description: "contain")]
        Contain,

        [Description(description: "none")]
        None
    }

    public static StyleOverscrollBehavior Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleOverscrollBehavior(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleOverscrollBehavior? result)
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

    public static implicit operator StyleOverscrollBehavior(string? value) => Parse(value: value);

    public static implicit operator string(StyleOverscrollBehavior? value) => value?.Value ?? string.Empty;
}
