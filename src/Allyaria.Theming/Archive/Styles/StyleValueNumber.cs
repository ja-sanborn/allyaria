namespace Allyaria.Theming.Styles;

public readonly record struct StyleValueNumber : IStyleValue
{
    private static readonly Regex NumberWithUnitRegex = new(
        pattern: @"^\s*(?<num>[+\-]?(?:\d+(?:\.\d+)?|\.\d+))\s*(?<unit>[A-Za-z%]+)?\s*$",
        options: RegexOptions.Compiled | RegexOptions.CultureInvariant,
        matchTimeout: TimeSpan.FromMilliseconds(value: 250)
    );

    private static readonly Dictionary<string, LengthUnits> UnitByToken = BuildUnitMap();

    public StyleValueNumber()
        : this(value: null) { }

    public StyleValueNumber(string? value)
    {
        if (!TryNormalizeNumber(input: value, value: out var newValue, number: out var number, unit: out var unit))
        {
            throw new AryArgumentException(message: "Invalid number value.", argName: nameof(value), argValue: value);
        }

        Value = newValue;
        LengthUnit = unit;
        Number = number;
    }

    private StyleValueNumber(string value, double number, LengthUnits? unit)
    {
        Value = value;
        LengthUnit = unit;
        Number = number;
    }

    public LengthUnits? LengthUnit { get; }

    public double Number { get; }

    public string Value { get; }

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

    public static StyleValueNumber Parse(string? value) => new(value: value);

    private static bool TryMapUnit(string token, out LengthUnits unit, out string canonical)
    {
        if (UnitByToken.TryGetValue(key: token.Trim().ToLowerInvariant(), value: out var u))
        {
            unit = u;
            canonical = u.GetDescription();

            return true;
        }

        unit = default(LengthUnits);
        canonical = string.Empty;

        return false;
    }

    private static bool TryNormalizeNumber(string? input, out string value, out double number, out LengthUnits? unit)
    {
        number = 0.0;
        unit = null;
        value = string.Empty;

        if (!input.TryValidateInput(result: out var valid))
        {
            return false;
        }

        if (valid.Equals(value: "auto", comparisonType: StringComparison.OrdinalIgnoreCase) ||
            valid.Equals(value: "normal", comparisonType: StringComparison.OrdinalIgnoreCase))
        {
            number = double.NaN;
            value = valid.ToLowerInvariant();

            return true;
        }

        var match = NumberWithUnitRegex.Match(input: valid);

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
            ) ||
            double.IsInfinity(d: number))
        {
            return false;
        }

        unit = null;
        var canonicalUnit = string.Empty;

        if (!string.IsNullOrEmpty(value: unitText))
        {
            if (!TryMapUnit(token: unitText, unit: out var lengthUnit, canonical: out canonicalUnit))
            {
                return false;
            }

            unit = lengthUnit;
        }

        var abs = Math.Abs(value: number);

        var numCanonical = number.ToString(
            format: abs is > 0 and < 0.0001 or >= 1e6
                ? "0.################"
                : "0.#######", provider: CultureInfo.InvariantCulture
        );

        value = string.Concat(str0: numCanonical, str1: canonicalUnit);

        return true;
    }

    public static bool TryParse(string? value, out StyleValueNumber result)
    {
        result = null;

        if (!TryNormalizeNumber(input: value, value: out var normalized, number: out var number, unit: out var unit))
        {
            return false;
        }

        result = new StyleValueNumber(value: normalized, number: number, unit: unit);

        return true;
    }

    public static implicit operator StyleValueNumber(string? value) => new(value: value);

    public static implicit operator string(StyleValueNumber? value) => value?.Value ?? string.Empty;
}
