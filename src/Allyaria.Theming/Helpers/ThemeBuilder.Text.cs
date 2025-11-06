namespace Allyaria.Theming.Helpers;

internal sealed partial class ThemeBuilder
{
    private void CreateHeading1(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading1,
                fontSize: Sizing.RelativeLarge4,
                fontWeight: StyleFontWeight.Kind.Weight700,
                lineHeight: "1.2",
                marginBottom: Sizing.RelativeLarge1
            )
        );

    private void CreateHeading2(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading2,
                fontSize: Sizing.RelativeLarge3,
                fontWeight: StyleFontWeight.Kind.Weight700,
                lineHeight: "1.25",
                marginBottom: Sizing.Relative
            )
        );

    private void CreateHeading3(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading3,
                fontSize: Sizing.RelativeLarge2,
                fontWeight: StyleFontWeight.Kind.Weight600,
                lineHeight: "1.3",
                marginBottom: Sizing.RelativeSmall2
            )
        );

    private void CreateHeading4(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading4,
                fontSize: Sizing.RelativeLarge1,
                fontWeight: StyleFontWeight.Kind.Weight600,
                lineHeight: "1.4",
                marginBottom: Sizing.RelativeSmall3
            )
        );

    private void CreateHeading5(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading5,
                fontSize: Sizing.Relative,
                fontWeight: StyleFontWeight.Kind.Weight600,
                lineHeight: "1.5",
                marginBottom: Sizing.RelativeSmall4
            )
        );

    private void CreateHeading6(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading6,
                fontSize: Sizing.RelativeSmall1,
                fontWeight: StyleFontWeight.Kind.Weight500,
                lineHeight: "1.5",
                marginBottom: Sizing.RelativeSmall4
            )
        );

    private void CreateLink(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Link,
                paletteType: PaletteType.Primary,
                textDecorationLine: StyleTextDecorationLine.Kind.Underline,
                textDecorationStyle: StyleTextDecorationStyle.Kind.Solid,
                textDecorationThickness: Sizing.Thin
            )
        );

    private void CreateText(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Text,
                fontSize: Sizing.Relative,
                lineHeight: "1.5",
                marginBottom: Sizing.Relative
            )
        );
}
