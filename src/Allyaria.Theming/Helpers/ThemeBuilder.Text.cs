namespace Allyaria.Theming.Helpers;

public sealed partial class ThemeBuilder
{
    private void CreateHeading1(bool isHighContrast)
    {
        // Font size
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading1)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontSize),
            value: new StyleLength(value: Sizing.RelativeLarge4)
        );

        // Font weight
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading1)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontWeight),
            value: new StyleFontWeight(kind: StyleFontWeight.Kind.Weight700)
        );

        // Line Height
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading1)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.LineHeight),
            value: new StyleLength(value: "1.2")
        );

        // Margin
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading1)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Margin),
            value: new StyleGroup(
                type: StyleGroupType.Margin,
                blockStart: new StyleLength(value: Sizing.Size0),
                blockEnd: new StyleLength(value: Sizing.RelativeLarge1),
                inlineStart: new StyleLength(value: Sizing.Size0),
                inlineEnd: new StyleLength(value: Sizing.Size0)
            )
        );

        // Padding
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading1)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Padding),
            value: new StyleGroup(type: StyleGroupType.Padding, value: new StyleLength(value: Sizing.Size0))
        );
    }

    private void CreateHeading2(bool isHighContrast)
    {
        // Font size
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading2)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontSize),
            value: new StyleLength(value: Sizing.RelativeLarge3)
        );

        // Font weight
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading2)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontWeight),
            value: new StyleFontWeight(kind: StyleFontWeight.Kind.Weight700)
        );

        // Line Height
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading2)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.LineHeight),
            value: new StyleLength(value: "1.25")
        );

        // Margin
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading2)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Margin),
            value: new StyleGroup(
                type: StyleGroupType.Margin,
                blockStart: new StyleLength(value: Sizing.Size0),
                blockEnd: new StyleLength(value: Sizing.Relative),
                inlineStart: new StyleLength(value: Sizing.Size0),
                inlineEnd: new StyleLength(value: Sizing.Size0)
            )
        );

        // Padding
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading2)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Padding),
            value: new StyleGroup(type: StyleGroupType.Padding, value: new StyleLength(value: Sizing.Size0))
        );
    }

    private void CreateHeading3(bool isHighContrast)
    {
        // Font size
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading3)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontSize),
            value: new StyleLength(value: Sizing.RelativeLarge2)
        );

        // Font weight
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading3)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontWeight),
            value: new StyleFontWeight(kind: StyleFontWeight.Kind.Weight600)
        );

        // Line Height
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading3)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.LineHeight),
            value: new StyleLength(value: "1.3")
        );

        // Margin
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading3)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Margin),
            value: new StyleGroup(
                type: StyleGroupType.Margin,
                blockStart: new StyleLength(value: Sizing.Size0),
                blockEnd: new StyleLength(value: Sizing.RelativeSmall2),
                inlineStart: new StyleLength(value: Sizing.Size0),
                inlineEnd: new StyleLength(value: Sizing.Size0)
            )
        );

        // Padding
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading3)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Padding),
            value: new StyleGroup(type: StyleGroupType.Padding, value: new StyleLength(value: Sizing.Size0))
        );
    }

    private void CreateHeading4(bool isHighContrast)
    {
        // Font size
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading4)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontSize),
            value: new StyleLength(value: Sizing.RelativeLarge1)
        );

        // Font weight
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading4)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontWeight),
            value: new StyleFontWeight(kind: StyleFontWeight.Kind.Weight600)
        );

        // Line Height
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading4)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.LineHeight),
            value: new StyleLength(value: "1.4")
        );

        // Margin
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading4)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Margin),
            value: new StyleGroup(
                type: StyleGroupType.Margin,
                blockStart: new StyleLength(value: Sizing.Size0),
                blockEnd: new StyleLength(value: Sizing.RelativeSmall3),
                inlineStart: new StyleLength(value: Sizing.Size0),
                inlineEnd: new StyleLength(value: Sizing.Size0)
            )
        );

        // Padding
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading4)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Padding),
            value: new StyleGroup(type: StyleGroupType.Padding, value: new StyleLength(value: Sizing.Size0))
        );
    }

    private void CreateHeading5(bool isHighContrast)
    {
        // Font size
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading5)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontSize),
            value: new StyleLength(value: Sizing.Relative)
        );

        // Font weight
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading5)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontWeight),
            value: new StyleFontWeight(kind: StyleFontWeight.Kind.Weight600)
        );

        // Line Height
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading5)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.LineHeight),
            value: new StyleLength(value: "1.5")
        );

        // Margin
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading5)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Margin),
            value: new StyleGroup(
                type: StyleGroupType.Margin,
                blockStart: new StyleLength(value: Sizing.Size0),
                blockEnd: new StyleLength(value: Sizing.RelativeSmall4),
                inlineStart: new StyleLength(value: Sizing.Size0),
                inlineEnd: new StyleLength(value: Sizing.Size0)
            )
        );

        // Padding
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading5)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Padding),
            value: new StyleGroup(type: StyleGroupType.Padding, value: new StyleLength(value: Sizing.Size0))
        );
    }

    private void CreateHeading6(bool isHighContrast)
    {
        // Font size
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading6)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontSize),
            value: new StyleLength(value: Sizing.RelativeSmall1)
        );

        // Font weight
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading6)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontWeight),
            value: new StyleFontWeight(kind: StyleFontWeight.Kind.Weight500)
        );

        // Line Height
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading6)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.LineHeight),
            value: new StyleLength(value: "1.5")
        );

        // Margin
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading6)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Margin),
            value: new StyleGroup(
                type: StyleGroupType.Margin,
                blockStart: new StyleLength(value: Sizing.Size0),
                blockEnd: new StyleLength(value: Sizing.RelativeSmall4),
                inlineStart: new StyleLength(value: Sizing.Size0),
                inlineEnd: new StyleLength(value: Sizing.Size0)
            )
        );

        // Padding
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Heading6)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Padding),
            value: new StyleGroup(type: StyleGroupType.Padding, value: new StyleLength(value: Sizing.Size0))
        );
    }

    private void CreateLink(bool isHighContrast)
    {
        // Caret color
        ApplyFromBrand(
            paletteType: PaletteType.Primary,
            isHighContrast: isHighContrast,
            isVariant: true,
            componentType: ComponentType.Link,
            styleType: StyleType.CaretColor,
            getColor: palette => palette.CaretColor
        );

        // Color
        ApplyFromBrand(
            paletteType: PaletteType.Primary,
            isHighContrast: isHighContrast,
            isVariant: true,
            componentType: ComponentType.Link,
            styleType: StyleType.Color,
            getColor: palette => palette.ForegroundColor
        );

        // Text decoration color
        ApplyFromBrand(
            paletteType: PaletteType.Primary,
            isHighContrast: isHighContrast,
            isVariant: true,
            componentType: ComponentType.Link,
            styleType: StyleType.TextDecorationColor,
            getColor: palette => palette.TextDecorationColor
        );

        // Text Decoration Line
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Link)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.TextDecorationLine),
            value: new StyleTextDecorationLine(kind: StyleTextDecorationLine.Kind.Underline)
        );

        // Text Decoration Style
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Link)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.TextDecorationStyle),
            value: new StyleTextDecorationStyle(kind: StyleTextDecorationStyle.Kind.Solid)
        );

        // Text Decoration Thickness
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Link)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.TextDecorationThickness),
            value: new StyleLength(value: Sizing.Thin)
        );
    }

    private void CreateText(bool isHighContrast)
    {
        // Font size
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Text)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.FontSize),
            value: new StyleLength(value: Sizing.Relative)
        );

        // Line Height
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Text)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.LineHeight),
            value: new StyleLength(value: "1.5")
        );

        // Margin
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Text)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Margin),
            value: new StyleGroup(
                type: StyleGroupType.Margin,
                blockStart: new StyleLength(value: Sizing.Size0),
                blockEnd: new StyleLength(value: Sizing.Relative),
                inlineStart: new StyleLength(value: Sizing.Size0),
                inlineEnd: new StyleLength(value: Sizing.Size0)
            )
        );

        // Padding
        _theme.Set(
            navigator: ThemeNavigator.Initialize
                .SetComponentTypes(ComponentType.Text)
                .SetContrastThemeTypes(isHighContrast: isHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(StyleType.Padding),
            value: new StyleGroup(type: StyleGroupType.Padding, value: new StyleLength(value: Sizing.Size0))
        );
    }
}
