namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents the set of computed <see cref="AryPalette" /> values mapped to common UI interaction states (Default,
/// Disabled, Hovered, Focused, Pressed, Dragged) for a component.
/// </summary>
/// <remarks>
/// The state palettes are derived from a single baseline <see cref="AryPalette" /> (the "Default") using extension methods
/// such as <c>ToDisabled()</c>, <c>ToHovered()</c>, etc. This struct is immutable.
/// </remarks>
internal readonly record struct AryPaletteState
{
    /// <summary>Initializes a new instance of the <see cref="AryPaletteState" /> struct from a baseline palette.</summary>
    /// <param name="palette">The baseline (rest) <see cref="AryPalette" /> used to derive all state palettes.</param>
    /// <remarks>
    /// All derived states are computed from <paramref name="palette" />. This avoids reading uninitialized members and ensures
    /// deterministic derivation across states.
    /// </remarks>
    public AryPaletteState(AryPalette palette)
    {
        Default = palette;
        Disabled = Default.ToDisabled();
        Hovered = Default.ToHovered();
        Focused = Default.ToFocused();
        Pressed = Default.ToPressed();
        Dragged = Default.ToDragged();
    }

    /// <summary>Gets the default (rest) <see cref="AryPalette" /> for the component.</summary>
    public AryPalette Default { get; }

    /// <summary>
    /// Gets the disabled-state <see cref="AryPalette" />, typically rendered with reduced contrast or opacity.
    /// </summary>
    public AryPalette Disabled { get; }

    /// <summary>Gets the dragged-state <see cref="AryPalette" />, applied during drag interactions.</summary>
    public AryPalette Dragged { get; }

    /// <summary>
    /// Gets the focused-state <see cref="AryPalette" />, applied when the component receives keyboard or programmatic focus.
    /// </summary>
    public AryPalette Focused { get; }

    /// <summary>
    /// Gets the hovered-state <see cref="AryPalette" />, applied when the user’s pointer is over the component.
    /// </summary>
    public AryPalette Hovered { get; }

    /// <summary>
    /// Gets the pressed-state <see cref="AryPalette" />, applied while the component is actively clicked or tapped.
    /// </summary>
    public AryPalette Pressed { get; }

    /// <summary>
    /// Returns the <see cref="AryPalette" /> corresponding to the specified <see cref="ComponentState" />.
    /// </summary>
    /// <param name="state">The component’s current visual state.</param>
    /// <returns>
    /// The <see cref="AryPalette" /> associated with the given state; returns <see cref="Default" /> if the state is
    /// unrecognized.
    /// </returns>
    public AryPalette ToPalette(ComponentState state)
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
