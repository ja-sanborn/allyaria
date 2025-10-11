using Allyaria.Theming.Enumerations;

namespace Allyaria.Theming.Styles;

/// <summary>Represents the typography configuration for a component within the Allyaria theming system.</summary>
/// <remarks>
/// This struct currently exposes a single surface-level typography (<see cref="Surface" />). It is designed to be extended
/// with additional roles (e.g., heading, body, caption) as <see cref="ComponentType" /> evolves. The type is immutable
/// (readonly record struct) and supports non-destructive updates via <see cref="Cascade(AryTypography?)" />.
/// </remarks>
internal readonly record struct AryTypographyArea
{
    /// <summary>Initializes a new instance of the <see cref="AryTypographyArea" /> struct.</summary>
    /// <param name="surfaceTypography">
    /// Optional surface typography. When <see langword="null" />, a new <see cref="AryTypography" /> is created using
    /// <see cref="AryTypography.AllyariaTypography()" /> defaults.
    /// </param>
    public AryTypographyArea(AryTypography? surfaceTypography = null)
        => Surface = surfaceTypography ?? new AryTypography();

    /// <summary>Gets the typography applied to the component’s primary surface (e.g., general text content).</summary>
    internal AryTypography Surface { get; init; }

    /// <summary>
    /// Returns a new <see cref="AryTypographyArea" /> with optional overrides applied. Any parameter left
    /// <see langword="null" /> retains the current value.
    /// </summary>
    /// <param name="surfaceTypography">Optional override for <see cref="Surface" />.</param>
    /// <returns>A new <see cref="AryTypographyArea" /> with the specified overrides.</returns>
    public AryTypographyArea Cascade(AryTypography? surfaceTypography = null)
        => this with
        {
            Surface = surfaceTypography ?? Surface
        };

    /// <summary>Resolves the appropriate <see cref="AryTypography" /> for a given <see cref="ComponentType" />.</summary>
    /// <param name="type">The component type requesting typography.</param>
    /// <returns>
    /// The resolved <see cref="AryTypography" /> for the specified <paramref name="type" />. Currently returns
    /// <see cref="Surface" /> for all types.
    /// </returns>
    internal AryTypography ToTypography(ComponentType type)
        => type switch
        {
            _ => Surface
        };
}
