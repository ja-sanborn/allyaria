namespace Allyaria.Theming.Themes;

/// <summary>These are for static default constants for <see cref="ThemeGroupPalette" />.</summary>
public sealed partial record ThemeGroupPalette
{
    public static readonly ThemeGroupPalette DarkPrimary = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.Dark, paletteType: PaletteType.Primary
    );

    public static readonly ThemeGroupPalette DarkSecondary = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.Dark, paletteType: PaletteType.Secondary
    );

    public static readonly ThemeGroupPalette DarkSurface = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.Dark, paletteType: PaletteType.Surface
    );

    public static readonly ThemeGroupPalette DarkSurfaceVariant = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.Dark, paletteType: PaletteType.SurfaceVariant
    );

    public static readonly ThemeGroupPalette DarkTertiary = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.Dark, paletteType: PaletteType.Tertiary
    );

    public static readonly ThemeGroupPalette Empty = new();

    public static readonly ThemeGroupPalette HighContrastDark = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.HighContrastDark, paletteType: PaletteType.Surface
    );

    public static readonly ThemeGroupPalette HighContrastLight = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.HighContrastLight, paletteType: PaletteType.Surface
    );

    public static readonly ThemeGroupPalette LightPrimary = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.Light, paletteType: PaletteType.Primary
    );

    public static readonly ThemeGroupPalette LightSecondary = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.Light, paletteType: PaletteType.Secondary
    );

    public static readonly ThemeGroupPalette LightSurface = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.Light, paletteType: PaletteType.Surface
    );

    public static readonly ThemeGroupPalette LightSurfaceVariant = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.Light, paletteType: PaletteType.SurfaceVariant
    );

    public static readonly ThemeGroupPalette LightTertiary = FromColorPalette(
        colorPalette: new ColorPalette(), themeType: ThemeType.Light, paletteType: PaletteType.Tertiary
    );
}
