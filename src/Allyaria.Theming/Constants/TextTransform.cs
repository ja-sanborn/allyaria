using System.Diagnostics.CodeAnalysis;

namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed text transform constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class TextTransform
{
    /// <summary>Represents capitalizing the first letter of each word.</summary>
    public static readonly AllyariaStringValue Capitalize = new("capitalize");

    /// <summary>Represents transforming all characters to lowercase.</summary>
    public static readonly AllyariaStringValue Lowercase = new("lowercase");

    /// <summary>Represents no text transformation.</summary>
    public static readonly AllyariaStringValue None = new("none");

    /// <summary>Represents transforming all characters to uppercase.</summary>
    public static readonly AllyariaStringValue Uppercase = new("uppercase");
}
