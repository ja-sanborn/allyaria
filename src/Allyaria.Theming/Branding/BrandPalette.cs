namespace Allyaria.Theming.Branding;

public readonly record struct BrandPalette
{
    public BrandPalette(HexColor? surface = null,
        HexColor? surfaceVariant = null,
        HexColor? primary = null,
        HexColor? secondary = null,
        HexColor? tertiary = null,
        HexColor? error = null,
        HexColor? warning = null,
        HexColor? success = null,
        HexColor? info = null)
    {
        Error = new BrandStyle(color: error ?? CssColors.ErrorColorLight.Color);
        Info = new BrandStyle(color: info ?? CssColors.InfoColorLight.Color);
        Primary = new BrandStyle(color: primary ?? CssColors.PrimaryColorLight.Color);
        Success = new BrandStyle(color: success ?? CssColors.SuccessColorLight.Color);
        Secondary = new BrandStyle(color: secondary ?? CssColors.SecondaryColorLight.Color);
        Surface = new BrandStyle(color: surface ?? CssColors.SurfaceColorLight.Color);
        SurfaceVariant = new BrandStyle(color: surfaceVariant ?? CssColors.SurfaceVariantColorLight.Color);
        Tertiary = new BrandStyle(color: tertiary ?? CssColors.TertiaryColorLight.Color);
        Warning = new BrandStyle(color: warning ?? CssColors.WarningColorLight.Color);
    }

    public BrandPalette(bool isHighContrastDark)
    {
        Error = new BrandStyle(isHighContrastDark: isHighContrastDark);
        Info = new BrandStyle(isHighContrastDark: isHighContrastDark);
        Primary = new BrandStyle(isHighContrastDark: isHighContrastDark);
        Secondary = new BrandStyle(isHighContrastDark: isHighContrastDark);
        Success = new BrandStyle(isHighContrastDark: isHighContrastDark);
        Surface = new BrandStyle(isHighContrastDark: isHighContrastDark);
        SurfaceVariant = new BrandStyle(isHighContrastDark: isHighContrastDark);
        Tertiary = new BrandStyle(isHighContrastDark: isHighContrastDark);
        Warning = new BrandStyle(isHighContrastDark: isHighContrastDark);
    }

    public BrandStyle Error { get; }

    public BrandStyle Info { get; }

    public BrandStyle Primary { get; }

    public BrandStyle Secondary { get; }

    public BrandStyle Success { get; }

    public BrandStyle Surface { get; }

    public BrandStyle SurfaceVariant { get; }

    public BrandStyle Tertiary { get; }

    public BrandStyle Warning { get; }

    public ThemeGroupPalette GetPalette(ThemeType themeType, PaletteType paletteType, ComponentState state)
        => paletteType switch
        {
            PaletteType.Error => Error.GetPalette(themeType: themeType, paletteType: paletteType, state: state),
            PaletteType.Info => Info.GetPalette(themeType: themeType, paletteType: paletteType, state: state),
            PaletteType.Primary => Primary.GetPalette(themeType: themeType, paletteType: paletteType, state: state),
            PaletteType.Secondary => Secondary.GetPalette(themeType: themeType, paletteType: paletteType, state: state),
            PaletteType.Success => Success.GetPalette(themeType: themeType, paletteType: paletteType, state: state),
            PaletteType.Surface => Surface.GetPalette(themeType: themeType, paletteType: paletteType, state: state),
            PaletteType.SurfaceVariant => SurfaceVariant.GetPalette(
                themeType: themeType, paletteType: paletteType, state: state
            ),
            PaletteType.Tertiary => Tertiary.GetPalette(themeType: themeType, paletteType: paletteType, state: state),
            PaletteType.Warning => Warning.GetPalette(themeType: themeType, paletteType: paletteType, state: state),
            _ => Surface.GetPalette(themeType: themeType, paletteType: paletteType, state: state)
        };
}
