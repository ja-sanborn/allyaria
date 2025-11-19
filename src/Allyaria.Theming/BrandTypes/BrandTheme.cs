namespace Allyaria.Theming.BrandTypes;

/// <summary>
/// Represents a comprehensive theme configuration for a brand, containing multiple color states across different tonal
/// elevations and semantic categories such as primary, secondary, error, success, and warning.
/// </summary>
public sealed record BrandTheme
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BrandTheme" /> struct using the provided color parameters.
    /// </summary>
    /// <param name="surface">
    /// The base surface <see cref="HexColor" /> for the theme. Defaults to <see cref="StyleDefaults.SurfaceColorLight" /> if
    /// null.
    /// </param>
    /// <param name="primary">
    /// The primary <see cref="HexColor" /> used for key actions or highlights. Defaults to
    /// <see cref="StyleDefaults.PrimaryColorLight" /> if null.
    /// </param>
    /// <param name="secondary">
    /// The secondary <see cref="HexColor" /> used for supporting accents. Defaults to
    /// <see cref="StyleDefaults.SecondaryColorLight" /> if null.
    /// </param>
    /// <param name="tertiary">
    /// The tertiary <see cref="HexColor" /> used for subtle UI accents. Defaults to
    /// <see cref="StyleDefaults.TertiaryColorLight" /> if null.
    /// </param>
    /// <param name="error">
    /// The error <see cref="HexColor" /> used for failure or validation states. Defaults to
    /// <see cref="StyleDefaults.ErrorColorLight" /> if null.
    /// </param>
    /// <param name="warning">
    /// The warning <see cref="HexColor" /> used for cautionary states. Defaults to
    /// <see cref="StyleDefaults.WarningColorLight" /> if null.
    /// </param>
    /// <param name="success">
    /// The success <see cref="HexColor" /> used for confirmation or positive states. Defaults to
    /// <see cref="StyleDefaults.SuccessColorLight" /> if null.
    /// </param>
    /// <param name="info">
    /// The info <see cref="HexColor" /> used for informational states. Defaults to <see cref="StyleDefaults.InfoColorLight" />
    /// if null.
    /// </param>
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

    /// <summary>Gets the elevation level 1 color state, used for the lowest raised surfaces.</summary>
    public BrandState Elevation1 { get; }

    /// <summary>Gets the elevation level 2 color state, used for slightly raised surfaces.</summary>
    public BrandState Elevation2 { get; }

    /// <summary>Gets the elevation level 3 color state, used for medium-raised UI regions.</summary>
    public BrandState Elevation3 { get; }

    /// <summary>Gets the elevation level 4 color state, representing higher-depth UI layers.</summary>
    public BrandState Elevation4 { get; }

    /// <summary>Gets the elevation level 5 color state, representing the highest raised surfaces.</summary>
    public BrandState Elevation5 { get; }

    /// <summary>Gets the color state representing error or critical feedback visuals.</summary>
    public BrandState Error { get; }

    /// <summary>Gets the color state representing informational messages or neutral alerts.</summary>
    public BrandState Info { get; }

    /// <summary>Gets the primary color state used for dominant elements such as buttons or links.</summary>
    public BrandState Primary { get; }

    /// <summary>Gets the secondary color state used for complementary or background highlights.</summary>
    public BrandState Secondary { get; }

    /// <summary>Gets the success color state used for positive confirmations.</summary>
    public BrandState Success { get; }

    /// <summary>Gets the surface color state used as the base layer for background regions.</summary>
    public BrandState Surface { get; }

    /// <summary>Gets the tertiary color state used for subtle or decorative accents.</summary>
    public BrandState Tertiary { get; }

    /// <summary>Gets the warning color state used for cautionary alerts or partial issues.</summary>
    public BrandState Warning { get; }
}
