using Allyaria.Theming.Enumerations;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents the root Allyaria theme object that composes borders, spacing, palette variants (light/dark/high-contrast),
/// and component typography. This struct serves as the primary entry point for computing per-component, per-state styles.
/// </summary>
/// <remarks>
/// The theme is composed of immutable sub-structures and supports non-destructive updates via
/// <see
///     cref="Cascade(AryBorders?, ArySpacing?, AryPalette?, AryPalette?, AryPalette?, AryTypography?)" />
/// . Use <see cref="ToStyle(ThemeType, ComponentType, ComponentElevation, ComponentState)" /> to resolve a concrete
/// <see cref="AryStyle" /> for a specific theme type, component type, elevation, and state; then call
/// <see cref="AryStyle.ToCss(string?, bool)" /> to produce CSS.
/// </remarks>
/// <seealso cref="AryStyle" />
/// <seealso cref="AryPaletteVariant" />
/// <seealso cref="AryTypographyArea" />
public readonly record struct AryTheme
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AryTheme" /> struct using default values for all subcomponents.
    /// </summary>
    public AryTheme()
        : this(null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryTheme" /> struct with optional subcomponents. Any parameter left as
    /// <see langword="null" /> falls back to that component’s default.
    /// </summary>
    /// <param name="borders">Optional borders configuration; when <see langword="null" />, defaults are used.</param>
    /// <param name="spacing">Optional spacing configuration; when <see langword="null" />, defaults are used.</param>
    /// <param name="paletteLight">Optional light-mode palette; when <see langword="null" />, defaults are used.</param>
    /// <param name="paletteDark">Optional dark-mode palette; when <see langword="null" />, defaults are used.</param>
    /// <param name="paletteHighContrast">Optional high-contrast palette; when <see langword="null" />, defaults are used.</param>
    /// <param name="typoSurface">Optional surface typography; when <see langword="null" />, defaults are used.</param>
    public AryTheme(AryBorders? borders = null,
        ArySpacing? spacing = null,
        AryPalette? paletteLight = null,
        AryPalette? paletteDark = null,
        AryPalette? paletteHighContrast = null,
        AryTypography? typoSurface = null)
    {
        Borders = borders ?? new AryBorders();
        Spacing = spacing ?? new ArySpacing();

        Palette = new AryPaletteVariant(
            paletteLight,
            paletteDark,
            paletteHighContrast
        );

        Typo = new AryTypographyArea(typoSurface);
    }

    /// <summary>Gets the border configuration applied by the theme.</summary>
    public AryBorders Borders { get; init; }

    /// <summary>Gets the palette variant set (light/dark/high-contrast) used by the theme.</summary>
    public AryPaletteVariant Palette { get; init; }

    /// <summary>Gets the spacing configuration (margins and paddings) used by the theme.</summary>
    public ArySpacing Spacing { get; init; }

    /// <summary>Gets the typography component mapping used by the theme.</summary>
    public AryTypographyArea Typo { get; init; }

    /// <summary>
    /// Returns a new <see cref="AryTheme" /> with optional component overrides applied. Any argument left
    /// <see langword="null" /> keeps the existing value.
    /// </summary>
    /// <param name="borders">Optional replacement for <see cref="Borders" />.</param>
    /// <param name="spacing">Optional replacement for <see cref="Spacing" />.</param>
    /// <param name="paletteLight">Optional override for the light-mode palette within <see cref="Palette" />.</param>
    /// <param name="paletteDark">Optional override for the dark-mode palette within <see cref="Palette" />.</param>
    /// <param name="paletteHighContrast">Optional override for the high-contrast palette within <see cref="Palette" />.</param>
    /// <param name="typoSurface">Optional override for the surface typography within <see cref="Typo" />.</param>
    /// <returns>A new <see cref="AryTheme" /> with supplied overrides applied.</returns>
    /// <example>
    ///     <code>
    /// var theme = new AryTheme();
    /// var updated = theme.Cascade(paletteDark: new AryPalette(backgroundColor: "#111", foregroundColor: "#eee"));
    /// </code>
    /// </example>
    public AryTheme Cascade(AryBorders? borders = null,
        ArySpacing? spacing = null,
        AryPalette? paletteLight = null,
        AryPalette? paletteDark = null,
        AryPalette? paletteHighContrast = null,
        AryTypography? typoSurface = null)
        => this with
        {
            Borders = borders ?? Borders,
            Palette = Palette.Cascade(paletteLight, paletteDark, paletteHighContrast),
            Typo = Typo.Cascade(typoSurface),
            Spacing = spacing ?? Spacing
        };

    /// <summary>
    /// Produces a CSS string for the resolved style. When state is Focused, the emitted border uses the focus presentation
    /// (thicker + dashed) while colors come from the focused palette for proper contrast.
    /// </summary>
    /// <param name="themeType">The active theme type (light, dark, or high-contrast).</param>
    /// <param name="componentType">The component requesting styles (e.g., content surface).</param>
    /// <param name="elevation">The elevation level to use; defaults to <see cref="ComponentElevation.Mid" />.</param>
    /// <param name="state">The component state to use; defaults to <see cref="ComponentState.Default" />.</param>
    /// <param name="varPrefix">
    /// Optional prefix for CSS custom properties; when provided, property names are emitted as
    /// <c>--{varPrefix}-[propertyName]</c>.
    /// </param>
    /// <returns>A concatenated CSS declaration string representing the resolved style.</returns>
    public string ToCss(ThemeType themeType,
        ComponentType componentType,
        ComponentElevation elevation = ComponentElevation.Mid,
        ComponentState state = ComponentState.Default,
        string? varPrefix = "")
        => ToStyle(themeType, componentType, elevation, state).ToCss(varPrefix, state is ComponentState.Focused);

    /// <summary>
    /// Resolves a concrete <see cref="AryStyle" /> for a specific theme type, component type, elevation, and state.
    /// </summary>
    /// <param name="themeType">The active theme type (light, dark, or high-contrast).</param>
    /// <param name="componentType">The component requesting styles.</param>
    /// <param name="elevation">The elevation level to use; defaults to <see cref="ComponentElevation.Mid" />.</param>
    /// <param name="state">The component state to use; defaults to <see cref="ComponentState.Default" />.</param>
    /// <returns>
    /// An <see cref="AryStyle" /> instance constructed from the theme’s palette, typography, spacing, and borders appropriate
    /// to the provided inputs.
    /// </returns>
    public AryStyle ToStyle(ThemeType themeType,
        ComponentType componentType,
        ComponentElevation elevation = ComponentElevation.Mid,
        ComponentState state = ComponentState.Default)
        => new(
            Palette.ToPalette(
                themeType,
                elevation,
                state
            ),
            Typo.ToTypography(componentType),
            Spacing,
            Borders
        );
}
