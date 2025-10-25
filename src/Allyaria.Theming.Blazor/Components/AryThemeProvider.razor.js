/**
 * Internal state keyed by the host element to support multiple component instances.
 * @type {WeakMap<Element, { forcedMq: MediaQueryList, darkMq: MediaQueryList, notify: () => Promise<boolean>, dotnetRef: any }>}
 */
const state = new WeakMap();

/**
 * Detects the current system theme with High Contrast taking precedence over Dark/Light.
 * Falls back to "System" if matchMedia is unavailable or throws.
 *
 * @returns {"HighContrastDark"|"HighContrastLight"|"Dark"|"Light"|"System"} The detected theme.
 */
export function detect() {
    try {
        const forced = window.matchMedia('(forced-colors: active)').matches;
        const dark = window.matchMedia('(prefers-color-scheme: dark)').matches;
        if (forced) {
            return dark ? 'HighContrastDark' : 'HighContrastLight';
        }
        return dark ? 'Dark' : 'Light';
    } catch {
        return 'System';
    }
}

/**
 * Initializes listeners for system theme changes and immediately notifies the .NET side.
 * Safe to call multiple times; re-initializes if already attached to the same host.
 *
 * @param {Element} host A stable ElementReference root from the Blazor component.
 * @param {{ invokeMethodAsync: (name: string, ...args: any[]) => Promise<any> }} dotnetRef A DotNetObjectReference with an invokeMethodAsync function.
 * @returns {Promise<boolean>} Resolves true when initialization and initial notify complete.
 * @throws {Error} When required arguments are missing or invalid.
 */
export async function init(host, dotnetRef) {
    if (!host) {
        throw new Error('host required');
    }
    if (!dotnetRef || typeof dotnetRef.invokeMethodAsync !== 'function') {
        throw new Error('valid dotnetRef required');
    }

    // Re-initialize safely if called twice for the same host.
    if (state.has(host)) {
        dispose(host);
    }

    const forcedMq = window.matchMedia('(forced-colors: active)');
    const darkMq = window.matchMedia('(prefers-color-scheme: dark)');

    /**
     * Notifies the .NET side of the current detection result.
     * Uses promise rejection handling to avoid unobserved rejections.
     * @returns {Promise<boolean>} True if the call was attempted; false if an error occurred synchronously.
     */
    const notify = () => {
        try {
            return dotnetRef
                .invokeMethodAsync('SetFromJs', detect())
                .then(() => true)
                .catch(() => {
                    // Swallow interop errors at runtime; dispose/teardown phases can race.
                    // Intentionally silent per interop lifecycle—see project JS standards.
                    return false;
                });
        } catch {
            return Promise.resolve(false);
        }
    };

    // Prefer modern event APIs; fall back to legacy addListener for older engines.
    const addChangeListener = (mql, handler) => {
        if (typeof mql.addEventListener === 'function') {
            mql.addEventListener('change', handler);
        } else if (typeof mql.addListener === 'function') {
            mql.addListener(handler);
        }
    };

    addChangeListener(forcedMq, notify);
    addChangeListener(darkMq, notify);

    state.set(host, {forcedMq, darkMq, notify, dotnetRef});

    await notify();
    return true;
}

/**
 * Disposes listeners and clears module state for a given host.
 *
 * @param {Element} host The same Element passed to init.
 * @returns {boolean} True if a subscription existed and was removed; false otherwise.
 */
export function dispose(host) {
    const entry = state.get(host);
    if (!entry) {
        return false;
    }

    const {forcedMq, darkMq, notify} = entry;

    const removeChangeListener = (mql, handler) => {
        if (typeof mql.removeEventListener === 'function') {
            mql.removeEventListener('change', handler);
        } else if (typeof mql.removeListener === 'function') {
            mql.removeListener(handler);
        }
    };

    removeChangeListener(forcedMq, notify);
    removeChangeListener(darkMq, notify);

    state.delete(host);
    return true;
}

/**
 * Attempts to acquire a safe Web Storage instance.
 *
 * @param {"localStorage"|"sessionStorage"} which The storage bucket to probe.
 * @returns {Storage|null} A usable Storage instance or null if blocked/unavailable.
 */
function tryStorage(which) {
    try {
        const s = window[which];
        if (!s) {
            return null;
        }
        const probe = '__allyaria_probe__';
        s.setItem(probe, '1');
        s.removeItem(probe);
        return s;
    } catch {
        return null;
    }
}

/**
 * Retrieves a theme value from localStorage if available.
 *
 * @param {string} key The storage key (must be a non-empty string).
 * @returns {string|null} The stored value or null when missing/unavailable.
 * @throws {Error} When key is not a non-empty string.
 */
export function getStoredTheme(key) {
    if (typeof key !== 'string' || key.trim() === '') {
        throw new Error('key must be a non-empty string');
    }
    try {
        const s = tryStorage('localStorage');
        if (s) {
            const v = s.getItem(key);
            if (typeof v === 'string' && v) {
                return v;
            }
        }
        return null;
    } catch {
        return null;
    }
}

/**
 * Stores a theme value in localStorage if available.
 *
 * @param {string} key The storage key (must be a non-empty string).
 * @param {string} value The value to store.
 * @returns {boolean} True if stored; false if storage is unavailable.
 * @throws {Error} When key or value are invalid.
 */
export function setStoredTheme(key, value) {
    if (typeof key !== 'string' || key.trim() === '') {
        throw new Error('key must be a non-empty string');
    }
    if (typeof value !== 'string' || value === '') {
        throw new Error('value must be a non-empty string');
    }
    try {
        const s = tryStorage('localStorage');
        if (s) {
            s.setItem(key, value);
            return true;
        }
        return false;
    } catch {
        return false;
    }
}

/**
 * Clears a theme value from localStorage if available.
 *
 * @param {string} key The storage key (must be a non-empty string).
 * @returns {boolean} True if a storage backend was reachable (not whether the key existed).
 * @throws {Error} When key is invalid.
 */
export function clearStoredTheme(key) {
    if (typeof key !== 'string' || key.trim() === '') {
        throw new Error('key must be a non-empty string');
    }
    let any = false;
    try {
        const s = tryStorage('localStorage');
        if (s) {
            s.removeItem(key);
            any = true;
        }
    } catch {
        // Intentionally silent; storage may be blocked by privacy settings.
    }
    return any;
}
