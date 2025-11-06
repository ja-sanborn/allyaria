namespace Allyaria.Theming.Contracts;

internal abstract class ThemeApplierBase : IReadOnlyList<ThemeUpdater>
{
    private readonly List<ThemeUpdater> _internalList = new();

    protected ThemeApplierBase(ThemeMapper themeMapper, bool isHighContrast, ComponentType componentType)
    {
        ThemeMapper = themeMapper;
        IsHighContrast = isHighContrast;
    }

    protected ComponentType ComponentType { get; }

    public int Count => _internalList.Count;

    protected bool IsHighContrast { get; }

    protected ThemeMapper ThemeMapper { get; }

    public ThemeUpdater this[int index] => _internalList[index: index];

    protected void Add(ThemeUpdater item) => _internalList.Add(item: item);

    protected void AddRange(IReadOnlyList<ThemeUpdater> collection) => _internalList.AddRange(collection: collection);

    protected ThemeUpdater CreateUpdater(StyleType styleType, IStyleValue value)
        => new(
            Navigator: ThemeNavigator.Initialize
                .SetComponentTypes(items: ComponentType)
                .SetContrastThemeTypes(isHighContrast: IsHighContrast)
                .SetAllComponentStates()
                .SetStyleTypes(styleType),
            Value: value
        );

    public IEnumerator<ThemeUpdater> GetEnumerator() => _internalList.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_internalList).GetEnumerator();
}
