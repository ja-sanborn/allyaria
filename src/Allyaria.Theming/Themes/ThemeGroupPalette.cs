namespace Allyaria.Theming.Themes;

public sealed partial record ThemeGroupPalette
{
    public ThemeGroupPalette(StyleValueColor? accentColor = null,
        StyleValueColor? backgroundColor = null,
        StyleGroupBorderColor? borderColor = null,
        StyleValueColor? caretColor = null,
        StyleValueColor? color = null,
        StyleValueColor? outlineColor = null,
        StyleValueColor? textDecorationColor = null)
    {
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

        AccentColor = AccentColor?.EnsureContrast(surface: BackgroundColor.Value);
        BorderColor = BorderColor?.EnsureContrast(backgroundColor: BackgroundColor.Value);
        CaretColor = CaretColor?.EnsureContrast(surface: BackgroundColor.Value);
        Color = Color?.EnsureContrast(surface: BackgroundColor.Value);
        OutlineColor = OutlineColor?.EnsureContrast(surface: BackgroundColor.Value);
        TextDecorationColor = TextDecorationColor?.EnsureContrast(surface: BackgroundColor.Value);
    }

    public StyleValueColor? AccentColor { get; init; }

    public StyleValueColor? BackgroundColor { get; init; }

    public StyleGroupBorderColor? BorderColor { get; init; }

    public StyleValueColor? CaretColor { get; init; }

    public StyleValueColor? Color { get; init; }

    public StyleValueColor? OutlineColor { get; init; }

    public StyleValueColor? TextDecorationColor { get; init; }

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

    public ThemeGroupPalette Merge(ThemeGroupPalette other)
        => SetAccentColor(value: other.AccentColor ?? AccentColor)
            .SetBackgroundColor(value: other.BackgroundColor ?? BackgroundColor)
            .SetBorderColor(value: other.BorderColor ?? BorderColor)
            .SetCaretColor(value: other.CaretColor ?? CaretColor)
            .SetColor(value: other.Color ?? Color)
            .SetOutlineColor(value: other.OutlineColor ?? OutlineColor)
            .SetTextDecorationColor(value: other.TextDecorationColor ?? TextDecorationColor);

