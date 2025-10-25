namespace Allyaria.Theming.Themes;

/// <summary>These are for static default constants for <see cref="ThemeGroupTypography" />.</summary>
public sealed partial record ThemeGroupTypography
{
    public static readonly ThemeGroupTypography Empty = new();

    public static readonly ThemeGroupTypography Monospace = FromFontDefinition(
        fontDefinition: new FontDefinition(), themeType: ThemeType.Light, fontType: FontType.Tertiary
    );

    public static readonly ThemeGroupTypography SansSerif = FromFontDefinition(
        fontDefinition: new FontDefinition(), themeType: ThemeType.Light, fontType: FontType.Primary
    );

    public static readonly ThemeGroupTypography Serif = FromFontDefinition(
        fontDefinition: new FontDefinition(), themeType: ThemeType.Light, fontType: FontType.Secondary
    );
}
