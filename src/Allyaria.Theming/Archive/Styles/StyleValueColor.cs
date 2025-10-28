namespace Allyaria.Theming.Styles;

public readonly record struct StyleValueColor : IStyleValue
{
    public StyleValueColor(string value) => Color = new HexColor(value: value);

    public StyleValueColor(HexColor color) => Color = color;

    public StyleValueColor(byte red, byte green, byte blue, double alpha = 1.0)
        => Color = new HexColor(
            red: new HexByte(value: red), green: new HexByte(value: green), blue: new HexByte(value: blue),
            alpha: HexByte.FromNormalized(value: alpha)
        );

    public HexColor Color { get; }

    public string Value => Color.ToString();

    public StyleValueColor EnsureContrast(StyleValueColor surface)
        => new(color: Color.EnsureMinimumContrast(surfaceColor: surface.Color, minimumRatio: 4.5));

    public static StyleValueColor Parse(string value) => new(value: value);

    public StyleValueColor ToAccent() => new(color: Color.ShiftLightness(delta: 0.6));

    public StyleValueColor ToDisabled() => new(color: Color.Desaturate(desaturateBy: 0.6));

    public StyleValueColor ToDragged() => new(color: Color.ShiftLightness(delta: 0.18));

    public StyleValueColor ToElevation1() => new(color: Color.ShiftLightness(delta: 0.04));

    public StyleValueColor ToElevation2() => new(color: Color.ShiftLightness(delta: 0.08));

    public StyleValueColor ToElevation3() => new(color: Color.ShiftLightness(delta: 0.12));

    public StyleValueColor ToElevation4() => new(color: Color.ShiftLightness(delta: 0.16));

    public StyleValueColor ToFocused() => new(color: Color.ShiftLightness(delta: 0.1));

    public StyleValueColor ToForeground() => new(color: Color.ShiftLightness(delta: 0.9));

    public StyleValueColor ToHovered() => new(color: Color.ShiftLightness(delta: 0.06));

    public StyleValueColor ToPressed() => new(color: Color.ShiftLightness(delta: 0.14));

    public StyleValueColor ToVisited() => new(color: Color.Desaturate(desaturateBy: 0.3));

    public static bool TryParse(string value, out StyleValueColor result)
    {
        try
        {
            result = new StyleValueColor(value: value);

            return true;
        }
        catch
        {
            result = default(StyleValueColor);

            return false;
        }
    }

    public static implicit operator StyleValueColor(HexColor value) => new(color: value);

    public static implicit operator HexColor(StyleValueColor theme) => theme.Color;

    public static implicit operator StyleValueColor(string value) => new(value: value);

    public static implicit operator string(StyleValueColor theme) => theme.Value;
}
