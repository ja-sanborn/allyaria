namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a composite Allyaria style configuration containing palette, typography, spacing, and border settings.
/// </summary>
public readonly record struct AryStyle
{
    /// <summary>Initializes a new instance of the <see cref="AryStyle" /> struct using default theme elements.</summary>
    public AryStyle()
        : this(null) { }

    /// <summary>Initializes a new instance of the <see cref="AryStyle" /> struct with optional style components.</summary>
    /// <param name="palette">The color palette component, or <c>null</c> to use defaults.</param>
    /// <param name="typography">The typography component, or <c>null</c> to use defaults.</param>
    /// <param name="spacing">The spacing component, or <c>null</c> to use defaults.</param>
    /// <param name="border">The border component, or <c>null</c> to use defaults.</param>
    public AryStyle(AryPalette? palette = null,
        AryTypo? typography = null,
        ArySpacing? spacing = null,
        AryBorders? border = null)
    {
        Palette = palette ?? new AryPalette();
        Typo = typography ?? new AryTypo();
        Spacing = spacing ?? new ArySpacing();
        Border = border ?? new AryBorders();
    }

    /// <summary>Gets the border configuration applied consistently across base, hover, and disabled states.</summary>
    public AryBorders Border { get; init; }

    /// <summary>Gets the color palette configuration for this style.</summary>
    public AryPalette Palette { get; init; }

    /// <summary>Gets the spacing configuration (margins and paddings) applied consistently across states.</summary>
    public ArySpacing Spacing { get; init; }

    /// <summary>Gets the typography configuration defining font styles and text behavior.</summary>
    public AryTypo Typo { get; init; }

    /// <summary>Returns a new <see cref="AryStyle" /> with optionally overridden components.</summary>
    /// <param name="palette">An optional replacement palette.</param>
    /// <param name="typography">An optional replacement typography configuration.</param>
    /// <param name="spacing">An optional replacement spacing configuration.</param>
    /// <param name="border">An optional replacement border configuration.</param>
    /// <returns>A new <see cref="AryStyle" /> instance with provided overrides applied.</returns>
    public AryStyle Cascade(AryPalette? palette = null,
        AryTypo? typography = null,
        ArySpacing? spacing = null,
        AryBorders? border = null)
        => this with
        {
            Palette = palette ?? Palette,
            Typo = typography ?? Typo,
            Spacing = spacing ?? Spacing,
            Border = border ?? Border
        };

    /// <summary>Converts this style and all its components to a single CSS variable declaration string.</summary>
    /// <param name="varPrefix">An optional prefix applied to all generated CSS variable names.</param>
    /// <returns>A concatenated CSS string representing this style configuration.</returns>
    public string ToCss(string? varPrefix = "")
        => string.Concat(
            Palette.ToCss(varPrefix),
            Typo.ToCss(varPrefix),
            Spacing.ToCss(varPrefix),
            Border.ToCss(varPrefix)
        );
}
