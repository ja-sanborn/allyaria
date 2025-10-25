namespace Allyaria.Theming.Themes;

public sealed partial record ThemeGroupPalette
{
    public ThemeGroupPalette(ThemeType themeType = ThemeType.Light,
        PaletteType paletteType = PaletteType.Surface,
        StyleValueColor? accentColor = null,
        StyleValueColor? backgroundColor = null,
        StyleGroupBorderColor? borderColor = null,
        StyleValueColor? caretColor = null,
        StyleValueColor? color = null,
        StyleValueColor? outlineColor = null,
        StyleValueColor? textDecorationColor = null)
    {
        ThemeType = themeType;
        PaletteType = paletteType;

        AccentColor = accentColor;
        BackgroundColor = backgroundColor;
        BorderColor = borderColor;
        CaretColor = caretColor;
        Color = color;
        OutlineColor = outlineColor;
        TextDecorationColor = textDecorationColor;

        if (BackgroundColor?.Color.IsTransparent() ?? true)
        {
            return;
        }

        AccentColor = AccentColor?.EnsureContrast(BackgroundColor.Value);
        BorderColor = BorderColor?.EnsureContrast(BackgroundColor.Value);
        CaretColor = CaretColor?.EnsureContrast(BackgroundColor.Value);
        Color = Color?.EnsureContrast(BackgroundColor.Value);
        OutlineColor = OutlineColor?.EnsureContrast(BackgroundColor.Value);
        TextDecorationColor = TextDecorationColor?.EnsureContrast(BackgroundColor.Value);
    }

    public StyleValueColor? AccentColor { get; init; }

    public StyleValueColor? BackgroundColor { get; init; }

    public StyleGroupBorderColor? BorderColor { get; init; }

    public StyleValueColor? CaretColor { get; init; }

    public StyleValueColor? Color { get; init; }

    public StyleValueColor? OutlineColor { get; init; }

    public PaletteType PaletteType { get; init; } = PaletteType.Surface;

    public StyleValueColor? TextDecorationColor { get; init; }

    public ThemeType ThemeType { get; init; } = ThemeType.Light;

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder
            .Add(propertyName: "accent-color", value: AccentColor, varPrefix: varPrefix)
            .Add(propertyName: "background-color", value: BackgroundColor, varPrefix: varPrefix)
            .Add(propertyName: "caret-color", value: CaretColor, varPrefix: varPrefix)
            .Add(propertyName: "color", value: Color, varPrefix: varPrefix)
            .Add(propertyName: "outline-color", value: OutlineColor, varPrefix: varPrefix)
            .Add(propertyName: "text-decoration-color", value: TextDecorationColor, varPrefix: varPrefix);

        builder = BorderColor?.BuildCss(builder: builder, varPrefix: varPrefix) ?? builder;

