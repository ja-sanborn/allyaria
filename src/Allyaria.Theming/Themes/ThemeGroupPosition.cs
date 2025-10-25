namespace Allyaria.Theming.Themes;

public sealed partial record ThemeGroupPosition(
    StyleValueString? AlignContent = null,
    StyleValueString? AlignItems = null,
    StyleValueString? AlignSelf = null,
    StyleValueString? BoxSizing = null,
    StyleValueString? Display = null,
    StyleValueString? JustifyContent = null,
    StyleValueString? JustifyItems = null,
    StyleValueString? JustifySelf = null,
    StyleValueString? Position = null,
    StyleValueString? TextOrientation = null,
    StyleValueString? UnicodeBidi = null,
    StyleValueString? WritingMode = null,
    StyleValueNumber? ZIndex = null
)
{
    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = null)
    {
        builder
            .Add(propertyName: "align-content", value: AlignContent, varPrefix: varPrefix)
            .Add(propertyName: "align-items", value: AlignItems, varPrefix: varPrefix)
            .Add(propertyName: "align-self", value: AlignSelf, varPrefix: varPrefix)
            .Add(propertyName: "box-sizing", value: BoxSizing, varPrefix: varPrefix)
            .Add(propertyName: "display", value: Display, varPrefix: varPrefix)
            .Add(propertyName: "justify-content", value: JustifyContent, varPrefix: varPrefix)
            .Add(propertyName: "justify-items", value: JustifyItems, varPrefix: varPrefix)
            .Add(propertyName: "justify-self", value: JustifySelf, varPrefix: varPrefix)
            .Add(propertyName: "position", value: Position, varPrefix: varPrefix)
            .Add(propertyName: "text-orientation", value: TextOrientation, varPrefix: varPrefix)
            .Add(propertyName: "unicode-bidi", value: UnicodeBidi, varPrefix: varPrefix)
            .Add(propertyName: "writing-mode", value: WritingMode, varPrefix: varPrefix)
            .Add(propertyName: "z-index", value: ZIndex, varPrefix: varPrefix);

        return builder;
    }

    public ThemeGroupPosition Merge(ThemeGroupPosition other)
        => SetAlignContent(other.AlignContent ?? AlignContent)
            .SetAlignItems(other.AlignItems ?? AlignItems)
            .SetAlignSelf(other.AlignSelf ?? AlignSelf)
            .SetBoxSizing(other.BoxSizing ?? BoxSizing)
            .SetDisplay(other.Display ?? Display)
            .SetJustifyContent(other.JustifyContent ?? JustifyContent)
            .SetJustifyItems(other.JustifyItems ?? JustifyItems)
            .SetJustifySelf(other.JustifySelf ?? JustifySelf)
            .SetPosition(other.Position ?? Position)
            .SetTextOrientation(other.TextOrientation ?? TextOrientation)
            .SetUnicodeBidi(other.UnicodeBidi ?? UnicodeBidi)
            .SetWritingMode(other.WritingMode ?? WritingMode)
            .SetZIndex(other.ZIndex ?? ZIndex);

    public ThemeGroupPosition SetAlignContent(StyleValueString? value)
        => this with
        {
            AlignContent = value
        };

    public ThemeGroupPosition SetAlignItems(StyleValueString? value)
        => this with
        {
            AlignItems = value
        };

    public ThemeGroupPosition SetAlignSelf(StyleValueString? value)
        => this with
        {
            AlignSelf = value
        };

    public ThemeGroupPosition SetBoxSizing(StyleValueString? value)
        => this with
        {
            BoxSizing = value
        };

    public ThemeGroupPosition SetDisplay(StyleValueString? value)
        => this with
        {
            Display = value
        };

    public ThemeGroupPosition SetJustifyContent(StyleValueString? value)
        => this with
        {
            JustifyContent = value
        };

    public ThemeGroupPosition SetJustifyItems(StyleValueString? value)
        => this with
        {
            JustifyItems = value
        };

    public ThemeGroupPosition SetJustifySelf(StyleValueString? value)
        => this with
        {
            JustifySelf = value
        };

    public ThemeGroupPosition SetPosition(StyleValueString? value)
        => this with
        {
            Position = value
        };

    public ThemeGroupPosition SetTextOrientation(StyleValueString? value)
        => this with
        {
            TextOrientation = value
        };

    public ThemeGroupPosition SetUnicodeBidi(StyleValueString? value)
        => this with
        {
            UnicodeBidi = value
        };

    public ThemeGroupPosition SetWritingMode(StyleValueString? value)
        => this with
        {
            WritingMode = value
        };

    public ThemeGroupPosition SetZIndex(StyleValueNumber? value)
        => this with
        {
            ZIndex = value
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
