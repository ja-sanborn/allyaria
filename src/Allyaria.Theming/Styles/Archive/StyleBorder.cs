namespace Allyaria.Theming.Types;

public readonly record struct StyleBorder(
    StyleBorderWidth? Width = null,
    StyleBorderStyle? Style = null,
    StyleBorderColor? Color = null,
    StyleBorderRadius? Radius = null
)
{
    public StyleBorder Cascade(StyleBorder value) => Cascade(value.Width, value.Style, value.Color, value.Radius);

    public StyleBorder Cascade(StyleBorderWidth? width = null,
        StyleBorderStyle? style = null,
        StyleBorderColor? color = null,
        StyleBorderRadius? radius = null)
        => this with
        {
            Color = Color?.Cascade(color) ?? color,
            Radius = Radius?.Cascade(radius) ?? radius,
            Style = Style?.Cascade(style) ?? style,
            Width = Width?.Cascade(width) ?? width
        };

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.Append(Color?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(Radius?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(Style?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(Width?.ToCss(varPrefix) ?? string.Empty);

        return builder.ToString();
    }
}
