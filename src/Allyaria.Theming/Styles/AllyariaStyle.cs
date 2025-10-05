using Allyaria.Theming.Helpers;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a complete set of visual <see cref="AllyariaStyleVariant" />s for a component, including the
/// <see cref="Default" /> variant and a range of state/tonal variants (e.g., <see cref="Hovered" />,
/// <see cref="Pressed" />, <see cref="High" />).
/// </summary>
/// <remarks>
/// All variants are immutable and designed to be derived from the <see cref="Default" /> paletteState. The
/// <see cref="Cascade(AllyariaStyleVariant)" /> method updates only the <em>paletteState</em> of each existing variant
/// to match a new default palette while preserving each variant's other configuration (typography, spacing,
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
        Disabled = Default.Cascade(ColorHelper.DeriveDisabled(Default.Palette));
        Hovered = Default.Cascade(ColorHelper.DeriveHovered(Default.Palette));
        Focused = Default.Cascade(ColorHelper.DeriveFocused(Default.Palette));
        Pressed = Default.Cascade(ColorHelper.DerivePressed(Default.Palette));
        Dragged = Default.Cascade(ColorHelper.DeriveDragged(Default.Palette));
        Lowest = Default.Cascade(ColorHelper.DeriveLowest(Default.Palette));
        Low = Default.Cascade(ColorHelper.DeriveLow(Default.Palette));
        High = Default.Cascade(ColorHelper.DeriveHigh(Default.Palette));
        Highest = Default.Cascade(ColorHelper.DeriveHighest(Default.Palette));
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
    /// palettes derived from <paramref name="defaultStyle" />'s palette.
    /// </summary>
    /// <param name="defaultStyle">The new default style variant to use as the basis for palette derivation.</param>
    /// <returns>
    /// A new <see cref="AllyariaStyle" /> where each non-default variant maintains its typography, spacing, and borders, but
    /// adopts a palette derived from <paramref name="defaultStyle" /> consistent with its role.
    /// </returns>
    /// <remarks>
    /// This method intentionally modifies only the <see cref="AllyariaStyleVariant.Palette" /> on each variant. Other
    /// aspects of the variants (typography, spacing, borders) remain unchanged.
    /// </remarks>
    public AllyariaStyle Cascade(AllyariaStyleVariant defaultStyle)
    {
        var newStyle = this with
        {
            Default = defaultStyle
        };

        return Cascade(newStyle.Default.Palette);
    }

    /// <summary>
    /// Updates this style by cascading a new base <paramref name="defaultPalette" /> to all variants, changing only
    /// their palettes and preserving non-palette configuration (typography, spacing, borders).
    /// </summary>
    /// <param name="defaultPalette">The palette from which all variant palettes are derived.</param>
    /// <returns>
    /// A new <see cref="AllyariaStyle" /> with updated palettes across all variants using the supplied
    /// <paramref name="defaultPalette" /> as the derivation source.
    /// </returns>
    public AllyariaStyle Cascade(AllyariaPalette defaultPalette)
        => this with
        {
            Default = Default.Cascade(defaultPalette),
            Disabled = Disabled.Cascade(ColorHelper.DeriveDisabled(defaultPalette)),
            Hovered = Hovered.Cascade(ColorHelper.DeriveHovered(defaultPalette)),
            Focused = Focused.Cascade(ColorHelper.DeriveFocused(defaultPalette)),
            Pressed = Pressed.Cascade(ColorHelper.DerivePressed(defaultPalette)),
            Dragged = Dragged.Cascade(ColorHelper.DeriveDragged(defaultPalette)),
            Lowest = Lowest.Cascade(ColorHelper.DeriveLowest(defaultPalette)),
            Low = Low.Cascade(ColorHelper.DeriveLow(defaultPalette)),
            High = High.Cascade(ColorHelper.DeriveHigh(defaultPalette)),
            Highest = Highest.Cascade(ColorHelper.DeriveHighest(defaultPalette))
        };
}
