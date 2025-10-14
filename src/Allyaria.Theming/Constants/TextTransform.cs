namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed text transform constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class TextTransform
{
    /// <summary>Represents capitalizing the first letter of each word.</summary>
    public static readonly AryStringValue Capitalize = new("capitalize");

    /// <summary>Represents transforming all characters to lowercase.</summary>
    public static readonly AryStringValue Lowercase = new("lowercase");

    /// <summary>Represents no text transformation.</summary>
    public static readonly AryStringValue None = new("none");

    /// <summary>Represents transforming all characters to uppercase.</summary>
    public static readonly AryStringValue Uppercase = new("uppercase");
}
