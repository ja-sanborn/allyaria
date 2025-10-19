using Allyaria.Theming.Blazor.Services;

namespace Allyaria.Theming.Blazor.Components;

/// <summary>
/// Provides centralized theme coordination between .NET, JS, and persisted storage. Handles system theme detection,
/// persistence, and synchronization across user preferences.
/// </summary>
public sealed partial class AryThemeProvider : ComponentBase, IAsyncDisposable
{
    /// <summary>The local storage key used to persist the selected theme type across sessions.</summary>
    private const string StorageKey = "allyaria.themeType";

    /// <summary>
    /// Indicates whether the provider is currently applying a system-detected theme to prevent recursive theme change events.
    /// </summary>
    private bool _applyingSystemEffective;

    /// <summary>The global CSS values.</summary>
    private string _css = string.Empty;

    /// <summary>
    /// The active <see cref="CancellationTokenSource" /> controlling asynchronous operations and allowing cooperative
    /// cancellation during teardown.
    /// </summary>
    private CancellationTokenSource? _cts;

    /// <summary>
    /// The root element reference for the component, used for JS interop bindings and element-scoped event handling.
    /// </summary>
    private ElementReference _host;

    /// <summary>The imported JavaScript module providing theme detection and persistence helpers.</summary>
    private IJSObjectReference? _module;

    /// <summary>Tracks the previous persisted state to detect transitions in user persistence preferences.</summary>
    private bool _prevPersisted;

    /// <summary>Gets or sets the render fragment representing the child content.</summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>Gets or sets the <see cref="IJSRuntime" /> for JavaScript interop calls.</summary>
    [Inject]
    public required IJSRuntime JsRuntime { get; init; }

    /// <summary>Gets or sets the <see cref="IThemePersistence" /> used to persist theme choices.</summary>
    [Inject]
    public required IThemePersistence Persistence { get; init; }

    /// <summary>Gets or sets the <see cref="IThemeProvider" /> used for applying and retrieving theme information.</summary>
    [Inject]
    public required IThemeProvider ThemeProvider { get; init; }

    /// <summary>Gets or sets the <see cref="IThemeWatcher" /> used to detect and watch system theme changes.</summary>
    [Inject]
    public required IThemeWatcher ThemeWatcher { get; init; }

    /// <summary>Rebuilds the global CSS values from the current theme.</summary>
    private void BuildCss()
    {
        // TODO: Link Colors
        var link = ThemeProvider.GetStyle(ComponentType.Surface);
        var linkFocus = ThemeProvider.GetStyle(ComponentType.Surface, state: ComponentState.Focused);
        var linkHover = ThemeProvider.GetStyle(ComponentType.Surface, state: ComponentState.Hovered);
        var linkPressed = ThemeProvider.GetStyle(ComponentType.Surface, state: ComponentState.Pressed);
        var surface = ThemeProvider.GetStyle(ComponentType.Surface, ComponentElevation.Low);
        var output = new StringBuilder();

        // Define the global variables
        output.Append(ThemeProvider.GetCss());

        // Root resets and color scheme
        output.Append(":root");
        output.Append("{");
        output.Append("color-scheme:light dark;");
        output.Append("}");

        // Html and Body global
        output.Append("html");
        output.Append("{");
        output.Append("box-sizing:border-box;");
        output.Append("-webkit-text-size-adjust:100%;");
        output.Append("text-size-adjust:100%;");
        output.Append("}");

        output.Append("*,*::before,*::after");
        output.Append("{");
        output.Append("box-sizing:inherit;");
        output.Append("}");

        output.Append("html,body");
        output.Append("{");
        output.Append("min-height:100%;");
        output.Append("}");

        output.Append("body");
        output.Append("{");

        // Typography and Colors
        output.Append(surface.Palette.ToCss());
        output.Append(surface.Typography.ToCss());
        output.Append("text-rendering:optimizeLegibility;");
        output.Append("-webkit-font-smoothing:antialiased;");
        output.Append("-moz-osx-font-smoothing:auto;");

        // Layout and Behavior
        output.Append("margin:0;");
        output.Append("padding:0;");
        output.Append("width:100%;");
        output.Append("overflow-x:clip;");

        // Scroll and Mobile
        output.Append("scroll-behavior:smooth;");
        output.Append("-webkit-tap-highlight-color:transparent;");
        output.Append("}");

        // Focus Visibility
        output.Append(":focus-visible");
        output.Append("{");
        output.Append(surface.Palette.ToCss());
        output.Append("outline-width:2px;");
        output.Append("outline-style:solid;");
        output.Append(surface.Palette.AccentColor.ToCss("outline-color"));
        output.Append("outline-offset:3px;");
        output.Append("}");

        // Links
        output.Append("a");
        output.Append("{");
        output.Append(link.Palette.ToCss(includeBackground: false));
        output.Append(link.Typography.ToCss(includeSize: false));
        output.Append("text-underline-offset:0.15em;");
        output.Append("transition: all 0.15s ease;");
        output.Append("}");

        output.Append("a:visited");
        output.Append("{");
        output.Append(linkPressed.Palette.ToCss(includeBackground: false));
        output.Append("}");

        output.Append("a:hover");
        output.Append("{");
        output.Append(linkHover.Palette.ToCss());
        output.Append("text-decoration-line:underline;");
        output.Append("text-decoration-thickness:2px;");
        output.Append("}");

        output.Append("a:focus-visible");
        output.Append("{");
        output.Append(linkFocus.Palette.ToCss(includeBackground: false));
        output.Append(linkFocus.Palette.AccentColor.ToCss("outline-color"));
        output.Append("text-decoration:none;");
        output.Append("}");

        output.Append("a:active");
        output.Append("{");
        output.Append(linkPressed.Palette.ToCss(includeBackground: false));
        output.Append("text-decoration:none;");
        output.Append("}");

        // Reduced Motion
        output.Append("@media(prefers-reduced-motion:reduce)");
        output.Append("{");
        output.Append("*");
        output.Append("{");
        output.Append("animation:none !important;");
        output.Append("transition:none !important;");
        output.Append("}");
        output.Append("html,body");
        output.Append("{");
        output.Append("scroll-behavior:auto !important;");
        output.Append("}");
        output.Append("}");

        // Output the global CSS values
        _css = output.ToString();

        try
        {
            StateHasChanged();
        }
        catch
        {
            _ = InvokeAsync(StateHasChanged);
        }
    }

