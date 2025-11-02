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
        Elevation1 = new BrandState(color: (surface ?? StyleDefaults.HighContrastSurfaceColorLight).ToElevation1());
        Elevation2 = new BrandState(color: (surface ?? StyleDefaults.HighContrastSurfaceColorLight).ToElevation2());
        Elevation3 = new BrandState(color: (surface ?? StyleDefaults.HighContrastSurfaceColorLight).ToElevation3());
        Elevation4 = new BrandState(color: (surface ?? StyleDefaults.HighContrastSurfaceColorLight).ToElevation4());
        Elevation5 = new BrandState(color: (surface ?? StyleDefaults.HighContrastSurfaceColorLight).ToElevation5());
        Error = new BrandState(color: error ?? StyleDefaults.HighContrastErrorColorLight);
        Info = new BrandState(color: info ?? StyleDefaults.HighContrastInfoColorLight);
        Primary = new BrandState(color: primary ?? StyleDefaults.HighContrastPrimaryColorLight);
        Success = new BrandState(color: success ?? StyleDefaults.HighContrastSuccessColorLight);
        Secondary = new BrandState(color: secondary ?? StyleDefaults.HighContrastSecondaryColorLight);
        Surface = new BrandState(color: surface ?? StyleDefaults.HighContrastSurfaceColorLight);
        SurfaceVariant = new BrandState(color: surfaceVariant ?? StyleDefaults.HighContrastSurfaceVariantColorLight);
        Tertiary = new BrandState(color: tertiary ?? StyleDefaults.HighContrastTertiaryColorLight);
        Warning = new BrandState(color: warning ?? StyleDefaults.HighContrastWarningColorLight);
    }

    public BrandState Elevation1 { get; }

    public BrandState Elevation2 { get; }

    public BrandState Elevation3 { get; }

    public BrandState Elevation4 { get; }

    public BrandState Elevation5 { get; }

    public BrandState Error { get; }

    public BrandState Info { get; }

    public BrandState Primary { get; }

    public BrandState Secondary { get; }

    public BrandState Success { get; }

    public BrandState Surface { get; }

    public BrandState SurfaceVariant { get; }

    public BrandState Tertiary { get; }

    public BrandState Warning { get; }
}
