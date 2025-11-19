namespace Allyaria.Theming.Constants;

/// <summary>
/// Defines the default color and font values used across Allyaria themes, including light, dark, and high-contrast
/// variants. These serve as the baseline for theme initialization when no explicit values are provided by the user.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class StyleDefaults
{
    /// <summary>Default error color for dark mode themes.</summary>
    public static readonly HexColor ErrorColorDark = Colors.Red300;

    /// <summary>Default error color for light mode themes.</summary>
    public static readonly HexColor ErrorColorLight = Colors.RedA700;

    /// <summary>High-contrast error color for dark mode themes.</summary>
    public static readonly HexColor HighContrastErrorColorDark = Colors.RedA400;

    /// <summary>High-contrast error color for light mode themes.</summary>
    public static readonly HexColor HighContrastErrorColorLight = Colors.RedA700;

    /// <summary>High-contrast informational color for dark mode themes.</summary>
    public static readonly HexColor HighContrastInfoColorDark = Colors.Aqua;

    /// <summary>High-contrast informational color for light mode themes.</summary>
    public static readonly HexColor HighContrastInfoColorLight = Colors.Blue700;

    /// <summary>High-contrast primary color for dark mode themes.</summary>
    public static readonly HexColor HighContrastPrimaryColorDark = Colors.Aqua;

    /// <summary>High-contrast primary color for light mode themes.</summary>
    public static readonly HexColor HighContrastPrimaryColorLight = Colors.Black;

    /// <summary>High-contrast secondary color for dark mode themes.</summary>
    public static readonly HexColor HighContrastSecondaryColorDark = Colors.YellowA400;

    /// <summary>High-contrast secondary color for light mode themes.</summary>
    public static readonly HexColor HighContrastSecondaryColorLight = Colors.Blue700;

    /// <summary>High-contrast success color for dark mode themes.</summary>
    public static readonly HexColor HighContrastSuccessColorDark = Colors.LimeA200;

    /// <summary>High-contrast success color for light mode themes.</summary>
    public static readonly HexColor HighContrastSuccessColorLight = Colors.Green800;

    /// <summary>High-contrast surface color for dark mode themes.</summary>
    public static readonly HexColor HighContrastSurfaceColorDark = Colors.Black;

    /// <summary>High-contrast surface color for light mode themes.</summary>
    public static readonly HexColor HighContrastSurfaceColorLight = Colors.White;

    /// <summary>High-contrast tertiary color for dark mode themes.</summary>
    public static readonly HexColor HighContrastTertiaryColorDark = Colors.Fuchsia;

    /// <summary>High-contrast tertiary color for light mode themes.</summary>
    public static readonly HexColor HighContrastTertiaryColorLight = Colors.Purple800;

    /// <summary>High-contrast warning color for dark mode themes.</summary>
    public static readonly HexColor HighContrastWarningColorDark = Colors.YellowA400;

    /// <summary>High-contrast warning color for light mode themes.</summary>
    public static readonly HexColor HighContrastWarningColorLight = Colors.Black;

    /// <summary>Default informational color for dark mode themes.</summary>
    public static readonly HexColor InfoColorDark = Colors.Lightblue300;

    /// <summary>Default informational color for light mode themes.</summary>
    public static readonly HexColor InfoColorLight = Colors.LightblueA700;

    /// <summary>Default monospace font stack for code and fixed-width text.</summary>
    public static readonly string MonospaceFont =
        "ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, 'Liberation Mono', 'Courier New', monospace";

    /// <summary>Default primary color for dark mode themes.</summary>
    public static readonly HexColor PrimaryColorDark = Colors.Blue300;

    /// <summary>Default primary color for light mode themes.</summary>
    public static readonly HexColor PrimaryColorLight = Colors.Blue700;

    /// <summary>Default sans-serif font stack for UI text.</summary>
    public static readonly string SansSerifFont =
        "system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif";

    /// <summary>Default secondary color for dark mode themes.</summary>
    public static readonly HexColor SecondaryColorDark = Colors.Indigo300;

    /// <summary>Default secondary color for light mode themes.</summary>
    public static readonly HexColor SecondaryColorLight = Colors.Indigo600;

    /// <summary>Default serif font stack for traditional or typographic elements.</summary>
    public static readonly string SerifFont = "ui-serif, Georgia, Cambria, 'Times New Roman', Times, serif";

    /// <summary>Default success color for dark mode themes.</summary>
    public static readonly HexColor SuccessColorDark = Colors.Green300;

    /// <summary>Default success color for light mode themes.</summary>
    public static readonly HexColor SuccessColorLight = Colors.Green600;

    /// <summary>Default surface background color for dark mode themes.</summary>
    public static readonly HexColor SurfaceColorDark = Colors.Grey900;

    /// <summary>Default surface background color for light mode themes.</summary>
    public static readonly HexColor SurfaceColorLight = Colors.Grey50;

    /// <summary>Default tertiary color for dark mode themes.</summary>
    public static readonly HexColor TertiaryColorDark = Colors.Teal300;

    /// <summary>Default tertiary color for light mode themes.</summary>
    public static readonly HexColor TertiaryColorLight = Colors.Teal600;

    /// <summary>Default CSS custom property prefix (used for variable naming).</summary>
    public static readonly string VarPrefix = "ary";

    /// <summary>Default warning color for dark mode themes.</summary>
    public static readonly HexColor WarningColorDark = Colors.Amber300;

    /// <summary>Default warning color for light mode themes.</summary>
    public static readonly HexColor WarningColorLight = Colors.Amber700;
}
