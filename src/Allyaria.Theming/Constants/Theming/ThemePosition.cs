namespace Allyaria.Theming.Constants.Theming;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class ThemePosition
{
    public static readonly ThemeGroupPosition VerticalDown = new(
        TextOrientation: CssTextOrientation.Mixed,
        WritingMode: CssWritingMode.VerticalLr
    );

    public static readonly ThemeGroupPosition VerticalUp = new(
        TextOrientation: CssTextOrientation.Mixed,
        WritingMode: CssWritingMode.SidewaysLr
    );
}
