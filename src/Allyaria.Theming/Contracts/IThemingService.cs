namespace Allyaria.Theming.Contracts;

public interface IThemingService
{
    event EventHandler? ThemeChanged;

    ThemeType EffectiveType { get; }

    ThemeType StoredType { get; }

    string GetComponentCss(string prefix, ComponentType componentType, ComponentState componentState);

    string GetDocumentCss();

    void SetEffectiveType(ThemeType themeType);

    void SetStoredType(ThemeType themeType);
}
