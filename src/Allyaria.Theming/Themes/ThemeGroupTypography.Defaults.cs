namespace Allyaria.Theming.Themes;

/// <summary>These are for static default constants for <see cref="ThemeGroupTypography" />.</summary>
public sealed partial record ThemeGroupTypography
{
    public static readonly ThemeGroupTypography Empty = new();

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
