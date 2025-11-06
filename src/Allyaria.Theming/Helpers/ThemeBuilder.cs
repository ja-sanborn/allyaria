namespace Allyaria.Theming.Helpers;

internal sealed partial class ThemeBuilder
{
    private bool _isReady;
    private ThemeMapper _mapper = new();
    private Theme _theme = new();

    private void ApplyTheme(ThemeApplierBase applier)
    {
        foreach (var updater in applier)
        {
            Set(updater: updater);
        }
    }

    public Theme Build()
    {
        if (!_isReady)
        {
            Create();
        }

        var theme = _theme;

        _mapper = new ThemeMapper();
        _theme = new Theme();
        _isReady = false;

        return theme;
    }

    public ThemeBuilder Create(Brand? brand = null)
    {
        _mapper = new ThemeMapper(brand: brand);
        _theme = new Theme();

        for (var contrast = 0; contrast < 2; contrast++)
        {
            var isHighContrast = contrast is 1;

            CreateGlobalBody(isHighContrast: isHighContrast);
            CreateGlobalFocus(isHighContrast: isHighContrast);
            CreateGlobalHtml(isHighContrast: isHighContrast);

            CreateHeading1(isHighContrast: isHighContrast);
            CreateHeading2(isHighContrast: isHighContrast);
            CreateHeading3(isHighContrast: isHighContrast);
            CreateHeading4(isHighContrast: isHighContrast);
            CreateHeading5(isHighContrast: isHighContrast);
            CreateHeading6(isHighContrast: isHighContrast);

            CreateLink(isHighContrast: isHighContrast);
            CreateSurface(isHighContrast: isHighContrast);
            CreateText(isHighContrast: isHighContrast);
        }

        _isReady = true;

        return this;
    }

    public ThemeBuilder Set(ThemeUpdater updater)
    {
        if (updater.Navigator.ThemeTypes.Contains(value: ThemeType.System))
        {
            throw new AryArgumentException(
                message: "System theme cannot be set directly.", argName: nameof(updater.Value)
            );
        }

        if (updater.Navigator.ComponentStates.Contains(value: ComponentState.Hidden) ||
            updater.Navigator.ComponentStates.Contains(value: ComponentState.ReadOnly))
        {
            throw new AryArgumentException(
                message: "Hidden and read-only states cannot be set directly.", argName: nameof(updater.Value)
            );
        }

        if (updater.Navigator.ThemeTypes.Contains(value: ThemeType.HighContrastDark) ||
            updater.Navigator.ThemeTypes.Contains(value: ThemeType.HighContrastLight))
        {
            throw new AryArgumentException(
                message: "Cannot alter High Contrast themes.", argName: nameof(updater.Value)
            );
        }

        if (updater.Navigator.ComponentStates.Contains(value: ComponentState.Focused) &&
            (updater.Navigator.StyleTypes.Contains(value: StyleType.OutlineOffset) ||
                updater.Navigator.StyleTypes.Contains(value: StyleType.OutlineStyle) ||
                updater.Navigator.StyleTypes.Contains(value: StyleType.OutlineWidth)))
        {
            throw new AryArgumentException(
                message: "Cannot change focused outline offset, style or width.", argName: nameof(updater.Value)
            );
        }

        _theme = _theme.Set(updater: updater);

        return this;
    }
}
