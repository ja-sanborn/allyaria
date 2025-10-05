using Allyaria.Theming.Helpers;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a complete set of visual <see cref="AllyariaStyleVariant" />s for a component, including the
/// <see cref="Default" /> variant and a range of state/tonal variants (e.g., <see cref="Hovered" />,
/// <see cref="Pressed" />, <see cref="High" />).
/// </summary>
/// <remarks>
/// All variants are immutable and designed to be derived from the <see cref="Default" /> paletteVariant. The
/// <see cref="Cascade(AllyariaStyleVariant)" /> method updates only the <em>paletteVariant</em> of each existing variant
/// to match a new default paletteVariant while preserving each variant's other configuration (typography, spacing,
/// borders).
/// </remarks>
public readonly record struct AllyariaStyle
{
    /// <summary>
    /// Initializes a new <see cref="AllyariaStyle" /> with a default-constructed <see cref="AllyariaStyleVariant" />.
    /// </summary>
    public AllyariaStyle()
        : this(null) { }

    /// <summary>Initializes a new <see cref="AllyariaStyle" /> using the provided default style variant.</summary>
    /// <param name="defaultStyle">
    /// An optional default <see cref="AllyariaStyleVariant" /> to seed all other variants. When <c>null</c>, a new default
    /// <see cref="AllyariaStyleVariant" /> is created.
    /// </param>
    /// <remarks>
    /// Non-default variants are created by <em>cascading</em> from <paramref name="defaultStyle" /> and replacing only their
    /// palettes using derived palettes from <see cref="ColorHelper" /> consistent with each variant's role.
    /// </remarks>
    public AllyariaStyle(AllyariaStyleVariant? defaultStyle = null)
    {
        Default = defaultStyle ?? new AllyariaStyleVariant();
        Disabled = Default.Cascade(ColorHelper.DeriveDisabled(Default.PaletteVariant));
        Hovered = Default.Cascade(ColorHelper.DeriveHovered(Default.PaletteVariant));
        Focused = Default.Cascade(ColorHelper.DeriveFocused(Default.PaletteVariant));
        Pressed = Default.Cascade(ColorHelper.DerivePressed(Default.PaletteVariant));
        Dragged = Default.Cascade(ColorHelper.DeriveDragged(Default.PaletteVariant));
        Lowest = Default.Cascade(ColorHelper.DeriveLowest(Default.PaletteVariant));
        Low = Default.Cascade(ColorHelper.DeriveLow(Default.PaletteVariant));
        High = Default.Cascade(ColorHelper.DeriveHigh(Default.PaletteVariant));
        Highest = Default.Cascade(ColorHelper.DeriveHighest(Default.PaletteVariant));
    }

    /// <summary>Gets the base/default visual variant from which other variants are derived.</summary>
    public AllyariaStyleVariant Default { get; init; }

    /// <summary>Gets the Disabled state variant (reduced contrast / muted interactions).</summary>
    public AllyariaStyleVariant Disabled { get; init; }

    /// <summary>Gets the Dragged state variant (used during drag interactions).</summary>
    public AllyariaStyleVariant Dragged { get; init; }

    /// <summary>Gets the Focused state variant (keyboard focus ring alignment).</summary>
    public AllyariaStyleVariant Focused { get; init; }

    /// <summary>Gets the High tonal variant (high emphasis).</summary>
    public AllyariaStyleVariant High { get; init; }

    /// <summary>Gets the Highest tonal variant (maximum emphasis).</summary>
    public AllyariaStyleVariant Highest { get; init; }

    /// <summary>Gets the Hovered state variant.</summary>
    public AllyariaStyleVariant Hovered { get; init; }

    /// <summary>Gets the Low tonal variant (low emphasis).</summary>
    public AllyariaStyleVariant Low { get; init; }

    /// <summary>Gets the Lowest tonal variant (lowest emphasis).</summary>
    public AllyariaStyleVariant Lowest { get; init; }

    /// <summary>Gets the Pressed/Active state variant.</summary>
    public AllyariaStyleVariant Pressed { get; init; }

    /// <summary>
    /// Produces a new <see cref="AllyariaStyle" /> whose <see cref="Default" /> variant is replaced with
    /// <paramref name="defaultStyle" />, and whose other variants are updated by changing <em>only their palettes</em> to
    /// palettes derived from <paramref name="defaultStyle" />'s paletteVariant.
    /// </summary>
    /// <param name="defaultStyle">The new default style variant to use as the basis for paletteVariant derivation.</param>
    /// <returns>
    /// A new <see cref="AllyariaStyle" /> where each non-default variant maintains its typography, spacing, and borders, but
    /// adopts a paletteVariant derived from <paramref name="defaultStyle" /> consistent with its role.
    /// </returns>
    /// <remarks>
    /// This method intentionally modifies only the <see cref="AllyariaStyleVariant.PaletteVariant" /> on each variant. Other
    /// aspects of the variants (typography, spacing, borders) remain unchanged.
    /// </remarks>
    public AllyariaStyle Cascade(AllyariaStyleVariant defaultStyle)
    {
        var newStyle = this with
        {
            Default = defaultStyle
        };

        return Cascade(newStyle.Default.PaletteVariant);
    }

    /// <summary>
    /// Updates this style by cascading a new base <paramref name="defaultPaletteVariant" /> to all variants, changing only
    /// their palettes and preserving non-paletteVariant configuration (typography, spacing, borders).
    /// </summary>
    /// <param name="defaultPaletteVariant">The paletteVariant from which all variant palettes are derived.</param>
    /// <returns>
    /// A new <see cref="AllyariaStyle" /> with updated palettes across all variants using the supplied
    /// <paramref name="defaultPaletteVariant" /> as the derivation source.
    /// </returns>
    public AllyariaStyle Cascade(AllyariaPaletteVariant defaultPaletteVariant)
        => this with
        {
            Default = Default.Cascade(defaultPaletteVariant),
            Disabled = Disabled.Cascade(ColorHelper.DeriveDisabled(defaultPaletteVariant)),
            Hovered = Hovered.Cascade(ColorHelper.DeriveHovered(defaultPaletteVariant)),
            Focused = Focused.Cascade(ColorHelper.DeriveFocused(defaultPaletteVariant)),
            Pressed = Pressed.Cascade(ColorHelper.DerivePressed(defaultPaletteVariant)),
            Dragged = Dragged.Cascade(ColorHelper.DeriveDragged(defaultPaletteVariant)),
            Lowest = Lowest.Cascade(ColorHelper.DeriveLowest(defaultPaletteVariant)),
            Low = Low.Cascade(ColorHelper.DeriveLow(defaultPaletteVariant)),
            High = High.Cascade(ColorHelper.DeriveHigh(defaultPaletteVariant)),
            Highest = Highest.Cascade(ColorHelper.DeriveHighest(defaultPaletteVariant))
        };
}
