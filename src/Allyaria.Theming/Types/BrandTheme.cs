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
