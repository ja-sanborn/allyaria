using System.Diagnostics.CodeAnalysis;

namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed font style constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class FontStyle
{
    /// <summary>Represents italic font style.</summary>
    public static readonly AllyariaStringValue Italic = new("italic");

    /// <summary>Represents normal font style.</summary>
    public static readonly AllyariaStringValue Normal = new("normal");

    /// <summary>Represents oblique font style.</summary>
    public static readonly AllyariaStringValue Oblique = new("oblique");
}
