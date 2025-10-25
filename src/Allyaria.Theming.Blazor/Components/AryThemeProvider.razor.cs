namespace Allyaria.Theming.Blazor.Components;

public sealed partial class AryThemeProvider : ComponentBase, IAsyncDisposable
{
    private const string StorageKey = "allyaria.themeType";

    private readonly string _css = string.Empty;

    private CancellationTokenSource? _cts;

    private DotNetObjectReference<AryThemeProvider>? _dotNetRef;

    private ThemeType _effectiveType;

    private ElementReference _host;

    private bool _isStarted;

    private IJSObjectReference? _module;

    private ThemeType _storedType = ThemeType.System;

    private Theme _theme;

    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    [CascadingParameter]
    public CultureInfo? Culture { get; set; }

    [Inject]
    public required IJSRuntime JsRuntime { get; init; }

    [Inject]
    public required IThemingService ThemingService { get; init; }

    //private void BuildCss()
    //{
    //    // TODO: Link Colors
    //    var link = ThemeProvider.GetStyle(ComponentType.Surface);
    //    var linkFocus = ThemeProvider.GetStyle(ComponentType.Surface, state: ComponentState.Focused);
    //    var linkHover = ThemeProvider.GetStyle(ComponentType.Surface, state: ComponentState.Hovered);
    //    var linkPressed = ThemeProvider.GetStyle(ComponentType.Surface, state: ComponentState.Pressed);
    //    var surface = ThemeProvider.GetStyle(ComponentType.Surface, ComponentElevation.Low);
    //    var output = new StringBuilder();

    //    // Root resets and color scheme
    //    output.Append(":root");
    //    output.Append("{");
    //    output.Append("color-scheme:light dark;");

    //    // Define the global variables
    //    output.Append(ThemeProvider.GetCss());
    //    output.Append("}");

    //    // Html and Body global
    //    output.Append("html");
    //    output.Append("{");
    //    output.Append("box-sizing:border-box;");
    //    output.Append("-webkit-text-size-adjust:100%;");
    //    output.Append("text-size-adjust:100%;");
    //    output.Append("}");

    //    output.Append("*,*::before,*::after");
    //    output.Append("{");
    //    output.Append("box-sizing:inherit;");
    //    output.Append("}");

    //    output.Append("html,body");
    //    output.Append("{");
    //    output.Append("min-height:100%;");
    //    output.Append("}");

    //    output.Append("body");
    //    output.Append("{");

    //    // Typography and Colors
    //    output.Append(surface.Palette.ToCss());
    //    output.Append(surface.Typography.ToCss());
    //    output.Append("text-rendering:optimizeLegibility;");
    //    output.Append("-webkit-font-smoothing:antialiased;");
    //    output.Append("-moz-osx-font-smoothing:auto;");

    //    // Layout and Behavior
    //    output.Append("margin:0;");
    //    output.Append("padding:0;");
    //    output.Append("width:100%;");
    //    output.Append("overflow-x:clip;");

    //    // Scroll and Mobile
    //    output.Append("scroll-behavior:smooth;");
    //    output.Append("-webkit-tap-highlight-color:transparent;");
    //    output.Append("}");

    //    // Focus Visibility
    //    output.Append(":focus-visible");
    //    output.Append("{");
    //    output.Append(surface.Palette.ToCss());
    //    output.Append("outline-width:2px;");
    //    output.Append("outline-style:solid;");
    //    output.Append(surface.Palette.AccentColor.ToCss("outline-color"));
    //    output.Append("outline-offset:3px;");
    //    output.Append("}");

    //    // Links
    //    output.Append("a");
    //    output.Append("{");
    //    output.Append(link.Palette.ToCss(includeBackground: false));
    //    output.Append(link.Typography.ToCss(includeSize: false));
    //    output.Append("text-underline-offset:0.15em;");
    //    output.Append("transition: all 0.15s ease;");
    //    output.Append("}");

    //    output.Append("a:visited");
    //    output.Append("{");
    //    output.Append(linkPressed.Palette.ToCss(includeBackground: false));
    //    output.Append("}");

    //    output.Append("a:hover");
    //    output.Append("{");
    //    output.Append(linkHover.Palette.ToCss());
    //    output.Append("text-decoration-line:underline;");
    //    output.Append("text-decoration-thickness:2px;");
    //    output.Append("}");

