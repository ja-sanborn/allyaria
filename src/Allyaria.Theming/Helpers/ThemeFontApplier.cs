namespace Allyaria.Theming.Helpers;

/// <summary>
/// Applies font, text, and spacing-related styles to a given <see cref="ComponentType" /> using Allyaria theming
/// conventions.
/// </summary>
/// <remarks>
///     <para>
///     The <see cref="ThemeFontApplier" /> class allows for applying typography settings such as font face, size, weight,
///     line height, margin, and text decoration.
///     </para>
///     <para>
///     It also supports color inheritance from the specified <see cref="PaletteType" /> when applicable, ensuring that
///     font styling remains consistent across brand variants and accessibility modes.
///     </para>
///     <para>
///     This class is used by <see cref="ThemeBuilder" /> to configure typography for headings, paragraphs, links, and
///     global text elements.
///     </para>
/// </remarks>
internal sealed class ThemeFontApplier : ThemeApplierBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeFontApplier" /> class with optional typography and color parameters.
    /// </summary>
    /// <param name="themeMapper">The <see cref="ThemeMapper" /> instance used for theme resolution.</param>
    /// <param name="isHighContrast">Specifies whether the configuration targets high-contrast themes.</param>
    /// <param name="componentType">The <see cref="ComponentType" /> representing the element being styled.</param>
    /// <param name="paletteType">
    /// Optional. The <see cref="PaletteType" /> defining which color family to use for the text element. If provided, colors
    /// are applied through a <see cref="ThemeColorApplier" />.
    /// </param>
    /// <param name="fontFace">
    /// Optional. The <see cref="FontFaceType" /> identifying which font family to apply. If not provided, defaults to the
    /// brand's standard font.
    /// </param>
    /// <param name="fontSize">Optional. The font size (e.g., "1rem", "16px").</param>
    /// <param name="fontStyle">Optional. The <see cref="StyleFontStyle.Kind" /> indicating italic or normal style.</param>
    /// <param name="fontWeight">Optional. The <see cref="StyleFontWeight.Kind" /> indicating font weight (e.g., 400, 700).</param>
    /// <param name="lineHeight">Optional. The line height value, typically expressed as a unitless number or CSS length.</param>
    /// <param name="marginBottom">
    /// Optional. The bottom margin for typographic spacing, typically applied to headings or
    /// paragraphs.
    /// </param>
    /// <param name="textDecorationLine">
    /// Optional. The <see cref="StyleTextDecorationLine.Kind" /> defining which text decoration line (e.g., underline).
    /// </param>
    /// <param name="textDecorationStyle">
    /// Optional. The <see cref="StyleTextDecorationStyle.Kind" /> specifying the decoration style (solid, dotted, etc.).
    /// </param>
    /// <param name="textDecorationThickness">Optional. The CSS thickness value (e.g., "2px") for text decorations.</param>
    /// <param name="textTransform">
    /// Optional. The <see cref="StyleTextTransform.Kind" /> specifying text casing (uppercase, lowercase, etc.).
    /// </param>
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
        // Apply palette-based colors (optional)
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

        // Apply font family
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

        // Apply font size
        if (!string.IsNullOrWhiteSpace(value: fontSize))
        {
            Add(item: CreateUpdater(styleType: StyleType.FontSize, value: new StyleLength(value: fontSize)));
        }

        // Apply font style (italic/normal)
        if (fontStyle is not null)
        {
            Add(item: CreateUpdater(styleType: StyleType.FontStyle, value: new StyleFontStyle(kind: fontStyle.Value)));
        }

        // Apply font weight (e.g., 400, 700)
        if (fontWeight is not null)
        {
            Add(
                item: CreateUpdater(styleType: StyleType.FontWeight, value: new StyleFontWeight(kind: fontWeight.Value))
            );
        }

        // Apply line height
        if (!string.IsNullOrWhiteSpace(value: lineHeight))
        {
            Add(item: CreateUpdater(styleType: StyleType.LineHeight, value: new StyleLength(value: lineHeight)));
        }

        // Apply bottom margin and padding reset
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

        // Apply text decoration line (e.g., underline)
        if (textDecorationLine is not null)
        {
            Add(
                item: CreateUpdater(
                    styleType: StyleType.TextDecorationLine,
                    value: new StyleTextDecorationLine(kind: textDecorationLine.Value)
                )
            );
        }

        // Apply text decoration style (e.g., solid, dotted)
        if (textDecorationStyle is not null)
        {
            Add(
                item: CreateUpdater(
                    styleType: StyleType.TextDecorationStyle,
                    value: new StyleTextDecorationStyle(kind: textDecorationStyle.Value)
                )
            );
        }

        // Apply text decoration thickness
        if (!string.IsNullOrWhiteSpace(value: textDecorationThickness))
        {
            Add(
                item: CreateUpdater(
                    styleType: StyleType.TextDecorationThickness,
                    value: new StyleLength(value: textDecorationThickness)
                )
            );
        }

        // Apply text transform (uppercase, lowercase, capitalize)
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
