using Allyaria.Theming.Abstractions;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a CSS <c>&lt;number&gt;</c>, <c>&lt;length&gt;</c>, or <c>&lt;percentage&gt;</c> value. Ensures
/// normalization according to CSS syntax rules and provides access to the numeric component.
/// </summary>
public sealed record AllyariaCssNumber : StyleValueBase
{
    /// <summary>
    /// Supported CSS length units (case-insensitive). Includes relative, viewport, container query, and absolute units.
    /// </summary>
    private static readonly string[] LengthUnits =
    {
        "cap",
        "cqb",
        "cqh",
        "cqi",
        "cqmax",
        "cqmin",
        "cqw",
        "ch",
        "cm",
        "dvb",
        "dvh",
        "dvi",
        "dvmax",
        "dvmin",
        "dvw",
        "em",
        "ex",
        "ic",
        "in",
        "lh",
        "lvb",
        "lvh",
        "lvi",
        "lvmax",
        "lvmin",
        "lvw",
        "mm",
        "pc",
        "pt",
        "px",
        "q",
        "rcap",
        "rch",
        "rem",
        "rex",
        "ric",
        "rlh",
        "vb",
        "vh",
        "vi",
        "vmax",
        "vmin",
        "vw",
        "svb",
        "svh",
        "svi",
        "svmax",
        "svmin",
        "svw"
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaCssNumber" /> class. Accepts a raw CSS string and normalizes it to
    /// a valid <c>&lt;number&gt;</c>, <c>&lt;length&gt;</c>, or <c>&lt;percentage&gt;</c> if possible.
    /// </summary>
    /// <param name="value">The raw CSS value to parse.</param>
    public AllyariaCssNumber(string value)
        : base(Normalize(value)) { }

    /// <summary>
    /// Gets the numeric portion of the value as a <see cref="decimal" />. Returns <c>0</c> when the value is invalid or cannot
    /// be parsed.
    /// </summary>
    public decimal Number
    {
        get
        {
            if (string.IsNullOrEmpty(Value))
            {
                return 0;
            }

            var match = Regex.Match(Value, @"^[+-]?\d+(\.\d+)?");

            return match.Success
                ? decimal.Parse(match.Value, CultureInfo.InvariantCulture)
                : 0;
        }
    }

    /// <summary>Determines whether the input is a valid CSS length with a supported unit.</summary>
    /// <param name="value">The candidate string to test.</param>
    /// <returns>
    /// <see langword="true" /> if the string ends with a supported unit and has a valid numeric prefix; otherwise
    /// <see langword="false" />.
    /// </returns>
    private static bool IsLength(string value)
        => LengthUnits.Any(u =>
            value.EndsWith(u, StringComparison.Ordinal) &&
            value.Length > u.Length &&
            double.TryParse(value[..^u.Length], NumberStyles.Float, CultureInfo.InvariantCulture, out _)
        );

    /// <summary>Determines whether the input is a plain numeric value.</summary>
    /// <param name="value">The candidate string to test.</param>
    /// <returns><see langword="true" /> if the string is a valid number; otherwise <see langword="false" />.</returns>
    private static bool IsNumeric(string value)
        => double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out _);

    /// <summary>Determines whether the input is a percentage value.</summary>
    /// <param name="value">The candidate string to test.</param>
    /// <returns><see langword="true" /> if the string is a valid percentage; otherwise <see langword="false" />.</returns>
    private static bool IsPercentage(string value)
    {
        if (!value.EndsWith('%'))
        {
            return false;
        }

        var number = value[..^1];

        return double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>Determines whether the input contains only characters valid for numbers, percentages, or lengths.</summary>
    /// <param name="value">The candidate string to test.</param>
    /// <returns>
    /// <see langword="true" /> if the string matches the allowed character set; otherwise <see langword="false" />.
    /// </returns>
    private static bool IsValid(string value) => Regex.IsMatch(value, @"^[A-Za-z0-9%+.\-]+$");

    /// <summary>
    /// Normalizes a raw CSS number, percentage, or length value. Returns <see cref="string.Empty" /> if invalid.
    /// </summary>
    /// <param name="value">The raw CSS value to normalize.</param>
    /// <returns>A normalized string or <see cref="string.Empty" /> when invalid.</returns>
    private static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var trimmedValue = value.Trim();

        if (!IsValid(trimmedValue))
        {
            return string.Empty;
        }

        if (IsNumeric(trimmedValue) || IsPercentage(trimmedValue))
        {
            return trimmedValue;
        }

        trimmedValue = trimmedValue.ToLowerInvariant();

        if (IsLength(trimmedValue))
        {
            return trimmedValue;
        }

        return string.Empty;
    }

    /// <summary>Attempts to parse and normalize a CSS number, percentage, or length value.</summary>
    /// <param name="value">The raw CSS value to parse.</param>
    /// <param name="number">
    /// When this method returns, contains an <see cref="AllyariaCssNumber" /> whose <see cref="StyleValueBase.Value" /> is the
    /// normalized representation, or <see cref="string.Empty" /> if parsing fails.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="value" /> was successfully normalized into a non-empty canonical form;
    /// otherwise <see langword="false" />.
    /// </returns>
    public static bool TryParse(string value, out AllyariaCssNumber number)
    {
        number = new AllyariaCssNumber(value);

        return !string.IsNullOrWhiteSpace(number.Value);
    }

    /// <summary>Implicitly converts a <see cref="string" /> to an <see cref="AllyariaCssNumber" />.</summary>
    /// <param name="value">The raw CSS value to normalize.</param>
    /// <returns>An <see cref="AllyariaCssNumber" /> representing the normalized value (or empty when invalid).</returns>
    public static implicit operator AllyariaCssNumber(string value) => new(value);

    /// <summary>
    /// Implicitly converts an <see cref="AllyariaCssNumber" /> to its normalized <see cref="string" /> representation.
    /// </summary>
    /// <param name="number">The instance to convert.</param>
    /// <returns>
    /// The normalized string value or <see cref="string.Empty" /> when <paramref name="number" /> is <c>null</c>.
    /// </returns>
    public static implicit operator string(AllyariaCssNumber? number) => number?.Value ?? string.Empty;
}
