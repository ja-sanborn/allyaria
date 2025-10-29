namespace Allyaria.Theming.Types;

public sealed record StyleColor : StyleValueBase
{
    public StyleColor(string value)
        : this(color: new HexColor(value: value)) { }

    public StyleColor(HexColor color)
        : base(value: color.ToString())
        => Color = color;

    public HexColor Color { get; }

    public static StyleColor Parse(string? value) => new(value: value ?? string.Empty);

    public static bool TryParse(string? value, out StyleColor? result)
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

    public static implicit operator StyleColor(string? value) => Parse(value: value);

    public static implicit operator string(StyleColor? value) => value?.Value ?? string.Empty;
}
