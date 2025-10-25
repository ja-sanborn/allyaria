namespace Allyaria.Theming.Themes;

public sealed record ThemeState(
    ThemeGroupBorder Border,
    ThemeGroupOverflow Overflow,
    ThemeGroupPalette Palette,
    ThemeGroupPosition Position,
    ThemeGroupSizing Sizing,
    ThemeGroupTypography Typography,
    ThemeGroupTypographyDisplay TypographyDisplay
)
{
    public static readonly ThemeState Empty = new(
        Border: ThemeGroupBorder.Empty,
        Overflow: ThemeGroupOverflow.Empty,
        Palette: ThemeGroupPalette.Empty,
        Position: ThemeGroupPosition.Empty,
        Sizing: ThemeGroupSizing.Empty,
        Typography: ThemeGroupTypography.Empty,
        TypographyDisplay: ThemeGroupTypographyDisplay.Empty
    );

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder = Border.BuildCss(builder: builder, varPrefix: varPrefix);
        builder = Overflow.BuildCss(builder: builder, varPrefix: varPrefix);
        builder = Palette.BuildCss(builder: builder, varPrefix: varPrefix);
        builder = Position.BuildCss(builder: builder, varPrefix: varPrefix);
        builder = Sizing.BuildCss(builder: builder, varPrefix: varPrefix);
        builder = Typography.BuildCss(builder: builder, varPrefix: varPrefix);
        builder = TypographyDisplay.BuildCss(builder: builder, varPrefix: varPrefix);

        return builder;
    }

    public static ThemeState FromDefault(ThemeType themeType, PaletteType paletteType, FontType fontType)
        => new(
            Border: new ThemeGroupBorder(),
            Overflow: new ThemeGroupOverflow(),
            Palette: new ThemeGroupPalette(themeType: themeType, paletteType: paletteType),
            Position: new ThemeGroupPosition(),
            Sizing: new ThemeGroupSizing(),
            Typography: new ThemeGroupTypography(ThemeType: themeType, FontType: fontType),
            TypographyDisplay: new ThemeGroupTypographyDisplay()
        );

    public ThemeState Merge(ThemeState other)
        => SetBorder(Border.Merge(other.Border))
            .SetOverflow(Overflow.Merge(other.Overflow))
            .SetPalette(Palette.Merge(other.Palette))
            .SetPosition(Position.Merge(other.Position))
            .SetSizing(Sizing.Merge(other.Sizing))
            .SetTypography(Typography.Merge(other.Typography))
            .SetTypographyDisplay(TypographyDisplay.Merge(other.TypographyDisplay));

    public ThemeState SetBorder(ThemeGroupBorder value)
        => this with
        {
            Border = value
        };

    public ThemeState SetOverflow(ThemeGroupOverflow value)
        => this with
        {
            Overflow = value
        };

    public ThemeState SetPalette(ThemeGroupPalette value)
        => this with
        {
            Palette = value
        };

    public ThemeState SetPosition(ThemeGroupPosition value)
        => this with
        {
            Position = value
        };

    public ThemeState SetSizing(ThemeGroupSizing value)
        => this with
        {
            Sizing = value
        };

    public ThemeState SetTypography(ThemeGroupTypography value)
        => this with
        {
            Typography = value
        };

    public ThemeState SetTypographyDisplay(ThemeGroupTypographyDisplay value)
        => this with
        {
            TypographyDisplay = value
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
