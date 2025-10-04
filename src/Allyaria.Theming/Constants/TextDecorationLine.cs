using Allyaria.Theming.Values;

namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed text decoration line constants for Allyaria theming.</summary>
public static class TextDecorationLine
{
    /// <summary>Represents a line-through text decoration.</summary>
    public static readonly AllyariaStringValue LineThrough = new("line-through");

    /// <summary>Represents no text decoration line.</summary>
    public static readonly AllyariaStringValue None = new("none");

    /// <summary>Represents an overline text decoration.</summary>
    public static readonly AllyariaStringValue Overline = new("overline");

    /// <summary>Represents an underline text decoration.</summary>
    public static readonly AllyariaStringValue Underline = new("underline");
}
