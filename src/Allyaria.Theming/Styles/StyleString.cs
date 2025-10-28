namespace Allyaria.Theming.Styles;

public sealed record StyleString : StyleBase
{
    public static StyleString Empty = new(name: string.Empty, value: string.Empty);

    public StyleString(string? name, string? value)
        : base(name: name, value: value) { }

    public static StyleString Parse(string? value) => new(name: value ?? string.Empty, value: string.Empty);

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

    public static implicit operator string(StyleString? value) => value?.ToCss() ?? string.Empty;
}
