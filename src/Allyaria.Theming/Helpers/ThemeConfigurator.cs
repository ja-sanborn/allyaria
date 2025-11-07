namespace Allyaria.Theming.Helpers;

public sealed class ThemeConfigurator : IThemeConfigurator
{
    private readonly List<ThemeUpdater> _internalList = new();

    public int Count => _internalList.Count;

    public ThemeUpdater this[int index] => _internalList[index: index];

    public IEnumerator<ThemeUpdater> GetEnumerator() => _internalList.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_internalList).GetEnumerator();

    public IThemeConfigurator Override(ThemeUpdater updater)
    {
        if (updater.Navigator.ThemeTypes.Contains(value: ThemeType.System))
        {
            throw new AryArgumentException(
                message: "System theme cannot be set directly.", argName: nameof(updater.Value)
            );
        }

        if (updater.Navigator.ThemeTypes.Contains(value: ThemeType.HighContrastDark) ||
            updater.Navigator.ThemeTypes.Contains(value: ThemeType.HighContrastLight))
        {
            throw new AryArgumentException(
                message: "High contrast themes cannot be overridden.", argName: nameof(updater.Value)
            );
        }

        _internalList.Add(item: updater);

        return this;
    }
}
