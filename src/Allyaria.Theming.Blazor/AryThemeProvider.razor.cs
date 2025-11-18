namespace Allyaria.Theming.Blazor;

/// <summary>
/// Provides a Blazor component that connects the <see cref="IThemingService" /> to the browser, handling system theme
/// detection, persisted theme preference, and document direction/language.
/// </summary>
/// <remarks>
///     <para>
///     This component is intended to be placed near the root of the app and wraps the rest of the UI via
///     <see cref="ChildContent" />. It:
///     </para>
///     <list type="bullet">
///         <item>
///             <description>
///             Synchronizes the effective theme with OS/browser preferences when <see cref="ThemeType.System" /> is
///             selected.
///             </description>
///         </item>
///         <item>
///             <description>Persists the stored theme type in browser storage.</description>
///         </item>
///         <item>
///             <description>
///             Updates <c>dir</c> and <c>lang</c> attributes on <c>document.documentElement</c>, and toggles a <c>rtl</c>
///             class on <c>body</c>.
///             </description>
///         </item>
///     </list>
/// </remarks>
public sealed partial class AryThemeProvider : ComponentBase, IAsyncDisposable
{
    /// <summary>Local storage key used to persist the user's theme preference.</summary>
    private const string StorageKey = "allyaria.themeType";

    /// <summary>
    /// Cancellation token source used to cancel ongoing asynchronous operations when the component is disposed.
    /// </summary>
    private CancellationTokenSource? _cts;

    /// <summary>DotNet object reference used by JavaScript interop to call back into this component.</summary>
    private DotNetObjectReference<AryThemeProvider>? _dotNetRef;

    /// <summary>Tracks the current effective theme type applied by this provider.</summary>
    /// <remarks>
    /// This value mirrors <see cref="IThemingService.EffectiveType" />, but is cached locally to avoid redundant updates.
    /// </remarks>
    private ThemeType _effectiveType;

    /// <summary>Host element reference for this component, used as the root for JS interop initialization.</summary>
    private ElementReference _host;

    /// <summary>Indicates whether system theme detection is currently initialized and listening for changes.</summary>
    private bool _isStarted;

    /// <summary>
    /// Tracks the previously applied culture in order to detect when the effective UI culture changes. Used to avoid redundant
    /// direction and language updates to the document root.
    /// </summary>
    private CultureInfo? _lastCulture;

    /// <summary>JavaScript module reference for the co-located <c>AryThemeProvider.razor.js</c> file.</summary>
    private IJSObjectReference? _module;

    /// <summary>Cached copy of the stored theme type used for persistence and detection lifecycle management.</summary>
    private ThemeType _storedType = ThemeType.System;

    /// <summary>Gets or sets the child content that will be rendered within this theme provider.</summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>Gets or sets the culture used to determine document direction and language attributes.</summary>
    /// <remarks>When not supplied, <see cref="CultureInfo.CurrentUICulture" /> is used as a fallback.</remarks>
    [CascadingParameter]
    public CultureInfo? Culture { get; set; }

    /// <summary>Gets the document-level CSS for the current effective theme.</summary>
    /// <remarks>
    /// This value is intended for consumption by the Razor markup to inject global CSS (for example, via a co-located &lt;
    /// style&gt; block managed by this component).
    /// </remarks>
    private string GlobalCss => ThemingService.GetDocumentCss();

    /// <summary>Gets the JavaScript runtime used for interop with the browser environment.</summary>
    [Inject]
    public required IJSRuntime JsRuntime { get; init; }

    /// <summary>Gets the theming service that maintains the current stored and effective theme types.</summary>
    [Inject]
    public required IThemingService ThemingService { get; init; }

