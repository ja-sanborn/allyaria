namespace Allyaria.Theming.StyleTypes;

public sealed record StyleHyphens : StyleValueBase
{
    public StyleHyphens(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "auto")]
        Auto,

        [Description(description: "manual")]
        Manual,

        [Description(description: "none")]
        None
    }

    public static StyleHyphens Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleHyphens(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleHyphens? result)
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

    public static implicit operator StyleHyphens(string? value) => Parse(value: value);

    public static implicit operator string(StyleHyphens? value) => value?.Value ?? string.Empty;
}
