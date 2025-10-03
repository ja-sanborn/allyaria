using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Values;
using System.Text;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a resolved color/image palette used by the Allyaria theming engine to generate inline CSS declarations and
/// CSS custom properties (variables). This type applies precedence rules, computes accessible foregrounds, and can derive
/// common UI state variants (e.g., disabled/hover).
/// </summary>
public readonly record struct AllyariaPalette
{
    /// <summary>
    /// The base background color as provided (raw input), before any precedence or derived effects are applied.
    /// </summary>
    private readonly AllyariaColorValue? _backgroundColor;

    /// <summary>Optional background image; <see langword="null" /> when no image was provided.</summary>
    private readonly AllyariaImageValue? _backgroundImage;

    /// <summary>Indicates whether the background image is stretched (<c>true</c>) or tiled (<c>false</c>).</summary>
    private readonly bool _backgroundImageStretch;

    /// <summary>
    /// Optional explicit border color as provided by the caller; may be <see langword="null" /> to signal defaulting.
    /// </summary>
    private readonly AllyariaColorValue? _borderColor;

    /// <summary>
    /// Optional explicit foreground color as provided by the caller; may be <see langword="null" /> to enable contrast-based
    /// defaulting from <see cref="BackgroundColor" />.
    /// </summary>
    private readonly AllyariaColorValue? _foregroundColor;

    /// <summary>Initializes a new instance of the <see cref="AllyariaPalette" /> struct.</summary>
    /// <param name="backgroundColor">
    /// Optional background color. When <see langword="null" />, defaults to
    /// <see cref="Colors.White" />.
    /// </param>
    /// <param name="foregroundColor">
    /// Optional foreground color. When <see langword="null" />, a contrasting foreground is computed against the effective
    /// background using WCAG contrast rules.
    /// </param>
    /// <param name="borderColor">
    /// Optional border color. When <see langword="null" />, the effective <see cref="BackgroundColor" /> is used as the
    /// default.
    /// </param>
    /// <param name="backgroundImage">Optional background image declaration; <see langword="null" /> to omit.</param>
    /// <param name="backgroundImageStretch">
    /// When <see langword="true" />, the background image is rendered using a stretch/cover strategy; otherwise tiled.
    /// </param>
    public AllyariaPalette(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaColorValue? borderColor = null,
        AllyariaImageValue? backgroundImage = null,
        bool backgroundImageStretch = true)
    {
        _backgroundColor = backgroundColor;
        _backgroundImage = backgroundImage;
        _backgroundImageStretch = backgroundImageStretch;
        _borderColor = borderColor;
        _foregroundColor = foregroundColor;
    }

    /// <summary>
    /// Gets the effective background color after precedence is applied; defaults to <see cref="Colors.White" /> when unset.
    /// </summary>
    public AllyariaColorValue BackgroundColor => _backgroundColor ?? Colors.White;

    /// <summary>
    /// Gets the effective background image declaration value, or <see langword="null" /> when no image is set.
    /// </summary>
    public AllyariaImageValue? BackgroundImage => _backgroundImage;

    /// <summary>
    /// Gets the effective border color. If no explicit color is supplied, this defaults to <see cref="BackgroundColor" />. The
    /// presence/visibility of borders is governed by the consuming theme/component (e.g., outline settings), not this type.
    /// </summary>
    public AllyariaColorValue BorderColor => _borderColor ?? BackgroundColor;

    /// <summary>
    /// Gets the resolved foreground color to render against <see cref="BackgroundColor" />. If no explicit foreground is set,
    /// selects black or white based on which yields the higher WCAG contrast ratio. If an explicit foreground is provided, its
    /// hue is preserved and it is adjusted as needed to meet a minimum contrast.
    /// </summary>
    /// <remarks>
    /// Uses precise WCAG contrast calculations (not simple brightness heuristics). When an explicit foreground is provided,
    /// <see cref="ColorHelper.EnsureMinimumContrast(AllyariaColorValue, AllyariaColorValue, double)" /> is used with a default
    /// minimum of 4.5:1 (suitable for normal text) against the effective background.
    /// </remarks>
    public AllyariaColorValue ForegroundColor
    {
        get
        {
            if (_foregroundColor is null)
            {
                var rWhite = ColorHelper.ContrastRatio(Colors.White, BackgroundColor);
                var rBlack = ColorHelper.ContrastRatio(Colors.Black, BackgroundColor);

                return rWhite >= rBlack
                    ? Colors.White
                    : Colors.Black;
            }

            var result = ColorHelper.EnsureMinimumContrast(_foregroundColor, BackgroundColor, 4.5);

            return result.ForegroundColor;
        }
    }

    /// <summary>
    /// Creates a new <see cref="AllyariaPalette" /> by cascading the current palette with optional overrides. Any
    /// <see langword="null" /> parameter inherits the corresponding effective value from this instance.
    /// </summary>
    /// <param name="backgroundColor">Optional new background color override.</param>
    /// <param name="foregroundColor">Optional new foreground color override.</param>
    /// <param name="borderColor">Optional new border color override.</param>
    /// <param name="backgroundImage">Optional new background image override.</param>
    /// <param name="backgroundImageStretch">Optional new background image stretch/tile behavior.</param>
    /// <returns>A new <see cref="AllyariaPalette" /> with the provided overrides applied.</returns>
    public AllyariaPalette Cascade(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaColorValue? borderColor = null,
        AllyariaImageValue? backgroundImage = null,
        bool? backgroundImageStretch = null)
        => new(
            backgroundColor ?? BackgroundColor,
            foregroundColor ?? ForegroundColor,
            borderColor ?? BorderColor,
            backgroundImage ?? BackgroundImage,
            backgroundImageStretch ?? _backgroundImageStretch
        );

    /// <summary>
    /// Builds a CSS declaration string (semicolon-terminated) for the effective palette, suitable for inline <c>style</c>
    /// usage. Includes background color, foreground color, border color, and (if present) background image declarations.
    /// </summary>
    /// <returns>A concatenated CSS string representing the palette.</returns>
    public string ToCss()
    {
        var builder = new StringBuilder();
        builder.Append(BackgroundColor.ToCss("background-color"));
        builder.Append(ForegroundColor.ToCss("color"));
        builder.Append(BorderColor.ToCss("border-color"));

        if (BackgroundImage is not null)
        {
            builder.Append(BackgroundImage.ToCssBackground(BackgroundColor, _backgroundImageStretch));
        }

        return builder.ToString();
    }

    /// <summary>Builds a CSS string containing custom property (CSS variable) declarations for the palette.</summary>
    /// <param name="prefix">
    /// Optional variable name prefix (e.g., <c>"editor"</c> → <c>--editor-…</c>). Any whitespace or hyphen runs are normalized
    /// to single hyphens, and the result is lower-cased. When omitted or blank, the default <c>--aa-</c> prefix is used.
    /// </param>
    /// <returns>
    /// A concatenated CSS string that declares variables for the effective color values (e.g., <c>--aa-color</c>,
    /// <c>--aa-background-color</c>, <c>--aa-border-color</c>) and, when present, background image variables.
    /// </returns>
    public string ToCssVars(string prefix = "")
    {
        var basePrefix = Regex.Replace(prefix, @"[\s-]+", "-").Trim('-').ToLowerInvariant();

        basePrefix = string.IsNullOrWhiteSpace(prefix)
            ? "--aa-"
            : $"--{basePrefix}-";

        var builder = new StringBuilder();
        builder.Append(ForegroundColor.ToCss($"{basePrefix}color"));
        builder.Append(BackgroundColor.ToCss($"{basePrefix}background-color")); // corrected to use BackgroundColor
        builder.Append(BorderColor.ToCss($"{basePrefix}border-color"));

        if (BackgroundImage is not null)
        {
            builder.Append(BackgroundImage.ToCssVarsBackground(basePrefix, BackgroundColor, _backgroundImageStretch));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Produces a derived palette for the <c>[disabled]</c> state by desaturating the background and blending its Value (V)
    /// toward the mid range to reduce emphasis. The foreground is then contrast-corrected to a relaxed minimum suitable for UI
    /// affordances.
    /// </summary>
    /// <param name="desaturateBy">Saturation (S) reduction in percentage points; default <c>60</c>.</param>
    /// <param name="valueBlendTowardMid">Blend factor in <c>[0,1]</c> toward V=50; default <c>0.15</c>.</param>
    /// <param name="minimumContrast">Minimum required contrast ratio; default <c>3.0</c>.</param>
    /// <returns>A new <see cref="AllyariaPalette" /> suitable for the disabled state.</returns>
    /// <remarks>
    /// Hue is preserved for background and (if present) border; only S and V are adjusted. Background images remain unchanged
    /// (image precedence still applies).
    /// </remarks>
    public AllyariaPalette ToDisabledPalette(double desaturateBy = 60.0,
        double valueBlendTowardMid = 0.15,
        double minimumContrast = 3.0)
    {
        var background = AllyariaColorValue.FromHsva(
            BackgroundColor.H,
            Math.Max(0.0, BackgroundColor.S - desaturateBy),
            ColorHelper.Blend(BackgroundColor.V, 50.0, valueBlendTowardMid)
        );

        var border = AllyariaColorValue.FromHsva(
            BorderColor.H,
            Math.Max(0.0, BorderColor.S - desaturateBy),
            ColorHelper.Blend(BorderColor.V, 50.0, valueBlendTowardMid)
        );

        var foreground = ColorHelper.EnsureMinimumContrast(ForegroundColor, background, minimumContrast)
            .ForegroundColor;

        return Cascade(background, foreground, border);
    }

    /// <summary>
    /// Produces a derived palette intended for the <c>:hover</c> state by nudging the background (and the border, if present)
    /// along the HSV Value rail to increase perceived affordance while preserving hue. The foreground is contrast-corrected to
    /// meet WCAG AA for body text.
    /// </summary>
    /// <param name="backgroundDeltaV">
    /// Absolute Value (V) change applied to <see cref="BackgroundColor" />. On light backgrounds (V ≥ 50) the value is
    /// decreased; on dark backgrounds it is increased. Default <c>6</c>.
    /// </param>
    /// <param name="borderDeltaV">
    /// Absolute Value (V) change applied to <see cref="BorderColor" /> when a border is present, mirroring the background
    /// direction. Default <c>8</c>.
    /// </param>
    /// <param name="minimumContrast">Minimum required contrast ratio; default <c>4.5</c> (WCAG AA).</param>
    /// <returns>A new <see cref="AllyariaPalette" /> suitable for the hover state.</returns>
    /// <remarks>
    /// Background images are not altered by this method. If a background image is active, precedence still applies; the
    /// derived background participates in contrast calculations for a readable foreground.
    /// </remarks>
    public AllyariaPalette ToHoverPalette(double backgroundDeltaV = 6.0,
        double borderDeltaV = 8.0,
        double minimumContrast = 4.5)
    {
        var direction = BackgroundColor.V >= 50.0
            ? -1.0
            : +1.0;

        var background = AllyariaColorValue.FromHsva(
            BackgroundColor.H, BackgroundColor.S, BackgroundColor.V + direction * backgroundDeltaV
        );

        var border = AllyariaColorValue.FromHsva(
            BorderColor.H, BorderColor.S, BorderColor.V + direction * borderDeltaV
        );

        var foreground = ColorHelper.EnsureMinimumContrast(ForegroundColor, background, minimumContrast)
            .ForegroundColor;

        return Cascade(background, foreground, border);
    }
}
