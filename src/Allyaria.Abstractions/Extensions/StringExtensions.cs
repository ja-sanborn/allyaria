using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Allyaria.Abstractions.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="string" /> values, including PascalCase ⇄ spaced-words
/// conversion, culture-aware capitalization, diacritic (accent) removal, and case-style conversions (camel/snake/kebab).
/// </summary>
/// <remarks>All methods are null/whitespace-safe and return <see cref="string.Empty" /> when appropriate.</remarks>
public static class StringExtensions
{
    /// <summary>
    /// Regular expression used to validate camelCase identifiers: must start with a lowercase ASCII letter and contain only
    /// ASCII letters and digits.
    /// </summary>
    /// <remarks>Pattern: <c>^[a-z][A-Za-z0-9]*$</c></remarks>
    private static readonly Regex CamelCaseIdentifierRegex = new(
        "^[a-z][A-Za-z0-9]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    /// <summary>
    /// Regular expression used to validate kebab-case identifiers: must start with an ASCII letter and then contain only ASCII
    /// letters, digits, or hyphens.
    /// </summary>
    /// <remarks>Pattern: <c>^[A-Za-z][A-Za-z0-9-]*$</c></remarks>
    private static readonly Regex KebabCaseIdentifierRegex = new(
        "^[A-Za-z][A-Za-z0-9-]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    /// <summary>
    /// Regular expression used to validate PascalCase identifiers: must start with an uppercase ASCII letter and contain only
    /// ASCII letters and digits.
    /// </summary>
    /// <remarks>Pattern: <c>^[A-Z][A-Za-z0-9]*$</c></remarks>
    private static readonly Regex PascalCaseIdentifierRegex = new(
        "^[A-Z][A-Za-z0-9]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    /// <summary>
    /// Regular expression used to validate snake_case identifiers: must start with an ASCII letter and then contain only ASCII
    /// letters, digits, or underscores.
    /// </summary>
    /// <remarks>Pattern: <c>^[A-Za-z][A-Za-z0-9_]*$</c></remarks>
    private static readonly Regex SnakeCaseIdentifierRegex = new(
        "^[A-Za-z][A-Za-z0-9_]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    /// <summary>
    /// Produces a word whose first character is uppercase and remaining characters are lowercase, honoring the specified
    /// culture's casing rules.
    /// </summary>
    public static string Capitalize(this string? word, CultureInfo? culture = null)
    {
        if (string.IsNullOrEmpty(word))
        {
            return word ?? string.Empty;
        }

        var ci = culture ?? CultureInfo.InvariantCulture;

        return word.Length == 1
            ? char.ToUpper(word[0], ci).ToString(ci)
            : char.ToUpper(word[0], ci) + word[1..].ToLower(ci);
    }

    /// <summary>
    /// Builds a compiled regular expression that matches one or more occurrences of the supplied separator character, for
    /// collapsing into a single instance.
    /// </summary>
    /// <remarks>Example: for <c>'-'</c>, the effective pattern is <c>"-+"</c>; for <c>'_'</c>, <c>"_+"</c>.</remarks>
    private static Regex CollapseSeparatorRegex(char replaceChar)
        => new($"[{Regex.Escape(replaceChar.ToString())}]+", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    /// <summary>Converts a camelCase identifier into a human-readable string with spaces.</summary>
    /// <exception cref="AryArgumentException">Thrown when the trimmed input is not a valid camelCase identifier.</exception>
    public static string FromCamelCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var text = value.Trim();

        if (!CamelCaseIdentifierRegex.IsMatch(text))
        {
            throw new AryArgumentException(
                "must be a camelCase identifier (start with a lowercase letter; letters and digits only)",
                nameof(value),
                value
            );
        }

        return SplitConcatenated(text);
    }

    /// <summary>Converts a kebab-case identifier into a human-readable string with spaces.</summary>
    /// <exception cref="AryArgumentException">Thrown when the trimmed input is not a valid kebab-case identifier.</exception>
    public static string FromKebabCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var text = value.Trim();

        if (!KebabCaseIdentifierRegex.IsMatch(text))
        {
            throw new AryArgumentException(
                "must be a kebab-case identifier (start with a letter; letters, digits, and hyphens only)",
                nameof(value),
                value
            );
        }

        // Replace hyphens with single spaces (consecutive hyphens collapse to one space)
        return ReplaceAndCollapseSeparators(text, '-');
    }

    /// <summary>Converts a PascalCase identifier into a human-readable string with spaces.</summary>
    /// <exception cref="AryArgumentException">Thrown when the trimmed input is not a valid PascalCase identifier.</exception>
    public static string FromPascalCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var text = value.Trim();

        if (!PascalCaseIdentifierRegex.IsMatch(text))
        {
            throw new AryArgumentException(
                "must be a PascalCase identifier (start with an uppercase letter; letters and digits only)",
                nameof(value),
                value
            );
        }

        return SplitConcatenated(text);
    }

    /// <summary>
    /// Attempts to detect the naming convention of an identifier that may include an optional leading prefix (<c>_</c> or
    /// one/more <c>-</c>) and converts it into a human-readable string.
    /// </summary>
    /// <exception cref="AryArgumentException">
    /// Thrown when the trimmed input cannot be classified as PascalCase, camelCase, snake_case, or kebab-case.
    /// </exception>
    public static string FromPrefixedCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        // Trim whitespace and leading prefix chars
        var core = value.Trim().TrimStart('_', '-');

        if (core.Length == 0)
        {
            throw new AryArgumentException(
                "Input cannot be reduced to a valid identifier.",
                nameof(value),
                value
            );
        }

        if (PascalCaseIdentifierRegex.IsMatch(core))
        {
            return FromPascalCase(core);
        }

        if (CamelCaseIdentifierRegex.IsMatch(core))
        {
            return FromCamelCase(core);
        }

        if (SnakeCaseIdentifierRegex.IsMatch(core))
        {
            return FromSnakeCase(core);
        }

        if (KebabCaseIdentifierRegex.IsMatch(core))
        {
            return FromKebabCase(core);
        }

        throw new AryArgumentException(
            "Input must be PascalCase, camelCase, snake_case, or kebab-case (with optional leading '_' or '-').",
            nameof(value),
            value
        );
    }

    /// <summary>Converts a snake_case identifier into a human-readable string with spaces.</summary>
    /// <exception cref="AryArgumentException">Thrown when the trimmed input is not a valid snake_case identifier.</exception>
    public static string FromSnakeCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var text = value.Trim();

        if (!SnakeCaseIdentifierRegex.IsMatch(text))
        {
            throw new AryArgumentException(
                "must be a snake_case identifier (start with a letter; letters, digits, and underscores only)",
                nameof(value),
                value
            );
        }

        // Replace underscores with single spaces (consecutive underscores collapse to one space)
        return ReplaceAndCollapseSeparators(text, '_');
    }

