using Allyaria.Abstractions.Enumerations;
using Allyaria.Theming.Constants;
using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Globalization;
using System.Text;

namespace Allyaria.Components.Layout;

public partial class ArySurface : IAsyncDisposable
{
    private const string BaseClass = "ary-surface";

    private IReadOnlyDictionary<string, object?> _additionalAttributes =
        new Dictionary<string, object?>(AttributeComparer);

    private string? _ariaLabel;
    private string? _attributeClass;
    private string? _attributeStyle;
    private string _classAttribute = BaseClass;
    private string _componentStyle = string.Empty;
    private CultureInfo _culture = CultureInfo.GetCultureInfo("en-US");
    private LabelPosition _effectiveLabelPosition = LabelPosition.Hidden;
    private bool _isFloatingLabel;
    private bool _isFocused;
    private bool _isPlaceholderActive;
    private string _labelClass = "ary-surface__label";
    private string _labelPositionAttribute = LabelPosition.Hidden.ToString();
    private string _labelStyle = string.Empty;
    private IJSObjectReference? _module;
    private bool _renderLabel;
    private AryTheme _resolvedTheme = StyleDefaults.Theme;
    private ThemeType _resolvedThemeType = ThemeType.Light;
    private ElementReference _root;
    private DotNetObjectReference<ArySurface>? _selfReference;
    private string _stateAttribute = ComponentState.Default.ToString().ToLowerInvariant();
    private string _styleAttribute = string.Empty;
    private ThemeType _systemThemeType = ThemeType.Light;
    private AryStyle? _themeOverride;
    private bool _watchingSystemTheme;
    private static readonly StringComparer AttributeComparer = StringComparer.OrdinalIgnoreCase;

    private static readonly char[] ClassSeparators =
    {
        ' ',
        '\t',
        '\r',
        '\n'
    };

    private static readonly ComponentState[] ThemedStates =
    {
        ComponentState.Default,
        ComponentState.Hovered,
        ComponentState.Focused,
        ComponentState.Pressed,
        ComponentState.Disabled,
        ComponentState.Dragged
    };

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object?>? AdditionalAttributes { get; set; }

    [CascadingParameter]
    public CultureInfo? CascadedCulture { get; set; }
        = CultureInfo.GetCultureInfo("en-US");

    [CascadingParameter]
    public AryTheme CascadedTheme { get; set; } = StyleDefaults.Theme;

    [CascadingParameter]
    public ThemeType CascadedThemeType { get; set; } = ThemeType.System;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public bool HasValue { get; set; }

    [Parameter]
    public AryNumberValue? Height { get; set; }

    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public LabelPosition LabelPosition { get; set; } = LabelPosition.Hidden;

    [Parameter]
    public ComponentState State { get; set; } = ComponentState.Default;

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public AryStyle? Theme { get; set; }

    private bool UseSystemTheme => _themeOverride is null && CascadedThemeType == ThemeType.System;

    [Parameter]
    public AryNumberValue? Width { get; set; }

    private string BuildComponentStyle()
    {
        var builder = new StringBuilder();
        var defaultStyle = GetStyleForState(ComponentState.Default);

        builder.Append(defaultStyle.ToCss("ary-surface-default"));

        foreach (var state in ThemedStates)
        {
            if (state == ComponentState.Default)
            {
                continue;
            }

            var style = GetStyleForState(state);
            builder.Append(style.ToCss($"ary-surface-{state.ToString().ToLowerInvariant()}"));
        }

        if (Width is
            { } width)
        {
            builder.Append(width.ToCss("width"));
        }

        if (Height is
            { } height)
        {
            builder.Append(height.ToCss("height"));
        }

        builder.Append($"--ary-surface-padding-inline-start:{GetInlinePadding(defaultStyle.Spacing, true)};");
        builder.Append($"--ary-surface-padding-inline-end:{GetInlinePadding(defaultStyle.Spacing, false)};");

        return builder.ToString();
    }

