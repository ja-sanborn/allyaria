using Allyaria.Theming.Contracts;

namespace Allyaria.Theming.Helpers;

/// <summary>Provides helper extensions for constructing CSS style declarations from Allyaria theme values.</summary>
internal static class StyleHelper
{
    /// <summary>Appends a CSS declaration for the specified property and value to a <see cref="StringBuilder" />.</summary>
    /// <param name="builder">The <see cref="StringBuilder" /> used to collect CSS declarations.</param>
    /// <param name="value">The theming value containing the CSS representation.</param>
    /// <param name="propertyName">The CSS property name to apply (for example, <c>"color"</c>).</param>
    /// <param name="varPrefix">
    /// The prefix used when generating a CSS variable name. The resulting variable takes the form
    /// <c>--{varPrefix}-{propertyName}</c>. Hyphens and whitespace are normalized.
    /// </param>
    public static void ToCss(this StringBuilder builder, ValueBase value, string propertyName, string? varPrefix)
    {
        if (string.IsNullOrWhiteSpace(propertyName) || string.IsNullOrWhiteSpace(value.Value))
        {
            return;
        }

        var prefix = varPrefix.ToCssPrefix();

        var prefixedProperty = string.IsNullOrWhiteSpace(prefix)
            ? propertyName
            : $"--{prefix}-{propertyName}";

        builder.Append(value.ToCss(prefixedProperty));
    }

    /// <summary>Converts a prefix string into a normalized, lowercase CSS variable prefix.</summary>
    /// <param name="prefix">The prefix to normalize. May be <see langword="null" /> or empty.</param>
    /// <returns>
    /// A normalized, lowercase prefix suitable for use in CSS variable names. If the input is <see langword="null" /> or
    /// whitespace, an empty string is returned.
    /// </returns>
    public static string ToCssPrefix(this string? prefix)
        => Regex.Replace((prefix ?? string.Empty).Replace('_', '-'), @"[\s-]+", "-").Trim('-').ToLowerInvariant();
}
