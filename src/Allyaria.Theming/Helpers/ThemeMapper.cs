namespace Allyaria.Theming.Helpers;

internal sealed class ThemeMapper
{
    private readonly Brand _brand;

    private readonly Brand _highContrast;

    public ThemeMapper(Brand? brand = null)
    {
        _brand = brand ?? new Brand();
        _highContrast = Brand.CreateHighContrastBrand();
    }

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

    private static BrandPalette GetBrandPalette(BrandState state, ComponentState componentState)
        => componentState switch
        {
            ComponentState.Disabled => state.Disabled,
            ComponentState.Dragged => state.Dragged,
            ComponentState.Focused => state.Focused,
            ComponentState.Hovered => state.Hovered,
            ComponentState.Pressed => state.Pressed,
            ComponentState.Visited => state.Visited,
            _ => state.Default
        };

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
