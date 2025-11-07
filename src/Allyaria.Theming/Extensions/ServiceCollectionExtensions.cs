using Microsoft.Extensions.DependencyInjection;

namespace Allyaria.Theming.Extensions;

public static class ServiceCollectionExtensions
{
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
