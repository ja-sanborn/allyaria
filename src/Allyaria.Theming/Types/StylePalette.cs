namespace Allyaria.Theming.Types;

public readonly record struct StylePalette
{
    public StylePalette(ThemeColor? backgroundColor = null,
        ThemeColor? foregroundColor = null,
        ThemeColor? caretColor = null,
        ThemeColor? accentColor = null,
        StyleBorderColor? borderColor = null,
        ThemeColor? textDecorationColor = null)
    {
        AccentColor = accentColor;
        BackgroundColor = backgroundColor;
        BorderColor = borderColor;
        CaretColor = caretColor;
        ForegroundColor = foregroundColor;
        TextDecorationColor = textDecorationColor;

        if (BackgroundColor is not null)
        {
            AccentColor = AccentColor?.EnsureContrast(BackgroundColor);
            BorderColor = BorderColor?.EnsureContrast(BackgroundColor);
            CaretColor = CaretColor?.EnsureContrast(BackgroundColor);
            ForegroundColor = ForegroundColor?.EnsureContrast(BackgroundColor);
            TextDecorationColor = TextDecorationColor?.EnsureContrast(BackgroundColor);
        }
    }

    public ThemeColor? AccentColor { get; init; }

    public ThemeColor? BackgroundColor { get; init; }

    public StyleBorderColor? BorderColor { get; init; }

    public ThemeColor? CaretColor { get; init; }

    public ThemeColor? ForegroundColor { get; init; }

    public ThemeColor? TextDecorationColor { get; init; }

    public StylePalette Cascade(StylePalette? value = null)
        => value is null
            ? this
            : Cascade(
                value.Value.BackgroundColor,
                value.Value.ForegroundColor,
                value.Value.CaretColor,
                value.Value.AccentColor,
                value.Value.BorderColor,
                value.Value.TextDecorationColor
            );

    public StylePalette Cascade(ThemeColor? backgroundColor = null,
        ThemeColor? foregroundColor = null,
        ThemeColor? caretColor = null,
        ThemeColor? accentColor = null,
        StyleBorderColor? borderColor = null,
        ThemeColor? textDecorationColor = null)
    {
        var newAccentColor = accentColor ?? AccentColor;
        var newBackgroundColor = backgroundColor ?? BackgroundColor;
        var newBorderColor = BorderColor?.Cascade(borderColor) ?? borderColor;
        var newCaretColor = caretColor ?? CaretColor;
        var newForegroundColor = foregroundColor ?? ForegroundColor;
        var newTextDecorationColor = textDecorationColor ?? TextDecorationColor;

        if (newBackgroundColor is not null)
        {
            newForegroundColor = newForegroundColor?.EnsureContrast(newBackgroundColor);
            newAccentColor = newAccentColor?.EnsureContrast(newBackgroundColor);
            newBorderColor = newBorderColor?.EnsureContrast(newBackgroundColor);
            newCaretColor = newCaretColor?.EnsureContrast(newBackgroundColor);
            newTextDecorationColor = newTextDecorationColor?.EnsureContrast(newBackgroundColor);
        }

        return this with
        {
            AccentColor = newAccentColor,
            BackgroundColor = newBackgroundColor,
            BorderColor = newBorderColor,
            CaretColor = newCaretColor,
            ForegroundColor = newForegroundColor,
            TextDecorationColor = newTextDecorationColor
        };
    }

    public static StylePalette FromColor(ThemeColor color)
    {
        if (color.Color == Colors.Transparent)
        {
            return FromHighContrast(false);
        }

        var foregroundColor = color.ToForeground().EnsureContrast(color);
        var caretColor = new ThemeColor(foregroundColor.Color);
        var accentColor = foregroundColor.ToAccent().EnsureContrast(color);
        var borderColor = color.ToAccent().EnsureContrast(color);
        var textDecorationColor = new ThemeColor(accentColor.Color);

        return new StylePalette(
            color,
            foregroundColor,
            caretColor,
            accentColor,
            StyleBorderColor.FromSingle(borderColor),
            textDecorationColor
        );
    }

    public static StylePalette FromHighContrast(bool isDark)
        => isDark
            ? new StylePalette(
                StyleDefaults.BackgroundColorHighContrastDark,
                StyleDefaults.ForegroundColorHighContrastDark,
                StyleDefaults.ForegroundColorHighContrastDark,
                StyleDefaults.ForegroundColorHighContrastDark,
                StyleBorderColor.FromSingle(StyleDefaults.AccentColorHighContrastDark),
                StyleDefaults.ForegroundColorHighContrastDark
            )
            : new StylePalette(
                StyleDefaults.BackgroundColorHighContrastLight,
                StyleDefaults.ForegroundColorHighContrastLight,
                StyleDefaults.ForegroundColorHighContrastLight,
                StyleDefaults.ForegroundColorHighContrastLight,
                StyleBorderColor.FromSingle(StyleDefaults.AccentColorHighContrastLight),
                StyleDefaults.ForegroundColorHighContrastLight
            );

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("accent-color", AccentColor, varPrefix);
        builder.ToCss("background-color", BackgroundColor, varPrefix);
        builder.Append(BorderColor?.ToCss(varPrefix) ?? string.Empty);
        builder.ToCss("caret-color", CaretColor, varPrefix);
        builder.ToCss("color", ForegroundColor, varPrefix);
        builder.ToCss("text-decoration-color", TextDecorationColor, varPrefix);

        return builder.ToString();
    }

    public StylePalette ToDisabled()
        => this with
        {
            AccentColor = AccentColor?.ToDisabled(),
            BackgroundColor = BackgroundColor?.ToDisabled(),
            BorderColor = BorderColor?.Cascade(
                BorderColor?.BlockStart?.ToDisabled(),
                BorderColor?.InlineStart?.ToDisabled(),
                BorderColor?.BlockEnd?.ToDisabled(),
                BorderColor?.InlineEnd?.ToDisabled()
            ),
            CaretColor = CaretColor?.ToDisabled(),
            ForegroundColor = ForegroundColor?.ToDisabled(),
            TextDecorationColor = TextDecorationColor?.ToDisabled()
        };

    public StylePalette ToDragged()
        => this with
        {
            AccentColor = AccentColor?.ToDragged(),
            BackgroundColor = BackgroundColor?.ToDragged(),
            BorderColor = BorderColor?.Cascade(
                BorderColor?.BlockStart?.ToDragged(),
                BorderColor?.InlineStart?.ToDragged(),
                BorderColor?.BlockEnd?.ToDragged(),
                BorderColor?.InlineEnd?.ToDragged()
            ),
            CaretColor = CaretColor?.ToDragged(),
            ForegroundColor = ForegroundColor?.ToDragged(),
            TextDecorationColor = TextDecorationColor?.ToDragged()
        };

    public StylePalette ToFocused()
        => this with
        {
            AccentColor = AccentColor?.ToFocused(),
            BackgroundColor = BackgroundColor?.ToFocused(),
            BorderColor = BorderColor?.Cascade(
                BorderColor?.BlockStart?.ToFocused(),
                BorderColor?.InlineStart?.ToFocused(),
                BorderColor?.BlockEnd?.ToFocused(),
                BorderColor?.InlineEnd?.ToFocused()
            ),
            CaretColor = CaretColor?.ToFocused(),
            ForegroundColor = ForegroundColor?.ToFocused(),
            TextDecorationColor = TextDecorationColor?.ToFocused()
        };

    public StylePalette ToHovered()
        => this with
        {
            AccentColor = AccentColor?.ToHovered(),
            BackgroundColor = BackgroundColor?.ToHovered(),
            BorderColor = BorderColor?.Cascade(
                BorderColor?.BlockStart?.ToHovered(),
                BorderColor?.InlineStart?.ToHovered(),
                BorderColor?.BlockEnd?.ToHovered(),
                BorderColor?.InlineEnd?.ToHovered()
            ),
            CaretColor = CaretColor?.ToHovered(),
            ForegroundColor = ForegroundColor?.ToHovered(),
            TextDecorationColor = TextDecorationColor?.ToHovered()
        };

    public StylePalette ToPressed()
        => this with
        {
            AccentColor = AccentColor?.ToPressed(),
            BackgroundColor = BackgroundColor?.ToPressed(),
            BorderColor = BorderColor?.Cascade(
                BorderColor?.BlockStart?.ToPressed(),
                BorderColor?.InlineStart?.ToPressed(),
                BorderColor?.BlockEnd?.ToPressed(),
                BorderColor?.InlineEnd?.ToPressed()
            ),
            CaretColor = CaretColor?.ToPressed(),
            ForegroundColor = ForegroundColor?.ToPressed(),
            TextDecorationColor = TextDecorationColor?.ToPressed()
        };
}
