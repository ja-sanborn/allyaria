namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a combined Allyaria style that encapsulates palette, typography, spacing, and border settings. Provides
/// helpers to generate CSS fragments and CSS custom property declarations for the base, hover, and disabled states.
/// </summary>
public readonly record struct AllyariaStyle
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaStyle" /> struct.</summary>
    /// <param name="palette">The base color palette to apply.</param>
    /// <param name="typography">The base typography settings to apply.</param>
    /// <param name="spacing">The spacing values (margins/paddings) to apply for all states.</param>
    /// <param name="border">The border values (per-side width/style and per-corner radius) to apply for all states.</param>
    /// <param name="paletteHover">
    /// Optional palette to apply for the <c>:hover</c> state. If <see langword="null" />, it is derived from
    /// <paramref name="palette" /> via <see cref="AllyariaPalette.ToHoverPalette" />.
    /// </param>
    /// <param name="typographyHover">
    /// Optional typography to apply for the <c>:hover</c> state. If <see langword="null" />, it defaults to
    /// <paramref name="typography" />.
    /// </param>
    /// <param name="paletteDisabled">
    /// Optional palette to apply for the disabled state. If <see langword="null" />, it is derived from
    /// <paramref name="palette" /> via <see cref="AllyariaPalette.ToDisabledPalette" />.
    /// </param>
    /// <param name="typographyDisabled">
    /// Optional typography to apply for the disabled state. If <see langword="null" />, it defaults to
    /// <paramref name="typography" />.
    /// </param>
    public AllyariaStyle(AllyariaPalette palette,
        AllyariaTypography typography,
        AllyariaSpacing spacing,
        AllyariaBorders border,
        AllyariaPalette? paletteHover = null,
        AllyariaTypography? typographyHover = null,
        AllyariaPalette? paletteDisabled = null,
        AllyariaTypography? typographyDisabled = null)
    {
        Spacing = spacing;
        Border = border;

        Palette = palette;
        Typography = typography;

        PaletteHover = paletteHover ?? palette.ToHoverPalette();
        TypographyHover = typographyHover ?? typography;

        PaletteDisabled = paletteDisabled ?? palette.ToDisabledPalette();
        TypographyDisabled = typographyDisabled ?? typography;
    }

    /// <summary>
    /// Gets the border values (per-side width/style and per-corner radius) applied consistently across base, hover, and
    /// disabled states.
    /// </summary>
    public AllyariaBorders Border { get; }

    /// <summary>Gets the base color palette.</summary>
    public AllyariaPalette Palette { get; }

    /// <summary>Gets the color palette used for the disabled state.</summary>
    public AllyariaPalette PaletteDisabled { get; }

    /// <summary>Gets the color palette used for the <c>:hover</c> state.</summary>
    public AllyariaPalette PaletteHover { get; }

    /// <summary>
    /// Gets the spacing values (margins and paddings) applied consistently across base, hover, and disabled states.
    /// </summary>
    public AllyariaSpacing Spacing { get; }

    /// <summary>Gets the base typography settings.</summary>
    public AllyariaTypography Typography { get; }

    /// <summary>Gets the typography settings used for the disabled state.</summary>
    public AllyariaTypography TypographyDisabled { get; }

    /// <summary>Gets the typography settings used for the <c>:hover</c> state.</summary>
    public AllyariaTypography TypographyHover { get; }

    /// <summary>
    /// Builds the full CSS string for this style, concatenating palette, typography, spacing, and border CSS declarations for
    /// the base state.
    /// </summary>
    /// <returns>
    /// A concatenated CSS string that includes base palette, typography, spacing, and border declarations (e.g.,
    /// <c>background-color</c>, <c>color</c>, font properties, margins, paddings, and border properties).
    /// </returns>
    public string ToCss() => string.Concat(Palette.ToCss(), Typography.ToCss(), Spacing.ToCss(), Border.ToCss());

    /// <summary>
    /// Builds the full CSS string for the disabled state, concatenating disabled palette, disabled typography, spacing, and
    /// border CSS declarations.
    /// </summary>
    /// <returns>
    /// A concatenated CSS string that includes disabled palette, disabled typography, spacing, and border declarations.
    /// </returns>
    public string ToCssDisabled()
        => string.Concat(PaletteDisabled.ToCss(), TypographyDisabled.ToCss(), Spacing.ToCss(), Border.ToCss());

    /// <summary>
    /// Builds the full CSS string for the <c>:hover</c> state, concatenating hover palette, hover typography, spacing, and
    /// border CSS declarations.
    /// </summary>
    /// <returns>
    /// A concatenated CSS string that includes hover palette, hover typography, spacing, and border declarations.
    /// </returns>
    public string ToCssHover()
        => string.Concat(PaletteHover.ToCss(), TypographyHover.ToCss(), Spacing.ToCss(), Border.ToCss());
}
