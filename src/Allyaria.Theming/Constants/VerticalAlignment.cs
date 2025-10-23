namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed vertical alignment constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class VerticalAlignment
{
    /// <summary>Represents baseline vertical alignment (default).</summary>
    public static readonly StyleValueString Baseline = new("baseline");

    /// <summary>Represents aligning to the bottom of the element.</summary>
    public static readonly StyleValueString Bottom = new("bottom");

    /// <summary>Represents aligning to the middle of the element.</summary>
    public static readonly StyleValueString Middle = new("middle");

    /// <summary>Represents subscript vertical alignment.</summary>
    public static readonly StyleValueString Sub = new("sub");

    /// <summary>Represents superscript vertical alignment.</summary>
    public static readonly StyleValueString Super = new("super");

    /// <summary>Represents aligning to the bottom of the parent element’s font.</summary>
    public static readonly StyleValueString TextBottom = new("text-bottom");

    /// <summary>Represents aligning to the top of the parent element’s font.</summary>
    public static readonly StyleValueString TextTop = new("text-top");

    /// <summary>Represents aligning to the top of the element.</summary>
    public static readonly StyleValueString Top = new("top");
}
