namespace Allyaria.Theming.Helpers;

/// <summary>
/// Applies color-related styles from a specified <see cref="PaletteType" /> to a given <see cref="ComponentType" />.
/// </summary>
/// <remarks>
///     <para>
///     The <see cref="ThemeColorApplier" /> class generates and adds <see cref="ThemeUpdater" /> instances representing
///     color mappings between <see cref="BrandPalette" /> values and component-level style properties such as
///     <see cref="StyleType.BackgroundColor" />, <see cref="StyleType.Color" />, and <see cref="StyleType.BorderColor" />.
///     </para>
///     <para>
///     It is primarily used by <see cref="ThemeBuilder" /> to construct theming layers that ensure color consistency
///     across component surfaces, text, outlines, and decorations.
///     </para>
/// </remarks>
internal sealed class ThemeColorApplier : ThemeApplierBase
{
    /// <summary>Indicates whether this applier targets variant palettes (e.g., light/dark inversion).</summary>
    private readonly bool _isVariant;

    /// <summary>Represents the <see cref="PaletteType" /> from which colors will be derived.</summary>
    private readonly PaletteType _paletteType;

    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeColorApplier" /> class, configuring color propagation for a specific
    /// component and palette type.
    /// </summary>
    /// <param name="themeMapper">The <see cref="ThemeMapper" /> responsible for resolving color mappings.</param>
    /// <param name="isHighContrast">Indicates whether high-contrast color mapping should be applied.</param>
    /// <param name="componentType">The <see cref="ComponentType" /> to which color styles will be applied.</param>
    /// <param name="paletteType">The <see cref="PaletteType" /> defining which brand color group to use.</param>
    /// <param name="isVariant">Determines whether variant theme colors should be applied (e.g., light/dark inversion).</param>
    /// <param name="hasBackground">Specifies whether the component includes a background color layer.</param>
    /// <param name="isOutline">Specifies whether the component is outline-only (no fill).</param>
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

    /// <summary>Adds a color mapping for the specified <see cref="StyleType" /> using the provided color selector.</summary>
    /// <param name="styleType">The <see cref="StyleType" /> to which the color should apply.</param>
    /// <param name="getColor">A function that extracts a <see cref="HexColor" /> from a <see cref="BrandPalette" />.</param>
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
