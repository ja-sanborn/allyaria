using Allyaria.Theming.Contracts;
using Allyaria.Theming.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a strongly-typed CSS image value used by Allyaria theming. The underlying string is normalized to a
/// canonical CSS <c>url("…")</c> token. If the input contains a pre-formed <c>url(...)</c> (even within a longer
/// declaration such as <c>linear-gradient(...), url(foo) no-repeat</c>), only the first <c>url(...)</c> is extracted and
/// used; all other content is discarded. Otherwise, the raw input is validated, unwrapped (if quoted), escaped, and
/// wrapped as <c>url("…")</c>.
/// </summary>
public sealed class AllyariaImageValue : ValueBase
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaImageValue" /> class.</summary>
    /// <param name="value">
    /// The raw image reference to normalize (absolute/relative URL, data/blob URI, or any string containing a <c>url(...)</c>
    /// ).
    /// </param>
    /// <exception cref="AllyariaArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, empty, whitespace, contains disallowed control characters, or
    /// resolves to an unsupported URI scheme (e.g., <c>javascript:</c>, <c>vbscript:</c>).
    /// </exception>
    public AllyariaImageValue(string value)
        : base(Normalize(value)) { }

    /// <summary>
    /// If <paramref name="value" /> parses as an absolute <see cref="Uri" />, ensures its scheme is allowed. Allowed schemes
    /// are <c>http</c>, <c>https</c>, <c>data</c>, and <c>blob</c>. Relative values are permitted.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <exception cref="AllyariaArgumentException">Thrown when an absolute URI uses a disallowed scheme.</exception>
    private static void EnsureAllowedAbsoluteSchemeIfPresent(string value)
    {
        if (Uri.TryCreate(value, UriKind.Absolute, out var uri))
        {
            var allowed = uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) ||
                uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase) ||
                uri.Scheme.Equals("data", StringComparison.OrdinalIgnoreCase) ||
                uri.Scheme.Equals("blob", StringComparison.OrdinalIgnoreCase);

            if (!allowed)
            {
                throw new AllyariaArgumentException($"Unsupported URI scheme '{uri.Scheme}'.", nameof(value), value);
            }
        }
    }

    /// <summary>
    /// Throws <see cref="AllyariaArgumentException" /> when <paramref name="value" /> starts with a dangerous scheme such as
    /// <c>javascript:</c> or <c>vbscript:</c> (case-insensitive).
    /// </summary>
    /// <param name="value">The value to inspect.</param>
    /// <exception cref="AllyariaArgumentException">Thrown when a dangerous scheme prefix is detected.</exception>
    private static void EnsureNoDangerousSchemes(string value)
    {
        if (value.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase) ||
            value.StartsWith("vbscript:", StringComparison.OrdinalIgnoreCase))
        {
            throw new AllyariaArgumentException("Unsupported URI scheme for CSS image value.", nameof(value), value);
        }
    }

    /// <summary>
    /// Escapes a string for safe inclusion inside a CSS <c>url("…")</c> token. Currently escapes backslashes and double
    /// quotes. Parentheses and spaces are safe inside quotes and preserved as-is.
    /// </summary>
    /// <param name="value">The unquoted, validated value to escape.</param>
    /// <returns>The escaped string.</returns>
    private static string EscapeForCssUrl(string value) => value.Replace("\\", "\\\\").Replace("\"", "\\\"");

    /// <summary>
    /// Attempts to extract the inner content of the first CSS <c>url(...)</c> token found in <paramref name="input" />. The
    /// returned string excludes the surrounding <c>url(</c> and <c>)</c> and preserves any quotes inside the token.
    /// </summary>
    /// <param name="input">A CSS string that may contain one or more <c>url(...)</c> tokens.</param>
    /// <returns>The inner value of the first <c>url(...)</c> if found; otherwise <c>null</c>.</returns>
    private static string? ExtractFirstUrlInnerValueOrDefault(string input)
    {
        var match = Regex.Match(
            input,
            @"url\(\s*(?<inner>.*?)\s*\)",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline
        );

        return match.Success
            ? match.Groups["inner"].Value
            : null;
    }

    /// <summary>Normalizes an input string for use as a canonical CSS image <c>url("…")</c> token.</summary>
    /// <param name="value">
    /// Raw image reference. May be a bare path/URI or a larger CSS value containing one or more <c>url(...)</c> tokens.
    /// </param>
    /// <returns>A trimmed, validated, and escaped string in the form <c>url("…")</c>.</returns>
    /// <exception cref="AllyariaArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, empty, whitespace, contains disallowed control characters, or
    /// resolves to an unsupported URI scheme (e.g., <c>javascript:</c>, <c>vbscript:</c>).
    /// </exception>
    private static string Normalize(string value)
    {
        var trimmed = ValidateInput(value);
        var extractedOrOriginal = ExtractFirstUrlInnerValueOrDefault(trimmed) ?? trimmed;
        var unquoted = UnwrapOptionalQuotes(extractedOrOriginal);

        EnsureNoDangerousSchemes(unquoted);
        EnsureAllowedAbsoluteSchemeIfPresent(unquoted);

        var escaped = EscapeForCssUrl(unquoted);

        return $"url(\"{escaped}\")";
    }

    /// <summary>Parses the specified string into an <see cref="AllyariaImageValue" />.</summary>
    /// <param name="value">The input string to parse.</param>
    /// <returns>A new <see cref="AllyariaImageValue" /> containing the normalized <paramref name="value" />.</returns>
    /// <exception cref="AllyariaArgumentException">
    /// Thrown when <paramref name="value" /> is invalid; see <see cref="Normalize(string)" /> for the validation rules.
    /// </exception>
    public static AllyariaImageValue Parse(string value) => new(value);

    /// <summary>Builds CSS declarations for a background image with a contrast-enhancing overlay.</summary>
    /// <param name="backgroundColor">
    /// The known page or container background color beneath the image. Used to determine whether the overlay should be dark or
    /// light for better contrast.
    /// </param>
    /// <param name="stretch">
    /// If <c>true</c>, returns a set of individual CSS declarations—<c>background-image</c>, <c>background-position</c>,
    /// <c>background-repeat</c>, and <c>background-size</c>—so that the image covers the area and is centered with no repeat.
    /// If <c>false</c>, returns only a single <c>background-image</c> declaration (no extra sizing or positioning).
    /// </param>
    /// <returns>
    /// A CSS string: either just a <c>background-image</c> property (when <paramref name="stretch" /> is <c>false</c>), or
    /// multiple declarations joined together to achieve a centered, cover-filling background (when <paramref name="stretch" />
    /// is <c>true</c>).
    /// </returns>
    /// <remarks>
    /// The overlay increases contrast: – For light backgrounds (relative luminance ≥ 0.5), it uses <c>rgba(0,0,0,0.5)</c>. –
    /// For dark backgrounds, it uses <c>rgba(255,255,255,0.5)</c>. This helps keep the background image legible regardless of
    /// the page’s base color.
    /// </remarks>
    public string ToCssBackground(AllyariaColorValue backgroundColor, bool stretch = true)
    {
        var lum = ColorHelper.RelativeLuminance(backgroundColor);

        var overlay = lum >= 0.5
            ? "rgba(0, 0, 0, 0.5)"
            : "rgba(255, 255, 255, 0.5)";

        var image = $"background-image:linear-gradient({overlay},{overlay}),{Value};";
        var position = "background-position:center;";
        var repeat = "background-repeat:no-repeat;";
        var size = "background-size:cover";

        return stretch
            ? $"{image}{position}{repeat}{size}"
            : image;
    }

    /// <summary>Builds CSS variables for a background image with a contrast-enhancing overlay.</summary>
    /// <param name="prefix">A string used to namespace the CSS variables.</param>
    /// <param name="backgroundColor">
    /// The known page or container background color beneath the image. Used to determine whether the overlay should be dark or
    /// light for better contrast.
    /// </param>
    /// <param name="stretch">
    /// If <c>true</c>, returns a set of individual CSS variables—<c>background-image</c>, <c>background-position</c>,
    /// <c>background-repeat</c>, and <c>background-size</c>—so that the image covers the area and is centered with no repeat.
    /// If <c>false</c>, returns only a single <c>background-image</c> declaration (no extra sizing or positioning).
    /// </param>
    /// <returns>
    /// A CSS string: either just a <c>background-image</c> variable (when <paramref name="stretch" /> is <c>false</c>), or
    /// multiple variables joined together to achieve a centered, cover-filling background (when <paramref name="stretch" /> is
    /// <c>true</c>).
    /// </returns>
    /// <remarks>
    /// The overlay increases contrast: – For light backgrounds (relative luminance ≥ 0.5), it uses <c>rgba(0,0,0,0.5)</c>. –
    /// For dark backgrounds, it uses <c>rgba(255,255,255,0.5)</c>. This helps keep the background image legible regardless of
    /// the page’s base color.
    /// </remarks>
    public string ToCssVarsBackground(string prefix, AllyariaColorValue backgroundColor, bool stretch = true)
    {
        var lum = ColorHelper.RelativeLuminance(backgroundColor);

        var overlay = lum >= 0.5
            ? "rgba(0, 0, 0, 0.5)"
            : "rgba(255, 255, 255, 0.5)";

        var image = $"{prefix}background-image:linear-gradient({overlay},{overlay}),{Value};";
        var position = $"{prefix}background-position:center;";
        var repeat = $"{prefix}background-repeat:no-repeat;";
        var size = $"{prefix}background-size:cover";

        return stretch
            ? $"{image}{position}{repeat}{size}"
            : image;
    }

    /// <summary>Attempts to parse the specified string into an <see cref="AllyariaImageValue" />.</summary>
    /// <param name="value">The input string to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="AllyariaImageValue" /> if parsing succeeded; otherwise
    /// <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(string value, [NotNullWhen(true)] out AllyariaImageValue? result)
    {
        try
        {
            result = new AllyariaImageValue(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>
    /// Removes a single pair of matching surrounding quotes (single or double) from <paramref name="s" />, if present. Returns
    /// the original string if not quoted or if quotes do not match.
    /// </summary>
    /// <param name="s">The input string to unwrap.</param>
    /// <returns>The unwrapped string, or <paramref name="s" /> if no wrapping quotes are present.</returns>
    private static string UnwrapOptionalQuotes(string s)
    {
        if (s.Length >= 2)
        {
            var first = s[0];
            var last = s[^1];

            if ((first is '"' && last is '"') || (first is '\'' && last is '\''))
            {
                return s.Substring(1, s.Length - 2);
            }
        }

        return s;
    }

    /// <summary>Defines an implicit conversion from <see cref="string" /> to <see cref="AllyariaImageValue" />.</summary>
    /// <param name="value">The string value to convert.</param>
    /// <returns>A new <see cref="AllyariaImageValue" /> containing the normalized <paramref name="value" />.</returns>
    /// <exception cref="AllyariaArgumentException">
    /// Thrown when <paramref name="value" /> is invalid; see <see cref="Normalize(string)" /> for the validation rules.
    /// </exception>
    public static implicit operator AllyariaImageValue(string value) => new(value);

    /// <summary>Defines an implicit conversion from <see cref="AllyariaImageValue" /> to <see cref="string" />.</summary>
    /// <param name="value">The <see cref="AllyariaImageValue" /> instance.</param>
    /// <returns>The underlying normalized string value (a canonical CSS <c>url("…")</c> token).</returns>
    public static implicit operator string(AllyariaImageValue value) => value.Value;
}
