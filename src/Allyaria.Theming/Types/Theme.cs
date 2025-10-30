namespace Allyaria.Theming.Types;

public sealed class Theme
{
    private readonly ThemeComponent _component = new();

    public string ToCss(ComponentType componentType, ThemeType themeType, ComponentState componentState)
        => _component.BuildCss(
                builder: new CssBuilder(),
                navigator: ThemeNavigator.Empty
                    .SetComponentTypes(componentType)
                    .SetThemeTypes(themeType)
                    .SetComponentStates(componentState)
            )
            .ToString();

    public string ToCssVars()
        => _component
            .BuildCss(builder: new CssBuilder(), navigator: ThemeNavigator.Empty, varPrefix: StyleDefaults.VarPrefix)
            .ToString();
}
