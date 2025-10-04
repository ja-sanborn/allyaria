using Allyaria.Theming.Values;

namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed vertical alignment constants for Allyaria theming.</summary>
public static class VerticalAlign
{
    /// <summary>Represents baseline vertical alignment (default).</summary>
    public static readonly AllyariaStringValue Baseline = new("baseline");

    /// <summary>Represents aligning to the bottom of the element.</summary>
    public static readonly AllyariaStringValue Bottom = new("bottom");

    /// <summary>Represents aligning to the middle of the element.</summary>
    public static readonly AllyariaStringValue Middle = new("middle");

    /// <summary>Represents subscript vertical alignment.</summary>
    public static readonly AllyariaStringValue Sub = new("sub");

    /// <summary>Represents superscript vertical alignment.</summary>
    public static readonly AllyariaStringValue Super = new("super");

    /// <summary>Represents aligning to the bottom of the parent element’s font.</summary>
    public static readonly AllyariaStringValue TextBottom = new("text-bottom");

    /// <summary>Represents aligning to the top of the parent element’s font.</summary>
    public static readonly AllyariaStringValue TextTop = new("text-top");

    /// <summary>Represents aligning to the top of the element.</summary>
    public static readonly AllyariaStringValue Top = new("top");
}
