namespace Allyaria.Theming.Types;

public readonly struct PaletteColor
{
    public PaletteColor(HexColor? lightSurface = null,
        HexColor? lightSurfaceVariant = null,
        HexColor? lightPrimary = null,
        HexColor? lightSecondary = null,
        HexColor? lightTertiary = null,
        HexColor? darkSurface = null,
        HexColor? darkSurfaceVariant = null,
        HexColor? darkPrimary = null,
        HexColor? darkSecondary = null,
        HexColor? darkTertiary = null)
    {
        DarkPrimary = darkPrimary ?? ThemingDefaults.PrimaryColorDark;
        DarkSecondary = darkSecondary ?? ThemingDefaults.SecondaryColorDark;
        DarkSurface = darkSurface ?? ThemingDefaults.SurfaceColorDark;
        DarkSurfaceVariant = darkSurfaceVariant ?? ThemingDefaults.SurfaceVariantColorDark;
        DarkTertiary = darkTertiary ?? ThemingDefaults.TertiaryColorDark;
        LightPrimary = lightPrimary ?? ThemingDefaults.PrimaryColorLight;
        LightSecondary = lightSecondary ?? ThemingDefaults.SecondaryColorLight;
        LightSurface = lightSurface ?? ThemingDefaults.SurfaceColorLight;
        LightSurfaceVariant = lightSurfaceVariant ?? ThemingDefaults.SurfaceVariantColorLight;
        LightTertiary = lightTertiary ?? ThemingDefaults.TertiaryColorLight;
    }

    public HexColor DarkPrimary { get; }

    public HexColor DarkSecondary { get; }

    public HexColor DarkSurface { get; }

    public HexColor DarkSurfaceVariant { get; }

    public HexColor DarkTertiary { get; }

    public HexColor LightPrimary { get; }

    public HexColor LightSecondary { get; }

    public HexColor LightSurface { get; }

    public HexColor LightSurfaceVariant { get; }

    public HexColor LightTertiary { get; }

    public StyleValueColor GetColor(ThemeType themeType, PaletteType paletteType)
    {
        StyleValueColor paletteColor;

        switch (themeType)
        {
            case ThemeType.HighContrastDark:
                return ThemingDefaults.BackgroundColorHighContrastDark;

            case ThemeType.HighContrastLight:
                return ThemingDefaults.BackgroundColorHighContrastLight;

            case ThemeType.Dark:
                switch (paletteType)
                {
                    case PaletteType.Primary:
                        paletteColor = new StyleValueColor(DarkPrimary);

                        break;

                    case PaletteType.Secondary:
                        paletteColor = new StyleValueColor(DarkSecondary);

                        break;

                    case PaletteType.SurfaceVariant:
                        paletteColor = new StyleValueColor(DarkSurfaceVariant);

                        break;

                    case PaletteType.Tertiary:
                        paletteColor = new StyleValueColor(DarkTertiary);

                        break;
                    default:
                        paletteColor = new StyleValueColor(DarkSurface);

                        break;
                }

                return paletteColor.Color.IsTransparent()
                    ? new StyleValueColor(DarkSurface)
                    : paletteColor;

            default:
                switch (paletteType)
                {
                    case PaletteType.Primary:
                        paletteColor = new StyleValueColor(LightPrimary);

                        break;

                    case PaletteType.Secondary:
                        paletteColor = new StyleValueColor(LightSecondary);

                        break;

                    case PaletteType.SurfaceVariant:
                        paletteColor = new StyleValueColor(LightSurfaceVariant);

                        break;

                    case PaletteType.Tertiary:
                        paletteColor = new StyleValueColor(LightTertiary);

                        break;
                    default:
                        paletteColor = new StyleValueColor(LightSurface);

                        break;
                }

                return paletteColor.Color.IsTransparent()
                    ? new StyleValueColor(LightSurface)
                    : paletteColor;
        }
    }
}
