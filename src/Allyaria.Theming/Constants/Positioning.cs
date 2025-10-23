namespace Allyaria.Theming.Constants;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class Positioning
{
    public static readonly StyleValueString Absolute = new("absolute");
    public static readonly StyleValueString Fixed = new("fixed");
    public static readonly StyleValueString Relative = new("relative");
    public static readonly StyleValueString Static = new("static");
    public static readonly StyleValueString Sticky = new("sticky");
}
