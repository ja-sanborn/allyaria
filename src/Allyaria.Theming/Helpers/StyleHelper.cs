using Allyaria.Theming.Contracts;
using System.Text;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Helpers;

/// <summary>Provides helper extensions for building CSS style declarations from theme values.</summary>
internal static class StyleHelper
{
    /// <summary>Appends a CSS declaration for the given property and value to a <see cref="StringBuilder" />.</summary>
    /// <param name="builder">The <see cref="StringBuilder" /> used to collect CSS declarations.</param>
    /// <param name="value">The theming value containing the CSS representation.</param>
    /// <param name="propertyName">The CSS property name to apply (e.g., "color").</param>
    /// <param name="varPrefix">
    /// The prefix used when generating a CSS variable name. Results in a property of the form
    /// <c>--{varPrefix}-{propertyName}</c>. Hyphens and whitespace are normalized.
    /// </param>
    public static void ToCss(this StringBuilder builder, ValueBase value, string propertyName, string? varPrefix)
    {
        if (string.IsNullOrWhiteSpace(propertyName) || string.IsNullOrWhiteSpace(value.Value))
        {
            return;
        }

        var prefix = Regex.Replace(varPrefix ?? string.Empty, @"[\s-]+", "-").Trim('-').ToLowerInvariant();

        var prefixedProperty = string.IsNullOrWhiteSpace(prefix)
            ? propertyName
            : $"--{prefix}-{propertyName}";

        builder.Append(value.ToCss(prefixedProperty));
    }
}
