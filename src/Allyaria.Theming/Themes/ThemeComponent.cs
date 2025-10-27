namespace Allyaria.Theming.Themes;

public sealed record ThemeComponent(
    ThemeStyle Light,
    ThemeStyle Dark,
    ThemeStyle HighContrastLight,
    ThemeStyle HighContrastDark
)
{
    public static readonly ThemeComponent Empty = new(
        Light: ThemeStyle.Empty,
        Dark: ThemeStyle.Empty,
        HighContrastLight: ThemeStyle.Empty,
        HighContrastDark: ThemeStyle.Empty
    );

    public CssBuilder BuildCss(CssBuilder builder,
        ThemeType themeType,
        ComponentState state,
        string? varPrefix = null)
    {
        var prefix = varPrefix.ToCssName();

        if (!string.IsNullOrWhiteSpace(value: prefix))
        {
            prefix = $"{prefix}-{themeType}";
        }

        switch (themeType)
        {
            case ThemeType.Dark:
                builder = Dark.BuildCss(builder: builder, state: state, varPrefix: prefix);

                break;

            case ThemeType.HighContrastDark:
                builder = HighContrastDark.BuildCss(builder: builder, state: state, varPrefix: prefix);

                break;

            case ThemeType.HighContrastLight:
                builder = HighContrastLight.BuildCss(builder: builder, state: state, varPrefix: prefix);

                break;

            default:
                builder = Light.BuildCss(builder: builder, state: state, varPrefix: prefix);

                break;
        }

        return builder;
    }

    public static ThemeComponent FromBrand(BrandTheme brand,
        FontType fontType,
        PaletteType paletteType)
    {
        var dark = ThemeStyle.FromBrand(
            brand: brand, themeType: ThemeType.Dark, fontType: fontType, paletteType: paletteType
        );

        var highContrastLight = ThemeStyle.FromBrand(
            brand: brand, themeType: ThemeType.HighContrastLight, fontType: fontType, paletteType: paletteType
        );

        var highContrastDark = ThemeStyle.FromBrand(
            brand: brand, themeType: ThemeType.HighContrastDark, fontType: fontType, paletteType: paletteType
        );

        var light = ThemeStyle.FromBrand(
            brand: brand, themeType: ThemeType.Light, fontType: fontType, paletteType: paletteType
        );

        return new ThemeComponent(
            Light: light,
            Dark: dark,
            HighContrastLight: highContrastLight,
            HighContrastDark: highContrastDark
        );
    }

    public ThemeComponent Merge(ThemeComponent other)
        => SetDark(value: Dark.Merge(other: other.Dark))
            .SetHighContrastDark(value: HighContrastDark.Merge(other: other.HighContrastDark))
            .SetHighContrastLight(value: HighContrastLight.Merge(other: other.HighContrastLight))
            .SetLight(value: Light.Merge(other: other.Light));

    public ThemeComponent SetDark(ThemeStyle value)
        => this with
        {
            Dark = value
        };

    public ThemeComponent SetHighContrastDark(ThemeStyle value)
        => this with
        {
            HighContrastDark = value
        };

    public ThemeComponent SetHighContrastLight(ThemeStyle value)
        => this with
        {
            HighContrastLight = value
        };

    public ThemeComponent SetLight(ThemeStyle value)
        => this with
        {
            Light = value
        };

    public string ToCss(ThemeType themeType, ComponentState state, string? varPrefix = "")
        => BuildCss(builder: new CssBuilder(), themeType: themeType, state: state, varPrefix: varPrefix).ToString();
}
