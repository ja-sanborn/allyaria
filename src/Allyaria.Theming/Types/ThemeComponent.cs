namespace Allyaria.Theming.Types;

public sealed class ThemeComponent
{
    private readonly Dictionary<ComponentType, ThemeVariant> _children = new();

    public ThemeComponent()
    {
        _children.Add(key: ComponentType.Body, value: new ThemeVariant());
        _children.Add(key: ComponentType.BodyVariant, value: new ThemeVariant());
        _children.Add(key: ComponentType.Link, value: new ThemeVariant());
        _children.Add(key: ComponentType.LinkVariant, value: new ThemeVariant());
        _children.Add(key: ComponentType.Surface, value: new ThemeVariant());
        _children.Add(key: ComponentType.SurfaceVariant, value: new ThemeVariant());
    }

    public CssBuilder BuildCss(CssBuilder builder, ThemeNavigator navigator, string? varPrefix = "")
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

    public ThemeVariant? Get(ComponentType key) => _children.GetValueOrDefault(key: key);

    public ThemeComponent Set(ThemeUpdater updater)
    {
        foreach (var key in updater.Navigator.ComponentTypes)
        {
            if (updater.Variant is null)
            {
                if (!_children.ContainsKey(key: key))
                {
                    _children.Add(key: key, value: new ThemeVariant());
                }

                Get(key: key)?.Set(updater: updater);
            }
            else
            {
                _children[key: key] = updater.Variant;

                if (updater.Value is not null)
                {
                    Get(key: key)?.Set(updater: updater);
                }
            }
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
