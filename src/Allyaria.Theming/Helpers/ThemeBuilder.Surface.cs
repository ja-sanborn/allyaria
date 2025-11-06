namespace Allyaria.Theming.Helpers;

internal sealed partial class ThemeBuilder
{
    private void CreateSurface(bool isHighContrast)
    {
        ApplyTheme(
            applier: new ThemeColorApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Surface,
                paletteType: PaletteType.Elevation1,
                isVariant: false,
                hasBackground: true,
                isOutline: false
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Surface,
                styleType: StyleType.Margin,
                value: new StyleLength(value: Sizing.Size2)
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Surface,
                styleType: StyleType.Padding,
                value: new StyleLength(value: Sizing.Size3)
            )
        );
    }
}
