namespace Allyaria.Theming.Themes;

public sealed record ThemeGroupTypographyDisplay(
    StyleValueString? Hyphens = null,
    StyleValueNumber? LetterSpacing = null,
    StyleValueString? LineBreak = null,
    StyleValueNumber? LineHeight = null,
    StyleValueString? TextAlign = null,
    StyleValueString? TextOverflow = null,
    StyleValueString? VerticalAlign = null,
    StyleValueString? WhiteSpace = null,
    StyleValueString? WordBreak = null,
    StyleValueNumber? WordSpacing = null
)
{
    public static readonly ThemeGroupTypographyDisplay Empty = new();

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder
            .Add(propertyName: "hyphens", value: Hyphens, varPrefix: varPrefix)
            .Add(propertyName: "letter-spacing", value: LetterSpacing, varPrefix: varPrefix)
            .Add(propertyName: "line-break", value: LineBreak, varPrefix: varPrefix)
            .Add(propertyName: "line-height", value: LineHeight, varPrefix: varPrefix)
            .Add(propertyName: "text-align", value: TextAlign, varPrefix: varPrefix)
            .Add(propertyName: "text-overflow", value: TextOverflow, varPrefix: varPrefix)
            .Add(propertyName: "vertical-align", value: VerticalAlign, varPrefix: varPrefix)
            .Add(propertyName: "white-space", value: WhiteSpace, varPrefix: varPrefix)
            .Add(propertyName: "word-break", value: WordBreak, varPrefix: varPrefix)
            .Add(propertyName: "word-spacing", value: WordSpacing, varPrefix: varPrefix);

        return builder;
    }

    public ThemeGroupTypographyDisplay Merge(ThemeGroupTypographyDisplay other)
        => SetHyphens(value: other.Hyphens ?? Hyphens)
            .SetLetterSpacing(value: other.LetterSpacing ?? LetterSpacing)
            .SetLineBreak(value: other.LineBreak ?? LineBreak)
            .SetLineHeight(value: other.LineHeight ?? LineHeight)
            .SetTextAlign(value: other.TextAlign ?? TextAlign)
            .SetTextOverflow(value: other.TextOverflow ?? TextOverflow)
            .SetVerticalAlign(value: other.VerticalAlign ?? VerticalAlign)
            .SetWhiteSpace(value: other.WhiteSpace ?? WhiteSpace)
            .SetWordBreak(value: other.WordBreak ?? WordBreak)
            .SetWordSpacing(value: other.WordSpacing ?? WordSpacing);

    public ThemeGroupTypographyDisplay SetHyphens(StyleValueString? value)
        => this with
        {
            Hyphens = value
        };

    public ThemeGroupTypographyDisplay SetLetterSpacing(StyleValueNumber? value)
        => this with
        {
            LetterSpacing = value
        };

    public ThemeGroupTypographyDisplay SetLineBreak(StyleValueString? value)
        => this with
        {
            LineBreak = value
        };

    public ThemeGroupTypographyDisplay SetLineHeight(StyleValueNumber? value)
        => this with
        {
            LineHeight = value
        };

    public ThemeGroupTypographyDisplay SetTextAlign(StyleValueString? value)
        => this with
        {
            TextAlign = value
        };

    public ThemeGroupTypographyDisplay SetTextOverflow(StyleValueString? value)
        => this with
        {
            TextOverflow = value
        };

    public ThemeGroupTypographyDisplay SetVerticalAlign(StyleValueString? value)
        => this with
        {
            VerticalAlign = value
        };

    public ThemeGroupTypographyDisplay SetWhiteSpace(StyleValueString? value)
        => this with
        {
            WhiteSpace = value
        };

    public ThemeGroupTypographyDisplay SetWordBreak(StyleValueString? value)
        => this with
        {
            WordBreak = value
        };

    public ThemeGroupTypographyDisplay SetWordSpacing(StyleValueNumber? value)
        => this with
        {
            WordSpacing = value
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
