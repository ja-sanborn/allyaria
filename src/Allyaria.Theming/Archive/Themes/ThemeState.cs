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

    public static ThemeState FromBrand(BrandTheme brand,
        ThemeType themeType,
        FontType fontType,
        PaletteType paletteType,
        ComponentState state)
        => new(
            Border: ThemeBorders.BorderNone,
            Overflow: ThemeGroupOverflow.Empty,
            Palette: brand.GetPalette(themeType: themeType, paletteType: paletteType, state: state),
            Position: ThemeGroupPosition.Empty,
            Sizing: ThemeSizing.MarginPadding,
            Typography: ThemeGroupTypography.FromBrand(brand: brand.Font, themeType: themeType, fontType: fontType),
            TypographyDisplay: ThemeGroupTypographyDisplay.Empty
        );

    public ThemeState Merge(ThemeState other)
        => SetBorder(value: Border.Merge(other: other.Border))
            .SetOverflow(value: Overflow.Merge(other: other.Overflow))
            .SetPalette(value: Palette.Merge(other: other.Palette))
            .SetPosition(value: Position.Merge(other: other.Position))
            .SetSizing(value: Sizing.Merge(other: other.Sizing))
            .SetTypography(value: Typography.Merge(other: other.Typography))
            .SetTypographyDisplay(value: TypographyDisplay.Merge(other: other.TypographyDisplay));

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
