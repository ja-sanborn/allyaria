namespace Allyaria.Theming.Types;

public readonly record struct StyleLayout(
    StyleLayoutPosition? Position = null,
    StyleLayoutOverflow? Overflow = null,
    StyleLayoutAlignment? Alignment = null,
    StyleLayoutSize? Sizing = null,
    StyleLayoutBorder? Borders = null
)
{
    public StyleLayout Cascade(StyleLayout? value = null)
        => value is null
            ? this
            : Cascade(
                value.Value.Position,
                value.Value.Overflow,
                value.Value.Alignment,
                value.Value.Sizing,
                value.Value.Borders
            );

    public StyleLayout Cascade(StyleLayoutPosition? position = null,
        StyleLayoutOverflow? overflow = null,
        StyleLayoutAlignment? alignment = null,
        StyleLayoutSize? sizing = null,
        StyleLayoutBorder? borders = null)
        => this with
        {
            Alignment = Alignment?.Cascade(alignment) ?? alignment,
            Borders = Borders?.Cascade(borders) ?? borders,
            Overflow = Overflow?.Cascade(overflow) ?? overflow,
            Position = Position?.Cascade(position) ?? position,
            Sizing = Sizing?.Cascade(sizing) ?? sizing
        };

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.Append(Alignment?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(Borders?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(Overflow?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(Position?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(Sizing?.ToCss(varPrefix) ?? string.Empty);

        return builder.ToString();
    }
}
