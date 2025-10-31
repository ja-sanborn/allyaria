namespace Allyaria.Theming.Types.Style;

public sealed record StyleLength : StyleValueBase
{
    private static readonly Regex LengthWithUnitRegex = new(
        pattern: @"^\s*(?<num>[+\-]?(?:\d+(?:\.\d+)?|\.\d+))\s*(?<unit>[A-Za-z%]+)?\s*$",
        options: RegexOptions.Compiled | RegexOptions.CultureInvariant,
        matchTimeout: TimeSpan.FromMilliseconds(value: 250)
    );

    private static readonly Dictionary<string, LengthUnits> UnitByToken = BuildUnitMap();

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

    public LengthUnits? LengthUnit { get; }

    public double Number { get; }

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

    public static StyleLength Parse(string? value) => new(value: value ?? string.Empty);

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
        var canonicalUnit = string.Empty;

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

    public static implicit operator StyleLength(string? value) => Parse(value: value);

    public static implicit operator string(StyleLength? value) => value?.Value ?? string.Empty;
}
