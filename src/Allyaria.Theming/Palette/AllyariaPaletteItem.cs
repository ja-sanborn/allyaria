using System.Text;

namespace Allyaria.Theming.Palette;

/// <summary>
/// Represents a palette item consisting of background, border, and foreground colors, including hover variants and an
/// optional background image. Property getters enforce precedence and never return <c>null</c>.
/// </summary>
/// <remarks>
/// Precedence rules (summary):
/// <list type="number">
///     <item>
///         <description>If <see cref="BorderColor" /> backing value is unset, it falls back to the base background color.</description>
///     </item>
///     <item>
///         <description>If the base <see cref="_backgroundColor" /> is unset, it defaults to white.</description>
///     </item>
///     <item>
///         <description>
///         If <see cref="HasBorder" /> is <c>true</c>, the effective <see cref="BackgroundColor" /> is the hover variant
///         of the base background.
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
///         <see cref="AllyariaColor.HoverColor" />().
///         </description>
///     </item>
///     <item>
///         <description>
///         If <see cref="ForegroundHoverColor" /> is unset, it is <see cref="ForegroundColor" />.
///         <see cref="AllyariaColor.HoverColor" />().
///         </description>
///     </item>
///     <item>
///         <description>
///         If <see cref="BackgroundImage" /> is non-empty and <see cref="HasBackground" /> is <c>true</c>, an overlay is
///         applied via <c>linear-gradient(rgba(255,255,255,0.5),...)</c> in CSS.
///         </description>
///     </item>
/// </list>
/// </remarks>
public sealed class AllyariaPaletteItem
{
    /// <summary>
    /// Backing field for <see cref="BackgroundColor" />. May be <c>null</c> until set; when null, resolves to
    /// <see cref="White" />.
    /// </summary>
    private AllyariaColor? _backgroundColor;

    /// <summary>
    /// Backing field for <see cref="BackgroundHoverColor" />. May be <c>null</c>; when null, resolves to
    /// <see cref="BackgroundColor" />.<see cref="AllyariaColor.HoverColor()" />.
    /// </summary>
    private AllyariaColor? _backgroundHoverColor;

    /// <summary>Backing field for <see cref="BackgroundImage" />. May be <c>null</c> when no image is assigned.</summary>
    private string? _backgroundImage;

    /// <summary>
    /// Backing field for <see cref="BorderColor" />. May be <c>null</c>; when null, resolves to <see cref="BackgroundColor" />
    /// .
    /// </summary>
    private AllyariaColor? _borderColor;

    /// <summary>
    /// Backing field for <see cref="ForegroundColor" />. May be <c>null</c>; when null, resolves based on
    /// <see cref="BackgroundColor" />.<see cref="AllyariaColor.V" />.
    /// </summary>
    private AllyariaColor? _foregroundColor;

    /// <summary>
    /// Backing field for <see cref="ForegroundHoverColor" />. May be <c>null</c>; when null, resolves to
    /// <see cref="ForegroundColor" />.<see cref="AllyariaColor.HoverColor()" />.
    /// </summary>
    private AllyariaColor? _foregroundHoverColor;

    /// <summary>
    /// Shared black color instance for fallbacks (used when <see cref="ForegroundColor" /> requires black).
    /// </summary>
    private static readonly AllyariaColor Black = new("black");

    /// <summary>
    /// Shared white color instance for fallbacks (used when <see cref="_backgroundColor" /> is <c>null</c> or when
    /// <see cref="ForegroundColor" /> requires white).
    /// </summary>
    private static readonly AllyariaColor White = new("white");

    /// <summary>Initializes a new instance with defaults.</summary>
    public AllyariaPaletteItem() { }

    /// <summary>Initializes a new instance with a background color.</summary>
    /// <param name="backgroundColor">The base background color.</param>
    public AllyariaPaletteItem(AllyariaColor backgroundColor) => _backgroundColor = backgroundColor;

    /// <summary>Initializes a new instance with a background and foreground color.</summary>
    /// <param name="backgroundColor">The base background color.</param>
    /// <param name="foregroundColor">The foreground color.</param>
    public AllyariaPaletteItem(AllyariaColor backgroundColor, AllyariaColor foregroundColor)
    {
        _backgroundColor = backgroundColor;
        _foregroundColor = foregroundColor;
    }