        return builder;
    }

    public static ThemeGroupPalette FromColorPalette(ColorPalette colorPalette,
        ThemeType themeType,
        PaletteType paletteType)
    {
        var backgroundColor = colorPalette.GetColor(themeType: themeType, paletteType: paletteType);
        var color = backgroundColor.ToForeground().EnsureContrast(backgroundColor);
        var caretColor = new StyleValueColor(color.Color);
        var accentColor = color.ToAccent().EnsureContrast(backgroundColor);
        var borderColor = new StyleGroupBorderColor(backgroundColor);
        var outlineColor = new StyleValueColor(accentColor.Color);
        var textDecorationColor = new StyleValueColor(accentColor.Color);

        if (themeType is ThemeType.HighContrastDark)
        {
            backgroundColor = CssColors.BackgroundColorHighContrastDark;
            color = CssColors.ForegroundColorHighContrastDark;
            caretColor = color;
            accentColor = CssColors.AccentColorHighContrastDark;
            borderColor = new StyleGroupBorderColor(accentColor);
            outlineColor = accentColor;
            textDecorationColor = color;
        }
        else if (themeType is ThemeType.HighContrastLight)
        {
            backgroundColor = CssColors.BackgroundColorHighContrastLight;
            color = CssColors.ForegroundColorHighContrastLight;
            caretColor = color;
            accentColor = CssColors.AccentColorHighContrastLight;
            borderColor = new StyleGroupBorderColor(accentColor);
            outlineColor = accentColor;
            textDecorationColor = color;
        }

        return new ThemeGroupPalette(
            themeType: themeType,
            paletteType: paletteType,
            accentColor: accentColor,
            backgroundColor: backgroundColor,
            borderColor: borderColor,
            caretColor: caretColor,
            color: color,
            outlineColor: outlineColor,
            textDecorationColor: textDecorationColor
        );
    }

    public ThemeGroupPalette Merge(ThemeGroupPalette other)
        => SetAccentColor(other.AccentColor ?? AccentColor)
            .SetBackgroundColor(other.BackgroundColor ?? BackgroundColor)
            .SetBorderColor(other.BorderColor ?? BorderColor)
            .SetCaretColor(other.CaretColor ?? CaretColor)
            .SetColor(other.Color ?? Color)
            .SetOutlineColor(other.OutlineColor ?? OutlineColor)
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
            .SetOutlineColor(OutlineColor)
            .SetTextDecorationColor(TextDecorationColor);
    }

    public ThemeGroupPalette SetBorderColor(StyleGroupBorderColor? value)
        => value is not null
            ? SetBorderColor(
                blockStart: value.Value.BlockStart, blockEnd: value.Value.BlockEnd,
                inlineStart: value.Value.InlineStart, inlineEnd: value.Value.InlineEnd
            )
            : this with
            {
                BorderColor = null
            };

    public ThemeGroupPalette SetBorderColor(StyleValueColor? value)
        => value is not null
            ? SetBorderColor(
                blockStart: value.Value.Value, blockEnd: value.Value.Value, inlineStart: value.Value.Value,
                inlineEnd: value.Value.Value
            )
            : this with
            {
                BorderColor = null
            };

    public ThemeGroupPalette SetBorderColor(StyleValueColor block, StyleValueColor inline)
        => SetBorderColor(blockStart: block, blockEnd: block, inlineStart: inline, inlineEnd: inline);

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
                BorderColor = new StyleGroupBorderColor(
                    blockStart: newBlockStart, blockEnd: newBlockEnd, inlineStart: newInlineStart,
                    inlineEnd: newInlineEnd
                )
            }
            : this with
            {
                BorderColor = BorderColor.Value
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

    public ThemeGroupPalette SetOutlineColor(StyleValueColor? value)
        => this with
        {
            OutlineColor = BackgroundColor?.Color.IsTransparent() ?? true
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

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();

    public ThemeGroupPalette ToDisabled()
        => SetBackgroundColor(BackgroundColor?.ToDisabled())
            .SetAccentColor(AccentColor?.ToDisabled())
            .SetBorderColor(BorderColor?.ToDisabled())
            .SetCaretColor(CaretColor?.ToDisabled())
            .SetColor(Color?.ToDisabled())
            .SetOutlineColor(OutlineColor?.ToDisabled())
            .SetTextDecorationColor(TextDecorationColor?.ToDisabled());

    public ThemeGroupPalette ToDragged()
        => SetBackgroundColor(BackgroundColor?.ToDragged())
            .SetAccentColor(AccentColor?.ToDragged())
            .SetBorderColor(BorderColor?.ToDragged())
            .SetCaretColor(CaretColor?.ToDragged())
            .SetColor(Color?.ToDragged())
            .SetOutlineColor(OutlineColor?.ToDragged())
            .SetTextDecorationColor(TextDecorationColor?.ToDragged());

    public ThemeGroupPalette ToFocused()
        => SetBackgroundColor(BackgroundColor?.ToFocused())
            .SetAccentColor(AccentColor?.ToFocused())
            .SetBorderColor(BorderColor?.ToFocused())
            .SetCaretColor(CaretColor?.ToFocused())
            .SetColor(Color?.ToFocused())
            .SetOutlineColor(OutlineColor?.ToFocused())
            .SetTextDecorationColor(TextDecorationColor?.ToFocused());

    public ThemeGroupPalette ToHovered()
        => SetBackgroundColor(BackgroundColor?.ToHovered())
            .SetAccentColor(AccentColor?.ToHovered())
            .SetBorderColor(BorderColor?.ToHovered())
            .SetCaretColor(CaretColor?.ToHovered())
            .SetColor(Color?.ToHovered())
            .SetOutlineColor(OutlineColor?.ToHovered())
            .SetTextDecorationColor(TextDecorationColor?.ToHovered());

    public ThemeGroupPalette ToPressed()
        => SetBackgroundColor(BackgroundColor?.ToPressed())
            .SetAccentColor(AccentColor?.ToPressed())
            .SetBorderColor(BorderColor?.ToPressed())
            .SetCaretColor(CaretColor?.ToPressed())
            .SetColor(Color?.ToPressed())
            .SetOutlineColor(OutlineColor?.ToPressed())
            .SetTextDecorationColor(TextDecorationColor?.ToPressed());

    public ThemeGroupPalette ToVisited()
        => SetBackgroundColor(BackgroundColor?.ToVisited())
            .SetAccentColor(AccentColor?.ToVisited())
            .SetBorderColor(BorderColor?.ToVisited())
            .SetCaretColor(CaretColor?.ToVisited())
            .SetColor(Color?.ToVisited())
            .SetOutlineColor(OutlineColor?.ToVisited())
            .SetTextDecorationColor(TextDecorationColor?.ToVisited());
}
