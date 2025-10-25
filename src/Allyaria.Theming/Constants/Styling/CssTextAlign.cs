namespace Allyaria.Theming.Constants.Styling;

/// <summary>Provides strongly-typed text alignment constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssTextAlign
{
    /// <summary>Represents center-aligned text.</summary>
    public static readonly StyleValueString Center = new(value: "center");

    /// <summary>Represents alignment at the end of the inline direction (depends on writing mode and direction).</summary>
    public static readonly StyleValueString End = new(value: "end");

    /// <summary>Represents justified text alignment.</summary>
    public static readonly StyleValueString Justify = new(value: "justify");

    /// <summary>Represents alignment inherited from the parent element (match-parent).</summary>
    public static readonly StyleValueString Match = new(value: "match-parent");

    /// <summary>Represents alignment at the start of the inline direction (depends on writing mode and direction).</summary>
    public static readonly StyleValueString Start = new(value: "start");
}