    public ThemeGroupPalette SetAccentColor(StyleValueColor? value)
        => this with
        {
            AccentColor = BackgroundColor?.Color.IsTransparent() ?? true
                ? value
                : value?.EnsureContrast(surface: BackgroundColor.Value)
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
            .SetAccentColor(value: AccentColor)
            .SetBorderColor(value: BorderColor)
            .SetCaretColor(value: CaretColor)
            .SetColor(value: Color)
            .SetOutlineColor(value: OutlineColor)
            .SetTextDecorationColor(value: TextDecorationColor);
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
            : blockEnd.EnsureContrast(surface: BackgroundColor.Value);

        var newBlockStart = BackgroundColor?.Color.IsTransparent() ?? true
            ? blockStart
            : blockStart.EnsureContrast(surface: BackgroundColor.Value);

        var newInlineEnd = BackgroundColor?.Color.IsTransparent() ?? true
            ? inlineEnd
            : inlineEnd.EnsureContrast(surface: BackgroundColor.Value);

        var newInlineStart = BackgroundColor?.Color.IsTransparent() ?? true
            ? inlineStart
            : inlineStart.EnsureContrast(surface: BackgroundColor.Value);

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
                    .SetBlockEnd(value: newBlockEnd)
                    .SetBlockStart(value: newBlockStart)
                    .SetInlineEnd(value: newInlineEnd)
                    .SetInlineStart(value: newInlineStart)
            };
    }

    public ThemeGroupPalette SetCaretColor(StyleValueColor? value)
        => this with
        {
            CaretColor = BackgroundColor?.Color.IsTransparent() ?? true
                ? value
                : value?.EnsureContrast(surface: BackgroundColor.Value)
        };

    public ThemeGroupPalette SetColor(StyleValueColor? value)
        => this with
        {
            Color = BackgroundColor?.Color.IsTransparent() ?? true
                ? value
                : value?.EnsureContrast(surface: BackgroundColor.Value)
        };

    public ThemeGroupPalette SetOutlineColor(StyleValueColor? value)
        => this with
        {
            OutlineColor = BackgroundColor?.Color.IsTransparent() ?? true
                ? value
                : value?.EnsureContrast(surface: BackgroundColor.Value)
        };

    public ThemeGroupPalette SetTextDecorationColor(StyleValueColor? value)
        => this with
        {
            TextDecorationColor = BackgroundColor?.Color.IsTransparent() ?? true
                ? value
                : value?.EnsureContrast(surface: BackgroundColor.Value)
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();

    public ThemeGroupPalette ToDisabled()
        => SetBackgroundColor(value: BackgroundColor?.ToDisabled())
            .SetAccentColor(value: AccentColor?.ToDisabled())
            .SetBorderColor(value: BorderColor?.ToDisabled())
            .SetCaretColor(value: CaretColor?.ToDisabled())
            .SetColor(value: Color?.ToDisabled())
            .SetOutlineColor(value: OutlineColor?.ToDisabled())
            .SetTextDecorationColor(value: TextDecorationColor?.ToDisabled());

    public ThemeGroupPalette ToDragged()
        => SetBackgroundColor(value: BackgroundColor?.ToDragged())
            .SetAccentColor(value: AccentColor?.ToDragged())
            .SetBorderColor(value: BorderColor?.ToDragged())
            .SetCaretColor(value: CaretColor?.ToDragged())
            .SetColor(value: Color?.ToDragged())
            .SetOutlineColor(value: OutlineColor?.ToDragged())
            .SetTextDecorationColor(value: TextDecorationColor?.ToDragged());

    public ThemeGroupPalette ToFocused()
        => SetBackgroundColor(value: BackgroundColor?.ToFocused())
            .SetAccentColor(value: AccentColor?.ToFocused())
            .SetBorderColor(value: BorderColor?.ToFocused())
            .SetCaretColor(value: CaretColor?.ToFocused())
            .SetColor(value: Color?.ToFocused())
            .SetOutlineColor(value: OutlineColor?.ToFocused())
            .SetTextDecorationColor(value: TextDecorationColor?.ToFocused());

    public ThemeGroupPalette ToHovered()
        => SetBackgroundColor(value: BackgroundColor?.ToHovered())
            .SetAccentColor(value: AccentColor?.ToHovered())
            .SetBorderColor(value: BorderColor?.ToHovered())
            .SetCaretColor(value: CaretColor?.ToHovered())
            .SetColor(value: Color?.ToHovered())
            .SetOutlineColor(value: OutlineColor?.ToHovered())
            .SetTextDecorationColor(value: TextDecorationColor?.ToHovered());

    public ThemeGroupPalette ToPressed()
        => SetBackgroundColor(value: BackgroundColor?.ToPressed())
            .SetAccentColor(value: AccentColor?.ToPressed())
            .SetBorderColor(value: BorderColor?.ToPressed())
            .SetCaretColor(value: CaretColor?.ToPressed())
            .SetColor(value: Color?.ToPressed())
            .SetOutlineColor(value: OutlineColor?.ToPressed())
            .SetTextDecorationColor(value: TextDecorationColor?.ToPressed());

    public ThemeGroupPalette ToVisited()
        => SetBackgroundColor(value: BackgroundColor?.ToVisited())
            .SetAccentColor(value: AccentColor?.ToVisited())
            .SetBorderColor(value: BorderColor?.ToVisited())
            .SetCaretColor(value: CaretColor?.ToVisited())
            .SetColor(value: Color?.ToVisited())
            .SetOutlineColor(value: OutlineColor?.ToVisited())
            .SetTextDecorationColor(value: TextDecorationColor?.ToVisited());
}
