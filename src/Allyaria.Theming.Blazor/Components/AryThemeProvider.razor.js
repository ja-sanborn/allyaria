// AryThemeProvider.razor.js
// Co-located ES module for Allyaria theme detection.
// Detects system theme (HighContrast/Dark/Light) and notifies the owning Blazor component.
// Lifecycle: init(host, dotnetRef) → dispose(host?) to remove listeners.

const state = new WeakMap();

/**
 * Detect the current system theme preference.
 * Forced-colors (High Contrast) takes precedence over Dark/Light.
 * @returns {'HighContrast'|'Dark'|'Light'|'System'}
 */
export function detect() {
    try {
        const forced = window.matchMedia('(forced-colors: active)').matches;
        if (forced) {
            return 'HighContrast';
        }

        const dark = window.matchMedia('(prefers-color-scheme: dark)').matches;
        return dark ? 'Dark' : 'Light';
    } catch {
        // If matchMedia is unavailable (older/embedded environments), fall back to System.
        return 'System';
    }
}

/**
 * Initialize listeners for system theme changes for a specific host element.
 * Stores handlers in a per-host WeakMap so multiple component instances are supported.
 * @param {HTMLElement} host - The component's root element (ElementReference passed from .NET).
 * @param {{ invokeMethodAsync: Function }} dotnetRef - .NET interop reference to call SetFromJs(string).
 * @returns {boolean} true when initialized.
 * @throws {Error} when required arguments are missing.
 */
export function init(host, dotnetRef) {
    if (!host) {
        throw new Error('init(host, dotnetRef): host element is required.');
    }
    if (!dotnetRef || typeof dotnetRef.invokeMethodAsync !== 'function') {
        throw new Error('init(host, dotnetRef): a valid dotnetRef with invokeMethodAsync is required.');
    }

    const forcedMq = window.matchMedia('(forced-colors: active)');
    const darkMq = window.matchMedia('(prefers-color-scheme: dark)');

    const notify = () => {
        const value = detect();
        // Surface errors to Blazor; do not swallow, but keep this call guarded to avoid breaking the loop on rare failures.
        try {
            dotnetRef.invokeMethodAsync('SetFromJs', value);
        } catch {
            /* Intentionally no-op: Blazor may be disposing; subsequent dispose() will clean listeners. */
        }
    };

    // Register listeners (use modern addEventListener if available; older Safari relied on addListener).
    forcedMq.addEventListener?.('change', notify);
    darkMq.addEventListener?.('change', notify);

    // Track per-host so multiple instances don't clobber each other.
    state.set(host, {forcedMq, darkMq, notify, dotnetRef});

    // Send an initial value immediately.
    notify();

    return true;
}

/**
 * Dispose listeners. If a host is provided, only that instance is cleaned.
 * If no host is provided, all known instances are cleaned (back-compat with previous API).
 * @param {HTMLElement} [host] - Optional host element to dispose.
 * @returns {boolean} true if any cleanup occurred; false otherwise.
 */
export function dispose(host) {
    if (host) {
        const entry = state.get(host);
        if (!entry) {
            return false;
        }

        entry.forcedMq.removeEventListener?.('change', entry.notify);
        entry.darkMq.removeEventListener?.('change', entry.notify);
        state.delete(host);
        return true;
    }

    return false;
}
