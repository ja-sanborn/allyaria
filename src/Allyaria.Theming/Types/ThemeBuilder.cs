namespace Allyaria.Theming.Types;

public sealed class ThemeBuilder
{
    private Brand _brand;
    private bool _isReady;
    private Theme _theme = new();

    public Theme Build()
    {
        if (!_isReady)
        {
            Create();
        }

        _brand = new Brand();
        _theme = new Theme();
        _isReady = false;
        return _theme;
    }

    public ThemeBuilder Create(Brand? brand = null)
    {
        _brand = brand ?? new Brand();
        _theme = new Theme();



        _isReady = true;
        return this;
    }

    private SetBackgroundDark()
    {
        ThemeNavigator navigator = ThemeNavigator.Initialize
            .SetComponentTypes(ComponentType.Global)
            .SetThemeTypes(ThemeType.Dark)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

    }
}
