using Allyaria.Theming.Helpers;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a complete set of visual style variants (default + interactive + tone levels) used by Allyaria components.
/// This is a small, immutable value type intended to be copied efficiently and composed via
/// <see
///     cref="Cascade(AllyariaStyleVariant?, AllyariaStyleVariant?, AllyariaStyleVariant?, AllyariaStyleVariant?, AllyariaStyleVariant?, AllyariaStyleVariant?, AllyariaStyleVariant?, AllyariaStyleVariant?, AllyariaStyleVariant?, AllyariaStyleVariant?)" />
/// .
/// </summary>
/// <remarks>
/// Construction follows the theming precedence rules:
/// <list type="number">
///     <item>
///         <description>Start from <see cref="Default" /> (base palette).</description>
///     </item>
///     <item>
///         <description>
///         Derive state variants (Disabled/Hovered/Focused/Pressed/Dragged) by cascading palette adjustments from the
///         base.
///         </description>
///     </item>
///     <item>
///         <description>Derive tonal variants (Lowest/Low/High/Highest) similarly from the base.</description>
///     </item>
/// </list>
/// Callers may override any subset of variants explicitly; unspecified variants are derived from <see cref="Default" />.
/// This aligns with Allyaria's strong, C#-driven theming model and avoids relying on global CSS overrides.
/// </remarks>
public readonly record struct AllyariaStyle
{
    /// <summary>
    /// Initializes a new <see cref="AllyariaStyle" /> with an optional base <paramref name="defaultStyle" /> and optional
    /// explicit overrides for any other variants. Any <c>null</c> parameter is derived from the base style using
    /// <see cref="ColorHelper" /> heuristics.
    /// </summary>
    /// <param name="defaultStyle">The base variant. If <c>null</c>, a new default <see cref="AllyariaStyleVariant" /> is used.</param>
    /// <param name="disabledStyle">Optional explicit Disabled variant; otherwise derived from the base.</param>
    /// <param name="hoveredStyle">Optional explicit Hovered variant; otherwise derived from the base.</param>
    /// <param name="focusedStyle">Optional explicit Focused variant; otherwise derived from the base.</param>
    /// <param name="pressedStyle">Optional explicit Pressed variant; otherwise derived from the base.</param>
    /// <param name="draggedStyle">Optional explicit Dragged variant; otherwise derived from the base.</param>
    /// <param name="lowestStyle">Optional explicit Lowest tonal variant; otherwise derived from the base.</param>
    /// <param name="lowStyle">Optional explicit Low tonal variant; otherwise derived from the base.</param>
    /// <param name="highStyle">Optional explicit High tonal variant; otherwise derived from the base.</param>
    /// <param name="highestStyle">Optional explicit Highest tonal variant; otherwise derived from the base.</param>
    /// <remarks>No exceptions are thrown; all parameters are optional and safely default to derived values.</remarks>
    public AllyariaStyle(AllyariaStyleVariant? defaultStyle = null,
        AllyariaStyleVariant? disabledStyle = null,
        AllyariaStyleVariant? hoveredStyle = null,
        AllyariaStyleVariant? focusedStyle = null,
        AllyariaStyleVariant? pressedStyle = null,
        AllyariaStyleVariant? draggedStyle = null,
        AllyariaStyleVariant? lowestStyle = null,
        AllyariaStyleVariant? lowStyle = null,
        AllyariaStyleVariant? highStyle = null,
        AllyariaStyleVariant? highestStyle = null)
    {
        Default = defaultStyle ?? new AllyariaStyleVariant();
        Disabled = disabledStyle ?? Default.Cascade(ColorHelper.DeriveDisabled(Default.Palette));
        Hovered = hoveredStyle ?? Default.Cascade(ColorHelper.DeriveHovered(Default.Palette));
        Focused = focusedStyle ?? Default.Cascade(ColorHelper.DeriveFocused(Default.Palette));
        Pressed = pressedStyle ?? Default.Cascade(ColorHelper.DerivePressed(Default.Palette));
        Dragged = draggedStyle ?? Default.Cascade(ColorHelper.DeriveDragged(Default.Palette));
        Lowest = lowestStyle ?? Default.Cascade(ColorHelper.DeriveLowest(Default.Palette));
        Low = lowStyle ?? Default.Cascade(ColorHelper.DeriveLow(Default.Palette));
        High = highStyle ?? Default.Cascade(ColorHelper.DeriveHigh(Default.Palette));
        Highest = highestStyle ?? Default.Cascade(ColorHelper.DeriveHighest(Default.Palette));
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

    /// <summary>Creates a new <see cref="AllyariaStyle" /> by selectively overriding any subset of variants.</summary>
    /// <param name="defaultStyle">
    /// Optional new base variant.
    /// <list type="bullet">
    ///     <item>
    ///     If <c>defaultStyle</c> is provided (non-null), the returned style uses it as the new <see cref="Default" />. Any
    ///     <c>null</c> state/tonal parameters are then <b>re-derived</b> from this new base using <see cref="ColorHelper" />
    ///     heuristics (mirrors constructor behavior).
    ///     </item>
    ///     <item>
    ///     If <c>defaultStyle</c> is <c>null</c>, the current instance’s <see cref="Default" /> is kept and any <c>null</c>
    ///     parameters keep their existing values (legacy behavior).
    ///     </item>
    /// </list>
    /// </param>
    /// <param name="disabledStyle">Optional override for <see cref="Disabled" />.</param>
    /// <param name="hoveredStyle">Optional override for <see cref="Hovered" />.</param>
    /// <param name="focusedStyle">Optional override for <see cref="Focused" />.</param>
    /// <param name="pressedStyle">Optional override for <see cref="Pressed" />.</param>
    /// <param name="draggedStyle">Optional override for <see cref="Dragged" />.</param>
    /// <param name="lowestStyle">Optional override for <see cref="Lowest" />.</param>
    /// <param name="lowStyle">Optional override for <see cref="Low" />.</param>
    /// <param name="highStyle">Optional override for <see cref="High" />.</param>
    /// <param name="highestStyle">Optional override for <see cref="Highest" />.</param>
    /// <returns>
    /// A new <see cref="AllyariaStyle" /> with overrides applied and, when <paramref name="defaultStyle" /> is provided, null
    /// variants re-derived from the new base.
    /// </returns>
    public AllyariaStyle Cascade(AllyariaStyleVariant? defaultStyle = null,
        AllyariaStyleVariant? disabledStyle = null,
        AllyariaStyleVariant? hoveredStyle = null,
        AllyariaStyleVariant? focusedStyle = null,
        AllyariaStyleVariant? pressedStyle = null,
        AllyariaStyleVariant? draggedStyle = null,
        AllyariaStyleVariant? lowestStyle = null,
        AllyariaStyleVariant? lowStyle = null,
        AllyariaStyleVariant? highStyle = null,
        AllyariaStyleVariant? highestStyle = null)
    {
        if (defaultStyle is null)
        {
            return this with
            {
                Default = defaultStyle ?? Default,
                Disabled = disabledStyle ?? Disabled,
                Hovered = hoveredStyle ?? Hovered,
                Focused = focusedStyle ?? Focused,
                Pressed = pressedStyle ?? Pressed,
                Dragged = draggedStyle ?? Dragged,
                Lowest = lowestStyle ?? Lowest,
                Low = lowStyle ?? Low,
                High = highStyle ?? High,
                Highest = highestStyle ?? Highest
            };
        }

        var newDefault = defaultStyle.Value;
        var newPalette = newDefault.Palette;

        return this with
        {
            Default = newDefault,
            Disabled = disabledStyle ?? newDefault.Cascade(ColorHelper.DeriveDisabled(newPalette)),
            Hovered = hoveredStyle ?? newDefault.Cascade(ColorHelper.DeriveHovered(newPalette)),
            Focused = focusedStyle ?? newDefault.Cascade(ColorHelper.DeriveFocused(newPalette)),
            Pressed = pressedStyle ?? newDefault.Cascade(ColorHelper.DerivePressed(newPalette)),
            Dragged = draggedStyle ?? newDefault.Cascade(ColorHelper.DeriveDragged(newPalette)),
            Lowest = lowestStyle ?? newDefault.Cascade(ColorHelper.DeriveLowest(newPalette)),
            Low = lowStyle ?? newDefault.Cascade(ColorHelper.DeriveLow(newPalette)),
            High = highStyle ?? newDefault.Cascade(ColorHelper.DeriveHigh(newPalette)),
            Highest = highestStyle ?? newDefault.Cascade(ColorHelper.DeriveHighest(newPalette))
        };
    }
}
