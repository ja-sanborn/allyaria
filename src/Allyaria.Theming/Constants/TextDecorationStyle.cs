namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed text decoration style constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class TextDecorationStyle
{
    /// <summary>Represents a dashed text decoration style.</summary>
    public static readonly AryStringValue Dashed = new("dashed");

    /// <summary>Represents a dotted text decoration style.</summary>
    public static readonly AryStringValue Dotted = new("dotted");

    /// <summary>Represents a double-line text decoration style.</summary>
    public static readonly AryStringValue Double = new("double");

    /// <summary>Represents a solid text decoration style.</summary>
    public static readonly AryStringValue Solid = new("solid");

    /// <summary>Represents a wavy text decoration style.</summary>
    public static readonly AryStringValue Wavy = new("wavy");
}
