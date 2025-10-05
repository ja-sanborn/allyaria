using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Values;
using System.Text;

namespace Allyaria.Theming.Styles;

internal readonly record struct AllyariaPaletteState
{
    public AllyariaPaletteState()
        : this(null) { }

    public AllyariaPaletteState(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaColorValue? borderColor = null)
    {
        BackgroundColor = backgroundColor ?? StyleDefaults.BackgroundColorLight;
        ForegroundColor = foregroundColor ?? StyleDefaults.ForegroundColorLight;
        BorderColor = borderColor ?? BackgroundColor;

        var result = ColorHelper.EnsureMinimumContrast(ForegroundColor, BackgroundColor, 4.5);
        ForegroundColor = result.ForegroundColor;
    }

    public AllyariaColorValue BackgroundColor { get; init; }

    public AllyariaColorValue BorderColor { get; init; }

    public AllyariaColorValue ForegroundColor { get; init; }

    public AllyariaPaletteState Cascade(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaColorValue? borderColor = null)
    {
        var next = this with
        {
            BackgroundColor = backgroundColor ?? BackgroundColor,
            ForegroundColor = foregroundColor ?? ForegroundColor,
            BorderColor = borderColor ?? BorderColor
        };

        var contrasted = ColorHelper.EnsureMinimumContrast(next.ForegroundColor, next.BackgroundColor, 4.5);

        return next with
        {
            ForegroundColor = contrasted.ForegroundColor
        };
    }

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss(ForegroundColor, "color", varPrefix);
        builder.ToCss(BackgroundColor, "background-color", varPrefix);
        builder.ToCss(BorderColor, "border-color", varPrefix);

        return builder.ToString();
    }
}
