namespace Allyaria.Theming.ThemeTypes;

/// <summary>
/// Represents a hierarchical theming component node that stores and resolves <see cref="ThemeVariant" /> instances for
/// each <see cref="ComponentType" />.
/// </summary>
/// <remarks>
///     <para>
///     Each <see cref="ThemeComponent" /> corresponds to a level in the Allyaria theming structure. It can recursively
///     build CSS declarations for child components and apply updates through <see cref="ThemeUpdater" /> objects.
///     </para>
///     <para>
///     The structure supports both full theme traversal (when no component types are specified) and targeted CSS
///     generation for specific <see cref="ComponentType" /> values.
///     </para>
/// </remarks>
internal sealed class ThemeComponent
{
    /// <summary>A collection of component variants indexed by <see cref="ComponentType" />.</summary>
    private readonly Dictionary<ComponentType, ThemeVariant> _children = new();

    /// <summary>
    /// Builds a CSS representation of this component and its children using the provided <see cref="CssBuilder" />.
    /// </summary>
    /// <param name="builder">The <see cref="CssBuilder" /> instance used to accumulate CSS output.</param>
    /// <param name="navigator">The <see cref="ThemeNavigator" /> that defines which components, themes, and states to include.</param>
    /// <param name="varPrefix">An optional variable prefix for CSS variable names (used for <c>:root</c> variable generation).</param>
    /// <returns>A <see cref="CssBuilder" /> instance containing the combined CSS for the specified theme scope.</returns>
    internal CssBuilder BuildCss(CssBuilder builder, ThemeNavigator navigator, string? varPrefix = "")
    {
        if (navigator.ComponentTypes.Count is 0)
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
            foreach (var key in navigator.ComponentTypes)
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

    /// <summary>Retrieves the <see cref="ThemeVariant" /> associated with the given component type, if it exists.</summary>
    /// <param name="key">The <see cref="ComponentType" /> to retrieve.</param>
    /// <returns>The corresponding <see cref="ThemeVariant" /> instance, or <see langword="null" /> if not found.</returns>
    private ThemeVariant? Get(ComponentType key) => _children.GetValueOrDefault(key: key);

    /// <summary>
    /// Applies a theme update by inserting or updating child <see cref="ThemeVariant" /> instances according to the specified
    /// <see cref="ThemeUpdater" />.
    /// </summary>
    /// <param name="updater">The <see cref="ThemeUpdater" /> describing which components and style values to update.</param>
    /// <returns>The current <see cref="ThemeComponent" /> instance, enabling fluent chaining.</returns>
    internal ThemeComponent Set(ThemeUpdater updater)
    {
        foreach (var key in updater.Navigator.ComponentTypes)
        {
            if (!_children.ContainsKey(key: key))
            {
                _children.Add(key: key, value: new ThemeVariant());
            }

            Get(key: key)?.Set(updater: updater);
        }

        return this;
    }

    /// <summary>
    /// Constructs a CSS variable prefix by combining an existing prefix with the specified component type.
    /// </summary>
    /// <param name="varPrefix">The existing prefix, if any.</param>
    /// <param name="type">The <see cref="ComponentType" /> to append.</param>
    /// <returns>A normalized CSS-friendly prefix string suitable for use in variable naming.</returns>
    private static string SetPrefix(string? varPrefix, ComponentType type)
    {
        var prefix = varPrefix?.ToCssName() ?? string.Empty;

        return string.IsNullOrWhiteSpace(value: prefix)
            ? string.Empty
            : $"{prefix}-{type}".ToCssName();
    }
}
