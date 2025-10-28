namespace Allyaria.Theming.Constants.Styling;

/// <summary>Provides strongly typed constants representing standard CSS border styles.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssOutlineStyle
{
    /// <summary>Represents a dashed border style.</summary>
    public static readonly StyleValueString Dashed = new(value: "dashed");

    /// <summary>Represents a dotted border style.</summary>
    public static readonly StyleValueString Dotted = new(value: "dotted");

    /// <summary>Represents a double line border style.</summary>
    public static readonly StyleValueString Double = new(value: "double");

    /// <summary>Represents a groove border style.</summary>
    public static readonly StyleValueString Groove = new(value: "groove");

    /// <summary>Represents a hidden border style.</summary>
    public static readonly StyleValueString Hidden = new(value: "hidden");

    /// <summary>Represents an inset border style.</summary>
    public static readonly StyleValueString Inset = new(value: "inset");

    /// <summary>Represents no border.</summary>
    public static readonly StyleValueString None = new(value: "none");

    /// <summary>Represents an outset border style.</summary>
    public static readonly StyleValueString Outset = new(value: "outset");

    /// <summary>Represents a ridge border style.</summary>
    public static readonly StyleValueString Ridge = new(value: "ridge");

    /// <summary>Represents a solid border style.</summary>
    public static readonly StyleValueString Solid = new(value: "solid");
}
