using Allyaria.Theming.Values;
using System.Diagnostics.CodeAnalysis;

namespace Allyaria.Theming.Constants;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class StyleDefaults
{
    public static readonly AllyariaNumberValue BorderRadius = Sizing.Size2;

    public static readonly AllyariaStringValue BorderStyle = Constants.BorderStyle.Solid;

    public static readonly AllyariaNumberValue BorderWidth = Sizing.Size0;

    public static readonly AllyariaStringValue FontFamily = new(
        "system-ui, -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Helvetica, Arial, sans-serif"
    );

    public static readonly AllyariaNumberValue FontSize = Sizing.Size3;

    public static readonly AllyariaStringValue FontStyle = Constants.FontStyle.Normal;

    public static readonly AllyariaStringValue FontWeight = Constants.FontWeight.Normal;

    public static readonly AllyariaNumberValue LetterSpacing = new("0.5px");

    public static readonly AllyariaNumberValue LineHeight = new("1.5");

    public static readonly AllyariaNumberValue Margin = Sizing.Size2;

    public static readonly AllyariaNumberValue Padding = Sizing.Size3;

    public static readonly AllyariaStringValue TextAlign = Constants.TextAlign.Left;

    public static readonly AllyariaStringValue TextDecorationLine = Constants.TextDecorationLine.None;

    public static readonly AllyariaStringValue TextDecorationStyle = Constants.TextDecorationStyle.Solid;

    public static readonly AllyariaStringValue TextTransform = Constants.TextTransform.None;

    public static readonly AllyariaStringValue VerticalAlign = Constants.VerticalAlign.Baseline;
}
