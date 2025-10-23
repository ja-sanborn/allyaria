namespace Allyaria.Theming.Blazor.Services;

/// <summary>
/// Watches the system/application theme and notifies listeners when it changes. Wraps a co-located JavaScript module that
/// can initialize/dispose DOM listeners, detect the current theme, and notify .NET via <see cref="SetFromJs(string)" />.
/// </summary>
/// <remarks>
/// This service is safe to reuse across component lifetimes. Call
/// <see cref="StartAsync(ElementReference, CancellationToken)" /> once per host element to begin listening, and
/// <see cref="StopAsync(ElementReference, CancellationToken)" /> to stop. Always dispose with <see cref="DisposeAsync" />
/// to release JS resources.
/// </remarks>
public sealed class AryThemeWatcher : IThemeWatcher
{
    /// <summary>
    /// <see cref="DotNetObjectReference{T}" /> that enables JS to call back into this instance. Created on first
    /// <see cref="StartAsync(ElementReference, CancellationToken)" /> and disposed on
    /// <see cref="StopAsync(ElementReference, CancellationToken)" /> or <see cref="DisposeAsync" />.
    /// </summary>
    private DotNetObjectReference<AryThemeWatcher>? _dotNetRef;

    /// <summary>
    /// Indicates whether <see cref="StartAsync(ElementReference, CancellationToken)" /> has successfully initialized the JS
    /// listener.
    /// </summary>
    private bool _isStarted;

    /// <summary>JavaScript runtime used for module import and interop calls.</summary>
    private readonly IJSRuntime _js;

    /// <summary>
    /// Reference to the imported JavaScript module. Created on demand and disposed with <see cref="DisposeAsync" />.
    /// </summary>
    private IJSObjectReference? _module;

    /// <summary>
    /// The last observed <see cref="ThemeType" /> value. Defaults to <see cref="Enumerations.ThemeType.System" />.
    /// </summary>
    private ThemeType _themeType = ThemeType.System;

    /// <summary>Initializes a new instance of <see cref="AryThemeWatcher" />.</summary>
    /// <param name="js">The <see cref="IJSRuntime" /> used for JS interop.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="js" /> is <c>null</c>.</exception>
    public AryThemeWatcher(IJSRuntime js) => _js = js ?? throw new ArgumentNullException(nameof(js));

    /// <summary>
    /// Occurs when the detected theme changes. Subscribers are notified after the internal state is updated.
    /// </summary>
    public event EventHandler? Changed;

    /// <summary>Gets the last detected <see cref="ThemeType" />.</summary>
    public ThemeType ThemeType => _themeType;

    /// <summary>
    /// Detects the current system/application theme by delegating to the JS module. Updates <see cref="ThemeType" /> and
    /// raises <see cref="Changed" /> if the value differs.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for interop operations to complete.</param>
    /// <returns>The detected <see cref="ThemeType" />.</returns>
    /// <exception cref="JSException">Propagated if the underlying JS import or detect call fails.</exception>
    public async Task<ThemeType> DetectAsync(CancellationToken cancellationToken = default)
    {
        _module ??= await _js.InvokeAsync<IJSObjectReference>(
            "import",
            cancellationToken,
            "./_content/Allyaria.Theming.Blazor/Components/AryThemeProvider.razor.js"
        ).ConfigureAwait(false);

        var raw = await _module.InvokeAsync<string>("detect", cancellationToken).ConfigureAwait(false);
        var detected = ParseThemeType(raw);
        SetIfChanged(detected);

        return detected;
    }

    /// <summary>
    /// Disposes JS resources created by this watcher. Attempts to dispose the imported module and the .NET reference; swallow
    /// errors at dispose time.
    /// </summary>
    /// <returns>A <see cref="ValueTask" /> that completes when disposal finishes.</returns>
    public async ValueTask DisposeAsync()
    {
        try
        {
            _dotNetRef?.Dispose();
            _dotNetRef = null;

            if (_module is not null)
            {
                await _module.DisposeAsync().ConfigureAwait(false);
                _module = null;
            }
        }
        catch
        {
            // Swallow disposal-time failures to avoid surfacing exceptions during teardown.
        }

        _isStarted = false;
    }

