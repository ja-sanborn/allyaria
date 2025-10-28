namespace Allyaria.Theming.Constants.Styling;

/// <summary>Provides strongly-typed font style constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssFontStyle
{
    /// <summary>Represents italic font style.</summary>
    public static readonly StyleValueString Italic = new(value: "italic");

    /// <summary>Represents normal font style.</summary>
    public static readonly StyleValueString Normal = new(value: "normal");

    /// <summary>Represents oblique font style.</summary>
    public static readonly StyleValueString Oblique = new(value: "oblique");
}
