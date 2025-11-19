namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeMapperTests
{
    [Fact]
    public void GetColors_Should_FallbackToSurfacePalette_When_PaletteTypeIsUnknown()
    {
        // Arrange
        var sut = new ThemeMapper();
        var unknownPaletteType = (PaletteType)999;

        // Act
        var result = sut.GetColors(
            isHighContrast: false,
            isVariant: false,
            paletteType: unknownPaletteType,
            componentType: ComponentType.Text,
            styleType: StyleType.BackgroundColor,
            getColor: p => p.BackgroundColor
        );

        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData(PaletteType.Elevation1)]
    [InlineData(PaletteType.Elevation2)]
    [InlineData(PaletteType.Elevation3)]
    [InlineData(PaletteType.Elevation4)]
    [InlineData(PaletteType.Elevation5)]
    [InlineData(PaletteType.Error)]
    [InlineData(PaletteType.Info)]
    [InlineData(PaletteType.Primary)]
    [InlineData(PaletteType.Secondary)]
    [InlineData(PaletteType.Success)]
    [InlineData(PaletteType.Surface)]
    [InlineData(PaletteType.Tertiary)]
    [InlineData(PaletteType.Warning)]
    public void GetColors_Should_ReturnColors_ForEachPaletteType(PaletteType paletteType)
    {
        // Arrange
        var sut = new ThemeMapper();

        // Act
        var result = sut.GetColors(
            isHighContrast: false,
            isVariant: false,
            paletteType: paletteType,
            componentType: ComponentType.Text,
            styleType: StyleType.Color,
            getColor: p => p.ForegroundColor
        );

        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public void GetColors_Should_ReturnEmptyList_When_GetColorReturnsNull()
    {
        // Arrange
        var sut = new ThemeMapper();

        // Act
        var result = sut.GetColors(
            isHighContrast: false,
            isVariant: false,
            paletteType: PaletteType.Primary,
            componentType: ComponentType.Text,
            styleType: StyleType.BackgroundColor,
            getColor: _ => null
        );

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public void GetColors_Should_ReturnOneUpdaterPerThemeAndState_When_ColorsAreAvailable()
    {
        // Arrange
        var sut = new ThemeMapper();

        // Act
        var result = sut.GetColors(
            isHighContrast: false,
            isVariant: false,
            paletteType: PaletteType.Primary,
            componentType: ComponentType.Surface,
            styleType: StyleType.BackgroundColor,
            getColor: palette => palette.BackgroundColor
        );

        // Assert
        result.Should().NotBeNull();

        // 2 themes (dark + light) * 7 states
        result.Should().HaveCount(expected: 14);

        // All values should be StyleColor instances
        result.Select(selector: x => x.Value)
            .Should()
            .AllBeAssignableTo<StyleColor>();

        // Component types and style types should be set from the call
        result.Select(selector: x => x.Navigator.ComponentTypes.Single())
            .Distinct()
            .Should()
            .BeEquivalentTo(
                expectation: new[]
                {
                    ComponentType.Surface
                }
            );

        result.Select(selector: x => x.Navigator.StyleTypes.Single())
            .Distinct()
            .Should()
            .BeEquivalentTo(
                expectation: new[]
                {
                    StyleType.BackgroundColor
                }
            );

        // Theme types should include dark and light, each used once per state
        var byTheme = result.GroupBy(keySelector: x => x.Navigator.ThemeTypes.Single()).ToDictionary(
            keySelector: g => g.Key, elementSelector: g => g.ToList()
        );

        byTheme.Keys.Should().BeEquivalentTo(
            expectation: new[]
            {
                ThemeType.Dark,
                ThemeType.Light
            }
        );

        byTheme[key: ThemeType.Dark].Should().HaveCount(expected: 7);
        byTheme[key: ThemeType.Light].Should().HaveCount(expected: 7);

        // Each state should be produced for both themes
        var expectedStates = new[]
        {
            ComponentState.Default,
            ComponentState.Disabled,
            ComponentState.Dragged,
            ComponentState.Focused,
            ComponentState.Hovered,
            ComponentState.Pressed,
            ComponentState.Visited
        };

        foreach (var state in expectedStates)
        {
            result.Count(predicate: x => x.Navigator.ComponentStates.Single() == state)
                .Should()
                .Be(expected: 2);
        }
    }

    [Fact]
    public void GetColors_Should_UseHighContrastBrandAndThemeTypes_When_IsHighContrastIsTrue()
    {
        // Arrange
        var sut = new ThemeMapper();

        // Act
        var result = sut.GetColors(
            isHighContrast: true,
            isVariant: false,
            paletteType: PaletteType.Surface,
            componentType: ComponentType.GlobalBody,
            styleType: StyleType.BackgroundColor,
            getColor: p => p.BackgroundColor
        );

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(expected: 14);

        result.Select(selector: x => x.Navigator.ComponentTypes.Single())
            .Distinct()
            .Should()
            .BeEquivalentTo(
                expectation: new[]
                {
                    ComponentType.GlobalBody
                }
            );

        result.Select(selector: x => x.Navigator.StyleTypes.Single())
            .Distinct()
            .Should()
            .BeEquivalentTo(
                expectation: new[]
                {
                    StyleType.BackgroundColor
                }
            );

        var themeTypes = result
            .Select(selector: x => x.Navigator.ThemeTypes.Single())
            .Distinct()
            .ToArray();

        themeTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                ThemeType.HighContrastDark,
                ThemeType.HighContrastLight
            }
        );
    }

    [Fact]
    public void GetColors_Should_UseVariantHighContrastThemes_When_IsVariantAndHighContrastAreTrue()
    {
        // Arrange
        var sut = new ThemeMapper();

        // Act
        var result = sut.GetColors(
            isHighContrast: true,
            isVariant: true,
            paletteType: PaletteType.Surface,
            componentType: ComponentType.Surface,
            styleType: StyleType.BackgroundColor,
            getColor: p => p.BackgroundColor
        );

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(expected: 14);

        var themeTypes = result
            .Select(selector: x => x.Navigator.ThemeTypes.Single())
            .Distinct()
            .ToArray();

        themeTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                ThemeType.HighContrastDark,
                ThemeType.HighContrastLight
            }
        );
    }

    [Fact]
    public void GetColors_Should_UseVariantThemes_When_IsVariantIsTrue()
    {
        // Arrange
        var sut = new ThemeMapper();

        // Act
        var result = sut.GetColors(
            isHighContrast: false,
            isVariant: true,
            paletteType: PaletteType.Surface,
            componentType: ComponentType.Surface,
            styleType: StyleType.BackgroundColor,
            getColor: p => p.BackgroundColor
        );

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(expected: 14);

        var themeTypes = result
            .Select(selector: x => x.Navigator.ThemeTypes.Single())
            .Distinct()
            .ToArray();

        themeTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                ThemeType.Dark,
                ThemeType.Light
            }
        );
    }

    [Fact]
    public void GetFont_Should_SetHighContrastThemeTypes_When_IsHighContrastIsTrue()
    {
        // Arrange
        var sut = new ThemeMapper();

        // Act
        var result = sut.GetFont(
            isHighContrast: true,
            componentType: ComponentType.Text,
            fontType: FontFaceType.SansSerif
        );

        // Assert
        var navigator = result.Navigator;

        navigator.ComponentTypes.Should().ContainSingle()
            .Which.Should().Be(expected: ComponentType.Text);

        navigator.StyleTypes.Should().ContainSingle()
            .Which.Should().Be(expected: StyleType.FontFamily);

        var themeTypes = navigator.ThemeTypes.ToArray();

        themeTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                ThemeType.HighContrastLight,
                ThemeType.HighContrastDark
            }
        );

        var expectedStates = Enum.GetValues<ComponentState>()
            .Except(
                second: new[]
                {
                    ComponentState.Hidden,
                    ComponentState.ReadOnly
                }
            )
            .ToArray();

        navigator.ComponentStates.Should().BeEquivalentTo(expectation: expectedStates);
    }

    [Fact]
    public void GetFont_Should_SetLightAndDarkThemeTypes_When_IsHighContrastIsFalse()
    {
        // Arrange
        var sut = new ThemeMapper();

        // Act
        var result = sut.GetFont(
            isHighContrast: false,
            componentType: ComponentType.Text,
            fontType: FontFaceType.SansSerif
        );

        // Assert
        var navigator = result.Navigator;

        navigator.ComponentTypes.Should().ContainSingle()
            .Which.Should().Be(expected: ComponentType.Text);

        navigator.StyleTypes.Should().ContainSingle()
            .Which.Should().Be(expected: StyleType.FontFamily);

        var themeTypes = navigator.ThemeTypes.ToArray();

        themeTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                ThemeType.Light,
                ThemeType.Dark
            }
        );

        var expectedStates = Enum.GetValues<ComponentState>()
            .Except(
                second: new[]
                {
                    ComponentState.Hidden,
                    ComponentState.ReadOnly
                }
            )
            .ToArray();

        navigator.ComponentStates.Should().BeEquivalentTo(expectation: expectedStates);
    }

    [Theory]
    [InlineData(FontFaceType.Monospace, "MyMono")]
    [InlineData(FontFaceType.SansSerif, "MySans")]
    [InlineData(FontFaceType.Serif, "MySerif")]
    [InlineData((FontFaceType)999, "MySans")] // default branch
    public void GetFont_Should_UseCorrectFont_When_FontFaceTypeIsProvided(FontFaceType fontFaceType,
        string expectedFont)
    {
        // Arrange
        var brandFont = new BrandFont(
            sansSerif: "MySans",
            serif: "MySerif",
            monospace: "MyMono"
        );

        var brand = new Brand(font: brandFont);
        var sut = new ThemeMapper(brand: brand);

        // Act
        var result = sut.GetFont(
            isHighContrast: false,
            componentType: ComponentType.Text,
            fontType: fontFaceType
        );

        // Assert
        result.Value.Should().BeOfType<StyleString>();

        var styleString = (StyleString)result.Value;
        styleString.Value.Should().Be(expected: expectedFont);
    }
}
