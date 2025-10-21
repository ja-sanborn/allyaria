namespace Allyaria.Theming.Types;

public readonly record struct StyleLayoutSize(
    ThemeNumber? Height = null,
    ThemeNumber? Width = null,
    ThemeNumber? MaxHeight = null,
    ThemeNumber? MaxWidth = null,
    StyleMargin? Margin = null,
    StylePadding? Padding = null
)
{
    public StyleLayoutSize Cascade(StyleLayoutSize? value = null)
        => value is null
            ? this
            : Cascade(
                value.Value.Height, value.Value.Width, value.Value.MaxHeight, value.Value.MaxWidth, value.Value.Margin,
                value.Value.Padding
            );

    public StyleLayoutSize Cascade(ThemeNumber? height = null,
        ThemeNumber? width = null,
        ThemeNumber? maxHeight = null,
        ThemeNumber? maxWidth = null,
        StyleMargin? margin = null,
        StylePadding? padding = null)
        => this with
        {
            Height = height ?? Height,
            Margin = Margin?.Cascade(margin) ?? margin,
            MaxHeight = maxHeight ?? MaxHeight,
            MaxWidth = maxWidth ?? MaxWidth,
            Padding = Padding?.Cascade(padding) ?? padding,
            Width = width ?? Width
        };

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("height", Height, varPrefix);
        builder.Append(Margin?.ToCss(varPrefix) ?? string.Empty);
        builder.ToCss("max-height", MaxHeight, varPrefix);
        builder.ToCss("max-width", MaxWidth, varPrefix);
        builder.ToCss("width", Width, varPrefix);
        builder.Append(Padding?.ToCss(varPrefix) ?? string.Empty);

        return builder.ToString();
    }
}
