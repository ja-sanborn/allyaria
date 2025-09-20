namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>font-family</c> CSS value.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> implements value-based equality and accepts a sequence of
/// font family names. Each name is normalized by trimming whitespace, splitting comma-separated lists, applying quotes
/// when necessary, and de-duplicating while preserving order.
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns <c>font-family:value1,value2,...;</c> (no spaces after commas).
/// <see cref="ToString" /> calls <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct AllyariaFontFamily : IEquatable<AllyariaFontFamily>
{
    /// <summary>Backing field for <see cref="Families" />.</summary>
    private readonly string[]? _families;

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaFontFamily" /> struct from one or more raw family names. This
    /// constructor accepts either a single comma-separated string, multiple strings, or an array of strings. Any string
    /// containing commas will be split into multiple family names.
    /// </summary>
    /// <param name="families">One or more raw font family names. Items that contain commas will be split into separate names.</param>
    public AllyariaFontFamily(params string[] families) => _families = Normalize(families);

    /// <summary>Gets the normalized font family array.</summary>
    public string[] Families => _families ?? Array.Empty<string>();

    /// <summary>Gets the normalized font family list joined with commas (no spaces).</summary>
    public string Value => string.Join(",", Families);

    /// <summary>Determines whether the specified object is equal to the current instance (value equality).</summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is AllyariaFontFamily other && Equals(other);

    /// <summary>
    /// Determines whether the specified <see cref="AllyariaFontFamily" /> is equal to the current instance (value equality).
    /// </summary>
    /// <param name="other">The other instance to compare.</param>
    /// <returns>
    /// <see langword="true" /> if both contain the same normalized sequence; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(AllyariaFontFamily other)
    {
        var a = Families;
        var b = other.Families;

        if (a.Length != b.Length)
        {
            return false;
        }

        for (var i = 0; i < a.Length; i++)
        {
            if (!string.Equals(a[i], b[i], StringComparison.Ordinal))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Flattens a sequence of raw family strings, splitting any items that contain commas into separate entries, trimming
    /// whitespace from each resulting token, and removing empty results.
    /// </summary>
    /// <param name="families">The raw sequence of family strings (some entries may contain commas).</param>
    internal static string[] FlattenCommaSeparated(IEnumerable<string> families)
    {
        if (families is null)
        {
            throw new ArgumentNullException(nameof(families));
        }

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

    /// <summary>Returns a hash code for this instance based on all normalized family names.</summary>
    /// <returns>A 32-bit signed hash code.</returns>
    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var family in Families)
        {
            hash.Add(family, StringComparer.Ordinal);
        }

        return hash.ToHashCode();
    }

    /// <summary>
    /// Normalizes input provided via the <c>params</c> constructor: validates arguments, splits comma-separated items, and
    /// applies canonicalization and de-duplication.
    /// </summary>
    /// <param name="families">The raw family names supplied to the constructor.</param>
    /// <returns>A normalized array of font family names.</returns>
    internal static string[] Normalize(string[]? families = null)
    {
        if (families is null || families.Length == 0)
        {
            return Array.Empty<string>();
        }

        var flattened = FlattenCommaSeparated(families);
        var seen = new HashSet<string>(StringComparer.Ordinal);
        var ordered = new List<string>(flattened.Length);

        foreach (var family in flattened)
        {
            if (string.IsNullOrWhiteSpace(family))
            {
                continue;
            }

            var normalized = NormalizeQuotes(family.Trim());

            if (seen.Add(normalized))
            {
                ordered.Add(normalized);
            }
        }

        return ordered.ToArray();
    }

    /// <summary>
    /// Quotes a font-family token when necessary according to CSS rules: tokens containing whitespace, commas, or quotes are
    /// wrapped in double quotes, with inner double-quotes escaped.
    /// </summary>
    /// <param name="family">A single font family name.</param>
    /// <returns>The possibly quoted family token.</returns>
    internal static string NormalizeQuotes(string family)
    {
        if (string.IsNullOrEmpty(family))
        {
            return family;
        }

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
    /// Generates a CSS declaration in the form <c>font-family:value1,value2,...;</c> (no spaces after commas).
    /// </summary>
    /// <returns>The CSS declaration for the font families.</returns>
    public string ToCss()
        => Families.Length == 0
            ? string.Empty
            : $"font-family:{Value};";

    /// <summary>Returns the same output as <see cref="ToCss" />.</summary>
    /// <returns>The CSS declaration string.</returns>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaFontFamily" /> using value equality.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(AllyariaFontFamily left, AllyariaFontFamily right) => left.Equals(right);

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
    /// <param name="fontFamily">The <see cref="AllyariaFontFamily" /> instance.</param>
    /// <returns>The normalized font family array.</returns>
    public static implicit operator string[](AllyariaFontFamily fontFamily) => fontFamily.Families;

    /// <summary>
    /// Implicit conversion from <see cref="AllyariaFontFamily" /> to <see langword="string" /> (the normalized, comma-joined
    /// value).
    /// </summary>
    /// <param name="fontFamily">The <see cref="AllyariaFontFamily" /> instance.</param>
    /// <returns>The normalized font family list joined by commas with no spaces.</returns>
    public static implicit operator string(AllyariaFontFamily fontFamily) => fontFamily.Value;

    /// <summary>Inequality operator for <see cref="AllyariaFontFamily" /> using value equality.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(AllyariaFontFamily left, AllyariaFontFamily right) => !left.Equals(right);
}
