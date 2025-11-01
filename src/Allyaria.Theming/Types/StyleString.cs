namespace Allyaria.Theming.Types;

public sealed record StyleString : StyleValueBase
{
    public StyleString(string? value = "")
        : base(value: value ?? string.Empty) { }

    public static StyleString Parse(string? value) => new(value: value);

    public static bool TryParse(string? value, out StyleString? result)
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

    public static implicit operator StyleString(string? value) => Parse(value: value);

    public static implicit operator string(StyleString? value) => value?.Value ?? string.Empty;
}
