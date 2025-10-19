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
public readonly record struct Palette
{
    /// <summary>
    /// Initializes a new <see cref="Palette" /> using theme defaults (Light) with no fill and derived border.
    /// </summary>
    public Palette()
        : this(null) { }

    /// <summary>
    /// Initializes a new <see cref="Palette" /> with optional overrides for background, foreground, fill, and border. Missing
    /// values fall back to theme defaults and computed derivations.
    /// </summary>
    /// <param name="surfaceColor">Optional surface color. Defaults to Light or HighContrast preset.</param>
    /// <param name="backgroundColor">Optional background fill color. Defaults to transparent.</param>
    /// <param name="foregroundColor">
    /// Optional foreground (text) color. Defaults to Light or HighContrast preset, then adjusted for contrast.
    /// </param>
    /// <param name="borderColor">
    /// Optional border color. If not provided, it is derived from foreground/background/fill and contrast rules.
    /// </param>
    /// <param name="isHighContrast">Indicates whether high-contrast rules are active (affects defaults and border derivation).</param>
    public Palette(ThemeColor? surfaceColor = null,
        ThemeColor? backgroundColor = null,
        ThemeColor? foregroundColor = null,
        ThemeColor? borderColor = null,
        bool isHighContrast = false)
    {
        IsHighContrast = isHighContrast;

        SurfaceThemeColor = surfaceColor ?? (IsHighContrast
            ? StyleDefaults.BackgroundThemeColorHighContrast
            : StyleDefaults.BackgroundThemeColorLight);

        BackgroundThemeColor = backgroundColor ?? StyleDefaults.Transparent;

        ForegroundThemeColor = foregroundColor ?? (IsHighContrast
            ? StyleDefaults.ForegroundThemeColorHighContrast
            : StyleDefaults.ForegroundThemeColorLight);

        ForegroundThemeColor = ForegroundThemeColor.EnsureContrast(BackgroundThemeColor);

        var fillForBorder = backgroundColor is null || BackgroundThemeColor == StyleDefaults.Transparent ||
            BackgroundThemeColor == SurfaceThemeColor
                ? null
                : BackgroundThemeColor;

        BorderThemeColor = borderColor ?? ForegroundThemeColor.ToBorder(
            SurfaceThemeColor, fillForBorder, IsHighContrast
        );
    }

    /// <summary>
    /// Gets or initializes the background fill color of the palette. Transparent fill is treated as absent for border
    /// derivation.
    /// </summary>
    public ThemeColor BackgroundThemeColor { get; init; }

    /// <summary>Gets or initializes the border color of the palette.</summary>
    public ThemeColor BorderThemeColor { get; init; }

    /// <summary>
    /// Gets or initializes the foreground (text) color of the palette. This theme is contrast-adjusted against
    /// <see cref="BackgroundThemeColor" /> during construction/cascade.
    /// </summary>
    public ThemeColor ForegroundThemeColor { get; init; }

    /// <summary>Gets or initializes a theme indicating whether high-contrast rules are active.</summary>
    public bool IsHighContrast { get; init; }

    /// <summary>Gets or initializes the surface color for use in contrast and border calculations.</summary>
    public ThemeColor SurfaceThemeColor { get; init; }

    /// <summary>
    /// Creates a new palette by applying the provided overrides and re-computing contrast-sensitive and derived values
    /// (foreground contrast and border).
    /// </summary>
    /// <param name="surfaceColor">Optional surface color override.</param>
    /// <param name="backgroundColor">Optional background fill override.</param>
    /// <param name="foregroundColor">Optional foreground override (will be contrast-adjusted against the resulting fill).</param>
    /// <param name="borderColor">Optional explicit border override; if not provided, the border is derived.</param>
    /// <param name="isHighContrast">Optional high-contrast override.</param>
    /// <returns>
    /// A new <see cref="Palette" /> instance that reflects the overrides, with foreground contrast enforced and border derived
    /// from the resulting colors when not explicitly specified.
    /// </returns>
    public Palette Cascade(ThemeColor? surfaceColor = null,
        ThemeColor? backgroundColor = null,
        ThemeColor? foregroundColor = null,
        ThemeColor? borderColor = null,
        bool? isHighContrast = null)
    {
        // Compute the next-base values first.
        var nextSurface = surfaceColor ?? SurfaceThemeColor;
        var nextBackground = backgroundColor ?? BackgroundThemeColor;
        var nextIsHighContrast = isHighContrast ?? IsHighContrast;

        // Enforce readable contrast for the prospective foreground against the prospective fill.
        var baseForeground = foregroundColor ?? ForegroundThemeColor;
        var contrastedForeground = baseForeground.EnsureContrast(nextBackground);

        // Normalize the fill for border derivation.
        var fillForBorder = nextBackground == StyleDefaults.Transparent || nextBackground == nextSurface
            ? null
            : nextBackground;

        // Derive border if not explicitly supplied, using the *resulting* values.
        var derivedBorder = borderColor ?? contrastedForeground.ToBorder(
            nextSurface, fillForBorder, nextIsHighContrast
        );

        // Return the final next record in one go to avoid intermediate inconsistencies.
        return this with
        {
            SurfaceThemeColor = nextSurface,
            BackgroundThemeColor = nextBackground,
            ForegroundThemeColor = contrastedForeground,
            BorderThemeColor = derivedBorder,
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

        builder.ToCss(BackgroundThemeColor, "background-color", varPrefix);
        builder.ToCss(ForegroundThemeColor, "color", varPrefix);
        builder.ToCss(BorderThemeColor, "border-color", varPrefix);

        return builder.ToString();
    }

    /// <summary>Returns a desaturated version of this palette for disabled UI states.</summary>
    /// <returns>A new <see cref="Palette" /> instance with reduced saturation.</returns>
    public Palette ToDisabled()
        => Cascade(
            SurfaceThemeColor.ToDisabled(),
            BackgroundThemeColor.ToDisabled(),
            ForegroundThemeColor.ToDisabled(),
            BorderThemeColor.ToDisabled()
        );

    /// <summary>Returns a brightened version of this palette for dragged UI states.</summary>
    /// <returns>A new <see cref="Palette" /> for dragged state.</returns>
    public Palette ToDragged()
        => Cascade(
            SurfaceThemeColor.ToDragged(IsHighContrast),
            BackgroundThemeColor.ToDragged(IsHighContrast),
            ForegroundThemeColor.ToDragged(),
            BorderThemeColor.ToDragged(IsHighContrast)
        );

    /// <summary>Returns a slightly elevated version of this palette (Elevation 1).</summary>
    /// <returns>A new <see cref="Palette" /> instance for Elevation 1.</returns>
    public Palette ToElevation1()
        => Cascade(
            SurfaceThemeColor.ToElevation1(IsHighContrast),
            BackgroundThemeColor.ToElevation1(IsHighContrast),
            ForegroundThemeColor,
            BorderThemeColor.ToElevation1(IsHighContrast)
        );

    /// <summary>Returns a moderately elevated version of this palette (Elevation 2).</summary>
    /// <returns>A new <see cref="Palette" /> instance for Elevation 2.</returns>
    public Palette ToElevation2()
        => Cascade(
            SurfaceThemeColor.ToElevation2(IsHighContrast),
            BackgroundThemeColor.ToElevation2(IsHighContrast),
            ForegroundThemeColor,
            BorderThemeColor.ToElevation2(IsHighContrast)
        );

    /// <summary>Returns a higher elevated version of this palette (Elevation 3).</summary>
    /// <returns>A new <see cref="Palette" /> instance for Elevation 3.</returns>
    public Palette ToElevation3()
        => Cascade(
            SurfaceThemeColor.ToElevation3(IsHighContrast),
            BackgroundThemeColor.ToElevation3(IsHighContrast),
            ForegroundThemeColor,
            BorderThemeColor.ToElevation3(IsHighContrast)
        );

    /// <summary>Returns a strongly elevated version of this palette (Elevation 4).</summary>
    /// <returns>A new <see cref="Palette" /> instance for Elevation 4.</returns>
    public Palette ToElevation4()
        => Cascade(
            SurfaceThemeColor.ToElevation4(IsHighContrast),
            BackgroundThemeColor.ToElevation4(IsHighContrast),
            ForegroundThemeColor,
            BorderThemeColor.ToElevation4(IsHighContrast)
        );

    /// <summary>Returns a slightly lighter version of this palette for focused UI states.</summary>
    /// <returns>A new <see cref="Palette" /> for focused state.</returns>
    public Palette ToFocused()
        => Cascade(
            SurfaceThemeColor.ToFocused(IsHighContrast),
            BackgroundThemeColor.ToFocused(IsHighContrast),
            ForegroundThemeColor.ToFocused(),
            BorderThemeColor.ToFocused(IsHighContrast)
        );

    /// <summary>Returns a slightly lighter version of this palette for hovered UI states.</summary>
    /// <returns>A new <see cref="Palette" /> for hovered state.</returns>
    public Palette ToHovered()
        => Cascade(
            SurfaceThemeColor.ToHovered(IsHighContrast),
            BackgroundThemeColor.ToHovered(IsHighContrast),
            ForegroundThemeColor.ToHovered(),
            BorderThemeColor.ToHovered(IsHighContrast)
        );

    /// <summary>Returns a noticeably lighter version of this palette for pressed UI states.</summary>
    /// <returns>A new <see cref="Palette" /> for pressed state.</returns>
    public Palette ToPressed()
        => Cascade(
            SurfaceThemeColor.ToPressed(IsHighContrast),
            BackgroundThemeColor.ToPressed(IsHighContrast),
            ForegroundThemeColor.ToPressed(),
            BorderThemeColor.ToPressed(IsHighContrast)
        );
}
