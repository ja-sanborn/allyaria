using Allyaria.Theming.Enumerations;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a collection of <see cref="AryPaletteState" /> instances organized by visual elevation levels (e.g., lowest,
/// low, mid, high, highest). Each elevation level defines color behavior for shadows, layering, and perceived depth within
/// the Allyaria theming system.
/// </summary>
/// <remarks>
/// Elevation layers are derived from a base <see cref="AryPalette" /> using <see cref="ColorHelper" /> methods to ensure
/// accessible contrast and consistent depth perception across themes.
/// </remarks>
internal readonly record struct AryPaletteElevation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AryPaletteElevation" /> struct using the specified base palette.
    /// </summary>
    /// <param name="palette">
    /// The base <see cref="AryPalette" /> from which all elevation layers are derived. If <see langword="null" />, a new
    /// default palette is created using <see cref="AryPalette.AllyariaPalette()" />.
    /// </param>
    /// <remarks>
    /// The constructor creates five elevation layers:
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="Lowest" /> — darkest or most recessed layer.</description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="Low" /> — low depth layer.</description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="Mid" /> — neutral layer (base surface).</description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="High" /> — slightly raised layer with lighter or more contrasted colors.</description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="Highest" /> — topmost elevation, brightest or most accentuated layer.</description>
    ///     </item>
    /// </list>
    /// </remarks>
    public AryPaletteElevation(AryPalette? palette)
    {
        Palette = palette ?? new AryPalette();

        Lowest = new AryPaletteState(ColorHelper.DeriveLowest(Palette));
        Low = new AryPaletteState(ColorHelper.DeriveLow(Palette));
        Mid = new AryPaletteState(Palette);
        High = new AryPaletteState(ColorHelper.DeriveHigh(Palette));
        Highest = new AryPaletteState(ColorHelper.DeriveHighest(Palette));
    }

    /// <summary>
    /// Gets the <see cref="AryPaletteState" /> representing the “high” elevation layer, used for moderately raised components
    /// (e.g., popovers, cards).
    /// </summary>
    internal AryPaletteState High { get; }

    /// <summary>
    /// Gets the <see cref="AryPaletteState" /> representing the “highest” elevation layer, used for topmost components such as
    /// dialogs or modals.
    /// </summary>
    internal AryPaletteState Highest { get; }

    /// <summary>
    /// Gets the <see cref="AryPaletteState" /> representing the “low” elevation layer, typically used for slightly raised or
    /// inset elements.
    /// </summary>
    internal AryPaletteState Low { get; }

    /// <summary>
    /// Gets the <see cref="AryPaletteState" /> representing the “lowest” elevation layer, generally used for background or
    /// deeply recessed elements.
    /// </summary>
    internal AryPaletteState Lowest { get; }

    /// <summary>
    /// Gets the <see cref="AryPaletteState" /> representing the “mid” (neutral) elevation layer, commonly used for primary
    /// surfaces and content regions.
    /// </summary>
    internal AryPaletteState Mid { get; }

    /// <summary>Gets the base <see cref="AryPalette" /> from which all elevation layers are derived.</summary>
    internal AryPalette Palette { get; }

    /// <summary>
    /// Returns the <see cref="AryPalette" /> corresponding to the specified elevation and component state.
    /// </summary>
    /// <param name="elevation">The desired elevation level for the component.</param>
    /// <param name="state">The visual state of the component (e.g., default, hovered, pressed).</param>
    /// <returns>
    /// The <see cref="AryPalette" /> instance appropriate for the given elevation and state. Returns the <see cref="Mid" />
    /// layer’s palette if the elevation is unrecognized.
    /// </returns>
    internal AryPalette ToPalette(ComponentElevation elevation, ComponentState state)
        => elevation switch
        {
            ComponentElevation.Lowest => Lowest.ToPalette(state),
            ComponentElevation.Low => Low.ToPalette(state),
            ComponentElevation.High => High.ToPalette(state),
            ComponentElevation.Highest => Highest.ToPalette(state),
            _ => Mid.ToPalette(state)
        };
}
