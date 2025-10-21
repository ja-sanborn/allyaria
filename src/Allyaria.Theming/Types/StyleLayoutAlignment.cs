namespace Allyaria.Theming.Types;

public readonly record struct StyleLayoutAlignment(
    bool? IsRtl = null,
    ThemeString? TextAlign = null,
    ThemeString? VerticalAlign = null,
    ThemeString? JustifyContent = null,
    ThemeString? JustifyItems = null,
    ThemeString? JustifySelf = null,
    ThemeString? AlignContent = null,
    ThemeString? AlignItems = null,
    ThemeString? AlignSelf = null
)
{
    public StyleLayoutAlignment Cascade(StyleLayoutAlignment? value = null)
        => value is null
            ? this
            : Cascade(
                value.Value.IsRtl, value.Value.TextAlign, value.Value.VerticalAlign, value.Value.JustifyContent,
                value.Value.JustifyItems, value.Value.JustifySelf, value.Value.AlignContent, value.Value.AlignItems,
                value.Value.AlignSelf
            );

    public StyleLayoutAlignment Cascade(bool? isRtl = null,
        ThemeString? textAlign = null,
        ThemeString? verticalAlign = null,
        ThemeString? justifyContent = null,
        ThemeString? justifyItems = null,
        ThemeString? justifySelf = null,
        ThemeString? alignContent = null,
        ThemeString? alignItems = null,
        ThemeString? alignSelf = null)
        => this with
        {
            AlignContent = alignContent ?? AlignContent,
            AlignItems = alignItems ?? AlignItems,
            AlignSelf = alignSelf ?? AlignSelf,
            IsRtl = isRtl ?? IsRtl,
            JustifyContent = justifyContent ?? JustifyContent,
            JustifyItems = justifyItems ?? JustifyItems,
            JustifySelf = justifySelf ?? JustifySelf,
            TextAlign = textAlign ?? TextAlign
        };

    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        var direction = IsRtl is null
            ? null
            : IsRtl.Value
                ? new ThemeString("rtl")
                : new ThemeString("ltr");

        builder.ToCss("align-content", AlignContent, varPrefix);
        builder.ToCss("align-items", AlignItems, varPrefix);
        builder.ToCss("align-self", AlignSelf, varPrefix);
        builder.ToCss("direction", direction, varPrefix);
        builder.ToCss("justify-content", JustifyContent, varPrefix);
        builder.ToCss("justify-items", JustifyItems, varPrefix);
        builder.ToCss("justify-self", JustifySelf, varPrefix);
        builder.ToCss("text-align", TextAlign, varPrefix);

        return builder.ToString();
    }
}
