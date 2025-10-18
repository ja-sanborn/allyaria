using Allyaria.Theming.Interfaces;

namespace Allyaria.Theming.Services;

/// <summary>
/// Provides a generic, platform-agnostic implementation of <see cref="IThemeTypeWatcher" /> suitable for server-side,
/// headless, and test environments. It does not subscribe to platform events but allows programmatic theme control via
/// <see cref="SetCurrent" />.
/// </summary>
/// <remarks>
/// This implementation is thread-safe and does not rely on any JavaScript or platform interop. It simply tracks and
/// reports a manually managed <see cref="ThemeType" /> value.
/// </remarks>
public sealed class ThemeTypeWatcher : IThemeTypeWatcher
{
    /// <summary>Holds the current effective <see cref="ThemeType" /> value being tracked by this watcher.</summary>
    private ThemeType _current;

    /// <summary>
    /// Represents the watcher’s active monitoring state, where 1 indicates “started” and 0 indicates “stopped.”
    /// </summary>
    private int _started;

    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeTypeWatcher" /> class with an optional initial theme type.
    /// </summary>
    /// <param name="initial">The initial <see cref="ThemeType" /> value. Defaults to <see cref="ThemeType.System" />.</param>
    public ThemeTypeWatcher(ThemeType initial = ThemeType.System) => _current = initial;

    /// <summary>Occurs when the effective <see cref="ThemeType" /> changes.</summary>
    public event EventHandler? Changed;

    /// <summary>Gets the most recently observed <see cref="ThemeType" />.</summary>
    public ThemeType Current => _current;

    /// <summary>Detects the current effective <see cref="ThemeType" /> once without subscribing for future changes.</summary>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that completes with the detected <see cref="ThemeType" />. This generic implementation simply returns the
    /// current stored value.
    /// </returns>
    public Task<ThemeType> DetectAsync(CancellationToken cancellationToken = default) => Task.FromResult(_current);

    /// <summary>
    /// Releases any unmanaged resources associated with this instance. This implementation holds no such resources, so it
    /// completes synchronously.
    /// </summary>
    /// <returns>A completed <see cref="ValueTask" />.</returns>
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    /// <summary>
    /// Sets a new <see cref="ThemeType" /> value programmatically and raises <see cref="Changed" /> if the value changed.
    /// </summary>
    /// <param name="value">The new <see cref="ThemeType" /> to apply.</param>
    /// <returns>
    /// <see langword="true" /> if the current value was updated and <see cref="Changed" /> was raised; otherwise,
    /// <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// Thread-safe and idempotent for identical values. Event subscribers should read the updated value from
    /// <see cref="Current" />.
    /// </remarks>
    public bool SetCurrent(ThemeType value)
    {
        if (value == _current)
        {
            return false;
        }

        _current = value;
        Changed?.Invoke(this, EventArgs.Empty);

        return true;
    }

    /// <summary>
    /// Starts monitoring for theme changes. In this implementation, the operation is a no-op and marks the watcher as started.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>A task that completes immediately.</returns>
    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        Interlocked.Exchange(ref _started, 1);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Stops monitoring for theme changes. In this implementation, the operation is a no-op and marks the watcher as stopped.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>A task that completes immediately.</returns>
    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        Interlocked.Exchange(ref _started, 0);

        return Task.CompletedTask;
    }
}
