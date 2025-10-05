using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Values;
using System.Text;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a self-contained paletteVariant of theme colors and background for Allyaria components, including
/// foreground, background, and border colors, plus optional background image semantics. The paletteVariant enforces
/// minimum contrast for readability and is designed to be composed and transformed (e.g., for disabled/hover states) while
/// respecting theming precedence rules.
/// </summary>
/// <remarks>
/// This type is a <see langword="readonly" /> <see langword="record" /> <see langword="struct" /> to remain value-like and
/// inexpensive to copy. It ensures at least WCAG AA contrast (4.5:1) for body text by adjusting the foreground against the
/// background. Background image semantics follow Allyaria theming rules (image wins when set; size/position applied when
/// <see cref="BackgroundImageStretch" /> is true).
/// </remarks>
public readonly record struct AllyariaPaletteVariant
{
    /// <summary>
    /// Initializes a new paletteVariant with project defaults (light background/foreground) and contrast-corrected foreground.
    /// </summary>
    public AllyariaPaletteVariant()
        : this(null) { }

    /// <summary>
    /// Initializes a new paletteVariant with optional overrides. Any omitted value falls back to project defaults or other
    /// provided values.
    /// </summary>
    /// <param name="backgroundColor">
    /// Background color; defaults to <see cref="StyleDefaults.BackgroundColorLight" /> when <see langword="null" />.
    /// </param>
    /// <param name="foregroundColor">
    /// Foreground (text) color; defaults to <see cref="StyleDefaults.ForegroundColorLight" /> when <see langword="null" />.
    /// </param>
    /// <param name="borderColor">
    /// Border color; defaults to the effective <paramref name="backgroundColor" /> when <see langword="null" />.
    /// </param>
    public AllyariaPaletteVariant(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaColorValue? borderColor = null)
    {
        BackgroundColor = backgroundColor ?? StyleDefaults.BackgroundColorLight;
        ForegroundColor = foregroundColor ?? StyleDefaults.ForegroundColorLight;
        BorderColor = borderColor ?? BackgroundColor;

        var result = ColorHelper.EnsureMinimumContrast(ForegroundColor, BackgroundColor, 4.5);
        ForegroundColor = result.ForegroundColor;
    }

    /// <summary>Gets the background color for the paletteVariant.</summary>
    public AllyariaColorValue BackgroundColor { get; init; }

    /// <summary>Gets the border color for the paletteVariant.</summary>
    public AllyariaColorValue BorderColor { get; init; }

    /// <summary>
    /// Gets the foreground (text) color for the paletteVariant. This value is contrast-corrected against the background.
    /// </summary>
    public AllyariaColorValue ForegroundColor { get; init; }

    /// <summary>
    /// Creates a derived paletteVariant by selectively overriding members, preserving the invariant that the foreground
    /// contrasts sufficiently against the background.
    /// </summary>
    /// <param name="backgroundColor">Optional new background color.</param>
    /// <param name="foregroundColor">Optional new foreground color.</param>
    /// <param name="borderColor">Optional new border color.</param>
    /// <returns>A new <see cref="AllyariaPaletteVariant" /> instance with the provided overrides applied.</returns>
    public AllyariaPaletteVariant Cascade(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaColorValue? borderColor = null)
    {
        var next = this with
        {
            BackgroundColor = backgroundColor ?? BackgroundColor,
            ForegroundColor = foregroundColor ?? ForegroundColor,
            BorderColor = borderColor ?? BorderColor
        };

        var contrasted = ColorHelper.EnsureMinimumContrast(next.ForegroundColor, next.BackgroundColor, 4.5);

        return next with
        {
            ForegroundColor = contrasted.ForegroundColor
        };
    }

    /// <summary>
    /// Converts the paletteVariant to a block of CSS custom property declarations and/or direct declarations, suitable for
    /// inline style emission or CSS variable application via the provided prefix.
    /// </summary>
    /// <param name="varPrefix">
    /// A prefix for CSS variables (if the underlying values resolve to variables), or an empty string for none.
    /// </param>
    /// <returns>
    /// A <see cref="string" /> containing CSS declarations for all margin and padding sides, each terminated with a semicolon.
    /// </returns>
    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();
        builder.ToCss(ForegroundColor, "color", varPrefix);
        builder.ToCss(BackgroundColor, "background-color", varPrefix);
        builder.ToCss(BorderColor, "border-color", varPrefix);

        return builder.ToString();
    }
}
