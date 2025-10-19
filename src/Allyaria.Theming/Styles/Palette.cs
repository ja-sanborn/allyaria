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
    /// <param name="accentColor">
    /// Optional accent color. If not provided, it is derived from foreground/background/fill and contrast rules.
    /// </param>
    /// <param name="borderColor">
    /// Optional border color. If not provided, it is derived from foreground/background/fill and contrast rules.
    /// </param>
    /// <param name="isHighContrast">Indicates whether high-contrast rules are active (affects defaults and border derivation).</param>
    public Palette(ThemeColor? surfaceColor = null,
        ThemeColor? backgroundColor = null,
        ThemeColor? foregroundColor = null,
        ThemeColor? accentColor = null,
        ThemeColor? borderColor = null,
        bool isHighContrast = false)
    {
        IsHighContrast = isHighContrast;

        SurfaceThemeColor = surfaceColor ?? (IsHighContrast
            ? StyleDefaults.BackgroundColorHighContrast
            : StyleDefaults.BackgroundColorLight);

        BackgroundColor = backgroundColor ?? StyleDefaults.Transparent;

        ForegroundColor = foregroundColor ?? (IsHighContrast
            ? StyleDefaults.ForegroundColorHighContrast
            : StyleDefaults.ForegroundColorLight);

        ForegroundColor = ForegroundColor.EnsureContrast(BackgroundColor);
        AccentColor = accentColor ?? ForegroundColor.ToAccent(IsHighContrast);

        var fillForBorder = backgroundColor is null || BackgroundColor == StyleDefaults.Transparent ||
            BackgroundColor == SurfaceThemeColor
                ? null
                : BackgroundColor;

        BorderColor = borderColor ?? ForegroundColor.ToBorder(SurfaceThemeColor, fillForBorder, IsHighContrast);
    }

    /// <summary>
    /// Gets or initializes the accent color of the palette calculated from the <see cref="ForegroundColor" />.
    /// </summary>
    public ThemeColor AccentColor { get; init; }

    /// <summary>
    /// Gets or initializes the background fill color of the palette. Transparent fill is treated as absent for border
    /// derivation.
    /// </summary>
    public ThemeColor BackgroundColor { get; init; }

    /// <summary>Gets or initializes the border color of the palette.</summary>
    public ThemeColor BorderColor { get; init; }

    /// <summary>
    /// Gets or initializes the foreground (text) color of the palette. This theme is contrast-adjusted against
    /// <see cref="BackgroundColor" /> during construction/cascade.
    /// </summary>
    public ThemeColor ForegroundColor { get; init; }

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
    /// <param name="accentColor">Optional explicit accent override; if not provided, the accent is derived.</param>
    /// <param name="borderColor">Optional explicit border override; if not provided, the border is derived.</param>
    /// <param name="isHighContrast">Optional high-contrast override.</param>
    /// <returns>
    /// A new <see cref="Palette" /> instance that reflects the overrides, with foreground contrast enforced and border derived
    /// from the resulting colors when not explicitly specified.
    /// </returns>
    public Palette Cascade(ThemeColor? surfaceColor = null,
        ThemeColor? backgroundColor = null,
        ThemeColor? foregroundColor = null,
        ThemeColor? accentColor = null,
        ThemeColor? borderColor = null,
        bool? isHighContrast = null)
    {
        // Compute the next-base values first.
        var nextSurface = surfaceColor ?? SurfaceThemeColor;
        var nextBackground = backgroundColor ?? BackgroundColor;
        var nextIsHighContrast = isHighContrast ?? IsHighContrast;

        // Enforce readable contrast for the prospective foreground against the prospective fill.
        var baseForeground = foregroundColor ?? ForegroundColor;
        var contrastedForeground = baseForeground.EnsureContrast(nextBackground);

        // Derive the accent color if not explicitly supplied.
        var nextAccent = accentColor ?? contrastedForeground.ToAccent(nextIsHighContrast);

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
            BackgroundColor = nextBackground,
            ForegroundColor = contrastedForeground,
            AccentColor = nextAccent,
            BorderColor = derivedBorder,
            IsHighContrast = nextIsHighContrast
        };
    }

    /// <summary>Builds CSS declarations for the palette’s colors.</summary>
    /// <param name="varPrefix">
    /// Optional prefix used when generating CSS custom properties. If provided, each property name is emitted as
    /// <c>--{varPrefix}-[propertyName]</c>. Hyphens and whitespace in the prefix may be normalized by the underlying helper.
    /// </param>
    /// <param name="includeBackground">Include the background color in the CSS output.</param>
    /// <returns>
    /// A string containing CSS color declarations for <c>background-color</c>, <c>color</c>, and <c>border-color</c>.
    /// </returns>
    /// <remarks>
    /// Output relies on <see cref="StringBuilder" /> extension helpers to emit inline declarations and optional custom
    /// properties.
    /// </remarks>
    public string ToCss(string? varPrefix = "", bool includeBackground = true)
    {
        var builder = new StringBuilder();

        if (includeBackground)
        {
            builder.ToCss(BackgroundColor, "background-color", varPrefix);
        }

        builder.ToCss(ForegroundColor, "color", varPrefix);
        builder.ToCss(BorderColor, "border-color", varPrefix);
        builder.ToCss(AccentColor, "accent-color", varPrefix);

        return builder.ToString();
    }

    /// <summary>Returns a desaturated version of this palette for disabled UI states.</summary>
    /// <returns>A new <see cref="Palette" /> instance with reduced saturation.</returns>
    public Palette ToDisabled()
        => Cascade(
            SurfaceThemeColor.ToDisabled(),
            BackgroundColor.ToDisabled(),
            ForegroundColor.ToDisabled(),
            AccentColor.ToDisabled(),
            BorderColor.ToDisabled()
        );

    /// <summary>Returns a brightened version of this palette for dragged UI states.</summary>
    /// <returns>A new <see cref="Palette" /> for dragged state.</returns>
    public Palette ToDragged()
        => Cascade(
            SurfaceThemeColor.ToDragged(IsHighContrast),
            BackgroundColor.ToDragged(IsHighContrast),
            ForegroundColor.ToDragged(),
            AccentColor.ToDragged(),
            BorderColor.ToDragged(IsHighContrast)
        );

    /// <summary>Returns a slightly elevated version of this palette (Elevation 1).</summary>
    /// <returns>A new <see cref="Palette" /> instance for Elevation 1.</returns>
    public Palette ToElevation1()
        => Cascade(
            SurfaceThemeColor.ToElevation1(IsHighContrast),
            BackgroundColor.ToElevation1(IsHighContrast),
            ForegroundColor,
            AccentColor,
            BorderColor.ToElevation1(IsHighContrast)
        );

    /// <summary>Returns a moderately elevated version of this palette (Elevation 2).</summary>
    /// <returns>A new <see cref="Palette" /> instance for Elevation 2.</returns>
    public Palette ToElevation2()
        => Cascade(
            SurfaceThemeColor.ToElevation2(IsHighContrast),
            BackgroundColor.ToElevation2(IsHighContrast),
            ForegroundColor,
            AccentColor,
            BorderColor.ToElevation2(IsHighContrast)
        );

    /// <summary>Returns a higher elevated version of this palette (Elevation 3).</summary>
    /// <returns>A new <see cref="Palette" /> instance for Elevation 3.</returns>
    public Palette ToElevation3()
        => Cascade(
            SurfaceThemeColor.ToElevation3(IsHighContrast),
            BackgroundColor.ToElevation3(IsHighContrast),
            ForegroundColor,
            AccentColor,
            BorderColor.ToElevation3(IsHighContrast)
        );

    /// <summary>Returns a strongly elevated version of this palette (Elevation 4).</summary>
    /// <returns>A new <see cref="Palette" /> instance for Elevation 4.</returns>
    public Palette ToElevation4()
        => Cascade(
            SurfaceThemeColor.ToElevation4(IsHighContrast),
            BackgroundColor.ToElevation4(IsHighContrast),
            ForegroundColor,
            AccentColor,
            BorderColor.ToElevation4(IsHighContrast)
        );

    /// <summary>Returns a slightly lighter version of this palette for focused UI states.</summary>
    /// <returns>A new <see cref="Palette" /> for focused state.</returns>
    public Palette ToFocused()
        => Cascade(
            SurfaceThemeColor.ToFocused(IsHighContrast),
            BackgroundColor.ToFocused(IsHighContrast),
            ForegroundColor.ToFocused(),
            AccentColor.ToFocused(),
            BorderColor.ToFocused(IsHighContrast)
        );

    /// <summary>Returns a slightly lighter version of this palette for hovered UI states.</summary>
    /// <returns>A new <see cref="Palette" /> for hovered state.</returns>
    public Palette ToHovered()
        => Cascade(
            SurfaceThemeColor.ToHovered(IsHighContrast),
            BackgroundColor.ToHovered(IsHighContrast),
            ForegroundColor.ToHovered(),
            AccentColor.ToHovered(),
            BorderColor.ToHovered(IsHighContrast)
        );

    /// <summary>Returns a noticeably lighter version of this palette for pressed UI states.</summary>
    /// <returns>A new <see cref="Palette" /> for pressed state.</returns>
    public Palette ToPressed()
        => Cascade(
            SurfaceThemeColor.ToPressed(IsHighContrast),
            BackgroundColor.ToPressed(IsHighContrast),
            ForegroundColor.ToPressed(),
            AccentColor.ToPressed(),
            BorderColor.ToPressed(IsHighContrast)
        );
}
