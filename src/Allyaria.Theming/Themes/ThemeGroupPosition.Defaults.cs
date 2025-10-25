namespace Allyaria.Theming.Themes;

/// <summary>These are for static default constants for <see cref="ThemeGroupPosition" />.</summary>
public sealed partial record ThemeGroupPosition
{
    public static readonly ThemeGroupPosition Empty = new();

    public static readonly ThemeGroupPosition VerticalDown = new(
        TextOrientation: CssTextOrientation.Mixed,
        WritingMode: CssWritingMode.VerticalLr
    );

    public static readonly ThemeGroupPosition VerticalUp = new(
        TextOrientation: CssTextOrientation.Mixed,
        WritingMode: CssWritingMode.SidewaysLr
    );
}
