namespace Allyaria.Theming.BrandTypes;

public readonly record struct BrandTheme
{
    public BrandTheme(HexColor? surface = null,
        HexColor? primary = null,
        HexColor? secondary = null,
        HexColor? tertiary = null,
        HexColor? error = null,
        HexColor? warning = null,
        HexColor? success = null,
        HexColor? info = null)
    {
        Elevation1 = new BrandState(color: (surface ?? StyleDefaults.SurfaceColorLight).ToElevation1());
        Elevation2 = new BrandState(color: (surface ?? StyleDefaults.SurfaceColorLight).ToElevation2());
        Elevation3 = new BrandState(color: (surface ?? StyleDefaults.SurfaceColorLight).ToElevation3());
        Elevation4 = new BrandState(color: (surface ?? StyleDefaults.SurfaceColorLight).ToElevation4());
        Elevation5 = new BrandState(color: (surface ?? StyleDefaults.SurfaceColorLight).ToElevation5());
        Error = new BrandState(color: error ?? StyleDefaults.ErrorColorLight);
        Info = new BrandState(color: info ?? StyleDefaults.InfoColorLight);
        Primary = new BrandState(color: primary ?? StyleDefaults.PrimaryColorLight);
        Success = new BrandState(color: success ?? StyleDefaults.SuccessColorLight);
        Secondary = new BrandState(color: secondary ?? StyleDefaults.SecondaryColorLight);
        Surface = new BrandState(color: surface ?? StyleDefaults.SurfaceColorLight);
        Tertiary = new BrandState(color: tertiary ?? StyleDefaults.TertiaryColorLight);
        Warning = new BrandState(color: warning ?? StyleDefaults.WarningColorLight);
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

    public BrandState Tertiary { get; }

    public BrandState Warning { get; }
}
