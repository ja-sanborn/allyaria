namespace Allyaria.Theming.Contracts;

/// <summary>
/// Provides a base implementation for applying theme updates across components, managing a collection of
/// <see cref="ThemeUpdater" /> instances that describe style changes for various theme states and components.
/// </summary>
internal abstract class ThemeApplierBase : IReadOnlyList<ThemeUpdater>
{
    /// <summary>Internal backing list for theme updaters.</summary>
    private readonly List<ThemeUpdater> _internalList = new();

    /// <summary>Initializes a new instance of the <see cref="ThemeApplierBase" /> class.</summary>
    /// <param name="themeMapper">The <see cref="ThemeMapper" /> instance used to resolve style mappings.</param>
    /// <param name="isHighContrast">A value indicating whether the theme is high-contrast.</param>
    /// <param name="componentType">The <see cref="ComponentType" /> to which the theme applies.</param>
    protected ThemeApplierBase(ThemeMapper themeMapper, bool isHighContrast, ComponentType componentType)
    {
        ThemeMapper = themeMapper;
        IsHighContrast = isHighContrast;
        ComponentType = componentType;
    }

    /// <summary>Gets the component type this applier targets.</summary>
    protected ComponentType ComponentType { get; }

    /// <summary>Gets the number of <see cref="ThemeUpdater" /> items contained within this instance.</summary>
    public int Count => _internalList.Count;

    /// <summary>Gets a value indicating whether the current theme is high-contrast.</summary>
    protected bool IsHighContrast { get; }

    /// <summary>Gets the <see cref="ThemeMapper" /> responsible for mapping style data to theme properties.</summary>
    protected ThemeMapper ThemeMapper { get; }

    /// <summary>Gets the <see cref="ThemeUpdater" /> at the specified index.</summary>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <returns>The <see cref="ThemeUpdater" /> at the specified index.</returns>
    public ThemeUpdater this[int index] => _internalList[index: index];

    /// <summary>Adds a single <see cref="ThemeUpdater" /> to the internal collection.</summary>
    /// <param name="item">The <see cref="ThemeUpdater" /> to add.</param>
    protected void Add(ThemeUpdater item) => _internalList.Add(item: item);

    /// <summary>Adds a range of <see cref="ThemeUpdater" /> items to the internal collection.</summary>
    /// <param name="collection">A read-only list of <see cref="ThemeUpdater" /> instances to append.</param>
    protected void AddRange(IReadOnlyList<ThemeUpdater> collection) => _internalList.AddRange(collection: collection);

    /// <summary>
    /// Creates a new <see cref="ThemeUpdater" /> for the specified style type and value, automatically setting the relevant
    /// <see cref="ThemeNavigator" /> parameters.
    /// </summary>
    /// <param name="styleType">The style type associated with this updater.</param>
    /// <param name="value">The value to apply for the style type.</param>
    /// <returns>A constructed <see cref="ThemeUpdater" /> with navigation and value set.</returns>
    protected ThemeUpdater CreateUpdater(StyleType styleType, IStyleValue value)
        => new(
            Navigator: ThemeNavigator.Initialize
                .SetComponentTypes(items: ComponentType)
                .SetContrastThemeTypes(isHighContrast: IsHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(styleType),
            Value: value
        );

    /// <summary>Returns an enumerator that iterates through the contained <see cref="ThemeUpdater" /> collection.</summary>
    /// <returns>An enumerator for the collection.</returns>
    public IEnumerator<ThemeUpdater> GetEnumerator() => _internalList.GetEnumerator();

    /// <summary>
    /// Returns a non-generic enumerator that iterates through the contained <see cref="ThemeUpdater" /> collection.
    /// </summary>
    /// <returns>A non-generic enumerator for the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_internalList).GetEnumerator();
}
