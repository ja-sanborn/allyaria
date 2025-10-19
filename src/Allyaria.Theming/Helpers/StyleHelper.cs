namespace Allyaria.Theming.Helpers;

/// <summary>Provides helper extensions for constructing CSS style declarations from Allyaria theme values.</summary>
internal static class StyleHelper
{
    /// <summary>Appends a CSS declaration for the specified property and value to a <see cref="StringBuilder" />.</summary>
    /// <param name="builder">The <see cref="StringBuilder" /> used to collect CSS declarations.</param>
    /// <param name="propertyName">The CSS property name to apply (for example, <c>"color"</c>).</param>
    /// <param name="value">The theming value containing the CSS representation.</param>
    /// <param name="varPrefix">
    /// The prefix used when generating a CSS variable name. The resulting variable takes the form
    /// <c>--{varPrefix}-{propertyName}</c>. Hyphens and whitespace are normalized.
    /// </param>
    public static void ToCss(this StringBuilder builder,
        string propertyName,
        ThemeBase? value = null,
        string? varPrefix = "")
    {
        if (string.IsNullOrWhiteSpace(propertyName) || string.IsNullOrWhiteSpace(value?.Value))
        {
            return;
        }

        var prefix = varPrefix.ToCssName();

        var prefixedProperty = string.IsNullOrWhiteSpace(prefix)
            ? propertyName
            : $"--{prefix}-{propertyName}";

        builder.Append(value.ToCss(prefixedProperty));
    }

    /// <summary>Converts a value string into a normalized, lowercase CSS variable value for property name or prefix.</summary>
    /// <param name="value">The value to normalize. May be <see langword="null" /> or empty.</param>
    /// <returns>
    /// A normalized, lowercase value suitable for use in CSS variable names. If the input is <see langword="null" /> or
    /// whitespace, an empty string is returned.
    /// </returns>
    public static string ToCssName(this string? value)
        => Regex.Replace((value ?? string.Empty).Replace('_', '-'), @"[\s-]+", "-").Trim('-').ToLowerInvariant();
}
