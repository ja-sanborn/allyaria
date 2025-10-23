namespace Allyaria.Theming.Themes;

public sealed record Theme(
    ThemeVariant Light,
    ThemeVariant Dark,
    ThemeVariant HighContrastLight,
    ThemeVariant HighContrastDark
)
{
    public static readonly Theme Empty = new(
        ThemeVariant.Empty, ThemeVariant.Empty, ThemeVariant.Empty, ThemeVariant.Empty
    );

    public CssBuilder BuildCss(CssBuilder builder,
        ThemeType theme,
        ComponentType type,
        ComponentState state,
        string? varPrefix = null)
    {
        var prefix = varPrefix.ToCssName();

        if (!string.IsNullOrWhiteSpace(prefix))
        {
            prefix = $"{prefix}-{theme}";
        }

        switch (theme)
        {
            case ThemeType.Dark:
                builder = Dark.BuildCss(builder, type, state, prefix);

                break;

            case ThemeType.HighContrastDark:
                builder = HighContrastDark.BuildCss(builder, type, state, prefix);

                break;

            case ThemeType.HighContrastLight:
                builder = HighContrastLight.BuildCss(builder, type, state, prefix);

                break;

            default:
                builder = Light.BuildCss(builder, type, state, prefix);

                break;
        }

        return builder;
    }

    public static Theme FromDefault(PaletteColor? paletteColor = null, string? fontFamily = null)
        => new(
            ThemeVariant.FromDefault(paletteColor ?? new PaletteColor(), ThemeType.Light, fontFamily),
            ThemeVariant.FromDefault(paletteColor ?? new PaletteColor(), ThemeType.Dark, fontFamily),
            ThemeVariant.FromDefault(paletteColor ?? new PaletteColor(), ThemeType.HighContrastLight, fontFamily),
            ThemeVariant.FromDefault(paletteColor ?? new PaletteColor(), ThemeType.HighContrastDark, fontFamily)
        );

    public Theme Merge(Theme other)
        => SetDark(Dark.Merge(other.Dark))
            .SetHighContrastDark(HighContrastDark.Merge(other.HighContrastDark))
            .SetHighContrastLight(HighContrastLight.Merge(other.HighContrastLight))
            .SetLight(Light.Merge(other.Light));

    public Theme SetDark(ThemeVariant value)
        => this with
        {
            Dark = value
        };

    public Theme SetHighContrastDark(ThemeVariant value)
        => this with
        {
            HighContrastDark = value
        };

    public Theme SetHighContrastLight(ThemeVariant value)
        => this with
        {
            HighContrastLight = value
        };

    public Theme SetLight(ThemeVariant value)
        => this with
        {
            Light = value
        };

    public string ToCss(ThemeType themeType, ComponentType component, ComponentState state, string? varPrefix = "")
        => BuildCss(new CssBuilder(), themeType, component, state, varPrefix).ToString();

    public string ToCssVars(ThemeType themeType)
    {
        var builder = new CssBuilder();
        var componentItems = Enum.GetValues<ComponentType>();
        var stateItems = Enum.GetValues<ComponentState>();

        foreach (var component in componentItems)
        {
            foreach (var state in stateItems)
            {
                builder = BuildCss(builder, themeType, component, state, ThemingDefaults.VarPrefix);
            }
        }

        return builder.ToString();
    }
}