    /// <summary>Removes diacritic marks (accents) from a string.</summary>
    public static string NormalizeAccents(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var normalized = value.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(normalized.Length);

        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Returns the provided string if it is not <c>null</c>; otherwise, returns the specified default value, or
    /// <see cref="string.Empty" /> if no default is provided.
    /// </summary>
    /// <param name="value">The string to evaluate.</param>
    /// <param name="defaultValue">
    /// The value to return when <paramref name="value" /> is <c>null</c>. If omitted, <see cref="string.Empty" /> is used.
    /// </param>
    /// <returns><paramref name="value" /> when it is not <c>null</c>; otherwise, <paramref name="defaultValue" />.</returns>
    public static string OrDefault(this string? value, string defaultValue = "") => value ?? defaultValue;

    /// <summary>
    /// Replaces all occurrences of a specified separator character in the input string with spaces, collapsing multiple
    /// consecutive separators into a single space.
    /// </summary>
    private static string ReplaceAndCollapseSeparators(string value, char replaceChar)
    {
        var text = value.Trim();

        return CollapseSeparatorRegex(replaceChar).Replace(text, " ").Trim();
    }

    /// <summary>
    /// Inserts spaces at inferred word boundaries within a concatenated identifier, preserving acronyms (e.g.,
    /// <c>HTTPRequest</c> → <c>HTTP Request</c>).
    /// </summary>
    private static string SplitConcatenated(string value)
    {
        var sb = new StringBuilder(value.Length + 8);

        for (var i = 0; i < value.Length; i++)
        {
            var c = value[i];

            if (char.IsUpper(c) && i > 0)
            {
                var prev = value[i - 1];

                // Insert a space at boundaries:
                // 1) Prev is lowercase or digit (…aA… or …1A…)
                // 2) Current is uppercase and next is lowercase (…HTTPs… → "HTTP s")
                if (!char.IsUpper(prev) ||
                    (i + 1 < value.Length && char.IsLower(value[i + 1])))
                {
                    if (sb.Length > 0 && sb[^1] != ' ')
                    {
                        sb.Append(' ');
                    }
                }
            }

            sb.Append(c);
        }

        return sb.ToString().Trim();
    }

    /// <summary>Converts a string into camelCase form (first letter lowercased, subsequent words capitalized).</summary>
    public static string ToCamelCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var text = value.Trim();

        // If there is no whitespace, infer token boundaries from concatenated identifiers (e.g., "XMLHttpRequest" -> "XML Http Request").
        var tokenSource = text.IndexOfAny(
            [
                ' ',
                '\t',
                '\r',
                '\n'
            ]
        ) >= 0
            ? text
            : SplitConcatenated(text);

        var words = tokenSource.Split(
            [
                ' ',
                '\t',
                '\r',
                '\n'
            ], StringSplitOptions.RemoveEmptyEntries
        );

        var sb = new StringBuilder(text.Length);

        for (var i = 0; i < words.Length; i++)
        {
            var w = words[i].NormalizeAccents().Trim();

            if (w.Length == 0)
            {
                continue;
            }

            if (i == 0)
            {
                // First token: if it's an acronym, lower it fully; otherwise lower first char and the rest.
                if (w.All(char.IsUpper))
                {
                    sb.Append(w.ToLowerInvariant());
                }
                else
                {
                    sb.Append(char.ToLower(w[0], CultureInfo.InvariantCulture));

                    if (w.Length > 1)
                    {
                        sb.Append(w[1..].ToLower(CultureInfo.InvariantCulture));
                    }
                }
            }
            else
            {
                // Subsequent tokens: TitleCase the lower-cased token (acronyms like HTTP -> Http).
                var lower = w.ToLower(CultureInfo.InvariantCulture);
                sb.Append(char.ToUpper(lower[0], CultureInfo.InvariantCulture));

                if (lower.Length > 1)
                {
                    sb.Append(lower[1..]);
                }
            }
        }

        return sb.ToString();
    }

