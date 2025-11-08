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
        pattern: "^[a-z][A-Za-z0-9]*$", options: RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    /// <summary>
    /// Regular expression used to validate kebab-case identifiers: must start with an ASCII letter and then contain only ASCII
    /// letters, digits, or hyphens.
    /// </summary>
    /// <remarks>Pattern: <c>^[A-Za-z][A-Za-z0-9-]*$</c></remarks>
    private static readonly Regex KebabCaseIdentifierRegex = new(
        pattern: "^[A-Za-z][A-Za-z0-9-]*$", options: RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    /// <summary>
    /// Regular expression used to validate PascalCase identifiers: must start with an uppercase ASCII letter and contain only
    /// ASCII letters and digits.
    /// </summary>
    /// <remarks>Pattern: <c>^[A-Z][A-Za-z0-9]*$</c></remarks>
    private static readonly Regex PascalCaseIdentifierRegex = new(
        pattern: "^[A-Z][A-Za-z0-9]*$", options: RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    /// <summary>
    /// Regular expression used to validate snake_case identifiers: must start with an ASCII letter and then contain only ASCII
    /// letters, digits, or underscores.
    /// </summary>
    /// <remarks>Pattern: <c>^[A-Za-z][A-Za-z0-9_]*$</c></remarks>
    private static readonly Regex SnakeCaseIdentifierRegex = new(
        pattern: "^[A-Za-z][A-Za-z0-9_]*$", options: RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    /// <summary>
    /// Produces a word whose first character is uppercase and remaining characters are lowercase, honoring the specified
    /// culture's casing rules.
    /// </summary>
    public static string Capitalize(this string? word, CultureInfo? culture = null)
    {
        if (string.IsNullOrEmpty(value: word))
        {
            return string.Empty;
        }

        var ci = culture ?? CultureInfo.InvariantCulture;

        return word.Length == 1
            ? char.ToUpper(c: word[index: 0], culture: ci).ToString(provider: ci)
            : char.ToUpper(c: word[index: 0], culture: ci) + word[1..].ToLower(culture: ci);
    }

    /// <summary>
    /// Builds a compiled regular expression that matches one or more occurrences of the supplied separator character, for
    /// collapsing into a single instance.
    /// </summary>
    /// <remarks>Example: for <c>'-'</c>, the effective pattern is <c>"-+"</c>; for <c>'_'</c>, <c>"_+"</c>.</remarks>
    private static Regex CollapseSeparatorRegex(char replaceChar)
        => new(
            pattern: $"[{Regex.Escape(str: replaceChar.ToString())}]+",
            options: RegexOptions.Compiled | RegexOptions.CultureInvariant
        );

    /// <summary>Converts a camelCase identifier into a human-readable string with spaces.</summary>
    /// <exception cref="AryArgumentException">Thrown when the trimmed input is not a valid camelCase identifier.</exception>
    public static string FromCamelCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return string.Empty;
        }

        var text = value.Trim();
        var argName = nameof(value);

        AryGuard.When(
            condition: !CamelCaseIdentifierRegex.IsMatch(input: text),
            argName: argName,
            message:
            $"{argName} must be a camelCase identifier (start with a lowercase letter; letters and digits only)"
        );

        return SplitConcatenated(value: text);
    }

    /// <summary>Converts a kebab-case identifier into a human-readable string with spaces.</summary>
    /// <exception cref="AryArgumentException">Thrown when the trimmed input is not a valid kebab-case identifier.</exception>
    public static string FromKebabCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return string.Empty;
        }

        var text = value.Trim();
        var argName = nameof(value);

        AryGuard.When(
            condition: !KebabCaseIdentifierRegex.IsMatch(input: text),
            argName: argName,
            message:
            $"{argName} must be a kebab-case identifier (start with a letter; letters, digits, and hyphens only)"
        );

        return ReplaceAndCollapseSeparators(value: text, replaceChar: '-');
    }

    /// <summary>Converts a PascalCase identifier into a human-readable string with spaces.</summary>
    /// <exception cref="AryArgumentException">Thrown when the trimmed input is not a valid PascalCase identifier.</exception>
    public static string FromPascalCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return string.Empty;
        }

        var text = value.Trim();
        var argName = nameof(value);

        AryGuard.When(
            condition: !PascalCaseIdentifierRegex.IsMatch(input: text),
            argName: argName,
            message:
            $"{argName} must be a PascalCase identifier (start with an uppercase letter; letters and digits only)"
        );

        return SplitConcatenated(value: text);
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
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return string.Empty;
        }

        var core = value.Trim().TrimStart('_', '-');
        var argName = nameof(value);

        AryGuard.When(
            condition: core.Length is 0,
            argName: argName,
            message: $"{argName} cannot be reduced to a valid identifier."
        );

        if (PascalCaseIdentifierRegex.IsMatch(input: core))
        {
            return FromPascalCase(value: core);
        }

        if (CamelCaseIdentifierRegex.IsMatch(input: core))
        {
            return FromCamelCase(value: core);
        }

        if (SnakeCaseIdentifierRegex.IsMatch(input: core))
        {
            return FromSnakeCase(value: core);
        }

        if (KebabCaseIdentifierRegex.IsMatch(input: core))
        {
            return FromKebabCase(value: core);
        }

        throw new AryArgumentException(
            message:
            "Input must be PascalCase, camelCase, snake_case, or kebab-case (with optional leading '_' or '-').",
            argName: nameof(value),
            argValue: value
        );
    }

    /// <summary>Converts a snake_case identifier into a human-readable string with spaces.</summary>
    /// <exception cref="AryArgumentException">Thrown when the trimmed input is not a valid snake_case identifier.</exception>
    public static string FromSnakeCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return string.Empty;
        }

        var text = value.Trim();
        var argName = nameof(value);

        AryGuard.When(
            condition: !SnakeCaseIdentifierRegex.IsMatch(input: text),
            argName: argName,
            message:
            $"{argName} must be a snake_case identifier (start with a letter; letters, digits, and underscores only)"
        );

        return ReplaceAndCollapseSeparators(value: text, replaceChar: '_');
    }

    /// <summary>Removes diacritic marks (accents) from a string.</summary>
    public static string NormalizeAccents(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return string.Empty;
        }

        var normalized = value.Normalize(normalizationForm: NormalizationForm.FormD);
        var sb = new StringBuilder(capacity: normalized.Length);

        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(ch: c) != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(value: c);
            }
        }

        return sb.ToString().Normalize(normalizationForm: NormalizationForm.FormC);
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

        return CollapseSeparatorRegex(replaceChar: replaceChar).Replace(input: text, replacement: " ").Trim();
    }

    /// <summary>
    /// Inserts spaces at inferred word boundaries within a concatenated identifier, preserving acronyms (e.g.,
    /// <c>HTTPRequest</c> → <c>HTTP Request</c>).
    /// </summary>
    private static string SplitConcatenated(string value)
    {
        var sb = new StringBuilder(capacity: value.Length + 8);

        for (var i = 0; i < value.Length; i++)
        {
            var c = value[index: i];

            if (char.IsUpper(c: c) && i > 0)
            {
                var prev = value[index: i - 1];

                // Insert a space at boundaries:
                // 1) Prev is lowercase or digit (…aA… or …1A…)
                // 2) Current is uppercase and next is lowercase (…HTTPs… → "HTTP s")
                if (!char.IsUpper(c: prev) ||
                    (i + 1 < value.Length && char.IsLower(c: value[index: i + 1])))
                {
                    if (sb.Length > 0 && sb[^1] != ' ')
                    {
                        sb.Append(value: ' ');
                    }
                }
            }

            sb.Append(value: c);
        }

        return sb.ToString().Trim();
    }

    /// <summary>Converts a string into camelCase form (first letter lowercased, subsequent words capitalized).</summary>
    public static string ToCamelCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return string.Empty;
        }

        var text = value.Trim();

        // If there is no whitespace, infer token boundaries from concatenated identifiers (e.g., "XMLHttpRequest" -> "XML Http Request").
        var tokenSource = text.IndexOfAny(
            anyOf:
            [
                ' ',
                '\t',
                '\r',
                '\n'
            ]
        ) >= 0
            ? text
            : SplitConcatenated(value: text);

        var words = tokenSource.Split(
            separator:
            [
                ' ',
                '\t',
                '\r',
                '\n'
            ], options: StringSplitOptions.RemoveEmptyEntries
        );

        var sb = new StringBuilder(capacity: text.Length);

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
                if (w.All(predicate: char.IsUpper))
                {
                    sb.Append(value: w.ToLowerInvariant());
                }
                else
                {
                    sb.Append(value: char.ToLower(c: w[index: 0], culture: CultureInfo.InvariantCulture));

                    if (w.Length > 1)
                    {
                        sb.Append(value: w[1..].ToLower(culture: CultureInfo.InvariantCulture));
                    }
                }
            }
            else
            {
                // Subsequent tokens: TitleCase the lower-cased token (acronyms like HTTP -> Http).
                var lower = w.ToLower(culture: CultureInfo.InvariantCulture);
                sb.Append(value: char.ToUpper(c: lower[index: 0], culture: CultureInfo.InvariantCulture));

                if (lower.Length > 1)
                {
                    sb.Append(value: lower[1..]);
                }
            }
        }

        return sb.ToString();
    }

    /// <summary>Converts the current string into a normalized CSS-compatible name.</summary>
    /// <param name="name">The string to normalize into a CSS-compatible identifier.</param>
    /// <returns>A lowercase, hyphenated CSS-friendly name (e.g., <c>"Font_Size"</c> → <c>"font-size"</c>).</returns>
    public static string ToCssName(this string? name)
        => Regex.Replace(
                input: (name ?? string.Empty).Replace(oldChar: '_', newChar: '-'), pattern: @"[\s-]+", replacement: "-"
            ).Trim(trimChar: '-')
            .ToLowerInvariant();

    /// <summary>Converts a string into kebab-case form (words separated by hyphens, lowercased).</summary>
    public static string ToKebabCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return string.Empty;
        }

        var cleaned = value.Trim().NormalizeAccents();

        var withHyphens = Regex.Replace(
            input: cleaned,
            pattern: "\\s+",
            replacement: "-",
            options: RegexOptions.CultureInvariant
        );

        var collapsed = CollapseSeparatorRegex(replaceChar: '-').Replace(input: withHyphens, replacement: "-");

        return collapsed.ToLowerInvariant();
    }

    /// <summary>Converts a string into PascalCase, preserving acronyms.</summary>
    public static string ToPascalCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return string.Empty;
        }

        var words = value.Split(
            separator:
            [
                ' ',
                '\t',
                '\r',
                '\n'
            ], options: StringSplitOptions.RemoveEmptyEntries
        );

        var sb = new StringBuilder(capacity: value.Length);

        foreach (var word in words)
        {
            var normalized = word.NormalizeAccents().Trim();

            if (normalized.Length == 0)
            {
                continue;
            }

            sb.Append(
                value: normalized.All(predicate: char.IsUpper)
                    ? normalized
                    : Capitalize(word: normalized, culture: CultureInfo.InvariantCulture)
            );
        }

        return sb.ToString();
    }

    /// <summary>Converts a string into snake_case form (words separated by underscores, lowercased).</summary>
    public static string ToSnakeCase(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return string.Empty;
        }

        var cleaned = value.Trim().NormalizeAccents();

        var withUnderscores = Regex.Replace(
            input: cleaned,
            pattern: "\\s+",
            replacement: "_",
            options: RegexOptions.CultureInvariant
        );

        var collapsed = CollapseSeparatorRegex(replaceChar: '_').Replace(input: withUnderscores, replacement: "_");

        return collapsed.ToLowerInvariant();
    }
}
