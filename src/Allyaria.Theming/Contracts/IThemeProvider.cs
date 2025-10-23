namespace Allyaria.Theming.Contracts;

public interface IThemeProvider
{
    event EventHandler? ThemeChanged;

    ThemeType ThemeType { get; }

    string GetCss(ComponentType componentType,
        ComponentState state = ComponentState.Default,
        string? varPrefix = "");

    string GetCssVars();
}