    /// <summary>Converts a string into kebab-case form (words separated by hyphens, lowercased).</summary>
    public static string ToKebabCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var cleaned = value.Trim().NormalizeAccents();

        // Convert any whitespace to hyphens, then collapse runs of hyphens.
        var withHyphens = Regex.Replace(cleaned, "\\s+", "-", RegexOptions.CultureInvariant);
        var collapsed = CollapseSeparatorRegex('-').Replace(withHyphens, "-");

        return collapsed.ToLowerInvariant();
    }

    /// <summary>Converts a string into PascalCase, preserving acronyms.</summary>
    public static string ToPascalCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var words = value.Split(
            [
                ' ',
                '\t',
                '\r',
                '\n'
            ], StringSplitOptions.RemoveEmptyEntries
        );

        var sb = new StringBuilder(value.Length);

        foreach (var word in words)
        {
            var normalized = word.NormalizeAccents().Trim();

            if (normalized.Length == 0)
            {
                continue;
            }

            sb.Append(
                normalized.All(char.IsUpper)
                    ? normalized
                    : Capitalize(normalized, CultureInfo.InvariantCulture)
            );
        }

        return sb.ToString();
    }

    /// <summary>Converts a string into snake_case form (words separated by underscores, lowercased).</summary>
    public static string ToSnakeCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var cleaned = value.Trim().NormalizeAccents();

        // Convert any whitespace to underscores, then collapse runs of underscores.
        var withUnderscores = Regex.Replace(cleaned, "\\s+", "_", RegexOptions.CultureInvariant);
        var collapsed = CollapseSeparatorRegex('_').Replace(withUnderscores, "_");

        return collapsed.ToLowerInvariant();
    }
}
