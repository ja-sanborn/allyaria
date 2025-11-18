namespace Allyaria.Theming.Helpers;

/// <summary>
/// Provides a fluent, immutable-safe builder for constructing CSS property/value declarations used throughout the Allyaria
/// theming system.
/// <para>
/// <see cref="CssBuilder" /> is designed to aggregate style key-value pairs in a deterministic order using a
/// <see cref="SortedDictionary{TKey,TValue}" /> for consistent and predictable CSS output.
/// </para>
/// </summary>
/// <remarks>
/// This class is central to how Allyaria generates serialized CSS from the strongly-typed <see cref="IStyleValue" /> and
/// <see cref="StyleGroup" /> abstractions.
/// </remarks>
public sealed class CssBuilder
{
    /// <summary>
    /// The underlying collection of CSS property/value pairs. The use of <see cref="SortedDictionary{TKey,TValue}" /> ensures
    /// consistent ordering of CSS properties in the final output string, improving diffability and test determinism.
    /// </summary>
    private readonly SortedDictionary<string, string> _styles = new();

    /// <summary>
    /// Adds a new CSS property/value pair to the builder, if both are valid and non-empty.
    /// <para>
    /// Property names and prefixes are automatically converted to CSS-compliant formats using the <c>ToCssName()</c> helper
    /// (e.g., PascalCase → kebab-case).
    /// </para>
    /// </summary>
    /// <param name="name">The CSS property name or token name to add. This will be normalized to kebab-case.</param>
    /// <param name="value">
    /// The CSS value associated with the property. Must be non-empty and already validated by the corresponding
    /// <see cref="IStyleValue" /> implementation.
    /// </param>
    /// <param name="varPrefix">
    /// An optional variable prefix used to generate CSS custom properties (e.g., <c>--theme-color-primary</c>). If provided,
    /// the final property name becomes <c>--{prefix}-{property}</c>.
    /// </param>
    /// <returns>The current <see cref="CssBuilder" /> instance, enabling fluent method chaining.</returns>
    /// <remarks>
    /// If the property or value is empty or invalid, the call is ignored and the builder remains unchanged.
    /// </remarks>
    public CssBuilder Add(string? name, string? value, string? varPrefix = "")
    {
        if (string.IsNullOrWhiteSpace(value: name) || string.IsNullOrWhiteSpace(value: value))
        {
            return this;
        }

        string property;

        try
        {
            property = name.FromPrefixedCase().ToCssName();
        }
        catch
        {
            property = string.Empty;
        }

        if (string.IsNullOrWhiteSpace(value: property))
        {
            return this;
        }

        var prefix = varPrefix.ToCssName();

        var propertyName = string.IsNullOrWhiteSpace(value: prefix)
            ? property
            : $"--{prefix}-{property}";

        _ = _styles.TryAdd(key: propertyName, value: value);

        return this;
    }

    /// <summary>Adds multiple CSS property/value pairs to the builder by parsing a semicolon-delimited list.</summary>
    /// <param name="cssList">
    /// A semicolon-separated list of CSS declarations in the form <c>property:value</c>. Entries with missing property or
    /// value segments are ignored.
    /// </param>
    /// <returns>The current <see cref="CssBuilder" /> instance, enabling fluent method chaining.</returns>
    /// <remarks>
    /// This method provides a convenience parser for raw CSS fragments and forwards each parsed entry to
    /// <see cref="Add(string?, string?, string?)" />. Invalid entries are safely skipped.
    /// </remarks>
    public CssBuilder AddRange(string? cssList)
    {
        if (string.IsNullOrWhiteSpace(value: cssList))
        {
            return this;
        }

        var split = cssList.Split(separator: ';', options: StringSplitOptions.RemoveEmptyEntries);

        foreach (var item in split)
        {
            var pair = item.Split(separator: ':', options: StringSplitOptions.RemoveEmptyEntries);

            if (pair.Length < 2)
            {
                continue;
            }

            Add(name: pair[0], value: pair[1]);
        }

        return this;
    }

    /// <summary>
    /// Builds the final CSS representation of all accumulated styles in this builder.
    /// <para>
    /// The result is a semicolon-separated list of <c>property:value</c> pairs, ready for inline or stylesheet inclusion.
    /// </para>
    /// </summary>
    /// <returns>
    /// A single CSS string containing all property/value pairs. If no styles are defined, returns <see cref="string.Empty" />.
    /// </returns>
    public override string ToString()
    {
        if (_styles.Count is 0)
        {
            return string.Empty;
        }

        var list = new List<string>();

        foreach (var styleValue in _styles)
        {
            list.Add(item: $"{styleValue.Key}:{styleValue.Value}");
        }

        return string.Join(separator: ';', values: list);
    }
}
