namespace Allyaria.Theming.Styles;

/// <summary>
/// Strongly-typed border definition for Allyaria theming. Supports per-side <c>width</c> and <c>style</c>
/// (top/right/bottom/left) and per-corner <c>radius</c> (top-left/top-right/bottom-right/bottom-left). Widths and radii
/// use <see cref="ThemeNumber" /> (e.g., <c>1px</c>, <c>.125rem</c>, <c>0</c>, <c>50%</c>), and styles use
/// <see cref="ThemeString" /> (e.g., <c>solid</c>, <c>dashed</c>, <c>none</c>).
/// </summary>
/// <remarks>
/// This type intentionally omits border color; colors come from the palette/theme layer. Values are emitted to CSS only
/// when their underlying representation is non-empty (see
/// <see cref="StyleHelper.ToCss(StringBuilder, ThemeBase, string, string)" />).
/// </remarks>
public readonly record struct Borders
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Borders" /> struct, setting all widths, styles, and radii to
    /// <see cref="StyleDefaults" /> values.
    /// </summary>
    /// <remarks>
    /// This constructor exists so that <c>new Borders()</c> will apply the theme’s default border width, style, and radius
    /// values, rather than leaving the struct in its zero-initialized state.
    /// </remarks>
    public Borders()
        : this(null) { }

    /// <summary>
    /// Initializes a new <see cref="Borders" /> with optional per-side widths/styles and per-corner radii. Any parameter left
    /// as <see langword="null" /> falls back to <see cref="StyleDefaults" /> for that member.
    /// </summary>
    /// <param name="topWidth">Optional <c>border-top-width</c>.</param>
    /// <param name="endWidth">Optional <c>border-inline-end-width</c>.</param>
    /// <param name="bottomWidth">Optional <c>border-bottom-width</c>.</param>
    /// <param name="startWidth">Optional <c>border-inline-start-width</c>.</param>
    /// <param name="topStyle">Optional <c>border-top-style</c> (e.g., <c>solid</c>).</param>
    /// <param name="endStyle">Optional <c>border-inline-end-style</c>.</param>
    /// <param name="bottomStyle">Optional <c>border-bottom-style</c>.</param>
    /// <param name="startStyle">Optional <c>border-inline-start-style</c>.</param>
    /// <param name="topLeftRadius">Optional <c>border-top-left-radius</c>.</param>
    /// <param name="topRightRadius">Optional <c>border-top-right-radius</c>.</param>
    /// <param name="bottomRightRadius">Optional <c>border-bottom-right-radius</c>.</param>
    /// <param name="bottomLeftRadius">Optional <c>border-bottom-left-radius</c>.</param>
    public Borders(ThemeNumber? topWidth = null,
        ThemeNumber? endWidth = null,
        ThemeNumber? bottomWidth = null,
        ThemeNumber? startWidth = null,
        ThemeString? topStyle = null,
        ThemeString? endStyle = null,
        ThemeString? bottomStyle = null,
        ThemeString? startStyle = null,
        ThemeNumber? topLeftRadius = null,
        ThemeNumber? topRightRadius = null,
        ThemeNumber? bottomRightRadius = null,
        ThemeNumber? bottomLeftRadius = null)
    {
        TopWidth = topWidth ?? StyleDefaults.BorderWidth;
        EndWidth = endWidth ?? StyleDefaults.BorderWidth;
        BottomWidth = bottomWidth ?? StyleDefaults.BorderWidth;
        StartWidth = startWidth ?? StyleDefaults.BorderWidth;
        TopStyle = topStyle ?? StyleDefaults.BorderStyle;
        EndStyle = endStyle ?? StyleDefaults.BorderStyle;
        BottomStyle = bottomStyle ?? StyleDefaults.BorderStyle;
        StartStyle = startStyle ?? StyleDefaults.BorderStyle;
        TopLeftRadius = topLeftRadius ?? StyleDefaults.BorderRadius;
        TopRightRadius = topRightRadius ?? StyleDefaults.BorderRadius;
        BottomRightRadius = bottomRightRadius ?? StyleDefaults.BorderRadius;
        BottomLeftRadius = bottomLeftRadius ?? StyleDefaults.BorderRadius;
    }

    /// <summary>Gets or initializes the <c>border-bottom-left-radius</c>.</summary>
    public ThemeNumber BottomLeftRadius { get; init; }

    /// <summary>Gets or initializes the <c>border-bottom-right-radius</c>.</summary>
    public ThemeNumber BottomRightRadius { get; init; }

    /// <summary>Gets or initializes the <c>border-bottom-style</c>.</summary>
    public ThemeString BottomStyle { get; init; }

    /// <summary>Gets or initializes the <c>border-bottom-width</c>.</summary>
    public ThemeNumber BottomWidth { get; init; }

    /// <summary>Gets or initializes the <c>border-inline-end-style</c>.</summary>
    public ThemeString EndStyle { get; init; }

    /// <summary>Gets or initializes the <c>border-inline-end-width</c>.</summary>
    public ThemeNumber EndWidth { get; init; }

    /// <summary>
    /// Gets the focus border width based on the largest of all four border widths. If the largest width is less than 2, the
    /// focus width is 2. Otherwise, the focus width is the largest width plus 2.
    /// </summary>
    public ThemeNumber FocusWidth
    {
        get
        {
            var largest = new[]
            {
                TopWidth.Number,
                StartWidth.Number,
                BottomWidth.Number,
                EndWidth.Number
            }.Max();

            var focus = largest < 2
                ? 2
                : largest + 2;

            return new ThemeNumber($"{focus}px");
        }
    }

    /// <summary>Gets or initializes the <c>border-inline-start-style</c>.</summary>
    public ThemeString StartStyle { get; init; }

    /// <summary>Gets or initializes the <c>border-inline-start-width</c>.</summary>
    public ThemeNumber StartWidth { get; init; }

    /// <summary>Gets or initializes the <c>border-top-left-radius</c>.</summary>
    public ThemeNumber TopLeftRadius { get; init; }

    /// <summary>Gets or initializes the <c>border-top-right-radius</c>.</summary>
    public ThemeNumber TopRightRadius { get; init; }

    /// <summary>Gets or initializes the <c>border-top-style</c> (e.g., <c>solid</c>).</summary>
    public ThemeString TopStyle { get; init; }

    /// <summary>Gets or initializes the <c>border-top-width</c>.</summary>
    public ThemeNumber TopWidth { get; init; }

    /// <summary>
    /// Returns a new borders definition by applying per-side/per-corner overrides. Each argument is optional; when
    /// <see langword="null" />, the current theme is preserved.
    /// </summary>
    /// <param name="topWidth">Optional <c>border-top-width</c>.</param>
    /// <param name="endWidth">Optional <c>border-inline-end-width</c>.</param>
    /// <param name="bottomWidth">Optional <c>border-bottom-width</c>.</param>
    /// <param name="startWidth">Optional <c>border-inline-start-width</c>.</param>
    /// <param name="topStyle">Optional <c>border-top-style</c> (e.g., <c>solid</c>).</param>
    /// <param name="endStyle">Optional <c>border-inline-end-style</c>.</param>
    /// <param name="bottomStyle">Optional <c>border-bottom-style</c>.</param>
    /// <param name="startStyle">Optional <c>border-inline-start-style</c>.</param>
    /// <param name="topLeftRadius">Optional <c>border-top-left-radius</c>.</param>
    /// <param name="topRightRadius">Optional <c>border-top-right-radius</c>.</param>
    /// <param name="bottomRightRadius">Optional <c>border-bottom-right-radius</c>.</param>
    /// <param name="bottomLeftRadius">Optional <c>border-bottom-left-radius</c>.</param>
    /// <returns>A new <see cref="Borders" /> instance with overrides applied.</returns>
    public Borders Cascade(ThemeNumber? topWidth = null,
        ThemeNumber? endWidth = null,
        ThemeNumber? bottomWidth = null,
        ThemeNumber? startWidth = null,
        ThemeString? topStyle = null,
        ThemeString? endStyle = null,
        ThemeString? bottomStyle = null,
        ThemeString? startStyle = null,
        ThemeNumber? topLeftRadius = null,
        ThemeNumber? topRightRadius = null,
        ThemeNumber? bottomRightRadius = null,
        ThemeNumber? bottomLeftRadius = null)
        => this with
        {
            TopWidth = topWidth ?? TopWidth,
            EndWidth = endWidth ?? EndWidth,
            BottomWidth = bottomWidth ?? BottomWidth,
            StartWidth = startWidth ?? StartWidth,
            TopStyle = topStyle ?? TopStyle,
            EndStyle = endStyle ?? EndStyle,
            BottomStyle = bottomStyle ?? BottomStyle,
            StartStyle = startStyle ?? StartStyle,
            TopLeftRadius = topLeftRadius ?? TopLeftRadius,
            TopRightRadius = topRightRadius ?? TopRightRadius,
            BottomRightRadius = bottomRightRadius ?? BottomRightRadius,
            BottomLeftRadius = bottomLeftRadius ?? BottomLeftRadius
        };

    /// <summary>
    /// Creates a uniform <see cref="Borders" /> where all sides share the same width and style, and all four corners share the
    /// same radius.
    /// </summary>
    /// <param name="width">Width for top/right/bottom/left (e.g., <c>1px</c>).</param>
    /// <param name="style">Style for top/right/bottom/left (e.g., <c>solid</c>).</param>
    /// <param name="radius">Radius for all four corners (e.g., <c>.25rem</c>).</param>
    /// <returns>A new uniform <see cref="Borders" /> instance.</returns>
    public static Borders FromSingle(ThemeNumber width,
        ThemeString style,
        ThemeNumber radius)
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
    /// Creates a <see cref="Borders" /> with symmetric horizontal/vertical widths and styles, and symmetric radii for top vs.
    /// bottom pairs.
    /// </summary>
    /// <param name="widthHorizontal">Width for left/right.</param>
    /// <param name="widthVertical">Width for top/bottom.</param>
    /// <param name="styleHorizontal">Style for left/right.</param>
    /// <param name="styleVertical">Style for top/bottom.</param>
    /// <param name="radiusTop">Radius for top-left/top-right.</param>
    /// <param name="radiusBottom">Radius for bottom-left/bottom-right.</param>
    /// <returns>A new symmetric <see cref="Borders" /> instance.</returns>
    public static Borders FromSymmetric(ThemeNumber widthHorizontal,
        ThemeNumber widthVertical,
        ThemeString styleHorizontal,
        ThemeString styleVertical,
        ThemeNumber radiusTop,
        ThemeNumber radiusBottom)
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
    /// <c>--{varPrefix}-[propertyName]</c>. Hyphens and whitespace in the prefix are normalized; case is lowered.
    /// </param>
    /// <param name="isFocus">Determines if this is a focus border or not.</param>
    /// <returns>
    /// A CSS string containing zero or more declarations (each ending as produced by
    /// <see cref="StyleHelper.ToCss(StringBuilder, ThemeBase, string, string)" />).
    /// </returns>
    public string ToCss(string? varPrefix = "", bool isFocus = false)
    {
        var topWidth = isFocus
            ? FocusWidth
            : TopWidth;

        var endWidth = isFocus
            ? FocusWidth
            : EndWidth;

        var bottomWidth = isFocus
            ? FocusWidth
            : BottomWidth;

        var startWidth = isFocus
            ? FocusWidth
            : StartWidth;

        var topStyle = isFocus
            ? BorderStyle.Dashed
            : TopStyle;

        var endStyle = isFocus
            ? BorderStyle.Dashed
            : EndStyle;

        var bottomStyle = isFocus
            ? BorderStyle.Dashed
            : BottomStyle;

        var startStyle = isFocus
            ? BorderStyle.Dashed
            : StartStyle;

        var builder = new StringBuilder();

        builder.ToCss(topWidth, "border-top-width", varPrefix);
        builder.ToCss(endWidth, "border-inline-end-width", varPrefix);
        builder.ToCss(bottomWidth, "border-bottom-width", varPrefix);
        builder.ToCss(startWidth, "border-inline-start-width", varPrefix);
        builder.ToCss(topStyle, "border-top-style", varPrefix);
        builder.ToCss(endStyle, "border-inline-end-style", varPrefix);
        builder.ToCss(bottomStyle, "border-bottom-style", varPrefix);
        builder.ToCss(startStyle, "border-inline-start-style", varPrefix);
        builder.ToCss(TopLeftRadius, "border-top-left-radius", varPrefix);
        builder.ToCss(TopRightRadius, "border-top-right-radius", varPrefix);
        builder.ToCss(BottomLeftRadius, "border-bottom-left-radius", varPrefix);
        builder.ToCss(BottomRightRadius, "border-bottom-right-radius", varPrefix);

        return builder.ToString();
    }
}
