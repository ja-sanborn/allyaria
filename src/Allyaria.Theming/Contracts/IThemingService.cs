namespace Allyaria.Theming.Contracts;

public interface IThemingService
{
    event EventHandler? ThemeChanged;

    ThemeType EffectiveType { get; }

    ThemeType StoredType { get; }

    Theme Theme { get; }

    void SetEffectiveType(ThemeType type);

    void SetStoredType(ThemeType themeType);

    void SetTheme(Theme theme);
}