    /// <summary>
    /// Performs asynchronous cleanup of resources and unsubscribes from all event handlers. Cancels any pending operations,
    /// stops theme watchers if active, and disposes the JS module.
    /// </summary>
    /// <returns>A <see cref="ValueTask" /> representing the asynchronous disposal operation.</returns>
    /// <remarks>
    /// This method guarantees safe teardown regardless of prior errors or component state, ensuring that no unobserved
    /// exceptions occur during disposal.
    /// </remarks>
    public async ValueTask DisposeAsync()
    {
        Persistence.PersistChanged -= OnPersistChanged;
        ThemeProvider.ThemeChanged -= OnThemeProviderChanged;
        ThemeWatcher.Changed -= OnWatcherChanged;

        try
        {
            if (_cts is
            {
                IsCancellationRequested: false
            } && ThemeProvider.ThemeType is ThemeType.System)
            {
                await (ThemeWatcher as AryThemeWatcher)!.StopAsync(_host, _cts.Token).ConfigureAwait(false);
            }
        }
        catch
        {
            // Ignore teardown failures.
        }

        if (_cts is not null)
        {
            try
            {
                await _cts.CancelAsync();
            }
            catch
            {
                // Ignore if cancellation fails.
            }

            _cts.Dispose();
        }

        if (_module is not null)
        {
            try
            {
                await _module.DisposeAsync();
            }
            catch
            {
                // Ignore disposal failures.
            }
        }
    }

    /// <summary>Synchronizes the provider’s theme to the detected system theme.</summary>
    private void EnsureProviderMatchesSystem(ThemeType systemType)
    {
        if (ThemeProvider.ThemeType == systemType)
        {
            return;
        }

        _applyingSystemEffective = true;

        try
        {
            ThemeProvider.SetThemeType(systemType);
            BuildCss();
        }
        finally
        {
            _applyingSystemEffective = false;
        }
    }

    /// <summary>
    /// Executes logic after the component has rendered for the first time. Initializes JavaScript interop, subscribes to theme
    /// events, restores persisted theme state, and starts system theme watching when appropriate.
    /// </summary>
    /// <param name="firstRender">True if this is the first render of the component; otherwise, false.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    /// <exception cref="JSException">Thrown if JavaScript module import fails.</exception>
    /// <remarks>
    /// This method ensures proper initialization only once and avoids redundant event subscriptions or watcher startup on
    /// subsequent renders.
    /// </remarks>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _cts = new CancellationTokenSource();
        _prevPersisted = Persistence.IsPersisted;

        Persistence.PersistChanged += OnPersistChanged;
        ThemeProvider.ThemeChanged += OnThemeProviderChanged;
        ThemeWatcher.Changed += OnWatcherChanged;

        _module ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./AryThemeProvider.razor.js");

        if (Persistence.IsPersisted)
        {
            var persisted = await TryReadPersistedThemeAsync(_cts.Token).ConfigureAwait(false);
            var toApply = persisted ?? ThemeType.System;

            if (toApply != ThemeProvider.ThemeType)
            {
                ThemeProvider.SetThemeType(toApply);
            }
        }

