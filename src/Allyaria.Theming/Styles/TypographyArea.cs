namespace Allyaria.Theming.Styles;

/// <summary>Represents the typography configuration for a component within the Allyaria theming system.</summary>
/// <remarks>
/// This struct currently exposes a single surface-level typography (<see cref="Surface" />). It is designed to be extended
/// with additional roles (e.g., heading, body, caption) as <see cref="ComponentType" /> evolves. The type is immutable
/// (readonly record struct) and supports non-destructive updates via <see cref="Cascade(Typography?)" />.
/// </remarks>
internal readonly record struct TypographyArea
{
    /// <summary>
    ///     <summary>Initializes a new instance of the <see cref="TypographyArea" /> struct.</summary>
    /// </summary>
    public TypographyArea()
        : this(null) { }

    /// <summary>Initializes a new instance of the <see cref="TypographyArea" /> struct.</summary>
    /// <param name="surfaceTypography">
    /// Optional surface typography. When <see langword="null" />, a new <see cref="Typography" /> is created using default
    /// values.
    /// </param>
    public TypographyArea(Typography? surfaceTypography = null) => Surface = surfaceTypography ?? new Typography();

    /// <summary>Gets the typography applied to the component’s primary surface (e.g., general text content).</summary>
    public Typography Surface { get; init; }

    /// <summary>
    /// Returns a new <see cref="TypographyArea" /> with optional overrides applied. Any parameter left <see langword="null" />
    /// retains the current theme.
    /// </summary>
    /// <param name="surfaceTypography">Optional override for <see cref="Surface" />.</param>
    /// <returns>A new <see cref="TypographyArea" /> with the specified overrides.</returns>
    public TypographyArea Cascade(Typography? surfaceTypography = null)
        => this with
        {
            Surface = surfaceTypography ?? Surface
        };

    /// <summary>Resolves the appropriate <see cref="Typography" /> for a given <see cref="ComponentType" />.</summary>
    /// <param name="type">The component type requesting typography.</param>
    /// <returns>
    /// The resolved <see cref="Typography" /> for the specified <paramref name="type" />. Currently returns
    /// <see cref="Surface" /> for all types.
    /// </returns>
    public Typography ToTypography(ComponentType type)
        => type switch
        {
            _ => Surface
        };
}
