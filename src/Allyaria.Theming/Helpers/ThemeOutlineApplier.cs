namespace Allyaria.Theming.Helpers;

internal sealed class ThemeOutlineApplier : ThemeApplierBase
{
    public ThemeOutlineApplier(ThemeMapper themeMapper,
        bool isHighContrast,
        ComponentType componentType,
        PaletteType paletteType)
        : base(themeMapper: themeMapper, isHighContrast: isHighContrast, componentType: componentType)
    {
        AddRange(
            collection: new ThemeColorApplier(
                themeMapper: themeMapper,
                isHighContrast: isHighContrast,
                componentType: componentType,
                paletteType: paletteType,
                isVariant: true,
                hasBackground: false,
                isOutline: true
            )
        );

        Add(item: CreateUpdater(styleType: StyleType.OutlineOffset, value: new StyleNumber(value: Sizing.Size1)));

        Add(
            item: CreateUpdater(
                styleType: StyleType.OutlineStyle,
                value: new StyleBorderOutlineStyle(kind: StyleBorderOutlineStyle.Kind.Solid)
            )
        );

        Add(item: CreateUpdater(styleType: StyleType.OutlineWidth, value: new StyleNumber(value: Sizing.Thick)));
    }
}
