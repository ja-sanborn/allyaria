namespace Allyaria.Theming.Types;

public readonly record struct BrandPalette
{
    public BrandPalette(HexColor backgroundColor)
    {
        BackgroundColor = backgroundColor;
        ForegroundColor = BackgroundColor.ToForeground().EnsureContrast(background: BackgroundColor);
        CaretColor = ForegroundColor;
        AccentColor = ForegroundColor.ToAccent().EnsureContrast(background: BackgroundColor);
        BorderColor = BackgroundColor.ToAccent();
        OutlineColor = AccentColor;
        TextDecorationColor = AccentColor;
    }

    public HexColor AccentColor { get; }

    public HexColor BackgroundColor { get; }

    public HexColor BorderColor { get; }

    public HexColor CaretColor { get; }

    public HexColor ForegroundColor { get; }

    public HexColor OutlineColor { get; }

    public HexColor TextDecorationColor { get; }
}
