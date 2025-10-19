namespace Allyaria.Theming.Types;

public readonly record struct StyleState(
    ThemeColor? BackgroundColor = null,
    ThemeColor? ForegroundColor = null,
    ThemeColor? AccentColor = null,
    StyleBorderColor? BorderColor = null,
    StyleBorderWidth? BorderWidth = null,
    StyleBorderStyle? BorderStyle = null,
    StyleBorderRadius? BorderRadius = null,
    StyleMargin? Margin = null,
    StylePadding? Padding = null,
    ThemeString? FontFamily = null,
    ThemeNumber? FontSize = null,
    ThemeString? FontStyle = null,
    ThemeString? FontWeight = null,
    ThemeNumber? LetterSpacing = null,
    ThemeNumber? LineHeight = null,
    ThemeString? TextAlign = null,
    ThemeString? TextDecorationLine = null,
    ThemeString? TextDecorationStyle = null,
    ThemeString? TextTransform = null,
    ThemeString? VerticalAlign = null
)
{
    public StyleState Cascade(StyleState value)
        => Cascade(
            value.BackgroundColor,
            value.ForegroundColor,
            value.AccentColor,
            value.BorderColor,
            value.BorderWidth,
            value.BorderStyle,
            value.BorderRadius,
            value.Margin,
            value.Padding,
            value.FontFamily,
            value.FontSize,
            value.FontStyle,
            value.FontWeight,
            value.LetterSpacing,
            value.LineHeight,
            value.TextAlign,
            value.TextDecorationLine,
            value.TextDecorationStyle,
            value.TextTransform,
            value.VerticalAlign
        );

    public StyleState Cascade(ThemeColor? backgroundColor = null,
        ThemeColor? foregroundColor = null,
        ThemeColor? accentColor = null,
        StyleBorderColor? borderColor = null,
        StyleBorderWidth? borderWidth = null,
        StyleBorderStyle? borderStyle = null,
        StyleBorderRadius? borderRadius = null,
        StyleMargin? margin = null,
        StylePadding? padding = null,
        ThemeString? fontFamily = null,
        ThemeNumber? fontSize = null,
        ThemeString? fontStyle = null,
        ThemeString? fontWeight = null,
        ThemeNumber? letterSpacing = null,
        ThemeNumber? lineHeight = null,
        ThemeString? textAlign = null,
        ThemeString? textDecorationLine = null,
        ThemeString? textDecorationStyle = null,
        ThemeString? textTransform = null,
        ThemeString? verticalAlign = null)
        => this with
        {
            AccentColor = accentColor ?? AccentColor,
            BackgroundColor = backgroundColor ?? BackgroundColor,
            BorderColor = BorderColor?.Cascade(borderColor) ?? borderColor,
            BorderRadius = BorderRadius?.Cascade(borderRadius) ?? borderRadius,
            BorderStyle = BorderStyle?.Cascade(borderStyle) ?? borderStyle,
            BorderWidth = BorderWidth?.Cascade(borderWidth) ?? borderWidth,
            FontFamily = fontFamily ?? FontFamily,
            FontSize = fontSize ?? FontSize,
            FontStyle = fontStyle ?? FontStyle,
            FontWeight = fontWeight ?? FontWeight,
            ForegroundColor = foregroundColor ?? ForegroundColor,
            LetterSpacing = letterSpacing ?? LetterSpacing,
            LineHeight = lineHeight ?? LineHeight,
            Margin = Margin?.Cascade(margin) ?? margin,
            Padding = Padding?.Cascade(padding) ?? padding,
            TextAlign = textAlign ?? TextAlign,
            TextDecorationLine = textDecorationLine ?? TextDecorationLine,
            TextDecorationStyle = textDecorationStyle ?? TextDecorationStyle,
            TextTransform = textTransform ?? TextTransform,
            VerticalAlign = verticalAlign ?? VerticalAlign
        };

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("accent-color", AccentColor, varPrefix);
        builder.ToCss("background-color", BackgroundColor, varPrefix);
        builder.Append(BorderColor?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(BorderRadius?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(BorderStyle?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(BorderWidth?.ToCss(varPrefix) ?? string.Empty);
        builder.ToCss("font-family", FontFamily, varPrefix);
        builder.ToCss("font-style", FontStyle, varPrefix);
        builder.ToCss("font-weight", FontWeight, varPrefix);
        builder.ToCss("color", ForegroundColor, varPrefix);
        builder.ToCss("letter-spacing", LetterSpacing, varPrefix);
        builder.ToCss("line-height", LineHeight, varPrefix);
        builder.Append(Margin?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(Padding?.ToCss(varPrefix) ?? string.Empty);
        builder.ToCss("text-align", TextAlign, varPrefix);
        builder.ToCss("text-decoration-line", TextDecorationLine, varPrefix);
        builder.ToCss("text-decoration-style", TextDecorationStyle, varPrefix);
        builder.ToCss("text-transform", TextTransform, varPrefix);
        builder.ToCss("vertical-align", VerticalAlign, varPrefix);

        return builder.ToString();
    }
}