    /// <summary>Detects the current system theme using the co-located JavaScript module.</summary>
    /// <param name="cancellationToken">A token used to cancel the detection operation.</param>
    /// <returns>
    /// A <see cref="ThemeType" /> corresponding to the detected system theme, or <c>null</c> when detection fails or the
    /// result is <see cref="ThemeType.System" />.
    /// </returns>
    public async Task<ThemeType?> DetectThemeAsync(CancellationToken cancellationToken = default)
    {
        if (_module is null || IsCancellationRequested())
        {
            return null;
        }

        try
        {
            var raw = await _module
                .InvokeAsync<string>(identifier: "detect", cancellationToken: cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);

            var parsed = ParseThemeType(value: raw);

            return parsed is ThemeType.System
                ? null
                : parsed;
        }
        catch
        {
            // On any JS interop failure, fall back to "no detection".
            return null;
        }
    }

    /// <summary>
    /// Asynchronously disposes the component, cancelling outstanding work and releasing JS interop resources.
    /// </summary>
    /// <returns>A task representing the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync()
    {
        try
        {
            ThemingService.ThemeChanged -= OnThemeChangedAsync;

            if (_cts is not null)
            {
                await _cts.CancelAsync().ConfigureAwait(continueOnCapturedContext: false);
                _cts.Dispose();
                _cts = null;
            }

            if (_module is not null)
            {
                try
                {
                    await _module.InvokeVoidAsync(identifier: "dispose", _host)
                        .ConfigureAwait(continueOnCapturedContext: false);
                }
                catch
                {
                    // Ignore teardown errors; they can race with navigation/unload.
                }

                await _module.DisposeAsync().ConfigureAwait(continueOnCapturedContext: false);
                _module = null;
            }

            _dotNetRef?.Dispose();
            _dotNetRef = null;
        }
        catch
        {
            // Swallow dispose-time failures to avoid surfacing teardown exceptions to the host.
        }
    }

    /// <summary>Attempts to load a previously stored theme type from the browser using the JS module.</summary>
    /// <param name="cancellationToken">A token used to cancel the retrieval operation.</param>
    /// <returns>
    /// The stored <see cref="ThemeType" /> if available and valid; otherwise <see cref="ThemeType.System" />.
    /// </returns>
    private async Task<ThemeType> GetStoredTypeAsync(CancellationToken cancellationToken = default)
    {
        if (_module is null || IsCancellationRequested())
        {
            return ThemeType.System;
        }

        try
        {
            var raw = await _module
                .InvokeAsync<string?>(
                    identifier: "getStoredTheme",
                    cancellationToken: cancellationToken,
                    StorageKey
                )
                .ConfigureAwait(continueOnCapturedContext: false);

            return Enum.TryParse(value: raw, ignoreCase: true, result: out ThemeType parsed)
                ? parsed
                : ThemeType.System;
        }
        catch
        {
            // On any JS failure, treat as "no stored value".
            return ThemeType.System;
        }
    }

    /// <summary>Determines whether cancellation has been requested for this component's operations.</summary>
    /// <returns>
    /// <c>true</c> if there is no active <see cref="CancellationTokenSource" /> or the current token has been cancelled;
    /// otherwise, <c>false</c>.
    /// </returns>
    private bool IsCancellationRequested()
    {
        var cts = _cts;

        return cts is null || cts.IsCancellationRequested;
    }

    /// <summary>
    /// Performs post-render initialization, wiring up JS interop, synchronizing stored theme, and setting document direction.
    /// </summary>
    /// <param name="firstRender">
    /// <c>true</c> when this is the first time the component has been rendered; otherwise,
    /// <c>false</c>.
    /// </param>
    /// <returns>A task that completes when initialization steps have finished.</returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        ThemingService.ThemeChanged += OnThemeChangedAsync;

        _cts = new CancellationTokenSource();

        _module ??= await JsRuntime
            .InvokeAsync<IJSObjectReference>(
                identifier: "import",
                cancellationToken: _cts.Token,
                "./AryThemeProvider.razor.js"
            )
            .ConfigureAwait(continueOnCapturedContext: false);

        var storedType = await GetStoredTypeAsync(cancellationToken: _cts.Token)
            .ConfigureAwait(continueOnCapturedContext: false);

        // Always adopt the stored theme when it differs from the current service value,
        // including ThemeType.System, so that ThemingService.StoredType is driven by browser storage.
        if (storedType != ThemingService.StoredType)
        {
            ThemingService.SetStoredType(themeType: storedType);
        }

