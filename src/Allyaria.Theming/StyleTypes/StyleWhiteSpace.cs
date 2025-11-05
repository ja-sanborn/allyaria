namespace Allyaria.Theming.StyleTypes;

public sealed record StyleWhiteSpace : StyleValueBase
{
    public StyleWhiteSpace(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "break-spaces")]
        BreakSpaces,

        [Description(description: "collapse")]
        Collapse,

        [Description(description: "normal")]
        Normal,

        [Description(description: "nowrap")]
        Nowrap,

        [Description(description: "pre")]
        Pre,

        [Description(description: "pre-line")]
        PreLine,

        [Description(description: "preserve nowrap")]
        PreserveNowrap,

        [Description(description: "pre-wrap")]
        PreWrap
    }

    public static StyleWhiteSpace Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleWhiteSpace(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleWhiteSpace? result)
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

    public static implicit operator StyleWhiteSpace(string? value) => Parse(value: value);

    public static implicit operator string(StyleWhiteSpace? value) => value?.Value ?? string.Empty;
}
