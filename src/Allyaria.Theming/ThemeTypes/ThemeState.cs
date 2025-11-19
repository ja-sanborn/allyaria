namespace Allyaria.Theming.ThemeTypes;

/// <summary>
/// Represents the state-level layer of the Allyaria theming hierarchy, managing <see cref="ThemeStyle" /> instances for
/// each <see cref="ComponentState" />.
/// </summary>
/// <remarks>
///     <para>
///     Each <see cref="ThemeState" /> corresponds to a single <see cref="ThemeType" /> variant and contains one or more
///     <see cref="ThemeStyle" /> objects, each mapped to a specific <see cref="ComponentState" /> such as <c>Default</c>,
///     <c>Hovered</c>, or <c>Focused</c>.
///     </para>
///     <para>
///     The class is responsible for building CSS rules at the component-state level and propagating updates via
///     <see cref="ThemeUpdater" /> through the theming hierarchy.
///     </para>
/// </remarks>
internal sealed class ThemeState
{
    /// <summary>
    /// A mapping of <see cref="ComponentState" /> values to their respective <see cref="ThemeStyle" /> definitions.
    /// </summary>
    private readonly Dictionary<ComponentState, ThemeStyle> _children = new();

    /// <summary>
    /// Builds the CSS representation of this theme state and its associated <see cref="ThemeStyle" /> instances.
    /// </summary>
    /// <param name="builder">The <see cref="CssBuilder" /> used to accumulate CSS output.</param>
    /// <param name="navigator">
    /// The <see cref="ThemeNavigator" /> defining the current traversal scope (components, themes, states, styles).
    /// </param>
    /// <param name="varPrefix">An optional variable prefix used to build scoped CSS variable names.</param>
    /// <returns>A <see cref="CssBuilder" /> containing the merged CSS for all applicable component states.</returns>
    internal CssBuilder BuildCss(CssBuilder builder, ThemeNavigator navigator, string? varPrefix = "")
    {
        if (navigator.ComponentStates.Count is 0)
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
            foreach (var key in navigator.ComponentStates)
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
    /// Retrieves the <see cref="ThemeStyle" /> associated with a given <see cref="ComponentState" />, if available.
    /// </summary>
    /// <param name="key">The <see cref="ComponentState" /> to retrieve.</param>
    /// <returns>The corresponding <see cref="ThemeStyle" /> if found; otherwise, <see langword="null" />.</returns>
    private ThemeStyle? Get(ComponentState key) => _children.GetValueOrDefault(key: key);

    /// <summary>
    /// Applies a <see cref="ThemeUpdater" /> to modify or add <see cref="ThemeStyle" /> entries associated with the specified
    /// <see cref="ComponentState" /> values.
    /// </summary>
    /// <param name="updater">The <see cref="ThemeUpdater" /> defining the update operation.</param>
    /// <returns>The same <see cref="ThemeState" /> instance, enabling fluent configuration.</returns>
    internal ThemeState Set(ThemeUpdater updater)
    {
        foreach (var key in updater.Navigator.ComponentStates)
        {
            if (!_children.ContainsKey(key: key))
            {
                _children.Add(key: key, value: new ThemeStyle());
            }

            Get(key: key)?.Set(updater: updater);
        }

        return this;
    }

    /// <summary>Constructs a hierarchical CSS variable prefix combining the provided prefix and component state.</summary>
    /// <param name="varPrefix">The current prefix string, if any.</param>
    /// <param name="type">The <see cref="ComponentState" /> to append.</param>
    /// <returns>A CSS-safe prefix string used for scoped variable naming.</returns>
    private static string SetPrefix(string? varPrefix, ComponentState type)
    {
        var prefix = varPrefix?.ToCssName() ?? string.Empty;

        return string.IsNullOrWhiteSpace(value: prefix)
            ? string.Empty
            : $"{prefix}-{type}".ToCssName();
    }
}
