namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeNavigatorTests
{
    [Fact]
    public void Initialize_Should_CreateEmptyNavigator_When_Accessed()
    {
        // Arrange & Act
        var sut = ThemeNavigator.Initialize;

        // Assert
        sut.ComponentTypes.Should().NotBeNull();
        sut.ComponentTypes.Should().BeEmpty();

        sut.ThemeTypes.Should().NotBeNull();
        sut.ThemeTypes.Should().BeEmpty();

        sut.ComponentStates.Should().NotBeNull();
        sut.ComponentStates.Should().BeEmpty();

        sut.StyleTypes.Should().NotBeNull();
        sut.StyleTypes.Should().BeEmpty();
    }

    [Fact]
    public void SetAllComponentStates_Should_SetAllInteractiveStates_When_Called()
    {
        // Arrange
        var sut = ThemeNavigator.Initialize;

        // Act
        var result = sut.SetAllComponentStates();

        // Assert
        var allStates = Enum.GetValues<ComponentState>().ToList();

        var nonInteractiveStates = new[]
        {
            ComponentState.Hidden,
            ComponentState.ReadOnly
        };

        var expectedStates = allStates.Except(second: nonInteractiveStates);

        result.ComponentStates.Should().BeEquivalentTo(
            expectation: expectedStates, config: options => options.WithoutStrictOrdering()
        );

        result.ComponentStates.Should().NotContain(unexpected: ComponentState.Hidden);
        result.ComponentStates.Should().NotContain(unexpected: ComponentState.ReadOnly);
    }

    [Fact]
    public void SetAllComponentTypes_Should_SetAllComponentTypes_When_Called()
    {
        // Arrange
        var sut = ThemeNavigator.Initialize;

        // Act
        var result = sut.SetAllComponentTypes();

        // Assert
        var expectedTypes = Enum.GetValues<ComponentType>();

        result.ComponentTypes.Should().BeEquivalentTo(
            expectation: expectedTypes, config: options => options.WithoutStrictOrdering()
        );
    }

    [Fact]
    public void SetAllStyleTypes_Should_SetAllStyleTypes_When_Called()
    {
        // Arrange
        var sut = ThemeNavigator.Initialize;

        // Act
        var result = sut.SetAllStyleTypes();

        // Assert
        var expectedStyles = Enum.GetValues<StyleType>();

        result.StyleTypes.Should().BeEquivalentTo(
            expectation: expectedStyles, config: options => options.WithoutStrictOrdering()
        );
    }

    [Fact]
    public void SetComponentStates_Should_AllowEmptyArray_When_NoValuesProvided()
    {
        // Arrange
        var sut = ThemeNavigator.Initialize
            .SetComponentStates(ComponentState.Pressed); // ensure replacement happens

        // Act
        var result = sut.SetComponentStates();

        // Assert
        result.ComponentStates.Should().BeEmpty();
    }

    [Fact]
    public void SetComponentStates_Should_SetSpecifiedStates_When_ValuesProvided()
    {
        // Arrange
        var sut = ThemeNavigator.Initialize;

        var expectedStates = new[]
        {
            ComponentState.Default,
            ComponentState.Hovered
        };

        // Act
        var result = sut.SetComponentStates(items: expectedStates);

        // Assert
        result.ComponentStates.Should().BeEquivalentTo(expectation: expectedStates);
        sut.ComponentStates.Should().BeEmpty(); // original instance unchanged
    }

    [Fact]
    public void SetComponentTypes_Should_SetSpecifiedTypes_When_ValuesProvided()
    {
        // Arrange
        var sut = ThemeNavigator.Initialize;

        var expectedTypes = new[]
        {
            ComponentType.GlobalBody,
            ComponentType.Text
        };

        // Act
        var result = sut.SetComponentTypes(items: expectedTypes);

        // Assert
        result.ComponentTypes.Should().BeEquivalentTo(expectation: expectedTypes);
        sut.ComponentTypes.Should().BeEmpty(); // original instance unchanged
    }

    [Fact]
    public void SetContrastThemeTypes_Should_SetHighContrastThemes_When_IsHighContrastTrue()
    {
        // Arrange
        var sut = ThemeNavigator.Initialize;

        // Act
        var result = sut.SetContrastThemeTypes(isHighContrast: true);

        // Assert
        result.ThemeTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                ThemeType.HighContrastLight,
                ThemeType.HighContrastDark
            },
            config: options => options.WithStrictOrdering()
        );
    }

    [Fact]
    public void SetContrastThemeTypes_Should_SetStandardThemes_When_IsHighContrastFalse()
    {
        // Arrange
        var sut = ThemeNavigator.Initialize;

        // Act
        var result = sut.SetContrastThemeTypes(isHighContrast: false);

        // Assert
        result.ThemeTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                ThemeType.Light,
                ThemeType.Dark
            },
            config: options => options.WithStrictOrdering()
        );
    }

    [Fact]
    public void SetStyleTypes_Should_SetSpecifiedStyles_When_ValuesProvided()
    {
        // Arrange
        var sut = ThemeNavigator.Initialize;

        var expectedStyles = new[]
        {
            StyleType.BackgroundColor,
            StyleType.Color
        };

        // Act
        var result = sut.SetStyleTypes(items: expectedStyles);

        // Assert
        result.StyleTypes.Should().BeEquivalentTo(expectation: expectedStyles);
        sut.StyleTypes.Should().BeEmpty(); // original instance unchanged
    }

    [Theory]
    [InlineData(ThemeType.Light)]
    [InlineData(ThemeType.Dark)]
    public void SetThemeType_Should_SetSingleThemeType_When_ThemeTypeIsValid(ThemeType themeType)
    {
        // Arrange
        var sut = ThemeNavigator.Initialize;

        // Act
        var result = sut.SetThemeType(themeType: themeType);

        // Assert
        result.ThemeTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                themeType
            }
        );
    }

    [Theory]
    [InlineData(ThemeType.System)]
    [InlineData(ThemeType.HighContrastLight)]
    [InlineData(ThemeType.HighContrastDark)]
    public void SetThemeType_Should_ThrowAryArgumentException_When_ThemeTypeIsInvalid(ThemeType invalidThemeType)
    {
        // Arrange
        var sut = ThemeNavigator.Initialize;

        // Act
        var act = () => sut.SetThemeType(themeType: invalidThemeType);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Where(
                exceptionExpression: ex => ex.Message.Contains("Invalid theme type", StringComparison.OrdinalIgnoreCase)
            );
    }

    [Fact]
    public void SetThemeTypes_Should_ReplaceExistingThemeTypes_When_CalledMultipleTimes()
    {
        // Arrange
        var sut = ThemeNavigator.Initialize.SetThemeTypes(ThemeType.Light);

        // Act
        var result = sut.SetThemeTypes(ThemeType.Dark, ThemeType.HighContrastDark);

        // Assert
        result.ThemeTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                ThemeType.Dark,
                ThemeType.HighContrastDark
            },
            config: options => options.WithStrictOrdering()
        );

        // also ensures previous instance remains unchanged
        sut.ThemeTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                ThemeType.Light
            }
        );
    }
}
