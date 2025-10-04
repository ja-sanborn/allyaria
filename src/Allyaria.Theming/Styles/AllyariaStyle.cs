namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a group of related visual styles for a component state set, including default, disabled, and hover variants.
/// </summary>
public readonly record struct AllyariaStyle
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaStyle" /> struct.</summary>
    /// <param name="defaultStyle">
    /// The default style to use, or <c>null</c> to create a new <see cref="AllyariaStyleVariant" />
    /// .
    /// </param>
    /// <param name="disabledStyle">The disabled style to use, or <c>null</c> to derive from the default style.</param>
    /// <param name="hoverStyle">The hover style to use, or <c>null</c> to derive from the default style.</param>
    public AllyariaStyle(AllyariaStyleVariant? defaultStyle = null,
        AllyariaStyleVariant? disabledStyle = null,
        AllyariaStyleVariant? hoverStyle = null)
    {
        Default = defaultStyle ?? new AllyariaStyleVariant();
        Disabled = disabledStyle ?? Default.Cascade(Default.Palette.ToDisabled());
        Hover = hoverStyle ?? Default.Cascade(Default.Palette.ToHover());
    }

    /// <summary>Gets the default style configuration.</summary>
    public AllyariaStyleVariant Default { get; init; }

    /// <summary>Gets the disabled state style configuration.</summary>
    public AllyariaStyleVariant Disabled { get; init; }

    /// <summary>Gets the hover state style configuration.</summary>
    public AllyariaStyleVariant Hover { get; init; }

    /// <summary>
    /// Creates a new <see cref="AllyariaStyle" /> by cascading new style values over the current instance.
    /// </summary>
    /// <param name="defaultStyle">An optional default style override.</param>
    /// <param name="disabledStyle">An optional disabled style override.</param>
    /// <param name="hoverStyle">An optional hover style override.</param>
    /// <returns>A new <see cref="AllyariaStyle" /> instance with the provided style overrides applied.</returns>
    public AllyariaStyle Cascade(AllyariaStyleVariant? defaultStyle = null,
        AllyariaStyleVariant? disabledStyle = null,
        AllyariaStyleVariant? hoverStyle = null)
        => this with
        {
            Default = defaultStyle ?? Default,
            Disabled = disabledStyle ?? Disabled,
            Hover = hoverStyle ?? Hover
        };
}
