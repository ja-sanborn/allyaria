namespace Allyaria.Theming.Helpers;

public sealed class ThemeBuilder
{
    private Brand _brand;
    private readonly Brand _highContrast = Brand.CreateHighContrastBrand();
    private bool _isReady;
    private Theme _theme = new();

    private void ApplyFromBrand(Brand brand,
        PaletteType paletteType,
        bool isVariant,
        ComponentType componentType,
        StyleType styleType,
        Func<BrandPalette, HexColor?> getColor)
    {
        var themeMap = isVariant
            ? BuildThemeVariantMap(brand: brand)
            : BuildThemeMap(brand: brand);

        foreach ((var theme, var themeType) in themeMap)
        {
            var brandState = GetBrandState(theme: theme, paletteType: paletteType);
            var paletteMap = BuildPaletteMap(state: brandState);

            foreach ((var palette, var state) in paletteMap)
            {
                var color = getColor(arg: palette);

                if (color is not null)
                {
                    _theme.Set(
                        navigator: ThemeNavigator.Initialize
                            .SetComponentTypes(componentType)
                            .SetThemeTypes(themeType)
                            .SetComponentStates(state)
                            .SetStyleTypes(styleType),
                        value: new StyleColor(color: color.Value)
                    );
                }
            }
        }
    }

    public Theme Build()
    {
        if (!_isReady)
        {
            Create();
        }

        var theme = _theme;

        _brand = new Brand();
        _theme = new Theme();
        _isReady = false;

        return theme;
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

    private static (BrandTheme Theme, ThemeType ThemeType)[] BuildThemeMap(Brand brand)
        =>
        [
            (brand.Variant.Dark, ThemeType.Dark),
            (brand.Variant.Light, ThemeType.Light)
        ];

    private static (BrandTheme Theme, ThemeType ThemeType)[] BuildThemeVariantMap(Brand brand)
        =>
        [
            (brand.Variant.DarkVariant, ThemeType.Dark),
            (brand.Variant.LightVariant, ThemeType.Light)
        ];

    public ThemeBuilder Create(Brand? brand = null)
    {
        _brand = brand ?? new Brand();
        _theme = new Theme();

        ApplyFromBrand(
            brand: _brand,
            paletteType: PaletteType.Surface,
            isVariant: false,
            componentType: ComponentType.Global,
            styleType: StyleType.BackgroundColor,
            getColor: palette => palette.BackgroundColor
        );

        ApplyFromBrand(
            brand: _highContrast,
            paletteType: PaletteType.Surface,
            isVariant: false,
            componentType: ComponentType.Global,
            styleType: StyleType.BackgroundColor,
            getColor: palette => palette.BackgroundColor
        );

        _isReady = true;

        return this;
    }

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

    public ThemeBuilder Set(ThemeNavigator navigator, IStyleValue? value)
    {
        if (navigator.ThemeTypes.Contains(value: ThemeType.System))
        {
            throw new AryArgumentException(message: "System theme cannot be set directly.", argName: nameof(value));
        }

        if (navigator.ComponentStates.Contains(value: ComponentState.Hidden) ||
            navigator.ComponentStates.Contains(value: ComponentState.ReadOnly))
        {
            throw new AryArgumentException(
                message: "Hidden and read-only states cannot be set directly.", argName: nameof(value)
            );
        }

        if (navigator.ThemeTypes.Contains(value: ThemeType.HighContrastDark) ||
            navigator.ThemeTypes.Contains(value: ThemeType.HighContrastLight))
        {
            throw new AryArgumentException(message: "Cannot alter High Contrast themes.", argName: nameof(value));
        }

        if (navigator.ComponentStates.Contains(value: ComponentState.Focused) &&
            (navigator.StyleTypes.Contains(value: StyleType.OutlineStyle) ||
                navigator.StyleTypes.Contains(value: StyleType.OutlineWidth)))
        {
            throw new AryArgumentException(
                message: "Cannot change focused outline style or width.", argName: nameof(value)
            );
        }

        _theme = _theme.Set(navigator: navigator, value: value);

        return this;
    }
}
