namespace Allyaria.Theming.ThemeTypes;

internal sealed class ThemeComponent
{
    private readonly Dictionary<ComponentType, ThemeVariant> _children = new();

    internal CssBuilder BuildCss(CssBuilder builder, ThemeNavigator navigator, string? varPrefix = "")
    {
        if (navigator.ComponentTypes.Count is 0)
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
            foreach (var key in navigator.ComponentTypes)
            {
                builder = Get(key: key)?.BuildCss(
                    builder: builder, navigator: navigator, varPrefix: SetPrefix(varPrefix: varPrefix, type: key)
                ) ?? builder;
            }
        }

        return builder;
    }

    private ThemeVariant? Get(ComponentType key) => _children.GetValueOrDefault(key: key);

    internal ThemeComponent Set(ThemeNavigator navigator, IStyleValue? value)
    {
        foreach (var key in navigator.ComponentTypes)
        {
            if (!_children.ContainsKey(key: key))
            {
                _children.Add(key: key, value: new ThemeVariant());
            }

            Get(key: key)?.Set(navigator: navigator, value: value);
        }

        return this;
    }

    private static string SetPrefix(string? varPrefix, ComponentType type)
    {
        var prefix = varPrefix?.ToCssName() ?? string.Empty;

        return string.IsNullOrWhiteSpace(value: prefix)
            ? string.Empty
            : $"{prefix}-{type}".ToCssName();
    }
}
