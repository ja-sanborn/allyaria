namespace Allyaria.Theming.StyleTypes;

public sealed record StyleTextTransform : StyleValueBase
{
    public StyleTextTransform(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "capitalize")]
        Capitalize,

        [Description(description: "lowercase")]
        Lowercase,

        [Description(description: "none")]
        None,

        [Description(description: "uppercase")]
        Uppercase
    }

    public static StyleTextTransform Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleTextTransform(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleTextTransform? result)
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

    public static implicit operator StyleTextTransform(string? value) => Parse(value: value);

    public static implicit operator string(StyleTextTransform? value) => value?.Value ?? string.Empty;
}
