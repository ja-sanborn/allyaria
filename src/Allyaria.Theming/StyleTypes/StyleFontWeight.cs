namespace Allyaria.Theming.StyleTypes;

public sealed record StyleFontWeight : StyleValueBase
{
    public StyleFontWeight(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "bold")]
        Bold,

        [Description(description: "bolder")]
        Bolder,

        [Description(description: "lighter")]
        Lighter,

        [Description(description: "normal")]
        Normal,

        [Description(description: "100")]
        Weight100,

        [Description(description: "200")]
        Weight200,

        [Description(description: "300")]
        Weight300,

        [Description(description: "400")]
        Weight400,

        [Description(description: "500")]
        Weight500,

        [Description(description: "600")]
        Weight600,

        [Description(description: "700")]
        Weight700,

        [Description(description: "800")]
        Weight800,

        [Description(description: "900")]
        Weight900
    }

    public static StyleFontWeight Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleFontWeight(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleFontWeight? result)
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

    public static implicit operator StyleFontWeight(string? value) => Parse(value: value);

    public static implicit operator string(StyleFontWeight? value) => value?.Value ?? string.Empty;
}
