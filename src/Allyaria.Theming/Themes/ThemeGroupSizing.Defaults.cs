namespace Allyaria.Theming.Themes;

/// <summary>These are for static default constants for <see cref="ThemeGroupSizing" />.</summary>
public sealed partial record ThemeGroupSizing
{
    public static readonly ThemeGroupSizing Empty = new();

    public static readonly ThemeGroupSizing FullHeight = new(Height: new StyleValueNumber(value: "100%"));

    public static readonly ThemeGroupSizing FullSize = new(
        Height: new StyleValueNumber(value: "100%"),
        Width: new StyleValueNumber(value: "100%")
    );

    public static readonly ThemeGroupSizing FullWidth = new(Width: new StyleValueNumber(value: "100%"));

    public static readonly ThemeGroupSizing MarginPadding = new(
        Margin: new StyleGroupMargin(value: CssSize.Size2),
        Padding: new StyleGroupPadding(value: CssSize.Size3)
    );
}
