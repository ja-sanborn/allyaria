namespace Allyaria.Theming.Constants.Styling;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssPosition
{
    public static readonly StyleValueString Absolute = new(value: "absolute");
    public static readonly StyleValueString Fixed = new(value: "fixed");
    public static readonly StyleValueString Relative = new(value: "relative");
    public static readonly StyleValueString Static = new(value: "static");
    public static readonly StyleValueString Sticky = new(value: "sticky");
}