    /// <summary>
    /// Gets or sets the <b>effective</b> background color. The setter sets the base background; the getter applies precedence:
    /// base → white if unset, then if <see cref="HasBorder" /> is true, returns the hover variant.
    /// </summary>
    public AllyariaColor BackgroundColor
    {
        get
        {
            var baseBg = _backgroundColor ?? White;

            return HasBorder
                ? baseBg.HoverColor()
                : baseBg;
        }

        set => _backgroundColor = value;
    }

    /// <summary>
    /// Gets or sets the background color used on hover. If unset, derives from <see cref="BackgroundColor" /> via
    /// <see cref="AllyariaColor.HoverColor" />().
    /// </summary>
    public AllyariaColor BackgroundHoverColor
    {
        get => _backgroundHoverColor ?? BackgroundColor.HoverColor();
        set => _backgroundHoverColor = value;
    }

    /// <summary>
    /// Gets or sets the optional background image URL (nullable). When set and <see cref="HasBackground" /> is <c>true</c>,
    /// CSS output applies a 50% white overlay over the image to improve legibility.
    /// </summary>
    public string? BackgroundImage
    {
        get => _backgroundImage;

        set => _backgroundImage = string.IsNullOrWhiteSpace(value)
            ? null
            : value;
    }

    /// <summary>
    /// Gets or sets the border color. If unset, falls back to the <b>base</b> background (pre-shift), per precedence list
    /// order.
    /// </summary>
    public AllyariaColor BorderColor
    {
        get
        {
            if (_borderColor.HasValue)
            {
                return _borderColor.Value;
            }

            // Border fallback uses the base background (before HasBorder shift).
            var baseBg = _backgroundColor ?? White;

            return baseBg;
        }

        set => _borderColor = value;
    }

    /// <summary>
    /// Gets or sets the foreground (text) color. If unset, computed for contrast against the <b>effective</b>
    /// <see cref="BackgroundColor" />: <c>V &lt; 50</c> → white; otherwise black.
    /// </summary>
    public AllyariaColor ForegroundColor
    {
        get
        {
            if (_foregroundColor.HasValue)
            {
                return _foregroundColor.Value;
            }

            var bg = BackgroundColor;

            return bg.V < 50.0
                ? White
                : Black;
        }

        set => _foregroundColor = value;
    }

