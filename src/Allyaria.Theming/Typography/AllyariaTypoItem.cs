using System.Globalization;

namespace Allyaria.Theming.Typography;

/// <summary>
/// Immutable, validated typography (non-color) settings that can render to CSS. All members are nullable and omitted from
/// CSS when null or whitespace.
/// </summary>
/// <remarks>
/// Validation occurs in the constructor. Keywords are trimmed and lowercased (except <see cref="FontFamily" /> items). For
/// lengths requiring units, a bare number assumes <c>px</c>. All numeric parsing uses
/// <see cref="CultureInfo.InvariantCulture" />.
/// </remarks>
public readonly record struct AllyariaTypoItem
{
    /// <summary>Allowed keyword set for <see cref="FontStyle" />.</summary>
    private static readonly HashSet<string> AllowedFontStyle = new(StringComparer.Ordinal)
    {
        "normal",
        "italic",
        "oblique"
    };

    /// <summary>Allowed keyword set for <see cref="TextAlign" />.</summary>
    private static readonly HashSet<string> AllowedTextAlign = new(StringComparer.Ordinal)
    {
        "left",
        "right",
        "center",
        "justify",
        "start",
        "end"
    };

    /// <summary>Allowed keyword set for <see cref="TextTransform" />.</summary>
    private static readonly HashSet<string> AllowedTextTransform = new(StringComparer.Ordinal)
    {
        "none",
        "capitalize",
        "uppercase",
        "lowercase"
    };

    /// <summary>
    /// Creates a validated, immutable instance. All parameters are optional (nullable). Invalid inputs throw
    /// <see cref="ArgumentException" />.
    /// </summary>
    /// <param name="fontFamily">Font family list; null or empty after filtering → omit <c>font-family</c>.</param>
    /// <param name="fontSize">Font size.</param>
    /// <param name="fontStyle">Font style.</param>
    /// <param name="fontWeight">Font weight.</param>
    /// <param name="letterSpacing">Letter spacing.</param>
    /// <param name="lineHeight">Line height.</param>
    /// <param name="textAlign">Text alignment.</param>
    /// <param name="textDecoration">Text decoration keywords.</param>
    /// <param name="textTransform">Text transform.</param>
    /// <param name="wordSpacing">Word spacing.</param>
    /// <exception cref="ArgumentException">Thrown when a value fails validation.</exception>
    public AllyariaTypoItem(string[]? fontFamily = null,
        string? fontSize = null,
        string? fontStyle = null,
        string? fontWeight = null,
        string? letterSpacing = null,
        string? lineHeight = null,
        string? textAlign = null,
        string? textDecoration = null,
        string? textTransform = null,
        string? wordSpacing = null)
    {
        // Normalize keywords (trim + lower) except font families.
        var fs = NormalizeTrimToLower(fontStyle);
        var fw = NormalizeTrimToLower(fontWeight);
        var la = NormalizeTrimToLower(textAlign);
        var td = NormalizeTrimToLower(textDecoration);
        var tt = NormalizeTrimToLower(textTransform);

        // Validate & normalize each.
        var normalizedFontFamily = NormalizeFontFamily(fontFamily);
        var normalizedFontSize = NormalizeFontSize(fontSize);
        var normalizedFontStyle = ValidateFromSet(fs, AllowedFontStyle, nameof(FontStyle), "normal,italic,oblique");
        var normalizedFontWeight = NormalizeFontWeight(fw);
        var normalizedLetterSpacing = NormalizeTrack(letterSpacing, nameof(LetterSpacing));
        var normalizedLineHeight = NormalizeLineHeight(lineHeight);

        var normalizedTextAlign = ValidateFromSet(
            la, AllowedTextAlign, nameof(TextAlign), "left,right,center,justify,start,end"
        );

        var normalizedTextDecoration = NormalizeTextDecoration(td);

        var normalizedTextTransform = ValidateFromSet(
            tt, AllowedTextTransform, nameof(TextTransform), "none,capitalize,uppercase,lowercase"
        );

        var normalizedWordSpacing = NormalizeTrack(wordSpacing, nameof(WordSpacing));

        // Assign properties (init).
        FontFamily = normalizedFontFamily;
        FontSize = normalizedFontSize;
        FontStyle = normalizedFontStyle;
        FontWeight = normalizedFontWeight;
        LetterSpacing = normalizedLetterSpacing;
        LineHeight = normalizedLineHeight;
        TextAlign = normalizedTextAlign;
        TextDecoration = normalizedTextDecoration;
        TextTransform = normalizedTextTransform;
        WordSpacing = normalizedWordSpacing;
    }

    /// <summary>
    /// Canonicalized font families (quoted where needed, deduped, order-preserving). If empty, <c>font-family</c> is omitted.
    /// </summary>
    public string[]? FontFamily { get; init; }

    /// <summary>Font size (e.g., <c>16px</c>, <c>1rem</c>, <c>smaller</c>, <c>var(--fs)</c>, <c>calc(...)</c>).</summary>
    public string? FontSize { get; init; }

    /// <summary>Font style: <c>normal</c> | <c>italic</c> | <c>oblique</c>.</summary>
    public string? FontStyle { get; init; }

    /// <summary>
    /// Font weight: <c>normal</c> | <c>bold</c> | <c>lighter</c> | <c>bolder</c> | <c>100</c> … <c>900</c> (100-step).
    /// </summary>
    public string? FontWeight { get; init; }

    /// <summary>
    /// Letter spacing: <c>normal</c> or length (<c>px</c>/<c>em</c>/<c>rem</c>) or <c>var()</c>/<c>calc()</c>. Bare number →
    /// <c>px</c>.
    /// </summary>
    public string? LetterSpacing { get; init; }

    /// <summary>
    /// Line height: <c>normal</c> | unitless positive number | length | percentage | <c>var()</c>/<c>calc()</c>.
    /// </summary>
    public string? LineHeight { get; init; }

    /// <summary>
    /// Text alignment: <c>left</c> | <c>right</c> | <c>center</c> | <c>justify</c> | <c>start</c> | <c>end</c>.
    /// </summary>
    public string? TextAlign { get; init; }

    /// <summary>
    /// Text decoration keywords: space-separated subset of { <c>none</c>, <c>underline</c>, <c>overline</c>,
    /// <c>line-through</c> }.
    /// </summary>
    public string? TextDecoration { get; init; }

    /// <summary>Text transform: <c>none</c> | <c>capitalize</c> | <c>uppercase</c> | <c>lowercase</c>.</summary>
    public string? TextTransform { get; init; }

    /// <summary>
    /// Word spacing: <c>normal</c> or length (<c>px</c>/<c>em</c>/<c>rem</c>) or <c>var()</c>/<c>calc()</c>. Bare number →
    /// <c>px</c>.
    /// </summary>
    public string? WordSpacing { get; init; }

    /// <summary>Appends a CSS declaration to a list if the value is not null/whitespace.</summary>
    /// <param name="parts">The accumulating list of CSS declarations.</param>
    /// <param name="name">The CSS property name.</param>
    /// <param name="value">The CSS value.</param>
    internal static void Append(List<string> parts, string name, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            parts.Add($"{name}:{value};");
        }
    }

    /// <summary>Determines if a string represents a generic CSS font-family keyword.</summary>
    /// <param name="s">The candidate family name.</param>
    /// <returns>True if <paramref name="s" /> is a generic family; otherwise false.</returns>
    internal static bool IsGenericFamily(string s)
        => s is "serif" or "sans-serif" or "monospace" or "cursive" or "fantasy" or "system-ui"
            or "ui-serif" or "ui-sans-serif" or "ui-monospace" or "emoji" or "math" or "fangsong";

    /// <summary>Determines whether the given value is a length token (px|em|rem|%).</summary>
    /// <param name="s">The token.</param>
    /// <returns>True if length-like; otherwise false.</returns>
    internal static bool IsLength(string s)
        => s.EndsWith("px", StringComparison.Ordinal) ||
            s.EndsWith("em", StringComparison.Ordinal) ||
            s.EndsWith("rem", StringComparison.Ordinal) ||
            s.EndsWith("%", StringComparison.Ordinal);

    /// <summary>Determines whether the given value is numeric using <see cref="CultureInfo.InvariantCulture" />.</summary>
    /// <param name="s">The token.</param>
    /// <returns>True if numeric; otherwise false.</returns>
    internal static bool IsNumeric(string s)
        => double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out _);

    /// <summary>Determines whether the given value is a unitless positive number (line-height friendly).</summary>
    /// <param name="s">The token.</param>
    /// <returns>True if unitless positive; otherwise false.</returns>
    internal static bool IsUnitlessPositive(string s)
        => double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var d) && d > 0;

    /// <summary>Returns true if the token is a <c>var(…)</c> or <c>calc(…)</c> expression.</summary>
    /// <param name="s">The token.</param>
    /// <returns>True if var/calc; otherwise false.</returns>
    internal static bool IsVarOrCalc(string? s)
        => s is not null && (s.StartsWith("var(", StringComparison.Ordinal) ||
            s.StartsWith("calc(", StringComparison.Ordinal));

    /// <summary>
    /// Normalizes a font-family array: trims, quotes where needed, dedupes, preserves order; returns null if none remain.
    /// </summary>
    /// <param name="families">Input families.</param>
    /// <returns>Canonical families or null.</returns>
    internal static string[]? NormalizeFontFamily(string[]? families)
    {
        if (families is null || families.Length == 0)
        {
            return null;
        }

        var seen = new HashSet<string>(StringComparer.Ordinal);
        var result = new List<string>(families.Length);

        foreach (var raw in families)
        {
            if (string.IsNullOrWhiteSpace(raw))
            {
                continue;
            }

            var trimmed = raw.Trim();

            var canonical = IsGenericFamily(trimmed)
                ? trimmed
                : QuoteIfNeeded(trimmed);

            if (seen.Add(canonical))
            {
                result.Add(canonical);
            }
        }

        return result.Count > 0
            ? result.ToArray()
            : null;
    }

    /// <summary>
    /// Normalizes <see cref="FontSize" /> to a keyword, length, percentage, var()/calc(), or px-added bare number.
    /// </summary>
    /// <param name="value">Input value.</param>
    /// <returns>Canonical font-size or null.</returns>
    /// <exception cref="ArgumentException">When invalid.</exception>
    internal static string? NormalizeFontSize(string? value)
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

        var keywordSizes = new HashSet<string>(StringComparer.Ordinal)
        {
            "xx-small",
            "x-small",
            "small",
            "medium",
            "large",
            "x-large",
            "xx-large",
            "smaller",
            "larger"
        };

        if (keywordSizes.Contains(lower))
        {
            return lower;
        }

        if (IsLength(lower))
        {
            return lower;
        }

        if (IsNumeric(lower))
        {
            return string.Create(CultureInfo.InvariantCulture, $"{lower}px");
        }

        throw new ArgumentException(
            "FontSize must be a length (px|em|rem|%) or CSS size keyword (xx-small..larger) or var()/calc().",
            nameof(FontSize)
        );
    }

    /// <summary>Normalizes <see cref="FontWeight" /> to keyword or 100-step numeric within 100..900.</summary>
    /// <param name="value">Input value.</param>
    /// <returns>Canonical font-weight or null.</returns>
    /// <exception cref="ArgumentException">When invalid.</exception>
    internal static string? NormalizeFontWeight(string? value)
    {
        if (value is null)
        {
            return null;
        }

        var v = value.Trim().ToLowerInvariant();

        if (v is "normal" or "bold" or "lighter" or "bolder")
        {
            return v;
        }

        if (int.TryParse(v, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) &&
            n % 100 == 0 && n >= 100 && n <= 900)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        throw new ArgumentException(
            "FontWeight must be normal|bold|lighter|bolder or a multiple of 100 between 100 and 900.",
            nameof(FontWeight)
        );
    }

    /// <summary>
    /// Normalizes <see cref="LineHeight" /> to 'normal', unitless positive, length, percentage, or var()/calc().
    /// </summary>
    /// <param name="value">Input value.</param>
    /// <returns>Canonical line-height or null.</returns>
    /// <exception cref="ArgumentException">When invalid or negative.</exception>
    internal static string? NormalizeLineHeight(string? value)
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
            return double.Parse(lower, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
        }

        if (IsLength(lower))
        {
            if (lower.StartsWith("-", StringComparison.Ordinal))
            {
                throw new ArgumentException("LineHeight must not be negative.", nameof(LineHeight));
            }

            return lower;
        }

        throw new ArgumentException(
            "LineHeight must be 'normal', a positive unitless number, a length (px|em|rem|%), or var()/calc().",
            nameof(LineHeight)
        );
    }

    /// <summary>Normalizes <see cref="TextDecoration" /> from a space-separated subset of allowed tokens.</summary>
    /// <param name="value">Input value.</param>
    /// <returns>Canonical text-decoration or null.</returns>
    /// <exception cref="ArgumentException">When invalid or 'none' is combined.</exception>
    internal static string? NormalizeTextDecoration(string? value)
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

        var allowed = new HashSet<string>(StringComparer.Ordinal)
        {
            "none",
            "underline",
            "overline",
            "line-through"
        };

        if (tokens.Any(t => !allowed.Contains(t)))
        {
            throw new ArgumentException(
                "TextDecoration allows only: none, underline, overline, line-through.", nameof(TextDecoration)
            );
        }

        if (tokens.Length > 1 && tokens.Contains("none"))
        {
            throw new ArgumentException(
                "TextDecoration 'none' cannot be combined with other values.", nameof(TextDecoration)
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

    /// <summary>
    /// Normalizes tracking properties (<see cref="LetterSpacing" /> / <see cref="WordSpacing" />). Accepts 'normal', lengths
    /// (px|em|rem), var()/calc(), or bare number (→ px).
    /// </summary>
    /// <param name="value">Input value.</param>
    /// <param name="paramName">Parameter name for error messages.</param>
    /// <returns>Canonical value or null.</returns>
    /// <exception cref="ArgumentException">When invalid.</exception>
    internal static string? NormalizeTrack(string? value, string paramName)
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

        if (IsLength(lower))
        {
            return lower;
        }

        if (IsNumeric(lower))
        {
            return string.Create(CultureInfo.InvariantCulture, $"{lower}px");
        }

        throw new ArgumentException($"{paramName} must be 'normal', a length (px|em|rem), or var()/calc().", paramName);
    }

    /// <summary>Lowercases and trims a string value, returning null if null/whitespace.</summary>
    /// <param name="value">Input value.</param>
    /// <returns>Lowercased, trimmed value or null.</returns>
    internal static string? NormalizeTrimToLower(string? value)
        => string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim().ToLowerInvariant();

    /// <summary>
    /// Quotes a font family name when needed (spaces, commas, or quotes present). Uses double quotes and escapes inner double
    /// quotes.
    /// </summary>
    /// <param name="s">The family name.</param>
    /// <returns>Possibly quoted family name.</returns>
    internal static string QuoteIfNeeded(string s)
    {
        var needsQuotes = s.Any(char.IsWhiteSpace) || s.Contains(',') || s.Contains('"') || s.Contains('\'');

        if (!needsQuotes)
        {
            return s;
        }

        var escaped = s.Replace("\"", "\\\"", StringComparison.Ordinal);

        return $"\"{escaped}\"";
    }

    /// <summary>
    /// Produces a single-line CSS declaration string in fixed order, skipping null/whitespace properties. Format:
    /// <c>prop:value;prop:value;</c> (no spaces around <c>:</c> or <c>;</c>).
    /// </summary>
    /// <returns>CSS declarations string.</returns>
    internal string ToCss()
    {
        var parts = new List<string>(10);

        // Order: font-family;font-size;font-weight;line-height;font-style;text-align;letter-spacing;word-spacing;text-transform;text-decoration
        if (FontFamily is
        {
            Length: > 0
        })
        {
            parts.Add($"font-family:{string.Join(",", FontFamily)};");
        }

        Append(parts, "font-size", FontSize);
        Append(parts, "font-weight", FontWeight);
        Append(parts, "line-height", LineHeight);
        Append(parts, "font-style", FontStyle);
        Append(parts, "text-align", TextAlign);
        Append(parts, "letter-spacing", LetterSpacing);
        Append(parts, "word-spacing", WordSpacing);
        Append(parts, "text-transform", TextTransform);
        Append(parts, "text-decoration", TextDecoration);

        return string.Concat(parts);
    }

    /// <summary>Validates that a value exists in an allowed set; returns the value or throws.</summary>
    /// <param name="value">Candidate value (already trimmed/lowercased).</param>
    /// <param name="allowed">Allowed set.</param>
    /// <param name="param">Parameter name.</param>
    /// <param name="allowedHint">Comma-separated hint for exception text.</param>
    /// <returns>The validated value or null.</returns>
    /// <exception cref="ArgumentException">When the value is not allowed.</exception>
    internal static string? ValidateFromSet(string? value, HashSet<string> allowed, string param, string allowedHint)
    {
        if (value is null)
        {
            return null;
        }

        if (!allowed.Contains(value))
        {
            throw new ArgumentException($"{param} must be one of: {allowedHint}.", param);
        }

        return value;
    }
}
