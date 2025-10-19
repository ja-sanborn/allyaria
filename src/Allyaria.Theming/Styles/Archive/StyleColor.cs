namespace Allyaria.Theming.Types;

public readonly record struct StyleColor(ThemeColor? Color = null)
{
    public StyleColor Cascade(StyleBorderColor? value = null)
        => value is null
            ? this
            : Cascade(value.Value);

    public StyleColor Cascade(ThemeColor? color = null)
        => this with
        {
            Color = color ?? Color
        };

    public string ToCss(string? propertyName = "", string? varPrefix = "")
    {
        var property = propertyName.ToCssName();

        if (string.IsNullOrWhiteSpace(property))
        {
            property = "color";
        }

        var builder = new StringBuilder();

        builder.ToCss(property, Color, varPrefix);

        return builder.ToString();
    }

}
