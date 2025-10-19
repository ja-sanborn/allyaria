# Allyaria.Theming.Contracts.IThemeWatcher

`IThemeWatcher` is an interface that provides a cross-platform contract for detecting and monitoring the effective
`ThemeType` (e.g., light, dark, system) without depending on JavaScript or Blazor. It is designed for use in
applications that need to adapt UI styling dynamically based on system or user theme preferences. Implementations are
expected to handle asynchronous operations gracefully, honor cancellation tokens, and avoid raising duplicate events for
unchanged theme values.

## Constructors

*None*

## Properties

| Name      | Type        | Description                                                                                   |
|-----------|-------------|-----------------------------------------------------------------------------------------------|
| `Current` | `ThemeType` | Gets the most recently observed `ThemeType`. Defaults to `ThemeType.System` before detection. |

## Methods

| Name                                                         | Returns           | Description                                                                                                                                         |
|--------------------------------------------------------------|-------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------|
| `DetectAsync(CancellationToken cancellationToken = default)` | `Task<ThemeType>` | Detects the current effective `ThemeType` once without monitoring for future changes. Updates `Current` with the detected value.                    |
| `StartAsync(CancellationToken cancellationToken = default)`  | `Task`            | Begins monitoring for subsequent theme changes and raises the `Changed` event when transitions occur. Calling multiple times should have no effect. |
| `StopAsync(CancellationToken cancellationToken = default)`   | `Task`            | Stops monitoring for theme changes if active; can be resumed with `StartAsync`.                                                                     |

## Operators

*None*

## Events

| Event     | Description                                                                                             |
|-----------|---------------------------------------------------------------------------------------------------------|
| `Changed` | Occurs when the effective `ThemeType` changes. Duplicate consecutive notifications should be coalesced. |

## Exceptions

*None*

## Example

```csharp
public class ThemeWatcherExample
{
    private readonly IThemeWatcher watcher;

    public ThemeWatcherExample(IThemeWatcher watcher)
    {
        this.watcher = watcher;
        this.watcher.Changed += OnThemeChanged;
    }

    public async Task InitializeAsync()
    {
        // Detect the current theme once
        ThemeType current = await watcher.DetectAsync();

        Console.WriteLine($"Current theme: {current}");

        // Begin monitoring for future changes
        await watcher.StartAsync();
    }

    private void OnThemeChanged(object? sender, EventArgs e)
    {
        Console.WriteLine($"Theme changed to: {watcher.Current}");
    }
}
```

---

*Revision Date: 2025-10-18*
