using System.Globalization;

namespace Allyaria.Theming.Helpers;

/// <summary>
/// Internal helper utilities for validating, normalizing, and composing CSS style values. Exposed as <c>public static</c>
/// methods to allow consumption from style structs, while the class itself remains <c>internal</c> to the theming
/// assembly. All numeric parsing uses <see cref="CultureInfo.InvariantCulture" />.
/// </summary>
internal static class StyleHelpers
{
    /// <summary>
    /// A comprehensive set of CSS length units recognized by <see cref="IsLength(string)" />. Includes absolute, relative, and
    /// viewport units.
    /// </summary>
    public static readonly string[] LengthUnits =
    {
        // Relative font & element units
        "em",
        "rem",
        "lh",
        "rlh",
        "ex",
        "ch",

        // Viewport units (legacy + logical)
        "vw",
        "vh",
        "vmin",
        "vmax",
        "vi",
        "vb",

        // Absolute units
        "px",
        "cm",
        "mm",
        "q",
        "in",
        "pt",
        "pc"
    };

    /// <summary>
    /// Determines whether the token starts with a supported CSS function name. Typical examples are <c>var(…)</c>,
    /// <c>calc(…)</c>, <c>min(…)</c>, <c>max(…)</c>, <c>clamp(…)</c>.
    /// </summary>
    /// <param name="s">The token to evaluate.</param>
    /// <param name="functionNames">Function names to check, without the trailing parenthesis (e.g., "var").</param>
    /// <returns>
    /// <c>true</c> if the token starts with any of the provided function names followed by '('; otherwise <c>false</c>.
    /// </returns>
    public static bool IsCssFunction(string? s, params string[]? functionNames)
    {
        if (s is null || functionNames is null || functionNames.Length == 0)
        {
            return false;
        }

        return functionNames.Any(name => s.StartsWith(name, StringComparison.Ordinal) && s.Length > name.Length &&
            s[name.Length] == '('
        );
    }

    /// <summary>
    /// Determines whether the provided token is a CSS length value using a known unit. The check is case-sensitive and assumes
    /// the input is already trimmed/lowercased when appropriate.
    /// </summary>
    /// <param name="s">The token to evaluate.</param>
    /// <returns><c>true</c> when the token is a length; otherwise <c>false</c>.</returns>
    public static bool IsLength(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        return LengthUnits.Any(u => s.EndsWith(u, StringComparison.Ordinal) &&
            s.Length > u.Length &&
            double.TryParse(s[..^u.Length], NumberStyles.Float, CultureInfo.InvariantCulture, out _)
        );
    }

    /// <summary>Determines whether the provided token is a CSS length or percentage value.</summary>
    /// <param name="s">The token to evaluate.</param>
    /// <returns><c>true</c> when the token is a length or percentage; otherwise <c>false</c>.</returns>
    public static bool IsLengthOrPercentage(string s) => IsLength(s) || IsPercentage(s);

