namespace Allyaria.Theming.Services;

public sealed class ThemingService : IThemingService
{
    public event EventHandler? ThemeChanged;

    public ThemeType EffectiveType { get; private set; } = ThemeType.System;

    public ThemeType StoredType { get; private set; } = ThemeType.System;

    public Theme Theme { get; private set; } = Theme.FromDefault();

    private void OnThemeChanged() => ThemeChanged?.Invoke(sender: this, e: EventArgs.Empty);

    public void SetEffectiveType(ThemeType themeType)
    {
        if (themeType == ThemeType.System || EffectiveType == themeType)
        {
            return;
        }

        EffectiveType = themeType;
        OnThemeChanged();
    }

    public void SetStoredType(ThemeType themeType)
    {
        if (StoredType == themeType)
        {
            return;
        }

        StoredType = themeType;

        if (themeType != ThemeType.System)
        {
            SetEffectiveType(themeType: themeType);
        }
        else
        {
            ThemeChanged?.Invoke(sender: this, e: EventArgs.Empty);
        }
    }

    public void SetTheme(Theme theme)
    {
        var effectiveTheme = theme == Theme.Empty
            ? Theme.FromDefault()
            : theme;

        if (Theme == effectiveTheme)
        {
            return;
        }

        Theme = effectiveTheme;
        OnThemeChanged();
    }
}
