using Allyaria.Theming.Values;
using System.Text;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Strongly-typed border definition for Allyaria theming. Supports per-side <c>width</c> and <c>style</c>
/// (top/right/bottom/left) and per-corner <c>radius</c> (top-left/top-right/bottom-right/bottom-left). Widths and radii
/// are expressed as <see cref="AllyariaNumberValue" /> (e.g., <c>1px</c>, <c>.125rem</c>, <c>0</c>, <c>50%</c>), and
/// styles as <see cref="AllyariaStringValue" /> (e.g., <c>solid</c>, <c>dashed</c>, <c>none</c>). When a member is
/// <see langword="null" />, it is considered unset and omitted from CSS emission via <see cref="ToCss" /> and
/// <see cref="ToCssVars(string)" />. Non-destructive overrides are supported via <see cref="Cascade" />.
/// </summary>
/// <remarks>
/// This type intentionally omits border <c>color</c>. Colors are provided by the palette/theme layer. Pattern and
/// implementation mirror <see cref="AllyariaSpacing" /> for consistency across theming structs.
/// :contentReference[oaicite:0]{index=0}
/// </remarks>
public readonly record struct AllyariaBorders
{
    /// <summary>
    /// Initializes a new <see cref="AllyariaBorders" /> with optional per-side widths/styles and per-corner radii. Pass
    /// <see langword="null" /> for any member to leave it unset.
    /// </summary>
    /// <param name="topWidth">Optional border-top-width.</param>
    /// <param name="rightWidth">Optional border-right-width.</param>
    /// <param name="bottomWidth">Optional border-bottom-width.</param>
    /// <param name="leftWidth">Optional border-left-width.</param>
    /// <param name="topStyle">Optional border-top-style (e.g., "solid").</param>
    /// <param name="rightStyle">Optional border-right-style.</param>
    /// <param name="bottomStyle">Optional border-bottom-style.</param>
    /// <param name="leftStyle">Optional border-left-style.</param>
    /// <param name="topLeftRadius">Optional border-top-left-radius.</param>
    /// <param name="topRightRadius">Optional border-top-right-radius.</param>
    /// <param name="bottomRightRadius">Optional border-bottom-right-radius.</param>
    /// <param name="bottomLeftRadius">Optional border-bottom-left-radius.</param>
    public AllyariaBorders(AllyariaNumberValue? topWidth = null,
        AllyariaNumberValue? rightWidth = null,
        AllyariaNumberValue? bottomWidth = null,
        AllyariaNumberValue? leftWidth = null,
        AllyariaStringValue? topStyle = null,
        AllyariaStringValue? rightStyle = null,
        AllyariaStringValue? bottomStyle = null,
        AllyariaStringValue? leftStyle = null,
        AllyariaNumberValue? topLeftRadius = null,
        AllyariaNumberValue? topRightRadius = null,
        AllyariaNumberValue? bottomRightRadius = null,
        AllyariaNumberValue? bottomLeftRadius = null)
    {
        TopWidth = topWidth;
        RightWidth = rightWidth;
        BottomWidth = bottomWidth;
        LeftWidth = leftWidth;

        TopStyle = topStyle;
        RightStyle = rightStyle;
        BottomStyle = bottomStyle;
        LeftStyle = leftStyle;

        TopLeftRadius = topLeftRadius;
        TopRightRadius = topRightRadius;
        BottomRightRadius = bottomRightRadius;
        BottomLeftRadius = bottomLeftRadius;
    }

    /// <summary>Gets or initializes the border-bottom-left-radius, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? BottomLeftRadius { get; init; }

    /// <summary>Gets or initializes the border-bottom-right-radius, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? BottomRightRadius { get; init; }

    /// <summary>Gets or initializes the border-bottom-style, or <see langword="null" /> when unset.</summary>
    public AllyariaStringValue? BottomStyle { get; init; }

    /// <summary>Gets or initializes the border-bottom-width, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? BottomWidth { get; init; }

    /// <summary>Gets or initializes the border-left-style, or <see langword="null" /> when unset.</summary>
    public AllyariaStringValue? LeftStyle { get; init; }

    /// <summary>Gets or initializes the border-left-width, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? LeftWidth { get; init; }

    /// <summary>Gets or initializes the border-right-style, or <see langword="null" /> when unset.</summary>
    public AllyariaStringValue? RightStyle { get; init; }

    /// <summary>Gets or initializes the border-right-width, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? RightWidth { get; init; }

    /// <summary>Gets or initializes the border-top-left-radius, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? TopLeftRadius { get; init; }

    /// <summary>Gets or initializes the border-top-right-radius, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? TopRightRadius { get; init; }

    /// <summary>Gets or initializes the border-top-style (e.g., "solid"), or <see langword="null" /> when unset.</summary>
    public AllyariaStringValue? TopStyle { get; init; }

    /// <summary>Gets or initializes the border-top-width, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? TopWidth { get; init; }

    /// <summary>
    /// Appends a CSS declaration (<c>property:value;</c>) to the builder when the value is non-null. Overload for number
    /// values (widths/radii). Values are normalized by <see cref="AllyariaNumberValue" />.
    /// :contentReference[oaicite:1]{index=1}
    /// </summary>
    private static void AppendIfNotNull(StringBuilder builder, AllyariaNumberValue? value, string propertyName)
    {
        if (value is not null)
        {
            builder.Append(value.ToCss(propertyName));
        }
    }

    /// <summary>
    /// Appends a CSS declaration (<c>property:value;</c>) to the builder when the value is non-null. Overload for style
    /// tokens. Values are normalized by <see cref="AllyariaStringValue" />. :contentReference[oaicite:2]{index=2}
    /// </summary>
    private static void AppendIfNotNull(StringBuilder builder, AllyariaStringValue? value, string propertyName)
    {
        if (value is not null)
        {
            builder.Append(value.ToCss(propertyName));
        }
    }

    /// <summary>
    /// Returns a new borders definition by applying per-side/per-corner overrides. Each parameter is optional; when
    /// <see langword="null" />, the existing value for that member is preserved (which may also be <see langword="null" />).
    /// </summary>
    public AllyariaBorders Cascade(AllyariaNumberValue? topWidth = null,
        AllyariaNumberValue? rightWidth = null,
        AllyariaNumberValue? bottomWidth = null,
        AllyariaNumberValue? leftWidth = null,
        AllyariaStringValue? topStyle = null,
        AllyariaStringValue? rightStyle = null,
        AllyariaStringValue? bottomStyle = null,
        AllyariaStringValue? leftStyle = null,
        AllyariaNumberValue? topLeftRadius = null,
        AllyariaNumberValue? topRightRadius = null,
        AllyariaNumberValue? bottomRightRadius = null,
        AllyariaNumberValue? bottomLeftRadius = null)
        => new()
        {
            TopWidth = topWidth ?? TopWidth,
            RightWidth = rightWidth ?? RightWidth,
            BottomWidth = bottomWidth ?? BottomWidth,
            LeftWidth = leftWidth ?? LeftWidth,

            TopStyle = topStyle ?? TopStyle,
            RightStyle = rightStyle ?? RightStyle,
            BottomStyle = bottomStyle ?? BottomStyle,
            LeftStyle = leftStyle ?? LeftStyle,

            TopLeftRadius = topLeftRadius ?? TopLeftRadius,
            TopRightRadius = topRightRadius ?? TopRightRadius,
            BottomRightRadius = bottomRightRadius ?? BottomRightRadius,
            BottomLeftRadius = bottomLeftRadius ?? BottomLeftRadius
        };

    /// <summary>
    /// Creates a uniform <see cref="AllyariaBorders" /> where all sides share the same width and style, and all four corners
    /// share the same radius.
    /// </summary>
    /// <param name="width">Width for top/right/bottom/left (e.g., "1px").</param>
    /// <param name="style">Style for top/right/bottom/left (e.g., "solid").</param>
    /// <param name="radius">Radius for all corners (e.g., ".25rem").</param>
    /// <returns>New uniform <see cref="AllyariaBorders" /> instance.</returns>
    public static AllyariaBorders FromSingle(AllyariaNumberValue width,
        AllyariaStringValue style,
        AllyariaNumberValue radius)
        => new(
            width, width, width, width, style, style, style, style, radius, radius,
            radius, radius
        );

    /// <summary>
    /// Creates a <see cref="AllyariaBorders" /> with symmetric horizontal/vertical widths and styles, and symmetric radii for
    /// (top-left/top-right) vs. (bottom-left/bottom-right).
    /// </summary>
    /// <param name="widthHorizontal">Width for left/right.</param>
    /// <param name="widthVertical">Width for top/bottom.</param>
    /// <param name="styleHorizontal">Style for left/right.</param>
    /// <param name="styleVertical">Style for top/bottom.</param>
    /// <param name="radiusTop">Radius for top-left/top-right.</param>
    /// <param name="radiusBottom">Radius for bottom-left/bottom-right.</param>
    /// <returns>New symmetric <see cref="AllyariaBorders" /> instance.</returns>
    public static AllyariaBorders FromSymmetric(AllyariaNumberValue widthHorizontal,
        AllyariaNumberValue widthVertical,
        AllyariaStringValue styleHorizontal,
        AllyariaStringValue styleVertical,
        AllyariaNumberValue radiusTop,
        AllyariaNumberValue radiusBottom)
        => new(
            widthVertical,
            widthHorizontal,
            widthVertical,
            widthHorizontal,
            styleVertical,
            styleHorizontal,
            styleVertical,
            styleHorizontal,
            radiusTop,
            radiusTop,
            radiusBottom,
            radiusBottom
        );

    /// <summary>
    /// Builds a CSS declaration list (e.g., <c>border-top-width:1px;border-left-style:solid;border-top-left-radius:.25rem;</c>
    /// ). Only non-<see langword="null" /> members are emitted.
    /// </summary>
    public string ToCss()
    {
        var sb = new StringBuilder();

        // widths
        AppendIfNotNull(sb, TopWidth, "border-top-width");
        AppendIfNotNull(sb, RightWidth, "border-right-width");
        AppendIfNotNull(sb, BottomWidth, "border-bottom-width");
        AppendIfNotNull(sb, LeftWidth, "border-left-width");

        // styles
        AppendIfNotNull(sb, TopStyle, "border-top-style");
        AppendIfNotNull(sb, RightStyle, "border-right-style");
        AppendIfNotNull(sb, BottomStyle, "border-bottom-style");
        AppendIfNotNull(sb, LeftStyle, "border-left-style");

        // radii (per-corner to avoid unintended shorthands)
        AppendIfNotNull(sb, TopLeftRadius, "border-top-left-radius");
        AppendIfNotNull(sb, TopRightRadius, "border-top-right-radius");
        AppendIfNotNull(sb, BottomRightRadius, "border-bottom-right-radius");
        AppendIfNotNull(sb, BottomLeftRadius, "border-bottom-left-radius");

        return sb.ToString();
    }

    /// <summary>
    /// Builds a CSS custom-properties string for this border set. The optional <paramref name="prefix" /> is normalized by
    /// collapsing whitespace/dashes, trimming leading/trailing dashes, and lowercasing. If empty, the default <c>--aa-</c>
    /// prefix is used; otherwise variables emit as <c>--{prefix}-border-top-width</c>, etc. Only non-null members are emitted.
    /// </summary>
    /// <param name="prefix">Optional logical namespace for variables (e.g., <c>editor</c>).</param>
    public string ToCssVars(string prefix = "")
    {
        var basePrefix = Regex.Replace(prefix, @"[\s-]+", "-").Trim('-').ToLowerInvariant();

        basePrefix = string.IsNullOrWhiteSpace(basePrefix)
            ? "--aa-"
            : $"--{basePrefix}-";

        var sb = new StringBuilder();

        // widths
        AppendIfNotNull(sb, TopWidth, $"{basePrefix}border-top-width");
        AppendIfNotNull(sb, RightWidth, $"{basePrefix}border-right-width");
        AppendIfNotNull(sb, BottomWidth, $"{basePrefix}border-bottom-width");
        AppendIfNotNull(sb, LeftWidth, $"{basePrefix}border-left-width");

        // styles
        AppendIfNotNull(sb, TopStyle, $"{basePrefix}border-top-style");
        AppendIfNotNull(sb, RightStyle, $"{basePrefix}border-right-style");
        AppendIfNotNull(sb, BottomStyle, $"{basePrefix}border-bottom-style");
        AppendIfNotNull(sb, LeftStyle, $"{basePrefix}border-left-style");

        // radii
        AppendIfNotNull(sb, TopLeftRadius, $"{basePrefix}border-top-left-radius");
        AppendIfNotNull(sb, TopRightRadius, $"{basePrefix}border-top-right-radius");
        AppendIfNotNull(sb, BottomRightRadius, $"{basePrefix}border-bottom-right-radius");
        AppendIfNotNull(sb, BottomLeftRadius, $"{basePrefix}border-bottom-left-radius");

        return sb.ToString();
    }
}
