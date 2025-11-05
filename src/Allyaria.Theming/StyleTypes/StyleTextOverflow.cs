namespace Allyaria.Theming.StyleTypes;

public sealed record StyleTextOverflow : StyleValueBase
{
    public StyleTextOverflow(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "clip")]
        Clip,

        [Description(description: "ellipsis")]
        Ellipsis
    }

    public static StyleTextOverflow Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleTextOverflow(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleTextOverflow? result)
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

    public static implicit operator StyleTextOverflow(string? value) => Parse(value: value);

    public static implicit operator string(StyleTextOverflow? value) => value?.Value ?? string.Empty;
}
