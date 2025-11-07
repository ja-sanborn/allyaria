namespace Allyaria.Theming.Helpers;

/// <summary>
/// Provides a configurable implementation of <see cref="IThemeConfigurator" /> that allows consumers to override specific
/// theming values through a collection of <see cref="ThemeUpdater" /> entries.
/// </summary>
/// <remarks>
///     <para>
///     The <see cref="ThemeConfigurator" /> is used during application startup to register or customize the Allyaria theme
///     by chaining calls to <see cref="Override(ThemeUpdater)" />.
///     </para>
///     <para>
///     It enforces immutability rules by preventing modification of system or high-contrast themes, ensuring consistent
///     accessibility and visual integrity.
///     </para>
/// </remarks>
public sealed class ThemeConfigurator : IThemeConfigurator
{
    /// <summary>Internal list of <see cref="ThemeUpdater" /> instances representing theme overrides.</summary>
    private readonly List<ThemeUpdater> _internalList = new();

    /// <summary>Gets the total number of configured <see cref="ThemeUpdater" /> entries.</summary>
    public int Count => _internalList.Count;

    /// <summary>Gets the <see cref="ThemeUpdater" /> at the specified index.</summary>
    /// <param name="index">The zero-based index of the updater to retrieve.</param>
    /// <returns>The <see cref="ThemeUpdater" /> at the specified index.</returns>
    public ThemeUpdater this[int index] => _internalList[index: index];

    /// <summary>Returns an enumerator that iterates through the collection of theme updaters.</summary>
    /// <returns>An enumerator for the <see cref="ThemeUpdater" /> collection.</returns>
    public IEnumerator<ThemeUpdater> GetEnumerator() => _internalList.GetEnumerator();

    /// <summary>Returns a non-generic enumerator that iterates through the collection of theme updaters.</summary>
    /// <returns>An enumerator for the <see cref="ThemeUpdater" /> collection.</returns>
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_internalList).GetEnumerator();

    /// <summary>Adds or replaces a <see cref="ThemeUpdater" /> in the configuration sequence.</summary>
    /// <param name="updater">The <see cref="ThemeUpdater" /> that defines a theming update or override to apply.</param>
    /// <returns>The current <see cref="IThemeConfigurator" /> instance, allowing for fluent chaining.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when an invalid override is attempted, such as modifying system or high-contrast themes, or restricted component
    /// states.
    /// </exception>
    public IThemeConfigurator Override(ThemeUpdater updater)
    {
        if (updater.Navigator.ThemeTypes.Contains(value: ThemeType.System))
        {
            throw new AryArgumentException(
                message: "System theme cannot be set directly.", argName: nameof(updater.Value)
            );
        }

        if (updater.Navigator.ComponentStates.Contains(value: ComponentState.Hidden) ||
            updater.Navigator.ComponentStates.Contains(value: ComponentState.ReadOnly))
        {
            throw new AryArgumentException(
                message: "Hidden and read-only states cannot be set directly.", argName: nameof(updater.Value)
            );
        }

        if (updater.Navigator.ThemeTypes.Contains(value: ThemeType.HighContrastDark) ||
            updater.Navigator.ThemeTypes.Contains(value: ThemeType.HighContrastLight))
        {
            throw new AryArgumentException(
                message: "Cannot alter High Contrast themes.", argName: nameof(updater.Value)
            );
        }

        if (updater.Navigator.ComponentStates.Contains(value: ComponentState.Focused) &&
            (updater.Navigator.StyleTypes.Contains(value: StyleType.OutlineOffset) ||
                updater.Navigator.StyleTypes.Contains(value: StyleType.OutlineStyle) ||
                updater.Navigator.StyleTypes.Contains(value: StyleType.OutlineWidth)))
        {
            throw new AryArgumentException(
                message: "Cannot change focused outline offset, style or width.", argName: nameof(updater.Value)
            );
        }

        _internalList.Add(item: updater);

        return this;
    }
}
