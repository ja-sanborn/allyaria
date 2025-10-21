namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed text alignment constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class TextAlignment
{
    /// <summary>Represents center-aligned text.</summary>
    public static readonly ThemeString Center = new("center");

    /// <summary>Represents alignment at the end of the inline direction (depends on writing mode and direction).</summary>
    public static readonly ThemeString End = new("end");

    /// <summary>Represents justified text alignment.</summary>
    public static readonly ThemeString Justify = new("justify");

    /// <summary>Represents alignment inherited from the parent element (match-parent).</summary>
    public static readonly ThemeString Match = new("match-parent");

    /// <summary>Represents alignment at the start of the inline direction (depends on writing mode and direction).</summary>
    public static readonly ThemeString Start = new("start");
}
