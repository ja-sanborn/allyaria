using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Values;
using System.Text;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a self-contained palette of theme colors and background for Allyaria components, including foreground,
/// background, and border colors, plus optional background image semantics. The palette enforces minimum contrast for
/// readability and is designed to be composed and transformed (e.g., for disabled/hover states) while respecting theming
/// precedence rules.
/// </summary>
/// <remarks>
/// This type is a <see langword="readonly" /> <see langword="record" /> <see langword="struct" /> to remain value-like and
/// inexpensive to copy. It ensures at least WCAG AA contrast (4.5:1) for body text by adjusting the foreground against the
/// background. Background image semantics follow Allyaria theming rules (image wins when set; size/position applied when
/// <see cref="BackgroundImageStretch" /> is true).
/// </remarks>
public readonly record struct AllyariaPalette
{
    /// <summary>
    /// Initializes a new palette with project defaults (light background/foreground) and contrast-corrected foreground.
    /// </summary>
    public AllyariaPalette()
        : this(null) { }

    /// <summary>
    /// Initializes a new palette with optional overrides. Any omitted value falls back to project defaults or other provided
    /// values.
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
    /// <param name="backgroundImage">Optional background image URL/string. Whitespace becomes empty (no image).</param>
    /// <param name="backgroundImageStretch">
    /// When <see langword="true" />, applies <c>center / cover no-repeat</c> sizing to the background image. Defaults to
    /// <see langword="false" />.
    /// </param>
    public AllyariaPalette(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaColorValue? borderColor = null,
        string? backgroundImage = null,
        bool? backgroundImageStretch = null)
    {
        BackgroundColor = backgroundColor ?? StyleDefaults.BackgroundColorLight;
        ForegroundColor = foregroundColor ?? StyleDefaults.ForegroundColorLight;
        BorderColor = borderColor ?? BackgroundColor;

        BackgroundImage = string.IsNullOrWhiteSpace(backgroundImage)
            ? string.Empty
            : backgroundImage.Trim();

        BackgroundImageStretch = backgroundImageStretch ?? false;

        var result = ColorHelper.EnsureMinimumContrast(ForegroundColor, BackgroundColor, 4.5);
        ForegroundColor = result.ForegroundColor;
    }

    /// <summary>Gets the background color for the palette.</summary>
    public AllyariaColorValue BackgroundColor { get; init; }

    /// <summary>Gets the background image (empty string if none).</summary>
    public string BackgroundImage { get; init; }

    /// <summary>
    /// Gets a value indicating whether the background image should be stretched with <c>cover center no-repeat</c>.
    /// </summary>
    public bool BackgroundImageStretch { get; init; }

    /// <summary>Gets the border color for the palette.</summary>
    public AllyariaColorValue BorderColor { get; init; }

    /// <summary>
    /// Gets the foreground (text) color for the palette. This value is contrast-corrected against the background.
    /// </summary>
    public AllyariaColorValue ForegroundColor { get; init; }

    /// <summary>
    /// Creates a derived palette by selectively overriding members, preserving the invariant that the foreground contrasts
    /// sufficiently against the background.
    /// </summary>
    /// <param name="backgroundColor">Optional new background color.</param>
    /// <param name="foregroundColor">Optional new foreground color.</param>
    /// <param name="borderColor">Optional new border color.</param>
    /// <param name="backgroundImage">Optional new background image string; when provided, whitespace becomes empty.</param>
    /// <param name="backgroundImageStretch">Optional new image stretch flag.</param>
    /// <returns>A new <see cref="AllyariaPalette" /> instance with the provided overrides applied.</returns>
    public AllyariaPalette Cascade(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaColorValue? borderColor = null,
        string? backgroundImage = null,
        bool? backgroundImageStretch = null)
    {
        var next = this with
        {
            BackgroundColor = backgroundColor ?? BackgroundColor,
            ForegroundColor = foregroundColor ?? ForegroundColor,
            BorderColor = borderColor ?? BorderColor,

            BackgroundImage = backgroundImage is null
                ? BackgroundImage
                : string.IsNullOrWhiteSpace(backgroundImage)
                    ? string.Empty
                    : backgroundImage.Trim(),

            BackgroundImageStretch = backgroundImageStretch ?? BackgroundImageStretch
        };

        var contrasted = ColorHelper.EnsureMinimumContrast(next.ForegroundColor, next.BackgroundColor, 4.5);

        return next with
        {
            ForegroundColor = contrasted.ForegroundColor
        };
    }

    /// <summary>
    /// Converts the palette to a block of CSS custom property declarations and/or direct declarations, suitable for inline
    /// style emission or CSS variable application via the provided prefix.
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

        if (!string.IsNullOrWhiteSpace(BackgroundImage))
        {
            builder.ToCss(new AllyariaImageValue(BackgroundImage, BackgroundColor), "background-image", varPrefix);

            if (BackgroundImageStretch)
            {
                builder.ToCss(new AllyariaStringValue("center"), "background-position", varPrefix);
                builder.ToCss(new AllyariaStringValue("no-repeat"), "background-repeat", varPrefix);
                builder.ToCss(new AllyariaStringValue("cover"), "background-size", varPrefix);
            }
        }

        return builder.ToString();
    }
}