        if (ThemeProvider.ThemeType is ThemeType.System)
        {
            var detected = await ThemeWatcher.DetectAsync(_cts.Token).ConfigureAwait(false);
            EnsureProviderMatchesSystem(detected);
            await (ThemeWatcher as AryThemeWatcher)!.StartAsync(_host, _cts.Token).ConfigureAwait(false);
        }

        BuildCss();
    }

    /// <summary>Handles persistence state changes by enabling/disabling local storage usage.</summary>
    private async void OnPersistChanged(object? sender, bool isPersisted)
    {
        if (_cts is null || _cts.IsCancellationRequested)
        {
            return;
        }

        try
        {
            if (!isPersisted && _prevPersisted)
            {
                await TryClearPersistedThemeAsync(_cts.Token).ConfigureAwait(false);
            }
            else if (isPersisted && !_prevPersisted)
            {
                var persisted = await TryReadPersistedThemeAsync(_cts.Token).ConfigureAwait(false);
                var toApply = persisted ?? ThemeType.System;

                if (toApply != ThemeProvider.ThemeType)
                {
                    ThemeProvider.SetThemeType(toApply);

                    if (toApply is ThemeType.System)
                    {
                        var detected = await ThemeWatcher.DetectAsync(_cts.Token).ConfigureAwait(false);
                        EnsureProviderMatchesSystem(detected);
                        await (ThemeWatcher as AryThemeWatcher)!.StartAsync(_host, _cts.Token).ConfigureAwait(false);
                    }
                }
            }

            _prevPersisted = isPersisted;
        }
        catch
        {
            _prevPersisted = isPersisted;
        }
    }

    /// <summary>Handles changes in the provider’s theme type, updating persistence and watcher accordingly.</summary>
    private async void OnThemeProviderChanged(object? sender, EventArgs e)
    {
        if (_applyingSystemEffective || _cts is null || _cts.IsCancellationRequested)
        {
            return;
        }

        try
        {
            var providerType = ThemeProvider.ThemeType;

            if (Persistence.IsPersisted && _module is not null)
            {
                await TryWritePersistedThemeAsync(providerType, _cts.Token).ConfigureAwait(false);
            }

            if (providerType is ThemeType.System)
            {
                await (ThemeWatcher as AryThemeWatcher)!.StartAsync(_host, _cts.Token).ConfigureAwait(false);
                var detected = await ThemeWatcher.DetectAsync(_cts.Token).ConfigureAwait(false);
                EnsureProviderMatchesSystem(detected);
            }
            else
            {
                await (ThemeWatcher as AryThemeWatcher)!.StopAsync(_host, _cts.Token).ConfigureAwait(false);
                BuildCss();
            }
        }
        catch
        {
            // Ignore interop failures; maintain UI stability.
        }
    }

    /// <summary>Handles notifications from the system theme watcher.</summary>
    private void OnWatcherChanged(object? sender, EventArgs e)
    {
        if (_cts is null || _cts.IsCancellationRequested || ThemeProvider.ThemeType != ThemeType.System)
        {
            return;
        }

        try
        {
            EnsureProviderMatchesSystem(ThemeWatcher.ThemeType);
        }
        catch
        {
            // Ignore transient interop errors.
        }
    }

    /// <summary>Attempts to remove the persisted theme type from local storage.</summary>
    private async Task TryClearPersistedThemeAsync(CancellationToken cancellationToken = default)
    {
        if (_module is null)
        {
            return;
        }

        try
        {
            await _module.InvokeAsync<bool>("clearStoredTheme", cancellationToken, StorageKey).ConfigureAwait(false);
        }
        catch
        {
            // Ignored intentionally.
        }
    }

    /// <summary>Attempts to read the persisted theme type from local storage.</summary>
    private async Task<ThemeType?> TryReadPersistedThemeAsync(CancellationToken cancellationToken = default)
    {
        if (_module is null)
        {
            return null;
        }

        try
        {
            var raw = await _module.InvokeAsync<string?>("getStoredTheme", cancellationToken, StorageKey)
                .ConfigureAwait(false);

            return Enum.TryParse(raw, true, out ThemeType parsed)
                ? parsed
                : null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>Attempts to write the current theme type to local storage.</summary>
    private async Task TryWritePersistedThemeAsync(ThemeType theme, CancellationToken cancellationToken = default)
    {
        if (_module is null)
        {
            return;
        }

        try
        {
            await _module.InvokeAsync<bool>("setStoredTheme", cancellationToken, StorageKey, theme.ToString())
                .ConfigureAwait(false);
        }
        catch
        {
            // Ignored intentionally.
        }
    }
}
