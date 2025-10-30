namespace Allyaria.Theming.Types;

public readonly record struct BrandPalette
{
    public BrandPalette(HexColor backgroundColor)
    {
        BackgroundColor = backgroundColor;
        ForegroundColor = BackgroundColor.ToForeground().EnsureContrast(background: BackgroundColor);
        CaretColor = ForegroundColor;
        AccentColor = ForegroundColor.ToAccent().EnsureContrast(background: BackgroundColor);
        BorderColor = BackgroundColor;
        OutlineColor = AccentColor;
        TextDecorationColor = AccentColor;
    }

    public BrandPalette(bool isHighContrastDark)
    {
        if (isHighContrastDark)
        {
            BackgroundColor = StyleDefaults.BackgroundColorHighContrastDark;
            ForegroundColor = StyleDefaults.ForegroundColorHighContrastDark;
            CaretColor = ForegroundColor;
            AccentColor = StyleDefaults.AccentColorHighContrastDark;
            BorderColor = AccentColor;
            OutlineColor = AccentColor;
            TextDecorationColor = ForegroundColor;
        }
        else
        {
            BackgroundColor = StyleDefaults.BackgroundColorHighContrastLight;
            ForegroundColor = StyleDefaults.ForegroundColorHighContrastLight;
            CaretColor = ForegroundColor;
            AccentColor = StyleDefaults.AccentColorHighContrastLight;
            BorderColor = AccentColor;
            OutlineColor = AccentColor;
            TextDecorationColor = ForegroundColor;
        }
    }

    public HexColor AccentColor { get; }

    public HexColor BackgroundColor { get; }

    public HexColor BorderColor { get; }

    public HexColor CaretColor { get; }

    public HexColor ForegroundColor { get; }

    public HexColor OutlineColor { get; }

    public HexColor TextDecorationColor { get; }
}
