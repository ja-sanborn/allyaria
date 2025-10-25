namespace Allyaria.Theming.Branding;

public readonly record struct BrandFont
{
    public BrandFont(string? sansSerif = null,
        string? serif = null,
        string? monospace = null)
    {
        var setMonospace = string.IsNullOrWhiteSpace(value: monospace)
            ? CssFontFamily.Monospace.Value
            : monospace.Trim();

        var setSansSerif = string.IsNullOrWhiteSpace(value: sansSerif)
            ? CssFontFamily.SansSerif.Value
            : sansSerif.Trim();

        var setSerif = string.IsNullOrWhiteSpace(value: serif)
            ? CssFontFamily.Serif.Value
            : serif.Trim();

        Monospace = new StyleValueString(value: setMonospace);
        SansSerif = new StyleValueString(value: setSansSerif);
        Serif = new StyleValueString(value: setSerif);
    }

    public StyleValueString HighContrastMonospace => CssFontFamily.Monospace;

    public StyleValueString HighContrastSansSerif => CssFontFamily.SansSerif;

    public StyleValueString HighContrastSerif => CssFontFamily.Serif;

    public StyleValueString Monospace { get; }

    public StyleValueString SansSerif { get; }

    public StyleValueString Serif { get; }

    public StyleValueString GetFont(ThemeType themeType, FontType fontType)
        => themeType is ThemeType.HighContrastDark or ThemeType.HighContrastLight
            ? fontType switch
            {
                FontType.Monospace => HighContrastMonospace,
                FontType.SansSerif => HighContrastSansSerif,
                FontType.Serif => HighContrastSerif,
                _ => HighContrastSansSerif
            }
            : fontType switch
            {
                FontType.Monospace => Monospace,
                FontType.SansSerif => SansSerif,
                FontType.Serif => Serif,
                _ => SansSerif
            };
}
