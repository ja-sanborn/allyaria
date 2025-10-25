namespace Allyaria.Theming.Themes;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class ThemeTypography
{
    public static readonly ThemeGroupTypography Monospace = FromFontDefinition(
        fontDefinitions: new FontDefinitions(), themeType: ThemeType.Light, fontType: FontType.Monospace
    );

    public static readonly ThemeGroupTypography SansSerif = FromFontDefinition(
        fontDefinitions: new FontDefinitions(), themeType: ThemeType.Light, fontType: FontType.SansSerif
    );

    public static readonly ThemeGroupTypography Serif = FromFontDefinition(
        fontDefinitions: new FontDefinitions(), themeType: ThemeType.Light, fontType: FontType.Serif
    );
}
