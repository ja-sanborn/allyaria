namespace Allyaria.Theming.Types.Theme;

public sealed class Theme
{
    private ThemeComponent _component = new();

    public static Theme Build(Brand.Brand brand)
    {
        var theme = new Theme();

        // TODO: Cascade Brand to Theme

        return theme;
    }

    public Theme Set(ThemeUpdater updater)
    {
        _component = _component.Set(updater: updater);

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
