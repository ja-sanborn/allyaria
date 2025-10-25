namespace Allyaria.Theming.Constants.Styling;

/// <summary>Provides strongly-typed text decoration line constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssTextDecorationLine
{
    /// <summary>Represents a line-through text decoration.</summary>
    public static readonly StyleValueString LineThrough = new("line-through");

    /// <summary>Represents no text decoration line.</summary>
    public static readonly StyleValueString None = new("none");

    /// <summary>Represents an overline text decoration.</summary>
    public static readonly StyleValueString Overline = new("overline");

    /// <summary>Represents an underline text decoration.</summary>
    public static readonly StyleValueString Underline = new("underline");
}
