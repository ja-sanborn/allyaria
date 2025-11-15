namespace Allyaria.Theming.UnitTests.ThemeTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeComponentTests
{
    [Fact]
    public void BuildCss_Should_EnumerateAllChildren_When_NavigatorHasNoComponentTypes()
    {
        // Arrange
        var sut = new ThemeComponent();
        var componentType1 = GetAnyComponentType();

        var navigatorForFirst = CreateNavigatorFor(componentType1);
        var updaterForFirst = new ThemeUpdater(Navigator: navigatorForFirst, Value: new StyleString(value: "red"));

        sut.Set(updater: updaterForFirst);

        var validThemeType = GetValidThemeType();
        var anyState = GetAnyComponentState();

        var navigatorAllComponents = ThemeNavigator.Initialize
            .SetThemeType(themeType: validThemeType)
            .SetComponentStates(anyState)
            .SetStyleTypes(StyleType.Color);

        var builder = new CssBuilder();

        // Act
        var css = sut.BuildCss(builder: builder, navigator: navigatorAllComponents, varPrefix: "root-prefix")
            .ToString();

        // Assert
        css.Should().NotBeNullOrWhiteSpace();
        css.Should().Contain(expected: "--root-prefix-");
    }

    [Fact]
    public void BuildCss_Should_IgnoreUnknownComponentTypes_When_NavigatorContainsComponentsWithoutChildren()
    {
        // Arrange
        var sut = new ThemeComponent();
        var knownComponentType = GetAnyComponentType();
        var navigatorKnown = CreateNavigatorFor(knownComponentType);
        var updater = new ThemeUpdater(Navigator: navigatorKnown, Value: new StyleString(value: "red"));

        sut.Set(updater: updater);

        var unknownComponentType = (ComponentType)int.MaxValue;
        var navigatorWithUnknown = CreateNavigatorFor(knownComponentType, unknownComponentType);

        // Act
        var cssKnown = sut.BuildCss(builder: new CssBuilder(), navigator: navigatorKnown, varPrefix: null).ToString();

        var cssWithUnknown = sut.BuildCss(builder: new CssBuilder(), navigator: navigatorWithUnknown, varPrefix: null)
            .ToString();

        // Assert
        cssWithUnknown.Should().Be(expected: cssKnown);
    }

    [Fact]
    public void BuildCss_Should_ReturnEmptyCss_When_NoChildrenAndNavigatorTargetsComponents()
    {
        // Arrange
        var sut = new ThemeComponent();
        var componentType = GetAnyComponentType();

        var navigator = ThemeNavigator.Initialize
            .SetComponentTypes(componentType);

        var builder = new CssBuilder();

        // Act
        var result = sut.BuildCss(builder: builder, navigator: navigator, varPrefix: "root-prefix");
        var css = result.ToString();

        // Assert
        css.Should().BeEmpty();
    }

    private static ThemeNavigator CreateNavigatorFor(params ComponentType[] componentTypes)
    {
        var validThemeType = GetValidThemeType();
        var anyState = GetAnyComponentState();

        return ThemeNavigator.Initialize
            .SetComponentTypes(items: componentTypes)
            .SetThemeType(themeType: validThemeType)
            .SetComponentStates(anyState)
            .SetStyleTypes(StyleType.Color);
    }

    private static ComponentState GetAnyComponentState() => Enum.GetValues<ComponentState>()[0];

    private static ComponentType GetAnyComponentType() => Enum.GetValues<ComponentType>()[0];

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
    public void Set_Should_CreateVariantAndCascadeValue_When_NewComponentType()
    {
        // Arrange
        var sut = new ThemeComponent();
        var componentType = GetAnyComponentType();
        var navigator = CreateNavigatorFor(componentType);
        var updater = new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "red"));

        // Act
        sut.Set(updater: updater);
        var css = sut.BuildCss(builder: new CssBuilder(), navigator: navigator, varPrefix: null).ToString();

        // Assert
        css.Should().Contain(expected: "color:red");
    }

    [Fact]
    public void Set_Should_ReuseExistingVariant_When_CalledMultipleTimesForSameComponentType()
    {
        // Arrange
        var sut = new ThemeComponent();
        var componentType = GetAnyComponentType();
        var navigator = CreateNavigatorFor(componentType);
        var firstUpdater = new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "red"));
        var secondUpdater = new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "blue"));

        // Act
        sut.Set(updater: firstUpdater);
        sut.Set(updater: secondUpdater);

        var css = sut.BuildCss(builder: new CssBuilder(), navigator: navigator, varPrefix: null).ToString();

        // Assert
        css.Should().Contain(expected: "color:blue");
    }
}
