namespace Allyaria.Theming.Types;

public readonly record struct StyleState(
    StylePalette? Palette = null,
    StyleLayout? Layout = null,
    StyleTypography? Typography = null
)
{
    public StyleState Cascade(StyleState value) => Cascade(value.Palette, value.Layout, value.Typography);

    public StyleState Cascade(StylePalette? palette = null,
        StyleLayout? layout = null,
        StyleTypography? typography = null)
        => this with
        {
            Palette = Palette?.Cascade(palette) ?? palette,
            Layout = Layout?.Cascade(layout) ?? layout,
            Typography = Typography?.Cascade(typography) ?? typography
        };

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.Append(Layout?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(Palette?.ToCss(varPrefix) ?? string.Empty);
        builder.Append(Typography?.ToCss(varPrefix) ?? string.Empty);

        return builder.ToString();
    }

    public StyleState ToDisabled()
        => this with
        {
            Palette = Palette?.ToDisabled()
        };

    public StyleState ToDragged()
        => this with
        {
            Palette = Palette?.ToDragged()
        };

    public StyleState ToFocused()
        => this with
        {
            Palette = Palette?.ToFocused()
        };

    public StyleState ToHovered()
        => this with
        {
            Palette = Palette?.ToHovered()
        };

    public StyleState ToPressed()
        => this with
        {
            Palette = Palette?.ToPressed()
        };
}
