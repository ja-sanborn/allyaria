namespace Allyaria.Theming.Types;

public readonly record struct StyleLayoutOverflow(
    ThemeString? OverflowX = null,
    ThemeString? OverflowY = null,
    ThemeString? OverflowWrap = null,
    ThemeString? OverscrollX = null,
    ThemeString? OverscrollY = null,
    ThemeString? WhiteSpace = null
)
{
    public StyleLayoutOverflow Cascade(StyleLayoutOverflow? value = null)
        => value is null
            ? this
            : Cascade(
                value.Value.OverflowX,
                value.Value.OverflowY,
                value.Value.OverflowWrap,
                value.Value.OverscrollX,
                value.Value.OverscrollY,
                value.Value.WhiteSpace
            );

    public StyleLayoutOverflow Cascade(ThemeString? overflowX = null,
        ThemeString? overflowY = null,
        ThemeString? overflowWrap = null,
        ThemeString? overscrollX = null,
        ThemeString? overscrollY = null,
        ThemeString? whiteSpace = null)
        => this with
        {
            OverflowX = OverflowX ?? overflowX,
            OverflowY = OverflowY ?? overflowY,
            OverflowWrap = OverflowWrap ?? overflowWrap,
            OverscrollX = OverscrollX ?? overscrollX,
            OverscrollY = OverscrollY ?? overscrollY,
            WhiteSpace = WhiteSpace ?? whiteSpace
        };

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("overflow-x", OverflowX, varPrefix);
        builder.ToCss("overflow-y", OverflowY, varPrefix);
        builder.ToCss("overflow-wrap", OverflowWrap, varPrefix);
        builder.ToCss("overscroll-x", OverscrollX, varPrefix);
        builder.ToCss("overscroll-y", OverscrollY, varPrefix);
        builder.ToCss("white-space", WhiteSpace, varPrefix);

        return builder.ToString();
    }
}
