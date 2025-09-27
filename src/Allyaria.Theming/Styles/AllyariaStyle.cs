namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a combined Allyaria style that encapsulates both palette and typography settings. Provides methods to
/// generate CSS fragments for applying the style.
/// </summary>
/// <param name="Palette">The color palette to apply.</param>
/// <param name="Typography">The typography settings to apply.</param>
public readonly record struct AllyariaStyle(AllyariaPalette Palette, AllyariaTypography Typography)
{
    /// <summary>Builds the full CSS string for this style.</summary>
    /// <returns>A concatenated CSS string including palette and typography CSS.</returns>
    public string ToCss() => string.Concat(Palette.ToCss(), Typography.ToCss());

    /// <summary>
    /// Builds a CSS string for custom property (variable) declarations representing this style. The method normalizes the
    /// optional <paramref name="prefix" /> by trimming whitespace and dashes, converting to lowercase, and replacing spaces
    /// with hyphens. If no usable prefix remains, variables are emitted with the default <c>--aa-</c> prefix; otherwise, the
    /// computed prefix is applied.
    /// </summary>
    /// <param name="prefix">
    /// An optional string used to namespace the CSS variables. May contain spaces or leading/trailing dashes, which are
    /// normalized before use. If empty or whitespace, defaults to <c>--aa-</c>.
    /// </param>
    /// <returns>A concatenated CSS string that includes both palette and typography variable declarations.</returns>
    /// <remarks>
    /// This method composes the results of <see cref="AllyariaPalette.ToCssVars(string)" /> and
    /// <see cref="AllyariaTypography.ToCssVars(string)" /> using the same normalized prefix.
    /// </remarks>
    public string ToCssVars(string prefix = "")
        => string.Concat(Palette.ToCssVars(prefix), Typography.ToCssVars(prefix));
}
