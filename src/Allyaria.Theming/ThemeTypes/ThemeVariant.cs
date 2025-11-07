namespace Allyaria.Theming.ThemeTypes;

/// <summary>
/// Represents a collection of <see cref="ThemeState" /> objects grouped by <see cref="ThemeType" />, forming a
/// hierarchical layer in the Allyaria theming structure.
/// </summary>
/// <remarks>
///     <para>
///     Each <see cref="ThemeVariant" /> corresponds to a specific set of theme variants (e.g., Light, Dark,
///     HighContrastLight, HighContrastDark) under a particular <see cref="ComponentType" />. It recursively builds CSS
///     output and applies updates for its child <see cref="ThemeState" /> instances.
///     </para>
///     <para>
///     This class participates in the theming resolution chain alongside <see cref="ThemeComponent" />,
///     <see cref="ThemeState" />, and <see cref="ThemeUpdater" />.
///     </para>
/// </remarks>
internal sealed class ThemeVariant
{
    /// <summary>
    /// A dictionary mapping <see cref="ThemeType" /> values to their respective <see cref="ThemeState" /> definitions.
    /// </summary>
    private readonly Dictionary<ThemeType, ThemeState> _children = new();

    /// <summary>
    /// Builds CSS for this theme variant and its child <see cref="ThemeState" /> instances using the provided context.
    /// </summary>
    /// <param name="builder">The <see cref="CssBuilder" /> used to accumulate CSS declarations.</param>
    /// <param name="navigator">
    /// The <see cref="ThemeNavigator" /> describing the scope of components, themes, and states to include.
    /// </param>
    /// <param name="varPrefix">An optional variable prefix used when generating CSS variable declarations.</param>
    /// <returns>A <see cref="CssBuilder" /> instance containing the concatenated CSS output for this variant.</returns>
    internal CssBuilder BuildCss(CssBuilder builder, ThemeNavigator navigator, string? varPrefix = "")
    {
        if (navigator.ThemeTypes.Count is 0)
        {
            foreach (var child in _children)
            {
                builder = child.Value.BuildCss(
                    builder: builder,
                    navigator: navigator,
                    varPrefix: SetPrefix(varPrefix: varPrefix, type: child.Key)
                );
            }
        }
        else
        {
            foreach (var key in navigator.ThemeTypes)
            {
                builder = Get(key: key)?.BuildCss(
                    builder: builder,
                    navigator: navigator,
                    varPrefix: SetPrefix(varPrefix: varPrefix, type: key)
                ) ?? builder;
            }
        }

        return builder;
    }

    /// <summary>
    /// Retrieves a <see cref="ThemeState" /> associated with a given <see cref="ThemeType" />, if it exists.
    /// </summary>
    /// <param name="key">The <see cref="ThemeType" /> identifier to retrieve.</param>
    /// <returns>The corresponding <see cref="ThemeState" /> if found; otherwise, <see langword="null" />.</returns>
    private ThemeState? Get(ThemeType key) => _children.GetValueOrDefault(key: key);

    /// <summary>
    /// Applies a <see cref="ThemeUpdater" /> to update or create <see cref="ThemeState" /> entries for all matching
    /// <see cref="ThemeType" /> values in this variant.
    /// </summary>
    /// <param name="updater">The <see cref="ThemeUpdater" /> instance containing the update definition.</param>
    /// <returns>The current <see cref="ThemeVariant" /> instance, allowing fluent configuration.</returns>
    internal ThemeVariant Set(ThemeUpdater updater)
    {
        foreach (var key in updater.Navigator.ThemeTypes)
        {
            if (!_children.ContainsKey(key: key))
            {
                _children.Add(key: key, value: new ThemeState());
            }

            Get(key: key)?.Set(updater: updater);
        }

        return this;
    }

    /// <summary>Generates a hierarchical CSS variable prefix string combining the provided prefix and theme type.</summary>
    /// <param name="varPrefix">The existing variable prefix, if any.</param>
    /// <param name="type">The <see cref="ThemeType" /> to append to the prefix.</param>
    /// <returns>A normalized CSS-compatible prefix string.</returns>
    private static string SetPrefix(string? varPrefix, ThemeType type)
    {
        var prefix = varPrefix?.ToCssName() ?? string.Empty;

        return string.IsNullOrWhiteSpace(value: prefix)
            ? string.Empty
            : $"{prefix}-{type}".ToCssName();
    }
}
