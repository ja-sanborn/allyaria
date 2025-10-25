namespace Allyaria.Theming.Themes;

public sealed record ThemeComponent(
    ThemeStyle Light,
    ThemeStyle Dark,
    ThemeStyle HighContrastLight,
    ThemeStyle HighContrastDark
)
{
    public static readonly ThemeComponent Empty = new(
        Light: ThemeStyle.Empty, Dark: ThemeStyle.Empty, HighContrastLight: ThemeStyle.Empty,
        HighContrastDark: ThemeStyle.Empty
    );

    public CssBuilder BuildCss(CssBuilder builder,
        ThemeType themeType,
        ComponentState state,
        string? varPrefix = null)
    {
        var prefix = varPrefix.ToCssName();

        if (!string.IsNullOrWhiteSpace(prefix))
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

    public static ThemeComponent FromDefault(PaletteType paletteType, FontType fontType)
        => new(
            Light: ThemeStyle.FromDefault(themeType: ThemeType.Light, paletteType: paletteType, fontType: fontType),
            Dark: ThemeStyle.FromDefault(themeType: ThemeType.Dark, paletteType: paletteType, fontType: fontType),
            HighContrastLight: ThemeStyle.FromDefault(
                themeType: ThemeType.HighContrastLight, paletteType: paletteType, fontType: fontType
            ),
            HighContrastDark: ThemeStyle.FromDefault(
                themeType: ThemeType.HighContrastDark, paletteType: paletteType, fontType: fontType
            )
        );

    public ThemeComponent Merge(ThemeComponent other)
        => SetDark(Dark.Merge(other.Dark))
            .SetHighContrastDark(HighContrastDark.Merge(other.HighContrastDark))
            .SetHighContrastLight(HighContrastLight.Merge(other.HighContrastLight))
            .SetLight(Light.Merge(other.Light));

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

    public ThemeComponent Update(ColorPalette colorPalette, FontDefinition fontDefinion)
        => SetDark(Dark.Update(colorPalette: colorPalette, fontDefinion: fontDefinion))
            .SetHighContrastDark(HighContrastDark.Update(colorPalette: colorPalette, fontDefinion: fontDefinion))
            .SetHighContrastLight(HighContrastLight.Update(colorPalette: colorPalette, fontDefinion: fontDefinion))
            .SetLight(Light.Update(colorPalette: colorPalette, fontDefinion: fontDefinion));

    public ThemeComponent UpdateFontFamily(FontDefinition fontDefinion)
        => SetDark(Dark.UpdateFontFamily(fontDefinion))
            .SetHighContrastDark(HighContrastDark.UpdateFontFamily(fontDefinion))
            .SetHighContrastLight(HighContrastLight.UpdateFontFamily(fontDefinion))
            .SetLight(Light.UpdateFontFamily(fontDefinion));

    public ThemeComponent UpdatePalette(ColorPalette colorPalette)
        => SetDark(Dark.UpdatePalette(colorPalette))
            .SetHighContrastDark(HighContrastDark.UpdatePalette(colorPalette))
            .SetHighContrastLight(HighContrastLight.UpdatePalette(colorPalette))
            .SetLight(Light.UpdatePalette(colorPalette));
}
