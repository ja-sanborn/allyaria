namespace Allyaria.Theming.Types;

public readonly record struct StylePadding(
    ThemeNumber? BlockStart = null,
    ThemeNumber? InlineStart = null,
    ThemeNumber? BlockEnd = null,
    ThemeNumber? InlineEnd = null
)
{
    public StylePadding Cascade(StylePadding? value = null)
        => value is null
            ? this
            : Cascade(value.Value.BlockStart, value.Value.InlineStart, value.Value.BlockEnd, value.Value.InlineEnd);

    public StylePadding Cascade(ThemeNumber? blockStart = null,
        ThemeNumber? inlineStart = null,
        ThemeNumber? blockEnd = null,
        ThemeNumber? inlineEnd = null)
        => this with
        {
            BlockEnd = blockEnd ?? BlockEnd,
            BlockStart = blockStart ?? BlockStart,
            InlineEnd = inlineEnd ?? InlineEnd,
            InlineStart = inlineStart ?? InlineStart
        };

    public static StylePadding FromSingle(ThemeNumber value) => new(value, value, value, value);

    public static StylePadding FromSymmetric(ThemeNumber block, ThemeNumber inline)
        => new(block, inline, block, inline);

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("padding-block-end-width", BlockEnd, varPrefix);
        builder.ToCss("padding-block-start-width", BlockStart, varPrefix);
        builder.ToCss("padding-inline-end-width", InlineEnd, varPrefix);
        builder.ToCss("padding-inline-start-width", InlineStart, varPrefix);

        return builder.ToString();
    }
}
