using Allyaria.Theming.Abstractions;
using System.Text;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a normalized CSS <c>font-family</c> list. Accepts one or more raw family names (each may be
/// comma-separated), applies quoting rules per CSS (quote tokens that contain whitespace, commas, or quotes),
/// de-duplicates, trims, and produces a canonical comma-separated <see cref="StyleValueBase.Value" />.
/// </summary>
public sealed record AllyariaFontFamily : StyleValueBase
{
    /// <summary>
    /// Backing field for <see cref="Families" />. Caches the normalized font family array after the first access to avoid
    /// repeated parsing of the canonical <see cref="StyleValueBase.Value" />.
    /// </summary>
    private string[]? _families;

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaFontFamily" /> class from one or more raw family names. Any
    /// argument that contains commas is split into multiple names before normalization.
    /// </summary>
    /// <param name="families">One or more raw font family names. Items containing commas will be split into separate names.</param>
    public AllyariaFontFamily(params string[] families)
        : base(Normalize(families)) { }

    /// <summary>
    /// Gets the normalized font families as an array. This splits the canonical comma-separated
    /// <see cref="StyleValueBase.Value" /> on commas that are <b>outside</b> of quotes (so commas inside a quoted family name
    /// are ignored), then removes any surrounding double quotes from each item and unescapes inner quotes (i.e., <c>\"</c>
    /// becomes <c>"</c>). Returned items are unquoted, trimmed family names. The result is cached after the first access.
    /// </summary>
    public string[] Families
    {
        get
        {
            _families ??= SplitCanonicalFamilies(Value);

            return _families;
        }
    }

    /// <summary>
    /// Flattens a sequence of raw family strings, splitting any items that contain commas into separate entries, trimming
    /// whitespace from each resulting token, and removing empty results.
    /// </summary>
    /// <param name="families">The raw sequence of family strings (some entries may contain commas).</param>
    /// <returns>An array of tokens with commas expanded and whitespace trimmed.</returns>
    private static string[] FlattenCommaSeparated(IEnumerable<string> families)
    {
        var tokens = new List<string>();

        foreach (var family in families)
        {
            if (string.IsNullOrWhiteSpace(family))
            {
                continue;
            }

            var parts = family.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var p in parts)
            {
                if (!string.IsNullOrWhiteSpace(p))
                {
                    tokens.Add(p);
                }
            }
        }

        return tokens.ToArray();
    }

    /// <summary>
    /// Normalizes input provided via the <c>params</c> constructor: validates arguments, splits comma-separated items, quotes
    /// tokens when required by CSS rules, trims, and de-duplicates while preserving first-seen ordering.
    /// </summary>
    /// <param name="families">The raw family names supplied to the constructor.</param>
    /// <returns>A normalized comma-separated string of font family names (or <see cref="string.Empty" /> when none).</returns>
    private static string Normalize(string[]? families = null)
    {
        if (families is null || families.Length == 0)
        {
            return string.Empty;
        }

        var flattened = FlattenCommaSeparated(families);

        // Use case-insensitive de-dupe to align with typical CSS name matching semantics.
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var ordered = new List<string>(flattened.Length);

        foreach (var family in flattened)
        {
            var normalized = NormalizeQuotes(family.Trim());

            if (seen.Add(normalized))
            {
                ordered.Add(normalized);
            }
        }

        return string.Join(",", ordered);
    }

    /// <summary>
    /// Quotes a font-family token when necessary according to CSS rules. Tokens containing whitespace, commas, or quotes are
    /// wrapped in double quotes, with internal double-quotes escaped.
    /// </summary>
    /// <param name="family">A single font family name.</param>
    /// <returns>
    /// The possibly quoted family token. If <paramref name="family" /> is already wrapped in matching quotes, the outer quotes
    /// are stripped before canonicalization.
    /// </returns>
    private static string NormalizeQuotes(string family)
    {
        // If already wrapped in matching quotes, strip them before canonicalizing.
        if (family.Length >= 2 &&
            ((family[0] == '"' && family[^1] == '"') ||
                (family[0] == '\'' && family[^1] == '\'')))
        {
            family = family.Substring(1, family.Length - 2);
        }

        var hasWhitespace = family.IndexOfAny(
            new[]
            {
                ' ',
                '\t',
                '\r',
                '\n',
                '\f'
            }
        ) >= 0;

        var needsQuotes = hasWhitespace || family.Contains(',') || family.Contains('"') || family.Contains('\'');

        if (!needsQuotes)
        {
            return family;
        }

        var escaped = family.Replace("\"", "\\\"", StringComparison.Ordinal);

        return $"\"{escaped}\"";
    }

    /// <summary>
    /// Splits a canonical comma-joined value (no spaces) on commas that are outside of double quotes, then strips surrounding
    /// quotes and unescapes inner quotes. Assumes tokens were produced by <see cref="NormalizeQuotes" /> (i.e., double quotes
    /// used and inner quotes escaped as \" ).
    /// </summary>
    private static string[] SplitCanonicalFamilies(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Array.Empty<string>();
        }

        var items = new List<string>();
        var sb = new StringBuilder(value.Length);

        var inQuotes = false;
        var prev = '\0';

        foreach (var ch in value)
        {
            if (ch == ',' && !inQuotes)
            {
                items.Add(UnquoteAndUnescape(sb.ToString().Trim()));
                sb.Clear();
                prev = '\0';

                continue;
            }

            if (ch == '"' && prev != '\\')
            {
                inQuotes = !inQuotes;
                sb.Append(ch); // keep quotes for now; we'll remove them in UnquoteAndUnescape
            }
            else
            {
                sb.Append(ch);
            }

            prev = ch;
        }

        if (sb.Length > 0)
        {
            items.Add(UnquoteAndUnescape(sb.ToString().Trim()));
        }

        return items.ToArray();
    }

    /// <summary>Attempts to parse a raw font family list and normalize it into a canonical, comma-separated form.</summary>
    /// <param name="value">
    /// A raw font family string, which may contain one or more comma-separated family names. Individual tokens are trimmed;
    /// tokens requiring quotes per CSS rules will be quoted.
    /// </param>
    /// <param name="func">
    /// When this method returns, contains an <see cref="AllyariaFontFamily" /> whose <see cref="StyleValueBase.Value" /> is
    /// the normalized representation, or <see cref="string.Empty" /> if parsing fails (e.g., input is null/whitespace or
    /// normalizes to no tokens).
    /// </param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="value" /> was successfully normalized into a non-empty canonical form;
    /// otherwise <see langword="false" />.
    /// </returns>
    public static bool TryParse(string value, out AllyariaFontFamily func)
    {
        func = new AllyariaFontFamily(value);

        return !string.IsNullOrWhiteSpace(func.Value);
    }

    /// <summary>Removes surrounding double quotes if present and unescapes inner quotes (\" -> ").</summary>
    private static string UnquoteAndUnescape(string token)
    {
        if (token.Length >= 2 && token[0] == '"' && token[^1] == '"')
        {
            // strip surrounding quotes
            token = token.Substring(1, token.Length - 2);

            // unescape inner quotes
            token = token.Replace("\\\"", "\"", StringComparison.Ordinal);
        }

        return token;
    }

    /// <summary>Implicit conversion from <see langword="string[]" /> to <see cref="AllyariaFontFamily" />.</summary>
    /// <param name="families">The raw font family array to convert.</param>
    /// <returns>An <see cref="AllyariaFontFamily" /> created from <paramref name="families" />.</returns>
    public static implicit operator AllyariaFontFamily(string[] families) => new(families);

    /// <summary>
    /// Implicit conversion from <see langword="string" /> to <see cref="AllyariaFontFamily" />. Accepts a single
    /// comma-separated string of font family names.
    /// </summary>
    /// <param name="families">A single string containing one or more comma-separated font family names.</param>
    /// <returns>An <see cref="AllyariaFontFamily" /> created from <paramref name="families" />.</returns>
    public static implicit operator AllyariaFontFamily(string families) => new(families);

    /// <summary>
    /// Implicit conversion from <see cref="AllyariaFontFamily" /> to <see langword="string[]" /> (the normalized array).
    /// </summary>
    /// <param name="fontFamilyFamily">The <see cref="AllyariaFontFamily" /> instance.</param>
    /// <returns>The normalized font family array.</returns>
    public static implicit operator string[](AllyariaFontFamily fontFamilyFamily) => fontFamilyFamily.Families;

    /// <summary>
    /// Implicit conversion from <see cref="AllyariaFontFamily" /> to <see langword="string" /> (the normalized, comma-joined
    /// value).
    /// </summary>
    /// <param name="fontFamilyFamily">The <see cref="AllyariaFontFamily" /> instance.</param>
    /// <returns>The normalized font family list joined by commas with no spaces.</returns>
    public static implicit operator string(AllyariaFontFamily fontFamilyFamily) => fontFamilyFamily.Value;
}
