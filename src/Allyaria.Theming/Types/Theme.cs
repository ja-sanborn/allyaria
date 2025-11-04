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

        builder.Append(
            value: GetCss(
                prefix: "p",
                componentType: ComponentType.Text,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetCss(
                prefix: "h1",
                componentType: ComponentType.Heading1,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetCss(
                prefix: "h2",
                componentType: ComponentType.Heading2,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetCss(
                prefix: "h3",
                componentType: ComponentType.Heading3,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetCss(
                prefix: "h4",
                componentType: ComponentType.Heading4,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetCss(
                prefix: "h5",
                componentType: ComponentType.Heading5,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetCss(
                prefix: "h6",
                componentType: ComponentType.Heading6,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(value: GetLinkCss(themeType: themeType));

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

    public string GetLinkCss(ThemeType themeType)
    {
        var link = GetCss(
            prefix: "a",
            componentType: ComponentType.Link,
            themeType: themeType,
            componentState: ComponentState.Default
        );

        var focused = GetCss(
            prefix: "a:focus-visible",
            componentType: ComponentType.Link,
            themeType: themeType,
            componentState: ComponentState.Focused
        );

        var active = GetCss(
            prefix: "a:active",
            componentType: ComponentType.Link,
            themeType: themeType,
            componentState: ComponentState.Pressed
        );

        var visited = GetCss(
            prefix: "a:visited",
            componentType: ComponentType.Link,
            themeType: themeType,
            componentState: ComponentState.Visited
        );

        return $"{link}{focused}{active}{visited}";
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
