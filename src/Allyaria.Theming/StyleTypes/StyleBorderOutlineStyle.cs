namespace Allyaria.Theming.StyleTypes;

public sealed record StyleBorderOutlineStyle : StyleValueBase
{
    public StyleBorderOutlineStyle(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
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

    public static StyleBorderOutlineStyle Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleBorderOutlineStyle(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    public static bool TryParse(string? value, out StyleBorderOutlineStyle? result)
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

    public static implicit operator StyleBorderOutlineStyle(string? value) => Parse(value: value);

    public static implicit operator string(StyleBorderOutlineStyle? value) => value?.Value ?? string.Empty;
}
