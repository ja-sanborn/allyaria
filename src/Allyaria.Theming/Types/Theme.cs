namespace Allyaria.Theming.Types;

public readonly record struct Theme(
    StyleVariant Light,
    StyleVariant Dark,
    StyleVariant HighContrastLight,
    StyleVariant HighContrastDark
)
{
    public Theme Cascade(Theme? value = null)
        => value is null
            ? this
            : Cascade(value.Value.Light, value.Value.Dark, value.Value.HighContrastLight, value.Value.HighContrastDark);

    public Theme Cascade(StyleVariant? light = null,
        StyleVariant? dark = null,
        StyleVariant? highContrastLight = null,
        StyleVariant? highContrastDark = null)
        => this with
        {
            Dark = Dark.Cascade(dark),
            Light = Light.Cascade(light),
            HighContrastDark = HighContrastDark.Cascade(highContrastDark),
            HighContrastLight = HighContrastLight.Cascade(highContrastLight)
        };

    public string ToCss(ThemeType themeType)
    {
        var output = new HashSet<string>(StringComparer.Ordinal);
        var componentItems = Enum.GetValues<ComponentType>();
        var stateItems = Enum.GetValues<ComponentState>();

        foreach (var component in componentItems)
        {
            foreach (var state in stateItems)
            {
                var css = ToCss(themeType, component, state, StyleDefaults.VarPrefix);
                var parts = css.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                foreach (var part in parts)
                {
                    if (part.Length > 0)
                    {
                        _ = output.Add(part);
                    }
                }
            }
        }

        return string.Join(';', output);
    }

    public string ToCss(ThemeType themeType, ComponentType component, ComponentState state, string? varPrefix = "")
    {
        var builder = new StringBuilder();

        switch (themeType)
        {
            case ThemeType.Dark:
                builder.Append(Dark.ToCss(component, state, varPrefix));

                break;
            case ThemeType.Light:
                builder.Append(Light.ToCss(component, state, varPrefix));

                break;
            case ThemeType.HighContrastDark:
                builder.Append(HighContrastDark.ToCss(component, state, varPrefix));

                break;
            case ThemeType.HighContrastLight:
                builder.Append(HighContrastLight.ToCss(component, state, varPrefix));

                break;
        }

        return builder.ToString();
    }
}
