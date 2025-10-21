namespace Allyaria.Theming.Types;

public readonly record struct StyleTypography(
    ThemeString? FontFamily = null,
    ThemeNumber? FontSize = null,
    ThemeString? FontStyle = null,
    ThemeString? FontWeight = null,
    ThemeNumber? LetterSpacing = null,
    ThemeNumber? LineHeight = null,
    ThemeString? TextDecorationLine = null,
    ThemeString? TextDecorationStyle = null,
    ThemeString? TextTransform = null
)
{
    public StyleTypography Cascade(StyleTypography? value = null)
        => value is null
            ? this
            : Cascade(
                value.Value.FontFamily,
                value.Value.FontSize,
                value.Value.FontStyle,
                value.Value.FontWeight,
                value.Value.LetterSpacing,
                value.Value.LineHeight,
                value.Value.TextDecorationLine,
                value.Value.TextDecorationStyle,
                value.Value.TextTransform
            );

    public StyleTypography Cascade(ThemeString? fontFamily = null,
        ThemeNumber? fontSize = null,
        ThemeString? fontStyle = null,
        ThemeString? fontWeight = null,
        ThemeNumber? letterSpacing = null,
        ThemeNumber? lineHeight = null,
        ThemeString? textDecorationLine = null,
        ThemeString? textDecorationStyle = null,
        ThemeString? textTransform = null)
        => this with
        {
            FontFamily = fontFamily ?? FontFamily,
            FontSize = fontSize ?? FontSize,
            FontStyle = fontStyle ?? FontStyle,
            FontWeight = fontWeight ?? FontWeight,
            LetterSpacing = letterSpacing ?? LetterSpacing,
            LineHeight = lineHeight ?? LineHeight,
            TextDecorationLine = textDecorationLine ?? TextDecorationLine,
            TextDecorationStyle = textDecorationStyle ?? TextDecorationStyle,
            TextTransform = textTransform ?? TextTransform
        };

    public static StyleMargin FromSingle(ThemeNumber value) => new(value, value, value, value);

    public static StyleMargin FromSymmetric(ThemeNumber block, ThemeNumber inline) => new(block, inline, block, inline);

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("font-family", FontFamily, varPrefix);
        builder.ToCss("font-size", FontSize, varPrefix);
        builder.ToCss("font-style", FontStyle, varPrefix);
        builder.ToCss("font-weight", FontWeight, varPrefix);
        builder.ToCss("letter-spacing", LetterSpacing, varPrefix);
        builder.ToCss("line-height", LineHeight, varPrefix);
        builder.ToCss("text-decoration-line", TextDecorationLine, varPrefix);
        builder.ToCss("text-decoration-style", TextDecorationStyle, varPrefix);
        builder.ToCss("text-transform", TextTransform, varPrefix);

        return builder.ToString();
    }
}
