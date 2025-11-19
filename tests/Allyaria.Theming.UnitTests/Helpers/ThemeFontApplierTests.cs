namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeFontApplierTests
{
    [Fact]
    public void Constructor_Should_AddFontFamilyUpdater_When_FontFaceProvided()
    {
        // Arrange
        var themeMapper = new ThemeMapper();
        var componentType = ComponentType.Heading1;

        // Act
        var sut = new ThemeFontApplier(
            themeMapper: themeMapper,
            isHighContrast: true,
            componentType: componentType,
            fontFace: FontFaceType.SansSerif
        );

        // Assert
        sut.Count.Should().Be(expected: 1);

        var updater = sut.Single();

        updater.Navigator.ComponentTypes.Should().ContainSingle()
            .Which.Should().Be(expected: componentType);

        updater.Navigator.StyleTypes.Should().ContainSingle()
            .Which.Should().Be(expected: StyleType.FontFamily);

        updater.Value.Should().BeOfType<StyleString>();
    }

    [Fact]
    public void Constructor_Should_AddPaletteColors_When_PaletteTypeProvided()
    {
        // Arrange
        var themeMapper = new ThemeMapper();
        var componentType = ComponentType.Text;
        var paletteType = PaletteType.Primary;

        // Act
        var sut = new ThemeFontApplier(
            themeMapper: themeMapper,
            isHighContrast: false,
            componentType: componentType,
            paletteType: paletteType
        );

        // Assert
        sut.Count.Should().BeGreaterThan(expected: 0);

        // All updaters should target the provided component type
        sut.SelectMany(selector: x => x.Navigator.ComponentTypes)
            .Distinct()
            .Should()
            .BeEquivalentTo(
                expectation: new[]
                {
                    componentType
                }
            );
    }

    [Fact]
    public void Constructor_Should_AddTypographyUpdaters_For_AllNonNullOptions()
    {
        // Arrange
        var themeMapper = new ThemeMapper();
        var componentType = ComponentType.Heading2;
        var paletteType = PaletteType.Secondary;

        // Baseline with palette only (executes palette branch)
        var baseline = new ThemeFontApplier(
            themeMapper: themeMapper,
            isHighContrast: false,
            componentType: componentType,
            paletteType: paletteType
        );

        var baselineCount = baseline.Count;

        // Act
        var sut = new ThemeFontApplier(
            themeMapper: themeMapper,
            isHighContrast: false,
            componentType: componentType,
            paletteType: paletteType,
            fontFace: FontFaceType.SansSerif,
            fontSize: "16px",
            fontStyle: 0,
            fontWeight: 0,
            lineHeight: "24px",
            marginBottom: "8px",
            textDecorationLine: 0,
            textDecorationStyle: 0,
            textDecorationThickness: "2px",
            textTransform: 0
        );

        // Assert
        // 1 (fontFace) +
        // 1 (fontSize) +
        // 1 (fontStyle) +
        // 1 (fontWeight) +
        // 1 (lineHeight) +
        // 2 (margin + padding) +
        // 1 (textDecorationLine) +
        // 1 (textDecorationStyle) +
        // 1 (textDecorationThickness) +
        // 1 (textTransform) = 11 additional updaters
        sut.Count.Should().Be(expected: baselineCount + 11);

        ThemeUpdater GetByStyle(StyleType styleType)
            => sut.Single(predicate: u => u.Navigator.StyleTypes.Contains(value: styleType));

        // Font family from fontFace
        var fontFamilyUpdater = GetByStyle(styleType: StyleType.FontFamily);
        fontFamilyUpdater.Value.Should().BeOfType<StyleString>();

        // Font size
        var fontSizeUpdater = GetByStyle(styleType: StyleType.FontSize);
        fontSizeUpdater.Value.Should().BeOfType<StyleLength>();

        // Font style
        var fontStyleUpdater = GetByStyle(styleType: StyleType.FontStyle);
        fontStyleUpdater.Value.Should().BeOfType<StyleFontStyle>();

        // Font weight
        var fontWeightUpdater = GetByStyle(styleType: StyleType.FontWeight);
        fontWeightUpdater.Value.Should().BeOfType<StyleFontWeight>();

        // Line height
        var lineHeightUpdater = GetByStyle(styleType: StyleType.LineHeight);
        lineHeightUpdater.Value.Should().BeOfType<StyleLength>();

        // Margin (group with bottom margin & zeroed sides)
        var marginUpdater = GetByStyle(styleType: StyleType.Margin);
        marginUpdater.Value.Should().BeOfType<StyleGroup>();

        // Padding reset
        var paddingUpdater = GetByStyle(styleType: StyleType.Padding);
        paddingUpdater.Value.Should().BeOfType<StyleLength>();

        // Text decoration line
        var decorationLineUpdater = GetByStyle(styleType: StyleType.TextDecorationLine);
        decorationLineUpdater.Value.Should().BeOfType<StyleTextDecorationLine>();

        // Text decoration style
        var decorationStyleUpdater = GetByStyle(styleType: StyleType.TextDecorationStyle);
        decorationStyleUpdater.Value.Should().BeOfType<StyleTextDecorationStyle>();

        // Text decoration thickness
        var decorationThicknessUpdater = GetByStyle(styleType: StyleType.TextDecorationThickness);
        decorationThicknessUpdater.Value.Should().BeOfType<StyleLength>();

        // Text transform
        var textTransformUpdater = GetByStyle(styleType: StyleType.TextTransform);
        textTransformUpdater.Value.Should().BeOfType<StyleTextTransform>();
    }

    [Fact]
    public void Constructor_Should_NotAddAnyUpdaters_When_AllOptionalArgumentsAreNullOrWhitespace()
    {
        // Arrange
        var themeMapper = new ThemeMapper();
        var componentType = ComponentType.Text;

        // Act
        var sut = new ThemeFontApplier(
            themeMapper: themeMapper,
            isHighContrast: false,
            componentType: componentType,
            paletteType: null,
            fontFace: null,
            fontSize: "   ",
            fontStyle: null,
            fontWeight: null,
            lineHeight: "   ",
            marginBottom: "   ",
            textDecorationLine: null,
            textDecorationStyle: null,
            textDecorationThickness: "   ",
            textTransform: null
        );

        // Assert
        sut.Count.Should().Be(expected: 0);
        sut.Should().BeEmpty();
    }
}
