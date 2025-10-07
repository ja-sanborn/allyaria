using System.Diagnostics.CodeAnalysis;

namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed font weight constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class FontWeight
{
    /// <summary>Represents a bold font weight.</summary>
    public static readonly AryStringValue Bold = new("bold");

    /// <summary>Represents a font weight of 100.</summary>
    public static readonly AryStringValue Bold1 = new("100");

    /// <summary>Represents a font weight of 200.</summary>
    public static readonly AryStringValue Bold2 = new("200");

    /// <summary>Represents a font weight of 300.</summary>
    public static readonly AryStringValue Bold3 = new("300");

    /// <summary>Represents a font weight of 400.</summary>
    public static readonly AryStringValue Bold4 = new("400");

    /// <summary>Represents a font weight of 500.</summary>
    public static readonly AryStringValue Bold5 = new("500");

    /// <summary>Represents a font weight of 600.</summary>
    public static readonly AryStringValue Bold6 = new("600");

    /// <summary>Represents a font weight of 700.</summary>
    public static readonly AryStringValue Bold7 = new("700");

    /// <summary>Represents a font weight of 800.</summary>
    public static readonly AryStringValue Bold8 = new("800");

    /// <summary>Represents a font weight of 900.</summary>
    public static readonly AryStringValue Bold9 = new("900");

    /// <summary>Represents a bolder font weight relative to the parent element.</summary>
    public static readonly AryStringValue Bolder = new("bolder");

    /// <summary>Represents a lighter font weight relative to the parent element.</summary>
    public static readonly AryStringValue Lighter = new("lighter");

    /// <summary>Represents a normal font weight.</summary>
    public static readonly AryStringValue Normal = new("normal");
}
