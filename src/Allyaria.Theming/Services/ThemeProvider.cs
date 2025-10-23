namespace Allyaria.Theming.Services;

public sealed class ThemeProvider : IThemeProvider
{
    private readonly Theme _theme;

    public ThemeProvider(Theme? theme = null, ThemeType themeType = ThemeType.System)
        => (_theme, ThemeType) = (theme ?? Theme.FromDefault(), themeType);

    public event EventHandler? ThemeChanged;

    public ThemeType ThemeType { get; }

    public string GetCss(ComponentType componentType,
        ComponentState state = ComponentState.Default,
        string? varPrefix = "")
        => _theme.ToCss(ThemeType, componentType, state, varPrefix);

    public string GetCssVars() => _theme.ToCssVars(ThemeType);

    private void OnThemeChanged(bool raiseEvent)
    {
        if (!raiseEvent)
        {
            return;
        }

        var handler = ThemeChanged;
        handler?.Invoke(this, EventArgs.Empty);
    }
}
