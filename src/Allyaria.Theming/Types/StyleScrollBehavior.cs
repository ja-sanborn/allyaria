namespace Allyaria.Theming.Types;

public sealed record StyleScrollBehavior : StyleValueBase
{
    public StyleScrollBehavior(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "auto")]
        Auto,

        [Description(description: "smooth")]
        Smooth
    }

    public static StyleScrollBehavior Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleScrollBehavior(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleScrollBehavior? result)
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

    public static implicit operator StyleScrollBehavior(string? value) => Parse(value: value);

    public static implicit operator string(StyleScrollBehavior? value) => value?.Value ?? string.Empty;
}
