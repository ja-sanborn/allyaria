namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents the set of computed <see cref="Palette" /> values mapped to common UI interaction states (Default, Disabled,
/// Hovered, Focused, Pressed, Dragged) for a component.
/// </summary>
/// <remarks>
/// The state palettes are derived from a single baseline <see cref="Palette" /> (the "Default") using extension methods
/// such as <c>ToDisabled()</c>, <c>ToHovered()</c>, etc. This struct is immutable.
/// </remarks>
internal readonly record struct PaletteState
{
    /// <summary>Initializes a new instance of the <see cref="PaletteState" /> struct from a baseline palette.</summary>
    /// <param name="palette">The baseline (rest) <see cref="Palette" /> used to derive all state palettes.</param>
    /// <remarks>
    /// All derived states are computed from <paramref name="palette" />. This avoids reading uninitialized members and ensures
    /// deterministic derivation across states.
    /// </remarks>
    public PaletteState(Palette palette)
    {
        Default = palette;
        Disabled = Default.ToDisabled();
        Hovered = Default.ToHovered();
        Focused = Default.ToFocused();
        Pressed = Default.ToPressed();
        Dragged = Default.ToDragged();
    }

    /// <summary>Gets the default (rest) <see cref="Palette" /> for the component.</summary>
    public Palette Default { get; }

    /// <summary>
    /// Gets the disabled-state <see cref="Palette" />, typically rendered with reduced contrast or opacity.
    /// </summary>
    public Palette Disabled { get; }

    /// <summary>Gets the dragged-state <see cref="Palette" />, applied during drag interactions.</summary>
    public Palette Dragged { get; }

    /// <summary>
    /// Gets the focused-state <see cref="Palette" />, applied when the component receives keyboard or programmatic focus.
    /// </summary>
    public Palette Focused { get; }

    /// <summary>
    /// Gets the hovered-state <see cref="Palette" />, applied when the user’s pointer is over the component.
    /// </summary>
    public Palette Hovered { get; }

    /// <summary>
    /// Gets the pressed-state <see cref="Palette" />, applied while the component is actively clicked or tapped.
    /// </summary>
    public Palette Pressed { get; }

    /// <summary>Returns the <see cref="Palette" /> corresponding to the specified <see cref="ComponentState" />.</summary>
    /// <param name="state">The component’s current visual state.</param>
    /// <returns>
    /// The <see cref="Palette" /> associated with the given state; returns <see cref="Default" /> if the state is
    /// unrecognized.
    /// </returns>
    public Palette ToPalette(ComponentState state)
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
