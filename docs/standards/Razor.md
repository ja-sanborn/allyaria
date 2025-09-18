# Razor Coding Standards (Blazor-focused)

> *Version 1: 2025-09-18*

These standards define how we write Razor component files (`.razor`) in this project. Razor files should contain *
*markup and lightweight binding only**. All C# logic belongs in a **code-behind** partial class (`.razor.cs`). **No
inline `<script>` or `<style>`** in Razor files—JavaScript and styles must be **referenced**, not embedded.

---

## 1. Core Principles

* **Blazor-first:** Keep logic in C#, templates in Razor, styles in CSS isolation, and minimal JS interop.
* **Accessibility-first:** Semantic HTML, keyboard support, clear focus management.
* **Localization-first:** All user-visible text is localizable (en-US fallback).
* **Extensibility & Theming:** Components should compose cleanly, respect theme APIs, and avoid fragile overrides.

---

## 2. File Layout & Co-location (No Inline Code/Styles/Scripts)

Each component **must co-locate** its supporting files and avoid global/inline code:

* `Component.razor` → **markup only** (parameters, minimal directives, bindings).
* `Component.razor.cs` → **code-behind** (lifecycle, methods, state, DI, events).
* `Component.razor.css` → **CSS isolation** for component styles (compiled from SCSS if used).
* `Component.razor.js` → **ES module** for interop used by this component (co-located with the component).
* `Component.resx` / `Component.<culture>.resx` → **per-component resources** (localization) placed next to the
  component.

> **Do not** place `<script>` or `<style>` tags in `.razor`. **Do not** put component-specific CSS/JS in global files.
> Co-locate JS/CSS/RESX with the component and reference them via code-behind.

### 2.1 Serving co-located `.razor.js` as a Static Web Asset

By default, static files are served from `wwwroot`. To serve `Component.razor.js` from the component folder, expose it
as a **Static Web Asset** via MSBuild:

```xml
<ItemGroup>
  <!-- Serve every co-located .razor.js file under /_content/Allyaria.Editor/components/ -->
  <Content Include="**/*.razor.js" CopyToOutputDirectory="Always" CopyToPublishDirectory="Always">
    <LogicalName>$([System.String]::Copy('%(Identity)'))</LogicalName>
    <StaticWebAssetBasePath>_content/Allyaria.Editor/components</StaticWebAssetBasePath>
  </Content>
</ItemGroup>
```

Then import in code-behind using the generated URL (replace path segments to match your folder structure):

```csharp
_mod = await JS.InvokeAsync<IJSObjectReference>(
    "import",
    "/_content/Allyaria.Editor/components/Components/Widget/Widget.razor.js");
```

> If your package/library name differs, update the `StaticWebAssetBasePath`. For app projects, you may omit the
`_content/<LibName>` prefix and choose a suitable base path.

---

## 3. Formatting

* Encoding: **UTF-8**
* Line endings: **LF**
* Final newline: **required**
* Trim trailing whitespace: **yes**
* Indentation: **4 spaces** (no tabs)

---

## 4. Parameters, State, and Naming

* **Parameters**: `[Parameter]` for incoming values; `[CascadingParameter]` where appropriate.
* Two-way binding: `Value` + `ValueChanged` + `ValueExpression` (for forms) → `@bind-Value`.
* **Private fields**: `_camelCase` (underscore prefix). Public parameters: **PascalCase**.
* Keep component UI state minimal; move complex logic to services.

---

## 5. Lifecycle & Async Patterns

* Use `OnInitializedAsync` / `OnParametersSetAsync` for async work; never block with `.Result`/`.Wait()`.
* Suffix **`Async`** on all async methods and **propagate `CancellationToken`** where possible.
* Avoid excessive `StateHasChanged()`; batch state updates.
* Dispose/cancel long-lived resources in `Dispose`/`DisposeAsync`.

---

## 6. Events, Callbacks, and Binding

* Use `EventCallback`/`EventCallback<T>` for component events; avoid exposing `Action`/`Func` in public API.
* Prefer small method groups in code-behind rather than large inline lambdas.
* Use `@bind-Value:event="oninput"` only when real-time updates are required.

---

## 7. JavaScript Interop (Co-located Modules Only)

* **Co-locate** per-component interop as `Component.razor.js` (see §2.1 for serving from the component folder).
* Import via `IJSRuntime`/`IJSObjectReference` in `.razor.cs`; **never** inline `<script>`.
* Keep JS minimal, focused, with **init/dispose** functions; operate on passed `ElementReference` only.
* Localize any user-visible text via .NET; JS must not hardcode user-facing strings.

