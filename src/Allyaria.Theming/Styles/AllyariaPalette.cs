using Allyaria.Theming.Constants;
using Allyaria.Theming.Values;
using System.Text;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a theme palette consisting of background, border, and foreground colors, including hover variants and an
/// optional background image.
/// </summary>
/// <remarks>
/// Property getters enforce precedence and never return <c>null</c>. This type is immutable and therefore thread-safe.
/// Precedence rules:
/// <list type="number">
///     <item>
///         <description>
///         If <see cref="BorderColor" /> backing value is unset and <see cref="HasBorder" /> is <c>true</c>, it falls back
///         to the effective <see cref="BackgroundColor" />; if <see cref="HasBorder" /> is <c>false</c>, the border is
///         transparent.
///         </description>
///     </item>
///     <item>
///         <description>If the base background is unset, it defaults to white.</description>
///     </item>
///     <item>
///         <description>
///         If <see cref="HasBorder" /> is <c>true</c>, the effective <see cref="BackgroundColor" /> is the
///         <see cref="AllyariaColorValue.HoverColor()" /> of the base background.
///         </description>
///     </item>
///     <item>
///         <description>
///         If <see cref="ForegroundColor" /> backing value is unset, it is computed from the effective background value:
///         <c>V &lt; 50 → White</c>, else <c>Black</c>.
///         </description>
///     </item>
///     <item>
///         <description>
///         If <see cref="BackgroundHoverColor" /> is unset, it is <see cref="BackgroundColor" />.
///         <see cref="AllyariaColorValue.HoverColor()" />.
///         </description>
///     </item>
///     <item>
///         <description>
///         If <see cref="ForegroundHoverColor" /> is unset, it is <see cref="ForegroundColor" />.
///         <see cref="AllyariaColorValue.HoverColor()" />.
///         </description>
///     </item>
///     <item>
///         <description>
///         If <see cref="BackgroundImage" /> is non-empty and <see cref="HasBackground" /> is <c>true</c>, an overlay is
///         applied via <c>linear-gradient(rgba(255,255,255,0.5), ...)</c> and the image is rendered with
///         <c>center / cover no-repeat</c>.
///         </description>
///     </item>
/// </list>
/// </remarks>
public readonly struct AllyariaPalette
{
    /// <summary>The base background color as provided, before precedence is applied.</summary>
    private readonly AllyariaColorValue _backgroundColor;

    /// <summary>
    /// Optional explicit background hover color as provided by the caller; may be empty to signal defaulting.
    /// </summary>
    private readonly AllyariaColorValue? _backgroundHover;

    /// <summary>Normalized background image URL (lowercased, trimmed) or empty when not in use.</summary>
    private readonly string _backgroundImage;

    /// <summary>Optional explicit border color as provided by the caller; may be empty to signal defaulting.</summary>
    private readonly AllyariaColorValue? _borderColor;

    /// <summary>Optional explicit foreground color as provided by the caller; may be empty to signal defaulting.</summary>
    private readonly AllyariaColorValue? _foregroundColor;

    /// <summary>
    /// Optional explicit foreground hover color as provided by the caller; may be empty to signal defaulting.
    /// </summary>
    private readonly AllyariaColorValue? _foregroundHover;

    /// <summary>Indicates whether a background (color or image) should be applied.</summary>
    private readonly bool _hasBackground;

    /// <summary>Indicates whether a border should be rendered.</summary>
    private readonly bool _hasBorder;

    /// <summary>Initializes a new immutable <see cref="AllyariaPalette" />.</summary>
    /// <param name="backgroundColor">Optional base background color; defaults to white when not provided.</param>
    /// <param name="foregroundColor">
    /// Optional explicit foreground color; when not provided, it is computed from <see cref="BackgroundColor" /> lightness.
    /// </param>
    /// <param name="backgroundHoverColor">
    /// Optional explicit background hover color; when not provided, it is <see cref="BackgroundColor" />.
    /// <see cref="AllyariaColorValue.HoverColor()" />.
    /// </param>
    /// <param name="foregroundHoverColor">
    /// Optional explicit foreground hover color; when not provided, it is <see cref="ForegroundColor" />.
    /// <see cref="AllyariaColorValue.HoverColor()" />.
    /// </param>
    /// <param name="borderColor">
    /// Optional explicit border color; when not provided and <see cref="HasBorder" /> is true, it falls back to
    /// <see cref="BackgroundColor" />.
    /// </param>
    /// <param name="backgroundImage">
    /// Optional background image URL. Used only when non-empty and <see cref="HasBackground" />
    /// is true.
    /// </param>
    /// <param name="hasBackground">Whether a background (color or image) should be applied.</param>
    /// <param name="hasBorder">Whether a border should be applied.</param>
    public AllyariaPalette(AllyariaColorValue? backgroundColor = null,
        AllyariaColorValue? foregroundColor = null,
        AllyariaColorValue? backgroundHoverColor = null,
        AllyariaColorValue? foregroundHoverColor = null,
        AllyariaColorValue? borderColor = null,
        string backgroundImage = "",
        bool hasBackground = true,
        bool hasBorder = true)
    {
        _hasBackground = hasBackground;
        _hasBorder = hasBorder;

        _backgroundImage = _hasBackground && !string.IsNullOrWhiteSpace(backgroundImage)
            ? backgroundImage.Trim().ToLowerInvariant()
            : string.Empty;

        _backgroundColor = backgroundColor ?? Colors.White;
        _foregroundColor = foregroundColor;
        _backgroundHover = backgroundHoverColor;
        _foregroundHover = foregroundHoverColor;
        _borderColor = borderColor;
    }

    /// <summary>Gets the effective background color after precedence is applied.</summary>
    public AllyariaColorValue BackgroundColor
        => HasBorder
            ? _backgroundColor.HoverColor()
            : _backgroundColor;

    /// <summary>Gets the effective background hover color.</summary>
    public AllyariaColorValue BackgroundHoverColor => _backgroundHover ?? BackgroundColor.HoverColor();

    /// <summary>Gets the normalized background image URL (or empty when not in use).</summary>
    public string BackgroundImage => _backgroundImage;

    /// <summary>
    /// Builds the effective background-image CSS value including a 50% white overlay for readability and standard positioning
    /// (<c>center</c>), repeat (<c>no-repeat</c>), and sizing (<c>cover</c>).
    /// </summary>
    /// <remarks>
    /// When <see cref="BackgroundImage" /> is empty, the returned value still represents a syntactically valid
    /// <c>linear-gradient(...), url("")</c>; callers should check emptiness before use.
    /// </remarks>
    private string BackgroundImageCss
        => $"linear-gradient(rgba(255,255,255,0.5),rgba(255,255,255,0.5)),url(\"{_backgroundImage}\")";

    /// <summary>
    /// Gets the effective border color. If <see cref="HasBorder" /> is <c>false</c>, the color is transparent.
    /// </summary>
    public AllyariaColorValue BorderColor
        => HasBorder
            ? _borderColor ?? BackgroundColor
            : Colors.Transparent;

    /// <summary>
    /// Gets the effective foreground (text) color. Defaults based on <see cref="BackgroundColor" /> value (V component).
    /// </summary>
    public AllyariaColorValue ForegroundColor
        => _foregroundColor ?? (BackgroundColor.V < 50.0
            ? Colors.White
            : Colors.Black);

    /// <summary>Gets the effective foreground hover color.</summary>
    public AllyariaColorValue ForegroundHoverColor => _foregroundHover ?? ForegroundColor.HoverColor();

    /// <summary>Gets whether a background (color or image) is intended to be rendered.</summary>
    public bool HasBackground => _hasBackground;

    /// <summary>Gets whether a border is intended to be rendered.</summary>
    public bool HasBorder => _hasBorder;

    /// <summary>
    /// Builds a string of inline CSS declarations (e.g., <c>color: #fff; background-color: #000;</c>) that applies the current
    /// palette with the documented precedence.
    /// </summary>
    /// <returns>A CSS declaration string suitable for inline <c>style</c> attributes.</returns>
    public string ToCss()
    {
        var builder = new StringBuilder();

        builder.Append(ForegroundColor.ToCss("color"));

        if (HasBackground)
        {
            if (!string.IsNullOrWhiteSpace(BackgroundImage))
            {
                builder.Append($"background-image:{BackgroundImageCss};");
                builder.Append("background-position:center;");
                builder.Append("background-repeat:no-repeat;");
                builder.Append("background-size:cover;");
            }
            else
            {
                builder.Append(BackgroundColor.ToCss("background-color"));
            }
        }

        if (HasBorder)
        {
            builder.Append(BorderColor.ToCss("border-color"));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Builds a string of CSS custom property declarations for theming (e.g., <c>--aa-fg</c>, <c>--aa-bg</c>).
    /// </summary>
    /// <returns>A CSS declaration string defining theme variables.</returns>
    public string ToCssVars()
    {
        var builder = new StringBuilder();

        builder.Append(ForegroundColor.ToCss("--aa-fg"));
        builder.Append(ForegroundHoverColor.ToCss("--aa-fg-hover"));

        if (HasBackground && !string.IsNullOrWhiteSpace(BackgroundImage))
        {
            builder.Append($"--aa-bg-image:{BackgroundImageCss};");
        }
        else
        {
            builder.Append(BackgroundColor.ToCss("--aa-bg"));
            builder.Append(BackgroundHoverColor.ToCss("--aa-bg-hover"));
        }

        if (HasBorder)
        {
            builder.Append(BorderColor.ToCss("--aa-border"));
        }

        return builder.ToString();
    }

    /// <summary>Returns <see cref="ToCss" /> for convenience.</summary>
    /// <returns>The same value as <see cref="ToCss" />.</returns>
    public override string ToString() => ToCss();
}
