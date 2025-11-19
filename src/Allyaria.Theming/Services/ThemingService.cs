namespace Allyaria.Theming.Services;

/// <summary>
/// Provides the core implementation of the <see cref="IThemingService" /> interface, managing the currently active and
/// stored theme types, as well as CSS generation for Allyaria-themed components and documents.
/// </summary>
/// <remarks>
///     <para>
///     This service maintains both the <see cref="EffectiveType" /> (the theme currently applied in the UI) and the
///     <see cref="StoredType" /> (the user’s persisted theme preference).
///     </para>
///     <para>
///     When <see cref="StoredType" /> changes, the service raises the <see cref="ThemeChanged" /> event so that UI
///     components can reactively update their styling.
///     </para>
/// </remarks>
public sealed class ThemingService : IThemingService
{
    /// <summary>The underlying theme definition used to compute and generate CSS styles.</summary>
    private readonly Theme _theme;

    /// <summary>
    /// Initializes a new instance of the <see cref="ThemingService" /> class with the specified theme and initial type.
    /// </summary>
    /// <param name="theme">The <see cref="Theme" /> instance used to generate CSS and map component styles.</param>
    /// <param name="themeType">The initial <see cref="ThemeType" /> to use. Defaults to <see cref="ThemeType.System" />.</param>
    internal ThemingService(Theme theme, ThemeType themeType = ThemeType.System)
    {
        _theme = theme;
        StoredType = themeType;

        EffectiveType = StoredType is ThemeType.System
            ? ThemeType.Light
            : StoredType;
    }

    /// <summary>Occurs when the currently active (effective) theme changes.</summary>
    public event EventHandler? ThemeChanged;

    /// <summary>Gets the currently effective <see cref="ThemeType" /> applied to the UI.</summary>
    public ThemeType EffectiveType { get; private set; }

    /// <summary>
    /// Gets the persisted or stored <see cref="ThemeType" />, representing user preference or saved configuration.
    /// </summary>
    public ThemeType StoredType { get; private set; }

    /// <summary>
    /// Generates component-level CSS for a given prefix, component type, and state, based on the current effective theme.
    /// </summary>
    /// <param name="prefix">The CSS class prefix for the component.</param>
    /// <param name="componentType">The <see cref="ComponentType" /> representing the themed component.</param>
    /// <param name="componentState">The <see cref="ComponentState" /> representing the current visual state.</param>
    /// <returns>A string containing CSS rules scoped to the specified component and theme state.</returns>
    public string GetComponentCss(string prefix, ComponentType componentType, ComponentState componentState)
        => _theme.GetComponentCss(
            prefix: prefix,
            componentType: componentType,
            themeType: EffectiveType,
            componentState: componentState
        );

    /// <summary>
    /// Generates CSS variable declarations for a specific <see cref="ThemeType" />, <see cref="ComponentType" />, and
    /// <see cref="ComponentState" /> by transforming computed component CSS into corresponding <c>var(--prefix-property)</c>
    /// references.
    /// </summary>
    /// <param name="themeType">
    /// The theme from which variables should be generated. If <see cref="ThemeType.System" /> is specified, no variables are
    /// produced.
    /// </param>
    /// <param name="componentType">The component type whose themed styles are being converted into variables.</param>
    /// <param name="componentState">The visual state of the component whose styles should be mapped.</param>
    /// <returns>
    /// A string containing CSS variable references derived from the component’s themed CSS, or an empty string when no
    /// variables can be generated.
    /// </returns>
    public string GetComponentCssVars(ThemeType themeType, ComponentType componentType, ComponentState componentState)
    {
        if (themeType is ThemeType.System)
        {
            return string.Empty;
        }

        var cssVars = GetComponentCss(
            prefix: StyleDefaults.VarPrefix, componentType: componentType, componentState: componentState
        );

        if (string.IsNullOrWhiteSpace(value: cssVars))
        {
            // Code Coverage: Unreachable code path
            return string.Empty;
        }

        var builder = new StringBuilder();
        var prefix = $"{StyleDefaults.VarPrefix}-{componentType}-{themeType}-{componentState}".ToCssName();
        var split = cssVars.Split(separator: ';', options: StringSplitOptions.RemoveEmptyEntries);

        foreach (var item in split)
        {
            var pair = item.Split(separator: ':', options: StringSplitOptions.RemoveEmptyEntries);

            if (pair.Length < 2)
            {
                continue;
            }

            var property = pair[0].ToCssName();

            if (string.IsNullOrWhiteSpace(value: property))
            {
                // Code Coverage: Unreachable code path
                continue;
            }

            builder.Append(handler: $"{property}:var(--{prefix}-{property});");
        }

        return builder.ToString();
    }

    /// <summary>Generates global document-level CSS reflecting the currently active theme.</summary>
    /// <returns>A string containing CSS rules applicable at the document scope.</returns>
    public string GetDocumentCss() => _theme.GetDocumentCss(themeType: EffectiveType);

    /// <summary>Raises the <see cref="ThemeChanged" /> event to notify subscribers of theme changes.</summary>
    private void OnThemeChanged() => ThemeChanged?.Invoke(sender: this, e: EventArgs.Empty);

    /// <summary>Sets the currently effective <see cref="ThemeType" /> and triggers a theme update if it changes.</summary>
    /// <param name="themeType">The new <see cref="ThemeType" /> to apply.</param>
    public void SetEffectiveType(ThemeType themeType)
    {
        if (themeType == ThemeType.System || EffectiveType == themeType)
        {
            return;
        }

        EffectiveType = themeType;
        OnThemeChanged();
    }

    /// <summary>Sets the stored <see cref="ThemeType" /> preference and updates the effective type accordingly.</summary>
    /// <param name="themeType">The <see cref="ThemeType" /> to store and potentially activate.</param>
    public void SetStoredType(ThemeType themeType)
    {
        if (StoredType == themeType)
        {
            return;
        }

        StoredType = themeType;

        if (themeType != ThemeType.System)
        {
            SetEffectiveType(themeType: themeType);
        }
        else
        {
            ThemeChanged?.Invoke(sender: this, e: EventArgs.Empty);
        }
    }
}
