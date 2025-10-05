using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Values;
using System.Text;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Strongly-typed border definition for Allyaria theming. Supports per-side <c>width</c> and <c>style</c>
/// (top/right/bottom/left) and per-corner <c>radius</c> (top-left/top-right/bottom-right/bottom-left). Widths and radii
/// use <see cref="AllyariaNumberValue" /> (e.g., <c>1px</c>, <c>.125rem</c>, <c>0</c>, <c>50%</c>), and styles use
/// <see cref="AllyariaStringValue" /> (e.g., <c>solid</c>, <c>dashed</c>, <c>none</c>).
/// </summary>
/// <remarks>
/// This type intentionally omits border color; colors come from the palette/theme layer. Values are emitted to CSS
/// only when their underlying representation is non-empty (see
/// <see cref="StyleHelper.ToCss(StringBuilder, Allyaria.Theming.Contracts.ValueBase, string, string)" />).
/// </remarks>
public readonly record struct AllyariaBorders
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaBorders" /> struct, setting all widths, styles, and radii to
    /// <see cref="StyleDefaults" /> values.
    /// </summary>
    /// <remarks>
    /// This constructor exists so that <c>new AllyariaBorders()</c> will apply the theme’s default border width, style, and
    /// radius values, rather than leaving the struct in its zero-initialized state.
    /// </remarks>
    public AllyariaBorders()
        : this(null) { }

    /// <summary>
    /// Initializes a new <see cref="AllyariaBorders" /> with optional per-side widths/styles and per-corner radii. Any
    /// parameter left as <see langword="null" /> falls back to <see cref="StyleDefaults" /> for that member.
    /// </summary>
    /// <param name="topWidth">Optional <c>border-top-width</c>.</param>
    /// <param name="rightWidth">Optional <c>border-right-width</c>.</param>
    /// <param name="bottomWidth">Optional <c>border-bottom-width</c>.</param>
    /// <param name="leftWidth">Optional <c>border-left-width</c>.</param>
    /// <param name="topStyle">Optional <c>border-top-style</c> (e.g., <c>solid</c>).</param>
    /// <param name="rightStyle">Optional <c>border-right-style</c>.</param>
    /// <param name="bottomStyle">Optional <c>border-bottom-style</c>.</param>
    /// <param name="leftStyle">Optional <c>border-left-style</c>.</param>
    /// <param name="topLeftRadius">Optional <c>border-top-left-radius</c>.</param>
    /// <param name="topRightRadius">Optional <c>border-top-right-radius</c>.</param>
    /// <param name="bottomRightRadius">Optional <c>border-bottom-right-radius</c>.</param>
    /// <param name="bottomLeftRadius">Optional <c>border-bottom-left-radius</c>.</param>
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
        TopWidth = topWidth ?? StyleDefaults.BorderWidth;
        RightWidth = rightWidth ?? StyleDefaults.BorderWidth;
        BottomWidth = bottomWidth ?? StyleDefaults.BorderWidth;
        LeftWidth = leftWidth ?? StyleDefaults.BorderWidth;
        TopStyle = topStyle ?? StyleDefaults.BorderStyle;
        RightStyle = rightStyle ?? StyleDefaults.BorderStyle;
        BottomStyle = bottomStyle ?? StyleDefaults.BorderStyle;
        LeftStyle = leftStyle ?? StyleDefaults.BorderStyle;
        TopLeftRadius = topLeftRadius ?? StyleDefaults.BorderRadius;
        TopRightRadius = topRightRadius ?? StyleDefaults.BorderRadius;
        BottomRightRadius = bottomRightRadius ?? StyleDefaults.BorderRadius;
        BottomLeftRadius = bottomLeftRadius ?? StyleDefaults.BorderRadius;
    }

    /// <summary>Gets or initializes the <c>border-bottom-left-radius</c>.</summary>
    public AllyariaNumberValue BottomLeftRadius { get; init; }

    /// <summary>Gets or initializes the <c>border-bottom-right-radius</c>.</summary>
    public AllyariaNumberValue BottomRightRadius { get; init; }

    /// <summary>Gets or initializes the <c>border-bottom-style</c>.</summary>
    public AllyariaStringValue BottomStyle { get; init; }

    /// <summary>Gets or initializes the <c>border-bottom-width</c>.</summary>
    public AllyariaNumberValue BottomWidth { get; init; }

    /// <summary>Gets or initializes the <c>border-left-style</c>.</summary>
    public AllyariaStringValue LeftStyle { get; init; }

    /// <summary>Gets or initializes the <c>border-left-width</c>.</summary>
    public AllyariaNumberValue LeftWidth { get; init; }

    /// <summary>Gets or initializes the <c>border-right-style</c>.</summary>
    public AllyariaStringValue RightStyle { get; init; }

    /// <summary>Gets or initializes the <c>border-right-width</c>.</summary>
    public AllyariaNumberValue RightWidth { get; init; }

    /// <summary>Gets or initializes the <c>border-top-left-radius</c>.</summary>
    public AllyariaNumberValue TopLeftRadius { get; init; }

    /// <summary>Gets or initializes the <c>border-top-right-radius</c>.</summary>
    public AllyariaNumberValue TopRightRadius { get; init; }

    /// <summary>Gets or initializes the <c>border-top-style</c> (e.g., <c>solid</c>).</summary>
    public AllyariaStringValue TopStyle { get; init; }

    /// <summary>Gets or initializes the <c>border-top-width</c>.</summary>
    public AllyariaNumberValue TopWidth { get; init; }

    /// <summary>
    /// Returns a new borders definition by applying per-side/per-corner overrides. Each argument is optional; when
    /// <see langword="null" />, the current value is preserved.
    /// </summary>
    /// <param name="topWidth">Optional override for <c>border-top-width</c>.</param>
    /// <param name="rightWidth">Optional override for <c>border-right-width</c>.</param>
    /// <param name="bottomWidth">Optional override for <c>border-bottom-width</c>.</param>
    /// <param name="leftWidth">Optional override for <c>border-left-width</c>.</param>
    /// <param name="topStyle">Optional override for <c>border-top-style</c>.</param>
    /// <param name="rightStyle">Optional override for <c>border-right-style</c>.</param>
    /// <param name="bottomStyle">Optional override for <c>border-bottom-style</c>.</param>
    /// <param name="leftStyle">Optional override for <c>border-left-style</c>.</param>
    /// <param name="topLeftRadius">Optional override for <c>border-top-left-radius</c>.</param>
    /// <param name="topRightRadius">Optional override for <c>border-top-right-radius</c>.</param>
    /// <param name="bottomRightRadius">Optional override for <c>border-bottom-right-radius</c>.</param>
    /// <param name="bottomLeftRadius">Optional override for <c>border-bottom-left-radius</c>.</param>
    /// <returns>A new <see cref="AllyariaBorders" /> instance with overrides applied.</returns>
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
        => this with
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
    /// <param name="width">Width for top/right/bottom/left (e.g., <c>1px</c>).</param>
    /// <param name="style">Style for top/right/bottom/left (e.g., <c>solid</c>).</param>
    /// <param name="radius">Radius for all four corners (e.g., <c>.25rem</c>).</param>
    /// <returns>A new uniform <see cref="AllyariaBorders" /> instance.</returns>
    public static AllyariaBorders FromSingle(AllyariaNumberValue width,
        AllyariaStringValue style,
        AllyariaNumberValue radius)
        => new(
            width,
            width,
            width,
            width,
            style,
            style,
            style,
            style,
            radius,
            radius,
            radius,
            radius
        );

    /// <summary>
    /// Creates a <see cref="AllyariaBorders" /> with symmetric horizontal/vertical widths and styles, and symmetric radii for
    /// top vs. bottom pairs.
    /// </summary>
    /// <param name="widthHorizontal">Width for left/right.</param>
    /// <param name="widthVertical">Width for top/bottom.</param>
    /// <param name="styleHorizontal">Style for left/right.</param>
    /// <param name="styleVertical">Style for top/bottom.</param>
    /// <param name="radiusTop">Radius for top-left/top-right.</param>
    /// <param name="radiusBottom">Radius for bottom-left/bottom-right.</param>
    /// <returns>A new symmetric <see cref="AllyariaBorders" /> instance.</returns>
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

    /// <summary>Builds CSS declarations for all non-empty border members.</summary>
    /// <param name="varPrefix">
    /// Optional prefix for generating CSS custom properties. When provided, each property name is emitted as
    /// <c>--{varPrefix}-var-[propertyName]</c>. Hyphens and whitespace in the prefix are normalized; case is lowered.
    /// </param>
    /// <returns>
    /// A CSS string containing zero or more declarations (each ending as produced by
    /// <see cref="StyleHelper.ToCss(StringBuilder, Allyaria.Theming.Contracts.ValueBase, string, string)" />).
    /// </returns>
    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();
        builder.ToCss(TopWidth, "border-top-width", varPrefix);
        builder.ToCss(RightWidth, "border-right-width", varPrefix);
        builder.ToCss(BottomWidth, "border-bottom-width", varPrefix);
        builder.ToCss(LeftWidth, "border-left-width", varPrefix);
        builder.ToCss(TopStyle, "border-top-style", varPrefix);
        builder.ToCss(RightStyle, "border-right-style", varPrefix);
        builder.ToCss(BottomStyle, "border-bottom-style", varPrefix);
        builder.ToCss(LeftStyle, "border-left-style", varPrefix);
        builder.ToCss(TopLeftRadius, "border-top-left-radius", varPrefix);
        builder.ToCss(TopRightRadius, "border-top-right-radius", varPrefix);
        builder.ToCss(BottomLeftRadius, "border-bottom-left-radius", varPrefix);
        builder.ToCss(BottomRightRadius, "border-bottom-right-radius", varPrefix);

        return builder.ToString();
    }
}
