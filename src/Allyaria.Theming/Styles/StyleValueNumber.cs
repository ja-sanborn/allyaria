namespace Allyaria.Theming.Styles;

public readonly record struct StyleValueNumber : IStyleValue
{
    private static readonly Regex NumberWithUnitRegex = new(
        @"^\s*(?<num>[+\-]?(?:\d+(?:\.\d+)?|\.\d+))\s*(?<unit>[A-Za-z%]+)?\s*$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant,
        TimeSpan.FromMilliseconds(250)
    );

    private static readonly Dictionary<string, LengthUnits> UnitByToken = BuildUnitMap();

    public StyleValueNumber()
        : this(null) { }

    public StyleValueNumber(string? value)
    {
        if (!TryNormalizeNumber(value, out var newValue, out var number, out var unit))
        {
            throw new AryArgumentException("Invalid number value.", nameof(value), value);
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
        var dict = new Dictionary<string, LengthUnits>(StringComparer.OrdinalIgnoreCase);

        foreach (var unit in Enum.GetValues<LengthUnits>())
        {
            var desc = unit.GetDescription();

            if (!string.IsNullOrWhiteSpace(desc))
            {
                var key = desc.ToLowerInvariant();
                dict.TryAdd(key, unit);
            }
        }

        return dict;
    }

    public static StyleValueNumber Parse(string? value) => new(value);

    private static bool TryMapUnit(string token, out LengthUnits unit, out string canonical)
    {
        if (UnitByToken.TryGetValue(token.Trim().ToLowerInvariant(), out var u))
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

        if (!input.TryValidateInput(out var valid))
        {
            return false;
        }

        if (valid.Equals("auto", StringComparison.OrdinalIgnoreCase))
        {
            number = double.NaN;
            value = "auto";

            return true;
        }

        var match = NumberWithUnitRegex.Match(valid);

        if (!match.Success)
        {
            return false;
        }

        var numText = match.Groups["num"].Value;

        var unitText = match.Groups["unit"].Success
            ? match.Groups["unit"].Value
            : null;

        if (!double.TryParse(numText, NumberStyles.Float, CultureInfo.InvariantCulture, out number) ||
            double.IsInfinity(number))
        {
            return false;
        }

        unit = null;
        var canonicalUnit = string.Empty;

        if (!string.IsNullOrEmpty(unitText))
        {
            if (!TryMapUnit(unitText, out var lengthUnit, out canonicalUnit))
            {
                return false;
            }

            unit = lengthUnit;
        }

        var abs = Math.Abs(number);

        var numCanonical = number.ToString(
            abs is > 0 and < 0.0001 or >= 1e6
                ? "0.################"
                : "0.#######", CultureInfo.InvariantCulture
        );

        value = string.Concat(numCanonical, canonicalUnit);

        return true;
    }

    public static bool TryParse(string? value, out StyleValueNumber result)
    {
        result = null;

        if (!TryNormalizeNumber(value, out var normalized, out var number, out var unit))
        {
            return false;
        }

        result = new StyleValueNumber(normalized, number, unit);

        return true;
    }

    public static implicit operator StyleValueNumber(string? value) => new(value);

    public static implicit operator string(StyleValueNumber? value) => value?.Value ?? string.Empty;
}
