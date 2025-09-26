using Allyaria.Theming.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a CSS <c>&lt;number&gt;</c>, <c>&lt;length&gt;</c>, or <c>&lt;percentage&gt;</c> value. Ensures
/// normalization according to CSS syntax rules and provides access to the numeric component.
/// </summary>
/// <remarks>
/// This type follows the <c>Parse/TryParse</c> pattern:
/// <list type="bullet">
///     <item>
///         <description>
///         <see cref="Parse(string)" /> and the implicit conversion from <see cref="string" /> throw on invalid input.
///         </description>
///     </item>
///     <item>
///         <description>
///         <see cref="TryParse(string, out AllyariaNumberValue?)" /> returns <see langword="false" /> on invalid input.
///         </description>
///     </item>
/// </list>
/// </remarks>
public sealed class AllyariaNumberValue : ValueBase
{
    /// <summary>
    /// Supported CSS length units (case-insensitive). Includes relative, viewport, container query, and absolute units.
    /// </summary>
    private static readonly HashSet<string> LengthUnits = new(
        [
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
        ],
        StringComparer.OrdinalIgnoreCase
    );

    /// <summary>
    /// Compiled regular expression that matches the leading numeric portion of a CSS number. Supports optional sign and a
    /// leading or trailing decimal part.
    /// </summary>
    /// <remarks>
    /// Pattern (case-insensitive, invariant): <c>^[+-]?(?:\d+(?:\.\d*)?|\.\d+)</c>. We deliberately exclude exponent notation
    /// from the capture to ensure reliable <see cref="decimal" /> parsing.
    /// </remarks>
    private static readonly Regex NumberPrefixRegex = new(
        @"^[+-]?(?:\d+(?:\.\d*)?|\.\d+)",
        RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaNumberValue" /> class. Accepts a raw CSS string and normalizes it
    /// to a valid <c>&lt;number&gt;</c>, <c>&lt;length&gt;</c>, or <c>&lt;percentage&gt;</c> if possible.
    /// </summary>
    /// <param name="value">The raw CSS value to parse.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is not a valid number, percentage, or length.</exception>
    public AllyariaNumberValue(string value)
        : base(Normalize(value)) { }

    /// <summary>
    /// Gets the numeric portion of the value as a <see cref="double" />. Returns <c>0.0</c> when the numeric portion is not
    /// present or cannot be parsed.
    /// </summary>
    public double Number
    {
        get
        {
            var match = NumberPrefixRegex.Match(Value);

            if (!match.Success)
            {
                return 0.0d;
            }

            return double.TryParse(match.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var dbl)
                ? dbl
                : 0.0d;
        }
    }

    /// <summary>Determines whether the input is a valid CSS length with a supported unit.</summary>
    /// <param name="value">The candidate string to test. Must be lowercase, trimmed.</param>
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
    /// <param name="value">The candidate string to test. Must be lowercase, trimmed.</param>
    /// <returns><see langword="true" /> if the string is a valid number; otherwise <see langword="false" />.</returns>
    private static bool IsNumeric(string value)
        => double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out _);

    /// <summary>Determines whether the input is a percentage value.</summary>
    /// <param name="value">The candidate string to test. Must be lowercase, trimmed.</param>
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
    /// <param name="value">The candidate string to test. Must be lowercase, trimmed.</param>
    /// <returns>
    /// <see langword="true" /> if the string matches the allowed character set; otherwise <see langword="false" />.
    /// </returns>
    private static bool IsValid(string value)
        => Regex.IsMatch(value, @"^[a-z0-9%+.\-]+$", RegexOptions.CultureInvariant);

    /// <summary>Normalizes a raw CSS number, percentage, or length value.</summary>
    /// <param name="value">The raw CSS value to normalize.</param>
    /// <returns>A normalized, lowercase string.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> is <see langword="null" />, whitespace, or not a valid number, percentage, or
    /// length.
    /// </exception>
    private static string Normalize(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        var trimmedValue = value.Trim().ToLowerInvariant();

        if (IsValid(trimmedValue) && (IsNumeric(trimmedValue) || IsPercentage(trimmedValue) || IsLength(trimmedValue)))
        {
            return trimmedValue;
        }

        throw new ArgumentException("Value is not a valid CSS number, percentage, or length.", nameof(value));
    }

    /// <summary>Parses a raw CSS value into an <see cref="AllyariaNumberValue" />.</summary>
    /// <param name="value">The raw CSS value to parse.</param>
    /// <returns>An instance containing the normalized value.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is invalid.</exception>
    public static AllyariaNumberValue Parse(string value) => new(value);

    /// <summary>Attempts to parse and normalize a CSS number, percentage, or length value.</summary>
    /// <param name="value">The raw CSS value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains an <see cref="AllyariaNumberValue" /> with the normalized representation, or
    /// <see langword="null" /> if parsing fails.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="value" /> was successfully normalized; otherwise <see langword="false" />.
    /// </returns>
    public static bool TryParse(string value, out AllyariaNumberValue? result)
    {
        try
        {
            result = new AllyariaNumberValue(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a <see cref="string" /> to an <see cref="AllyariaNumberValue" />.</summary>
    /// <param name="value">The raw CSS value to normalize.</param>
    /// <returns>An <see cref="AllyariaNumberValue" /> representing the normalized value.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is invalid.</exception>
    public static implicit operator AllyariaNumberValue(string value) => new(value);

    /// <summary>
    /// Implicitly converts an <see cref="AllyariaNumberValue" /> to its normalized <see cref="string" /> representation.
    /// </summary>
    /// <param name="number">The instance to convert.</param>
    /// <returns>
    /// The normalized string value, or <see cref="string.Empty" /> if <paramref name="number" /> is <see langword="null" />.
    /// </returns>
    public static implicit operator string(AllyariaNumberValue? number) => number?.Value ?? string.Empty;
}
