using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Helpers;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a set of <see cref="AllyariaPalette" /> instances corresponding to the interactive visual states of a
/// component (e.g., default, hovered, focused, pressed, disabled, dragged).
/// </summary>
/// <remarks>
/// Each state-specific palette is derived from the base <see cref="Default" /> palette using helper methods in
/// <see cref="ColorHelper" /> to ensure consistent color transformations and accessibility contrast across UI states.
/// </remarks>
internal readonly record struct AllyariaPaletteState
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaPaletteState" /> struct using the specified base palette.
    /// </summary>
    /// <param name="palette">
    /// The base <see cref="AllyariaPalette" /> from which all state variants will be derived. If <see langword="null" />, a
    /// new default palette is created using <see cref="AllyariaPalette.AllyariaPalette()" />.
    /// </param>
    /// <remarks>
    /// The constructor automatically derives the <see cref="Disabled" />, <see cref="Hovered" />, <see cref="Focused" />,
    /// <see cref="Pressed" />, and <see cref="Dragged" /> palettes using <see cref="ColorHelper" />’s state derivation
    /// methods.
    /// </remarks>
    public AllyariaPaletteState(AllyariaPalette? palette)
    {
        Default = palette ?? new AllyariaPalette();
        Disabled = ColorHelper.DeriveDisabled(Default);
        Hovered = ColorHelper.DeriveHovered(Default);
        Focused = ColorHelper.DeriveFocused(Default);
        Pressed = ColorHelper.DerivePressed(Default);
        Dragged = ColorHelper.DeriveDragged(Default);
    }

    /// <summary>Gets the default (rest) <see cref="AllyariaPalette" /> for the component.</summary>
    internal AllyariaPalette Default { get; }

    /// <summary>
    /// Gets the disabled-state <see cref="AllyariaPalette" />, typically rendered with reduced contrast or opacity.
    /// </summary>
    internal AllyariaPalette Disabled { get; }

    /// <summary>Gets the dragged-state <see cref="AllyariaPalette" />, applied during drag interactions.</summary>
    internal AllyariaPalette Dragged { get; }

    /// <summary>
    /// Gets the focused-state <see cref="AllyariaPalette" />, applied when the component receives keyboard or programmatic
    /// focus.
    /// </summary>
    internal AllyariaPalette Focused { get; }

    /// <summary>
    /// Gets the hovered-state <see cref="AllyariaPalette" />, applied when the user’s pointer is over the component.
    /// </summary>
    internal AllyariaPalette Hovered { get; }

    /// <summary>
    /// Gets the pressed-state <see cref="AllyariaPalette" />, applied while the component is actively clicked or tapped.
    /// </summary>
    internal AllyariaPalette Pressed { get; }

    /// <summary>
    /// Returns the <see cref="AllyariaPalette" /> corresponding to the specified <see cref="ComponentState" />.
    /// </summary>
    /// <param name="state">The component’s current visual state.</param>
    /// <returns>
    /// The <see cref="AllyariaPalette" /> associated with the given state; returns <see cref="Default" /> if the state is
    /// unrecognized.
    /// </returns>
    internal AllyariaPalette ToPalette(ComponentState state)
        => state switch
        {
            ComponentState.Disabled => Disabled,
            ComponentState.Dragged => Dragged,
            ComponentState.Focused => Focused,
            ComponentState.Hovered => Hovered,
            ComponentState.Pressed => Pressed,
            _ => Default
        };
}
