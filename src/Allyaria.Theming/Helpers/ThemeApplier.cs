namespace Allyaria.Theming.Helpers;

internal sealed class ThemeApplier : ThemeApplierBase
{
    public ThemeApplier(ThemeMapper themeMapper,
        bool isHighContrast,
        ComponentType componentType,
        StyleType styleType,
        IStyleValue value)
        : base(themeMapper: themeMapper, isHighContrast: isHighContrast, componentType: componentType)
        => Add(item: CreateUpdater(styleType: styleType, value: value));
}