---

## 8. Styles (CSS Isolation Only)

* Use `Component.razor.css` for styles (or `Component.razor.scss` → compiled). **No** `<style>` tags in Razor.
* Keep selectors shallow; avoid `!important` except narrow third-party overrides.
* Respect accessibility: `:focus-visible`, contrast, no color-only meaning.

---

## 9. Accessibility & UX

* Prefer semantic HTML over ARIA; add ARIA only when necessary.
* Manage focus for dialogs/menus; trap focus only while open and return to opener on close.
* Maintain visible focus indicators; ensure keyboard operability.

---

## 10. Localization (Per-Component Resources)

**Recommended pattern:**

* Name resources after the component **class**, not the `.razor` file extension: `Component.resx`, `Component.fr.resx`,
  etc.
* Keep the `.resx` file in the **same folder/namespace** as the `Component` partial class.
* Inject `IStringLocalizer<Component>` in `.razor.cs` and pass localized strings to markup.

```csharp
[Inject] private IStringLocalizer<Component> L { get; set; } = default!;

protected override void OnInitialized()
{
    _title = L["Title_MyWidget"]; // e.g., reads from Component.resx
}
```

> **Why not `Component.razor.resx`?** The .NET resource lookup uses the type’s **FullName** as the base name (
`Namespace.Component`). Using `.razor` in the resource file name is non-standard and won’t be resolved by
`IStringLocalizer<T>` without customization.

**If you must support `Component.razor.resx`:** Provide a small adapter (e.g., `AllyariaResources`) that maps a
component type to a custom base name (strip `.razor` segment) and constructs a `ResourceManagerStringLocalizer`. Prefer
the standard `Component.resx` unless there’s a compelling reason.

---

## 11. Error Handling

* Throw from code-behind for critical errors; render friendly, localized messages in markup.
* Do not throw from markup; set state in code-behind and update UI accordingly.

---

## 12. Testing & QA

* Prefer **bUnit** for component tests; inject fakes for services.
* Avoid timing flakiness; await renders/events explicitly.
* Include accessibility checks (axe scans) for complex interactive components.

---

## 13. Examples

**Minimal component skeleton (co-located JS/CSS/RESX)**

```razor
@* Component.razor *@
<div class="widget" @ref="_root" aria-live="polite">
    <h2>@Title</h2>
    <button class="btn" @onclick="OnClick">@ButtonText</button>
</div>
```

```csharp
// Component.razor.cs
public partial class Component : IAsyncDisposable
{
    private ElementReference _root;
    [Inject] private IJSRuntime JS { get; set; } = default!;
    [Inject] private IStringLocalizer<Component> L { get; set; } = default!;
    private IJSObjectReference? _mod;

    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string ButtonText { get; set; } = string.Empty;
    [Parameter] public EventCallback OnClick { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _mod = await JS.InvokeAsync<IJSObjectReference>("import", "/_content/Allyaria.Editor/components/Components/Component/Component.razor.js");
            await _mod.InvokeVoidAsync("init", _root);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_mod is not null)
        {
            try { await _mod.InvokeVoidAsync("dispose", _root); }
            catch { /* swallow dispose-time errors */ }
            await _mod.DisposeAsync();
        }
    }
}
```

```css
/* Component.razor.css */
.widget { display: grid; gap: .75rem; }
.widget :focus-visible { outline: 2px solid currentColor; outline-offset: 2px; }
```

```xml
<!-- Component.resx lives next to the component; no .Designer.cs required when using IStringLocalizer -->
```

```js
// Component.razor.js (co-located)
export function init(el) { /* attach listeners, observers, etc. */ }
export function dispose(el) { /* remove listeners, observers, etc. */ }
```

---

## 14. Deviations

If a guideline must be violated (interop constraints, performance, third-party markup), document the rationale in code
comments and in the PR description. Keep the deviation narrow and localized.

---

## 15. Governance

* **Definition of Done** includes: code-behind, CSS isolation, co-located JS module **and** per-component `.resx`
  present; accessibility and localization checks completed.
* PRs must confirm: no inline `<style>`/`<script>`, no global leakage of component-specific CSS/JS, minimal JS interop,
  and correct resource lookup via `IStringLocalizer<Component>`.
