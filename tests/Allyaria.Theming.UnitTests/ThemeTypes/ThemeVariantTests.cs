namespace Allyaria.Theming.UnitTests.ThemeTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeVariantTests
{
    [Fact]
    public void BuildCss_Should_BuildCssForAllChildren_When_NavigatorHasNoThemeTypes()
    {
        // Arrange
        var lightNavigator = ThemeNavigator.Initialize
            .SetThemeType(themeType: ThemeType.Light)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var darkNavigator = ThemeNavigator.Initialize
            .SetThemeType(themeType: ThemeType.Dark)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var sut = new ThemeVariant();

        sut.Set(updater: new ThemeUpdater(Navigator: lightNavigator, Value: new StyleString(value: "red")));
        sut.Set(updater: new ThemeUpdater(Navigator: darkNavigator, Value: new StyleString(value: "red")));

        // navigator for building CSS has no ThemeTypes, so all children should be included
        var buildNavigator = ThemeNavigator.Initialize
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var builder = new CssBuilder();

        // Act
        var result = sut.BuildCss(builder: builder, navigator: buildNavigator, varPrefix: "theme");
        var css = result.ToString();

        // Assert
        css.Should().Contain(expected: "--theme-light-default-background-color:red");
        css.Should().Contain(expected: "--theme-dark-default-background-color:red");
    }

    [Fact]
    public void BuildCss_Should_IncludeOnlyRequestedThemeTypes_When_NavigatorHasThemeTypes()
    {
        // Arrange
        var updateNavigator = ThemeNavigator.Initialize
            .SetThemeType(themeType: ThemeType.Light)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var sut = new ThemeVariant();

        sut.Set(updater: new ThemeUpdater(Navigator: updateNavigator, Value: new StyleString(value: "red")));

        // Build navigator explicitly targets Light and Dark, but only Light exists in _children
        var buildNavigator = new ThemeNavigator(
            ComponentTypes: Array.Empty<ComponentType>(),
            ThemeTypes: new[]
            {
                ThemeType.Light,
                ThemeType.Dark
            },
            ComponentStates: new[]
            {
                ComponentState.Default
            },
            StyleTypes: new[]
            {
                StyleType.BackgroundColor
            }
        );

        var builder = new CssBuilder();

        // Act
        var result = sut.BuildCss(builder: builder, navigator: buildNavigator, varPrefix: "theme");
        var css = result.ToString();

        // Assert
        css.Should().Contain(expected: "--theme-light-default-background-color:red");
        css.Should().NotContain(unexpected: "--theme-dark-default-background-color");
    }

    [Fact]
    public void BuildCss_Should_ReturnEmptyCss_When_NoVariantChildrenExist()
    {
        // Arrange
        var sut = new ThemeVariant();

        var navigator = new ThemeNavigator(
            ComponentTypes: Array.Empty<ComponentType>(),
            ThemeTypes: new[]
            {
                ThemeType.Light
            },
            ComponentStates: new[]
            {
                ComponentState.Default
            },
            StyleTypes: new[]
            {
                StyleType.BackgroundColor
            }
        );

        var builder = new CssBuilder();

        // Act
        var result = sut.BuildCss(builder: builder, navigator: navigator, varPrefix: null);
        var css = result.ToString();

        // Assert
        css.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void BuildCss_Should_UsePlainPropertyNames_When_VarPrefixIsNull()
    {
        // Arrange
        var updateNavigator = ThemeNavigator.Initialize
            .SetThemeType(themeType: ThemeType.Light)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var sut = new ThemeVariant();

        sut.Set(updater: new ThemeUpdater(Navigator: updateNavigator, Value: new StyleString(value: "red")));

        var buildNavigator = ThemeNavigator.Initialize
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var builder = new CssBuilder();

        // Act
        var result = sut.BuildCss(builder: builder, navigator: buildNavigator, varPrefix: null);
        var css = result.ToString();

        // Assert
        css.Should().Contain(expected: "background-color:red");
        css.Should().NotContain(unexpected: "--");
    }

    [Fact]
    public void Set_Should_DoNothing_When_NavigatorHasNoThemeTypes()
    {
        // Arrange
        var navigatorWithoutThemes = ThemeNavigator.Initialize
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var sut = new ThemeVariant();

        // Act
        sut.Set(updater: new ThemeUpdater(Navigator: navigatorWithoutThemes, Value: new StyleString(value: "red")));

        // Build with any navigator; _children should still be empty
        var buildNavigator = new ThemeNavigator(
            ComponentTypes: Array.Empty<ComponentType>(),
            ThemeTypes: new[]
            {
                ThemeType.Light
            },
            ComponentStates: new[]
            {
                ComponentState.Default
            },
            StyleTypes: new[]
            {
                StyleType.BackgroundColor
            }
        );

        var builder = new CssBuilder();
        var result = sut.BuildCss(builder: builder, navigator: buildNavigator, varPrefix: "theme");
        var css = result.ToString();

        // Assert
        css.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void Set_Should_UpdateExistingThemeState_When_ThemeTypeIsAppliedMultipleTimes()
    {
        // Arrange
        var navigator = ThemeNavigator.Initialize
            .SetThemeType(themeType: ThemeType.Light)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var sut = new ThemeVariant();

        // First update
        sut.Set(updater: new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "red")));

        // Second update for the same ThemeType should reuse existing ThemeState
        sut.Set(updater: new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "blue")));

        var buildNavigator = ThemeNavigator.Initialize
            .SetThemeType(themeType: ThemeType.Light)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var builder = new CssBuilder();

        // Act
        var result = sut.BuildCss(builder: builder, navigator: buildNavigator, varPrefix: "theme");
        var css = result.ToString();

        // Assert
        css.Should().Contain(expected: "--theme-light-default-background-color:blue");
        css.Should().NotContain(unexpected: "--theme-light-default-background-color:red");
    }
}