    //    output.Append("a:focus-visible");
    //    output.Append("{");
    //    output.Append(linkFocus.Palette.ToCss(includeBackground: false));
    //    output.Append(linkFocus.Palette.AccentColor.ToCss("outline-color"));
    //    output.Append("text-decoration:none;");
    //    output.Append("}");

    //    output.Append("a:active");
    //    output.Append("{");
    //    output.Append(linkPressed.Palette.ToCss(includeBackground: false));
    //    output.Append("text-decoration:none;");
    //    output.Append("}");

    //    // Reduced Motion
    //    output.Append("@media(prefers-reduced-motion:reduce)");
    //    output.Append("{");
    //    output.Append("*");
    //    output.Append("{");
    //    output.Append("animation:none !important;");
    //    output.Append("transition:none !important;");
    //    output.Append("}");
    //    output.Append("html,body");
    //    output.Append("{");
    //    output.Append("scroll-behavior:auto !important;");
    //    output.Append("}");
    //    output.Append("}");

    //    // Output the global CSS values
    //    _css = output.ToString();

    //    try
    //    {
    //        StateHasChanged();
    //    }
    //    catch
    //    {
    //        _ = InvokeAsync(StateHasChanged);
    //    }
    //}

    public async Task<ThemeType?> DetectThemeAsync(CancellationToken cancellationToken = default)
    {
        if (_module is null || IsCancellationRequested())
        {
            return null;
        }

        try
        {
            var raw = await _module.InvokeAsync<string>(identifier: "detect", cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            var parsed = ParseThemeType(raw);

            return parsed is ThemeType.System
                ? null
                : parsed;
        }
        catch
        {
            return null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            ThemingService.ThemeChanged -= OnThemeChangedAsync;

            if (_cts is not null)
            {
                await _cts.CancelAsync();
                _cts.Dispose();
                _cts = null;
            }

            if (_module is not null)
            {
                await _module.InvokeVoidAsync(identifier: "dispose", _host);
                await _module.DisposeAsync();
                _module = null;
            }

            _dotNetRef?.Dispose();
            _dotNetRef = null;
        }
        catch
        {
            // swallow dispose-time failures
        }
    }

    private async Task<ThemeType> GetStoredTypeAsync(CancellationToken cancellationToken = default)
    {
        if (_module is null || IsCancellationRequested())
        {
            return ThemeType.System;
        }

        try
        {
            var raw = await _module.InvokeAsync<string?>(
                    identifier: "getStoredTheme", cancellationToken: cancellationToken, StorageKey
                )
                .ConfigureAwait(false);

            return Enum.TryParse(value: raw, ignoreCase: true, result: out ThemeType parsed)
                ? parsed
                : ThemeType.System;
        }
        catch
        {
            return ThemeType.System;
        }
    }

    private bool IsCancellationRequested() => _cts is null || _cts.IsCancellationRequested;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        ThemingService.ThemeChanged += OnThemeChangedAsync;

        _cts = new CancellationTokenSource();

        _module ??= await JsRuntime.InvokeAsync<IJSObjectReference>(
                identifier: "import", cancellationToken: _cts.Token, "./AryThemeProvider.razor.js"
            )
            .ConfigureAwait(false);

        _theme = ThemingService.Theme;

        var storedType = await GetStoredTypeAsync(_cts.Token)
            .ConfigureAwait(false);

        if (storedType != ThemeType.System || ThemingService.StoredType != ThemeType.System)
        {
            ThemingService.SetStoredType(storedType);
        }

        if (ThemingService.StoredType == ThemeType.System)
        {
            await StartDetectAsync(_cts.Token).ConfigureAwait(false);
        }

        var detected = ThemingService.StoredType == ThemeType.System
            ? await DetectThemeAsync(_cts.Token).ConfigureAwait(false)
            : null;

        var effectiveType = detected ?? (ThemingService.StoredType == ThemeType.System
            ? ThemeType.Light
            : ThemingService.StoredType);

        UpdateEffectiveType(effectiveType);

        await UpdateStoredTypeAsync(storedType: ThemingService.StoredType, cancellationToken: _cts.Token)
            .ConfigureAwait(false);

        await SetDirectionAsync().ConfigureAwait(false);
    }

