namespace Allyaria.Theming.Constants.Styling;

/// <summary>Provides strongly-typed text transform constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssTextTransform
{
    /// <summary>Represents capitalizing the first letter of each word.</summary>
    public static readonly StyleValueString Capitalize = new(value: "capitalize");

    /// <summary>Represents transforming all characters to lowercase.</summary>
    public static readonly StyleValueString Lowercase = new(value: "lowercase");

    /// <summary>Represents no text transformation.</summary>
    public static readonly StyleValueString None = new(value: "none");

    /// <summary>Represents transforming all characters to uppercase.</summary>
    public static readonly StyleValueString Uppercase = new(value: "uppercase");
}
