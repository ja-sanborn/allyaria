namespace Allyaria.Theming.Styles;

public readonly record struct StyleValueString : IStyleValue
{
    public StyleValueString()
        : this(value: null) { }

    public StyleValueString(string? value) => Value = value.ValidateInput();

    public string Value { get; }

    public static StyleValueString Parse(string? value) => new(value: value);

    public static bool TryParse(string? value, out StyleValueString? result)
    {
        result = null;

        if (!value.TryValidateInput(result: out var input))
        {
            return false;
        }

        result = new StyleValueString(value: input);

        return true;
    }

    public static implicit operator StyleValueString(string? value) => new(value: value);

    public static implicit operator string(StyleValueString? value) => value?.Value ?? string.Empty;
}
