# Localization Standards (Blazor-focused: C#, Razor, JavaScript, SCSS)

> *Version 1: 2025-09-18*

Ensure every part of the application is fully localizable, accessible, and consistent across C#, Razor, JavaScript, and
SCSS. Localization is **not optional**—all user-facing text, labels, errors, and accessibility attributes must support
multiple languages and cultures.

---

## 1. Core Principles

* **All user-visible strings must be localized.** No hardcoded English text in code, markup, or styles.
* **Default language:** English (en-US), but code must support switching cultures at runtime.
* **Accessibility & Localization Integration:** ARIA labels, alt text, and error messages must be localizable.
* **Consistency:** Use the same resource keys across layers where possible.
* **Formatting:** Always use culture-aware APIs for dates, numbers, and currency.

---

## 2. C# Standards

### 2.1 Resource Management

* Use `.resx` resource files with `IStringLocalizer<T>` or `IStringLocalizerFactory`.
* Organize resources per feature/component.

```csharp
[Inject]
private IStringLocalizer<ProductList> Localizer { get; set; } = default!;

public string Title => Localizer["Title_Products"];
```

### 2.2 Formatting

* Use `string.Format` or interpolated strings with **current culture**.
* Avoid `ToString()` without culture; prefer `ToString(CultureInfo.CurrentCulture)`.

```csharp
var message = Localizer["ItemCount", count]; // "You have {0} items"
```

### 2.3 Exceptions & Logging

* Exception messages must be localizable.
* Logging may remain invariant (for developers), but user-facing logs must be localized.

```csharp
throw new ArgumentException(Localizer["Error_InvalidEmail"], nameof(email));
```

### 2.4 Accessibility

* Localize ARIA attributes, error text, and validation summaries.
* Expose `aria-label`/`aria-describedby` with localized values.

---

## 3. Razor Standards

### 3.1 Markup

* Never hardcode strings; always use `@Localizer["Key"]`.
* For templated/localized parameters: pass localized values from parent.

```razor
<h1>@Localizer["Title_Dashboard"]</h1>
<button class="btn">@Localizer["Action_Save"]</button>
```

### 3.2 Accessibility

* Localize `aria-label`, `aria-labelledby`, `alt`, `title`.

```razor
<img src="logo.png" alt="@Localizer["Alt_CompanyLogo"]" />
<button aria-label="@Localizer["Action_OpenMenu"]">☰</button>
```

### 3.3 Error & Validation Messages

* Use `DataAnnotations` with localized error messages.

```csharp
[Required(ErrorMessage = "Error_FieldRequired")]
public string Name { get; set; } = string.Empty;
```

---

## 4. JavaScript Standards

> JS should be minimal, but when needed, never hardcode text.

### 4.1 Resource Access

* JS must not store localized strings directly.
* Retrieve them via:

    * Blazor interop (`IStringLocalizer` passed in from C#).
    * Data attributes in markup (server-rendered).
    * JSON resource files (last resort).

```razor
<div id="messages"
     data-error="@Localizer["Error_Generic"]"
     data-success="@Localizer["Message_Saved"]">
</div>
```

```js
export function showMessage(type) {
    const messages = document.getElementById('messages').dataset;
    alert(type === 'error' ? messages.error : messages.success);
}
```

### 4.2 Accessibility

* Localize ARIA attributes set in JS.
* Announcements via live regions must use localized strings from .NET.

---

## 5. SCSS Standards

### 5.1 Avoid Text in CSS

* Never encode user-facing text in CSS `content:` or images.

```scss
/* ❌ Wrong */
.button::after { content: "Save"; }

/* ✔️ Correct */
.button::after { content: attr(data-label); }
```

### 5.2 RTL (Right-to-Left) Support

* Use logical properties (`margin-inline-start`, `padding-block`).
* Ensure themes support bidirectional layouts.

```scss
.card {
    padding-inline-start: 1rem;
    padding-inline-end: 1rem;
}
```

---

## 6. Accessibility + Localization Combined

* Screen reader labels, ARIA roles, and announcements must be localized.
* Error messages: localized, concise, and associated with inputs.
* Live regions: announcements must use active culture.
* Focus management: localized context (headings, labels).

---

## 7. Culture Management

* Default to `CultureInfo.CurrentUICulture` for UI, `CultureInfo.CurrentCulture` for formatting.
* Allow runtime culture switching.
* Persist culture in cookies or query string.

```csharp
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});
```

---

## 8. Testing & QA

* Validate UI in all supported languages:

    * Check text expansion/shrinkage (German/Chinese).
    * Check RTL layout (Arabic/Hebrew).
    * Ensure localized alt/aria values exist.
* Automated tests must include resource lookup checks.
* Manual tests with screen readers in non-English languages.

---

## 9. Example Flow

**Razor**

```razor
<button class="btn"
        aria-label="@Localizer["Action_Delete"]"
        @onclick="DeleteAsync">
    @Localizer["Action_Delete"]
</button>
```

**C# Code-behind**

```csharp
private async Task DeleteAsync()
{
    try
    {
        await Service.DeleteAsync(ItemId);
        await JS.InvokeVoidAsync("announce", Localizer["Message_DeleteSuccess"]);
    }
    catch
    {
        await JS.InvokeVoidAsync("announce", Localizer["Error_DeleteFailed"]);
    }
}
```

**JS Module**

```js
export function announce(message) {
    let region = document.getElementById('live-region');
    if (!region) {
        region = document.createElement('div');
        region.id = 'live-region';
        region.setAttribute('aria-live', 'polite');
        region.className = 'sr-only';
        document.body.appendChild(region);
    }
    region.textContent = '';
    setTimeout(() => { region.textContent = message; }, 10);
}
```

**SCSS (RTL-aware button)**

```scss
.btn {
    padding-inline: 1rem;
    border-radius: $radius-md;
}
```

---

## 10. Governance

* **Definition of Done** includes localization checks (strings, ARIA, errors, JS interop).
* PRs must confirm no hardcoded strings remain.
* Localization resources reviewed by translators and QA.
* Accessibility testing must include localized variants.

---

> Following these standards ensures Allyaria is accessible and usable in all supported languages, cultures, and writing
> directions.
