namespace Allyaria.Theming.Constants.Styling;

/// <summary>Provides strongly-typed vertical alignment constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssVerticalAlign
{
    /// <summary>Represents baseline vertical alignment (default).</summary>
    public static readonly StyleValueString Baseline = new(value: "baseline");

    /// <summary>Represents aligning to the bottom of the element.</summary>
    public static readonly StyleValueString Bottom = new(value: "bottom");

    /// <summary>Represents aligning to the middle of the element.</summary>
    public static readonly StyleValueString Middle = new(value: "middle");

    /// <summary>Represents subscript vertical alignment.</summary>
    public static readonly StyleValueString Sub = new(value: "sub");

    /// <summary>Represents superscript vertical alignment.</summary>
    public static readonly StyleValueString Super = new(value: "super");

    /// <summary>Represents aligning to the bottom of the parent element’s font.</summary>
    public static readonly StyleValueString TextBottom = new(value: "text-bottom");

    /// <summary>Represents aligning to the top of the parent element’s font.</summary>
    public static readonly StyleValueString TextTop = new(value: "text-top");

    /// <summary>Represents aligning to the top of the element.</summary>
    public static readonly StyleValueString Top = new(value: "top");
}