        if (ThemingService.StoredType == ThemeType.System)
        {
            await StartDetectAsync(cancellationToken: _cts.Token)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        var detected = ThemingService.StoredType == ThemeType.System
            ? await DetectThemeAsync(cancellationToken: _cts.Token)
                .ConfigureAwait(continueOnCapturedContext: false)
            : null;

        var effectiveType = detected ?? (ThemingService.StoredType == ThemeType.System
            ? ThemeType.Light
            : ThemingService.StoredType);

        UpdateEffectiveType(effectiveType: effectiveType);

        await UpdateStoredTypeAsync(
                storedType: ThemingService.StoredType,
                cancellationToken: _cts.Token
            )
            .ConfigureAwait(continueOnCapturedContext: false);

        await SetDirectionAsync().ConfigureAwait(continueOnCapturedContext: false);
    }

    /// <summary>
    /// Invoked by the framework when parameter values or cascading values change. Detects whether the effective UI culture has
    /// changed (either through <see cref="Culture" /> or <see cref="CultureInfo.CurrentUICulture" />) and, when it has,
    /// updates document direction and language attributes by calling <see cref="SetDirectionAsync" />.
    /// </summary>
    /// <remarks>
    /// This method ensures RTL/LTR changes are applied reactively when the application culture updates via
    /// <c>CascadingLocalization</c>.
    /// </remarks>
    /// <param name="firstRender">Unused. Included for signature consistency.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        var currentCulture = Culture ?? CultureInfo.CurrentUICulture;

