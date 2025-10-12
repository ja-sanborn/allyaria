using Allyaria.Theming.Enumerations;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a hierarchy of <see cref="AryPaletteState" /> values corresponding to distinct elevation levels (Lowest →
/// Highest) within the Allyaria theming system.
/// </summary>
/// <remarks>
/// Each elevation level encapsulates a complete set of component states (Default, Hovered, Focused, etc.) derived from the
/// baseline <see cref="AryPalette" /> using elevation-specific transformations.
/// </remarks>
public readonly record struct AryPaletteElevation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AryPaletteElevation" /> struct using the provided base palette.
    /// </summary>
    /// <param name="palette">
    /// The base <see cref="AryPalette" /> from which all elevation layers (Lowest through Highest) are derived.
    /// </param>
    /// <remarks>
    /// Each layer is constructed by applying successive elevation transformations (<c>ToElevation1()</c>–<c>ToElevation4()</c>
    /// ) to the baseline palette, then wrapping the result into <see cref="AryPaletteState" /> objects for consistent state
    /// handling.
    /// </remarks>
    public AryPaletteElevation(AryPalette palette)
    {
        Lowest = new AryPaletteState(palette);
        Low = new AryPaletteState(palette.ToElevation1());
        Mid = new AryPaletteState(palette.ToElevation2());
        High = new AryPaletteState(palette.ToElevation3());
        Highest = new AryPaletteState(palette.ToElevation4());
    }

    /// <summary>
    /// Gets the <see cref="AryPaletteState" /> representing the “high” elevation layer, typically used for elements above
    /// primary surfaces (e.g., popovers, dropdowns).
    /// </summary>
    public AryPaletteState High { get; }

    /// <summary>
    /// Gets the <see cref="AryPaletteState" /> representing the “highest” elevation layer, used for topmost components such as
    /// dialogs or modals.
    /// </summary>
    public AryPaletteState Highest { get; }

    /// <summary>
    /// Gets the <see cref="AryPaletteState" /> representing the “low” elevation layer, typically used for slightly raised or
    /// inset elements.
    /// </summary>
    public AryPaletteState Low { get; }

    /// <summary>
    /// Gets the <see cref="AryPaletteState" /> representing the “lowest” elevation layer, generally used for background or
    /// deeply recessed elements.
    /// </summary>
    public AryPaletteState Lowest { get; }

    /// <summary>
    /// Gets the <see cref="AryPaletteState" /> representing the “mid” (neutral) elevation layer, commonly used for primary
    /// surfaces and content regions.
    /// </summary>
    public AryPaletteState Mid { get; }

    /// <summary>Returns the <see cref="AryPalette" /> corresponding to the specified elevation and component state.</summary>
    /// <param name="elevation">The desired elevation level for the component.</param>
    /// <param name="state">The visual state of the component (e.g., Default, Hovered, Pressed).</param>
    /// <returns>
    /// The <see cref="AryPalette" /> instance appropriate for the given elevation and state. Returns the <see cref="Mid" />
    /// layer’s palette if the elevation is unrecognized.
    /// </returns>
    public AryPalette ToPalette(ComponentElevation elevation, ComponentState state)
        => elevation switch
        {
            ComponentElevation.Lowest => Lowest.ToPalette(state),
            ComponentElevation.Low => Low.ToPalette(state),
            ComponentElevation.High => High.ToPalette(state),
            ComponentElevation.Highest => Highest.ToPalette(state),
            _ => Mid.ToPalette(state)
        };
}
