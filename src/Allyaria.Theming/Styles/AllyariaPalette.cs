using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Values;
using System.Text;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a strongly-typed color palette used by Allyaria theming. Provides background, foreground, and border colors,
/// with automatic contrast adjustment.
/// </summary>
/// <remarks>
/// The palette enforces a minimum contrast ratio of 4.5:1 (WCAG 2.2 AA) between foreground and background colors using
/// <see cref="ColorHelper.EnsureMinimumContrast(AllyariaColorValue, AllyariaColorValue, double)" />.
/// </remarks>
public readonly record struct AllyariaPalette
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaPalette" /> struct using the default light theme colors.
    /// </summary>
    /// <remarks>
    /// This constructor applies <see cref="StyleDefaults.BackgroundColorLight" />,
    /// <see cref="StyleDefaults.ForegroundColorLight" />, and sets the border color equal to the background color.
    /// </remarks>
    public AllyariaPalette()
        : this(null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaPalette" /> struct with optional custom colors. If any value is
    /// <see langword="null" />, defaults are applied from <see cref="StyleDefaults" />.
    /// </summary>
    /// <param name="backgroundColor">Optional background color value.</param>
    /// <param name="foregroundColor">Optional foreground color value.</param>
    /// <param name="borderColor">Optional border color value.</param>
    /// <remarks>The constructor ensures a minimum contrast ratio between the foreground and background colors.</remarks>
    public AllyariaPalette(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaColorValue? borderColor = null)
    {
        BackgroundColor = backgroundColor ?? StyleDefaults.BackgroundColorLight;
        ForegroundColor = foregroundColor ?? StyleDefaults.ForegroundColorLight;
        BorderColor = borderColor ?? BackgroundColor;

        var result = ColorHelper.EnsureMinimumContrast(ForegroundColor, BackgroundColor, 4.5);
        ForegroundColor = result.ForegroundColor;
    }

    /// <summary>Gets or initializes the background color of the palette.</summary>
    public AllyariaColorValue BackgroundColor { get; init; }

    /// <summary>Gets or initializes the border color of the palette.</summary>
    public AllyariaColorValue BorderColor { get; init; }

    /// <summary>Gets or initializes the foreground (text) color of the palette.</summary>
    public AllyariaColorValue ForegroundColor { get; init; }

    /// <summary>
    /// Returns a new <see cref="AllyariaPalette" /> instance with optional color overrides applied. Any parameter that is
    /// <see langword="null" /> retains the current palette’s value.
    /// </summary>
    /// <param name="backgroundColor">Optional override for the background color.</param>
    /// <param name="foregroundColor">Optional override for the foreground color.</param>
    /// <param name="borderColor">Optional override for the border color.</param>
    /// <returns>A new <see cref="AllyariaPalette" /> instance with overrides applied and minimum contrast enforced.</returns>
    /// <remarks>
    /// This method revalidates the contrast ratio between the foreground and background colors after overrides are applied.
    /// </remarks>
    public AllyariaPalette Cascade(AllyariaColorValue? backgroundColor = null,
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

    /// <summary>Builds CSS declarations for the palette’s colors.</summary>
    /// <param name="varPrefix">
    /// Optional prefix used when generating CSS custom properties. If provided, each property name is emitted as
    /// <c>--{varPrefix}-[propertyName]</c>. Hyphens and whitespace in the prefix are normalized; case is lowered.
    /// </param>
    /// <returns>
    /// A string containing CSS color declarations for <c>color</c>, <c>background-color</c>, and <c>border-color</c>.
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
