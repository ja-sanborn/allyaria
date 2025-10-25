namespace Allyaria.Theming.Themes;

/// <summary>These are for static default constants for <see cref="ThemeGroupSizing" />.</summary>
public sealed partial record ThemeGroupSizing
{
    public static readonly ThemeGroupSizing Empty = new();

    public static readonly ThemeGroupSizing FullHeight = new(new StyleValueNumber("100%"));

    public static readonly ThemeGroupSizing FullSize = new(
        Height: new StyleValueNumber("100%"),
        Width: new StyleValueNumber("100%")
    );

    public static readonly ThemeGroupSizing FullWidth = new(Width: new StyleValueNumber("100%"));

    public static readonly ThemeGroupSizing MarginPadding = new(
        Margin: new StyleGroupMargin(CssSize.Size2),
        Padding: new StyleGroupPadding(CssSize.Size3)
    );
}
