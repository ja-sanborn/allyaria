namespace Allyaria.Theming.Themes;

public sealed record ThemeGroupPalette
{
    public static readonly ThemeGroupPalette Empty = new();

    public ThemeGroupPalette(StyleValueColor? accentColor = null,
        StyleValueColor? backgroundColor = null,
        StyleGroupBorderColor? borderColor = null,
        StyleValueColor? caretColor = null,
        StyleValueColor? color = null,
        StyleValueColor? textDecorationColor = null)
    {
        AccentColor = accentColor;
        BackgroundColor = backgroundColor;
        BorderColor = borderColor;
        CaretColor = caretColor;
        Color = color;
        TextDecorationColor = textDecorationColor;

        if (BackgroundColor?.Color.IsTransparent() ?? true)
        {
            return;
        }

        AccentColor = AccentColor?.EnsureContrast(BackgroundColor.Value);
        BorderColor = BorderColor?.EnsureContrast(BackgroundColor.Value);
        CaretColor = CaretColor?.EnsureContrast(BackgroundColor.Value);
        Color = Color?.EnsureContrast(BackgroundColor.Value);
        TextDecorationColor = TextDecorationColor?.EnsureContrast(BackgroundColor.Value);
    }

    public StyleValueColor? AccentColor { get; init; }

    public StyleValueColor? BackgroundColor { get; init; }

    public StyleGroupBorderColor? BorderColor { get; init; }

    public StyleValueColor? CaretColor { get; init; }

    public StyleValueColor? Color { get; init; }

    public StyleValueColor? TextDecorationColor { get; init; }

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder
            .Add("accent-color", AccentColor, varPrefix)
            .Add("background-color", BackgroundColor, varPrefix)
            .Add("caret-color", CaretColor, varPrefix)
            .Add("color", Color, varPrefix)
            .Add("text-decoration-color", TextDecorationColor, varPrefix);

        builder = BorderColor?.BuildCss(builder, varPrefix) ?? builder;

