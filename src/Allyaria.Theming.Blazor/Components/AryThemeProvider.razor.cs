using Allyaria.Theming.Blazor.Services;

namespace Allyaria.Theming.Blazor.Components;

/// <summary>
/// Provides automatic theme synchronization between system preferences and the application's <see cref="IThemeProvider" />
/// . Supports dynamic switching when the theme type is <see cref="ThemeType.System" />.
/// </summary>
public sealed partial class AryThemeProvider : ComponentBase, IAsyncDisposable
{
    /// <summary>True while applying an effective theme value that came from system detection.</summary>
    private bool _applyingSystemEffective;

    /// <summary>
    /// The cancellation token source controlling asynchronous operations such as watcher start/stop and detection.
    /// </summary>
    private CancellationTokenSource? _cts;

    /// <summary>The root element reference for this component, used for JS interop initialization.</summary>
    private ElementReference _host;

    /// <summary>Gets or sets the child content rendered inside the provider.</summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the injected <see cref="IThemeProvider" /> instance responsible for managing theme state.
    /// </summary>
    [Inject]
    public required IThemeProvider ThemeProvider { get; init; }

    /// <summary>
    /// Gets or sets the injected <see cref="IThemeWatcher" /> used to detect system theme changes via JS interop.
    /// </summary>
    [Inject]
    public required IThemeWatcher ThemeWatcher { get; init; }

    /// <summary>Releases resources used by this component and unregisters event handlers.</summary>
    /// <returns>A <see cref="ValueTask" /> representing the asynchronous disposal operation.</returns>
    public async ValueTask DisposeAsync()
    {
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
            // Safe to ignore during cleanup.
        }

        if (_cts is not null)
        {
            try
            {
                await _cts.CancelAsync();
                _cts.Dispose();
            }
            catch
            {
                // Swallow exceptions during disposal.
            }
        }
    }

    /// <summary>
    /// Ensures that the <see cref="IThemeProvider" /> reflects the given system theme type. Uses an internal guard to prevent
    /// recursive provider-change handling from stopping the watcher.
    /// </summary>
    /// <param name="systemType">The detected system theme type.</param>
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
            InvokeAsync(StateHasChanged);
        }
        finally
        {
            _applyingSystemEffective = false;
        }
    }

    /// <summary>
    /// Called by the Blazor runtime after the component is rendered. On first render, wires up theme change listeners and
    /// starts watcher if system theme detection is enabled.
    /// </summary>
    /// <param name="firstRender">Indicates whether this is the first render of the component.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">Thrown when cancellation is requested during initialization.</exception>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _cts = new CancellationTokenSource();

        ThemeProvider.ThemeChanged += OnThemeProviderChanged;
        ThemeWatcher.Changed += OnWatcherChanged;

        if (ThemeProvider.ThemeType is ThemeType.System)
        {
            var detected = await ThemeWatcher.DetectAsync(_cts.Token).ConfigureAwait(false);
            EnsureProviderMatchesSystem(detected);
            await (ThemeWatcher as AryThemeWatcher)!.StartAsync(_host, _cts.Token).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Handles changes in the <see cref="IThemeProvider" /> to synchronize with system theme when appropriate. Ignores changes
    /// that are initiated by system detection to prevent stopping the watcher.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private async void OnThemeProviderChanged(object? sender, EventArgs e)
    {
        // If we are in the middle of applying a system-detected effective value, do nothing.
        if (_applyingSystemEffective)
        {
            return;
        }

        if (_cts is null || _cts.IsCancellationRequested)
        {
            return;
        }

        try
        {
            var providerType = ThemeProvider.ThemeType;

            if (providerType is ThemeType.System)
            {
                // Entering or remaining in System mode: ensure watcher runs and align effective value.
                await (ThemeWatcher as AryThemeWatcher)!.StartAsync(_host, _cts.Token).ConfigureAwait(false);
                var detected = await ThemeWatcher.DetectAsync(_cts.Token).ConfigureAwait(false);
                EnsureProviderMatchesSystem(detected);
            }
            else
            {
                // Leaving System mode by user/app choice: stop watcher.
                await (ThemeWatcher as AryThemeWatcher)!.StopAsync(_host, _cts.Token).ConfigureAwait(false);
                StateHasChanged();
            }
        }
        catch
        {
            // Swallow non-fatal errors to avoid UI crash.
        }
    }

    /// <summary>Handles notifications from the <see cref="IThemeWatcher" /> when the system theme changes.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
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
            // Transient JS or interop errors are ignored to maintain UI stability.
        }
    }
}
