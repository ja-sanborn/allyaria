namespace Allyaria.Theming.Helpers;

/// <summary>
/// Represents a basic style application utility that creates a single <see cref="ThemeUpdater" /> for a specified
/// <see cref="StyleType" /> and applies it to a component within the theme structure.
/// </summary>
/// <remarks>
///     <para>
///     The <see cref="ThemeApplier" /> is a concrete implementation of <see cref="ThemeApplierBase" />. It provides a
///     simplified entry point for applying a single style type—such as margin, padding, or color—to a particular
///     <see cref="ComponentType" />.
///     </para>
///     <para>
///     This class is commonly used during theme construction in <see cref="ThemeBuilder" /> to apply foundational styles
///     such as layout spacing and element sizing.
///     </para>
/// </remarks>
internal sealed class ThemeApplier : ThemeApplierBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeApplier" /> class, creating a <see cref="ThemeUpdater" /> for the
    /// specified style type and value.
    /// </summary>
    /// <param name="themeMapper">The <see cref="ThemeMapper" /> instance used for theme mapping operations.</param>
    /// <param name="isHighContrast">Specifies whether this applier operates in high-contrast mode.</param>
    /// <param name="componentType">The <see cref="ComponentType" /> to which this style applies.</param>
    /// <param name="styleType">The <see cref="StyleType" /> representing the style to be applied.</param>
    /// <param name="value">The <see cref="IStyleValue" /> representing the style’s assigned value.</param>
    public ThemeApplier(ThemeMapper themeMapper,
        bool isHighContrast,
        ComponentType componentType,
        StyleType styleType,
        IStyleValue value)
        : base(themeMapper: themeMapper, isHighContrast: isHighContrast, componentType: componentType)
        => Add(item: CreateUpdater(styleType: styleType, value: value));
}