    /// <summary>
    /// Gets or sets the foreground color used on hover. If unset, derives from <see cref="ForegroundColor" /> via
    /// <see cref="AllyariaColor.HoverColor" />().
    /// </summary>
    public AllyariaColor ForegroundHoverColor
    {
        get => _foregroundHoverColor ?? ForegroundColor.HoverColor();
        set => _foregroundHoverColor = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether a background (color/image) should be rendered. Defaults to <c>true</c>. When
    /// <c>false</c>, both <c>background-color</c> and <c>background-image</c> are omitted from the CSS output.
    /// </summary>
    public bool HasBackground { get; set; } = true;

    /// <summary>Gets or sets a value indicating whether a border should be rendered. Defaults to <c>false</c>.</summary>
    public bool HasBorder { get; set; }

    /// <summary>
    /// Builds inline CSS declarations for the current palette state. Includes:
    /// <list type="bullet">
    ///     <item>
    ///         <description><c>background-color</c> (when <see cref="HasBackground" /> is true)</description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///             <c>color</c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description><c>border-color</c> (when <see cref="HasBorder" /> is true)</description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         <c>background-image</c> with a 50% white overlay when <see cref="BackgroundImage" /> is set and
    ///         <see cref="HasBackground" /> is true
    ///         </description>
    ///     </item>
    /// </list>
    /// Uses <see cref="AllyariaColor.ToCss(string)" /> for color declarations.
    /// </summary>
    /// <returns>CSS declarations suitable for inclusion in a <c>style</c> attribute or block.</returns>
    public string ToCss()
    {
        var sb = new StringBuilder();

        // color (foreground) always emitted
        sb.Append(ForegroundColor.ToCss("color"));

        // background-color / image only when allowed
        if (HasBackground)
        {
            if (string.IsNullOrWhiteSpace(BackgroundImage))
            {
                sb.Append(BackgroundColor.ToCss("background-color"));
            }
            else
            {
                // Apply 50% white overlay over the image
                var img = BackgroundImage!.Replace("\"", "\\\"", StringComparison.Ordinal);

                sb.Append(
                    $"background-image:linear-gradient(rgba(255,255,255,0.5),rgba(255,255,255,0.5)),url(\"{img}\");"
                );
            }
        }

        // border if requested
        if (HasBorder)
        {
            sb.Append(BorderColor.ToCss("border-color"));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Builds a string of CSS custom property assignments (<c>--aa-*</c>) representing this palette’s effective background,
    /// foreground, border, and optional image values. These variables are intended for use in component-scoped CSS isolation,
    /// allowing normal and hover states to resolve through <c>var(--aa-…)</c> without requiring per-instance style blocks.
    /// </summary>
    /// <remarks>
    ///     <para>Output includes:</para>
    ///     <list type="bullet">
    ///         <item>
    ///             <description><c>--aa-bg</c>, <c>--aa-fg</c> – background/foreground colors</description>
    ///         </item>
    ///         <item>
    ///             <description><c>--aa-bg-hover</c>, <c>--aa-fg-hover</c> – hover colors</description>
    ///         </item>
    ///         <item>
    ///             <description><c>--aa-border</c> – border color reflects <see cref="HasBorder" /></description>
    ///         </item>
    ///         <item>
    ///             <description><c>--aa-bg-image</c> – URL when <see cref="BackgroundImage" /> is set; otherwise <c>none</c></description>
    ///         </item>
    ///     </list>
    ///     <para>
    ///     When <see cref="HasBackground" /> is <c>false</c>, <c>--aa-bg</c> resolves to transparent and <c>--aa-bg-image</c>
    ///     is <c>none</c>.
    ///     </para>
    /// </remarks>
    /// <returns>A semicolon-delimited string of CSS variable assignments suitable for inline <c>style</c>.</returns>
    public string ToCssVars()
    {
        var sb = new StringBuilder();

        // Colors (normal + hover)
        sb.Append(BackgroundColor.ToCss("--aa-bg"));
        sb.Append(ForegroundColor.ToCss("--aa-fg"));
        sb.Append(BackgroundHoverColor.ToCss("--aa-bg-hover"));
        sb.Append(ForegroundHoverColor.ToCss("--aa-fg-hover"));
        sb.Append(BorderColor.ToCss("--aa-border")).Append(' ');

        // Optional background image
        if (HasBackground && !string.IsNullOrWhiteSpace(BackgroundImage))
        {
            var img = BackgroundImage!.Replace("\"", "\\\"", StringComparison.Ordinal);
            sb.Append($"--aa-bg-image: url(\"{img}\");");
        }
        else
        {
            sb.Append("--aa-bg-image: none;");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Builds inline Hover CSS declarations for the current palette state. Includes:
    /// <list type="bullet">
    ///     <item>
    ///         <description><c>background-color</c> (when <see cref="HasBackground" /> is true)</description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///             <c>color</c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description><c>border-color</c> (when <see cref="HasBorder" /> is true)</description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         <c>background-image</c> with a 50% white overlay when <see cref="BackgroundImage" /> is set and
    ///         <see cref="HasBackground" /> is true
    ///         </description>
    ///     </item>
    /// </list>
    /// Uses <see cref="AllyariaColor.ToCss(string)" /> for color declarations.
    /// </summary>
    /// <returns>CSS declarations suitable for inclusion in a <c>style</c> attribute or block.</returns>
    public string ToHoverCss()
    {
        var sb = new StringBuilder();

        // color (foreground) always emitted
        sb.Append(ForegroundHoverColor.ToCss("color"));

        // background-color / image only when allowed
        if (HasBackground)
        {
            if (string.IsNullOrWhiteSpace(BackgroundImage))
            {
                sb.Append(BackgroundHoverColor.ToCss("background-color"));
            }
            else
            {
                // Apply 50% white overlay over the image
                var img = BackgroundImage!.Replace("\"", "\\\"", StringComparison.Ordinal);

                sb.Append(
                    $"background-image:linear-gradient(rgba(255,255,255,0.5),rgba(255,255,255,0.5)),url(\"{img}\");"
                );
            }
        }

        // border if requested
        if (HasBorder)
        {
            sb.Append(BorderColor.ToCss("border-color"));
        }

        return sb.ToString();
    }

    /// <summary>Renders the current palette state as a CSS declaration.</summary>
    /// <returns>CSS declarations suitable for inclusion in a <c>style</c> attribute or block.</returns>
    public override string ToString() => ToCss();
}