    private string BuildContainerClass()
    {
        var classes = new List<string>
        {
            BaseClass
        };

        if (_culture.TextInfo.IsRightToLeft)
        {
            classes.Add("ary-surface--rtl");
        }

        if (_isFocused || State == ComponentState.Focused)
        {
            classes.Add("ary-surface--focused");
        }

        if (_isPlaceholderActive)
        {
            classes.Add("ary-surface--label-placeholder");
        }

        if (_isFloatingLabel)
        {
            classes.Add("ary-surface--label-floating");
        }

        switch (_effectiveLabelPosition)
        {
            case LabelPosition.Above:
                classes.Add("ary-surface--label-above");

                break;
            case LabelPosition.Below:
                classes.Add("ary-surface--label-below");

                break;
            case LabelPosition.PlaceholderAbove:
            case LabelPosition.PlaceholderBelow:
            case LabelPosition.PlaceholderHidden:
                classes.Add("ary-surface--label-inline");

                break;
            case LabelPosition.Hidden:
                classes.Add("ary-surface--label-hidden");

                break;
        }

        if (HasValue)
        {
            classes.Add("ary-surface--has-value");
        }

        if (_themeOverride is not null)
        {
            classes.Add("ary-surface--theme-override");
        }

        if (State != ComponentState.Default)
        {
            classes.Add($"ary-surface--state-{State.ToString().ToLowerInvariant()}");
        }

        if (!string.IsNullOrWhiteSpace(_attributeClass))
        {
            classes.Add(_attributeClass!);
        }

        if (!string.IsNullOrWhiteSpace(Class))
        {
            classes.Add(Class!);
        }

        return string.Join(' ', DeduplicateSplitClasses(classes));
    }

    private string BuildLabelClass()
    {
        var classes = new List<string>
        {
            "ary-surface__label"
        };

        if (_isPlaceholderActive)
        {
            classes.Add("ary-surface__label--placeholder");
        }

        if (_isFloatingLabel)
        {
            classes.Add("ary-surface__label--floating");
        }

        switch (_effectiveLabelPosition)
        {
            case LabelPosition.Above:
                classes.Add("ary-surface__label--above");

                break;
            case LabelPosition.Below:
                classes.Add("ary-surface__label--below");

                break;
            case LabelPosition.PlaceholderAbove:
            case LabelPosition.PlaceholderBelow:
            case LabelPosition.PlaceholderHidden:
                classes.Add("ary-surface__label--inline");

                break;
        }

        if (_culture.TextInfo.IsRightToLeft)
        {
            classes.Add("ary-surface__label--rtl");
        }

        return string.Join(' ', DeduplicateClasses(classes));
    }

    private string BuildLabelStyle(AryStyle activeStyle)
    {
        var palette = activeStyle.Palette;
        var labelColor = palette.ForegroundColor;
        var backgroundColor = "transparent";

        if (_effectiveLabelPosition == LabelPosition.Above)
        {
            labelColor = HasVisibleBorder(activeStyle.Border)
                ? palette.BorderColor
                : palette.ForegroundColor;

            if (_isFloatingLabel)
            {
                backgroundColor = palette.BackgroundColor.Value;
            }
        }

        if (_isPlaceholderActive)
        {
            var alpha = Math.Clamp(palette.ForegroundColor.A * 0.5, 0.0, 1.0);

            labelColor = AryColorValue.FromRgba(
                palette.ForegroundColor.R,
                palette.ForegroundColor.G,
                palette.ForegroundColor.B,
                alpha
            );
        }

        var builder = new StringBuilder();
        builder.Append($"color:{labelColor.Value};");
        builder.Append($"background-color:{backgroundColor};");

        if (_effectiveLabelPosition is LabelPosition.Above or LabelPosition.Below)
        {
            builder.Append($"font-size:{Sizing.Size2.Value};");
            builder.Append("max-width:50%;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;");
        }

        return builder.ToString();
    }

    private AryTheme BuildResolvedTheme()
    {
        if (_themeOverride is
            { } overrideStyle)
        {
            return CascadedTheme.Cascade(
                overrideStyle.Border,
                overrideStyle.Spacing,
                overrideStyle.Palette,
                overrideStyle.Palette,
                overrideStyle.Palette,
                overrideStyle.Typo
            );
        }

        return CascadedTheme;
    }

