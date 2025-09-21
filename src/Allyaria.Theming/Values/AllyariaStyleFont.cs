using Allyaria.Theming.Abstractions;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a normalized CSS <c>font-family</c> list. Accepts one or more raw family names (each may be
/// comma-separated), applies quoting rules per CSS (quote tokens that contain whitespace, commas, or quotes),
/// de-duplicates, trims, and produces a canonical comma-separated <see cref="StyleValueBase.Value" />.
/// </summary>
public sealed record AllyariaStyleFont : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaStyleFont" /> class from one or more raw family names. Any
    /// argument that contains commas is split into multiple names before normalization.
    /// </summary>
    /// <param name="families">One or more raw font family names. Items containing commas will be split into separate names.</param>
    public AllyariaStyleFont(params string[] families)
        : base(Normalize(families)) { }

    /// <summary>
    /// Gets the normalized font families as an array. This is derived by splitting the canonical comma-separated
    /// <see cref="Value" /> and trimming entries.
    /// </summary>
    public string[] Families
        => Value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    /// <summary>
    /// Flattens a sequence of raw family strings, splitting any items that contain commas into separate entries, trimming
    /// whitespace from each resulting token, and removing empty results.
    /// </summary>
    /// <param name="families">The raw sequence of family strings (some entries may contain commas).</param>
    /// <returns>An array of tokens with commas expanded and whitespace trimmed.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="families" /> is <c>null</c>.</exception>
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

    /// <summary>Implicit conversion from <see langword="string[]" /> to <see cref="AllyariaStyleFont" />.</summary>
    /// <param name="families">The raw font family array to convert.</param>
    /// <returns>An <see cref="AllyariaStyleFont" /> created from <paramref name="families" />.</returns>
    public static implicit operator AllyariaStyleFont(string[] families) => new(families);

    /// <summary>
    /// Implicit conversion from <see langword="string" /> to <see cref="AllyariaStyleFont" />. Accepts a single
    /// comma-separated string of font family names.
    /// </summary>
    /// <param name="families">A single string containing one or more comma-separated font family names.</param>
    /// <returns>An <see cref="AllyariaStyleFont" /> created from <paramref name="families" />.</returns>
    public static implicit operator AllyariaStyleFont(string families) => new(families);

    /// <summary>
    /// Implicit conversion from <see cref="AllyariaStyleFont" /> to <see langword="string[]" /> (the normalized array).
    /// </summary>
    /// <param name="fontFamily">The <see cref="AllyariaStyleFont" /> instance.</param>
    /// <returns>The normalized font family array.</returns>
    public static implicit operator string[](AllyariaStyleFont fontFamily) => fontFamily.Families;

    /// <summary>
    /// Implicit conversion from <see cref="AllyariaStyleFont" /> to <see langword="string" /> (the normalized, comma-joined
    /// value).
    /// </summary>
    /// <param name="fontFamily">The <see cref="AllyariaStyleFont" /> instance.</param>
    /// <returns>The normalized font family list joined by commas with no spaces.</returns>
    public static implicit operator string(AllyariaStyleFont fontFamily) => fontFamily.Value;
}
