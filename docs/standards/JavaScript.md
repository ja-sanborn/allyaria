# JavaScript Coding Standards (Blazor-focused)

> *Version 1: 2025-09-18*

JavaScript should be avoided unless absolutely necessary, and kept as minimal as possible to achieve the required
functionality. Prefer C# / Razor and .NET libraries first. Use JavaScript only when platform/browser APIs are not
available in .NET or when integrating a third-party widget that requires JS.

---

## 1. Core Principles

* **Blazor-first:** Use C# and Razor where possible; JS only when unavoidable.
* **Minimal & Focused:** Keep JS modules small, deterministic, and side-effect free.
* **Accessible:** Ensure keyboard/focus support, avoid interfering with Blazor rendering.
* **Disposable:** Always provide cleanup (events, observers, timers).

---

## 2. Formatting

* Encoding: UTF-8
* Line endings: LF
* Final newline: required
* Trim trailing whitespace: yes
* Indentation: 4 spaces (no tabs)

---

## 3. Language Level & Modules

* Use modern ECMAScript with **ES modules** (`import`/`export`).
* One responsibility per file; keep files short.
* File names: **kebab-case** (e.g., `scroll-lock.js`).
* Avoid globals; export functions explicitly.

---

## 4. Naming Conventions

* Functions/variables: `camelCase`.
* Classes (rare): `PascalCase`.
* Constants: `const` with `camelCase`; reserve `UPPER_SNAKE_CASE` for true compile-time constants.

---

## 5. Blazor Interop Contract

* Export **small, pure functions**; pass/return JSON-serializable values.
* Prefer `ElementReference` from .NET instead of querying DOM globally.
* Always provide **dispose** or **cancel** for long-lived work.
* Surface failures via rejected promises; never swallow errors.

**Example:**

```js
// wwwroot/js/focus-trap.js
const traps = new WeakMap();

export function initFocusTrap(element) {
    if (!element) throw new Error('Element is required');
    const handler = (e) => {
        if (!element.contains(document.activeElement)) {
            element.focus();
        }
    };
    document.addEventListener('focusin', handler);
    traps.set(element, handler);
    return true;
}

export function disposeFocusTrap(element) {
    const handler = traps.get(element);
    if (handler) {
        document.removeEventListener('focusin', handler);
        traps.delete(element);
    }
    return true;
}
```

---

## 6. Syntax & Style

* Use semicolons consistently.
* Prefer `const`, then `let`; never `var`.
* Use strict equality (`===`, `!==`).
* Use optional chaining (`?.`) and nullish coalescing (`??`) for clarity.
* Prefer early-return to avoid deep nesting.

---

## 7. DOM Operations

* Operate only within element(s) passed in from .NET.
* Avoid manipulating Blazor-owned markup in ways that conflict with the renderer.
* Always unregister listeners/observers in a `dispose` function.
* Avoid layout thrashing; batch reads/writes when needed.

**Example:**

```js
// wwwroot/js/click-outside.js
const handlers = new WeakMap();

export function attach(el) {
    const onDocClick = (e) => {
        if (el && !el.contains(e.target)) {
            el.dispatchEvent(new CustomEvent('blazor:click-outside', { bubbles: true }));
        }
    };
    document.addEventListener('click', onDocClick, true);
    handlers.set(el, onDocClick);
}

export function detach(el) {
    const h = handlers.get(el);
    if (h) {
        document.removeEventListener('click', h, true);
        handlers.delete(el);
    }
}
```

---

## 8. Async & Long-Running Work

* Prefer `async`/`await`; always handle promise rejections.
* Use `AbortSignal` for cancellation when supported.
* Do not leave floating promises, intervals, or observers running after disposal.

**Example:**

```js
export async function fetchJson(url, { signal } = {}) {
    const res = await fetch(url, { signal });
    if (!res.ok) throw new Error(`HTTP ${res.status}`);
    return res.json();
}
```

---

## 9. Errors & Security

* Throw `Error` with clear, actionable messages.
* Never leak sensitive data in errors.
* Never inject unsanitized HTML (avoid `innerHTML`).
* Avoid `eval`, `new Function`, or dynamic script construction.
* Respect Content Security Policy (CSP).

---

## 10. Testing & QA

* For tiny interop utilities, rely on .NET component-level tests where practical.
* If needed, write small deterministic JS unit tests.
* JS affecting accessibility (focus, announcements) must be tested with bUnit + axe scans.

---

## 11. Packaging & Placement

* Place JS modules under `wwwroot/js/`.
* Reference them via `<script type="module">` or import maps.
* No bundler/transpiler unless required by a dependency.
* Versioning handled by static file options or .NET query strings.

---

## 12. Deviations

* If a rule must be violated (interop constraints, performance, or third-party needs), document rationale in code
  comments and PR description.

---

## 13. Governance

* **Definition of Done** includes:

    * Proper init/dispose lifecycle.
    * No global DOM manipulation.
    * Accessibility validated (focus, keyboard).
    * No hardcoded text (localized via interop).
* PRs must confirm that JS usage is minimal and justified.

---

> Following these standards ensures JS in Allyaria remains minimal, accessible, and maintainable.
