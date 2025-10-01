using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;
using System.Diagnostics.CodeAnalysis;

namespace Allyaria.Theming.Constants;

/// <summary>
/// Provides predefined <see cref="AllyariaStyle" /> presets for light, dark, and high-contrast themes.
/// </summary>
/// <remarks>
/// This class exposes WCAG-compliant style constants that combine palette, typography, and spacing. These presets are
/// intended as safe defaults for Material Design–inspired UIs. Individual properties (typography, spacing, palette) are
/// also exposed so they can be reused directly or cascaded into custom styles.
/// </remarks>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class Styles
{
    /// <summary>
    /// Gets the default palette used by style presets. Uses a light surface (<c>Grey50</c>) with dark foreground text (
    /// <c>Grey900</c>).
    /// </summary>
    public static AllyariaPalette DefaultPalette => new(Colors.Grey50, Colors.Grey900);

    /// <summary>
    /// Gets the default spacing used by style presets. Applies <c>8px</c> margins and <c>16px</c> paddings, consistent with
    /// the Material Design 8dp grid.
    /// </summary>
    public static AllyariaSpacing DefaultSpacing
        => new(
            Sizing.Size2, Sizing.Size2, Sizing.Size2, Sizing.Size2,
            Sizing.Size3, Sizing.Size3, Sizing.Size3, Sizing.Size3
        );

    /// <summary>
    /// Gets a default WCAG-compliant dark style preset. Uses a dark surface (<c>Grey900</c>) with light foreground text (
    /// <c>Grey50</c> ), paired with default typography and spacing.
    /// </summary>
    public static AllyariaStyle DefaultThemeDark { get; } = new(
        DefaultPalette.Cascade(Colors.Grey900, Colors.Grey50),
        DefaultTypography,
        DefaultSpacing
    );

    /// <summary>
    /// Gets a default high-contrast grayscale style preset. Uses a white surface with black text for maximum legibility,
    /// paired with default typography and spacing. Useful for accessibility contexts requiring strong visual contrast.
    /// </summary>
    public static AllyariaStyle DefaultThemeHighContrast { get; } = new(
        DefaultPalette.Cascade(Colors.White, Colors.Black),
        DefaultTypography,
        DefaultSpacing
    );

    /// <summary>
    /// Gets a default WCAG-compliant light style preset. Uses a light surface (<c>Grey50</c>) with dark foreground text (
    /// <c>Grey900</c>), paired with default typography and spacing.
    /// </summary>
    public static AllyariaStyle DefaultThemeLight { get; } = new(
        DefaultPalette,
        DefaultTypography,
        DefaultSpacing
    );

    /// <summary>
    /// Gets the default typography used by style presets. Uses a system-first font stack for Material Design–aligned
    /// cross-platform rendering, with a base size of <c>1rem</c>.
    /// </summary>
    public static AllyariaTypography DefaultTypography
        => new(
            new AllyariaStringValue(
                "system-ui, -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Helvetica, Arial, sans-serif"
            ),
            new AllyariaNumberValue("1rem")
        );
}
