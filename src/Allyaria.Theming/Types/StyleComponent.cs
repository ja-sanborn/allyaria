namespace Allyaria.Theming.Types;

public readonly record struct StyleComponent
{
    public StyleComponent(ThemeColor? backgroundColor = null,
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
    {
        Default = new StyleState(
            backgroundColor,
            foregroundColor,
            accentColor,
            borderColor,
            borderWidth,
            borderStyle,
            borderRadius,
            margin,
            padding,
            fontFamily,
            fontSize,
            fontStyle,
            fontWeight,
            letterSpacing,
            lineHeight,
            textAlign,
            textDecorationLine,
            textDecorationStyle,
            textTransform,
            verticalAlign
        );

        Disabled = new StyleState(
            backgroundColor?.ToDisabled(),
            foregroundColor?.ToDisabled(),
            accentColor?.ToDisabled(),
            borderColor,
            borderWidth,
            borderStyle,
            borderRadius,
            margin,
            padding,
            fontFamily,
            fontSize,
            fontStyle,
            fontWeight,
            letterSpacing,
            lineHeight,
            textAlign,
            textDecorationLine,
            textDecorationStyle,
            textTransform,
            verticalAlign
        );

        Dragged = new StyleState(
            backgroundColor?.ToDragged(),
            foregroundColor?.ToDragged(),
            accentColor?.ToDragged(),
            borderColor,
            borderWidth,
            borderStyle,
            borderRadius,
            margin,
            padding,
            fontFamily,
            fontSize,
            fontStyle,
            fontWeight,
            letterSpacing,
            lineHeight,
            textAlign,
            textDecorationLine,
            textDecorationStyle,
            textTransform,
            verticalAlign
        );

        Focused = new StyleState(
            backgroundColor?.ToFocused(),
            foregroundColor?.ToFocused(),
            accentColor?.ToFocused(),
            borderColor,
            borderWidth,
            borderStyle,
            borderRadius,
            margin,
            padding,
            fontFamily,
            fontSize,
            fontStyle,
            fontWeight,
            letterSpacing,
            lineHeight,
            textAlign,
            textDecorationLine,
            textDecorationStyle,
            textTransform,
            verticalAlign
        );

        Hovered = new StyleState(
            backgroundColor?.ToHovered(),
            foregroundColor?.ToHovered(),
            accentColor?.ToHovered(),
            borderColor,
            borderWidth,
            borderStyle,
            borderRadius,
            margin,
            padding,
            fontFamily,
            fontSize,
            fontStyle,
            fontWeight,
            letterSpacing,
            lineHeight,
            textAlign,
            textDecorationLine,
            textDecorationStyle,
            textTransform,
            verticalAlign
        );

        Pressed = new StyleState(
            backgroundColor?.ToPressed(),
            foregroundColor?.ToPressed(),
            accentColor?.ToPressed(),
            borderColor,
            borderWidth,
            borderStyle,
            borderRadius,
            margin,
            padding,
            fontFamily,
            fontSize,
            fontStyle,
            fontWeight,
            letterSpacing,
            lineHeight,
            textAlign,
            textDecorationLine,
            textDecorationStyle,
            textTransform,
            verticalAlign
        );
    }

    public StyleState Default { get; init; }

    public StyleState Disabled { get; init; }

    public StyleState Dragged { get; init; }

    public StyleState Focused { get; init; }

    public StyleState Hovered { get; init; }

    public StyleState Pressed { get; init; }

    public StyleComponent Cascade(StyleComponent? value = null)
        => Cascade(
            value?.Default.BackgroundColor,
            value?.Default.ForegroundColor,
            value?.Default.AccentColor,
            value?.Default.BorderColor,
            value?.Default.BorderWidth,
            value?.Default.BorderStyle,
            value?.Default.BorderRadius,
            value?.Default.Margin,
            value?.Default.Padding,
            value?.Default.FontFamily,
            value?.Default.FontSize,
            value?.Default.FontStyle,
            value?.Default.FontWeight,
            value?.Default.LetterSpacing,
            value?.Default.LineHeight,
            value?.Default.TextAlign,
            value?.Default.TextDecorationLine,
            value?.Default.TextDecorationStyle,
            value?.Default.TextTransform,
            value?.Default.VerticalAlign
        );

    public StyleComponent Cascade(ThemeColor? backgroundColor = null,
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
        => new(
            backgroundColor ?? Default.BackgroundColor,
            foregroundColor ?? Default.ForegroundColor,
            accentColor ?? Default.AccentColor,
            borderColor ?? Default.BorderColor,
            borderWidth ?? Default.BorderWidth,
            borderStyle ?? Default.BorderStyle,
            borderRadius ?? Default.BorderRadius,
            margin ?? Default.Margin,
            padding ?? Default.Padding,
            fontFamily ?? Default.FontFamily,
            fontSize ?? Default.FontSize,
            fontStyle ?? Default.FontStyle,
            fontWeight ?? Default.FontWeight,
            letterSpacing ?? Default.LetterSpacing,
            lineHeight ?? Default.LineHeight,
            textAlign ?? Default.TextAlign,
            textDecorationLine ?? Default.TextDecorationLine,
            textDecorationStyle ?? Default.TextDecorationStyle,
            textTransform ?? Default.TextTransform,
            verticalAlign ?? Default.VerticalAlign
        );

    public string ToCss(ComponentState state, string? varPrefix = "")
    {
        var builder = new StringBuilder();
        var prefix = varPrefix.ToCssName();

        if (!string.IsNullOrWhiteSpace(prefix))
        {
            prefix = $"{prefix}-{state}";
        }

        switch (state)
        {
            case ComponentState.Default:
                builder.Append(Default.ToCss(prefix));
                break;

            case ComponentState.Disabled:
                builder.Append(Disabled.ToCss(prefix));
                break;

            case ComponentState.Dragged:
                builder.Append(Dragged.ToCss(prefix));
                break;

            case ComponentState.Focused:
                builder.Append(Focused.ToCss(prefix));
                break;

            case ComponentState.Hovered:
                builder.Append(Hovered.ToCss(prefix));
                break;

            case ComponentState.Pressed:
                builder.Append(Pressed.ToCss(prefix));
                break;
        }

        return builder.ToString();
    }
}
