namespace Allyaria.Theming.Themes;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class ThemeSizing
{
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
