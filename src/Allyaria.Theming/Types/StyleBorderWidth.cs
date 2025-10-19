namespace Allyaria.Theming.Types;

public readonly record struct StyleBorderWidth(
    ThemeNumber? BlockStart = null,
    ThemeNumber? InlineStart = null,
    ThemeNumber? BlockEnd = null,
    ThemeNumber? InlineEnd = null
)
{
    public StyleBorderWidth Cascade(StyleBorderWidth? value = null)
        => value is null
            ? this
            : Cascade(value.Value.BlockStart, value.Value.InlineStart, value.Value.BlockEnd, value.Value.InlineEnd);

    public StyleBorderWidth Cascade(ThemeNumber? blockStart = null,
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

    public static StyleBorderWidth FromSingle(ThemeNumber value) => new(value, value, value, value);

    public static StyleBorderWidth FromSymmetric(ThemeNumber block, ThemeNumber inline)
        => new(block, inline, block, inline);

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("border-block-end-width", BlockEnd, varPrefix);
        builder.ToCss("border-block-start-width", BlockStart, varPrefix);
        builder.ToCss("border-inline-end-width", InlineEnd, varPrefix);
        builder.ToCss("border-inline-start-width", InlineStart, varPrefix);

        return builder.ToString();
    }
}
