namespace Allyaria.Theming.Contracts;

/// <summary>
/// Defines a contract for configuring and extending theme behavior within the Allyaria theming system. Implementations
/// manage a sequence of <see cref="ThemeUpdater" /> instances, allowing controlled customization of theme properties
/// before finalization.
/// </summary>
/// <remarks>
/// This interface supports fluent configuration, enabling multiple chained <see cref="Override(ThemeUpdater)" /> calls to
/// progressively refine the theme prior to being built or applied.
/// </remarks>
public interface IThemeConfigurator : IReadOnlyList<ThemeUpdater>
{
    /// <summary>Adds or replaces a <see cref="ThemeUpdater" /> that modifies the theme configuration.</summary>
    /// <param name="updater">The <see cref="ThemeUpdater" /> delegate or object that defines how the theme should be updated.</param>
    /// <returns>
    /// A new <see cref="IThemeConfigurator" /> instance with the specified updater applied, enabling fluent chaining of
    /// multiple overrides.
    /// </returns>
    IThemeConfigurator Override(ThemeUpdater updater);
}
