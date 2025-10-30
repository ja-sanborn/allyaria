namespace Allyaria.Theming.Types;

public readonly record struct BrandTheme
{
    public BrandTheme(HexColor? surface = null,
        HexColor? surfaceVariant = null,
        HexColor? primary = null,
        HexColor? secondary = null,
        HexColor? tertiary = null,
        HexColor? error = null,
        HexColor? warning = null,
        HexColor? success = null,
        HexColor? info = null)
    {
        Error = new BrandState(color: error ?? StyleDefaults.ErrorColorLight);
        Info = new BrandState(color: info ?? StyleDefaults.InfoColorLight);
        Primary = new BrandState(color: primary ?? StyleDefaults.PrimaryColorLight);
        Success = new BrandState(color: success ?? StyleDefaults.SuccessColorLight);
        Secondary = new BrandState(color: secondary ?? StyleDefaults.SecondaryColorLight);
        Surface = new BrandState(color: surface ?? StyleDefaults.SurfaceColorLight);
        SurfaceVariant = new BrandState(color: surfaceVariant ?? StyleDefaults.SurfaceVariantColorLight);
        Tertiary = new BrandState(color: tertiary ?? StyleDefaults.TertiaryColorLight);
        Warning = new BrandState(color: warning ?? StyleDefaults.WarningColorLight);
    }

    public BrandTheme(bool isHighContrastDark)
    {
        Error = new BrandState(isHighContrastDark: isHighContrastDark);
        Info = new BrandState(isHighContrastDark: isHighContrastDark);
        Primary = new BrandState(isHighContrastDark: isHighContrastDark);
        Secondary = new BrandState(isHighContrastDark: isHighContrastDark);
        Success = new BrandState(isHighContrastDark: isHighContrastDark);
        Surface = new BrandState(isHighContrastDark: isHighContrastDark);
        SurfaceVariant = new BrandState(isHighContrastDark: isHighContrastDark);
        Tertiary = new BrandState(isHighContrastDark: isHighContrastDark);
        Warning = new BrandState(isHighContrastDark: isHighContrastDark);
    }

    public BrandState Error { get; }

    public BrandState Info { get; }

    public BrandState Primary { get; }

    public BrandState Secondary { get; }

    public BrandState Success { get; }

    public BrandState Surface { get; }

    public BrandState SurfaceVariant { get; }

    public BrandState Tertiary { get; }

    public BrandState Warning { get; }

    public BrandPalette GetPalette(PaletteType paletteType, ComponentState state)
        => paletteType switch
        {
            PaletteType.Error => Error.GetPalette(state: state),
            PaletteType.Info => Info.GetPalette(state: state),
            PaletteType.Primary => Primary.GetPalette(state: state),
            PaletteType.Secondary => Secondary.GetPalette(state: state),
            PaletteType.Success => Success.GetPalette(state: state),
            PaletteType.Surface => Surface.GetPalette(state: state),
            PaletteType.SurfaceVariant => SurfaceVariant.GetPalette(state: state),
            PaletteType.Tertiary => Tertiary.GetPalette(state: state),
            PaletteType.Warning => Warning.GetPalette(state: state),
            _ => Surface.GetPalette(state: state)
        };
}
