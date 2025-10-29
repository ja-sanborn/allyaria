namespace Allyaria.Theming.Contracts;

public abstract record StyleValueBase : IStyleValue
{
    protected StyleValueBase(string value) => Value = ValidateInput(value: value);

    public string Value { get; }

    protected bool TryValidateInput(string? value, out string result)
    {
        result = value?.Trim() ?? string.Empty;

        return !result.Any(predicate: static c => char.IsControl(c: c));
    }

    protected string ValidateInput(string? value)
        => TryValidateInput(value: value, result: out var result)
            ? result
            : throw new AryArgumentException(message: "Invalid value", argName: nameof(value), argValue: value);
}
