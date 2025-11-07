namespace Allyaria.Theming.Services;

public sealed class ThemingService : IThemingService
{
    private readonly Theme _theme;

    internal ThemingService(Theme theme, ThemeType themeType = ThemeType.System)
    {
        _theme = theme;

        StoredType = themeType;

        EffectiveType = StoredType is ThemeType.System
            ? ThemeType.Light
            : StoredType;
    }

    public event EventHandler? ThemeChanged;

    public ThemeType EffectiveType { get; private set; } = ThemeType.System;

    public ThemeType StoredType { get; private set; } = ThemeType.System;

    public string GetComponentCss(string prefix, ComponentType componentType, ComponentState componentState)
        => _theme.GetComponentCss(
            prefix: prefix,
            componentType: componentType,
            themeType: EffectiveType,
            componentState: componentState
        );

    public string GetDocumentCss() => _theme.GetDocumentCss(themeType: EffectiveType);

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
}
