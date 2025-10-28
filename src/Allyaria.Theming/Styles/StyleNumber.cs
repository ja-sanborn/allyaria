namespace Allyaria.Theming.Styles;

public sealed record StyleNumber : StyleBase
{
    public static readonly StyleNumber Empty = new(value: string.Empty);

    public StyleNumber(string value)
        : this(name: value, value: string.Empty) { }

    public StyleNumber(string name, StyleBase value)
        : this(name: name, value: value.Value) { }

    public StyleNumber(string name, string value)
        : base(name: name, value: value)
    {
        if (string.IsNullOrWhiteSpace(value: Value))
        {
            return;
        }

        if (TryNormalizeInteger(input: Value, number: out var number))
        {
            Number = number;
        }
        else
        {
            throw new ArgumentException(message: $"Invalid number: {Value}");
        }
    }

    public StyleNumber(string name, int number)
        : base(name: name, value: number.ToString(provider: CultureInfo.InvariantCulture))
        => Number = number;

    public int Number { get; }

    public static StyleNumber Parse(string? value) => new(value: value ?? string.Empty);

    private static bool TryNormalizeInteger(string input, out int number)
    {
        number = 0;

        return input.Equals(value: "auto", comparisonType: StringComparison.OrdinalIgnoreCase) ||
            string.IsNullOrWhiteSpace(value: input) || int.TryParse(
                s: input, style: NumberStyles.Integer, provider: CultureInfo.InvariantCulture, result: out number
            );
    }

    public static bool TryParse(string? value, out StyleNumber? result)
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

    public static implicit operator StyleNumber(string? value) => Parse(value: value);

    public static implicit operator string(StyleNumber? value) => value?.ToCss() ?? string.Empty;
}
