namespace Allyaria.Theming.ThemeTypes;

/// <summary>
/// Represents the internal core theme engine responsible for generating CSS strings based on Allyaria’s theming
/// definitions, including component-level, document-level, and global visual styles.
/// </summary>
/// <remarks>
///     <para>
///     This class composes CSS dynamically by mapping <see cref="ComponentType" />, <see cref="ThemeType" />, and
///     <see cref="ComponentState" /> values through the theming pipeline. The resulting CSS is applied via Blazor
///     component rendering or injected globally through <see cref="IThemingService" />.
///     </para>
///     <para>
///     The <see cref="Theme" /> class is internal and used by higher-level abstractions such as
///     <see cref="ThemingService" /> and <see cref="ThemeBuilder" />. It supports accessibility features such as reduced
///     motion and visible focus indicators, and automatically includes high-contrast adjustments where applicable.
///     </para>
/// </remarks>
internal sealed class Theme
{
    /// <summary>
    /// Internal reference to the <see cref="ThemeComponent" /> used to construct the CSS output for all theming targets.
    /// </summary>
    private ThemeComponent _component = new();

    /// <summary>Generates a reusable CSS rule that enforces consistent box sizing across all elements.</summary>
    /// <returns>A CSS string that ensures all elements, including pseudo-elements, inherit box sizing.</returns>
    private static string GetBoxSizingCss() => "*,*::before,*::after{box-sizing:inherit;}";

    /// <summary>Builds the CSS string for a specific themed component based on its type, state, and prefix.</summary>
    /// <param name="prefix">The CSS selector or class prefix for the component.</param>
    /// <param name="componentType">The <see cref="ComponentType" /> representing the element type.</param>
    /// <param name="themeType">The <see cref="ThemeType" /> defining the active theme variant.</param>
    /// <param name="componentState">The <see cref="ComponentState" /> defining the current visual state.</param>
    /// <returns>A formatted CSS string scoped to the provided prefix and theme parameters.</returns>
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

    /// <summary>
    /// Builds a complete CSS string representing document-wide theming styles, including global variables, base resets,
    /// typography, focus outlines, and accessibility rules.
    /// </summary>
    /// <param name="themeType">The <see cref="ThemeType" /> for which to generate document styles.</param>
    /// <returns>A full CSS string suitable for document-level injection.</returns>
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

    /// <summary>Generates the CSS rules for focus-visible outlines and focusable elements.</summary>
    /// <param name="themeType">The current <see cref="ThemeType" /> being rendered.</param>
    /// <returns>A CSS string defining focus outlines for global and interactive elements.</returns>
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

    /// <summary>Generates global CSS for base elements like <c>html</c> and <c>body</c>.</summary>
    /// <param name="themeType">The current <see cref="ThemeType" /> being rendered.</param>
    /// <returns>A CSS string for global page-level elements.</returns>
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

    /// <summary>Generates link-specific CSS rules, including states for default, focused, pressed, and visited.</summary>
    /// <param name="themeType">The active <see cref="ThemeType" />.</param>
    /// <returns>A CSS string containing anchor element theming rules.</returns>
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

    /// <summary>Generates a CSS block that disables animations and transitions when users prefer reduced motion.</summary>
    /// <returns>A CSS string applying <c>prefers-reduced-motion</c> adjustments for accessibility.</returns>
    private static string GetReducedMotionCss()
        => "@media(prefers-reduced-motion:reduce){*{animation:none !important;transition:none !important;}html,body{scroll-behavior:auto !important;}}";

    /// <summary>Generates the <c>:root</c> CSS variables section containing theme-wide color and style tokens.</summary>
    /// <returns>A CSS string containing variable declarations for the current theme.</returns>
    private string GetRootCss() => $":root{{{ToCssVars()}}}";

    /// <summary>Generates text and heading CSS rules based on the current theme type.</summary>
    /// <param name="themeType">The <see cref="ThemeType" /> being rendered.</param>
    /// <returns>A concatenated CSS string for paragraph and heading elements.</returns>
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

    /// <summary>Updates this theme by applying the provided <see cref="ThemeUpdater" />.</summary>
    /// <param name="updater">The <see cref="ThemeUpdater" /> describing the update to apply.</param>
    /// <returns>The modified <see cref="Theme" /> instance (for chaining).</returns>
    public Theme Set(ThemeUpdater updater)
    {
        _component = _component.Set(updater: updater);

        return this;
    }

    /// <summary>Builds the CSS rules for a specific combination of component, theme, and state.</summary>
    /// <param name="componentType">The <see cref="ComponentType" /> to generate CSS for.</param>
    /// <param name="themeType">The <see cref="ThemeType" /> to apply.</param>
    /// <param name="componentState">The <see cref="ComponentState" /> representing the current UI state.</param>
    /// <returns>A CSS string containing the computed declarations.</returns>
    private string ToCss(ComponentType componentType, ThemeType themeType, ComponentState componentState)
        => _component.BuildCss(
                builder: new CssBuilder(),
                navigator: ThemeNavigator.Initialize
                    .SetComponentTypes(componentType)
                    .SetThemeTypes(themeType)
                    .SetComponentStates(componentState)
            )
            .ToString();

    /// <summary>Builds CSS variable declarations for all theme properties.</summary>
    /// <returns>A CSS string containing root-level variable definitions.</returns>
    private string ToCssVars()
        => _component
            .BuildCss(
                builder: new CssBuilder(), navigator: ThemeNavigator.Initialize, varPrefix: StyleDefaults.VarPrefix
            )
            .ToString();
}
