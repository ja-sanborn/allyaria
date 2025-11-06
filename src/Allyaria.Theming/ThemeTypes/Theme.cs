namespace Allyaria.Theming.ThemeTypes;

internal sealed class Theme
{
    private ThemeComponent _component = new();

    private static string GetBoxSizingCss() => "*,*::before,*::after{box-sizing:inherit;}";

    public string GetComponentCss(string prefix,
        ComponentType componentType,
        ThemeType themeType,
        ComponentState componentState)
    {
        var css = ToCss(componentType: componentType, themeType: themeType, componentState: componentState);

        return string.IsNullOrWhiteSpace(value: prefix)
            ? css
            : $"{prefix.Trim()}{{{css}}}";
    }

    public string GetDocumentCss(ThemeType themeType)
    {
        var builder = new StringBuilder();

        builder.Append(value: GetRootCss());
        builder.Append(value: GetGlobalCss(themeType: themeType));
        builder.Append(value: GetBoxSizingCss());
        builder.Append(value: GetFocusCss(themeType: themeType));
        builder.Append(value: GetReducedMotionCss());
        builder.Append(value: GetTextCss(themeType: themeType));
        builder.Append(value: GetLinkCss(themeType: themeType));

        return builder.ToString();
    }

    private string GetFocusCss(ThemeType themeType)
    {
        var globalFocus = GetComponentCss(
            prefix: ":focus-visible",
            componentType: ComponentType.GlobalFocus,
            themeType: themeType,
            componentState: ComponentState.Focused
        );

        var whereFocus = GetComponentCss(
            prefix: ":where(a,button,input,textarea,select,[tabindex]):focus-visible",
            componentType: ComponentType.GlobalFocus,
            themeType: themeType,
            componentState: ComponentState.Focused
        );

        return $"{globalFocus}{whereFocus}";
    }

    private string GetGlobalCss(ThemeType themeType)
    {
        var html = GetComponentCss(
            prefix: "html",
            componentType: ComponentType.GlobalHtml,
            themeType: themeType,
            componentState: ComponentState.Default
        );

        var body = GetComponentCss(
            prefix: "body",
            componentType: ComponentType.GlobalBody,
            themeType: themeType,
            componentState: ComponentState.Default
        );

        return $"{html}{body}";
    }

    private string GetLinkCss(ThemeType themeType)
    {
        var builder = new StringBuilder();

        builder.Append(
            value: GetComponentCss(
                prefix: "a",
                componentType: ComponentType.Link,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetComponentCss(
                prefix: "a:focus-visible",
                componentType: ComponentType.Link,
                themeType: themeType,
                componentState: ComponentState.Focused
            )
        );

        builder.Append(
            value: GetComponentCss(
                prefix: "a:active",
                componentType: ComponentType.Link,
                themeType: themeType,
                componentState: ComponentState.Pressed
            )
        );

        builder.Append(
            value: GetComponentCss(
                prefix: "a:visited",
                componentType: ComponentType.Link,
                themeType: themeType,
                componentState: ComponentState.Visited
            )
        );

        return builder.ToString();
    }

    private static string GetReducedMotionCss()
        => "@media(prefers-reduced-motion:reduce){*{animation:none !important;transition:none !important;}html,body{scroll-behavior:auto !important;}}";

    private string GetRootCss() => $":root{{{ToCssVars()}}}";

    private string GetTextCss(ThemeType themeType)
    {
        var builder = new StringBuilder();

        builder.Append(
            value: GetComponentCss(
                prefix: "p",
                componentType: ComponentType.Text,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetComponentCss(
                prefix: "h1",
                componentType: ComponentType.Heading1,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetComponentCss(
                prefix: "h2",
                componentType: ComponentType.Heading2,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetComponentCss(
                prefix: "h3",
                componentType: ComponentType.Heading3,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetComponentCss(
                prefix: "h4",
                componentType: ComponentType.Heading4,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetComponentCss(
                prefix: "h5",
                componentType: ComponentType.Heading5,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        builder.Append(
            value: GetComponentCss(
                prefix: "h6",
                componentType: ComponentType.Heading6,
                themeType: themeType,
                componentState: ComponentState.Default
            )
        );

        return builder.ToString();
    }

    public Theme Set(ThemeUpdater updater)
    {
        _component = _component.Set(updater: updater);

        return this;
    }

    private string ToCss(ComponentType componentType, ThemeType themeType, ComponentState componentState)
        => _component.BuildCss(
                builder: new CssBuilder(),
                navigator: ThemeNavigator.Initialize
                    .SetComponentTypes(componentType)
                    .SetThemeTypes(themeType)
                    .SetComponentStates(componentState)
            )
            .ToString();

    private string ToCssVars()
        => _component
            .BuildCss(
                builder: new CssBuilder(), navigator: ThemeNavigator.Initialize, varPrefix: StyleDefaults.VarPrefix
            )
            .ToString();
}
