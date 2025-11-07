using Microsoft.Extensions.DependencyInjection;

namespace Allyaria.Theming.Extensions;

/// <summary>
/// Provides extension methods for registering Allyaria theming services within a dependency injection (DI) container.
/// </summary>
/// <remarks>
/// This class enables consumers to configure and register the Allyaria theming system by supplying an optional
/// <see cref="Brand" />, an initial <see cref="ThemeType" />, and optional <see cref="IThemeConfigurator" /> overrides
/// during service setup.
/// </remarks>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds and configures the Allyaria theming service for the application’s dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to which the theming service will be added.</param>
    /// <param name="brand">
    /// An optional <see cref="Brand" /> instance defining the font and theme variants for the application. If null, a new
    /// default <see cref="Brand" /> will be created.
    /// </param>
    /// <param name="initialThemeType">
    /// The initial <see cref="ThemeType" /> to apply (e.g., Light, Dark, System, or HighContrast). Defaults to
    /// <see cref="ThemeType.System" />.
    /// </param>
    /// <param name="overrides">
    /// An optional <see cref="IThemeConfigurator" /> providing additional or replacement <see cref="ThemeUpdater" /> instances
    /// for fine-grained theme customization.
    /// </param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, allowing for method chaining.</returns>
    public static IServiceCollection AddAllyariaTheming(this IServiceCollection services,
        Brand? brand = null,
        ThemeType initialThemeType = ThemeType.System,
        IThemeConfigurator? overrides = null)
    {
        var builder = new ThemeBuilder().Create(brand: brand ?? new Brand());

        if (overrides is not null)
        {
            foreach (var updater in overrides)
            {
                builder.Set(updater: updater);
            }
        }

        var theme = builder.Build();
        var service = new ThemingService(theme: theme, themeType: initialThemeType);
        services.AddSingleton<IThemingService>(implementationInstance: service);

        return services;
    }
}
