using Allyaria.Theming.Constants;
using Allyaria.Theming.Values;
using System.Text;

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

    /// <summary>
    /// Optional explicit background hover color as provided by the caller; may be <see langword="null" /> to signal defaulting
    /// to <see cref="BackgroundColor" />.<see cref="AllyariaColorValue.HoverColor()" />.
    /// </summary>
    private readonly AllyariaColorValue? _backgroundHover;

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

    /// <summary>
    /// Optional explicit foreground hover color as provided by the caller; may be <see langword="null" /> to signal defaulting
    /// to <see cref="ForegroundColor" />.<see cref="AllyariaColorValue.HoverColor()" />.
    /// </summary>
    private readonly AllyariaColorValue? _foregroundHover;

    /// <summary>Initializes a new immutable <see cref="AllyariaPalette" />.</summary>
    /// <param name="backgroundColor">
    /// Optional base background color; defaults to <see cref="Colors.White" /> when not
    /// provided.
    /// </param>
    /// <param name="foregroundColor">
    /// Optional explicit foreground color; when not provided, it is computed from <see cref="BackgroundColor" /> lightness for
    /// contrast.
    /// </param>
    /// <param name="backgroundHoverColor">
    /// Optional explicit background hover color; when not provided, defaults to <see cref="BackgroundColor" />.
    /// <see cref="AllyariaColorValue.HoverColor()" />.
    /// </param>
    /// <param name="foregroundHoverColor">
    /// Optional explicit foreground hover color; when not provided, defaults to <see cref="ForegroundColor" />.
    /// <see cref="AllyariaColorValue.HoverColor()" />.
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
        AllyariaColorValue? backgroundHoverColor = null,
        AllyariaColorValue? foregroundHoverColor = null,
        string backgroundImage = "",
        int borderWidth = 0,
        AllyariaColorValue? borderColor = null,
        AllyariaStringValue? borderStyle = null,
        AllyariaStringValue? borderRadius = null)
    {
        _backgroundImage = backgroundImage;
        _backgroundColor = backgroundColor;
        _backgroundHover = backgroundHoverColor;
        _foregroundColor = foregroundColor;
        _foregroundHover = foregroundHoverColor;

        _borderColor = borderColor;
        _borderRadius = borderRadius;
        _borderStyle = borderStyle;
        _borderWidth = borderWidth;
    }

    /// <summary>
    /// Gets the effective background color after precedence is applied. When a border is present, the background is slightly
    /// adjusted via <see cref="AllyariaColorValue.HoverColor()" /> for contrast.
    /// </summary>
    private AllyariaColorValue BackgroundColor
        => _backgroundColor is null
            ? Colors.White
            : HasBorder
                ? _backgroundColor.HoverColor()
                : _backgroundColor;

    /// <summary>
    /// Gets the effective background hover color. Defaults to <see cref="BackgroundColor" />.
    /// <see cref="AllyariaColorValue.HoverColor()" /> when not explicitly provided.
    /// </summary>
    private AllyariaColorValue BackgroundHoverColor => _backgroundHover ?? BackgroundColor.HoverColor();

    /// <summary>
    /// Gets the effective background image declaration value, or <see langword="null" /> when no image is set.
    /// </summary>
    private AllyariaStringValue? BackgroundImage
        => string.IsNullOrWhiteSpace(_backgroundImage)
            ? null
            : new AllyariaStringValue(
                $"linear-gradient(rgba(255,255,255,0.5),rgba(255,255,255,0.5)),url(\"{_backgroundImage.Trim().ToLowerInvariant()}\")"
            );

    /// <summary>
    /// Gets the effective border color. If <see cref="HasBorder" /> is <see langword="false" />, the color is
    /// <see cref="Colors.Transparent" />.
    /// </summary>
    private AllyariaColorValue BorderColor
        => HasBorder
            ? _borderColor ?? BackgroundColor
            : Colors.Transparent;

    /// <summary>Gets the effective border radius declaration value, or <see langword="null" /> when not set.</summary>
    private AllyariaStringValue? BorderRadius => _borderRadius;

    /// <summary>Gets the border style token (e.g., <c>solid</c>).</summary>
    private AllyariaStringValue BorderStyle => _borderStyle ?? new AllyariaStringValue("solid");

    /// <summary>
    /// Gets the effective border width declaration value, or <see langword="null" /> when no border should be rendered.
    /// </summary>
    private AllyariaStringValue? BorderWidth
        => _borderWidth > 0
            ? new AllyariaStringValue($"{_borderWidth}px")
            : null;

    /// <summary>
    /// Gets the effective foreground (text) color. When not explicitly provided, this is computed from
    /// <see cref="BackgroundColor" /> value/lightness for accessible contrast (dark backgrounds → white; light → black).
    /// </summary>
    private AllyariaColorValue ForegroundColor
        => _foregroundColor ?? (BackgroundColor.V < 50.0
            ? Colors.White
            : Colors.Black);

    /// <summary>
    /// Gets the effective foreground hover color. Defaults to <see cref="ForegroundColor" />.
    /// <see cref="AllyariaColorValue.HoverColor()" /> when not explicitly provided.
    /// </summary>
    private AllyariaColorValue ForegroundHoverColor => _foregroundHover ?? ForegroundColor.HoverColor();

    /// <summary>
    /// Gets a value indicating whether a background image is set and should take precedence over background color.
    /// </summary>
    private bool HasBackground => BackgroundImage is not null;

    /// <summary>Gets a value indicating whether a border should be rendered (width &gt; 0).</summary>
    private bool HasBorder => BorderWidth is not null;

    /// <summary>Gets a value indicating whether a border radius should be emitted.</summary>
    private bool HasRadius => BorderRadius is not null;

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
    /// Builds a string of inline CSS declarations representing the <em>hover</em> state for this palette. The same precedence
    /// rules apply: background image (if any) takes priority over background color; otherwise the hover color variant is used.
    /// Border declarations mirror the non-hover state.
    /// </summary>
    /// <returns>A CSS declaration string for hover state styling (e.g., on <c>:hover</c>).</returns>
    public string ToCssHover()
    {
        var builder = new StringBuilder();
        builder.Append(ForegroundHoverColor.ToCss("color"));

        if (HasBackground)
        {
            builder.Append(BackgroundImage!.ToCss("background-image"));
            builder.Append("background-position:center;");
            builder.Append("background-repeat:no-repeat;");
            builder.Append("background-size:cover;");
        }
        else
        {
            builder.Append(BackgroundHoverColor.ToCss("background-color"));
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
    /// Builds a string of CSS custom property declarations for theming (e.g., <c>--aa-fg</c>, <c>--aa-bg</c>). When a
    /// background image is present, an <c>--aa-bg-image</c> variable is emitted and color variables are constrained to
    /// foregrounds and borders; otherwise, background color variables are emitted.
    /// </summary>
    /// <returns>A CSS declaration string defining theme variables that can be consumed by component CSS.</returns>
    public string ToCssVars()
    {
        var builder = new StringBuilder();
        builder.Append(ForegroundColor.ToCss("--aa-fg"));
        builder.Append(ForegroundHoverColor.ToCss("--aa-fg-hover"));

        if (HasBackground)
        {
            builder.Append(BackgroundImage!.ToCss("--aa-bg-image"));
        }
        else
        {
            builder.Append(BackgroundColor.ToCss("--aa-bg"));
            builder.Append(BackgroundHoverColor.ToCss("--aa-bg-hover"));
        }

        if (HasBorder)
        {
            builder.Append(BorderColor.ToCss("--aa-border-color"));
            builder.Append(BorderStyle.ToCss("--aa-border-style"));
            builder.Append(BorderWidth!.ToCss("--aa-border-width"));
        }

        if (HasRadius)
        {
            builder.Append(BorderRadius!.ToCss("--aa-border-radius"));
        }

        return builder.ToString();
    }
}
