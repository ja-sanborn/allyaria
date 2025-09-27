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

    /// <summary>Builds the CSS string for hover state of this style.</summary>
    /// <returns>A concatenated CSS string including palette hover and typography CSS.</returns>
    public string ToCssHover() => string.Concat(Palette.ToCssHover(), Typography.ToCss());

    /// <summary>Builds the CSS string for CSS variable declarations of this style.</summary>
    /// <returns>A concatenated CSS string including palette and typography variable declarations.</returns>
    public string ToCssVars() => string.Concat(Palette.ToCssVars(), Typography.ToCssVars());
}
