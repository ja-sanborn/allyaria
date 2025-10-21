namespace Allyaria.Theming.Types;

public readonly record struct StyleLayoutBorder(
    StyleBorderWidth? BorderWidth = null,
    StyleBorderStyle? BorderStyle = null,
    StyleBorderRadius? BorderRadius = null
)
{
    public StyleLayoutBorder Cascade(StyleLayoutBorder? value = null)
        => value is null
            ? this
            : Cascade(value.Value.BorderWidth, value.Value.BorderStyle, value.Value.BorderRadius);

    public StyleLayoutBorder Cascade(StyleBorderWidth? borderWidth = null,
        StyleBorderStyle? borderStyle = null,
        StyleBorderRadius? borderRadius = null)
        => this with
        {
            BorderRadius = BorderRadius?.Cascade(borderRadius) ?? borderRadius,
            BorderStyle = BorderStyle?.Cascade(borderStyle) ?? borderStyle,
            BorderWidth = BorderWidth?.Cascade(borderWidth) ?? borderWidth
        };

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.Append(BorderRadius?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(BorderStyle?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(BorderWidth?.ToCss(varPrefix) ?? string.Empty);

        return builder.ToString();
    }
}
