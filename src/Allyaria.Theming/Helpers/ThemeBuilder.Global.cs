namespace Allyaria.Theming.Helpers;

public sealed partial class ThemeBuilder
{
    private void CreateGlobal(bool isHighContrast)
    {
        var brand = isHighContrast
            ? _highContrast
            : _brand;

        // Accent color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.Global,
            styleType: StyleType.AccentColor, getColor: palette => palette.AccentColor
        );

        // Background color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.Global,
            styleType: StyleType.BackgroundColor, getColor: palette => palette.BackgroundColor
        );

        // Border color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.Global,
            styleType: StyleType.BorderColor, getColor: palette => palette.BorderColor
        );

        // Caret color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.Global,
            styleType: StyleType.CaretColor, getColor: palette => palette.CaretColor
        );

        // Color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.Global,
            styleType: StyleType.Color, getColor: palette => palette.ForegroundColor
        );

        // Outline color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.Global,
            styleType: StyleType.OutlineColor, getColor: palette => palette.OutlineColor
        );

        // Text decoration color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.Global,
            styleType: StyleType.TextDecorationColor, getColor: palette => palette.TextDecorationColor
        );

        // Font family
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Global)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontFamily),
            value: new StyleString(value: brand.Font.SansSerif)
        );
    }
}
