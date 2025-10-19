namespace Allyaria.Theming.Types;

public readonly record struct StyleBorderStyle(
    ThemeString? BlockStart = null,
    ThemeString? InlineStart = null,
    ThemeString? BlockEnd = null,
    ThemeString? InlineEnd = null
)
{
    public StyleBorderStyle Cascade(StyleBorderStyle? value = null)
        => value is null
            ? this
            : Cascade(value.Value.BlockStart, value.Value.InlineStart, value.Value.BlockEnd, value.Value.InlineEnd);

    public StyleBorderStyle Cascade(ThemeString? blockStart = null,
        ThemeString? inlineStart = null,
        ThemeString? blockEnd = null,
        ThemeString? inlineEnd = null)
        => this with
        {
            BlockEnd = blockEnd ?? BlockEnd,
            BlockStart = blockStart ?? BlockStart,
            InlineEnd = inlineEnd ?? InlineEnd,
            InlineStart = inlineStart ?? InlineStart
        };

    public static StyleBorderStyle FromSingle(ThemeString value) => new(value, value, value, value);

    public static StyleBorderStyle FromSymmetric(ThemeString block, ThemeString inline)
        => new(block, inline, block, inline);

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("border-block-end-style", BlockEnd, varPrefix);
        builder.ToCss("border-block-start-style", BlockStart, varPrefix);
        builder.ToCss("border-inline-end-style", InlineEnd, varPrefix);
        builder.ToCss("border-inline-start-style", InlineStart, varPrefix);

        return builder.ToString();
    }
}
