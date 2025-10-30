namespace Allyaria.Theming.Enumerations;

public enum StyleGroupType
{
    [Description(
        description: "border-block-end-color,border-block-start-color,border-inline-end-color,border-inline-start-color"
    )]
    BorderColor,

    [Description(
        description: "border-end-end-radius,border-end-start-radius,border-start-end-radius,border-start-start-radius"
    )]
    BorderRadius,

    [Description(
        description: "border-block-end-style,border-block-start-style,border-inline-end-style,border-inline-start-style"
    )]
    BorderStyle,

    [Description(
        description: "border-block-end-width,border-block-start-width,border-inline-end-width,border-inline-start-width"
    )]
    BorderWidth,

    [Description(description: "margin-block-end,margin-block-start,margin-inline-end,margin-inline-start")]
    Margin,

    [Description(description: "padding-block-end,padding-block-start,padding-inline-end,padding-inline-start")]
    Padding
}
