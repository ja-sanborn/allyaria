using Allyaria.Theming.Blazor.Services;
using Allyaria.Theming.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Allyaria.Theming.Blazor.Extensions;

/// <summary>
/// Registers Allyaria Theming for Blazor. Call this once in your host at startup:
/// <code>builder.Services.AddAllyariaThemingBlazor();</code>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>Adds the Blazor-aware theme watcher and theme provider to DI.</summary>
    public static IServiceCollection AddAllyariaThemingBlazor(this IServiceCollection services)
    {
        services.AddScoped<IThemeProvider, ThemeProvider>();
        services.AddSingleton<IThemeWatcher, AryThemeWatcher>();

        return services;
    }
}
