namespace Allyaria.Theming.Themes;

/// <summary>These are for static default constants for <see cref="ThemeGroupBorder" />.</summary>
public sealed partial record ThemeGroupBorder
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

    public static readonly ThemeGroupBorder Empty = new();

    public static readonly ThemeGroupBorder OutlineThick = new(
        OutlineOffset: new StyleValueNumber(value: CssSize.Thick),
        OutlineStyle: new StyleValueString(value: CssBorderStyle.Solid),
        OutlineWidth: new StyleValueNumber(value: CssSize.Thick)
    );

    public static readonly ThemeGroupBorder OutlineThin = new(
        OutlineOffset: new StyleValueNumber(value: CssSize.Thick),
        OutlineStyle: new StyleValueString(value: CssBorderStyle.Solid),
        OutlineWidth: new StyleValueNumber(value: CssSize.Thin)
    );

    public static readonly ThemeGroupBorder ThickThick = BorderThick.Merge(other: OutlineThick);

    public static readonly ThemeGroupBorder ThickThin = BorderThick.Merge(other: OutlineThin);

    public static readonly ThemeGroupBorder ThinThick = BorderThin.Merge(other: OutlineThick);

    public static readonly ThemeGroupBorder ThinThin = BorderThin.Merge(other: OutlineThin);
}
