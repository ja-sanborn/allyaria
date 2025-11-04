namespace Allyaria.Theming.Helpers;

public sealed partial class ThemeBuilder
{
    private void CreateSurface(bool isHighContrast)
    {
        // Background color
        ApplyFromBrand(
            paletteType: PaletteType.Elevation1,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.Surface,
            styleType: StyleType.BackgroundColor,
            getColor: palette => palette.BackgroundColor
        );

        // Caret color
        ApplyFromBrand(
            paletteType: PaletteType.Elevation1,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.Surface,
            styleType: StyleType.CaretColor,
            getColor: palette => palette.CaretColor
        );

        // Color
        ApplyFromBrand(
            paletteType: PaletteType.Elevation1,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.Surface,
            styleType: StyleType.Color,
            getColor: palette => palette.ForegroundColor
        );

        // Text decoration color
        ApplyFromBrand(
            paletteType: PaletteType.Elevation1,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.Surface,
            styleType: StyleType.TextDecorationColor,
            getColor: palette => palette.TextDecorationColor
        );

        // Margin
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Surface)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Margin),
            value: new StyleGroup(type: StyleGroupType.Margin, value: new StyleLength(value: Sizing.Size2))
        );

        // Padding
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Surface)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Padding),
            value: new StyleGroup(type: StyleGroupType.Padding, value: new StyleLength(value: Sizing.Size3))
        );
    }
}
