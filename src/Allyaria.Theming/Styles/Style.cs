namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a composite Allyaria style configuration containing palette, typography, spacing, and border settings.
/// </summary>
public readonly record struct Style
{
    /// <summary>Initializes a new instance of the <see cref="Style" /> struct using default theme elements.</summary>
    public Style()
        : this(null) { }

    /// <summary>Initializes a new instance of the <see cref="Style" /> struct with optional style components.</summary>
    /// <param name="palette">The color palette component, or <c>null</c> to use defaults.</param>
    /// <param name="typography">The typography component, or <c>null</c> to use defaults.</param>
    /// <param name="spacing">The spacing component, or <c>null</c> to use defaults.</param>
    /// <param name="border">The border component, or <c>null</c> to use defaults.</param>
    public Style(Palette? palette = null,
        Typography? typography = null,
        Spacing? spacing = null,
        Borders? border = null)
    {
        Palette = palette ?? new Palette();
        Typography = typography ?? new Typography();
        Spacing = spacing ?? new Spacing();
        Border = border ?? new Borders();
    }

    /// <summary>Gets the border configuration applied consistently across base, hover, and disabled states.</summary>
    public Borders Border { get; init; }

    /// <summary>Gets the color palette configuration for this style.</summary>
    public Palette Palette { get; init; }

    /// <summary>Gets the spacing configuration (margins and paddings) applied consistently across states.</summary>
    public Spacing Spacing { get; init; }

    /// <summary>Gets the typography configuration defining font styles and text behavior.</summary>
    public Typography Typography { get; init; }

    /// <summary>Returns a new <see cref="Style" /> with optionally overridden components.</summary>
    /// <param name="palette">An optional replacement palette.</param>
    /// <param name="typography">An optional replacement typography configuration.</param>
    /// <param name="spacing">An optional replacement spacing configuration.</param>
    /// <param name="border">An optional replacement border configuration.</param>
    /// <returns>A new <see cref="Style" /> instance with provided overrides applied.</returns>
    public Style Cascade(Palette? palette = null,
        Typography? typography = null,
        Spacing? spacing = null,
        Borders? border = null)
        => this with
        {
            Palette = palette ?? Palette,
            Typography = typography ?? Typography,
            Spacing = spacing ?? Spacing,
            Border = border ?? Border
        };

    /// <summary>Converts this style and all its subcomponents into a concatenated CSS variable declaration string.</summary>
    /// <param name="varPrefix">An optional prefix applied to all generated CSS variable names.</param>
    /// <param name="componentType">The logical component type (e.g., Surface, Text, Border) used for CSS scoping.</param>
    /// <param name="elevation">The elevation level applied to the style (used for palette tiering).</param>
    /// <param name="state">The visual state of the component (e.g., Default, Hovered, Focused).</param>
    /// <returns>
    /// A concatenated CSS string that represents the full style configuration for the given component, elevation, and state.
    /// </returns>
    /// <remarks>
    /// This method combines multiple style subsystems—Palette, Typography, Spacing, and Border—into a single CSS string. When
    /// <paramref name="varPrefix" /> is null or whitespace, subsystem CSS values are returned without prefixing.
    /// </remarks>
    public string ToCss(string? varPrefix = "",
        ComponentType componentType = ComponentType.Surface,
        ComponentElevation elevation = ComponentElevation.Mid,
        ComponentState state = ComponentState.Default)
    {
        var prefix = varPrefix.ToCssPrefix();

        if (string.IsNullOrWhiteSpace(prefix))
        {
            return string.Concat(
                Palette.ToCss(),
                Typography.ToCss(),
                Spacing.ToCss(),
                Border.ToCss()
            );
        }

        var typoPrefix = $"{prefix}-{componentType}";
        var palettePrefix = $"{prefix}-{elevation}-{state}";

        return string.Concat(
            Palette.ToCss(palettePrefix),
            Typography.ToCss(typoPrefix),
            Spacing.ToCss(varPrefix),
            Border.ToCss(varPrefix, state is ComponentState.Focused)
        );
    }
}
