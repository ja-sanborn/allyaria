using Microsoft.Extensions.DependencyInjection;

namespace Allyaria.Theming.UnitTests.Extensions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddAllyariaTheming_Should_ApplyOverrides_When_ConfiguratorProvided()
    {
        // Arrange
        var services = new ServiceCollection();

        var configurator = new ThemeConfigurator();

        var navigator = ThemeNavigator.Initialize
            .SetComponentTypes(ComponentType.Link)
            .SetThemeType(themeType: ThemeType.Light)
            .SetComponentStates(ComponentState.Default)
            .SetStyleTypes(StyleType.Color);

        const string overriddenColor = "rgb(1,2,3)";

        var updater = new ThemeUpdater(
            Navigator: navigator,
            Value: new StyleString(value: overriddenColor)
        );

        configurator.Override(updater: updater);

        services.AddAllyariaTheming(
            brand: null,
            initialThemeType: ThemeType.Light,
            overrides: configurator
        );

        var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IThemingService>();

        // Act
        var css = service.GetComponentCss(
            prefix: "a",
            componentType: ComponentType.Link,
            componentState: ComponentState.Default
        );

        // Assert
        css.Should().Contain(expected: overriddenColor);
    }

    [Fact]
    public void AddAllyariaTheming_Should_RegisterScopedThemingService_AndReturnSameCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var returned = services.AddAllyariaTheming();

        // Assert
        returned.Should().BeSameAs(expected: services);

        services.Should()
            .ContainSingle(
                predicate: descriptor =>
                    descriptor.ServiceType == typeof(IThemingService) &&
                    descriptor.Lifetime == ServiceLifetime.Scoped
            );

        var provider = services.BuildServiceProvider();

        using var scope1 = provider.CreateScope();
        var instance1 = scope1.ServiceProvider.GetRequiredService<IThemingService>();
        var instance2 = scope1.ServiceProvider.GetRequiredService<IThemingService>();

        using var scope2 = provider.CreateScope();
        var instance3 = scope2.ServiceProvider.GetRequiredService<IThemingService>();

        instance1.Should().NotBeNull();
        instance2.Should().BeSameAs(expected: instance1); // scoped: same within scope
        instance3.Should().NotBeSameAs(unexpected: instance1); // scoped: different across scopes

        instance1.Should().BeOfType<ThemingService>();
        var concrete = (ThemingService)instance1;
        concrete.StoredType.Should().Be(expected: ThemeType.System);
        concrete.EffectiveType.Should().Be(expected: ThemeType.Light); // System → Light by default
    }

    [Fact]
    public void AddAllyariaTheming_Should_RespectInitialThemeType_When_NonSystemTypeProvided()
    {
        // Arrange
        var services = new ServiceCollection();
        var brand = new Brand();

        // Act
        services.AddAllyariaTheming(
            brand: brand,
            initialThemeType: ThemeType.Dark
        );

        var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IThemingService>();

        // Assert
        service.Should().BeOfType<ThemingService>();
        var concrete = (ThemingService)service;

        concrete.StoredType.Should().Be(expected: ThemeType.Dark);
        concrete.EffectiveType.Should().Be(expected: ThemeType.Dark);
    }
}
