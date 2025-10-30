namespace Allyaria.Theming.Types;

public readonly record struct BrandFont
{
    public BrandFont(string? sansSerif = null,
        string? serif = null,
        string? monospace = null)
    {
        var setMonospace = string.IsNullOrWhiteSpace(value: monospace)
            ? StyleDefaults.MonospaceFont
            : monospace.Trim();

        var setSansSerif = string.IsNullOrWhiteSpace(value: sansSerif)
            ? StyleDefaults.SansSerifFont
            : sansSerif.Trim();

        var setSerif = string.IsNullOrWhiteSpace(value: serif)
            ? StyleDefaults.SerifFont
            : serif.Trim();

        Monospace = setMonospace;
        SansSerif = setSansSerif;
        Serif = setSerif;
    }

    public string HighContrastMonospace => StyleDefaults.MonospaceFont;

    public string HighContrastSansSerif => StyleDefaults.SansSerifFont;

    public string HighContrastSerif => StyleDefaults.SerifFont;

    public string Monospace { get; }

    public string SansSerif { get; }

    public string Serif { get; }

    public string GetFont(ThemeType themeType, FontType fontType)
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
