using Allyaria.Theming.Enumerations;

namespace Allyaria.Theming.Styles;

/// <summary>Represents the typography configuration for a component within the Allyaria theming system.</summary>
/// <remarks>
/// This struct currently exposes a single surface-level typography (<see cref="Surface" />). It is designed to be extended
/// with additional roles (e.g., heading, body, caption) as <see cref="ComponentType" /> evolves. The type is immutable
/// (readonly record struct) and supports non-destructive updates via <see cref="Cascade(AllyariaTypography?)" />.
/// </remarks>
internal readonly record struct AllyariaTypographyComponent
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaTypographyComponent" /> struct.</summary>
    /// <param name="surfaceTypography">
    /// Optional surface typography. When <see langword="null" />, a new <see cref="AllyariaTypography" /> is created using
    /// <see cref="AllyariaTypography.AllyariaTypography()" /> defaults.
    /// </param>
    public AllyariaTypographyComponent(AllyariaTypography? surfaceTypography = null)
        => Surface = surfaceTypography ?? new AllyariaTypography();

    /// <summary>Gets the typography applied to the component’s primary surface (e.g., general text content).</summary>
    internal AllyariaTypography Surface { get; init; }

    /// <summary>
    /// Returns a new <see cref="AllyariaTypographyComponent" /> with optional overrides applied. Any parameter left
    /// <see langword="null" /> retains the current value.
    /// </summary>
    /// <param name="surfaceTypography">Optional override for <see cref="Surface" />.</param>
    /// <returns>A new <see cref="AllyariaTypographyComponent" /> with the specified overrides.</returns>
    public AllyariaTypographyComponent Cascade(AllyariaTypography? surfaceTypography = null)
        => this with
        {
            Surface = surfaceTypography ?? Surface
        };

    /// <summary>Resolves the appropriate <see cref="AllyariaTypography" /> for a given <see cref="ComponentType" />.</summary>
    /// <param name="type">The component type requesting typography.</param>
    /// <returns>
    /// The resolved <see cref="AllyariaTypography" /> for the specified <paramref name="type" />. Currently returns
    /// <see cref="Surface" /> for all types.
    /// </returns>
    internal AllyariaTypography ToTypography(ComponentType type)
        => type switch
        {
            _ => Surface
        };
}
