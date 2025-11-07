namespace Allyaria.Theming.Contracts;

public interface IThemeConfigurator : IReadOnlyList<ThemeUpdater>
{
    IThemeConfigurator Override(ThemeUpdater updater);
}
