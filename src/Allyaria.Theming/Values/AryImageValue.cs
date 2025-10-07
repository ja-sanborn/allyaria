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
public sealed class AryImageValue : ValueBase
{
    /// <summary>Precompiled regular expression that extracts the first CSS <c>url(...)</c> token's inner value.</summary>
    private static readonly Regex UrlRegex = new(
        @"url\(\s*(?<inner>(?:(?:""(?:\\.|[^""\\])*"")|(?:'(?:\\.|[^'\\])*')|(?:\\.|[^'"")\\])+))\s*\)",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.Compiled
    );

    /// <summary>
    /// Initializes a new instance of the <see cref="AryImageValue" /> class from the provided raw value, normalizing it to a
    /// canonical CSS <c>url("…")</c> token. If a <paramref name="backgroundColor" /> is supplied, a theme-appropriate overlay
    /// is prepended via a <c>linear-gradient(...)</c> per theming rules.
    /// </summary>
    /// <param name="value">The raw CSS image value or URL-like string.</param>
    /// <param name="backgroundColor">Optional background color used to choose an overlay for contrast.</param>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is invalid or uses a disallowed URI
    /// scheme.
    /// </exception>
    public AryImageValue(string value, AryColorValue? backgroundColor = null)
        : base(Normalize(value, backgroundColor)) { }

    /// <summary>
    /// If <paramref name="value" /> parses as an absolute <see cref="Uri" />, ensures its scheme is allowed. Allowed schemes
    /// are <c>http</c>, <c>https</c>, <c>data</c>, and <c>blob</c>. Relative values are permitted.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <exception cref="AryArgumentException">Thrown when an absolute URI uses a disallowed scheme.</exception>
    private static void EnsureAllowedAbsoluteSchemeIfPresent(string value)
    {
        if (!Uri.TryCreate(value, UriKind.Absolute, out var uri))
        {
            return;
        }

        var allowed = uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) ||
            uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase) ||
            uri.Scheme.Equals("data", StringComparison.OrdinalIgnoreCase) ||
            uri.Scheme.Equals("blob", StringComparison.OrdinalIgnoreCase);

        if (!allowed)
        {
            throw new AryArgumentException($"Unsupported URI scheme '{uri.Scheme}'.", nameof(value), value);
        }
    }

    /// <summary>
    /// Throws <see cref="AryArgumentException" /> when <paramref name="value" /> starts with a dangerous scheme such as
    /// <c>javascript:</c> or <c>vbscript:</c> (case-insensitive). Leading whitespace is ignored.
    /// </summary>
    /// <param name="value">The value to inspect.</param>
    /// <exception cref="AryArgumentException">Thrown when a dangerous scheme prefix is detected.</exception>
    private static void EnsureNoDangerousSchemes(string value)
    {
        var s = value.TrimStart();

        if (s.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase) ||
            s.StartsWith("vbscript:", StringComparison.OrdinalIgnoreCase))
        {
            throw new AryArgumentException("Unsupported URI scheme for CSS image value.", nameof(value), value);
        }
    }

    /// <summary>
    /// Escapes a string for safe inclusion inside a CSS <c>url("…")</c> token. Currently escapes backslashes and double
    /// quotes. Parentheses and spaces are safe inside quotes and preserved as-is.
    /// </summary>
    /// <param name="value">The unquoted, validated value to escape.</param>
    /// <returns>The escaped string.</returns>
    private static string EscapeForCssUrl(string value) => value.Replace("\\", "\\\\").Trim();

    /// <summary>
    /// Attempts to extract the inner content of the first CSS <c>url(...)</c> token found in <paramref name="input" />. The
    /// returned string excludes the surrounding <c>url(</c> and <c>)</c> and preserves any quotes inside the token.
    /// </summary>
    /// <param name="input">A CSS string that may contain one or more <c>url(...)</c> tokens.</param>
    /// <returns>The inner value of the first <c>url(...)</c> if found; otherwise <c>null</c>.</returns>
    private static string? ExtractFirstUrlInnerValueOrDefault(string input)
    {
        var match = UrlRegex.Match(input);

        return match.Success
            ? match.Groups["inner"].Value
            : null;
    }

    /// <summary>
    /// Normalizes a raw CSS image value to a canonical CSS <c>url("…")</c> token. If a <paramref name="backgroundColor" /> is
    /// provided, a contrast-appropriate 50% overlay (black for light backgrounds, white for dark backgrounds) is prepended via
    /// a <c>linear-gradient(...)</c>, followed by the <c>url("…")</c>.
    /// </summary>
    /// <param name="value">The raw input string.</param>
    /// <param name="backgroundColor">Optional background color for overlay selection.</param>
    /// <returns>A normalized CSS value suitable for <c>background-image</c>.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is invalid or uses a disallowed/dangerous
    /// scheme.
    /// </exception>
    private static string Normalize(string value, AryColorValue? backgroundColor = null)
    {
        var trimmed = ValidateInput(value);
        var extractedOrOriginal = ExtractFirstUrlInnerValueOrDefault(trimmed) ?? trimmed;
        var unquoted = UnwrapOptionalQuotes(extractedOrOriginal);

        EnsureNoDangerousSchemes(unquoted);
        EnsureAllowedAbsoluteSchemeIfPresent(unquoted);

        var escaped = EscapeForCssUrl(unquoted);
        var url = $"url(\"{escaped}\")";

        if (backgroundColor is null)
        {
            return url;
        }

        var lum = ColorHelper.RelativeLuminance(backgroundColor);

        var overlay = lum >= 0.5
            ? "rgba(0,0,0,0.5)"
            : "rgba(255,255,255,0.5)";

        return $"{url},linear-gradient({overlay},{overlay})";
    }

    /// <summary>Parses the specified string into an <see cref="AryImageValue" />.</summary>
    /// <param name="value">The input string to parse.</param>
    /// <returns>A new <see cref="AryImageValue" /> containing the normalized <paramref name="value" />.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is invalid; see <see cref="Normalize(string, AryColorValue?)" /> for the
    /// validation rules.
    /// </exception>
    public static AryImageValue Parse(string value) => new(value);

    /// <summary>Attempts to parse the specified string into an <see cref="AryImageValue" />.</summary>
    /// <param name="value">The input string to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="AryImageValue" /> if parsing succeeded; otherwise <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(string value, [NotNullWhen(true)] out AryImageValue? result)
    {
        try
        {
            result = new AryImageValue(value);

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

    /// <summary>Defines an implicit conversion from <see cref="string" /> to <see cref="AryImageValue" />.</summary>
    /// <param name="value">The string value to convert.</param>
    /// <returns>A new <see cref="AryImageValue" /> containing the normalized <paramref name="value" />.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is invalid; see <see cref="Normalize(string, AryColorValue?)" /> for the
    /// validation rules.
    /// </exception>
    public static implicit operator AryImageValue(string value) => new(value);

    /// <summary>Defines an implicit conversion from <see cref="AryImageValue" /> to <see cref="string" />.</summary>
    /// <param name="value">The <see cref="AryImageValue" /> instance.</param>
    /// <returns>
    /// The underlying normalized string value (a canonical CSS <c>url("…")</c> token, or a value list when an overlay is
    /// present).
    /// </returns>
    public static implicit operator string(AryImageValue value) => value.Value;
}