    /// <summary>Parses a raw theme string to a <see cref="ThemeType" /> value.</summary>
    /// <param name="value">Raw string returned from JS (case-insensitive). May be <c>null</c>.</param>
    /// <returns>The corresponding <see cref="ThemeType" />, or <see cref="ThemeType.System" /> if unrecognized.</returns>
    private static ThemeType ParseThemeType(string? value)
        => value?.ToLowerInvariant() switch
        {
            "highcontrast" or "hc" or "forced" => ThemeType.HighContrastLight,
            "dark" => ThemeType.Dark,
            "light" => ThemeType.Light,
            _ => ThemeType.System
        };

    /// <summary>Called by JavaScript when the theme changes. Parses and applies the new value.</summary>
    /// <param name="value">Raw theme string from JS (e.g., "dark", "light", "hc").</param>
    [JSInvokable]
    public void SetFromJs(string value)
    {
        var parsed = ParseThemeType(value);
        SetIfChanged(parsed);
    }

    /// <summary>Sets the internal theme state and raises <see cref="Changed" /> if the value differs.</summary>
    /// <param name="next">The next theme to apply.</param>
    private void SetIfChanged(ThemeType next)
    {
        if (_themeType == next)
        {
            return;
        }

        _themeType = next;
        Changed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Starts listening for theme changes for the provided host element. Safe to call multiple times; subsequent calls are
    /// no-ops until <see cref="StopAsync(ElementReference, CancellationToken)" /> is invoked.
    /// </summary>
    /// <param name="host">Host element that scopes any DOM listeners created by the JS module.</param>
    /// <param name="cancellationToken">A token to observe while waiting for interop operations to complete.</param>
    /// <returns>A task that completes when initialization finishes.</returns>
    /// <exception cref="JSException">Propagated if the underlying JS import or initialization fails.</exception>
    public async Task StartAsync(ElementReference host, CancellationToken cancellationToken = default)
    {
        if (_isStarted)
        {
            return;
        }

        _module ??= await _js.InvokeAsync<IJSObjectReference>(
            "import",
            cancellationToken,
            "./_content/Allyaria.Theming.Blazor/Components/AryThemeProvider.razor.js"
        ).ConfigureAwait(false);

        _dotNetRef ??= DotNetObjectReference.Create(this);

        await _module.InvokeVoidAsync("init", cancellationToken, host, _dotNetRef).ConfigureAwait(false);

        _isStarted = true;
    }

    /// <summary>
    /// Unsupported overload. Use <see cref="StartAsync(ElementReference, CancellationToken)" /> to provide a host element.
    /// </summary>
    /// <param name="cancellationToken">Unused.</param>
    /// <returns>Never returns; always throws.</returns>
    /// <exception cref="AryArgumentException">Thrown to indicate a required <c>host</c> was not supplied.</exception>
    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        AryArgumentException.ThrowIfNull(null, "host");

        return Task.CompletedTask;
    }

    /// <summary>
    /// Stops listening for theme changes for the provided host element. Safe to call if not started. Disposes the
    /// <see cref="DotNetObjectReference{T}" /> so the instance can be GC'ed if no longer referenced.
    /// </summary>
    /// <param name="host">Host element previously passed to <see cref="StartAsync(ElementReference, CancellationToken)" />.</param>
    /// <param name="cancellationToken">A token to observe while waiting for interop operations to complete.</param>
    /// <returns>A task that completes when disposal on the JS side finishes.</returns>
    public async Task StopAsync(ElementReference host, CancellationToken cancellationToken = default)
    {
        if (!_isStarted)
        {
            return;
        }

        if (_module is not null)
        {
            await _module.InvokeVoidAsync("dispose", cancellationToken, host).ConfigureAwait(false);
        }

        _dotNetRef?.Dispose();
        _dotNetRef = null;
        _isStarted = false;
    }

    /// <summary>
    /// Unsupported overload. Use <see cref="StopAsync(ElementReference, CancellationToken)" /> to provide a host element.
    /// </summary>
    /// <param name="cancellationToken">Unused.</param>
    /// <returns>Never returns; always throws.</returns>
    /// <exception cref="AryArgumentException">Thrown to indicate a required <c>host</c> was not supplied.</exception>
    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        AryArgumentException.ThrowIfNull(null, "host");

        return Task.CompletedTask;
    }
}
