const listenerRegistry = new WeakMap();

function readSystemTheme() {
    if (typeof window === "undefined" || typeof window.matchMedia !== "function") {
        return "light";
    }

    const forced = window.matchMedia("(forced-colors: active)");

    if (forced?.matches) {
        return "high-contrast";
    }

    const darkScheme = window.matchMedia("(prefers-color-scheme: dark)");

    return darkScheme?.matches ? "dark" : "light";
}

function attachListener(mediaQuery, handler) {
    if (!mediaQuery) {
        return () => {
        };
    }

    if (typeof mediaQuery.addEventListener === "function") {
        mediaQuery.addEventListener("change", handler);

        return () => mediaQuery.removeEventListener("change", handler);
    }

    if (typeof mediaQuery.addEventListener === "function") {
        mediaQuery.addEventListener(handler);

        return () => mediaQuery.removeEventListener(handler);
    }

    return () => {
    };
}

export function getSystemTheme() {
    return readSystemTheme();
}

export function registerSystemThemeListener(dotNetRef) {
    if (!dotNetRef || typeof window === "undefined" || typeof window.matchMedia !== "function") {
        return;
    }

    const forced = window.matchMedia("(forced-colors: active)");
    const dark = window.matchMedia("(prefers-color-scheme: dark)");

    const notify = () => {
        try {
            dotNetRef.invokeMethodAsync("OnSystemThemeChanged", readSystemTheme());
        } catch (error) {
            // Ignore invocation errors (component disposed).
        }
    };

    const disposers = [attachListener(forced, notify), attachListener(dark, notify)];

    listenerRegistry.set(dotNetRef, disposers);
}

export function disposeThemeListener(dotNetRef) {
    if (!dotNetRef) {
        return;
    }

    const disposers = listenerRegistry.get(dotNetRef);

    if (!disposers) {
        return;
    }

    for (const dispose of disposers) {
        try {
            dispose();
        } catch (error) {
            // Ignore disposal errors.
        }
    }

    listenerRegistry.delete(dotNetRef);
}
