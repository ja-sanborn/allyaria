using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;
using System.Diagnostics.CodeAnalysis;

namespace Allyaria.Theming.Constants;

/// <summary>Provides predefined Allyaria styles for light, dark, and high-contrast themes.</summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class Styles
{
    /// <summary>Gets a WCAG-compliant dark style preset with Material colors and serif typography.</summary>
    public static AllyariaStyle Dark { get; } = new(
        new AllyariaPalette(Colors.Grey900, Colors.Grey50),
        new AllyariaTypography(
            new AllyariaStringValue("Segoe UI, Roboto, Helvetica, Arial, sans-serif"),
            new AllyariaStringValue("1rem")
        )
    );

    /// <summary>Gets a high-contrast grayscale style preset with serif typography.</summary>
    public static AllyariaStyle HighContrast { get; } = new(
        new AllyariaPalette(Colors.White, Colors.Black),
        new AllyariaTypography(
            new AllyariaStringValue("Segoe UI, Roboto, Helvetica, Arial, sans-serif"),
            new AllyariaStringValue("1rem")
        )
    );

    /// <summary>Gets a WCAG-compliant light style preset with Material colors and serif typography.</summary>
    public static AllyariaStyle Light { get; } = new(
        new AllyariaPalette(Colors.Grey50, Colors.Grey900),
        new AllyariaTypography(
            new AllyariaStringValue("Segoe UI, Roboto, Helvetica, Arial, sans-serif"),
            new AllyariaStringValue("1rem")
        )
    );
}
