namespace Allyaria.Theming.Types;

public sealed record StyleNumber : StyleValueBase
{
    public StyleNumber(string value)
        : base(value: value)
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

    public int Number { get; }

    public static StyleNumber Parse(string? value) => new(value: value ?? string.Empty);

    private static bool TryNormalizeInteger(string input, out int number)
    {
        number = 0;

        return string.IsNullOrWhiteSpace(value: input) || int.TryParse(
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

    public static implicit operator string(StyleNumber? value) => value?.Value ?? string.Empty;
}
