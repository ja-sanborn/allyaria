using Allyaria.Theming.Abstractions;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a CSS <c>&lt;number&gt;</c>, <c>&lt;length&gt;</c>, or <c>&lt;percentage&gt;</c> value. Ensures
/// normalization according to CSS syntax rules and provides access to the numeric component.
/// </summary>
public sealed record AllyariaStyleNumber : StyleValueBase
{
    /// <summary>
    /// Supported CSS length units (case-insensitive). Includes relative, viewport, container query, and absolute units.
    /// </summary>
    private static readonly string[] LengthUnits =
    {
        "em",
        "rem",
        "ex",
        "rex",
        "cap",
        "rcap",
        "ch",
        "rch",
        "ic",
        "ric",
        "lh",
        "rlh",
        "vw",
        "vh",
        "vi",
        "vb",
        "vmin",
        "vmax",
        "svw",
        "svh",
        "svi",
        "svb",
        "svmin",
        "svmax",
        "lvw",
        "lvh",
        "lvi",
        "lvb",
        "lvmin",
        "lvmax",
        "dvw",
        "dvh",
        "dvi",
        "dvb",
        "dvmin",
        "dvmax",
        "cqw",
        "cqh",
        "cqi",
        "cqb",
        "cqmin",
        "cqmax",
        "px",
        "cm",
        "mm",
        "q",
        "in",
        "pc",
        "pt"
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaStyleNumber" /> class. Accepts a raw CSS string and normalizes it
    /// to a valid <c>&lt;number&gt;</c>, <c>&lt;length&gt;</c>, or <c>&lt;percentage&gt;</c> if possible.
    /// </summary>
    /// <param name="value">The raw CSS value to parse.</param>
    public AllyariaStyleNumber(string value)
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
    private static bool IsLength(string value)
        => LengthUnits.Any(u =>
            value.EndsWith(u, StringComparison.Ordinal) &&
            value.Length > u.Length &&
            double.TryParse(value[..^u.Length], NumberStyles.Float, CultureInfo.InvariantCulture, out _)
        );

    /// <summary>Determines whether the input is a plain numeric value.</summary>
    private static bool IsNumeric(string value)
        => double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out _);

    /// <summary>Determines whether the input is a percentage value.</summary>
    private static bool IsPercentage(string value)
    {
        if (!value.EndsWith('%'))
        {
            return false;
        }

        var number = value[..^1];

        return double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>Determines whether the input is a valid CSS number, percentage, or length value.</summary>
    private static bool IsValid(string value) => Regex.IsMatch(value, @"^[A-Za-z0-9%+.\-]+$");

    /// <summary>
    /// Normalizes a raw CSS number, percentage, or length value. Returns <see cref="string.Empty" /> if invalid.
    /// </summary>
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

    /// <summary>Implicitly converts a <see cref="string" /> to an <see cref="AllyariaStyleNumber" />.</summary>
    public static implicit operator AllyariaStyleNumber(string value) => new(value);

    /// <summary>
    /// Implicitly converts an <see cref="AllyariaStyleNumber" /> to its normalized <see cref="string" /> representation.
    /// </summary>
    public static implicit operator string(AllyariaStyleNumber? number) => number?.Value ?? string.Empty;
}