    private async void OnThemeChangedAsync(object? sender, EventArgs e)
    {
        if (IsCancellationRequested())
        {
            return;
        }

        _theme = ThemingService.Theme;

        if (_storedType != ThemingService.StoredType)
        {
            await UpdateStoredTypeAsync(storedType: ThemingService.StoredType, cancellationToken: _cts!.Token);
        }

        if (_effectiveType == ThemingService.EffectiveType)
        {
            return;
        }

        var effectiveType = ThemingService.EffectiveType;

        if (effectiveType is ThemeType.System)
        {
            var detectedType = await DetectThemeAsync(_cts!.Token)
                .ConfigureAwait(false);

            effectiveType = detectedType ?? ThemeType.Light;
        }

        _effectiveType = effectiveType;
        ThemingService.SetEffectiveType(effectiveType);
    }

    private static ThemeType ParseThemeType(string? value)
        => value?.ToLowerInvariant() switch
        {
            "highcontrast" or "hc" or "forced" or "highcontrastlight" or "hcl" => ThemeType.HighContrastLight,
            "highcontrastdark" or "hcd" => ThemeType.HighContrastDark,
            "dark" => ThemeType.Dark,
            "light" => ThemeType.Light,
            _ => ThemeType.System
        };

    public async Task SetDirectionAsync()
    {
        var culture = Culture ?? CultureInfo.CurrentUICulture;

        var dir = culture.TextInfo.IsRightToLeft
            ? "rtl"
            : "ltr";

        var lang = culture.IetfLanguageTag;

        try
        {
            await JsRuntime.InvokeVoidAsync(identifier: "document.documentElement.setAttribute", "dir", dir);
            await JsRuntime.InvokeVoidAsync(identifier: "document.documentElement.setAttribute", "lang", lang);

            await JsRuntime.InvokeVoidAsync(
                identifier: "(d,cls,add)=>add?d.body.classList.add(cls):d.body.classList.remove(cls)", "document",
                "rtl",
                dir == "rtl"
            );
        }
        catch
        {
            // ignore if JS not ready (e.g., prerender)
        }
    }

    [JSInvokable]
    public void SetFromJs(string raw)
    {
        var effectiveType = ParseThemeType(raw);
        UpdateEffectiveType(effectiveType);
    }

    private async Task SetStoredTypeAsync(CancellationToken cancellationToken = default)
    {
        if (_module is null || IsCancellationRequested())
        {
            return;
        }

        try
        {
            await _module.InvokeAsync<bool>(
                    identifier: "setStoredTheme", cancellationToken: cancellationToken, StorageKey,
                    _storedType.ToString()
                )
                .ConfigureAwait(false);
        }
        catch
        {
            // Ignored intentionally.
        }
    }

    private async Task StartDetectAsync(CancellationToken cancellationToken = default)
    {
        if (_isStarted || _module is null || IsCancellationRequested())
        {
            return;
        }

        try
        {
            _dotNetRef ??= DotNetObjectReference.Create(this);

            await _module.InvokeVoidAsync(identifier: "init", cancellationToken: cancellationToken, _host, _dotNetRef)
                .ConfigureAwait(false);

            _isStarted = true;
        }
        catch
        {
            _dotNetRef?.Dispose();
            _dotNetRef = null;
            _isStarted = false;
        }
    }

    private async Task StopDetectAsync()
    {
        if (!_isStarted || IsCancellationRequested())
        {
            return;
        }

        try
        {
            if (_module is not null)
            {
                await _module.InvokeVoidAsync(identifier: "dispose", _host);
            }
        }
        catch
        {
            // Swallow teardown failures; may race with navigation/dispose.
        }
        finally
        {
            _dotNetRef?.Dispose();
            _dotNetRef = null;
            _isStarted = false;
        }
    }

    private void UpdateEffectiveType(ThemeType effectiveType)
    {
        if (_effectiveType == effectiveType)
        {
            return;
        }

        _effectiveType = effectiveType;
        ThemingService.SetEffectiveType(effectiveType);
    }

    private async Task UpdateStoredTypeAsync(ThemeType storedType, CancellationToken cancellationToken = default)
    {
        if (_storedType == storedType)
        {
            return;
        }

        _storedType = storedType;

        if (_storedType is ThemeType.System)
        {
            await StartDetectAsync(cancellationToken)
                .ConfigureAwait(false);
        }
        else
        {
            await StopDetectAsync().ConfigureAwait(false);
        }

        await SetStoredTypeAsync(cancellationToken).ConfigureAwait(false);
    }
}
