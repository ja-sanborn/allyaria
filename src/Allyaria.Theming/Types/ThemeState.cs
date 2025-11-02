namespace Allyaria.Theming.Types;

internal sealed class ThemeState
{
    private readonly Dictionary<ComponentState, ThemeStyle> _children = new();

    internal CssBuilder BuildCss(CssBuilder builder, ThemeNavigator navigator, string? varPrefix = "")
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

    private ThemeStyle? Get(ComponentState key) => _children.GetValueOrDefault(key: key);

    internal ThemeState Set(ThemeNavigator navigator, IStyleValue? value)
    {
        foreach (var key in navigator.ComponentStates)
        {
            if (!_children.ContainsKey(key: key))
            {
                _children.Add(key: key, value: new ThemeStyle());
            }

            Get(key: key)?.Set(navigator: navigator, value: value);
        }

        return this;
    }

    private static string SetPrefix(string? varPrefix, ComponentState type)
    {
        var prefix = varPrefix?.ToCssName() ?? string.Empty;

        return string.IsNullOrWhiteSpace(value: prefix)
            ? string.Empty
            : $"{prefix}-{type}".ToCssName();
    }
}
