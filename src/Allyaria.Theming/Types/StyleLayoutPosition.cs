namespace Allyaria.Theming.Types;

public readonly record struct StyleLayoutPosition(
    ThemeString? Display = null,
    ThemeString? Position = null,
    ThemeString? Float = null,
    ThemeString? BoxSizing = null,
    ThemeString? WritingMode = null,
    ThemeNumber? ZIndex = null
)
{
    public StyleLayoutPosition Cascade(StyleLayoutPosition? value = null)
        => value is null
            ? this
            : Cascade(
                value.Value.Display,
                value.Value.Position,
                value.Value.Float,
                value.Value.BoxSizing,
                value.Value.WritingMode,
                value.Value.ZIndex
            );

    public StyleLayoutPosition Cascade(ThemeString? display = null,
        ThemeString? position = null,
        ThemeString? floats = null,
        ThemeString? boxSizing = null,
        ThemeString? writingMode = null,
        ThemeNumber? zIndex = null)
        => this with
        {
            BoxSizing = boxSizing ?? BoxSizing,
            Display = display ?? Display,
            Float = floats ?? Float,
            Position = position ?? Position,
            WritingMode = writingMode ?? WritingMode,
            ZIndex = zIndex ?? ZIndex
        };

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss("box-sizing", BoxSizing, varPrefix);
        builder.ToCss("display", Display, varPrefix);
        builder.ToCss("float", Float, varPrefix);
        builder.ToCss("position", Position, varPrefix);
        builder.ToCss("writing-mode", WritingMode, varPrefix);
        builder.ToCss("z-index", ZIndex, varPrefix);

        return builder.ToString();
    }
}
