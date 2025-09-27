using System.Text.RegularExpressions;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a combined Allyaria style that encapsulates both palette and typography settings. Provides methods to
/// generate CSS fragments and CSS custom property declarations for applying the style, including distinct "hover"
/// variants.
/// </summary>
public readonly record struct AllyariaStyle
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaStyle" /> struct.</summary>
    /// <param name="palette">The color palette to apply.</param>
    /// <param name="typography">The typography settings to apply.</param>
    /// <param name="paletteHover">The color palette to apply for hover effects. Defaults to a derived hover palette.</param>
    /// <param name="typographyHover">
    /// The typography settings to apply for hover effects. Defaults to
    /// <paramref name="typography" />.
    /// </param>
    public AllyariaStyle(AllyariaPalette palette,
        AllyariaTypography typography,
        AllyariaPalette? paletteHover = null,
        AllyariaTypography? typographyHover = null)
    {
        Palette = palette;
        Typography = typography;

        PaletteHover = paletteHover
            ?? palette.Cascade(
                palette.BackgroundColor.HoverColor(),
                palette.ForegroundColor.HoverColor()
            );

        TypographyHover = typographyHover ?? typography;
    }

    /// <summary>Gets the color palette to apply.</summary>
    public AllyariaPalette Palette { get; }

    /// <summary>Gets the color palette to apply for hover effects.</summary>
    public AllyariaPalette PaletteHover { get; }

    /// <summary>Gets the typography settings to apply.</summary>
    public AllyariaTypography Typography { get; }

    /// <summary>Gets the typography settings to apply for hover effects.</summary>
    public AllyariaTypography TypographyHover { get; }

    /// <summary>Builds the full CSS string for this style, combining palette and typography CSS.</summary>
    /// <returns>A concatenated CSS string including palette and typography CSS.</returns>
    public string ToCss() => string.Concat(Palette.ToCss(), Typography.ToCss());

    /// <summary>
    /// Builds the full CSS string for the <c>:hover</c> state, combining hover palette and hover typography CSS.
    /// </summary>
    /// <returns>A concatenated CSS string including hover palette and hover typography CSS.</returns>
    public string ToCssHover() => string.Concat(PaletteHover.ToCss(), TypographyHover.ToCss());

    /// <summary>
    /// Builds a CSS string for custom property (variable) declarations representing this style and its hover variants. The
    /// optional <paramref name="prefix" /> is normalized by trimming whitespace and dashes, converting to lowercase, and
    /// replacing spaces with hyphens. If no usable prefix remains, variables are emitted with the default <c>aa</c> prefix.
    /// Hover prefix appends <c>-hover</c> to the prefix.
    /// </summary>
    /// <param name="prefix">
    /// An optional namespace for the CSS variables (e.g., <c>"editor"</c>). Spaces and control characters are replaced with
    /// hyphens. If empty or whitespace, defaults to <c>aa</c>. Hover prefix appends <c>-hover</c> to the prefix.
    /// </param>
    /// <returns>
    /// A concatenated CSS string that includes palette and typography variable declarations for both base and hover states,
    /// using the same normalized prefix.
    /// </returns>
    /// <remarks>
    /// This method composes the results of <see cref="AllyariaPalette.ToCssVars(string)" /> and
    /// <see cref="AllyariaTypography.ToCssVars(string)" /> with a normalized prefix. Hover variables are emitted with an
    /// additional <c>-hover</c> suffix appended to the base prefix.
    /// </remarks>
    public string ToCssVars(string prefix = "")
    {
        var basePrefix = Regex.Replace(prefix, @"\s+|-+", "-").Trim('-').ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(basePrefix))
        {
            basePrefix = "aa";
        }

        var hoverPrefix = $"{basePrefix}-hover";

        return string.Concat(
            Palette.ToCssVars(basePrefix),
            Typography.ToCssVars(basePrefix),
            PaletteHover.ToCssVars(hoverPrefix),
            TypographyHover.ToCssVars(hoverPrefix)
        );
    }
}
