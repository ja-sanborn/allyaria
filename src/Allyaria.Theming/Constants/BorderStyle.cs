using Allyaria.Theming.Values;
using System.Diagnostics.CodeAnalysis;

namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly typed constants representing standard CSS border styles.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class BorderStyle
{
    /// <summary>Represents a dashed border style.</summary>
    public static readonly AllyariaStringValue Dashed = new("dashed");

    /// <summary>Represents a dotted border style.</summary>
    public static readonly AllyariaStringValue Dotted = new("dotted");

    /// <summary>Represents a double line border style.</summary>
    public static readonly AllyariaStringValue Double = new("double");

    /// <summary>Represents a groove border style.</summary>
    public static readonly AllyariaStringValue Groove = new("groove");

    /// <summary>Represents a hidden border style.</summary>
    public static readonly AllyariaStringValue Hidden = new("hidden");

    /// <summary>Represents an inset border style.</summary>
    public static readonly AllyariaStringValue Inset = new("inset");

    /// <summary>Represents no border.</summary>
    public static readonly AllyariaStringValue None = new("none");

    /// <summary>Represents an outset border style.</summary>
    public static readonly AllyariaStringValue Outset = new("outset");

    /// <summary>Represents a ridge border style.</summary>
    public static readonly AllyariaStringValue Ridge = new("ridge");

    /// <summary>Represents a solid border style.</summary>
    public static readonly AllyariaStringValue Solid = new("solid");
}
