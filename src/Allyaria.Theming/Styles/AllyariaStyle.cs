using System.Text.RegularExpressions;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a combined Allyaria style that encapsulates both palette and typography settings. Provides helpers to
/// generate CSS fragments and CSS custom property declarations for the base, hover, and disabled states.
/// </summary>
public readonly record struct AllyariaStyle
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaStyle" /> struct.</summary>
    /// <param name="palette">The base color palette to apply.</param>
    /// <param name="typography">The base typography settings to apply.</param>
    /// <param name="paletteHover">
    /// Optional palette to apply for the <c>:hover</c> state. If <see langword="null" />, it is derived from
    /// <paramref name="palette" /> via <c>palette.ToHoverPalette()</c>.
    /// </param>
    /// <param name="typographyHover">
    /// Optional typography to apply for the <c>:hover</c> state. If <see langword="null" />, it defaults to
    /// <paramref name="typography" />.
    /// </param>
    /// <param name="paletteDisabled">
    /// Optional palette to apply for the disabled state. If <see langword="null" />, it is derived from
    /// <paramref name="palette" /> via <c>palette.ToDisabledPalette()</c>.
    /// </param>
    /// <param name="typographyDisabled">
    /// Optional typography to apply for the disabled state. If <see langword="null" />, it defaults to
    /// <paramref name="typography" />.
    /// </param>
    public AllyariaStyle(AllyariaPalette palette,
        AllyariaTypography typography,
        AllyariaPalette? paletteHover = null,
        AllyariaTypography? typographyHover = null,
        AllyariaPalette? paletteDisabled = null,
        AllyariaTypography? typographyDisabled = null)
    {
        Palette = palette;
        Typography = typography;
        PaletteHover = paletteHover ?? palette.ToHoverPalette();
        TypographyHover = typographyHover ?? typography;
        PaletteDisabled = paletteDisabled ?? palette.ToDisabledPalette();
        TypographyDisabled = typographyDisabled ?? typography;
    }

    /// <summary>Gets the base color palette.</summary>
    public AllyariaPalette Palette { get; }

    /// <summary>Gets the color palette used for the disabled state.</summary>
    public AllyariaPalette PaletteDisabled { get; }

    /// <summary>Gets the color palette used for the <c>:hover</c> state.</summary>
    public AllyariaPalette PaletteHover { get; }

    /// <summary>Gets the base typography settings.</summary>
    public AllyariaTypography Typography { get; }

    /// <summary>Gets the typography settings used for the disabled state.</summary>
    public AllyariaTypography TypographyDisabled { get; }

    /// <summary>Gets the typography settings used for the <c>:hover</c> state.</summary>
    public AllyariaTypography TypographyHover { get; }

    /// <summary>
    /// Builds the full CSS string for this style, concatenating palette and typography CSS declarations for the base state.
    /// </summary>
    /// <returns>
    /// A concatenated CSS string that includes base palette and typography declarations (e.g., <c>background-color</c>,
    /// <c>color</c>, font declarations).
    /// </returns>
    public string ToCss() => string.Concat(Palette.ToCss(), Typography.ToCss());

    /// <summary>
    /// Builds the full CSS string for the <c>:hover</c> state, concatenating hover palette and hover typography CSS.
    /// </summary>
    /// <returns>A concatenated CSS string that includes hover palette and hover typography declarations.</returns>
    public string ToCssHover() => string.Concat(PaletteHover.ToCss(), TypographyHover.ToCss());

    /// <summary>
    /// Builds CSS custom property (variable) declarations representing this style for the base, disabled, and hover states
    /// using a normalized variable prefix.
    /// </summary>
    /// <param name="prefix">
    /// Optional namespace for the CSS variables (e.g., <c>"editor"</c>). The value is normalized by: trimming whitespace and
    /// dashes, converting to lowercase, and collapsing whitespace/dashes to single dashes. If the normalized value is empty,
    /// the default prefix <c>aa</c> is used. The disabled and hover prefixes are formed by appending <c>-disabled</c> and
    /// <c>-hover</c> respectively.
    /// </param>
    /// <returns>
    /// A concatenated CSS string of custom property declarations for:
    /// <list type="bullet">
    ///     <item>
    ///         <description>Base variables (using the base prefix)</description>
    ///     </item>
    ///     <item>
    ///         <description>Disabled variables (using <c>{prefix}-disabled</c>)</description>
    ///     </item>
    ///     <item>
    ///         <description>Hover variables (using <c>{prefix}-hover</c>)</description>
    ///     </item>
    /// </list>
    /// Each underlying call delegates to <see cref="AllyariaPalette.ToCssVars(string)" /> and
    /// <see cref="AllyariaTypography.ToCssVars(string)" /> which apply their own <c>--</c> prefixing convention.
    /// </returns>
    /// <remarks>
    /// The final CSS variable names produced by the underlying palette/typography methods include the leading <c>--</c> and a
    /// trailing hyphen after the provided prefix (e.g., <c>--editor-*</c>).
    /// </remarks>
    public string ToCssVars(string prefix = "")
    {
        var basePrefix = Regex.Replace(prefix, @"[\s-]+", "-").Trim('-').ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(basePrefix))
        {
            basePrefix = "aa";
        }

        var disabledPrefix = $"{basePrefix}-disabled";
        var hoverPrefix = $"{basePrefix}-hover";

        return string.Concat(
            Palette.ToCssVars(basePrefix),
            Typography.ToCssVars(basePrefix),
            PaletteDisabled.ToCssVars(disabledPrefix),
            TypographyDisabled.ToCssVars(disabledPrefix),
            PaletteHover.ToCssVars(hoverPrefix),
            TypographyHover.ToCssVars(hoverPrefix)
        );
    }
}
