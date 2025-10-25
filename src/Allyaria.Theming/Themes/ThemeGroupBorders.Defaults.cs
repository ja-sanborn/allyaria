namespace Allyaria.Theming.Themes;

/// <summary>These are for static default constants for <see cref="ThemeGroupBorder" />.</summary>
public sealed partial record ThemeGroupBorder
{
    public static readonly ThemeGroupBorder BorderThick = new(
        BorderRadius: new StyleGroupBorderRadius(CssSize.Size2),
        BorderStyle: new StyleGroupBorderStyle(CssBorderStyle.None),
        BorderWidth: new StyleGroupBorderWidth(CssSize.Thick)
    );

    public static readonly ThemeGroupBorder BorderThin = new(
        BorderRadius: new StyleGroupBorderRadius(CssSize.Size2),
        BorderStyle: new StyleGroupBorderStyle(CssBorderStyle.None),
        BorderWidth: new StyleGroupBorderWidth(CssSize.Thin)
    );

    public static readonly ThemeGroupBorder Empty = new();

    public static readonly ThemeGroupBorder OutlineThick = new(
        OutlineOffset: new StyleValueNumber(CssSize.Thick),
        OutlineStyle: new StyleValueString(CssBorderStyle.Solid),
        OutlineWidth: new StyleValueNumber(CssSize.Thick)
    );

    public static readonly ThemeGroupBorder OutlineThin = new(
        OutlineOffset: new StyleValueNumber(CssSize.Thick),
        OutlineStyle: new StyleValueString(CssBorderStyle.Solid),
        OutlineWidth: new StyleValueNumber(CssSize.Thin)
    );

    public static readonly ThemeGroupBorder ThickThick = BorderThick.Merge(OutlineThick);

    public static readonly ThemeGroupBorder ThickThin = BorderThick.Merge(OutlineThin);

    public static readonly ThemeGroupBorder ThinThick = BorderThin.Merge(OutlineThick);

    public static readonly ThemeGroupBorder ThinThin = BorderThin.Merge(OutlineThin);
}
