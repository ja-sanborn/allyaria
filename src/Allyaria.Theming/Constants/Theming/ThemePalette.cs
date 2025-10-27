namespace Allyaria.Theming.Constants.Theming;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class ThemePalette
{
    public static readonly BrandTheme BrandTheme = new();

    public static readonly ThemeGroupPalette DarkPrimary = BrandTheme.GetPalette(
        themeType: ThemeType.Dark,
        paletteType: PaletteType.Primary,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette DarkSecondary = BrandTheme.GetPalette(
        themeType: ThemeType.Dark,
        paletteType: PaletteType.Secondary,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette DarkSurface = BrandTheme.GetPalette(
        themeType: ThemeType.Dark,
        paletteType: PaletteType.Surface,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette DarkSurfaceVariant = BrandTheme.GetPalette(
        themeType: ThemeType.Dark,
        paletteType: PaletteType.SurfaceVariant,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette DarkTertiary = BrandTheme.GetPalette(
        themeType: ThemeType.Dark,
        paletteType: PaletteType.Tertiary,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette HighContrastDark = BrandTheme.GetPalette(
        themeType: ThemeType.HighContrastDark,
        paletteType: PaletteType.Surface,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette HighContrastLight = BrandTheme.GetPalette(
        themeType: ThemeType.HighContrastLight,
        paletteType: PaletteType.Surface,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette LightPrimary = BrandTheme.GetPalette(
        themeType: ThemeType.Light,
        paletteType: PaletteType.Primary,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette LightSecondary = BrandTheme.GetPalette(
        themeType: ThemeType.Light,
        paletteType: PaletteType.Secondary,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette LightSurface = BrandTheme.GetPalette(
        themeType: ThemeType.Light,
        paletteType: PaletteType.Surface,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette LightSurfaceVariant = BrandTheme.GetPalette(
        themeType: ThemeType.Light,
        paletteType: PaletteType.SurfaceVariant,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette LightTertiary = BrandTheme.GetPalette(
        themeType: ThemeType.Light,
        paletteType: PaletteType.Tertiary,
        state: ComponentState.Default
    );
}
