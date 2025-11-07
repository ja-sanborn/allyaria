namespace Allyaria.Theming.Helpers;

/// <summary>
/// Provides mapping utilities between <see cref="Brand" /> configurations and Allyaria theming constructs, producing
/// <see cref="ThemeUpdater" /> instances used to construct theme components.
/// </summary>
/// <remarks>
///     <para>
///     The <see cref="ThemeMapper" /> acts as the bridge between brand-level definitions (fonts, palettes, variants) and
///     runtime theme generation.
///     </para>
///     <para>
///     It handles color palette resolution, font face selection, and state-based color derivation for light, dark, and
///     high-contrast variants.
///     </para>
/// </remarks>
internal sealed class ThemeMapper
{
    /// <summary>The primary <see cref="Brand" /> definition used for standard light and dark modes.</summary>
    private readonly Brand _brand;

    /// <summary>The dedicated high-contrast <see cref="Brand" /> variant used when accessibility modes are enabled.</summary>
    private readonly Brand _highContrast;

    /// <summary>Initializes a new instance of the <see cref="ThemeMapper" /> class.</summary>
    /// <param name="brand">An optional <see cref="Brand" /> instance. If omitted, a default <see cref="Brand" /> is created.</param>
    public ThemeMapper(Brand? brand = null)
    {
        _brand = brand ?? new Brand();
        _highContrast = Brand.CreateHighContrastBrand();
    }

    /// <summary>
    /// Builds a mapping between <see cref="BrandPalette" /> instances and <see cref="ComponentState" /> values.
    /// </summary>
    /// <param name="state">The <see cref="BrandState" /> defining the palette states.</param>
    /// <returns>
    /// An array of tuples pairing <see cref="BrandPalette" /> with its associated <see cref="ComponentState" />.
    /// </returns>
    private static (BrandPalette Palette, ComponentState ComponentState)[] BuildPaletteMap(BrandState state)
        =>
        [
            (state.Default, ComponentState.Default),
            (state.Disabled, ComponentState.Disabled),
            (state.Dragged, ComponentState.Dragged),
            (state.Focused, ComponentState.Focused),
            (state.Hovered, ComponentState.Hovered),
            (state.Pressed, ComponentState.Pressed),
            (state.Visited, ComponentState.Visited)
        ];

    /// <summary>Builds a mapping of light and dark theme variants from the given <see cref="Brand" />.</summary>
    /// <param name="brand">The <see cref="Brand" /> from which to derive color themes.</param>
    /// <param name="isHighContrast">Indicates whether high-contrast variants should be mapped.</param>
    /// <returns>
    /// An array of tuples pairing <see cref="BrandTheme" /> with its corresponding <see cref="ThemeType" />.
    /// </returns>
    private static (BrandTheme Theme, ThemeType ThemeType)[] BuildThemeMap(Brand brand, bool isHighContrast)
        =>
        [
            (brand.Variant.Dark, isHighContrast
                ? ThemeType.HighContrastDark
                : ThemeType.Dark),
            (brand.Variant.Light, isHighContrast
                ? ThemeType.HighContrastLight
                : ThemeType.Light)
        ];

    /// <summary>
    /// Builds a mapping for light and dark theme <em>variants</em> derived from the base <see cref="Brand" />.
    /// </summary>
    /// <param name="brand">The <see cref="Brand" /> used for variant mapping.</param>
    /// <param name="isHighContrast">Specifies whether high-contrast mappings should be included.</param>
    /// <returns>An array of tuples mapping <see cref="BrandTheme" /> variants to <see cref="ThemeType" /> values.</returns>
    private static (BrandTheme Theme, ThemeType ThemeType)[] BuildThemeVariantMap(Brand brand, bool isHighContrast)
        =>
        [
            (brand.Variant.DarkVariant, isHighContrast
                ? ThemeType.HighContrastDark
                : ThemeType.Dark),
            (brand.Variant.LightVariant, isHighContrast
                ? ThemeType.HighContrastLight
                : ThemeType.Light)
        ];

