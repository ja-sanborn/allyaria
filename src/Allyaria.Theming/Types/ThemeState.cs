namespace Allyaria.Theming.Types;

public sealed class ThemeState
{
    private readonly Dictionary<ComponentState, ThemeStyle> _children = new();

    public CssBuilder BuildCss(CssBuilder builder, ThemeNavigator navigator, string? varPrefix = "")
    {
        if (navigator.ComponentStates.Count is 0)
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
            foreach (var key in navigator.ComponentStates)
            {
                builder = Get(key: key)?.BuildCss(
                    builder: builder, navigator: navigator, varPrefix: SetPrefix(varPrefix: varPrefix, type: key)
                ) ?? builder;
            }
        }

        return builder;
    }

    public ThemeStyle? Get(ComponentState key)
        => _children.TryGetValue(key: key, value: out var value)
            ? value
            : null;

    public ThemeState Set(ThemeUpdater updater)
    {
        foreach (var key in updater.Navigator.ComponentStates)
        {
            if (updater.Style is null)
            {
                if (!_children.ContainsKey(key: key))
                {
                    _children.Add(key: key, value: new ThemeStyle());
                }

                Get(key: key)?.Set(updater: updater);
            }
            else
            {
                if (_children.ContainsKey(key: key))
                {
                    _children[key: key] = updater.Style;
                }
                else
                {
                    _children.Add(key: key, value: updater.Style);
                }

                if (updater.Value is not null)
                {
                    Get(key: key)?.Set(updater: updater);
                }
            }
        }

        return this;
    }

    private string SetPrefix(string? varPrefix, ComponentState type)
    {
        var prefix = varPrefix?.ToCssName() ?? string.Empty;

        return string.IsNullOrWhiteSpace(value: prefix)
            ? string.Empty
            : $"{prefix}-{type}".ToCssName();
    }
}
