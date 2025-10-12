using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using System.Text;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a computed color palette (background, foreground, fill, border) for Allyaria themes. Construction and
/// cascading honor theme defaults, enforce readable contrast for text, and derive border color from the current
/// background/foreground/fill in accordance with theming rules.
/// </summary>
/// <remarks>
///     <para>
///     Behavior aligns with Allyaria theming precedence: explicit values override defaults; foreground is adjusted to
///     maintain contrast against the fill; border color is derived from foreground/background (and optional fill) with
///     high-contrast considerations.
///     </para>
/// </remarks>
public readonly record struct AryPalette
{
    /// <summary>
    /// Initializes a new <see cref="AryPalette" /> using theme defaults (Light) with no fill and derived border.
    /// </summary>
    /// <remarks>
    /// Equivalent to
    /// <c>
    /// new AryPalette(backgroundColor: null, foregroundColor: null, fillColor: null, borderColor: null, isHighContrast: false)
    /// </c>
    /// .
    /// </remarks>
    public AryPalette()
        : this(null) { }

    /// <summary>
    /// Initializes a new <see cref="AryPalette" /> with optional overrides for background, foreground, fill, and border.
    /// Missing values fall back to theme defaults and computed derivations.
    /// </summary>
    /// <param name="backgroundColor">Optional background color. Defaults to Light or HighContrast preset.</param>
    /// <param name="foregroundColor">
    /// Optional foreground (text) color. Defaults to Light or HighContrast preset, then adjusted for contrast.
    /// </param>
    /// <param name="fillColor">Optional fill (background layer) color. Defaults to transparent.</param>
    /// <param name="borderColor">
    /// Optional border color. If not provided, it is derived from foreground/background/fill and contrast rules.
    /// </param>
    /// <param name="isHighContrast">Indicates whether high-contrast rules are active (affects defaults and border derivation).</param>
    public AryPalette(AryColorValue? backgroundColor = null,
        AryColorValue? foregroundColor = null,
        AryColorValue? fillColor = null,
        AryColorValue? borderColor = null,
        bool isHighContrast = false)
    {
        IsHighContrast = isHighContrast;

        BackgroundColor = backgroundColor ?? (IsHighContrast
            ? StyleDefaults.BackgroundColorHighContrast
            : StyleDefaults.BackgroundColorLight);

        FillColor = fillColor ?? StyleDefaults.Transparent;

        ForegroundColor = foregroundColor ?? (IsHighContrast
            ? StyleDefaults.ForegroundColorHighContrast
            : StyleDefaults.ForegroundColorLight);

        ForegroundColor = ForegroundColor.EnsureContrast(FillColor);

        var fillForBorder = fillColor is null || FillColor == StyleDefaults.Transparent || FillColor == BackgroundColor
            ? null
            : FillColor;

        BorderColor = borderColor ?? ForegroundColor.ToBorder(BackgroundColor, fillForBorder, IsHighContrast);
    }

    /// <summary>Gets or initializes the background color of the palette.</summary>
    public AryColorValue BackgroundColor { get; init; }

    /// <summary>Gets or initializes the border color of the palette.</summary>
    public AryColorValue BorderColor { get; init; }

    /// <summary>
    /// Gets or initializes the fill color of the palette. Transparent fill is treated as absent for border derivation.
    /// </summary>
    public AryColorValue FillColor { get; init; }

    /// <summary>
    /// Gets or initializes the foreground (text) color of the palette. This value is contrast-adjusted against
    /// <see cref="FillColor" /> during construction/cascade.
    /// </summary>
    public AryColorValue ForegroundColor { get; init; }

    /// <summary>Gets or initializes a value indicating whether high-contrast rules are active.</summary>
    public bool IsHighContrast { get; init; }

    /// <summary>
    /// Creates a new palette by applying the provided overrides and re-computing contrast-sensitive and derived values
    /// (foreground contrast and border).
    /// </summary>
    /// <param name="backgroundColor">Optional background override.</param>
    /// <param name="foregroundColor">Optional foreground override (will be contrast-adjusted against the resulting fill).</param>
    /// <param name="fillColor">Optional fill override.</param>
    /// <param name="borderColor">Optional explicit border override; if not provided, the border is derived.</param>
    /// <param name="isHighContrast">Optional high-contrast override.</param>
    /// <returns>
    /// A new <see cref="AryPalette" /> instance that reflects the overrides, with foreground contrast enforced and border
    /// derived from the resulting colors when not explicitly specified.
    /// </returns>
    public AryPalette Cascade(AryColorValue? backgroundColor = null,
        AryColorValue? foregroundColor = null,
        AryColorValue? fillColor = null,
        AryColorValue? borderColor = null,
        bool? isHighContrast = null)
    {
        // Compute the next-base values first.
        var nextBackground = backgroundColor ?? BackgroundColor;
        var nextFill = fillColor ?? FillColor;
        var nextIsHighContrast = isHighContrast ?? IsHighContrast;

        // Enforce readable contrast for the prospective foreground against the prospective fill.
        var baseForeground = foregroundColor ?? ForegroundColor;
        var contrastedForeground = baseForeground.EnsureContrast(nextFill);

        // Normalize the fill for border derivation.
        var fillForBorder = nextFill == StyleDefaults.Transparent || nextFill == nextBackground
            ? null
            : nextFill;

        // Derive border if not explicitly supplied, using the *resulting* values.
        var derivedBorder = borderColor ?? contrastedForeground.ToBorder(
            nextBackground, fillForBorder, nextIsHighContrast
        );

        // Return the final next record in one go to avoid intermediate inconsistencies.
        return this with
        {
            BackgroundColor = nextBackground,
            FillColor = nextFill,
            ForegroundColor = contrastedForeground,
            BorderColor = derivedBorder,
            IsHighContrast = nextIsHighContrast
        };
    }

    /// <summary>Builds CSS declarations for the palette’s colors.</summary>
    /// <param name="varPrefix">
    /// Optional prefix used when generating CSS custom properties. If provided, each property name is emitted as
    /// <c>--{varPrefix}-[propertyName]</c>. Hyphens and whitespace in the prefix may be normalized by the underlying helper.
    /// </param>
    /// <returns>
    /// A string containing CSS color declarations for <c>background-color</c>, <c>color</c>, and <c>border-color</c>.
    /// </returns>
    /// <remarks>
    /// Output relies on <see cref="StringBuilder" /> extension helpers to emit inline declarations and optional custom
    /// properties.
    /// </remarks>
    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();

        builder.ToCss(FillColor, "background-color", varPrefix);
        builder.ToCss(ForegroundColor, "color", varPrefix);
        builder.ToCss(BorderColor, "border-color", varPrefix);

        return builder.ToString();
    }

    /// <summary>Returns a desaturated version of this palette for disabled UI states.</summary>
    /// <returns>A new <see cref="AryPalette" /> instance with reduced saturation.</returns>
    public AryPalette ToDisabled()
        => new(
            BackgroundColor.ToDisabled(),
            ForegroundColor.ToDisabled(),
            FillColor.ToDisabled(),
            BorderColor.ToDisabled(),
            IsHighContrast
        );

    /// <summary>Returns a brightened version of this palette for dragged UI states.</summary>
    /// <returns>A new <see cref="AryPalette" /> for dragged state.</returns>
    public AryPalette ToDragged()
        => new(
            BackgroundColor.ToDragged(IsHighContrast),
            ForegroundColor.ToDragged(),
            FillColor.ToDragged(IsHighContrast),
            BorderColor.ToDragged(IsHighContrast),
            IsHighContrast
        );

    /// <summary>Returns a slightly elevated version of this palette (Elevation 1).</summary>
    /// <returns>A new <see cref="AryPalette" /> instance for Elevation 1.</returns>
    public AryPalette ToElevation1()
        => new(
            BackgroundColor.ToElevation1(IsHighContrast),
            ForegroundColor,
            FillColor.ToElevation1(IsHighContrast),
            BorderColor.ToElevation1(IsHighContrast),
            IsHighContrast
        );

    /// <summary>Returns a moderately elevated version of this palette (Elevation 2).</summary>
    /// <returns>A new <see cref="AryPalette" /> instance for Elevation 2.</returns>
    public AryPalette ToElevation2()
        => new(
            BackgroundColor.ToElevation2(IsHighContrast),
            ForegroundColor,
            FillColor.ToElevation2(IsHighContrast),
            BorderColor.ToElevation2(IsHighContrast),
            IsHighContrast
        );

    /// <summary>Returns a higher elevated version of this palette (Elevation 3).</summary>
    /// <returns>A new <see cref="AryPalette" /> instance for Elevation 3.</returns>
    public AryPalette ToElevation3()
        => new(
            BackgroundColor.ToElevation3(IsHighContrast),
            ForegroundColor,
            FillColor.ToElevation3(IsHighContrast),
            BorderColor.ToElevation3(IsHighContrast),
            IsHighContrast
        );

    /// <summary>Returns a strongly elevated version of this palette (Elevation 4).</summary>
    /// <returns>A new <see cref="AryPalette" /> instance for Elevation 4.</returns>
    public AryPalette ToElevation4()
        => new(
            BackgroundColor.ToElevation4(IsHighContrast),
            ForegroundColor,
            FillColor.ToElevation4(IsHighContrast),
            BorderColor.ToElevation4(IsHighContrast),
            IsHighContrast
        );

    /// <summary>Returns the highest elevated version of this palette (Elevation 5).</summary>
    /// <returns>A new <see cref="AryPalette" /> instance for Elevation 5.</returns>
    public AryPalette ToElevation5()
        => new(
            BackgroundColor.ToElevation5(IsHighContrast),
            ForegroundColor,
            FillColor.ToElevation5(IsHighContrast),
            BorderColor.ToElevation5(IsHighContrast),
            IsHighContrast
        );

    /// <summary>Returns a slightly lighter version of this palette for focused UI states.</summary>
    /// <returns>A new <see cref="AryPalette" /> for focused state.</returns>
    public AryPalette ToFocused()
        => new(
            BackgroundColor.ToFocused(IsHighContrast),
            ForegroundColor.ToFocused(),
            FillColor.ToFocused(IsHighContrast),
            BorderColor.ToFocused(IsHighContrast),
            IsHighContrast
        );

    /// <summary>Returns a slightly lighter version of this palette for hovered UI states.</summary>
    /// <returns>A new <see cref="AryPalette" /> for hovered state.</returns>
    public AryPalette ToHovered()
        => new(
            BackgroundColor.ToHovered(IsHighContrast),
            ForegroundColor.ToHovered(),
            FillColor.ToHovered(IsHighContrast),
            BorderColor.ToHovered(IsHighContrast),
            IsHighContrast
        );

    /// <summary>Returns a noticeably lighter version of this palette for pressed UI states.</summary>
    /// <returns>A new <see cref="AryPalette" /> for pressed state.</returns>
    public AryPalette ToPressed()
        => new(
            BackgroundColor.ToPressed(IsHighContrast),
            ForegroundColor.ToPressed(),
            FillColor.ToPressed(IsHighContrast),
            BorderColor.ToPressed(IsHighContrast),
            IsHighContrast
        );
}
