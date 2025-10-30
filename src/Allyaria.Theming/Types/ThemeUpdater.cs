namespace Allyaria.Theming.Types;

public readonly record struct ThemeUpdater(
    ThemeNavigator Navigator,
    IStyleValue? Value,
    ThemeStyle? Style,
    ThemeState? State,
    ThemeVariant? Variant,
    ThemeComponent? Component
);
