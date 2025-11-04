namespace Allyaria.Theming.Types;

public sealed class Theme
{
    private ThemeComponent _component = new();

    public string BuildCss(ThemeType themeType)
    {
        var builder = new StringBuilder();

        builder.Append(value: GetRootCss());

        builder.Append(
            value: GetCss(
                prefix: "html",
                componentType: ComponentType.GlobalHtml,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetCss(
                prefix: "body",
                componentType: ComponentType.GlobalBody,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(value: GetBoxSizingCss());
        builder.Append(value: GetFocusCss(themeType: themeType));
        builder.Append(value: GetReducedMotionCss());

        return builder.ToString();
    }

    public static string GetBoxSizingCss() => "*,*::before,*::after{box-sizing:inherit;}";

    public string GetCss(string prefix,
        ComponentType componentType,
        ThemeType themeType,
        ComponentState componentState)
    {
        var css = ToCss(componentType: componentType, themeType: themeType, componentState: componentState);

        return string.IsNullOrWhiteSpace(value: prefix)
            ? css
            : $"{prefix.Trim()}{{{css}}}";
    }

    public string GetFocusCss(ThemeType themeType)
    {
        var globalFocus = GetCss(
            prefix: ":focus-visible",
            componentType: ComponentType.GlobalFocus,
            themeType: themeType,
            componentState: ComponentState.Focused
        );

        var whereFocus = GetCss(
            prefix: ":where(a,button,input,textarea,select,[tabindex]):focus-visible",
            componentType: ComponentType.GlobalFocus,
            themeType: themeType,
            componentState: ComponentState.Focused
        );

        return $"{globalFocus}{whereFocus}";
    }

    public static string GetReducedMotionCss()
        => "@media(prefers-reduced-motion:reduce){*{animation:none !important;transition:none !important;}html,body{scroll-behavior:auto !important;}}";

    public string GetRootCss() => $":root{{{ToCssVars()}}}";

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
