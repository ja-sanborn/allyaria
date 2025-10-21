namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed text decoration line constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class TextDecorationLine
{
    /// <summary>Represents a line-through text decoration.</summary>
    public static readonly ThemeString LineThrough = new("line-through");

    /// <summary>Represents no text decoration line.</summary>
    public static readonly ThemeString None = new("none");

    /// <summary>Represents an overline text decoration.</summary>
    public static readonly ThemeString Overline = new("overline");

    /// <summary>Represents an underline text decoration.</summary>
    public static readonly ThemeString Underline = new("underline");
}