    /// <summary>
    /// Determines if the value represents a number using invariant culture. Accepts integer and floating-point forms and
    /// optional sign.
    /// </summary>
    /// <param name="s">The token to evaluate.</param>
    /// <returns><c>true</c> when numeric; otherwise <c>false</c>.</returns>
    public static bool IsNumeric(string s)
        => double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out _);

    /// <summary>
    /// Determines whether the provided token is a CSS percentage value (ends with '%' and the preceding part is numeric).
    /// </summary>
    /// <param name="s">The token to evaluate.</param>
    /// <returns><c>true</c> when the token is a percentage; otherwise <c>false</c>.</returns>
    public static bool IsPercentage(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        if (!s.EndsWith('%'))
        {
            return false;
        }

        var number = s[..^1];

        return double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Determines if the value represents a positive (greater than zero) unitless number. Useful for <c>line-height</c>
    /// unitless values.
    /// </summary>
    /// <param name="s">The token to evaluate.</param>
    /// <returns><c>true</c> when unitless and positive; otherwise <c>false</c>.</returns>
    public static bool IsUnitlessPositive(string s)
        => double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var d) && d > 0;

    /// <summary>
    /// Shorthand for common function checks: returns <c>true</c> if the token is <c>var(…)</c> or <c>calc(…)</c>.
    /// </summary>
    /// <param name="s">The token to evaluate.</param>
    /// <returns><c>true</c> when the token starts with <c>var(</c> or <c>calc(</c>; otherwise <c>false</c>.</returns>
    public static bool IsVarOrCalc(string? s) => IsCssFunction(s, "var", "calc");

    /// <summary>Normalizes and validates a <c>font-size</c> value.</summary>
    /// <param name="param">Parameter/property name used in exception messages.</param>
    /// <param name="raw">The raw input string.</param>
    /// <param name="allowed">The allowed set of keywords.</param>
    /// <returns>The normalized value.</returns>
    /// <exception cref="ArgumentException">Thrown when the input does not represent a valid CSS <c>font-size</c>.</exception>
    public static string Normalize(string param, string raw, HashSet<string> allowed)
    {
        // Preserve original casing for function identifiers; lower-case only when treating as keywords or unit tokens.
        var v = raw.Trim();

        // Accept common CSS function forms without altering the content.
        if (IsVarOrCalc(v) || IsCssFunction(v, "min", "max", "clamp"))
        {
            return v;
        }

        // Keyword path (lower-case & validate).
        var lower = v.ToLowerInvariant();

        if (allowed.Contains(lower))
        {
            return lower;
        }

        // Length/percentage path (accepts px, em, rem, ch, ex, lh, rlh, cm, mm, q, in, pt, pc, vw, vh, vi, vb, vmin, vmax, and %).
        if (IsLengthOrPercentage(lower))
        {
            return lower;
        }

        // Bare numeric → px (culture-invariant).
        if (IsNumeric(lower))
        {
            return string.Concat(lower, "px");
        }

        // Failed normalization.
        throw new ArgumentException(
            $"{param} must be a keyword {string.Join(',', allowed)}, a length/percentage, a bare number (→ px), or a supported CSS function such as var()/calc()/min()/max()/clamp().",
            nameof(param)
        );
    }

    /// <summary>
    /// Normalizes a <c>line-height</c>-like value: accepts <c>normal</c>, unitless positive numbers, arbitrary lengths,
    /// percentages, and <c>var()</c>/<c>calc()</c>. Negative values are rejected.
    /// </summary>
    /// <param name="value">The raw input value.</param>
    /// <returns>Canonicalized value or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown when the value is invalid.</exception>
    public static string? NormalizeLineHeight(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var v = value.Trim();

        if (IsVarOrCalc(v))
        {
            return v;
        }

        var lower = v.ToLowerInvariant();

        if (lower == "normal")
        {
            return lower;
        }

        if (IsUnitlessPositive(lower))
        {
            // Canonicalize numeric formatting (e.g., "1.0" => "1")
            return double.Parse(lower, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
        }

        if (IsLengthOrPercentage(lower))
        {
            if (lower.StartsWith("-", StringComparison.Ordinal))
            {
                throw new ArgumentException("Line height must not be negative.");
            }

            return lower;
        }

        throw new ArgumentException(
            "Line height must be 'normal', a positive unitless number, a length/percentage, or var()/calc()."
        );
    }

    /// <summary>
    /// Normalizes a space-separated token list by validating against an allowed set, optionally disallowing combinations with
    /// the <paramref name="noneKeyword" />. The resulting tokens are lowercased, de-duplicated (order-preserving), and joined
    /// with a single space.
    /// </summary>
    /// <param name="value">The input string containing tokens.</param>
    /// <param name="allowed">The allowed token set.</param>
    /// <param name="paramName">Parameter/property name used in exception messages.</param>
    /// <param name="disallowNoneCombination">
    /// When <c>true</c>, prevents combining <paramref name="noneKeyword" /> with other
    /// tokens.
    /// </param>
    /// <param name="noneKeyword">
    /// The special token that cannot be combined if <paramref name="disallowNoneCombination" /> is <c>true</c>.
    /// </param>
    /// <param name="allowedHint">Human-readable hint used in exception messages.</param>
    /// <returns>
    /// The canonicalized token string or <c>null</c> if <paramref name="value" /> is <c>null</c> or empty after trimming.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when an invalid token is present or when <paramref name="noneKeyword" /> is combined with other tokens while
    /// disallowed.
    /// </exception>
    public static string? NormalizeSpaceSeparatedTokens(string? value,
        ISet<string> allowed,
        string paramName,
        bool disallowNoneCombination,
        string noneKeyword,
        string allowedHint)
    {
        if (value is null)
        {
            return null;
        }

        var tokens = value
            .Split(
                new[]
                {
                    ' ',
                    '\t',
                    '\r',
                    '\n'
                }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            )
            .Select(t => t.ToLowerInvariant())
            .ToArray();

        if (tokens.Length == 0)
        {
            return null;
        }

        if (tokens.Any(t => !allowed.Contains(t)))
        {
            throw new ArgumentException($"{paramName} allows only: {allowedHint}.", paramName);
        }

        if (disallowNoneCombination && tokens.Length > 1 && tokens.Contains(noneKeyword))
        {
            throw new ArgumentException(
                $"{paramName} '{noneKeyword}' cannot be combined with other values.", paramName
            );
        }

        var ordered = new List<string>(tokens.Length);
        var seen = new HashSet<string>(StringComparer.Ordinal);

        foreach (var t in tokens)
        {
            if (seen.Add(t))
            {
                ordered.Add(t);
            }
        }

        return string.Join(' ', ordered);
    }

    /// <summary>Lowercases a non-null string using invariant culture.</summary>
    /// <param name="value">The input value.</param>
    /// <returns>Lowercased value.</returns>
    public static string NormalizeToLowerInvariant(string value) => value.ToLowerInvariant();

    /// <summary>
    /// Normalizes tracking-like values (e.g., <c>letter-spacing</c>, <c>word-spacing</c>). Accepts <c>normal</c>, arbitrary
    /// lengths, percentages, <c>var()</c>/<c>calc()</c>, or bare numeric values (which are treated as pixels).
    /// </summary>
    /// <param name="value">The raw input value.</param>
    /// <param name="paramName">Parameter/property name used in exception messages.</param>
    /// <returns>Canonicalized value or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown when the value is invalid.</exception>
    public static string? NormalizeTrack(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var v = value.Trim();

        if (IsVarOrCalc(v))
        {
            return v;
        }

        var lower = v.ToLowerInvariant();

        if (lower == "normal")
        {
            return lower;
        }

        if (IsLengthOrPercentage(lower))
        {
            return lower;
        }

        if (IsNumeric(lower))
        {
            return string.Create(CultureInfo.InvariantCulture, $"{lower}px");
        }

        throw new ArgumentException($"{paramName} must be 'normal', a length/percentage, or var()/calc().", paramName);
    }

    /// <summary>Trims and lowercases a value; returns <c>null</c> if the input is <c>null</c> or whitespace.</summary>
    /// <param name="value">The input value.</param>
    /// <returns>Lowercased and trimmed string or <c>null</c>.</returns>
    public static string? NormalizeTrimToLower(string? value)
        => string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim().ToLowerInvariant();

    /// <summary>
    /// Returns <c>null</c> when the input is <c>null</c> or whitespace; otherwise returns the trimmed string.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <returns>Trimmed string or <c>null</c>.</returns>
    public static string? NormalizeWhitespaceToNull(string? value)
        => string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();

    /// <summary>
    /// Validates that a candidate value appears in the provided allowed set; returns the value when valid, or <c>null</c> if
    /// the candidate is <c>null</c>. Throws <see cref="ArgumentException" /> when invalid. This helper centralizes set-based
    /// validation for keyword-like properties.
    /// </summary>
    /// <param name="value">Candidate value (already normalized as needed by the caller).</param>
    /// <param name="allowed">Allowed set (typically provided by the style struct).</param>
    /// <param name="paramName">Parameter/property name used in exception messages.</param>
    /// <param name="allowedHint">Human-readable list used in exception messages.</param>
    /// <returns>
    /// The same <paramref name="value" /> when valid, or <c>null</c> if <paramref name="value" /> is <c>null</c>.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the value is not contained in <paramref name="allowed" />.</exception>
    public static string? ValidateFromSet(string? value, ISet<string> allowed, string paramName, string allowedHint)
    {
        if (value is null)
        {
            return null;
        }

        if (!allowed.Contains(value))
        {
            throw new ArgumentException($"{paramName} must be one of: {allowedHint}.", paramName);
        }

        return value;
    }
}
