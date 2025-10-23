namespace Allyaria.Theming.Styles;

public readonly record struct StyleValueString : IStyleValue
{
    public StyleValueString()
        : this(null) { }

    public StyleValueString(string? value) => Value = value.ValidateInput();

    public string Value { get; }

    public static StyleValueString Parse(string? value) => new(value);

    public static bool TryParse(string? value, out StyleValueString? result)
    {
        result = null;

        if (!value.TryValidateInput(out var input))
        {
            return false;
        }

        result = new StyleValueString(input);

        return true;
    }

    public static implicit operator StyleValueString(string? value) => new(value);

    public static implicit operator string(StyleValueString? value) => value?.Value ?? string.Empty;
}
