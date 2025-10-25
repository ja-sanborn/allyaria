namespace Allyaria.Theming.Constants.Styling;

/// <summary>Provides strongly-typed font weight constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssFontWeight
{
    /// <summary>Represents a bold font weight.</summary>
    public static readonly StyleValueString Bold = new(value: "bold");

    /// <summary>Represents a font weight of 100.</summary>
    public static readonly StyleValueString Bold1 = new(value: "100");

    /// <summary>Represents a font weight of 200.</summary>
    public static readonly StyleValueString Bold2 = new(value: "200");

    /// <summary>Represents a font weight of 300.</summary>
    public static readonly StyleValueString Bold3 = new(value: "300");

    /// <summary>Represents a font weight of 400.</summary>
    public static readonly StyleValueString Bold4 = new(value: "400");

    /// <summary>Represents a font weight of 500.</summary>
    public static readonly StyleValueString Bold5 = new(value: "500");

    /// <summary>Represents a font weight of 600.</summary>
    public static readonly StyleValueString Bold6 = new(value: "600");

    /// <summary>Represents a font weight of 700.</summary>
    public static readonly StyleValueString Bold7 = new(value: "700");

    /// <summary>Represents a font weight of 800.</summary>
    public static readonly StyleValueString Bold8 = new(value: "800");

    /// <summary>Represents a font weight of 900.</summary>
    public static readonly StyleValueString Bold9 = new(value: "900");

    /// <summary>Represents a bolder font weight relative to the parent element.</summary>
    public static readonly StyleValueString Bolder = new(value: "bolder");

    /// <summary>Represents a lighter font weight relative to the parent element.</summary>
    public static readonly StyleValueString Lighter = new(value: "lighter");

    /// <summary>Represents a normal font weight.</summary>
    public static readonly StyleValueString Normal = new(value: "normal");
}
