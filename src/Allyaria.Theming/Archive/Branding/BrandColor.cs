namespace Allyaria.Theming.Branding;

public readonly record struct BrandColor
{
    public BrandColor(StyleValueColor backgroundColor)
    {
        BackgroundColor = backgroundColor;
        ForegroundColor = BackgroundColor.ToForeground().EnsureContrast(surface: BackgroundColor);
        CaretColor = ForegroundColor;
        AccentColor = ForegroundColor.ToAccent().EnsureContrast(surface: BackgroundColor);
        BorderColor = new StyleGroupBorderColor(value: BackgroundColor);
        OutlineColor = AccentColor;
        TextDecorationColor = AccentColor;
    }

    public BrandColor(bool isHighContrastDark)
    {
        if (isHighContrastDark)
        {
            BackgroundColor = CssColors.BackgroundColorHighContrastDark;
            ForegroundColor = CssColors.ForegroundColorHighContrastDark;
            CaretColor = ForegroundColor;
            AccentColor = CssColors.AccentColorHighContrastDark;
            BorderColor = new StyleGroupBorderColor(value: AccentColor);
            OutlineColor = AccentColor;
            TextDecorationColor = ForegroundColor;
        }
        else
        {
            BackgroundColor = CssColors.BackgroundColorHighContrastLight;
            ForegroundColor = CssColors.ForegroundColorHighContrastLight;
            CaretColor = ForegroundColor;
            AccentColor = CssColors.AccentColorHighContrastLight;
            BorderColor = new StyleGroupBorderColor(value: AccentColor);
            OutlineColor = AccentColor;
            TextDecorationColor = ForegroundColor;
        }
    }

    public StyleValueColor AccentColor { get; }

    public StyleValueColor BackgroundColor { get; }

    public StyleGroupBorderColor BorderColor { get; }

    public StyleValueColor CaretColor { get; }

    public StyleValueColor ForegroundColor { get; }

    public StyleValueColor OutlineColor { get; }

    public StyleValueColor TextDecorationColor { get; }

    public ThemeGroupPalette GetPalette(ThemeType themeType, PaletteType paletteType)
        => new(
            accentColor: AccentColor,
            backgroundColor: BackgroundColor,
            borderColor: BorderColor,
            caretColor: CaretColor,
            color: ForegroundColor,
            outlineColor: OutlineColor,
            textDecorationColor: TextDecorationColor
        );
}
