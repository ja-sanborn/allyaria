namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly typed default style values for Allyaria theming.</summary>
/// <remarks>The order of properties in this class is important for initialization.</remarks>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class ThemingDefaults
{
    public static readonly StyleValueColor AccentColorHighContrastDark = new(Colors.YellowA400);

    public static readonly StyleValueColor AccentColorHighContrastLight = new(Colors.BlueA700);

    public static readonly StyleValueColor BackgroundColorHighContrastDark = new(Colors.Black);

    public static readonly StyleValueColor BackgroundColorHighContrastLight = new(Colors.White);

    public static readonly StyleValueColor ErrorColorDark = new(Colors.Red300);

    public static readonly StyleValueColor ErrorColorHighContrastDark = new(Colors.RedA200);

    public static readonly StyleValueColor ErrorColorHighContrastLight = new(Colors.Red700);

    public static readonly StyleValueColor ErrorColorLight = new(Colors.Red700);

    public static readonly StyleValueString FontFamily = new(
        "system-ui, -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Helvetica, Arial, sans-serif"
    );

    public static readonly StyleValueColor ForegroundColorHighContrastDark = new(Colors.White);

    public static readonly StyleValueColor ForegroundColorHighContrastLight = new(Colors.Black);

    public static readonly StyleValueColor PrimaryColorDark = new(Colors.Blue300);

    public static readonly StyleValueColor PrimaryColorLight = new(Colors.Blue700);

    public static readonly StyleValueColor SecondaryColorDark = new(Colors.Indigo300);

    public static readonly StyleValueColor SecondaryColorLight = new(Colors.Indigo600);

    public static readonly StyleValueColor SuccessColorDark = new(Colors.Green300);

    public static readonly StyleValueColor SuccessColorHighContrastDark = new(Colors.GreenA400);

    public static readonly StyleValueColor SuccessColorHighContrastLight = new(Colors.Green800);

    public static readonly StyleValueColor SuccessColorLight = new(Colors.Green600);

    public static readonly StyleValueColor SurfaceColorDark = new(Colors.Grey900);

    public static readonly StyleValueColor SurfaceColorLight = new(Colors.Grey50);

    public static readonly StyleValueColor SurfaceVariantColorDark = new(Colors.BlueGrey700);

    public static readonly StyleValueColor SurfaceVariantColorLight = new(Colors.BlueGrey100);

    public static readonly StyleValueColor TertiaryColorDark = new(Colors.Teal300);

    public static readonly StyleValueColor TertiaryColorLight = new(Colors.Teal600);

    /// <summary>Default CSS var prefix.</summary>
    public static readonly string VarPrefix = "ary";

    public static readonly StyleValueColor WarningColorDark = new(Colors.Amber300);

    public static readonly StyleValueColor WarningColorHighContrastDark = new(Colors.YellowA400);

    public static readonly StyleValueColor WarningColorHighContrastLight = new(Colors.Amber800);

    public static readonly StyleValueColor WarningColorLight = new(Colors.Amber700);
}
