using Allyaria.Theming.Values;
using System.Diagnostics.CodeAnalysis;

namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly typed default style values for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class StyleDefaults
{
    /// <summary>Default background color for dark theme.</summary>
    public static readonly AllyariaColorValue BackgroundColorDark = Colors.Grey900;

    /// <summary>Default background color for high-contrast theme.</summary>
    public static readonly AllyariaColorValue BackgroundColorHighContrast = Colors.White;

    /// <summary>Default background color for light theme.</summary>
    public static readonly AllyariaColorValue BackgroundColorLight = Colors.Grey50;

    /// <summary>Default border radius.</summary>
    public static readonly AllyariaNumberValue BorderRadius = Sizing.Size2;

    /// <summary>Default border style.</summary>
    public static readonly AllyariaStringValue BorderStyle = Constants.BorderStyle.Solid;

    /// <summary>Default border width.</summary>
    public static readonly AllyariaNumberValue BorderWidth = Sizing.Size0;

    /// <summary>Default font family (system UI stack).</summary>
    public static readonly AllyariaStringValue FontFamily = new(
        "system-ui, -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Helvetica, Arial, sans-serif"
    );

    /// <summary>Default font size.</summary>
    public static readonly AllyariaNumberValue FontSize = Sizing.Size3;

    /// <summary>Default font style.</summary>
    public static readonly AllyariaStringValue FontStyle = Constants.FontStyle.Normal;

    /// <summary>Default font weight.</summary>
    public static readonly AllyariaStringValue FontWeight = Constants.FontWeight.Normal;

    /// <summary>Default foreground color for dark theme.</summary>
    public static readonly AllyariaColorValue ForegroundColorDark = Colors.Grey50;

    /// <summary>Default foreground color for high-contrast theme.</summary>
    public static readonly AllyariaColorValue ForegroundColorHighContrast = Colors.Black;

    /// <summary>Default foreground color for light theme.</summary>
    public static readonly AllyariaColorValue ForegroundColorLight = Colors.Grey900;

    /// <summary>Default letter spacing.</summary>
    public static readonly AllyariaNumberValue LetterSpacing = new("0.5px");

    /// <summary>Default line height.</summary>
    public static readonly AllyariaNumberValue LineHeight = new("1.5");

    /// <summary>Default margin.</summary>
    public static readonly AllyariaNumberValue Margin = Sizing.Size2;

    /// <summary>Default padding.</summary>
    public static readonly AllyariaNumberValue Padding = Sizing.Size3;

    /// <summary>Default text alignment.</summary>
    public static readonly AllyariaStringValue TextAlign = Constants.TextAlign.Left;

    /// <summary>Default text decoration line.</summary>
    public static readonly AllyariaStringValue TextDecorationLine = Constants.TextDecorationLine.None;

    /// <summary>Default text decoration style.</summary>
    public static readonly AllyariaStringValue TextDecorationStyle = Constants.TextDecorationStyle.Solid;

    /// <summary>Default text transform.</summary>
    public static readonly AllyariaStringValue TextTransform = Constants.TextTransform.None;

    /// <summary>Default vertical alignment.</summary>
    public static readonly AllyariaStringValue VerticalAlign = Constants.VerticalAlign.Baseline;
}
