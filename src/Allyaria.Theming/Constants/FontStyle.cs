namespace Allyaria.Theming.Constants;

/// <summary>Provides strongly-typed font style constants for Allyaria theming.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class FontStyle
{
    /// <summary>Represents italic font style.</summary>
    public static readonly ThemeString Italic = new("italic");

    /// <summary>Represents normal font style.</summary>
    public static readonly ThemeString Normal = new("normal");

    /// <summary>Represents oblique font style.</summary>
    public static readonly ThemeString Oblique = new("oblique");
}
