namespace Allyaria.Theming.Types;

public sealed class Theme
{
    private ThemeComponent _component = new();

    public string PrefixCss(string prefix,
        ComponentType componentType,
        ThemeType themeType,
        ComponentState componentState)
    {
        var css = ToCss(componentType: componentType, themeType: themeType, componentState: componentState);

        return string.IsNullOrWhiteSpace(value: prefix)
            ? css
            : $"{prefix.Trim()}{{{css}}}";
    }

    internal Theme Set(ThemeNavigator navigator, IStyleValue? value)
    {
        _component = _component.Set(navigator: navigator, value: value);

        return this;
    }

    public string ToCss(ComponentType componentType, ThemeType themeType, ComponentState componentState)
        => _component.BuildCss(
                builder: new CssBuilder(),
                navigator: ThemeNavigator.Initialize
                    .SetComponentTypes(componentType)
                    .SetThemeTypes(themeType)
                    .SetComponentStates(componentState)
            )
            .ToString();

    public string ToCssVars()
        => _component
            .BuildCss(
                builder: new CssBuilder(), navigator: ThemeNavigator.Initialize, varPrefix: StyleDefaults.VarPrefix
            )
            .ToString();
}
