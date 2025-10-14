using Allyaria.Abstractions.Constants;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly typed default style values for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class StyleDefaults
{
    /// <summary>Default background color for dark theme.</summary>
    public static readonly AryColorValue BackgroundColorDark = new(Colors.Grey900);

    /// <summary>Default background color for high-contrast theme.</summary>
    public static readonly AryColorValue BackgroundColorHighContrast = new(Colors.White);

    /// <summary>Default background color for light theme.</summary>
    public static readonly AryColorValue BackgroundColorLight = new(Colors.Grey50);

    /// <summary>Default border radius.</summary>
    public static readonly AryNumberValue BorderRadius = Sizing.Size2;

    /// <summary>Default border style.</summary>
    public static readonly AryStringValue BorderStyle = Constants.BorderStyle.Solid;

    /// <summary>Default border width.</summary>
    public static readonly AryNumberValue BorderWidth = Sizing.Size0;

    /// <summary>Default font family (system UI stack).</summary>
    public static readonly AryStringValue FontFamily = new(
        "system-ui, -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Helvetica, Arial, sans-serif"
    );

    /// <summary>Default font size.</summary>
    public static readonly AryNumberValue FontSize = Sizing.Size3;

    /// <summary>Default font style.</summary>
    public static readonly AryStringValue FontStyle = Constants.FontStyle.Normal;

    /// <summary>Default font weight.</summary>
    public static readonly AryStringValue FontWeight = Constants.FontWeight.Normal;

    /// <summary>Default foreground color for dark theme.</summary>
    public static readonly AryColorValue ForegroundColorDark = new(Colors.Grey50);

    /// <summary>Default foreground color for high-contrast theme.</summary>
    public static readonly AryColorValue ForegroundColorHighContrast = new(Colors.Black);

    /// <summary>Default foreground color for light theme.</summary>
    public static readonly AryColorValue ForegroundColorLight = new(Colors.Grey900);

    /// <summary>Default letter spacing.</summary>
    public static readonly AryNumberValue LetterSpacing = new("0.5px");

    /// <summary>Default line height.</summary>
    public static readonly AryNumberValue LineHeight = new("1.5");

    /// <summary>Default margin.</summary>
    public static readonly AryNumberValue Margin = Sizing.Size2;

    /// <summary>Default padding.</summary>
    public static readonly AryNumberValue Padding = Sizing.Size3;

    /// <summary>Default dark palette.</summary>
    public static readonly AryPalette PaletteDark = new(BackgroundColorDark, foregroundColor: ForegroundColorDark);

    /// <summary>Default high-contrast palette.</summary>
    public static readonly AryPalette PaletteHighContrast = new(
        BackgroundColorHighContrast, foregroundColor: ForegroundColorHighContrast, isHighContrast: true
    );

    /// <summary>Default light palette.</summary>
    public static readonly AryPalette PaletteLight = new(BackgroundColorLight, foregroundColor: ForegroundColorLight);

    /// <summary>Default text alignment.</summary>
    public static readonly AryStringValue TextAlign = Constants.TextAlign.Left;

    /// <summary>Default text decoration line.</summary>
    public static readonly AryStringValue TextDecorationLine = Constants.TextDecorationLine.None;

    /// <summary>Default text decoration style.</summary>
    public static readonly AryStringValue TextDecorationStyle = Constants.TextDecorationStyle.Solid;

    /// <summary>Default text transform.</summary>
    public static readonly AryStringValue TextTransform = Constants.TextTransform.None;

    /// <summary>
    /// Default theme instance composed of standard Allyaria borders, spacing, palette variants, and typography. Used as the
    /// baseline theme for components unless explicitly overridden.
    /// </summary>
    public static readonly AryTheme Theme = new();

    /// <summary>Default transparent color.</summary>
    public static readonly AryColorValue Transparent = new(Colors.Transparent);

    /// <summary>Default vertical alignment.</summary>
    public static readonly AryStringValue VerticalAlign = Constants.VerticalAlign.Baseline;
}
