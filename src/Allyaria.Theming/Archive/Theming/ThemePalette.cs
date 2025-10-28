namespace Allyaria.Theming.Constants.Theming;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class ThemePalette
{
    public static readonly BrandTheme BrandTheme = new();

    public static readonly ThemeGroupPalette DarkPrimary = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.Dark,
        paletteType: PaletteType.Primary,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette DarkSecondary = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.Dark,
        paletteType: PaletteType.Secondary,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette DarkSurface = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.Dark,
        paletteType: PaletteType.Surface,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette DarkSurfaceVariant = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.Dark,
        paletteType: PaletteType.SurfaceVariant,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette DarkTertiary = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.Dark,
        paletteType: PaletteType.Tertiary,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette HighContrastDark = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.HighContrastDark,
        paletteType: PaletteType.Surface,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette HighContrastLight = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.HighContrastLight,
        paletteType: PaletteType.Surface,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette LightPrimary = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.Light,
        paletteType: PaletteType.Primary,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette LightSecondary = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.Light,
        paletteType: PaletteType.Secondary,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette LightSurface = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.Light,
        paletteType: PaletteType.Surface,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette LightSurfaceVariant = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.Light,
        paletteType: PaletteType.SurfaceVariant,
        state: ComponentState.Default
    );

    public static readonly ThemeGroupPalette LightTertiary = ThemeGroupPalette.FromBrand(
        brand: new BrandPalette(),
        themeType: ThemeType.Light,
        paletteType: PaletteType.Tertiary,
        state: ComponentState.Default
    );
}
