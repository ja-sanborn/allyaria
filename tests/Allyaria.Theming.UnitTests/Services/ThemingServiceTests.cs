namespace Allyaria.Theming.UnitTests.Services;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemingServiceTests
{
    [Fact]
    public void Constructor_Should_SetEffectiveTypeToLight_When_InitialThemeTypeIsSystem()
    {
        // Arrange
        var theme = new Theme();

        // Act
        var sut = new ThemingService(theme: theme, themeType: ThemeType.System);

        // Assert
        sut.StoredType.Should().Be(expected: ThemeType.System);
        sut.EffectiveType.Should().Be(expected: ThemeType.Light);
    }

    [Fact]
    public void Constructor_Should_SetEffectiveTypeToStoredType_When_InitialThemeTypeIsNonSystem()
    {
        // Arrange
        var theme = new Theme();

        // Act
        var sut = new ThemingService(theme: theme, themeType: ThemeType.Dark);

        // Assert
        sut.StoredType.Should().Be(expected: ThemeType.Dark);
        sut.EffectiveType.Should().Be(expected: ThemeType.Dark);
    }

    [Fact]
    public void GetComponentCss_Should_UseEffectiveType_When_GeneratingCss()
    {
        // Arrange
        var theme = new Theme();

        var navigator = ThemeNavigator.Initialize
            .SetComponentTypes(items: ComponentType.Surface)
            .SetThemeType(themeType: ThemeType.Light)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var updater = new ThemeUpdater(
            Navigator: navigator,
            Value: new StyleString(value: "red")
        );

        theme.Set(updater: updater);

        // StoredType is System so EffectiveType falls back to Light
        var sut = new ThemingService(theme: theme, themeType: ThemeType.System);

        // Act
        var css = sut.GetComponentCss(
            prefix: string.Empty,
            componentType: ComponentType.Surface,
            componentState: ComponentState.Default
        );

        // Assert
        css.Should().NotBeNullOrWhiteSpace();
        css.Should().Contain(expected: "background-color:red");

        // If ThemingService were incorrectly using StoredType (System) instead of EffectiveType (Light),
        // no CSS would be generated for ThemeType.System because we never configured it.
    }

    [Fact]
    public void GetDocumentCss_Should_UseEffectiveType_When_GeneratingCss()
    {
        // Arrange
        var theme = new Theme();

        var navigatorLight = ThemeNavigator.Initialize
            .SetComponentTypes(ComponentType.Link)
            .SetThemeType(themeType: ThemeType.Light)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var updaterLight = new ThemeUpdater(
            Navigator: navigatorLight,
            Value: new StyleString(value: "red")
        );

        var navigatorDark = ThemeNavigator.Initialize
            .SetComponentTypes(ComponentType.Link)
            .SetThemeType(themeType: ThemeType.Dark)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.BackgroundColor);

        var updaterDark = new ThemeUpdater(
            Navigator: navigatorDark,
            Value: new StyleString(value: "blue")
        );

        theme
            .Set(updater: updaterLight)
            .Set(updater: updaterDark);

        var sut = new ThemingService(theme: theme, themeType: ThemeType.Light);

        // Act
        var lightCss = sut.GetDocumentCss();

        sut.SetEffectiveType(themeType: ThemeType.Dark);
        var darkCss = sut.GetDocumentCss();

        // Assert
        lightCss.Should().Contain(expected: "{background-color:red}");
        lightCss.Should().NotContain(unexpected: "{background-color:blue}");

        darkCss.Should().Contain(expected: "{background-color:blue}");
        darkCss.Should().NotContain(unexpected: "{background-color:red}");

        // This verifies that GetDocumentCss uses EffectiveType and that changing EffectiveType
        // changes the generated document-level CSS.
    }

    [Fact]
    public void SetEffectiveType_Should_NotChangeEffectiveTypeOrRaiseEvent_When_NewTypeIsSystemOrSame()
    {
        // Arrange
        var theme = new Theme();
        var sut = new ThemingService(theme: theme, themeType: ThemeType.Light);

        var eventCount = 0;
        sut.ThemeChanged += (_, _) => eventCount++;

        // Act
        sut.SetEffectiveType(themeType: ThemeType.System); // blocked because System
        sut.SetEffectiveType(themeType: ThemeType.Light); // blocked because same as current

        // Assert
        sut.EffectiveType.Should().Be(expected: ThemeType.Light);
        eventCount.Should().Be(expected: 0);
    }

    [Fact]
    public void SetEffectiveType_Should_UpdateEffectiveTypeAndRaiseEvent_When_NewTypeIsDifferentAndNotSystem()
    {
        // Arrange
        var theme = new Theme();
        var sut = new ThemingService(theme: theme, themeType: ThemeType.Light);

        var eventCount = 0;
        ThemeType? observedEffectiveType = null;

        sut.ThemeChanged += (sender, _) =>
        {
            eventCount++;
            observedEffectiveType = ((ThemingService)sender!).EffectiveType;
        };

        // Act
        sut.SetEffectiveType(themeType: ThemeType.Dark);

        // Assert
        sut.EffectiveType.Should().Be(expected: ThemeType.Dark);
        eventCount.Should().Be(expected: 1);
        observedEffectiveType.Should().Be(expected: ThemeType.Dark);
    }

    [Fact]
    public void SetStoredType_Should_NotChangeStoredTypeOrRaiseEvent_When_NewTypeEqualsCurrent()
    {
        // Arrange
        var theme = new Theme();
        var sut = new ThemingService(theme: theme, themeType: ThemeType.Dark);

        var eventCount = 0;
        sut.ThemeChanged += (_, _) => eventCount++;

        // Act
        sut.SetStoredType(themeType: ThemeType.Dark);

        // Assert
        sut.StoredType.Should().Be(expected: ThemeType.Dark);
        sut.EffectiveType.Should().Be(expected: ThemeType.Dark);
        eventCount.Should().Be(expected: 0);
    }

    [Fact]
    public void SetStoredType_Should_UpdateStoredAndEffectiveTypeAndRaiseEvent_When_NewTypeIsNonSystemAndDifferent()
    {
        // Arrange
        var theme = new Theme();
        var sut = new ThemingService(theme: theme, themeType: ThemeType.Light);

        var eventCount = 0;
        ThemeType? observedEffectiveType = null;

        sut.ThemeChanged += (sender, _) =>
        {
            eventCount++;
            observedEffectiveType = ((ThemingService)sender!).EffectiveType;
        };

        // Act
        sut.SetStoredType(themeType: ThemeType.Dark);

        // Assert
        sut.StoredType.Should().Be(expected: ThemeType.Dark);
        sut.EffectiveType.Should().Be(expected: ThemeType.Dark);
        eventCount.Should().Be(expected: 1);
        observedEffectiveType.Should().Be(expected: ThemeType.Dark);
    }

    [Fact]
    public void SetStoredType_Should_UpdateStoredTypeAndRaiseEventWithoutChangingEffectiveType_When_NewTypeIsSystem()
    {
        // Arrange
        var theme = new Theme();
        var sut = new ThemingService(theme: theme, themeType: ThemeType.Light);

        var eventCount = 0;
        ThemeType? observedEffectiveType = null;

        sut.ThemeChanged += (sender, _) =>
        {
            eventCount++;
            observedEffectiveType = ((ThemingService)sender!).EffectiveType;
        };

        // Act
        sut.SetStoredType(themeType: ThemeType.System);

        // Assert
        sut.StoredType.Should().Be(expected: ThemeType.System);
        sut.EffectiveType.Should().Be(expected: ThemeType.Light); // unchanged
        eventCount.Should().Be(expected: 1);
        observedEffectiveType.Should().Be(expected: ThemeType.Light);
    }
}
