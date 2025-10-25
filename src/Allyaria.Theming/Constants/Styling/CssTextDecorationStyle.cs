namespace Allyaria.Theming.Constants.Styling;

/// <summary>Provides strongly-typed text decoration style constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssTextDecorationStyle
{
    /// <summary>Represents a dashed text decoration style.</summary>
    public static readonly StyleValueString Dashed = new("dashed");

    /// <summary>Represents a dotted text decoration style.</summary>
    public static readonly StyleValueString Dotted = new("dotted");

    /// <summary>Represents a double-line text decoration style.</summary>
    public static readonly StyleValueString Double = new("double");

    /// <summary>Represents a solid text decoration style.</summary>
    public static readonly StyleValueString Solid = new("solid");

    /// <summary>Represents a wavy text decoration style.</summary>
    public static readonly StyleValueString Wavy = new("wavy");
}
