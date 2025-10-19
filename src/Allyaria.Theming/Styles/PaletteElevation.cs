namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a hierarchy of <see cref="PaletteState" /> values corresponding to distinct elevation levels (Lowest →
/// Highest) within the Allyaria theming system.
/// </summary>
/// <remarks>
/// Each elevation level encapsulates a complete set of component states (Default, Hovered, Focused, etc.) derived from the
/// baseline <see cref="Palette" /> using elevation-specific transformations.
/// </remarks>
internal readonly record struct PaletteElevation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaletteElevation" /> struct using the provided base palette.
    /// </summary>
    /// <param name="palette">
    /// The base <see cref="Palette" /> from which all elevation layers (Lowest through Highest) are derived.
    /// </param>
    /// <remarks>
    /// Each layer is constructed by applying successive elevation transformations (<c>ToElevation1()</c>–<c>ToElevation4()</c>
    /// ) to the baseline palette, then wrapping the result into <see cref="PaletteState" /> objects for consistent state
    /// handling.
    /// </remarks>
    public PaletteElevation(Palette palette)
    {
        Lowest = new PaletteState(palette);
        Low = new PaletteState(palette.ToElevation1());
        Mid = new PaletteState(palette.ToElevation2());
        High = new PaletteState(palette.ToElevation3());
        Highest = new PaletteState(palette.ToElevation4());
    }

    /// <summary>
    /// Gets the <see cref="PaletteState" /> representing the “high” elevation layer, typically used for elements above primary
    /// surfaces (e.g., popovers, dropdowns).
    /// </summary>
    public PaletteState High { get; }

    /// <summary>
    /// Gets the <see cref="PaletteState" /> representing the “highest” elevation layer, used for topmost components such as
    /// dialogs or modals.
    /// </summary>
    public PaletteState Highest { get; }

    /// <summary>
    /// Gets the <see cref="PaletteState" /> representing the “low” elevation layer, typically used for slightly raised or
    /// inset elements.
    /// </summary>
    public PaletteState Low { get; }

    /// <summary>
    /// Gets the <see cref="PaletteState" /> representing the “lowest” elevation layer, generally used for background or deeply
    /// recessed elements.
    /// </summary>
    public PaletteState Lowest { get; }

    /// <summary>
    /// Gets the <see cref="PaletteState" /> representing the “mid” (neutral) elevation layer, commonly used for primary
    /// surfaces and content regions.
    /// </summary>
    public PaletteState Mid { get; }

    /// <summary>Returns the <see cref="Palette" /> corresponding to the specified elevation and component state.</summary>
    /// <param name="elevation">The desired elevation level for the component.</param>
    /// <param name="state">The visual state of the component (e.g., Default, Hovered, Pressed).</param>
    /// <returns>
    /// The <see cref="Palette" /> instance appropriate for the given elevation and state. Returns the <see cref="Mid" />
    /// layer’s palette if the elevation is unrecognized.
    /// </returns>
    public Palette ToPalette(ComponentElevation elevation, ComponentState state)
        => elevation switch
        {
            ComponentElevation.Lowest => Lowest.ToPalette(state),
            ComponentElevation.Low => Low.ToPalette(state),
            ComponentElevation.High => High.ToPalette(state),
            ComponentElevation.Highest => Highest.ToPalette(state),
            _ => Mid.ToPalette(state)
        };
}
