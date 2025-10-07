using Allyaria.Theming.Contracts;
using Allyaria.Theming.Enumerations;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a normalized numeric value with an optional CSS length unit. Input is validated and canonicalized (e.g.,
/// <c>"12px"</c>, <c>"-3.5 rem"</c>, <c>"100%"</c>, <c>"42"</c>). The canonical string is exposed via
/// <see cref="ValueBase.Value" />, while <see cref="Number" /> provides the parsed <see cref="double" /> and
/// <see cref="LengthUnit" /> provides the parsed unit (or <c>null</c> when unitless).
/// </summary>
/// <remarks>
/// Parsing rules:
/// <list type="bullet">
///     <item>
///         <description>Optional sign (<c>+</c>/<c>-</c>), decimal allowed, invariant culture (dot decimal separator).</description>
///     </item>
///     <item>
///         <description>
///         Optional unit suffix matching <see cref="LengthUnits" /> <see cref="DescriptionAttribute" /> values
///         (case-insensitive), including <c>%</c> for <see cref="LengthUnits.Percent" />.
///         </description>
///     </item>
///     <item>
///         <description>Whitespace allowed around the number and between number and unit; removed during normalization.</description>
///     </item>
/// </list>
/// Any invalid input throws <see cref="AryArgumentException" />.
/// </remarks>
public sealed class AryNumberValue : ValueBase
{
    /// <summary>
    /// Compiled regex to parse an optional sign, a numeric literal, and an optional unit token. Examples: "12px", "-3.5 rem",
    /// "+.25em", "100%", "42".
    /// </summary>
    /// <remarks>
    /// Groups:
    /// <list type="bullet">
    ///     <item>
    ///         <term>num</term><description>Signed/unsigned floating-point number.</description>
    ///     </item>
    ///     <item>
    ///         <term>unit</term><description>Optional unit token (letters or <c>%</c>).</description>
    ///     </item>
    /// </list>
    /// A small timeout is used to guard against pathological inputs.
    /// </remarks>
    private static readonly Regex NumberWithUnitRegex = new(
        @"^\s*(?<num>[+\-]?(?:\d+(?:\.\d+)?|\.\d+))\s*(?<unit>[A-Za-z%]+)?\s*$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant,
        TimeSpan.FromMilliseconds(250)
    );

    /// <summary>
    /// Lookup of normalized unit tokens (via <see cref="DescriptionAttribute" /> on <see cref="LengthUnits" /> members) to
    /// their enum values. Keys are compared case-insensitively.
    /// </summary>
    private static readonly Dictionary<string, LengthUnits> UnitByToken = BuildUnitMap();

    /// <summary>
    /// Initializes a new instance of the <see cref="AryNumberValue" /> class by parsing and normalizing the provided input
    /// string. After normalization:
    /// </summary>
    /// <param name="value">The input string (e.g., <c>"12px"</c>, <c>"100%"</c>, <c>"-3.5"</c>, <c>"auto"</c>).</param>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, whitespace, contains control characters, fails numeric parsing,
    /// or uses an unsupported unit token.
    /// </exception>
    public AryNumberValue(string value)
        : base(Normalize(value, out var number, out var unit))
    {
        Number = number;
        LengthUnit = unit;
    }

    /// <summary>Gets the parsed length unit, or <c>null</c> when the input was unitless.</summary>
    public LengthUnits? LengthUnit { get; }

    /// <summary>Gets the parsed numeric value as a <see cref="double" /> using invariant-culture semantics.</summary>
    public double Number { get; }

    /// <summary>
    /// Builds a case-insensitive dictionary mapping enum description strings to <see cref="LengthUnits" /> values.
    /// </summary>
    /// <returns>The populated dictionary.</returns>
    private static Dictionary<string, LengthUnits> BuildUnitMap()
    {
        var dict = new Dictionary<string, LengthUnits>(StringComparer.OrdinalIgnoreCase);

        foreach (LengthUnits u in Enum.GetValues(typeof(LengthUnits)))
        {
            var desc = GetUnitDescription(u);

            if (!string.IsNullOrWhiteSpace(desc))
            {
                // Use lower-cased keys for robust lookup; preserve canonical casing in description.
                var key = desc.ToLowerInvariant();
                dict.TryAdd(key, u);
            }
        }

        return dict;
    }

    /// <summary>
    /// Retrieves the <see cref="DescriptionAttribute" /> text for a <see cref="LengthUnits" /> member (e.g., "px", "rem",
    /// "%"). Returns the enum name when no description is present (should not occur with current definitions).
    /// </summary>
    /// <param name="unit">The unit value.</param>
    /// <returns>The description text or enum name.</returns>
    private static string GetUnitDescription(LengthUnits unit)
    {
        var mi = typeof(LengthUnits).GetMember(unit.ToString());

        if (mi.Length > 0)
        {
            var attr = mi[0].GetCustomAttribute<DescriptionAttribute>(false);

            if (attr is not null && !string.IsNullOrWhiteSpace(attr.Description))
            {
                return attr.Description;
            }
        }

        return unit.ToString();
    }

    /// <summary>
    /// Normalizes and validates a raw CSS length string into a canonical form, while parsing out numeric and unit components.
    /// </summary>
    /// <param name="raw">
    /// The raw input string to normalize (examples: <c>"12px"</c>, <c>"100%"</c>, <c>"-3.5"</c>, <c>"auto"</c>).
    /// </param>
    /// <param name="number">
    /// Outputs the parsed numeric value as a <see cref="double" />. When the input is <c>"auto"</c>, this is set to <c>0</c>.
    /// </param>
    /// <param name="unit">
    /// Outputs the parsed <see cref="LengthUnits" /> value, or <c>null</c> if the input is unitless or <c>"auto"</c>.
    /// </param>
    /// <returns>
    /// A canonical string suitable for CSS:
    /// <list type="bullet">
    ///     <item>
    ///         <description><c>"auto"</c> when the input was <c>"auto"</c>.</description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         Otherwise, the normalized numeric value followed immediately by the canonical unit token (or nothing if
    ///         unitless). Examples: <c>"12px"</c>, <c>"100%"</c>, <c>"1.5rem"</c>, <c>"42"</c>.
    ///         </description>
    ///     </item>
    /// </list>
    /// </returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="raw" /> is <c>null</c>, whitespace, contains invalid characters, cannot be parsed as a
    /// finite number, or uses an unsupported unit token.
    /// </exception>
    private static string Normalize(string raw, out double number, out LengthUnits? unit)
    {
        var value = ValidateInput(raw);
        number = 0.0;
        unit = null;

        if (value.Equals("auto", StringComparison.OrdinalIgnoreCase))
        {
            return "auto";
        }

        var match = NumberWithUnitRegex.Match(value);

        if (!match.Success)
        {
            throw new AryArgumentException(
                "Invalid numeric format. Expected a number with an optional CSS unit (e.g., 12px, -3.5, 100%).",
                nameof(raw), raw
            );
        }

        var numText = match.Groups["num"].Value;

        var unitText = match.Groups["unit"].Success
            ? match.Groups["unit"].Value
            : null;

        if (!double.TryParse(numText, NumberStyles.Float, CultureInfo.InvariantCulture, out number) ||
            double.IsInfinity(number))
        {
            throw new AryArgumentException("Number is not a finite value.", nameof(raw), raw);
        }

        unit = null;

        var canonicalUnit = string.Empty;

        if (!string.IsNullOrEmpty(unitText))
        {
            if (!TryMapUnit(unitText, out var u, out canonicalUnit))
            {
                throw new AryArgumentException($"Unsupported unit token '{unitText}'.", nameof(raw), raw);
            }

            unit = u;
        }

        // Canonical number formatting: minimal yet stable invariant form.
        // Use "0.#######" to keep a few decimals while avoiding scientific notation;
        // fall back to "G17" for very small/large values.
        string numCanonical;
        var abs = Math.Abs(number);

        if (abs is > 0 and < 0.0001 || abs >= 1e6)
        {
            numCanonical = number.ToString("G17", CultureInfo.InvariantCulture);
        }
        else
        {
            numCanonical = number.ToString("0.#######", CultureInfo.InvariantCulture);
        }

        return string.Concat(numCanonical, canonicalUnit);
    }

    /// <summary>Parses the specified string into an <see cref="AryNumberValue" />.</summary>
    /// <param name="value">The input string to parse.</param>
    /// <returns>A new <see cref="AryNumberValue" /> instance.</returns>
    /// <exception cref="AryArgumentException">Thrown when <paramref name="value" /> cannot be parsed/validated.</exception>
    public static AryNumberValue Parse(string value) => new(value);

    /// <summary>
    /// Attempts to map a unit token (e.g., <c>"px"</c>, <c>"rem"</c>, <c>"%"</c>) to <see cref="LengthUnits" /> using
    /// <see cref="DescriptionAttribute" /> values. Returns the canonical case for the unit (as defined on the enum).
    /// </summary>
    /// <param name="token">The raw unit token.</param>
    /// <param name="unit">Outputs the matched unit.</param>
    /// <param name="canonical">Outputs the canonical unit string (from <see cref="DescriptionAttribute" />).</param>
    /// <returns><c>true</c> when mapping succeeds; otherwise <c>false</c>.</returns>
    private static bool TryMapUnit(string token, out LengthUnits unit, out string canonical)
    {
        if (UnitByToken.TryGetValue(token.Trim().ToLowerInvariant(), out var u))
        {
            unit = u;
            canonical = GetUnitDescription(u);

            return true;
        }

        unit = default(LengthUnits);
        canonical = string.Empty;

        return false;
    }

    /// <summary>Attempts to parse the specified string into an <see cref="AryNumberValue" />.</summary>
    /// <param name="value">The input string to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="AryNumberValue" /> if parsing succeeded; otherwise <c>null</c>
    /// .
    /// </param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(string value, out AryNumberValue? result)
    {
        try
        {
            result = new AryNumberValue(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Defines an implicit conversion from <see cref="string" /> to <see cref="AryNumberValue" />.</summary>
    /// <param name="value">The input string to convert.</param>
    /// <returns>A new <see cref="AryNumberValue" /> instance.</returns>
    /// <exception cref="AryArgumentException">Thrown when <paramref name="value" /> is invalid.</exception>
    public static implicit operator AryNumberValue(string value) => new(value);

    /// <summary>
    /// Defines an implicit conversion from <see cref="AryNumberValue" /> to <see cref="string" />, returning the canonical
    /// <see cref="ValueBase.Value" /> representation (e.g., <c>"12px"</c>, <c>"100%"</c>, <c>"-3.5"</c>).
    /// </summary>
    /// <param name="value">The instance to convert.</param>
    /// <returns>The canonical string form.</returns>
    public static implicit operator string(AryNumberValue value) => value.Value;
}
