namespace Allyaria.Theming.Themes;

public sealed record ThemeStyle(
    ThemeGroupBorder Border,
    ThemeGroupOverflow Overflow,
    ThemeGroupPalette Palette,
    ThemeGroupPosition Position,
    ThemeGroupSizing Sizing,
    ThemeGroupTypography Typography
)
{
    public static readonly ThemeStyle Empty = new(
        ThemeGroupBorder.Empty, ThemeGroupOverflow.Empty, ThemeGroupPalette.Empty, ThemeGroupPosition.Empty,
        ThemeGroupSizing.Empty, ThemeGroupTypography.Empty
    );

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder = Border.BuildCss(builder, varPrefix);
        builder = Overflow.BuildCss(builder, varPrefix);
        builder = Palette.BuildCss(builder, varPrefix);
        builder = Position.BuildCss(builder, varPrefix);
        builder = Sizing.BuildCss(builder, varPrefix);
        builder = Typography.BuildCss(builder, varPrefix);

        return builder;
    }

    public static ThemeStyle FromDefault(PaletteColor paletteColor,
        ThemeType themeType,
        PaletteType paletteType,
        string? fontFamily = null)
    {
        var border = ThemeGroupBorder.FromDefault();
        var overflow = ThemeGroupOverflow.FromDefault();
        var palette = ThemeGroupPalette.FromDefault(paletteColor, themeType, paletteType);
        var position = ThemeGroupPosition.FromDefault();
        var sizing = ThemeGroupSizing.FromDefault();
        var typography = ThemeGroupTypography.FromDefault(fontFamily);

        return new ThemeStyle(border, overflow, palette, position, sizing, typography);
    }

    public ThemeStyle Merge(ThemeStyle other)
        => SetBorder(Border.Merge(other.Border))
            .SetOverflow(Overflow.Merge(other.Overflow))
            .SetPalette(Palette.Merge(other.Palette))
            .SetPosition(Position.Merge(other.Position))
            .SetSizing(Sizing.Merge(other.Sizing))
            .SetTypography(Typography.Merge(other.Typography));

    public ThemeStyle SetBorder(ThemeGroupBorder value)
        => this with
        {
            Border = value
        };

    public ThemeStyle SetOverflow(ThemeGroupOverflow value)
        => this with
        {
            Overflow = value
        };

    public ThemeStyle SetPalette(ThemeGroupPalette value)
        => this with
        {
            Palette = value
        };

    public ThemeStyle SetPosition(ThemeGroupPosition value)
        => this with
        {
            Position = value
        };

    public ThemeStyle SetSizing(ThemeGroupSizing value)
        => this with
        {
            Sizing = value
        };

    public ThemeStyle SetTypography(ThemeGroupTypography value)
        => this with
        {
            Typography = value
        };
}
