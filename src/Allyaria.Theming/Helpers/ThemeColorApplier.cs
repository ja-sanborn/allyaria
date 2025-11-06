namespace Allyaria.Theming.Helpers;

internal sealed class ThemeColorApplier : ThemeApplierBase
{
    private readonly bool _isVariant;
    private readonly PaletteType _paletteType;

    public ThemeColorApplier(ThemeMapper themeMapper,
        bool isHighContrast,
        ComponentType componentType,
        PaletteType paletteType,
        bool isVariant,
        bool hasBackground,
        bool isOutline)
        : base(themeMapper: themeMapper, isHighContrast: isHighContrast, componentType: componentType)
    {
        _isVariant = isVariant;
        _paletteType = paletteType;

        if (hasBackground)
        {
            AddColor(styleType: StyleType.BackgroundColor, getColor: palette => palette.BackgroundColor);
        }

        if (!isOutline)
        {
            AddColor(styleType: StyleType.AccentColor, getColor: palette => palette.AccentColor);
            AddColor(styleType: StyleType.BorderColor, getColor: palette => palette.BorderColor);
            AddColor(styleType: StyleType.CaretColor, getColor: palette => palette.CaretColor);
            AddColor(styleType: StyleType.Color, getColor: palette => palette.ForegroundColor);
            AddColor(styleType: StyleType.TextDecorationColor, getColor: palette => palette.TextDecorationColor);
        }

        AddColor(styleType: StyleType.OutlineColor, getColor: palette => palette.OutlineColor);
    }

    private void AddColor(StyleType styleType, Func<BrandPalette, HexColor?> getColor)
        => AddRange(
            collection: ThemeMapper.GetColors(
                isHighContrast: IsHighContrast,
                isVariant: _isVariant,
                paletteType: _paletteType,
                componentType: ComponentType,
                styleType: styleType,
                getColor: getColor
            )
        );
}
