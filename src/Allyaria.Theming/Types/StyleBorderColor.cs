namespace Allyaria.Theming.Types;

public readonly record struct StyleBorderColor(
    ThemeColor? BlockStart = null,
    ThemeColor? InlineStart = null,
    ThemeColor? BlockEnd = null,
    ThemeColor? InlineEnd = null
)
{
    public StyleBorderColor Cascade(StyleBorderColor? value = null)
        => value is null
            ? this
            : Cascade(value.Value.BlockStart, value.Value.InlineStart, value.Value.BlockEnd, value.Value.InlineEnd);

    public StyleBorderColor Cascade(ThemeColor? blockStart = null,
        ThemeColor? inlineStart = null,
        ThemeColor? blockEnd = null,
        ThemeColor? inlineEnd = null)
        => this with
        {
            BlockEnd = blockEnd ?? BlockEnd,
            BlockStart = blockStart ?? BlockStart,
            InlineEnd = inlineEnd ?? InlineEnd,
            InlineStart = inlineStart ?? InlineStart
        };

    public static StyleBorderColor FromSingle(ThemeColor value) => new(value, value, value, value);

    public static StyleBorderColor FromSymmetric(ThemeColor block, ThemeColor inline)
        => new(block, inline, block, inline);

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("border-block-end-color", BlockEnd, varPrefix);
        builder.ToCss("border-block-start-color", BlockStart, varPrefix);
        builder.ToCss("border-inline-end-color", InlineEnd, varPrefix);
        builder.ToCss("border-inline-start-color", InlineStart, varPrefix);

        return builder.ToString();
    }
}
