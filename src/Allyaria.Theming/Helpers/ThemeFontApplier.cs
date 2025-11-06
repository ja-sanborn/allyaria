namespace Allyaria.Theming.Helpers;

internal sealed class ThemeFontApplier : ThemeApplierBase
{
    public ThemeFontApplier(ThemeMapper themeMapper,
        bool isHighContrast,
        ComponentType componentType,
        PaletteType? paletteType = null,
        FontFaceType? fontFace = null,
        string? fontSize = null,
        StyleFontStyle.Kind? fontStyle = null,
        StyleFontWeight.Kind? fontWeight = null,
        string? lineHeight = null,
        string? marginBottom = null,
        StyleTextDecorationLine.Kind? textDecorationLine = null,
        StyleTextDecorationStyle.Kind? textDecorationStyle = null,
        string? textDecorationThickness = null,
        StyleTextTransform.Kind? textTransform = null)
        : base(themeMapper: themeMapper, isHighContrast: isHighContrast, componentType: componentType)
    {
        if (paletteType is not null)
        {
            AddRange(
                collection: new ThemeColorApplier(
                    themeMapper: themeMapper,
                    isHighContrast: isHighContrast,
                    componentType: componentType,
                    paletteType: paletteType.Value,
                    isVariant: true,
                    hasBackground: false,
                    isOutline: false
                )
            );
        }

        if (fontFace is not null)
        {
            Add(
                item: themeMapper.GetFont(
                    isHighContrast: isHighContrast,
                    componentType: componentType,
                    fontType: fontFace.Value
                )
            );
        }

        if (!string.IsNullOrWhiteSpace(value: fontSize))
        {
            Add(item: CreateUpdater(styleType: StyleType.FontSize, value: new StyleLength(value: fontSize)));
        }

        if (fontStyle is not null)
        {
            Add(item: CreateUpdater(styleType: StyleType.FontStyle, value: new StyleFontStyle(kind: fontStyle.Value)));
        }

        if (fontWeight is not null)
        {
            Add(
                item: CreateUpdater(styleType: StyleType.FontWeight, value: new StyleFontWeight(kind: fontWeight.Value))
            );
        }

        if (!string.IsNullOrWhiteSpace(value: lineHeight))
        {
            Add(item: CreateUpdater(styleType: StyleType.LineHeight, value: new StyleLength(value: lineHeight)));
        }

        if (!string.IsNullOrWhiteSpace(value: marginBottom))
        {
            Add(
                item: CreateUpdater(
                    styleType: StyleType.Margin, value: new StyleGroup(
                        type: StyleGroupType.Margin,
                        blockStart: new StyleLength(value: Sizing.Size0),
                        blockEnd: new StyleLength(value: marginBottom),
                        inlineStart: new StyleLength(value: Sizing.Size0),
                        inlineEnd: new StyleLength(value: Sizing.Size0)
                    )
                )
            );

            Add(item: CreateUpdater(styleType: StyleType.Padding, value: new StyleLength(value: Sizing.Size0)));
        }

        if (textDecorationLine is not null)
        {
            Add(
                item: CreateUpdater(
                    styleType: StyleType.TextDecorationLine,
                    value: new StyleTextDecorationLine(kind: textDecorationLine.Value)
                )
            );
        }

        if (textDecorationStyle is not null)
        {
            Add(
                item: CreateUpdater(
                    styleType: StyleType.TextDecorationStyle,
                    value: new StyleTextDecorationStyle(kind: textDecorationStyle.Value)
                )
            );
        }

        if (!string.IsNullOrWhiteSpace(value: textDecorationThickness))
        {
            Add(
                item: CreateUpdater(
                    styleType: StyleType.TextDecorationThickness,
                    value: new StyleLength(value: textDecorationThickness)
                )
            );
        }

        if (textTransform is not null)
        {
            Add(
                item: CreateUpdater(
                    styleType: StyleType.TextTransform, value: new StyleTextTransform(kind: textTransform.Value)
                )
            );
        }
    }
}
