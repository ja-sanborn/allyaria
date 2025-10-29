namespace Allyaria.Theming.Types;

public readonly record struct ThemeUpdater(
    ThemeNavigator Navigator,
    ThemeVariant? Variant,
    ThemeState? State,
    ThemeStyle? Style,
    IStyleValue? Value
);
