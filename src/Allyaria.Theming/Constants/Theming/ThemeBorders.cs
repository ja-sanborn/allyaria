namespace Allyaria.Theming.Constants.Theming;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class ThemeBorders
{
    public static readonly ThemeGroupBorder BorderThick = new(
        BorderRadius: new StyleGroupBorderRadius(value: CssSize.Size2),
        BorderStyle: new StyleGroupBorderStyle(value: CssBorderStyle.None),
        BorderWidth: new StyleGroupBorderWidth(value: CssSize.Thick)
    );

    public static readonly ThemeGroupBorder BorderThin = new(
        BorderRadius: new StyleGroupBorderRadius(value: CssSize.Size2),
        BorderStyle: new StyleGroupBorderStyle(value: CssBorderStyle.None),
        BorderWidth: new StyleGroupBorderWidth(value: CssSize.Thin)
    );

    public static readonly ThemeGroupBorder OutlineThick = new(
        OutlineOffset: CssSize.Thick,
        OutlineStyle: CssBorderStyle.Solid,
        OutlineWidth: CssSize.Thick
    );

    public static readonly ThemeGroupBorder OutlineThin = new(
        OutlineOffset: CssSize.Thick,
        OutlineStyle: CssBorderStyle.Solid,
        OutlineWidth: CssSize.Thin
    );

    public static readonly ThemeGroupBorder ThickThick = BorderThick.Merge(other: OutlineThick);

    public static readonly ThemeGroupBorder ThickThin = BorderThick.Merge(other: OutlineThin);

    public static readonly ThemeGroupBorder ThinThick = BorderThin.Merge(other: OutlineThick);

    public static readonly ThemeGroupBorder ThinThin = BorderThin.Merge(other: OutlineThin);
}
