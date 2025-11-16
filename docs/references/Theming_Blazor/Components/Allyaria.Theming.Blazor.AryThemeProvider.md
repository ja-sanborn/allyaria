# Allyaria.Theming.Blazor.AryThemeProvider

`AryThemeProvider` is a Blazor root-level theming component that connects the `IThemingService` to the browser, handling
system theme detection, persisted theme preference, and document direction and language. It is intended to wrap your
application UI, synchronizing the effective theme with OS/browser preferences when `ThemeType.System` is selected,
persisting the stored theme in browser storage, and updating `dir` / `lang` attributes and RTL body classes so that your
app reacts consistently to theme and culture changes.

## Constructors

`AryThemeProvider()` Initializes a new instance of the `AryThemeProvider` component for use in a Blazor application.

## Properties

| Name             | Type              | Description                                                                                                                                                                                    |
|------------------|-------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `ChildContent`   | `RenderFragment?` | Gets or sets the child content rendered inside the theme provider; typically the rest of the app UI. Marked `[Parameter]` and `[EditorRequired]` so it must be supplied in markup.             |
| `Culture`        | `CultureInfo?`    | Gets or sets the cascaded culture used to determine document direction (`dir`) and language (`lang`) attributes; falls back to `CultureInfo.CurrentUICulture` when not supplied.               |
| `JsRuntime`      | `IJSRuntime`      | Gets the JavaScript runtime used for all browser interop, including theme detection, storage, and document attribute updates. Injected via `[Inject]` and must be provided by the Blazor host. |
| `ThemingService` | `IThemingService` | Gets the theming service that maintains the current stored and effective theme types and raises `ThemeChanged` events; injected via `[Inject]`.                                                |

## Methods

| Name                                                              | Returns            | Description                                                                                                                                                                                                            |
|-------------------------------------------------------------------|--------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `DetectThemeAsync(CancellationToken cancellationToken = default)` | `Task<ThemeType?>` | Detects the current system theme using the co-located JavaScript module and returns a mapped `ThemeType`, or `null` when detection fails or resolves to `ThemeType.System`; respects the supplied `cancellationToken`. |
| `DisposeAsync()`                                                  | `ValueTask`        | Asynchronously disposes the component by cancelling ongoing work, detaching from `IThemingService.ThemeChanged`, and releasing JavaScript interop resources such as the imported module and `DotNetObjectReference`.   |
| `SetDirectionAsync()`                                             | `Task`             | Sets `dir` and `lang` attributes on `document.documentElement` and toggles an `rtl` CSS class on `body` based on the current or cascaded `Culture`, ignoring failures (e.g., when JS is unavailable).                  |
| `SetFromJs(string raw)`                                           | `void`             | Receives theme updates from the JavaScript module via `[JSInvokable]`, parses the raw theme string into a `ThemeType`, and updates the effective theme through the theming service.                                    |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

**App.razor**

```csharp
@using Allyaria.Theming
@using Allyaria.Theming.Blazor.Components

<AryThemeProvider>
    <Router AppAssembly="@typeof(App).Assembly">
        <Found Context="routeData">
            <RouteView RouteData="@routeData" />
        </Found>
        <NotFound>
            <p>Sorry, there's nothing at this address.</p>
        </NotFound>
    </Router>
</AryThemeProvider>
```

---

*Revision Date: 2025-11-15*
