namespace Allyaria.Theming.Types;

public readonly record struct StyleBorderRadius(
    ThemeNumber? StartStart = null,
    ThemeNumber? StartEnd = null,
    ThemeNumber? EndStart = null,
    ThemeNumber? EndEnd = null
)
{
    public StyleBorderRadius Cascade(StyleBorderRadius? value = null)
        => value is null
            ? this
            : Cascade(value.Value.StartStart, value.Value.StartEnd, value.Value.EndStart, value.Value.EndEnd);

    public StyleBorderRadius Cascade(ThemeNumber? startStart = null,
        ThemeNumber? startEnd = null,
        ThemeNumber? endStart = null,
        ThemeNumber? endEnd = null)
        => this with
        {
            EndEnd = endEnd ?? EndEnd,
            EndStart = endStart ?? EndStart,
            StartEnd = startEnd ?? StartEnd,
            StartStart = startStart ?? StartStart
        };

    public static StyleBorderRadius FromSingle(ThemeNumber value) => new(value, value, value, value);

    public static StyleBorderRadius FromSymmetric(ThemeNumber start, ThemeNumber end) => new(start, end, end, start);

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("border-end-end-radius", EndEnd, varPrefix);
        builder.ToCss("border-end-start-radius", EndStart, varPrefix);
        builder.ToCss("border-start-end-radius", StartEnd, varPrefix);
        builder.ToCss("border-start-start-radius", StartStart, varPrefix);

        return builder.ToString();
    }
}
