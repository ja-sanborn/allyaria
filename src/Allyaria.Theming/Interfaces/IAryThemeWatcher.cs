namespace Allyaria.Theming.Interfaces;

/// <summary>
/// Provides a cross-platform contract for detecting the effective <see cref="ThemeType" /> and optionally watching for
/// subsequent changes without taking a dependency on JS/Blazor.
/// </summary>
/// <remarks>
/// Implementations should honor <see cref="CancellationToken" /> parameters for all asynchronous operations, avoid
/// throwing for expected conditions, and coalesce duplicate change notifications to reduce noise.
/// </remarks>
public interface IAryThemeWatcher : IAsyncDisposable
{
    /// <summary>
    /// Occurs when the effective <see cref="ThemeType" /> changes. Implementations should avoid raising duplicate events for
    /// the same effective value by coalescing changes.
    /// </summary>
    /// <remarks>
    /// The event sender should be the instance of <see cref="IAryThemeWatcher" /> raising the event. No event arguments are
    /// provided; consumers should read the new value from <see cref="Current" />.
    /// </remarks>
    event EventHandler? Changed;

    /// <summary>Gets the most recently observed <see cref="ThemeType" />.</summary>
    /// <remarks>
    /// The value should be set after a successful call to <see cref="DetectAsync(CancellationToken)" /> or while monitoring
    /// has been started via <see cref="StartAsync(CancellationToken)" />. Implementations should provide a sensible default
    /// (e.g., <see cref="ThemeType.System" />) prior to detection.
    /// </remarks>
    ThemeType Current { get; }

    /// <summary>Detects the current effective <see cref="ThemeType" /> once without subscribing for future changes.</summary>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that completes with the detected <see cref="ThemeType" />. Implementations should also update
    /// <see cref="Current" /> with the returned value.
    /// </returns>
    Task<ThemeType> DetectAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts monitoring for subsequent theme changes and raising <see cref="Changed" /> when the effective
    /// <see cref="ThemeType" /> transitions. Calling this method multiple times should be a no-op.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>A task that completes when monitoring is initialized.</returns>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops monitoring for theme changes if monitoring is currently active. A later call to
    /// <see cref="StartAsync(CancellationToken)" /> may resume monitoring.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>A task that completes when monitoring is stopped.</returns>
    Task StopAsync(CancellationToken cancellationToken = default);
}
