namespace Allyaria.Theming.Constants.Theming;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class ThemePalette
{
    public static readonly ThemeGroupPalette DarkPrimary = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.Dark, paletteType: PaletteType.Primary
    );

    public static readonly ThemeGroupPalette DarkSecondary = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.Dark, paletteType: PaletteType.Secondary
    );

    public static readonly ThemeGroupPalette DarkSurface = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.Dark, paletteType: PaletteType.Surface
    );

    public static readonly ThemeGroupPalette DarkSurfaceVariant = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.Dark, paletteType: PaletteType.SurfaceVariant
    );

    public static readonly ThemeGroupPalette DarkTertiary = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.Dark, paletteType: PaletteType.Tertiary
    );

    public static readonly ThemeGroupPalette HighContrastDark = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.HighContrastDark, paletteType: PaletteType.Surface
    );

    public static readonly ThemeGroupPalette HighContrastLight = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.HighContrastLight, paletteType: PaletteType.Surface
    );

    public static readonly ThemeGroupPalette LightPrimary = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.Light, paletteType: PaletteType.Primary
    );

    public static readonly ThemeGroupPalette LightSecondary = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.Light, paletteType: PaletteType.Secondary
    );

    public static readonly ThemeGroupPalette LightSurface = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.Light, paletteType: PaletteType.Surface
    );

    public static readonly ThemeGroupPalette LightSurfaceVariant = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.Light, paletteType: PaletteType.SurfaceVariant
    );

    public static readonly ThemeGroupPalette LightTertiary = FromColorPalette(
        colorPalettes: new ColorPalettes(), themeType: ThemeType.Light, paletteType: PaletteType.Tertiary
    );
}
