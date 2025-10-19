# Allyaria.Theming.Services.ThemeWatcher

`ThemeWatcher` is a lightweight, platform-agnostic implementation of `IThemeWatcher` that provides programmatic control
and observation of theme mode changes (e.g., Light, Dark, System). It is ideal for server-side, headless, and testing
environments where platform or browser-based event subscriptions are unavailable. Unlike UI-integrated watchers, this
implementation does not rely on JavaScript or system events; instead, it tracks a manually managed `ThemeType` and
raises notifications when updated.

## Constructors

`ThemeWatcher(ThemeType initial = ThemeType.System)` Initializes a new instance with an optional initial `ThemeType`.
Defaults to `ThemeType.System`.

## Properties

| Name      | Type        | Description                                        |
|-----------|-------------|----------------------------------------------------|
| `Current` | `ThemeType` | Gets the most recently observed `ThemeType` value. |

## Methods

| Name                                                         | Returns           | Description                                                                                                                         |
|--------------------------------------------------------------|-------------------|-------------------------------------------------------------------------------------------------------------------------------------|
| `DetectAsync(CancellationToken cancellationToken = default)` | `Task<ThemeType>` | Detects the current theme type once and returns it asynchronously. Simply returns the stored `Current` value.                       |
| `StartAsync(CancellationToken cancellationToken = default)`  | `Task`            | Marks the watcher as started. This is a no-op for headless implementations.                                                         |
| `StopAsync(CancellationToken cancellationToken = default)`   | `Task`            | Marks the watcher as stopped. This is a no-op for headless implementations.                                                         |
| `DisposeAsync()`                                             | `ValueTask`       | Releases resources. This implementation completes synchronously as there are no unmanaged resources.                                |
| `SetCurrent(ThemeType value)`                                | `bool`            | Programmatically sets a new `ThemeType` and raises `Changed` if the value differs. Thread-safe and idempotent for identical values. |

## Operators

*None*

## Events

| Event     | Description                                                                                      |
|-----------|--------------------------------------------------------------------------------------------------|
| `Changed` | Occurs when the effective `ThemeType` changes. Subscribers can read the new value via `Current`. |

## Exceptions

*None*

## Example

```csharp
public class ThemeTypeWatcherExample
{
    public async Task RunAsync()
    {
        var watcher = new ThemeWatcher();

        watcher.Changed += (_, _) => 
            Console.WriteLine($"Theme changed to: {watcher.Current}");

        // Start monitoring (no-op in this implementation)
        await watcher.StartAsync();

        // Detect current theme
        ThemeType current = await watcher.DetectAsync();
        Console.WriteLine($"Initial theme: {current}");

        // Simulate a theme change
        watcher.SetCurrent(ThemeType.Dark);

        // Stop monitoring (no-op)
        await watcher.StopAsync();

        await watcher.DisposeAsync();
    }
}
```

---

*Revision Date: 2025-10-18*
