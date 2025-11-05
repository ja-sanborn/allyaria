namespace Allyaria.Theming.StyleTypes;

public sealed record StyleFontStyle : StyleValueBase
{
    public StyleFontStyle(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "italic")]
        Italic,

        [Description(description: "normal")]
        Normal,

        [Description(description: "oblique")]
        Oblique
    }

    public static StyleFontStyle Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleFontStyle(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleFontStyle? result)
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

    public static implicit operator StyleFontStyle(string? value) => Parse(value: value);

    public static implicit operator string(StyleFontStyle? value) => value?.Value ?? string.Empty;
}
