namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed text decoration style constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class TextDecorationStyle
{
    /// <summary>Represents a dashed text decoration style.</summary>
    public static readonly ThemeString Dashed = new("dashed");

    /// <summary>Represents a dotted text decoration style.</summary>
    public static readonly ThemeString Dotted = new("dotted");

    /// <summary>Represents a double-line text decoration style.</summary>
    public static readonly ThemeString Double = new("double");

    /// <summary>Represents a solid text decoration style.</summary>
    public static readonly ThemeString Solid = new("solid");

    /// <summary>Represents a wavy text decoration style.</summary>
    public static readonly ThemeString Wavy = new("wavy");
}