    /// <summary>
    /// Retrieves a <see cref="BrandState" /> from a <see cref="BrandTheme" /> based on the specified
    /// <see cref="PaletteType" />.
    /// </summary>
    /// <param name="theme">The <see cref="BrandTheme" /> to search.</param>
    /// <param name="paletteType">The <see cref="PaletteType" /> indicating which palette to return.</param>
    /// <returns>A <see cref="BrandState" /> corresponding to the given palette type.</returns>
    private static BrandState GetBrandState(BrandTheme theme, PaletteType paletteType)
        => paletteType switch
        {
            PaletteType.Elevation1 => theme.Elevation1,
            PaletteType.Elevation2 => theme.Elevation2,
            PaletteType.Elevation3 => theme.Elevation3,
            PaletteType.Elevation4 => theme.Elevation4,
            PaletteType.Elevation5 => theme.Elevation5,
            PaletteType.Error => theme.Error,
            PaletteType.Info => theme.Info,
            PaletteType.Primary => theme.Primary,
            PaletteType.Secondary => theme.Secondary,
            PaletteType.Success => theme.Success,
            PaletteType.Surface => theme.Surface,
            PaletteType.Tertiary => theme.Tertiary,
            PaletteType.Warning => theme.Warning,
            _ => theme.Surface
        };

    /// <summary>
    /// Generates a list of <see cref="ThemeUpdater" /> instances representing color mappings for a component type.
    /// </summary>
    /// <param name="isHighContrast">Indicates whether high-contrast colors should be used.</param>
    /// <param name="isVariant">Specifies whether to use variant color mappings.</param>
    /// <param name="paletteType">The <see cref="PaletteType" /> indicating which color family to map.</param>
    /// <param name="componentType">The <see cref="ComponentType" /> representing the styled component.</param>
    /// <param name="styleType">The <see cref="StyleType" /> to which the color applies.</param>
    /// <param name="getColor">
    /// A delegate selecting which <see cref="HexColor" /> to extract from a <see cref="BrandPalette" />
    /// .
    /// </param>
    /// <returns>A list of <see cref="ThemeUpdater" /> instances for applying the derived colors.</returns>
    public IReadOnlyList<ThemeUpdater> GetColors(bool isHighContrast,
        bool isVariant,
        PaletteType paletteType,
        ComponentType componentType,
        StyleType styleType,
        Func<BrandPalette, HexColor?> getColor)
    {
        var list = new List<ThemeUpdater>();

        var brand = isHighContrast
            ? _highContrast
            : _brand;

        var themeMap = isVariant
            ? BuildThemeVariantMap(brand: brand, isHighContrast: isHighContrast)
            : BuildThemeMap(brand: brand, isHighContrast: isHighContrast);

        foreach ((var themeItem, var themeType) in themeMap)
        {
            var brandState = GetBrandState(theme: themeItem, paletteType: paletteType);
            var paletteMap = BuildPaletteMap(state: brandState);

            foreach ((var palette, var state) in paletteMap)
            {
                var color = getColor(arg: palette);

                if (color is not null)
                {
                    list.Add(
                        item: new ThemeUpdater(
                            Navigator: ThemeNavigator.Initialize
                                .SetComponentTypes(componentType)
                                .SetThemeTypes(themeType)
                                .SetComponentStates(state)
                                .SetStyleTypes(styleType),
                            Value: new StyleColor(color: color.Value)
                        )
                    );
                }
            }
        }

        return list;
    }

    /// <summary>
    /// Creates a <see cref="ThemeUpdater" /> that maps the appropriate font family from the current <see cref="Brand" /> or
    /// its high-contrast counterpart.
    /// </summary>
    /// <param name="isHighContrast">Specifies whether to use high-contrast font mapping.</param>
    /// <param name="componentType">The <see cref="ComponentType" /> for which the font applies.</param>
    /// <param name="fontType">The <see cref="FontFaceType" /> defining which font family to use.</param>
    /// <returns>A <see cref="ThemeUpdater" /> containing a <see cref="StyleString" /> with the selected font family.</returns>
    public ThemeUpdater GetFont(bool isHighContrast, ComponentType componentType, FontFaceType fontType)
    {
        var brand = isHighContrast
            ? _highContrast
            : _brand;

        var fontFace = fontType switch
        {
            FontFaceType.Monospace => brand.Font.Monospace,
            FontFaceType.SansSerif => brand.Font.SansSerif,
            FontFaceType.Serif => brand.Font.Serif,
            _ => brand.Font.SansSerif
        };

        return new ThemeUpdater(
            Navigator: ThemeNavigator.Initialize
                .SetComponentTypes(componentType)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontFamily),
            Value: new StyleString(value: fontFace)
        );
    }
}
