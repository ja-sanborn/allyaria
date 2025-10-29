namespace Allyaria.Theming.Types;

public sealed class ThemeVariant
{
    private readonly Dictionary<ThemeType, ThemeState> _children = new();

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

    public ThemeState? Get(ThemeType key)
        => _children.TryGetValue(key: key, value: out var value)
            ? value
            : null;

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
                if (_children.ContainsKey(key: key))
                {
                    _children[key: key] = updater.State;
                }
                else
                {
                    _children.Add(key: key, value: updater.State);
                }

                if (updater.Value is not null)
                {
                    Get(key: key)?.Set(updater: updater);
                }
            }
        }

        return this;
    }

    private string SetPrefix(string? varPrefix, ThemeType type)
    {
        var prefix = varPrefix?.ToCssName() ?? string.Empty;

        return string.IsNullOrWhiteSpace(value: prefix)
            ? string.Empty
            : $"{prefix}-{type}".ToCssName();
    }
}
