# Allyaria.Theming.Extensions.ServiceCollectionExtensions

`ServiceCollectionExtensions` is a static class that provides dependency-injection (DI) extension methods for
registering the Allyaria theming system into an application. It integrates the full theming pipeline—including brand
initialization, theme building, configurator overrides, and `IThemingService` creation—into a single DI-friendly API.

## Summary

`ServiceCollectionExtensions` enables consumers to install Allyaria’s theming system with one method call:
`AddAllyariaTheming()`. It constructs a `ThemeBuilder`, optionally applies a custom `Brand` and `IThemeConfigurator`
overrides, builds the final `Theme`, and registers an `IThemingService` (`ThemingService`) into the DI container. The
extension method can be chained with other DI service registrations and is compatible with typical ASP.NET Core, MAUI,
Blazor, console-hosted apps, and any container using Microsoft's `IServiceCollection`.

## Constructors

*None*

## Properties

*None*

## Methods

| Name                                                                                                                                                             | Returns              | Description                                                                                                                                                                                                                                                                                            |
|------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `AddAllyariaTheming(this IServiceCollection services, Brand? brand = null, ThemeType initialThemeType = ThemeType.System, IThemeConfigurator? overrides = null)` | `IServiceCollection` | Registers the Allyaria theming engine into the DI container. Creates a `ThemeBuilder`, applies the optional `Brand` and optional theme overrides, builds the final theme, and registers an `IThemingService` instance created from it. Returns the same service collection to support method chaining. |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Microsoft.Extensions.DependencyInjection;
using Allyaria.Theming.Extensions;
using Allyaria.Theming;
using Allyaria.Theming.Contracts;
using Allyaria.Theming.Enumerations;

public class Program
{
    public static void Main()
    {
        var services = new ServiceCollection();

        // Register the Allyaria theming system
        services.AddAllyariaTheming(
            brand: new Brand(),
            initialThemeType: ThemeType.Light,
            overrides: null // or a custom IThemeConfigurator
        );

        var provider = services.BuildServiceProvider();

        // Resolve the theming service
        var theming = provider.GetRequiredService<IThemingService>();

        // Use it to generate CSS
        string css = theming.GetDocumentCss();
    }
}
```

---

*Revision Date: 2025-11-15*
