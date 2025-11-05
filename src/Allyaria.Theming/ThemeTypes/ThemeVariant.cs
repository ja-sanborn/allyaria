namespace Allyaria.Theming.ThemeTypes;

internal sealed class ThemeVariant
{
    private readonly Dictionary<ThemeType, ThemeState> _children = new();

    internal CssBuilder BuildCss(CssBuilder builder, ThemeNavigator navigator, string? varPrefix = "")
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

    private ThemeState? Get(ThemeType key) => _children.GetValueOrDefault(key: key);

    internal ThemeVariant Set(ThemeNavigator navigator, IStyleValue? value)
    {
        foreach (var key in navigator.ThemeTypes)
        {
            if (!_children.ContainsKey(key: key))
            {
                _children.Add(key: key, value: new ThemeState());
            }

            Get(key: key)?.Set(navigator: navigator, value: value);
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
