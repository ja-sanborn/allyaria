namespace Allyaria.Components.Blazor;

/// <summary>
/// Provides a common base implementation for Allyaria Blazor components, including support for ARIA accessibility
/// attributes, theming, and additional HTML attributes.
/// </summary>
public abstract class AryComponentBase : ComponentBase
{
    /// <summary>
    /// Gets or sets additional arbitrary HTML attributes to be applied to the rendered element. Attributes that conflict with
    /// component-managed values (such as <c>class</c>, <c>id</c>, or ARIA attributes) may be filtered out via
    /// <see cref="GetFilteredAttributes(string[])" />.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Gets or sets the value for the <c>aria-describedby</c> attribute on the rendered element. This identifies one or more
    /// elements that provide descriptive text for the component.
    /// </summary>
    [Parameter]
    public string? AriaDescribedBy { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the rendered element should be hidden from assistive technologies. When set to
    /// <see langword="true" />, the resolved <c>aria-hidden</c> attribute will be <c>"true"</c>.
    /// </summary>
    [Parameter]
    public bool? AriaHidden { get; set; }

    /// <summary>
    /// Gets or sets the value for the <c>aria-label</c> attribute on the rendered element. This provides an accessible label
    /// when no visible label is present.
    /// </summary>
    [Parameter]
    public string? AriaLabel { get; set; }

    /// <summary>
    /// Gets or sets the value for the <c>aria-labelledby</c> attribute on the rendered element. This references one or more
    /// elements that label the component.
    /// </summary>
    [Parameter]
    public string? AriaLabelledBy { get; set; }

    /// <summary>
    /// Gets or sets the explicit ARIA role for the rendered element. When specified, this value is applied to the <c>role</c>
    /// attribute.
    /// </summary>
    [Parameter]
    public string? AriaRole { get; set; }

    /// <summary>
    /// Gets the base CSS class name used by the component before any additional classes are applied. Derived components must
    /// provide a stable, non-localized base class string.
    /// </summary>
    protected abstract string BaseClass { get; }

    /// <summary>
    /// Gets or sets the child content to be rendered inside the component. This content is typically the primary UI body for
    /// the component.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets additional CSS classes to apply to the rendered element. These classes are combined with
    /// <see cref="BaseClass" /> when constructing <see cref="DerivedClass" />.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Gets or sets the current visual and interactive state of the component. The state influences the resolved theming
    /// information for the component.
    /// </summary>
    [Parameter]
    public ComponentState ComponentState { get; set; } = ComponentState.Default;

    /// <summary>
    /// Gets the logical component type used when resolving theming information. Derived components must indicate which theme
    /// configuration should be applied.
    /// </summary>
    protected abstract ComponentType ComponentType { get; }

    /// <summary>
    /// Gets the combined CSS class string derived from <see cref="BaseClass" /> and <see cref="Class" />. Values are
    /// normalized for CSS naming and collapsed to <see langword="null" /> when empty.
    /// </summary>
    protected string? DerivedClass => $"{BaseClass.ToCssName()} {Class.ToCssName()}".Trim().OrNull();

    /// <summary>
    /// Gets the effective inline style string for the component based on the current theme and any user-provided
    /// <see cref="Style" /> overrides. Returns <see langword="null" /> when no styles should be applied.
    /// </summary>
    protected string? DerivedStyle
    {
        get
        {
            var localThemeType = ThemeType is ThemeType.System || ThemeType == EffectiveThemeType
                ? ThemeType.System
                : ThemeType;

            var effectiveTheme = ThemingService.GetComponentCssVars(
                themeType: localThemeType, componentType: ComponentType, componentState: ComponentState
            );

            var effectiveStyle = string.IsNullOrWhiteSpace(value: Style)
                ? effectiveTheme
                : new CssBuilder().AddRange(cssList: Style).AddRange(cssList: effectiveTheme).ToString();

            return effectiveStyle.OrNull();
        }
    }

    /// <summary>
    /// Gets or sets the effective theme type cascaded from a parent component or application-level theme. This value is
    /// typically used when <see cref="ThemeType" /> is set to <see cref="ThemeType.System" />.
    /// </summary>
    [CascadingParameter]
    public ThemeType EffectiveThemeType { get; set; }

    /// <summary>
    /// Gets or sets the HTML <c>id</c> attribute for the rendered element. When not specified, the component may render
    /// without an explicit identifier.
    /// </summary>
    [Parameter]
    public string? Id { get; set; }

    /// <summary>
    /// Gets the resolved string value for the <c>aria-hidden</c> attribute, or <see langword="null" /> when no attribute
    /// should be rendered. Returns <c>"true"</c> when <see cref="AriaHidden" /> is explicitly set to <see langword="true" />.
    /// </summary>
    protected string? ResolvedAriaHidden
        => AriaHidden is true
            ? "true"
            : null;

    /// <summary>
    /// Gets the resolved string value for the <c>tabindex</c> attribute, or <see langword="null" /> when no attribute should
    /// be rendered.
    /// </summary>
    protected string? ResolvedTabIndex
        => TabIndex is null
            ? null
            : TabIndex.ToString();

    /// <summary>
    /// Gets the effective theme type that should be used when resolving theming information for the component. When
    /// <see cref="ThemeType" /> is <see cref="ThemeType.System" />, this returns <see cref="EffectiveThemeType" />; otherwise
    /// it returns the explicitly set <see cref="ThemeType" />.
    /// </summary>
    protected ThemeType ResolvedThemeType
        => ThemeType is ThemeType.System
            ? EffectiveThemeType
            : ThemeType;

    /// <summary>
    /// Gets or sets additional inline CSS style declarations to be merged with theme-generated styles. When combined with
    /// theme values, user styles take precedence where declarations overlap.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>
    /// Gets or sets the logical tab index applied to the rendered element. When specified, the value is converted to a string
    /// for the <c>tabindex</c> attribute.
    /// </summary>
    [Parameter]
    public int? TabIndex { get; set; }

    /// <summary>
    /// Gets or sets the requested theme type for the component. When set to <see cref="ThemeType.System" />, the component
    /// will defer to <see cref="EffectiveThemeType" />.
    /// </summary>
    [Parameter]
    public ThemeType ThemeType { get; set; } = ThemeType.System;

    /// <summary>
    /// Gets or sets the theming service used to resolve CSS variables and styles for the component based on
    /// <see cref="ThemeType" />, <see cref="ComponentType" />, and <see cref="ComponentState" />.
    /// </summary>
    [Inject]
    public required IThemingService ThemingService { get; set; }

    /// <summary>
    /// Filters the <see cref="AdditionalAttributes" /> collection by removing attributes that are managed directly by the
    /// component, such as ARIA attributes, <c>class</c>, <c>id</c>, <c>style</c>, and <c>tabindex</c>, as well as any
    /// explicitly provided disallowed names.
    /// </summary>
    /// <param name="additionalDisallowed">
    /// An optional set of additional attribute names to exclude from the returned dictionary. Names are compared using
    /// <see cref="StringComparer.OrdinalIgnoreCase" />.
    /// </param>
    /// <returns>
    /// A new read-only dictionary containing only allowed attributes, or <see langword="null" /> when there are no attributes
    /// to apply.
    /// </returns>
    protected IReadOnlyDictionary<string, object>? GetFilteredAttributes(params string[] additionalDisallowed)
    {
        if (AdditionalAttributes?.Count is null or 0)
        {
            return null;
        }

        string[] disallowedKeys =
        [
            "aria-describedby",
            "aria-hidden",
            "aria-label",
            "aria-labelledby",
            "class",
            "id",
            "role",
            "style",
            "tabindex"
        ];

        var disallowed = new HashSet<string>(collection: disallowedKeys, comparer: StringComparer.OrdinalIgnoreCase);

        if (additionalDisallowed.Length > 0)
        {
            foreach (var name in additionalDisallowed)
            {
                if (string.IsNullOrWhiteSpace(value: name))
                {
                    continue;
                }

                disallowed.Add(item: name.Trim());
            }
        }

        var filtered = new Dictionary<string, object>(comparer: StringComparer.OrdinalIgnoreCase);

        foreach (var kvp in AdditionalAttributes)
        {
            if (disallowed.Contains(item: kvp.Key))
            {
                continue;
            }

            filtered.TryAdd(key: kvp.Key, value: kvp.Value);
        }

        return filtered.Count is 0
            ? null
            : filtered;
    }

    /// <summary>
    /// Requests a synchronous UI refresh for the component by scheduling <see cref="RefreshAsync" /> on the Blazor render
    /// queue. This is a convenience wrapper for triggering <see cref="ComponentBase.StateHasChanged" />.
    /// </summary>
    protected void Refresh() => _ = RefreshAsync();

    /// <summary>
    /// Asynchronously requests a UI refresh for the component by invoking <see cref="ComponentBase.StateHasChanged" /> on the
    /// correct synchronization context.
    /// </summary>
    /// <returns>A <see cref="Task" /> that completes when the render request has been queued.</returns>
    protected Task RefreshAsync() => InvokeAsync(workItem: StateHasChanged);
}
