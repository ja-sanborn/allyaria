namespace Allyaria.Theming.Types.Css;

public sealed record CssOutlineStyle : StyleValueBase
{
    public CssOutlineStyle(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "auto")]
        Auto,

        [Description(description: "dashed")]
        Dashed,

        [Description(description: "dotted")]
        Dotted,

        [Description(description: "double")]
        Double,

        [Description(description: "groove")]
        Groove,

        [Description(description: "inset")]
        Inset,

        [Description(description: "none")]
        None,

        [Description(description: "outset")]
        Outset,

        [Description(description: "ridge")]
        Ridge,

        [Description(description: "solid")]
        Solid
    }

    public static CssOutlineStyle Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new CssOutlineStyle(kind: kind)
            : throw new AryArgumentException(message: $"Invalid outline style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out CssOutlineStyle? result)
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

    public static implicit operator CssOutlineStyle(string? value) => Parse(value: value);

    public static implicit operator string(CssOutlineStyle? value) => value?.Value ?? string.Empty;
}
