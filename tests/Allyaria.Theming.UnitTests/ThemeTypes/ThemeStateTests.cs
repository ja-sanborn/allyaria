namespace Allyaria.Theming.UnitTests.ThemeTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeStateTests
{
    [Fact]
    public void BuildCss_Should_IgnoreMissingStates_When_NavigatorRequestsStateWithoutStyle()
    {
        // Arrange
        var setupNavigator = ThemeNavigator
            .Initialize
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var updater = new ThemeUpdater(Navigator: setupNavigator, Value: new StyleString(value: "red"));
        var sut = new ThemeState();
        sut.Set(updater: updater);

        var builderForExisting = new CssBuilder();

        var navigatorForExisting = ThemeNavigator
            .Initialize
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var expectedCss = sut.BuildCss(
            builder: builderForExisting, navigator: navigatorForExisting, varPrefix: "theme-root"
        ).ToString();

        var builderForMissing = new CssBuilder();

        var navigatorForMissing = ThemeNavigator
            .Initialize
            .SetComponentStates(ComponentState.Default, ComponentState.Focused) // Focused not configured
            .SetStyleTypes(StyleType.BackgroundColor);

        // Act
        var actualCss = sut.BuildCss(
            builder: builderForMissing, navigator: navigatorForMissing, varPrefix: "theme-root"
        ).ToString();

        // Assert
        actualCss.Should().Be(expected: expectedCss);
        actualCss.Should().NotContain(unexpected: "--theme-root-focused-background-color");
    }

    [Fact]
    public void BuildCss_Should_RenderAllChildStyles_When_NavigatorHasNoComponentStates_AndVarPrefixIsNull()
    {
        // Arrange
        var stateNavigator = ThemeNavigator
            .Initialize
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var updater = new ThemeUpdater(Navigator: stateNavigator, Value: new StyleString(value: "red"));
        var sut = new ThemeState();
        sut.Set(updater: updater);

        var builder = new CssBuilder();

        var buildNavigator = ThemeNavigator
            .Initialize
            .SetStyleTypes(StyleType.BackgroundColor); // ComponentStates empty → use all children

        // Act
        var css = sut.BuildCss(builder: builder, navigator: buildNavigator, varPrefix: null).ToString();

        // Assert
        css.Should().Contain(expected: "background-color:red");
        css.Should().NotContain(unexpected: "--"); // no CSS variable prefix when varPrefix resolves to empty
    }

    [Fact]
    public void BuildCss_Should_RenderOnlyRequestedStates_When_NavigatorSpecifiesComponentStates()
    {
        // Arrange
        var setupNavigator = ThemeNavigator
            .Initialize
            .SetComponentStates(ComponentState.Default, ComponentState.Hovered)
            .SetStyleTypes(StyleType.BackgroundColor);

        var updater = new ThemeUpdater(Navigator: setupNavigator, Value: new StyleString(value: "red"));
        var sut = new ThemeState();
        sut.Set(updater: updater);

        var builder = new CssBuilder();

        var buildNavigator = ThemeNavigator
            .Initialize
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        // Act
        var css = sut.BuildCss(builder: builder, navigator: buildNavigator, varPrefix: "theme-root").ToString();

        // Assert
        css.Should().Contain(expected: "--theme-root-default-background-color:red");
        css.Should().NotContain(unexpected: "--theme-root-hovered-background-color");
    }

    [Fact]
    public void Set_Should_CreateThemeStylePerComponentState_When_MultipleComponentStatesProvided()
    {
        // Arrange
        var navigator = ThemeNavigator
            .Initialize
            .SetComponentStates(ComponentState.Default, ComponentState.Hovered)
            .SetStyleTypes(StyleType.BackgroundColor);

        var updater = new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "red"));
        var sut = new ThemeState();

        // Act
        sut.Set(updater: updater);

        var builder = new CssBuilder();

        var buildNavigator = ThemeNavigator
            .Initialize
            .SetStyleTypes(StyleType.BackgroundColor);

        var css = sut.BuildCss(builder: builder, navigator: buildNavigator, varPrefix: "theme-root").ToString();

        // Assert
        css.Should().Contain(expected: "--theme-root-default-background-color:red");
        css.Should().Contain(expected: "--theme-root-hovered-background-color:red");
    }

    [Fact]
    public void Set_Should_NotCreateChildren_When_UpdaterHasNoComponentStates()
    {
        // Arrange
        var navigator = ThemeNavigator
            .Initialize
            .SetStyleTypes(StyleType.BackgroundColor); // ComponentStates remains empty

        var updater = new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "red"));
        var sut = new ThemeState();

        // Act
        sut.Set(updater: updater);

        var css = sut.BuildCss(
                builder: new CssBuilder(), navigator: ThemeNavigator.Initialize.SetStyleTypes(StyleType.BackgroundColor)
            )
            .ToString();

        // Assert
        css.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void Set_Should_UpdateExistingThemeStyle_When_CalledMultipleTimesForSameState()
    {
        // Arrange
        var navigator = ThemeNavigator
            .Initialize
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var firstUpdater = new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "red"));
        var secondUpdater = new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "blue"));

        var sut = new ThemeState();
        sut.Set(updater: firstUpdater);

        // Act
        sut.Set(updater: secondUpdater);

        var css = sut.BuildCss(
                builder: new CssBuilder(),
                navigator: ThemeNavigator.Initialize.SetStyleTypes(StyleType.BackgroundColor)
            )
            .ToString();

        // Assert
        css.Should().Contain(expected: "background-color:blue");
        css.Should().NotContain(unexpected: "background-color:red");
    }
}
