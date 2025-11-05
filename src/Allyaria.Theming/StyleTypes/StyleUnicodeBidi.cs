namespace Allyaria.Theming.StyleTypes;

public sealed record StyleUnicodeBidi : StyleValueBase
{
    public StyleUnicodeBidi(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "bidi-override")]
        BidiOverride,

        [Description(description: "embed")]
        Embed,

        [Description(description: "isolate")]
        Isolate,

        [Description(description: "normal")]
        Normal
    }

    public static StyleUnicodeBidi Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleUnicodeBidi(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleUnicodeBidi? result)
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

    public static implicit operator StyleUnicodeBidi(string? value) => Parse(value: value);

    public static implicit operator string(StyleUnicodeBidi? value) => value?.Value ?? string.Empty;
}
