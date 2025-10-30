namespace Allyaria.Theming.Types;

public sealed class ThemeState
{
    private readonly Dictionary<ComponentState, ThemeStyle> _children = new();

    public ThemeState()
    {
        _children.Add(key: ComponentState.Default, value: new ThemeStyle());
        _children.Add(key: ComponentState.Disabled, value: new ThemeStyle());
        _children.Add(key: ComponentState.Dragged, value: new ThemeStyle());
        _children.Add(key: ComponentState.Focused, value: new ThemeStyle());
        _children.Add(key: ComponentState.Hovered, value: new ThemeStyle());
        _children.Add(key: ComponentState.Pressed, value: new ThemeStyle());
        _children.Add(key: ComponentState.Visited, value: new ThemeStyle());
    }

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

    public ThemeStyle? Get(ComponentState key) => _children.GetValueOrDefault(key: key);

    public ThemeState Set(ThemeUpdater updater)
    {
        // TODO: Cascade Focused to Style, and in Style check to see if the Outline Style is NONE or the Outline Width is 0
        // TODO: If either are true, set the Outline Style and Outline Width.
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
                _children[key: key] = updater.Style;

                if (updater.Value is not null)
                {
                    Get(key: key)?.Set(updater: updater);
                }
            }
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