    private static IEnumerable<string> DeduplicateClasses(IEnumerable<string> classes)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var value in classes)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                continue;
            }

            if (seen.Add(value))
            {
                yield return value;
            }
        }
    }

    private static IEnumerable<string> DeduplicateSplitClasses(IEnumerable<string> sources)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var source in sources)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                continue;
            }

            var parts = source.Split(ClassSeparators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                if (seen.Add(part))
                {
                    yield return part;
                }
            }
        }
    }

    private ComponentState DetermineActiveState()
    {
        if (State is ComponentState.Disabled or ComponentState.Dragged or ComponentState.Pressed or
            ComponentState.Hovered or ComponentState.Focused)
        {
            return State;
        }

        return _isFocused
            ? ComponentState.Focused
            : ComponentState.Default;
    }

    private void DetermineLabelState()
    {
        if (string.IsNullOrWhiteSpace(Label) || State == ComponentState.Hidden)
        {
            _renderLabel = false;
            _effectiveLabelPosition = LabelPosition.Hidden;
            _isPlaceholderActive = false;
            _isFloatingLabel = false;

            return;
        }

        var hasValue = HasValue;
        var focusActive = _isFocused || State == ComponentState.Focused;

        switch (LabelPosition)
        {
            case LabelPosition.PlaceholderAbove:
                if (hasValue || focusActive)
                {
                    _effectiveLabelPosition = LabelPosition.Above;
                    _isPlaceholderActive = false;
                    _isFloatingLabel = true;
                }
                else
                {
                    _effectiveLabelPosition = LabelPosition.PlaceholderAbove;
                    _isPlaceholderActive = true;
                    _isFloatingLabel = false;
                }

                break;
            case LabelPosition.PlaceholderBelow:
                if (hasValue || focusActive)
                {
                    _effectiveLabelPosition = LabelPosition.Below;
                    _isPlaceholderActive = false;
                    _isFloatingLabel = true;
                }
                else
                {
                    _effectiveLabelPosition = LabelPosition.PlaceholderBelow;
                    _isPlaceholderActive = true;
                    _isFloatingLabel = false;
                }

                break;
            case LabelPosition.PlaceholderHidden:
                if (hasValue || focusActive)
                {
                    _effectiveLabelPosition = LabelPosition.Hidden;
                    _isPlaceholderActive = false;
                    _isFloatingLabel = false;
                }
                else
                {
                    _effectiveLabelPosition = LabelPosition.PlaceholderHidden;
                    _isPlaceholderActive = true;
                    _isFloatingLabel = false;
                }

                break;
            default:
                _effectiveLabelPosition = LabelPosition;
                _isPlaceholderActive = false;
                _isFloatingLabel = false;

                break;
        }

        if (_effectiveLabelPosition == LabelPosition.Hidden)
        {
            _renderLabel = _isPlaceholderActive && LabelPosition == LabelPosition.PlaceholderHidden;
        }
        else
        {
            _renderLabel = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_watchingSystemTheme)
        {
            await StopWatchingSystemThemeAsync().ConfigureAwait(false);
        }

        if (_module is not null)
        {
            try
            {
                await _module.DisposeAsync().ConfigureAwait(false);
            }
            catch (JSDisconnectedException)
            {
                // The renderer is gone; nothing else to dispose.
            }
        }

        _selfReference?.Dispose();
    }

    private async Task EnsureSystemThemeWatcherAsync()
    {
        try
        {
            _module ??= await JS.InvokeAsync<IJSObjectReference>("import", "./ArySurface.razor.js")
                .ConfigureAwait(false);
        }
        catch (JSDisconnectedException)
        {
            return;
        }
        catch (InvalidOperationException)
        {
            return;
        }

        if (_module is null)
        {
            return;
        }

        try
        {
            var current = await _module.InvokeAsync<string>("getSystemTheme").ConfigureAwait(false);
            var parsed = ParseTheme(current);

            if (_systemThemeType != parsed)
            {
                _systemThemeType = parsed;

                if (UseSystemTheme)
                {
                    RecomputeTheme();
                    UpdateVisualState();
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                }
            }

            if (!_watchingSystemTheme)
            {
                _selfReference ??= DotNetObjectReference.Create(this);
                await _module.InvokeVoidAsync("registerSystemThemeListener", _selfReference).ConfigureAwait(false);
                _watchingSystemTheme = true;
            }
        }
        catch (JSDisconnectedException)
        {
            // Ignore disconnections during dispatch.
        }
        catch (InvalidOperationException)
        {
            // The call may fail in non-browser environments.
        }
    }

    private string GetInlinePadding(ArySpacing spacing, bool inlineStart)
        => _culture.TextInfo.IsRightToLeft
            ? inlineStart
                ? spacing.PaddingRight.Value
                : spacing.PaddingLeft.Value
            : inlineStart
                ? spacing.PaddingLeft.Value
                : spacing.PaddingRight.Value;

    private AryStyle GetStyleForState(ComponentState state)
        => _themeOverride ?? _resolvedTheme.ToStyle(
            _resolvedThemeType,
            ComponentType.Surface,
            ComponentElevation.Mid,
            state
        );

    private void HandleFocusIn(FocusEventArgs args)
    {
        if (State == ComponentState.Disabled || State == ComponentState.Hidden)
        {
            return;
        }

        if (_isFocused)
        {
            return;
        }

        _isFocused = true;
        UpdateVisualState();
        StateHasChanged();
    }

    private void HandleFocusOut(FocusEventArgs args)
    {
        if (State == ComponentState.Disabled || State == ComponentState.Hidden)
        {
            return;
        }

        if (!_isFocused)
        {
            return;
        }

        _isFocused = false;
        UpdateVisualState();
        StateHasChanged();
    }

    private static bool HasVisibleBorder(AryBorders border)
        => (border.TopWidth.Number > 0 && !string.Equals(
                border.TopStyle.Value, "none", StringComparison.OrdinalIgnoreCase
            )) ||
            (border.RightWidth.Number > 0 && !string.Equals(
                border.RightStyle.Value, "none", StringComparison.OrdinalIgnoreCase
            )) ||
            (border.BottomWidth.Number > 0 && !string.Equals(
                border.BottomStyle.Value, "none", StringComparison.OrdinalIgnoreCase
            )) ||
            (border.LeftWidth.Number > 0 && !string.Equals(
                border.LeftStyle.Value, "none", StringComparison.OrdinalIgnoreCase
            ));

    private static string MergeStyles(params string?[] segments)
    {
        var order = new List<string>();
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var segment in segments)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                continue;
            }

            var declarations = segment.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var declaration in declarations)
            {
                var parts = declaration.Split(':', 2);

                if (parts.Length != 2)
                {
                    continue;
                }

                var property = parts[0].Trim();
                var value = parts[1].Trim();

                if (property.Length == 0 || value.Length == 0)
                {
                    continue;
                }

                if (map.ContainsKey(property))
                {
                    order.RemoveAll(p => string.Equals(p, property, StringComparison.OrdinalIgnoreCase));
                }

                map[property] = value;
                order.Add(property);
            }
        }

        var builder = new StringBuilder();

        foreach (var property in order)
        {
            builder.Append(property).Append(':').Append(map[property]).Append(';');
        }

        return builder.ToString();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (UseSystemTheme)
        {
            await EnsureSystemThemeWatcherAsync().ConfigureAwait(false);
        }
        else if (_watchingSystemTheme)
        {
            await StopWatchingSystemThemeAsync().ConfigureAwait(false);
        }
    }

    protected override void OnParametersSet()
    {
        _themeOverride = Theme;
        _culture = ResolveCulture();
        RebuildAttributes();
        RecomputeTheme();
        UpdateVisualState();
    }

    [JSInvokable]
    public Task OnSystemThemeChanged(string? theme)
    {
        var parsed = ParseTheme(theme);

        if (_systemThemeType == parsed)
        {
            return Task.CompletedTask;
        }

        _systemThemeType = parsed;

        if (!UseSystemTheme)
        {
            return Task.CompletedTask;
        }

        RecomputeTheme();
        UpdateVisualState();

        return InvokeAsync(StateHasChanged);
    }

    private static ThemeType ParseTheme(string? value)
        => value switch
        {
            "dark" => ThemeType.Dark,
            "high-contrast" => ThemeType.HighContrast,
            _ => ThemeType.Light
        };

    private void RebuildAttributes()
    {
        var attributes = AdditionalAttributes is null
            ? new Dictionary<string, object?>(AttributeComparer)
            : new Dictionary<string, object?>(AdditionalAttributes, AttributeComparer);

        if (attributes.TryGetValue("class", out var classValue))
        {
            _attributeClass = Convert.ToString(classValue, CultureInfo.InvariantCulture);
            attributes.Remove("class");
        }
        else
        {
            _attributeClass = null;
        }

        if (attributes.TryGetValue("style", out var styleValue))
        {
            _attributeStyle = Convert.ToString(styleValue, CultureInfo.InvariantCulture);
            attributes.Remove("style");
        }
        else
        {
            _attributeStyle = null;
        }

        if (!attributes.ContainsKey("lang"))
        {
            attributes["lang"] = _culture.Name;
        }

        if (!attributes.ContainsKey("dir"))
        {
            attributes["dir"] = _culture.TextInfo.IsRightToLeft
                ? "rtl"
                : "ltr";
        }

        if (State == ComponentState.Disabled && !attributes.ContainsKey("aria-disabled"))
        {
            attributes["aria-disabled"] = "true";
        }

        if (State == ComponentState.ReadOnly && !attributes.ContainsKey("aria-readonly"))
        {
            attributes["aria-readonly"] = "true";
        }

        if (State == ComponentState.Hidden)
        {
            if (!attributes.ContainsKey("aria-hidden"))
            {
                attributes["aria-hidden"] = "true";
            }

            if (!attributes.ContainsKey("hidden"))
            {
                attributes["hidden"] = "hidden";
            }
        }

        _additionalAttributes = attributes;

        _ariaLabel = attributes.ContainsKey("aria-label")
            ? null
            : string.IsNullOrWhiteSpace(Label)
                ? null
                : Label;
    }

    private void RecomputeTheme()
    {
        _resolvedThemeType = ResolveThemeType();
        _resolvedTheme = BuildResolvedTheme();
        _componentStyle = BuildComponentStyle();
        _styleAttribute = MergeStyles(_componentStyle, _attributeStyle, Style);
    }

    private CultureInfo ResolveCulture()
    {
        var culture = CascadedCulture ?? CultureInfo.CurrentCulture ?? CultureInfo.GetCultureInfo("en-US");

        if (string.IsNullOrWhiteSpace(culture.Name))
        {
            return CultureInfo.GetCultureInfo("en-US");
        }

        return culture;
    }

    private ThemeType ResolveThemeType()
    {
        var type = CascadedThemeType;

        if (type == ThemeType.System)
        {
            type = _systemThemeType;
        }

        return type;
    }

    private async Task StopWatchingSystemThemeAsync()
    {
        if (_module is null || !_watchingSystemTheme || _selfReference is null)
        {
            _watchingSystemTheme = false;

            return;
        }

        try
        {
            await _module.InvokeVoidAsync("disposeThemeListener", _selfReference).ConfigureAwait(false);
        }
        catch (JSDisconnectedException)
        {
            // Renderer already disposed.
        }
        catch (InvalidOperationException)
        {
            // Invocation not available; safe to ignore.
        }

        _watchingSystemTheme = false;
    }

    private void UpdateVisualState()
    {
        DetermineLabelState();

        var activeState = DetermineActiveState();

        var dataState = State == ComponentState.Default
            ? activeState
            : State;

        _stateAttribute = dataState.ToString().ToLowerInvariant();

        _labelPositionAttribute = _renderLabel
            ? _effectiveLabelPosition.ToString()
            : LabelPosition.Hidden.ToString();

        var activeStyle = GetStyleForState(activeState);

        _labelStyle = _renderLabel
            ? BuildLabelStyle(activeStyle)
            : string.Empty;

        _labelClass = _renderLabel
            ? BuildLabelClass()
            : string.Empty;

        _classAttribute = BuildContainerClass();
    }
}
