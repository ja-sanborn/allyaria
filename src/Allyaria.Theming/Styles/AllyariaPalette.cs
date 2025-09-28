using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Values;
using System.Text;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents an immutable, strongly typed palette used by the Allyaria theme engine to compute effective foreground,
/// background, and border styles (including hover variants) with the documented precedence rules.
/// </summary>
/// <remarks>
/// This type is designed for inline style generation and CSS custom property emission. It follows Allyaria theming
/// precedence—background images override region colors, explicit overrides beat defaults, and borders are opt-in. See
/// <see cref="ToCss" /> and <see cref="ToCssVars" />.
/// </remarks>
public readonly record struct AllyariaPalette
{
    /// <summary>The base background color as provided, before any precedence (e.g., border presence) is applied.</summary>
    private readonly AllyariaColorValue? _backgroundColor;

    /// <summary>Optional background image or <see langword="null" /> when no image was provided.</summary>
    private readonly string? _backgroundImage;

    /// <summary>
    /// Optional explicit border color as provided by the caller; may be <see langword="null" /> to signal defaulting to
    /// <see cref="BackgroundColor" /> when a border is present.
    /// </summary>
    private readonly AllyariaColorValue? _borderColor;

    /// <summary>Optional border radius value (e.g., <c>4px</c>) or <see langword="null" /> to omit the declaration.</summary>
    private readonly AllyariaStringValue? _borderRadius;

    /// <summary>Border style token (e.g., <c>solid</c>). Defaults to <c>solid</c> when not supplied.</summary>
    private readonly AllyariaStringValue? _borderStyle;

    /// <summary>
    /// Optional border width (e.g., <c>1px</c>) or <see langword="null" /> when no border should be rendered.
    /// </summary>
    private readonly int? _borderWidth;

    /// <summary>
    /// Optional explicit foreground color as provided by the caller; may be <see langword="null" /> to signal contrast-based
    /// defaulting from <see cref="BackgroundColor" />.
    /// </summary>
    private readonly AllyariaColorValue? _foregroundColor;

    /// <summary>Initializes a new immutable <see cref="AllyariaPalette" />.</summary>
    /// <param name="backgroundColor">
    /// Optional base background color; defaults to <see cref="Colors.White" /> when not
    /// provided.
    /// </param>
    /// <param name="foregroundColor">
    /// Optional explicit foreground color; when not provided, it is computed from <see cref="BackgroundColor" /> lightness for
    /// contrast.
    /// </param>
    /// <param name="backgroundImage">
    /// An optional background image URL. When non-empty, this is transformed into a composite
    /// <c>linear-gradient(...), url("...")</c> value to ensure readability. Whitespace is trimmed and the URL is lower-cased
    /// for stability. When empty or whitespace, no image is used.
    /// </param>
    /// <param name="borderWidth">
    /// Border width in CSS pixels. Values &lt;= 0 omit the border entirely; values &gt; 0 render as <c>&lt;width&gt;px</c>.
    /// </param>
    /// <param name="borderColor">
    /// Optional explicit border color. When not provided but a border is present, the border color defaults to
    /// <see cref="BackgroundColor" /> to maintain visual cohesion.
    /// </param>
    /// <param name="borderStyle">
    /// Optional border style token (e.g., <c>solid</c>, <c>dashed</c>). Defaults to <c>solid</c> when not supplied.
    /// </param>
    /// <param name="borderRadius">
    /// Optional border radius (e.g., <c>4px</c>). When provided and a border is present, a corresponding <c>border-radius</c>
    /// declaration is emitted.
    /// </param>
    public AllyariaPalette(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        string? backgroundImage = "",
        int? borderWidth = 0,
        AllyariaColorValue? borderColor = null,
        AllyariaStringValue? borderStyle = null,
        AllyariaStringValue? borderRadius = null)
    {
        _backgroundColor = backgroundColor;
        _backgroundImage = backgroundImage;
        _borderColor = borderColor;
        _borderRadius = borderRadius;
        _borderStyle = borderStyle;
        _borderWidth = borderWidth;
        _foregroundColor = foregroundColor;
    }

    /// <summary>Gets the effective background color after precedence is applied.</summary>
    public AllyariaColorValue BackgroundColor => _backgroundColor ?? Colors.White;

    /// <summary>
    /// Gets the effective background image declaration value, or <see langword="null" /> when no image is set.
    /// </summary>
    public AllyariaStringValue? BackgroundImage
        => string.IsNullOrWhiteSpace(_backgroundImage)
            ? null
            : new AllyariaStringValue(
                $"linear-gradient(rgba(255,255,255,0.5),rgba(255,255,255,0.5)),url(\"{_backgroundImage.Trim().ToLowerInvariant()}\")"
            );

    /// <summary>
    /// Gets the effective border color. If <see cref="HasBorder" /> is <see langword="false" />, the color is
    /// <see cref="Colors.Transparent" />.
    /// </summary>
    public AllyariaColorValue BorderColor => _borderColor ?? BackgroundColor;

    /// <summary>Gets the effective border radius declaration value, or <see langword="null" /> when not set.</summary>
    public AllyariaStringValue? BorderRadius => _borderRadius;

    /// <summary>Gets the border style token (e.g., <c>solid</c>).</summary>
    public AllyariaStringValue BorderStyle => _borderStyle ?? new AllyariaStringValue("solid");

    /// <summary>
    /// Gets the effective border width declaration value, or <see langword="null" /> when no border should be rendered.
    /// </summary>
    public AllyariaStringValue? BorderWidth
        => _borderWidth > 0
            ? new AllyariaStringValue($"{_borderWidth}px")
            : null;

    /// <summary>
    /// Gets the resolved foreground color to render against <see cref="BackgroundColor" />:
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If no explicit foreground is set (<c>_foregroundColor</c> is <see langword="null" />), the getter chooses
    ///         whichever of <see cref="Colors.White" /> or <see cref="Colors.Black" /> produces the higher WCAG contrast ratio
    ///         against <see cref="BackgroundColor" />.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If an explicit foreground is provided, the getter ensures it meets at least the minimum accessibility contrast
    ///         ratio (default 4.5:1 for normal text) using <see cref="ColorHelper" />; it will preserve the original hue if
    ///         possible and, if needed, adjust lightness or mix toward black/white to reach the target.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// This property uses real WCAG contrast calculations rather than brightness heuristics (e.g. V or H from HSV). It ensures
    /// text remains readable and accessible on the current background color while honoring any explicitly provided foreground
    /// when possible.
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

            // Ensure the explicit color meets contrast over the *effective* BackgroundColor
            var result = ColorHelper.EnsureMinimumContrast(_foregroundColor, BackgroundColor, 4.5);

            return result.ForegroundColor;
        }
    }

    /// <summary>
    /// Gets a value indicating whether a background image is set and should take precedence over background color.
    /// </summary>
    private bool HasBackground => BackgroundImage is not null;

    /// <summary>Gets a value indicating whether a border should be rendered (width &gt; 0).</summary>
    private bool HasBorder => BorderWidth is not null;

    /// <summary>Gets a value indicating whether a border radius should be emitted.</summary>
    private bool HasRadius => BorderRadius is not null;

    /// <summary>
    /// Cascades the palette by applying overrides, falling back to the original base values of this instance when not provided
    /// (no reapplication of effective/derived precedence).
    /// </summary>
    /// <param name="backgroundColor">Optional new background color (base).</param>
    /// <param name="foregroundColor">Optional new foreground color (base).</param>
    /// <param name="backgroundImage">
    /// Optional new background image URL (unwrapped; overlaying is handled by <see cref="BackgroundImage" />).
    /// </param>
    /// <param name="borderWidth">
    /// Optional new border width in CSS pixels; values &lt; 0 are treated as <c>null</c> (no
    /// border).
    /// </param>
    /// <param name="borderColor">Optional new border color (base).</param>
    /// <param name="borderStyle">Optional new border style token.</param>
    /// <param name="borderRadius">Optional new border radius token.</param>
    /// <returns>
    /// A new <see cref="AllyariaPalette" /> with the specified overrides applied atop this instance’s base values.
    /// </returns>
    public AllyariaPalette Cascade(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        string? backgroundImage = null,
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
        var newBorderColor = borderColor ?? _borderColor;
        var newBorderStyle = borderStyle ?? _borderStyle;
        var newBorderRadius = borderRadius ?? _borderRadius;
        var newBorderWidth = sanitizedBorderWidth ?? _borderWidth;
        var newForegroundColor = foregroundColor ?? _foregroundColor;

        return new AllyariaPalette(
            newBackgroundColor,
            newForegroundColor,
            newBackgroundImage,
            newBorderWidth,
            newBorderColor,
            newBorderStyle,
            newBorderRadius
        );
    }

    /// <summary>
    /// Builds a string of inline CSS declarations (e.g., <c>color: #fff; background-color: #000;</c>) that applies the current
    /// palette with the documented precedence (background image &gt; background color; explicit overrides &gt; defaults;
    /// border rendered only when width &gt; 0).
    /// </summary>
    /// <returns>A CSS declaration string suitable for inline <c>style</c> attributes.</returns>
    public string ToCss()
    {
        var builder = new StringBuilder();
        builder.Append(ForegroundColor.ToCss("color"));

        if (HasBackground)
        {
            builder.Append(BackgroundImage!.ToCss("background-image"));
            builder.Append("background-position:center;");
            builder.Append("background-repeat:no-repeat;");
            builder.Append("background-size:cover;");
        }
        else
        {
            builder.Append(BackgroundColor.ToCss("background-color"));
        }

        if (HasBorder)
        {
            builder.Append(BorderColor.ToCss("border-color"));
            builder.Append(BorderStyle.ToCss("border-style"));
            builder.Append(BorderWidth!.ToCss("border-width"));
        }

        if (HasRadius)
        {
            builder.Append(BorderRadius!.ToCss("border-radius"));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Builds a string of CSS custom property declarations for theming. The method normalizes the optional
    /// <paramref name="prefix" /> by trimming whitespace and dashes, converting to lowercase, and replacing spaces with
    /// hyphens. If no usable prefix remains, variables are emitted with the default <c>--aa-</c> prefix; otherwise, the
    /// computed prefix is applied (e.g., <c>--mytheme-color</c>, <c>--mytheme-background-color</c>).
    /// </summary>
    /// <param name="prefix">
    /// An optional string used to namespace the CSS variables. May contain spaces or leading/trailing dashes, which are
    /// normalized before use. If empty or whitespace, defaults to <c>--aa-</c>.
    /// </param>
    /// <returns>
    /// A CSS declaration string defining theme variables (foreground, background, border, and radius) that can be consumed by
    /// component CSS. If a background image is present, a <c>--{prefix}-background-image</c> variable is emitted and
    /// background color variables are omitted.
    /// </returns>
    /// <remarks>
    /// Border and radius variables are included only when explicitly set. This ensures that only relevant theming properties
    /// are emitted, keeping CSS concise.
    /// </remarks>
    public string ToCssVars(string prefix = "")
    {
        var basePrefix = Regex.Replace(prefix, @"[\s-]+", "-").Trim('-').ToLowerInvariant();

        basePrefix = string.IsNullOrWhiteSpace(prefix)
            ? "--aa-"
            : $"--{basePrefix}-";

        var builder = new StringBuilder();
        builder.Append(ForegroundColor.ToCss($"{basePrefix}color"));

        builder.Append(
            HasBackground
                ? BackgroundImage!.ToCss($"{basePrefix}background-image")
                : BackgroundColor.ToCss($"{basePrefix}background-color")
        );

        if (HasBorder)
        {
            builder.Append(BorderColor.ToCss($"{basePrefix}border-color"));
            builder.Append(BorderStyle.ToCss($"{basePrefix}border-style"));
            builder.Append(BorderWidth!.ToCss($"{basePrefix}border-width"));
        }

        if (HasRadius)
        {
            builder.Append(BorderRadius!.ToCss($"{prefix}border-radius"));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Produces a derived palette for the <c>[disabled]</c> state by desaturating the background and gently compressing its
    /// Value (V) toward the mid range to reduce emphasis. The foreground is then contrast-corrected to a relaxed minimum
    /// suitable for UI affordances (3.0:1).
    /// </summary>
    /// <param name="desaturateBy">
    /// Percentage points to subtract from the background Saturation (S). Default is <c>60</c>, yielding a muted surface.
    /// </param>
    /// <param name="valueBlendTowardMid">
    /// Blend factor in [0..1] that moves background Value (V) toward mid (50). Default is <c>0.15</c> (15% toward mid).
    /// </param>
    /// <param name="minimumContrast">
    /// Minimum required contrast ratio for disabled foreground over the derived background; defaults to <c>3.0</c>.
    /// </param>
    /// <returns>A new <see cref="AllyariaPalette" /> suitable for the disabled state.</returns>
    /// <remarks>
    /// *Hue is preserved* for the background; only S/V are adjusted. If a border is present, its hue is preserved and it is
    /// desaturated and value-compressed in tandem with the background to avoid visual dominance. Background images are left
    /// unchanged (image precedence still applies).
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

        // Start from existing foreground (effective), then ensure a relaxed contrast target against the disabled background.
        var candidateFg = ForegroundColor;
        var disabledFg = ColorHelper.EnsureMinimumContrast(candidateFg, disabledBg, minimumContrast).ForegroundColor;

        return Cascade(
            disabledBg,
            disabledFg,
            borderColor: disabledBorder
        );
    }

    /// <summary>
    /// Produces a derived palette intended for the <c>:hover</c> state by nudging the background (and border, if present)
    /// along the HSV Value rail to increase perceived affordance while preserving hue. The foreground is then
    /// contrast-corrected against the new background to meet WCAG AA for body text (4.5:1).
    /// </summary>
    /// <param name="backgroundDeltaV">
    /// The absolute Value (V) change in percentage points to apply to <see cref="BackgroundColor" />. On light backgrounds (V
    /// ≥ 50) the value is decreased; on dark backgrounds it is increased. Default is <c>6</c>.
    /// </param>
    /// <param name="borderDeltaV">
    /// The absolute Value (V) change in percentage points to apply to <see cref="BorderColor" /> when a border is present.
    /// Mirrors the direction used for the background. Default is <c>8</c>.
    /// </param>
    /// <param name="minimumContrast">
    /// Minimum required contrast ratio for the foreground over the derived background; defaults to <c>4.5</c> (WCAG AA).
    /// </param>
    /// <returns>A new <see cref="AllyariaPalette" /> suitable for the hover state.</returns>
    /// <remarks>
    /// This method does not alter the background image. If a background image is active, it remains in effect (image
    /// precedence still applies), but the derived background color—while not painted—still participates in computing a
    /// readable foreground as a fallback.
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
        var fgResolved = ColorHelper.EnsureMinimumContrast(ForegroundColor, hoverBg, minimumContrast)
            .ForegroundColor;

        return Cascade(
            hoverBg,
            fgResolved,
            borderColor: hoverBorder
        );
    }
}
