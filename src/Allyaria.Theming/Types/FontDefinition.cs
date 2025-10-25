namespace Allyaria.Theming.Types;

public readonly record struct FontDefinition
{
    public FontDefinition(string? primaryFamily = null,
        string? secondaryFamily = null,
        string? tertiaryFamily = null)
    {
        PrimaryFamily = string.IsNullOrWhiteSpace(primaryFamily)
            ? CssFontFamily.SansSerif.Value
            : primaryFamily.Trim();

        SecondaryFamily = string.IsNullOrWhiteSpace(secondaryFamily)
            ? CssFontFamily.Serif.Value
            : secondaryFamily.Trim();

        TertiaryFamily = string.IsNullOrWhiteSpace(tertiaryFamily)
            ? CssFontFamily.Monospace.Value
            : tertiaryFamily.Trim();
    }

    public string PrimaryFamily { get; init; }

    public string SecondaryFamily { get; init; }

    public string TertiaryFamily { get; init; }

    public StyleValueString GetFontFamily(ThemeType themeType, FontType fontType)
    {
        var fontFamily = fontType switch
        {
            FontType.Primary => PrimaryFamily,
            FontType.Secondary => SecondaryFamily,
            FontType.Tertiary => TertiaryFamily,
            _ => CssFontFamily.SansSerif.Value
        };

        if (themeType is ThemeType.HighContrastDark or ThemeType.HighContrastLight)
        {
            fontFamily = fontType switch
            {
                FontType.Primary => CssFontFamily.SansSerif.Value,
                FontType.Secondary => CssFontFamily.Serif.Value,
                FontType.Tertiary => CssFontFamily.Monospace.Value,
                _ => CssFontFamily.SansSerif.Value
            };
        }

        return new StyleValueString(fontFamily);
    }
}
