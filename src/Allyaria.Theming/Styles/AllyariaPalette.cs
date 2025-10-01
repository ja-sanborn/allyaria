using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Values;
using System.Text;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Immutable, strongly typed palette used by the Allyaria theme engine to compute effective foreground, background, and
/// border styles (including hover/disabled variants) according to documented precedence rules.
/// </summary>
/// <remarks>
/// This type is intended for inline style generation and CSS custom property emission. It follows Allyaria theming
/// precedence: background images override region colors; explicit overrides beat defaults; and borders are opt-in
/// (rendered only when a width is provided). See <see cref="ToCss" /> and <see cref="ToCssVars(string)" />.
/// </remarks>
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

    /// <summary>Optional border radius token (e.g., <c>4px</c>) or <see langword="null" /> to omit the declaration.</summary>
    private readonly AllyariaStringValue? _borderRadius;

    /// <summary>Border style token (e.g., <c>solid</c>). Defaults to <c>solid</c> when not supplied.</summary>
    private readonly AllyariaStringValue? _borderStyle;

    /// <summary>
    /// Optional border width in CSS pixels; <see langword="null" /> or a non-positive value omits the border.
    /// </summary>
    private readonly int? _borderWidth;

    /// <summary>
    /// Optional explicit foreground color as provided by the caller; may be <see langword="null" /> to enable contrast-based
    /// defaulting from <see cref="BackgroundColor" />.
    /// </summary>
    private readonly AllyariaColorValue? _foregroundColor;

    /// <summary>Initializes a new immutable <see cref="AllyariaPalette" />.</summary>
    /// <param name="backgroundColor">Optional base background color; defaults to <see cref="Colors.White" />.</param>
    /// <param name="foregroundColor">
    /// Optional explicit foreground color; when not provided, it is derived for contrast against
    /// <see cref="BackgroundColor" />.
    /// </param>
    /// <param name="backgroundImage">
    /// Optional background image source. When present, image precedence applies (an overlay/composite value is produced by the
    /// image helper), and background color is visually behind it.
    /// </param>
    /// <param name="backgroundImageStretch">
    /// Whether the background image should be stretched (<c>true</c>) or tiled (
    /// <c>false</c>).
    /// </param>
    /// <param name="borderWidth">
    /// Border width in CSS pixels. Values ≤ 0 omit the border entirely; values &gt; 0 render as <c>&lt;width&gt;px</c>.
    /// </param>
    /// <param name="borderColor">
    /// Optional explicit border color. When not provided but a border is present, the effective color defaults to
    /// <see cref="BackgroundColor" />.
    /// </param>
    /// <param name="borderStyle">Optional border style token (e.g., <c>solid</c>, <c>dashed</c>). Defaults to <c>solid</c>.</param>
    /// <param name="borderRadius">Optional border radius token (e.g., <c>4px</c>); omitted when <see langword="null" />.</param>
    public AllyariaPalette(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaImageValue? backgroundImage = null,
        bool backgroundImageStretch = true,
        int? borderWidth = 0,
        AllyariaColorValue? borderColor = null,
        AllyariaStringValue? borderStyle = null,
        AllyariaStringValue? borderRadius = null)
    {
        _backgroundColor = backgroundColor;
        _backgroundImage = backgroundImage;
        _backgroundImageStretch = backgroundImageStretch;
        _borderColor = borderColor ?? _backgroundColor;
        _borderRadius = borderRadius;
        _borderStyle = borderStyle;
        _borderWidth = borderWidth;
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
    /// Gets the effective border color. If no explicit color is supplied, this defaults to <see cref="BackgroundColor" />.
    /// Note that a border is only rendered when <see cref="BorderWidth" /> is not <see langword="null" />.
    /// </summary>
    public AllyariaColorValue BorderColor => _borderColor ?? BackgroundColor;

    /// <summary>Gets the border radius token, or <see langword="null" /> when not set.</summary>
    public AllyariaStringValue? BorderRadius => _borderRadius;

    /// <summary>Gets the border style token (e.g., <c>solid</c>), defaulting to <c>solid</c> when unset.</summary>
    public AllyariaStringValue BorderStyle => _borderStyle ?? new AllyariaStringValue("solid");

    /// <summary>
    /// Gets the effective border width declaration as a CSS token (e.g., <c>1px</c>), or <see langword="null" /> when no
    /// border should be rendered.
    /// </summary>
    public AllyariaNumberValue? BorderWidth
        => _borderWidth > 0
            ? new AllyariaNumberValue($"{_borderWidth}px")
            : null;

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

            // Ensure the explicit color meets contrast over the effective background.
            var result = ColorHelper.EnsureMinimumContrast(_foregroundColor, BackgroundColor, 4.5);

            return result.ForegroundColor;
        }
    }

    /// <summary>
    /// Produces a new palette by applying the specified raw overrides, falling back to this instance’s raw inputs where not
    /// provided. Derived/effective precedence is not re-applied during parameter merging.
    /// </summary>
    /// <param name="backgroundColor">Optional new base background color.</param>
    /// <param name="foregroundColor">Optional new base foreground color.</param>
    /// <param name="backgroundImage">
    /// Optional new background image source (unwrapped; image precedence is applied by <see cref="BackgroundImage" />).
    /// </param>
    /// <param name="backgroundImageStretch">Optional override for background image stretching.</param>
    /// <param name="borderWidth">Optional new border width (CSS pixels); values &lt; 0 are treated as <see langword="null" />.</param>
    /// <param name="borderColor">Optional new base border color.</param>
    /// <param name="borderStyle">Optional new border style token.</param>
    /// <param name="borderRadius">Optional new border radius token.</param>
    /// <returns>A new <see cref="AllyariaPalette" /> with the provided overrides applied.</returns>
    public AllyariaPalette Cascade(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaImageValue? backgroundImage = null,
        bool? backgroundImageStretch = null,
        int? borderWidth = null,
        AllyariaColorValue? borderColor = null,
        AllyariaStringValue? borderStyle = null,
        AllyariaStringValue? borderRadius = null)
    {
        var sanitizedBorderWidth = borderWidth is < 0
            ? null
            : borderWidth;

        var newBackgroundColor = backgroundColor ?? _backgroundColor;
        var newBackgroundImage = backgroundImage ?? _backgroundImage;
        var newBackgroundImageStretch = backgroundImageStretch ?? _backgroundImageStretch;
        var newBorderColor = borderColor ?? _borderColor;
        var newBorderStyle = borderStyle ?? _borderStyle;
        var newBorderRadius = borderRadius ?? _borderRadius;
        var newBorderWidth = sanitizedBorderWidth ?? _borderWidth;
        var newForegroundColor = foregroundColor ?? _foregroundColor;

        return new AllyariaPalette(
            newBackgroundColor,
            newForegroundColor,
            newBackgroundImage,
            newBackgroundImageStretch,
            newBorderWidth,
            newBorderColor,
            newBorderStyle,
            newBorderRadius
        );
    }

    /// <summary>
    /// Builds a string of inline CSS declarations (e.g., <c>color:#fff;background-color:#000;</c>) that applies this palette
    /// according to precedence (background image &gt; background color; explicit overrides &gt; defaults; border rendered only
    /// when width &gt; 0).
    /// </summary>
    /// <returns>A CSS declaration string suitable for an inline <c>style</c> attribute.</returns>
    public string ToCss()
    {
        var builder = new StringBuilder();
        builder.Append(BackgroundColor.ToCss("background-color"));
        builder.Append(ForegroundColor.ToCss("color"));

        if (BackgroundImage is not null)
        {
            builder.Append(BackgroundImage.ToCssBackground(BackgroundColor, _backgroundImageStretch));
        }

        if (BorderWidth is not null)
        {
            builder.Append(BorderColor.ToCss("border-color"));
            builder.Append(BorderStyle.ToCss("border-style"));
            builder.Append(BorderWidth.ToCss("border-width"));
        }

        if (BorderRadius is not null)
        {
            builder.Append(BorderRadius.ToCss("border-radius"));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Builds CSS custom property declarations for theming. The optional <paramref name="prefix" /> is normalized by trimming
    /// whitespace/dashes, converting to lowercase, and replacing spaces with hyphens. If the normalized prefix is empty,
    /// variables are emitted with the default <c>--aa-</c> prefix; otherwise, the computed prefix is used (e.g.,
    /// <c>--mytheme-color</c>, <c>--mytheme-background-color</c>).
    /// </summary>
    /// <param name="prefix">
    /// Optional namespace for the CSS variables. May contain spaces or leading/trailing dashes, which are normalized before
    /// use.
    /// </param>
    /// <returns>
    /// A CSS declaration string defining variables for foreground/background and any active border/radius. When a background
    /// image is present, an image variable is emitted and background color variables may be omitted by the image helper.
    /// </returns>
    /// <remarks>
    /// Border and radius variables are included only when explicitly set, keeping the emitted CSS concise.
    /// </remarks>
    public string ToCssVars(string prefix = "")
    {
        var basePrefix = Regex.Replace(prefix, @"[\s-]+", "-").Trim('-').ToLowerInvariant();

        basePrefix = string.IsNullOrWhiteSpace(prefix)
            ? "--aa-"
            : $"--{basePrefix}-";

        var builder = new StringBuilder();
        builder.Append(ForegroundColor.ToCss($"{basePrefix}color"));
        builder.Append(ForegroundColor.ToCss($"{basePrefix}background-color"));

        if (BackgroundImage is not null)
        {
            builder.Append(BackgroundImage.ToCssVarsBackground(basePrefix, BackgroundColor, _backgroundImageStretch));
        }

        if (BorderWidth is not null)
        {
            builder.Append(BorderColor.ToCss($"{basePrefix}border-color"));
            builder.Append(BorderStyle.ToCss($"{basePrefix}border-style"));
            builder.Append(BorderWidth.ToCss($"{basePrefix}border-width"));
        }

        if (BorderRadius is not null)
        {
            builder.Append(BorderRadius.ToCss($"{prefix}border-radius"));
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
        var baseBg = BackgroundColor;

        var disabledBg = AllyariaColorValue.FromHsva(
            baseBg.H,
            Math.Max(0.0, baseBg.S - desaturateBy),
            ColorHelper.Blend(baseBg.V, 50.0, valueBlendTowardMid)
        );

        AllyariaColorValue? disabledBorder = null;

        if (BorderWidth is not null)
        {
            var baseBorder = BorderColor;

            disabledBorder = AllyariaColorValue.FromHsva(
                baseBorder.H,
                Math.Max(0.0, baseBorder.S - desaturateBy),
                ColorHelper.Blend(baseBorder.V, 50.0, valueBlendTowardMid)
            );
        }

        // Start from existing foreground (effective) and ensure relaxed contrast against the disabled background.
        var candidateFg = ForegroundColor;
        var disabledFg = ColorHelper.EnsureMinimumContrast(candidateFg, disabledBg, minimumContrast).ForegroundColor;

        return Cascade(
            disabledBg,
            disabledFg,
            borderColor: disabledBorder
        );
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
        var baseBg = BackgroundColor;

        // Direction: lighten for dark surfaces, darken for light surfaces.
        var direction = baseBg.V >= 50.0
            ? -1.0
            : +1.0;

        var hoverBg = AllyariaColorValue.FromHsva(baseBg.H, baseBg.S, baseBg.V + direction * backgroundDeltaV);

        AllyariaColorValue? hoverBorder = null;

        if (BorderWidth is not null) // only compute if a border is rendered
        {
            var baseBorder = BorderColor;

            hoverBorder = AllyariaColorValue.FromHsva(
                baseBorder.H, baseBorder.S, baseBorder.V + direction * borderDeltaV
            );
        }

        // Ensure readable foreground against the derived background while preserving hue where possible.
        var fgResolved = ColorHelper.EnsureMinimumContrast(ForegroundColor, hoverBg, minimumContrast).ForegroundColor;

        return Cascade(
            hoverBg,
            fgResolved,
            borderColor: hoverBorder
        );
    }
}
