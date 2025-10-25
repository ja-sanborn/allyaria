using Allyaria.Theming.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Allyaria.Theming.Blazor.Extensions;

/// <summary>
/// Provides extension methods for registering Allyaria theming services within a Blazor application's dependency injection
/// container.
/// </summary>
/// <remarks>
/// This class ensures that all theming-related services are properly configured with their respective lifetimes and are
/// ready for use by Blazor components and other application services.
/// </remarks>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers Allyaria theming services required for Blazor components, including theme provider, persistence, and watcher
    /// implementations.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to which the theming services will be added.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, allowing for method chaining.</returns>
    /// <remarks>
    /// - <see cref="IThemePersistenceOld" /> is registered as scoped, ensuring one instance per Blazor circuit.<br /> -
    /// <see cref="IThemeProviderOld" /> is registered as scoped for consistent state management.<br /> -
    /// <see cref="IThemeWatcherOld" /> is registered as a singleton to coordinate theme detection across components.
    /// </remarks>
    public static IServiceCollection AddAllyariaThemingBlazor(this IServiceCollection services)
    {
        services.AddScoped<IThemingService, ThemingService>();

        return services;
    }
}