        return builder;
    }

    public static ThemeGroupPalette FromDefault(PaletteColor paletteColor, ThemeType themeType, PaletteType paletteType)
    {
        var backgroundColor = paletteColor.GetColor(themeType, paletteType);
        var color = backgroundColor.ToForeground().EnsureContrast(backgroundColor);
        var caretColor = new StyleValueColor(color.Color);
        var accentColor = color.ToAccent().EnsureContrast(backgroundColor);
        var borderColor = new StyleGroupBorderColor(backgroundColor.ToAccent().EnsureContrast(backgroundColor));
        var textDecorationColor = new StyleValueColor(accentColor.Color);

        if (themeType is ThemeType.HighContrastDark)
        {
            backgroundColor = ThemingDefaults.BackgroundColorHighContrastDark;
            color = ThemingDefaults.ForegroundColorHighContrastDark;
            caretColor = color;
            accentColor = ThemingDefaults.AccentColorHighContrastDark;
            borderColor = new StyleGroupBorderColor(accentColor);
            textDecorationColor = color;
        }
        else if (themeType is ThemeType.HighContrastLight)
        {
            backgroundColor = ThemingDefaults.BackgroundColorHighContrastLight;
            color = ThemingDefaults.ForegroundColorHighContrastLight;
            caretColor = color;
            accentColor = ThemingDefaults.AccentColorHighContrastLight;
            borderColor = new StyleGroupBorderColor(accentColor);
            textDecorationColor = color;
        }

        return new ThemeGroupPalette(
            accentColor,
            backgroundColor,
            borderColor,
            caretColor,
            color,
            textDecorationColor
        );
    }

    public ThemeGroupPalette Merge(ThemeGroupPalette other)
        => SetAccentColor(other.AccentColor ?? AccentColor)
            .SetBackgroundColor(other.BackgroundColor ?? BackgroundColor)
            .SetBorderColor(other.BorderColor ?? BorderColor)
            .SetCaretColor(other.CaretColor ?? CaretColor)
            .SetColor(other.Color ?? Color)
            .SetTextDecorationColor(other.TextDecorationColor ?? TextDecorationColor);

    public ThemeGroupPalette SetAccentColor(StyleValueColor? value)
        => this with
        {
            AccentColor = BackgroundColor?.Color.IsTransparent() ?? true
                ? value
                : value?.EnsureContrast(BackgroundColor.Value)
        };

    public ThemeGroupPalette SetBackgroundColor(StyleValueColor? value)
    {
        if (value is null)
        {
            return this with
            {
                BackgroundColor = null
            };
        }

        return (this with
            {
                BackgroundColor = value.Value
            })
            .SetAccentColor(AccentColor)
            .SetBorderColor(BorderColor)
            .SetCaretColor(CaretColor)
            .SetColor(Color)
            .SetTextDecorationColor(TextDecorationColor);
    }

    public ThemeGroupPalette SetBorderColor(StyleGroupBorderColor? value)
        => value is not null
            ? SetBorderColor(value.BlockStart, value.BlockEnd, value.InlineStart, value.InlineEnd)
            : this with
            {
                BorderColor = null
            };

    public ThemeGroupPalette SetBorderColor(StyleValueColor? value)
        => value is not null
            ? SetBorderColor(value.Value, value.Value, value.Value, value.Value)
            : this with
            {
                BorderColor = null
            };

    public ThemeGroupPalette SetBorderColor(StyleValueColor block, StyleValueColor inline)
        => SetBorderColor(block, block, inline, inline);

    public ThemeGroupPalette SetBorderColor(StyleValueColor blockStart,
        StyleValueColor blockEnd,
        StyleValueColor inlineStart,
        StyleValueColor inlineEnd)
    {
        var newBlockEnd = BackgroundColor?.Color.IsTransparent() ?? true
            ? blockEnd
            : blockEnd.EnsureContrast(BackgroundColor.Value);

        var newBlockStart = BackgroundColor?.Color.IsTransparent() ?? true
            ? blockStart
            : blockStart.EnsureContrast(BackgroundColor.Value);

        var newInlineEnd = BackgroundColor?.Color.IsTransparent() ?? true
            ? inlineEnd
            : inlineEnd.EnsureContrast(BackgroundColor.Value);

        var newInlineStart = BackgroundColor?.Color.IsTransparent() ?? true
            ? inlineStart
            : inlineStart.EnsureContrast(BackgroundColor.Value);

        return BorderColor is null
            ? this with
            {
                BorderColor = new StyleGroupBorderColor(newBlockStart, newBlockEnd, newInlineStart, newInlineEnd)
            }
            : this with
            {
                BorderColor = BorderColor
                    .SetBlockEnd(newBlockEnd)
                    .SetBlockStart(newBlockStart)
                    .SetInlineEnd(newInlineEnd)
                    .SetInlineStart(newInlineStart)
            };
    }

    public ThemeGroupPalette SetCaretColor(StyleValueColor? value)
        => this with
        {
            CaretColor = BackgroundColor?.Color.IsTransparent() ?? true
                ? value
                : value?.EnsureContrast(BackgroundColor.Value)
        };

    public ThemeGroupPalette SetColor(StyleValueColor? value)
        => this with
        {
            Color = BackgroundColor?.Color.IsTransparent() ?? true
                ? value
                : value?.EnsureContrast(BackgroundColor.Value)
        };

    public ThemeGroupPalette SetTextDecorationColor(StyleValueColor? value)
        => this with
        {
            TextDecorationColor = BackgroundColor?.Color.IsTransparent() ?? true
                ? value
                : value?.EnsureContrast(BackgroundColor.Value)
        };

    public string ToCss(string? varPrefix = "") => BuildCss(new CssBuilder(), varPrefix).ToString();

    public ThemeGroupPalette ToDisabled()
        => SetBackgroundColor(BackgroundColor?.ToDisabled())
            .SetAccentColor(AccentColor?.ToDisabled())
            .SetBorderColor(BorderColor?.ToDisabled())
            .SetCaretColor(CaretColor?.ToDisabled())
            .SetColor(Color?.ToDisabled())
            .SetTextDecorationColor(TextDecorationColor?.ToDisabled());

    public ThemeGroupPalette ToDragged()
        => SetBackgroundColor(BackgroundColor?.ToDragged())
            .SetAccentColor(AccentColor?.ToDragged())
            .SetBorderColor(BorderColor?.ToDragged())
            .SetCaretColor(CaretColor?.ToDragged())
            .SetColor(Color?.ToDragged())
            .SetTextDecorationColor(TextDecorationColor?.ToDragged());

    public ThemeGroupPalette ToFocused()
        => SetBackgroundColor(BackgroundColor?.ToFocused())
            .SetAccentColor(AccentColor?.ToFocused())
            .SetBorderColor(BorderColor?.ToFocused())
            .SetCaretColor(CaretColor?.ToFocused())
            .SetColor(Color?.ToFocused())
            .SetTextDecorationColor(TextDecorationColor?.ToFocused());

    public ThemeGroupPalette ToHovered()
        => SetBackgroundColor(BackgroundColor?.ToHovered())
            .SetAccentColor(AccentColor?.ToHovered())
            .SetBorderColor(BorderColor?.ToHovered())
            .SetCaretColor(CaretColor?.ToHovered())
            .SetColor(Color?.ToHovered())
            .SetTextDecorationColor(TextDecorationColor?.ToHovered());

    public ThemeGroupPalette ToPressed()
        => SetBackgroundColor(BackgroundColor?.ToPressed())
            .SetAccentColor(AccentColor?.ToPressed())
            .SetBorderColor(BorderColor?.ToPressed())
            .SetCaretColor(CaretColor?.ToPressed())
            .SetColor(Color?.ToPressed())
            .SetTextDecorationColor(TextDecorationColor?.ToPressed());

    public ThemeGroupPalette ToVisited()
        => SetBackgroundColor(BackgroundColor?.ToVisited())
            .SetAccentColor(AccentColor?.ToVisited())
            .SetBorderColor(BorderColor?.ToVisited())
            .SetCaretColor(CaretColor?.ToVisited())
            .SetColor(Color?.ToVisited())
            .SetTextDecorationColor(TextDecorationColor?.ToVisited());
}
