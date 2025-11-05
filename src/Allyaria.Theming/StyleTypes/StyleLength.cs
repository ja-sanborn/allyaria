namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS length value within the Allyaria theming system. Supports parsing numeric values with optional units
/// and exposes the normalized number and unit, while retaining the original string via <see cref="StyleValueBase.Value" />
/// .
/// </summary>
public sealed record StyleLength : StyleValueBase
{
    /// <summary>
    /// Regular expression used to parse numeric length values with optional units from an input string. Captures a signed
    /// floating-point number and an optional alphabetic or percent unit token.
    /// </summary>
    private static readonly Regex LengthWithUnitRegex = new(
        pattern: @"^\s*(?<num>[+\-]?(?:\d+(?:\.\d+)?|\.\d+))\s*(?<unit>[A-Za-z%]+)?\s*$",
        options: RegexOptions.Compiled | RegexOptions.CultureInvariant,
        matchTimeout: TimeSpan.FromMilliseconds(value: 250)
    );

    /// <summary>
    /// Lookup table mapping textual unit tokens (e.g., <c>px</c>, <c>em</c>, <c>%</c>) to <see cref="LengthUnits" /> values.
    /// The dictionary is built once from the <see cref="LengthUnits" /> descriptions and reused for parsing.
    /// </summary>
    private static readonly Dictionary<string, LengthUnits> UnitByToken = BuildUnitMap();

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleLength" /> record from the specified string value.
    /// </summary>
    /// <param name="value">
    /// The raw CSS length value to parse, which may contain a numeric component and an optional unit token.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="value" /> is non-empty and cannot be parsed as a valid length.
    /// </exception>
    public StyleLength(string value)
        : base(value: value)
    {
        if (string.IsNullOrWhiteSpace(value: Value))
        {
            return;
        }

        if (TryNormalizeLength(input: Value, number: out var number, unit: out var unit))
        {
            LengthUnit = unit;
            Number = number;
        }
        else
        {
            throw new ArgumentException(message: $"Invalid length: {Value}");
        }
    }

    /// <summary>
    /// Gets the parsed length unit, if one was specified and successfully mapped; otherwise <see langword="null" />.
    /// </summary>
    public LengthUnits? LengthUnit { get; }

    /// <summary>
    /// Gets the parsed numeric value of the length when parsing succeeds; otherwise the default value <c>0.0</c>.
    /// </summary>
    public double Number { get; }

    /// <summary>
    /// Builds a mapping from unit description strings to their corresponding <see cref="LengthUnits" /> values. The
    /// descriptions are obtained from the <see cref="LengthUnits" /> enumeration via its metadata.
    /// </summary>
    /// <returns>
    /// A case-insensitive dictionary keyed by unit token (e.g., <c>"px"</c>) with <see cref="LengthUnits" /> values.
    /// </returns>
    private static Dictionary<string, LengthUnits> BuildUnitMap()
    {
        var dict = new Dictionary<string, LengthUnits>(comparer: StringComparer.OrdinalIgnoreCase);

        foreach (var unit in Enum.GetValues<LengthUnits>())
        {
            var desc = unit.GetDescription();

            if (!string.IsNullOrWhiteSpace(value: desc))
            {
                var key = desc.ToLowerInvariant();
                dict.TryAdd(key: key, value: unit);
            }
        }

        return dict;
    }

    /// <summary>Parses the specified string into a <see cref="StyleLength" /> instance.</summary>
    /// <param name="value">The string to parse into a length value. If <see langword="null" />, an empty string is used.</param>
    /// <returns>A new <see cref="StyleLength" /> instance representing the parsed value.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="value" /> cannot be parsed as a valid
    /// length.
    /// </exception>
    public static StyleLength Parse(string? value) => new(value: value ?? string.Empty);

    /// <summary>Attempts to map a textual unit token to a <see cref="LengthUnits" /> value.</summary>
    /// <param name="token">The textual unit token to map (for example, <c>px</c>, <c>em</c>, or <c>%</c>).</param>
    /// <param name="unit">
    /// When this method returns, contains the mapped <see cref="LengthUnits" /> value if the token is recognized; otherwise,
    /// the default <see cref="LengthUnits" /> value.
    /// </param>
    /// <returns><see langword="true" /> if the token was successfully mapped; otherwise, <see langword="false" />.</returns>
    private static bool TryMapUnit(string token, out LengthUnits unit)
    {
        if (UnitByToken.TryGetValue(key: token.Trim().ToLowerInvariant(), value: out var unitParse))
        {
            unit = unitParse;

            return true;
        }

        unit = default(LengthUnits);

        return false;
    }

    /// <summary>
    /// Attempts to normalize an input length string into a numeric value and optional <see cref="LengthUnits" /> value.
    /// </summary>
    /// <param name="input">The input string containing a numeric length value and optional unit token.</param>
    /// <param name="number">
    /// When this method returns, contains the parsed numeric value if parsing succeeds; otherwise <c>0.0</c>.
    /// </param>
    /// <param name="unit">
    /// When this method returns, contains the parsed <see cref="LengthUnits" /> value if one was specified and recognized;
    /// otherwise <see langword="null" />.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the input could be successfully parsed into a number (and optional unit); otherwise,
    /// <see langword="false" />.
    /// </returns>
    private static bool TryNormalizeLength(string input, out double number, out LengthUnits? unit)
    {
        number = 0.0;
        unit = null;

        var match = LengthWithUnitRegex.Match(input: input);

        if (!match.Success)
        {
            return false;
        }

        var numText = match.Groups[groupname: "num"].Value;

        var unitText = match.Groups[groupname: "unit"].Success
            ? match.Groups[groupname: "unit"].Value
            : null;

        if (!double.TryParse(
            s: numText, style: NumberStyles.Float, provider: CultureInfo.InvariantCulture, result: out number
        ) || double.IsInfinity(d: number))
        {
            return false;
        }

        unit = null;

        if (!string.IsNullOrEmpty(value: unitText))
        {
            if (!TryMapUnit(token: unitText, unit: out var lengthUnit))
            {
                return false;
            }

            unit = lengthUnit;
        }

        return true;
    }

    /// <summary>Attempts to parse the specified string into a <see cref="StyleLength" /> instance.</summary>
    /// <param name="value">The string to parse into a length value.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleLength" /> instance or <see langword="null" /> if parsing
    /// failed.
    /// </param>
    /// <returns><see langword="true" /> if the value was successfully parsed; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleLength? result)
    {
        try
        {
            result = Parse(value: value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a string into a <see cref="StyleLength" /> instance.</summary>
    /// <param name="value">
    /// The string representation of the length value to convert. If <see langword="null" />, an empty string is used.
    /// </param>
    /// <returns>A <see cref="StyleLength" /> instance representing the provided value.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="value" /> cannot be parsed as a valid
    /// length.
    /// </exception>
    public static implicit operator StyleLength(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleLength" /> instance to its underlying string representation.</summary>
    /// <param name="value">The <see cref="StyleLength" /> instance to convert.</param>
    /// <returns>
    /// The original CSS length string stored in <see cref="StyleValueBase.Value" />, or an empty string if
    /// <paramref name="value" /> is <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleLength? value) => value?.Value ?? string.Empty;
}
