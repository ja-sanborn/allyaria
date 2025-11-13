namespace Allyaria.Theming.Helpers;

/// <summary>
/// Represents a navigational context for resolving theming relationships across components, states, and style types in the
/// Allyaria theming engine.
/// </summary>
/// <remarks>
///     <para>
///     <see cref="ThemeNavigator" /> is used to describe combinations of component types, visual states, and theme
///     variations. It provides fluent methods for building specific navigation contexts for theme updates, typically
///     consumed by <see cref="ThemeUpdater" />.
///     </para>
///     <para>
///     The static <see cref="Initialize" /> member provides an empty baseline instance that can be progressively
///     specialized using the <c>Set*</c> methods.
///     </para>
/// </remarks>
public readonly record struct ThemeNavigator(
    IReadOnlyList<ComponentType> ComponentTypes,
    IReadOnlyList<ThemeType> ThemeTypes,
    IReadOnlyList<ComponentState> ComponentStates,
    IReadOnlyList<StyleType> StyleTypes
)
{
    /// <summary>
    /// Gets a base instance of <see cref="ThemeNavigator" /> with all internal lists initialized and empty.
    /// </summary>
    public static readonly ThemeNavigator Initialize = new(
        ComponentTypes: BuildList<ComponentType>(),
        ThemeTypes: BuildList<ThemeType>(),
        ComponentStates: BuildList<ComponentState>(),
        StyleTypes: BuildList<StyleType>()
    );

    /// <summary>
    /// Builds a typed list of enum values, validating that the provided type is one of the supported theming enums (
    /// <see cref="ComponentType" />, <see cref="ThemeType" />, <see cref="ComponentState" />, or <see cref="StyleType" />).
    /// </summary>
    /// <typeparam name="TEnum">The enum type to populate.</typeparam>
    /// <param name="items">The enum values to include in the list.</param>
    /// <returns>A new list containing the provided items, or an empty list if none were provided.</returns>
    /// <exception cref="AryArgumentException">Thrown if an unsupported enum type is used.</exception>
    private static List<TEnum> BuildList<TEnum>(params TEnum[] items)
        where TEnum : Enum
    {
        if (items.Length is 0)
        {
            return new List<TEnum>();
        }

        if (!(items[0] is ComponentState or ComponentType or ThemeType or StyleType))
        {
            // Code Coverage: This is unreachable through all public methods.
            throw new AryArgumentException(message: "Invalid enum type", argName: nameof(items));
        }

        var list = new List<TEnum>(capacity: items.Length);
        list.AddRange(collection: items);

        return list;
    }

    /// <summary>
    /// Sets all component states except <see cref="ComponentState.Hidden" /> and <see cref="ComponentState.ReadOnly" />.
    /// </summary>
    /// <returns>A new <see cref="ThemeNavigator" /> instance with all interactive component states selected.</returns>
    public ThemeNavigator SetAllComponentStates()
    {
        var states = Enum.GetValues<ComponentState>().ToList();
        states.Remove(item: ComponentState.Hidden);
        states.Remove(item: ComponentState.ReadOnly);

        return SetComponentStates(items: states.ToArray());
    }

    /// <summary>Sets all available component types.</summary>
    /// <returns>A new <see cref="ThemeNavigator" /> with all <see cref="ComponentType" /> values applied.</returns>
    public ThemeNavigator SetAllComponentTypes() => SetComponentTypes(items: Enum.GetValues<ComponentType>());

    /// <summary>Sets all available style types.</summary>
    /// <returns>A new <see cref="ThemeNavigator" /> with all <see cref="StyleType" /> values applied.</returns>
    public ThemeNavigator SetAllStyleTypes() => SetStyleTypes(items: Enum.GetValues<StyleType>());

    /// <summary>Sets the component states for this navigator.</summary>
    /// <param name="items">An array of <see cref="ComponentState" /> values to include.</param>
    /// <returns>A new <see cref="ThemeNavigator" /> with the specified component states.</returns>
    public ThemeNavigator SetComponentStates(params ComponentState[] items)
        => this with
        {
            ComponentStates = BuildList(items: items)
        };

    /// <summary>Sets the component types for this navigator.</summary>
    /// <param name="items">An array of <see cref="ComponentType" /> values to include.</param>
    /// <returns>A new <see cref="ThemeNavigator" /> with the specified component types.</returns>
    public ThemeNavigator SetComponentTypes(params ComponentType[] items)
        => this with
        {
            ComponentTypes = BuildList(items: items)
        };

    /// <summary>Configures this navigator for high-contrast or standard light/dark theme variants.</summary>
    /// <param name="isHighContrast">Whether high-contrast themes should be targeted.</param>
    /// <returns>A new <see cref="ThemeNavigator" /> configured for the specified contrast context.</returns>
    internal ThemeNavigator SetContrastThemeTypes(bool isHighContrast)
        => isHighContrast
            ? SetThemeTypes(ThemeType.HighContrastLight, ThemeType.HighContrastDark)
            : SetThemeTypes(ThemeType.Light, ThemeType.Dark);

    /// <summary>Sets the style types for this navigator.</summary>
    /// <param name="items">An array of <see cref="StyleType" /> values to include.</param>
    /// <returns>A new <see cref="ThemeNavigator" /> with the specified style types.</returns>
    public ThemeNavigator SetStyleTypes(params StyleType[] items)
        => this with
        {
            StyleTypes = BuildList(items: items)
        };

    /// <summary>
    /// Sets the specific theme type to navigate, validating that system and high-contrast variants are excluded.
    /// </summary>
    /// <param name="themeType">The <see cref="ThemeType" /> to target.</param>
    /// <returns>A new <see cref="ThemeNavigator" /> configured with the specified theme type.</returns>
    /// <exception cref="AryArgumentException">Thrown if the specified theme type is invalid for this context.</exception>
    public ThemeNavigator SetThemeType(ThemeType themeType)
        => themeType is ThemeType.System or ThemeType.HighContrastDark or ThemeType.HighContrastLight
            ? throw new AryArgumentException(message: "Invalid theme type", argName: nameof(themeType))
            : SetThemeTypes(items: themeType);

    /// <summary>Sets the theme types for this navigator.</summary>
    /// <param name="items">An array of <see cref="ThemeType" /> values to include.</param>
    /// <returns>A new <see cref="ThemeNavigator" /> configured with the specified theme types.</returns>
    internal ThemeNavigator SetThemeTypes(params ThemeType[] items)
        => this with
        {
            ThemeTypes = BuildList(items: items)
        };
}
