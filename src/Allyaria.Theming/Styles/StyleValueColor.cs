namespace Allyaria.Theming.Styles;

public readonly record struct StyleValueColor : IStyleValue
{
    public StyleValueColor(string value) => Color = new HexColor(value);

    public StyleValueColor(HexColor color) => Color = color;

    public StyleValueColor(byte red, byte green, byte blue, double alpha = 1.0)
        => Color = new HexColor(new HexByte(red), new HexByte(green), new HexByte(blue), HexByte.FromNormalized(alpha));

    public HexColor Color { get; }

    public string Value => Color.ToString();

    public StyleValueColor EnsureContrast(StyleValueColor surface)
        => new(Color.EnsureMinimumContrast(surface.Color, 4.5));

    public static StyleValueColor Parse(string value) => new(value);

    public StyleValueColor ToAccent() => new(Color.ShiftLightness(0.6));

    public StyleValueColor ToDisabled() => new(Color.Desaturate(0.6));

    public StyleValueColor ToDragged() => new(Color.ShiftLightness(0.18));

    public StyleValueColor ToElevation1() => new(Color.ShiftLightness(0.04));

    public StyleValueColor ToElevation2() => new(Color.ShiftLightness(0.08));

    public StyleValueColor ToElevation3() => new(Color.ShiftLightness(0.12));

    public StyleValueColor ToElevation4() => new(Color.ShiftLightness(0.16));

    public StyleValueColor ToFocused() => new(Color.ShiftLightness(0.1));

    public StyleValueColor ToForeground() => new(Color.ShiftLightness(0.7));

    public StyleValueColor ToHovered() => new(Color.ShiftLightness(0.06));

    public StyleValueColor ToPressed() => new(Color.ShiftLightness(0.14));

    public StyleValueColor ToVisited() => new(Color.Desaturate(0.3));

    public static bool TryParse(string value, out StyleValueColor result)
    {
        try
        {
            result = new StyleValueColor(value);

            return true;
        }
        catch
        {
            result = default(StyleValueColor);

            return false;
        }
    }

    public static implicit operator StyleValueColor(HexColor value) => new(value);

    public static implicit operator HexColor(StyleValueColor theme) => theme.Color;

    public static implicit operator StyleValueColor(string value) => new(value);

    public static implicit operator string(StyleValueColor theme) => theme.Value;
}
