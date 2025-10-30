namespace Allyaria.Theming.Types;

public sealed class ThemeVariant
{
    private readonly Dictionary<ThemeType, ThemeState> _children = new();

    public ThemeVariant()
    {
        _children.Add(key: ThemeType.Dark, value: new ThemeState());
        _children.Add(key: ThemeType.HighContrastDark, value: new ThemeState());
        _children.Add(key: ThemeType.HighContrastLight, value: new ThemeState());
        _children.Add(key: ThemeType.Light, value: new ThemeState());
    }

    public CssBuilder BuildCss(CssBuilder builder, ThemeNavigator navigator, string? varPrefix = "")
    {
        if (navigator.ThemeTypes.Count is 0)
        {
            foreach (var child in _children)
            {
                builder = child.Value.BuildCss(
                    builder: builder, navigator: navigator, varPrefix: SetPrefix(varPrefix: varPrefix, type: child.Key)
                );
            }
        }
        else
        {
            foreach (var key in navigator.ThemeTypes)
            {
                builder = Get(key: key)?.BuildCss(
                    builder: builder, navigator: navigator, varPrefix: SetPrefix(varPrefix: varPrefix, type: key)
                ) ?? builder;
            }
        }

        return builder;
    }

    public ThemeState? Get(ThemeType key) => _children.GetValueOrDefault(key: key);

    public ThemeVariant Set(ThemeUpdater updater)
    {
        foreach (var key in updater.Navigator.ThemeTypes)
        {
            if (updater.State is null)
            {
                if (!_children.ContainsKey(key: key))
                {
                    _children.Add(key: key, value: new ThemeState());
                }

                Get(key: key)?.Set(updater: updater);
            }
            else
            {
                _children[key: key] = updater.State;

                if (updater.Value is not null)
                {
                    Get(key: key)?.Set(updater: updater);
                }
            }
        }

        return this;
    }

    private static string SetPrefix(string? varPrefix, ThemeType type)
    {
        var prefix = varPrefix?.ToCssName() ?? string.Empty;

        return string.IsNullOrWhiteSpace(value: prefix)
            ? string.Empty
            : $"{prefix}-{type}".ToCssName();
    }
}