        if (_lastCulture?.Name != currentCulture.Name)
        {
            _lastCulture = currentCulture;

            await SetDirectionAsync();
        }
    }

    /// <summary>
    /// Handles <see cref="IThemingService.ThemeChanged" /> events, synchronizing local state and requesting re-render.
    /// </summary>
    /// <param name="sender">The source of the event (typically the <see cref="IThemingService" /> instance).</param>
    /// <param name="e">Event data (unused).</param>
    /// <remarks>
    /// This method is intentionally <c>async void</c> to conform to the event handler pattern. It avoids throwing exceptions
    /// to the Blazor renderer, instead catching and swallowing failures from asynchronous work.
    /// </remarks>
    private async void OnThemeChangedAsync(object? sender, EventArgs e)
    {
        var cts = _cts;

        if (cts is null || cts.IsCancellationRequested)
        {
            return;
        }

        try
        {
            if (_storedType != ThemingService.StoredType)
            {
                await UpdateStoredTypeAsync(
                        storedType: ThemingService.StoredType,
                        cancellationToken: cts.Token
                    )
                    .ConfigureAwait(continueOnCapturedContext: false);
            }

            var effectiveType = ThemingService.EffectiveType;

            if (_effectiveType == effectiveType)
            {
                return;
            }

            _effectiveType = effectiveType;
            await InvokeAsync(workItem: StateHasChanged);
        }
        catch
        {
            // Intentionally ignore errors from theme change propagation to avoid breaking the render loop.
        }
    }

    /// <summary>Parses a raw theme type string into a <see cref="ThemeType" /> value, handling known aliases.</summary>
    /// <param name="value">The raw string value returned from JavaScript.</param>
    /// <returns>
    /// The corresponding <see cref="ThemeType" /> when recognized; otherwise <see cref="ThemeType.System" />.
    /// </returns>
    private static ThemeType ParseThemeType(string? value)
        => value?.ToLowerInvariant() switch
        {
            "highcontrast" or "hc" or "forced" or "highcontrastlight" or "hcl" => ThemeType.HighContrastLight,
            "highcontrastdark" or "hcd" => ThemeType.HighContrastDark,
            "dark" => ThemeType.Dark,
            "light" => ThemeType.Light,
            _ => ThemeType.System
        };

    /// <summary>Sets the document direction and language attributes based on the current or cascaded culture.</summary>
    /// <returns>A task that completes when the JS interop calls have finished or failed.</returns>
    /// <remarks>
    /// This method does not currently support cancellation because it is short-lived and best-effort. It silently ignores
    /// failures (for example, during prerendering when JS is not available).
    /// </remarks>
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
                identifier: "(d,cls,add)=>add?d.body.classList.add(cls):d.body.classList.remove(cls)",
                "document",
                "rtl",
                dir == "rtl"
            );
        }
        catch
        {
            // Code Coverage: Unreachable code. Ignore if JS is not ready (e.g., during prerendering).
        }
    }

    /// <summary>Receives theme updates from JavaScript when system theme detection fires.</summary>
    /// <param name="raw">The raw theme string supplied by the JS module.</param>
    [JSInvokable]
    public void SetFromJs(string raw)
    {
        var effectiveType = ParseThemeType(value: raw);
        UpdateEffectiveType(effectiveType: effectiveType);
    }

    /// <summary>Persists the current stored theme type to browser storage via the JS module.</summary>
    /// <param name="cancellationToken">A token used to cancel the storage operation.</param>
    /// <returns>A task that completes when the value has been sent to JS or an error has been ignored.</returns>
    private async Task SetStoredTypeAsync(CancellationToken cancellationToken = default)
    {
        if (_module is null || IsCancellationRequested())
        {
            return;
        }

        try
        {
            _ = await _module
                .InvokeAsync<bool>(
                    identifier: "setStoredTheme",
                    cancellationToken: cancellationToken,
                    StorageKey,
                    _storedType.ToString()
                )
                .ConfigureAwait(continueOnCapturedContext: false);
        }
        catch
        {
            // Code Coverage: Unreachable code. Intentionally ignored: storage may be blocked or unavailable.
        }
    }

    /// <summary>Starts system theme detection via the JS module if it is not already active.</summary>
    /// <param name="cancellationToken">A token used to cancel the initialization operation.</param>
    /// <returns>A task that completes when initialization has finished or failed.</returns>
    private async Task StartDetectAsync(CancellationToken cancellationToken = default)
    {
        if (_isStarted || _module is null || IsCancellationRequested())
        {
            // Code coverage: Unreachable code.
            return;
        }

        try
        {
            _dotNetRef ??= DotNetObjectReference.Create(value: this);

            await _module
                .InvokeVoidAsync(
                    identifier: "init",
                    cancellationToken: cancellationToken,
                    _host,
                    _dotNetRef
                )
                .ConfigureAwait(continueOnCapturedContext: false);

            _isStarted = true;
        }
        catch
        {
            _dotNetRef?.Dispose();
            _dotNetRef = null;
            _isStarted = false;
        }
    }

    /// <summary>Stops system theme detection and detaches JS listeners.</summary>
    /// <returns>A task that completes when listeners have been detached or an error has been ignored.</returns>
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
                await _module.InvokeVoidAsync(identifier: "dispose", _host)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }
        catch
        {
            // Code coverage: Unreachable code. Ignore failures during teardown.
        }
        finally
        {
            _dotNetRef?.Dispose();
            _dotNetRef = null;
            _isStarted = false;
        }
    }

    /// <summary>Updates the effective theme type both locally and in the theming service when it has changed.</summary>
    /// <param name="effectiveType">The new effective <see cref="ThemeType" /> to apply.</param>
    private void UpdateEffectiveType(ThemeType effectiveType)
    {
        if (_effectiveType == effectiveType)
        {
            return;
        }

        _effectiveType = effectiveType;
        ThemingService.SetEffectiveType(themeType: effectiveType);
    }

    /// <summary>Updates the stored theme type, managing detection lifecycle and persistence.</summary>
    /// <param name="storedType">The new stored <see cref="ThemeType" /> value.</param>
    /// <param name="cancellationToken">A token used to cancel JS-backed operations.</param>
    /// <returns>A task that completes when detection and storage updates have finished.</returns>
    private async Task UpdateStoredTypeAsync(ThemeType storedType, CancellationToken cancellationToken = default)
    {
        if (_storedType == storedType)
        {
            return;
        }

        _storedType = storedType;

        if (_storedType is ThemeType.System)
        {
            await StartDetectAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
        }
        else
        {
            await StopDetectAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        await SetStoredTypeAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);
    }
}
