namespace Allyaria.Theming.UnitTests.ThemeTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeTests
{
    private static ComponentState GetAnyComponentState() => Enum.GetValues<ComponentState>()[0];

    private static ComponentType GetAnyComponentType() => Enum.GetValues<ComponentType>()[0];

    [Fact]
    public void GetComponentCss_Should_ReturnRawCss_When_PrefixIsNullOrWhitespace()
    {
        // Arrange
        var componentType = GetAnyComponentType();
        var themeType = GetValidThemeType();
        var componentState = GetAnyComponentState();

        var navigator = ThemeNavigator.Initialize
            .SetComponentTypes(componentType)
            .SetThemeType(themeType: themeType)
            .SetComponentStates(componentState)
            .SetStyleTypes(StyleType.Color);

        var updater = new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "red"));

        var sut = new Theme();
        sut.Set(updater: updater);

        // Act
        var cssWithNullPrefix = sut.GetComponentCss(
            prefix: null!,
            componentType: componentType,
            themeType: themeType,
            componentState: componentState
        );

        var cssWithWhitespacePrefix = sut.GetComponentCss(
            prefix: "   ",
            componentType: componentType,
            themeType: themeType,
            componentState: componentState
        );

        // Assert
        cssWithNullPrefix.Should().Contain(expected: "color:red");
        cssWithWhitespacePrefix.Should().Be(expected: cssWithNullPrefix);
    }

    [Fact]
    public void GetComponentCss_Should_TrimPrefixAndWrapCss_When_PrefixIsProvided()
    {
        // Arrange
        var componentType = GetAnyComponentType();
        var themeType = GetValidThemeType();
        var componentState = GetAnyComponentState();

        var navigator = ThemeNavigator.Initialize
            .SetComponentTypes(componentType)
            .SetThemeType(themeType: themeType)
            .SetComponentStates(componentState)
            .SetStyleTypes(StyleType.Color);

        var updater = new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "red"));

        var sut = new Theme();
        sut.Set(updater: updater);

        // Act
        var css = sut.GetComponentCss(
            prefix: "  .my-component ",
            componentType: componentType,
            themeType: themeType,
            componentState: componentState
        );

        // Assert
        css.Should().StartWith(expected: ".my-component{");
        css.Should().Contain(expected: "color:red");
        css.Should().EndWith(expected: "}");
    }

    [Fact]
    public void GetDocumentCss_Should_ComposeRootGlobalAndAccessibilityCss_When_ThemeTypeIsProvided()
    {
        // Arrange
        var themeType = GetValidThemeType();
        var sut = new Theme();

        // Act
        var css = sut.GetDocumentCss(themeType: themeType);

        // Assert
        css.Should().Contain(expected: ":root{");
        css.Should().Contain(expected: "html{");
        css.Should().Contain(expected: "body{");

        css.Should().Contain(expected: ":focus-visible{");
        css.Should().Contain(expected: ":where(a,button,input,textarea,select,[tabindex]):focus-visible{");

        css.Should().Contain(expected: "a{");
        css.Should().Contain(expected: "a:focus-visible{");
        css.Should().Contain(expected: "a:active{");
        css.Should().Contain(expected: "a:visited{");

        css.Should().Contain(expected: "p{");
        css.Should().Contain(expected: "h1{");
        css.Should().Contain(expected: "h2{");
        css.Should().Contain(expected: "h3{");
        css.Should().Contain(expected: "h4{");
        css.Should().Contain(expected: "h5{");
        css.Should().Contain(expected: "h6{");

        css.Should().Contain(expected: "box-sizing:inherit;");
        css.Should().Contain(expected: "@media(prefers-reduced-motion:reduce)");
    }

    [Fact]
    public void GetDocumentCss_Should_IncludeComponentCss_When_ComponentStylesAreConfigured()
    {
        // Arrange
        var themeType = GetValidThemeType();

        var navigator = ThemeNavigator.Initialize
            .SetComponentTypes(ComponentType.Link)
            .SetThemeType(themeType: themeType)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.Color);

        var updater = new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "red"));

        var sut = new Theme();
        sut.Set(updater: updater);

        // Act
        var css = sut.GetDocumentCss(themeType: themeType);

        // Assert
        css.Should().Contain(expected: "a{");
        css.Should().Contain(expected: "color:red");
    }

    private static ThemeType GetValidThemeType()
    {
        foreach (var value in Enum.GetValues<ThemeType>())
        {
            if (value is not ThemeType.System
                and not ThemeType.HighContrastDark
                and not ThemeType.HighContrastLight)
            {
                return value;
            }
        }

        // Fallback – in a valid configuration this should never be hit.
        return Enum.GetValues<ThemeType>()[0];
    }

    [Fact]
    public void Set_Should_UpdateComponentCss_When_ValidUpdaterIsApplied()
    {
        // Arrange
        var componentType = GetAnyComponentType();
        var themeType = GetValidThemeType();
        var componentState = GetAnyComponentState();

        var navigator = ThemeNavigator.Initialize
            .SetComponentTypes(componentType)
            .SetThemeType(themeType: themeType)
            .SetComponentStates(componentState)
            .SetStyleTypes(StyleType.Color);

        var updater = new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "red"));

        var sut = new Theme();

        // Act
        var returned = sut.Set(updater: updater);

        var css = sut.GetComponentCss(
            prefix: null!,
            componentType: componentType,
            themeType: themeType,
            componentState: componentState
        );

        // Assert
        returned.Should().BeSameAs(expected: sut);
        css.Should().Contain(expected: "color:red");
    }
}
