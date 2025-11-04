namespace Allyaria.Theming.Helpers;

public sealed partial class ThemeBuilder
{
    private void CreateGlobalBody(bool isHighContrast)
    {
        // Accent color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.GlobalBody,
            styleType: StyleType.AccentColor,
            getColor: palette => palette.AccentColor
        );

        // Background color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.GlobalBody,
            styleType: StyleType.BackgroundColor,
            getColor: palette => palette.BackgroundColor
        );

        // Border color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.GlobalBody,
            styleType: StyleType.BorderColor,
            getColor: palette => palette.BorderColor
        );

        // Caret color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.GlobalBody,
            styleType: StyleType.CaretColor,
            getColor: palette => palette.CaretColor
        );

        // Color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.GlobalBody,
            styleType: StyleType.Color,
            getColor: palette => palette.ForegroundColor
        );

        // Outline color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.GlobalBody,
            styleType: StyleType.OutlineColor,
            getColor: palette => palette.OutlineColor
        );

        // Text decoration color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.GlobalBody,
            styleType: StyleType.TextDecorationColor,
            getColor: palette => palette.TextDecorationColor
        );

        // Font family
        var brand = isHighContrast
            ? _highContrast
            : _brand;

        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalBody)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontFamily),
            value: new StyleString(value: brand.Font.SansSerif)
        );

        // Line Height
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalBody)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.LineHeight),
            value: new StyleLength(value: "1.5")
        );

        // Margin
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalBody)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Margin),
            value: new StyleGroup(type: StyleGroupType.Margin, value: new StyleLength(value: Sizing.Size0))
        );

        // Min-Height
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalBody)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.MinHeight),
            value: new StyleLength(value: Sizing.Full)
        );

        // Padding
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalBody)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Padding),
            value: new StyleGroup(type: StyleGroupType.Padding, value: new StyleLength(value: Sizing.Size0))
        );

        // Overflow-x
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalBody)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.OverflowX),
            value: new StyleOverflow(kind: StyleOverflow.Kind.Clip)
        );
    }

    private void CreateGlobalFocus(bool isHighContrast)
    {
        // Outline color
        ApplyFromBrand(
            paletteType: PaletteType.Surface,
            isHighContrast: isHighContrast,
            isVariant: false,
            componentType: ComponentType.GlobalFocus,
            styleType: StyleType.OutlineColor,
            getColor: palette => palette.AccentColor
        );

        // Outline offset
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalFocus)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.OutlineOffset),
            value: new StyleNumber(value: Sizing.Size1)
        );

        // Outline style
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalFocus)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.OutlineStyle),
            value: new StyleBorderOutlineStyle(kind: StyleBorderOutlineStyle.Kind.Solid)
        );

        // Outline width
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.GlobalFocus)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.OutlineWidth),
            value: new StyleNumber(value: Sizing.Thick)
        );
    }

    private void CreateGlobalHtml(bool isHighContrast)
    {
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
