namespace Allyaria.Theming.Helpers;

public sealed partial class ThemeBuilder
{
    private void CreateGlobalHtml(bool isHighContrast)
    {
        var brand = isHighContrast
            ? _highContrast
            : _brand;

        // Box sizing
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalHtml)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.BoxSizing),
            value: new StyleBoxSizing(kind: StyleBoxSizing.Kind.BorderBox)
        );

        // Font size
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalHtml)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontSize),
            value: new StyleLength(value: Sizing.Size3)
        );

        // Margin
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalHtml)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Margin),
            value: new StyleGroup(type: StyleGroupType.Margin, value: new StyleLength(value: Sizing.Size0))
        );

        // Min-Height
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalHtml)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.MinHeight),
            value: new StyleLength(value: Sizing.Full)
        );

        // Padding
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalHtml)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Padding),
            value: new StyleGroup(type: StyleGroupType.Padding, value: new StyleLength(value: Sizing.Size0))
        );

        // Scroll Behavior
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalHtml)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.ScrollBehavior),
            value: new StyleScrollBehavior(kind: StyleScrollBehavior.Kind.Smooth)
        );

        // Text Size Adjust
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalHtml)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.TextSizeAdjust),
            value: new StyleLength(value: Sizing.Full)
        );

        // Webkit Tab Highlight Color
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalHtml)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.WebkitTapHighlightColor),
            value: new StyleColor(color: Colors.Transparent)
        );

        // Webkit Text Size Adjust
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalHtml)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.WebkitTextSizeAdjust),
            value: new StyleLength(value: Sizing.Full)
        );
    }
}
