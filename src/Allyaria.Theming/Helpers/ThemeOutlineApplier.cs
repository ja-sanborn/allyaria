namespace Allyaria.Theming.Helpers;

/// <summary>
/// Applies outline-related styles for focusable or interactive components within the Allyaria theming system.
/// </summary>
/// <remarks>
///     <para>
///     The <see cref="ThemeOutlineApplier" /> constructs outline configurations — including color, style, width, and
///     offset — based on brand-defined palettes and accessibility rules.
///     </para>
///     <para>
///     It ensures that focus indicators meet accessibility contrast requirements and visual clarity standards, especially
///     in high-contrast or system accessibility modes.
///     </para>
///     <para>
///     Typically used within <see cref="ThemeBuilder" /> to define focus outlines for components like links, inputs, and
///     interactive surfaces.
///     </para>
/// </remarks>
internal sealed class ThemeOutlineApplier : ThemeApplierBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeOutlineApplier" /> class, creating a focus outline with color and
    /// style derived from the specified <see cref="PaletteType" />.
    /// </summary>
    /// <param name="themeMapper">The <see cref="ThemeMapper" /> instance used to retrieve brand color mappings.</param>
    /// <param name="isHighContrast">Specifies whether this applier should operate in high-contrast mode.</param>
    /// <param name="componentType">The <see cref="ComponentType" /> representing the component being styled.</param>
    /// <param name="paletteType">The <see cref="PaletteType" /> defining which color group to use for outlines.</param>
    public ThemeOutlineApplier(ThemeMapper themeMapper,
        bool isHighContrast,
        ComponentType componentType,
        PaletteType paletteType)
        : base(themeMapper: themeMapper, isHighContrast: isHighContrast, componentType: componentType)
    {
        // Apply outline color mapping based on palette and theme variant.
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

        // Apply outline offset (distance between element and outline).
        Add(item: CreateUpdater(styleType: StyleType.OutlineOffset, value: new StyleLength(value: Sizing.Size1)));

        // Apply outline style (e.g., solid).
        Add(
            item: CreateUpdater(
                styleType: StyleType.OutlineStyle,
                value: new StyleBorderOutlineStyle(kind: StyleBorderOutlineStyle.Kind.Solid)
            )
        );

        // Apply outline width (thickness of focus ring).
        Add(item: CreateUpdater(styleType: StyleType.OutlineWidth, value: new StyleLength(value: Sizing.Thick)));
    }
}
