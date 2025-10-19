namespace Allyaria.Theming.Types;

public readonly record struct StyleMargin(
    ThemeNumber? BlockStart = null,
    ThemeNumber? InlineStart = null,
    ThemeNumber? BlockEnd = null,
    ThemeNumber? InlineEnd = null
)
{
    public StyleMargin Cascade(StyleMargin? value = null)
        => value is null
            ? this
            : Cascade(value.Value.BlockStart, value.Value.InlineStart, value.Value.BlockEnd, value.Value.InlineEnd);

    public StyleMargin Cascade(ThemeNumber? blockStart = null,
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

    public static StyleMargin FromSingle(ThemeNumber value) => new(value, value, value, value);

    public static StyleMargin FromSymmetric(ThemeNumber block, ThemeNumber inline)
        => new(block, inline, block, inline);

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("margin-block-end-width", BlockEnd, varPrefix);
        builder.ToCss("margin-block-start-width", BlockStart, varPrefix);
        builder.ToCss("margin-inline-end-width", InlineEnd, varPrefix);
        builder.ToCss("margin-inline-start-width", InlineStart, varPrefix);

        return builder.ToString();
    }
}
