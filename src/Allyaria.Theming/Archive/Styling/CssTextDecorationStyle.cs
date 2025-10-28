namespace Allyaria.Theming.Constants.Styling;

/// <summary>Provides strongly-typed text decoration style constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssTextDecorationStyle
{
    /// <summary>Represents a dashed text decoration style.</summary>
    public static readonly StyleValueString Dashed = new(value: "dashed");

    /// <summary>Represents a dotted text decoration style.</summary>
    public static readonly StyleValueString Dotted = new(value: "dotted");

    /// <summary>Represents a double-line text decoration style.</summary>
    public static readonly StyleValueString Double = new(value: "double");

    /// <summary>Represents a solid text decoration style.</summary>
    public static readonly StyleValueString Solid = new(value: "solid");

    /// <summary>Represents a wavy text decoration style.</summary>
    public static readonly StyleValueString Wavy = new(value: "wavy");
}
