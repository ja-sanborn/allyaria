namespace Allyaria.Theming.Types;

public sealed record StyleOverflow : StyleValueBase
{
    public StyleOverflow(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "auto")]
        Auto,

        [Description(description: "clip")]
        Clip,

        [Description(description: "hidden")]
        Hidden,

        [Description(description: "scroll")]
        Scroll,

        [Description(description: "visible")]
        Visible
    }

    public static StyleOverflow Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleOverflow(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleOverflow? result)
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

    public static implicit operator StyleOverflow(string? value) => Parse(value: value);

    public static implicit operator string(StyleOverflow? value) => value?.Value ?? string.Empty;
}
