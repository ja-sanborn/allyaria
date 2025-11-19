namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeColorApplierTests
{
    [Fact]
    public void Constructor_Should_CreateBackgroundAndContentAndOutlineColors_When_HasBackgroundAndNotOutline()
    {
        // Arrange
        var themeMapper = new ThemeMapper();
        const bool isHighContrast = false;
        const bool isVariant = false;
        const bool hasBackground = true;
        const bool isOutline = false;
        const ComponentType componentType = ComponentType.Surface;
        const PaletteType paletteType = PaletteType.Primary;

        // Act
        var sut = new ThemeColorApplier(
            themeMapper: themeMapper,
            isHighContrast: isHighContrast,
            componentType: componentType,
            paletteType: paletteType,
            isVariant: isVariant,
            hasBackground: hasBackground,
            isOutline: isOutline
        );

        // Assert
        // hasBackground -> BackgroundColor (1)
        // !isOutline    -> Accent, Border, Caret, Color, TextDecoration (5)
        // always        -> Outline (1)
        // Each StyleType yields 14 updaters (2 themes * 7 states) => 7 * 14 = 98
        sut.Count.Should().Be(expected: 98);

        var styleTypePerUpdater = sut
            .Select(selector: u => u.Navigator.StyleTypes.Single())
            .ToList();

        styleTypePerUpdater.Distinct().Should().BeEquivalentTo(
            expectation: new[]
            {
                StyleType.BackgroundColor,
                StyleType.AccentColor,
                StyleType.BorderColor,
                StyleType.CaretColor,
                StyleType.Color,
                StyleType.TextDecorationColor,
                StyleType.OutlineColor
            }
        );

        styleTypePerUpdater.Count(predicate: t => t == StyleType.BackgroundColor).Should().Be(expected: 14);
        styleTypePerUpdater.Count(predicate: t => t == StyleType.AccentColor).Should().Be(expected: 14);
        styleTypePerUpdater.Count(predicate: t => t == StyleType.BorderColor).Should().Be(expected: 14);
        styleTypePerUpdater.Count(predicate: t => t == StyleType.CaretColor).Should().Be(expected: 14);
        styleTypePerUpdater.Count(predicate: t => t == StyleType.Color).Should().Be(expected: 14);
        styleTypePerUpdater.Count(predicate: t => t == StyleType.TextDecorationColor).Should().Be(expected: 14);
        styleTypePerUpdater.Count(predicate: t => t == StyleType.OutlineColor).Should().Be(expected: 14);

        sut.SelectMany(selector: u => u.Navigator.ComponentTypes)
            .Distinct()
            .Should().BeEquivalentTo(
                expectation: new[]
                {
                    componentType
                }
            );

        sut.SelectMany(selector: u => u.Navigator.ThemeTypes)
            .Distinct()
            .Should().BeEquivalentTo(
                expectation: new[]
                {
                    ThemeType.Dark,
                    ThemeType.Light
                }
            );
    }

    [Fact]
    public void Constructor_Should_CreateBackgroundAndOutlineColorsOnly_When_IsOutlineAndHasBackground()
    {
        // Arrange
        var themeMapper = new ThemeMapper();
        const bool isHighContrast = false;
        const bool isVariant = false;
        const bool hasBackground = true;
        const bool isOutline = true;
        const ComponentType componentType = ComponentType.Heading2;
        const PaletteType paletteType = PaletteType.Surface;

        // Act
        var sut = new ThemeColorApplier(
            themeMapper: themeMapper,
            isHighContrast: isHighContrast,
            componentType: componentType,
            paletteType: paletteType,
            isVariant: isVariant,
            hasBackground: hasBackground,
            isOutline: isOutline
        );

        // Assert
        // hasBackground -> BackgroundColor (1)
        // isOutline     -> no Accent/Border/Caret/Color/TextDecoration
        // always        -> Outline (1)
        // => 2 style types * 14 updaters each = 28
        sut.Count.Should().Be(expected: 28);

        var styleTypePerUpdater = sut
            .Select(selector: u => u.Navigator.StyleTypes.Single())
            .ToList();

        styleTypePerUpdater.Distinct().Should().BeEquivalentTo(
            expectation: new[]
            {
                StyleType.BackgroundColor,
                StyleType.OutlineColor
            }
        );

        styleTypePerUpdater.Count(predicate: t => t == StyleType.BackgroundColor).Should().Be(expected: 14);
        styleTypePerUpdater.Count(predicate: t => t == StyleType.OutlineColor).Should().Be(expected: 14);

        sut.SelectMany(selector: u => u.Navigator.ComponentTypes)
            .Distinct()
            .Should().BeEquivalentTo(
                expectation: new[]
                {
                    componentType
                }
            );

        sut.SelectMany(selector: u => u.Navigator.ThemeTypes)
            .Distinct()
            .Should().BeEquivalentTo(
                expectation: new[]
                {
                    ThemeType.Dark,
                    ThemeType.Light
                }
            );
    }

    [Fact]
    public void Constructor_Should_CreateOnlyOutlineColors_When_IsOutlineAndNoBackground()
    {
        // Arrange
        var themeMapper = new ThemeMapper();
        const bool isHighContrast = false;
        const bool isVariant = false;
        const bool hasBackground = false;
        const bool isOutline = true;
        const ComponentType componentType = ComponentType.Heading1;
        const PaletteType paletteType = PaletteType.Secondary;

        // Act
        var sut = new ThemeColorApplier(
            themeMapper: themeMapper,
            isHighContrast: isHighContrast,
            componentType: componentType,
            paletteType: paletteType,
            isVariant: isVariant,
            hasBackground: hasBackground,
            isOutline: isOutline
        );

        // Assert
        // hasBackground == false  -> no BackgroundColor
        // isOutline == true       -> no Accent/Border/Caret/Color/TextDecoration
        // always                  -> Outline only => 14 updaters
        sut.Count.Should().Be(expected: 14);

        var styleTypes = sut
            .Select(selector: u => u.Navigator.StyleTypes.Single())
            .Distinct()
            .ToList();

        styleTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                StyleType.OutlineColor
            }
        );

        sut.SelectMany(selector: u => u.Navigator.ComponentTypes)
            .Distinct()
            .Should().BeEquivalentTo(
                expectation: new[]
                {
                    componentType
                }
            );

        sut.SelectMany(selector: u => u.Navigator.ThemeTypes)
            .Distinct()
            .Should().BeEquivalentTo(
                expectation: new[]
                {
                    ThemeType.Dark,
                    ThemeType.Light
                }
            );
    }

    [Fact]
    public void Constructor_Should_UseHighContrastVariantColors_When_IsHighContrastAndVariant()
    {
        // Arrange
        var themeMapper = new ThemeMapper();
        const bool isHighContrast = true;
        const bool isVariant = true;
        const bool hasBackground = false;
        const bool isOutline = false;
        const ComponentType componentType = ComponentType.Heading3;
        const PaletteType paletteType = PaletteType.Error;

        // Act
        var sut = new ThemeColorApplier(
            themeMapper: themeMapper,
            isHighContrast: isHighContrast,
            componentType: componentType,
            paletteType: paletteType,
            isVariant: isVariant,
            hasBackground: hasBackground,
            isOutline: isOutline
        );

        // Assert
        // hasBackground == false -> no BackgroundColor
        // !isOutline             -> Accent, Border, Caret, Color, TextDecoration (5)
        // always                 -> Outline (1)
        // => 6 style types * 14 updaters each = 84
        sut.Count.Should().Be(expected: 84);

        var styleTypesDistinct = sut
            .Select(selector: u => u.Navigator.StyleTypes.Single())
            .Distinct()
            .ToList();

        styleTypesDistinct.Should().BeEquivalentTo(
            expectation: new[]
            {
                StyleType.AccentColor,
                StyleType.BorderColor,
                StyleType.CaretColor,
                StyleType.Color,
                StyleType.TextDecorationColor,
                StyleType.OutlineColor
            }
        );

        sut.SelectMany(selector: u => u.Navigator.ThemeTypes)
            .Distinct()
            .Should().BeEquivalentTo(
                expectation: new[]
                {
                    ThemeType.HighContrastDark,
                    ThemeType.HighContrastLight
                }
            );

        sut.SelectMany(selector: u => u.Navigator.ComponentTypes)
            .Distinct()
            .Should().BeEquivalentTo(
                expectation: new[]
                {
                    componentType
                }
            );
    }
}
