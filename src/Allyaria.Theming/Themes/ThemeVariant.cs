namespace Allyaria.Theming.Themes;

public sealed record ThemeVariant(ThemeComponent Surface)
{
    public static readonly ThemeVariant Empty = new(ThemeComponent.Empty);

    public CssBuilder BuildCss(CssBuilder builder, ComponentType type, ComponentState state, string? varPrefix = null)
    {
        var prefix = varPrefix.ToCssName();

        if (!string.IsNullOrWhiteSpace(prefix))
        {
            prefix = $"{prefix}-{type}";
        }

        switch (type)
        {
            case ComponentType.Surface:
                builder = Surface.BuildCss(builder, state, prefix);

                break;
        }

        return builder;
    }

    public static ThemeVariant FromDefault(PaletteColor paletteColor, ThemeType themeType, string? fontFamily = null)
        => new(ThemeComponent.FromDefault(paletteColor, themeType, PaletteType.Surface, fontFamily));

    public ThemeVariant Merge(ThemeVariant other) => SetSurface(Surface.Merge(other.Surface));

    public ThemeVariant SetSurface(ThemeComponent value)
        => this with
        {
            Surface = value
        };

    public string ToCss(ComponentType component, ComponentState state, string? varPrefix = "")
        => BuildCss(new CssBuilder(), component, state, varPrefix).ToString();
}
