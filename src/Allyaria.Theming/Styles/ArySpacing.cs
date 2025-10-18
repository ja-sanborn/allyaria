namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents logical spacing (margins and paddings) for a themable element, providing strongly typed values and helpers
/// to compute CSS declarations.
/// </summary>
/// <remarks>
/// Defaults are taken from <see cref="StyleDefaults" /> when constructor arguments are not provided. This type is
/// immutable (readonly record struct) and safe for value semantics.
/// </remarks>
public readonly record struct ArySpacing
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArySpacing" /> struct using all default margin and padding values from
    /// <see cref="StyleDefaults" />.
    /// </summary>
    public ArySpacing()
        : this(null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArySpacing" /> struct, applying provided values or falling back to
    /// <see cref="StyleDefaults.Margin" /> and <see cref="StyleDefaults.Padding" /> when a value is not supplied.
    /// </summary>
    /// <param name="marginTop">The margin on the top side, or <c>null</c> to use the default margin.</param>
    /// <param name="marginEnd">The margin on the right side (left in RTL), or <c>null</c> to use the default margin.</param>
    /// <param name="marginBottom">The margin on the bottom side, or <c>null</c> to use the default margin.</param>
    /// <param name="marginStart">The margin on the left side (right in RTL), or <c>null</c> to use the default margin.</param>
    /// <param name="paddingTop">The padding on the top side, or <c>null</c> to use the default padding.</param>
    /// <param name="paddingEnd">The padding on the right side (left in RTL), or <c>null</c> to use the default padding.</param>
    /// <param name="paddingBottom">The padding on the bottom side, or <c>null</c> to use the default padding.</param>
    /// <param name="paddingStart">The padding on the left side (right in RTL), or <c>null</c> to use the default padding.</param>
    public ArySpacing(AryNumberValue? marginTop = null,
        AryNumberValue? marginEnd = null,
        AryNumberValue? marginBottom = null,
        AryNumberValue? marginStart = null,
        AryNumberValue? paddingTop = null,
        AryNumberValue? paddingEnd = null,
        AryNumberValue? paddingBottom = null,
        AryNumberValue? paddingStart = null)
    {
        MarginTop = marginTop ?? StyleDefaults.Margin;
        MarginEnd = marginEnd ?? StyleDefaults.Margin;
        MarginBottom = marginBottom ?? StyleDefaults.Margin;
        MarginStart = marginStart ?? StyleDefaults.Margin;
        PaddingTop = paddingTop ?? StyleDefaults.Padding;
        PaddingEnd = paddingEnd ?? StyleDefaults.Padding;
        PaddingBottom = paddingBottom ?? StyleDefaults.Padding;
        PaddingStart = paddingStart ?? StyleDefaults.Padding;
    }

    /// <summary>Gets or initializes the margin on the bottom side.</summary>
    public AryNumberValue MarginBottom { get; init; }

    /// <summary>Gets or initializes the margin on the right side (left in RTL settings).</summary>
    public AryNumberValue MarginEnd { get; init; }

    /// <summary>Gets or initializes the margin on the left side (right in RTL settings).</summary>
    public AryNumberValue MarginStart { get; init; }

    /// <summary>Gets or initializes the margin on the top side.</summary>
    public AryNumberValue MarginTop { get; init; }

    /// <summary>Gets or initializes the padding on the bottom side.</summary>
    public AryNumberValue PaddingBottom { get; init; }

    /// <summary>Gets or initializes the padding on the right side (left in RTL settings).</summary>
    public AryNumberValue PaddingEnd { get; init; }

    /// <summary>Gets or initializes the padding on the left side (right in RTL settings).</summary>
    public AryNumberValue PaddingStart { get; init; }

    /// <summary>Gets or initializes the padding on the top side.</summary>
    public AryNumberValue PaddingTop { get; init; }

    /// <summary>
    /// Returns a copy of this instance with the provided values overriding existing ones. Any argument left as <c>null</c>
    /// preserves the corresponding current value.
    /// </summary>
    /// <param name="marginTop">Optional new top margin.</param>
    /// <param name="marginEnd">Optional new right margin (left in RTL).</param>
    /// <param name="marginBottom">Optional new bottom margin.</param>
    /// <param name="marginStart">Optional new left margin (right in RTL).</param>
    /// <param name="paddingTop">Optional new top padding.</param>
    /// <param name="paddingEnd">Optional new right padding (left in RTL).</param>
    /// <param name="paddingBottom">Optional new bottom padding.</param>
    /// <param name="paddingStart">Optional new left padding (right in RTL).</param>
    /// <returns>A new <see cref="ArySpacing" /> with supplied values applied and unspecified values preserved.</returns>
    public ArySpacing Cascade(AryNumberValue? marginTop = null,
        AryNumberValue? marginEnd = null,
        AryNumberValue? marginBottom = null,
        AryNumberValue? marginStart = null,
        AryNumberValue? paddingTop = null,
        AryNumberValue? paddingEnd = null,
        AryNumberValue? paddingBottom = null,
        AryNumberValue? paddingStart = null)
        => this with
        {
            MarginTop = marginTop ?? MarginTop,
            MarginEnd = marginEnd ?? MarginEnd,
            MarginBottom = marginBottom ?? MarginBottom,
            MarginStart = marginStart ?? MarginStart,
            PaddingTop = paddingTop ?? PaddingTop,
            PaddingEnd = paddingEnd ?? PaddingEnd,
            PaddingBottom = paddingBottom ?? PaddingBottom,
            PaddingStart = paddingStart ?? PaddingStart
        };

    /// <summary>
    /// Creates an <see cref="ArySpacing" /> where all margins use the same value and all paddings use the same value.
    /// </summary>
    /// <param name="margin">The margin value to apply to all sides.</param>
    /// <param name="padding">The padding value to apply to all sides.</param>
    /// <returns>A new <see cref="ArySpacing" /> instance with uniform margins and paddings.</returns>
    public static ArySpacing FromSingle(AryNumberValue margin, AryNumberValue padding)
        => new(margin, margin, margin, margin, padding, padding, padding, padding);

    /// <summary>Creates an <see cref="ArySpacing" /> with symmetric horizontal and vertical margins and paddings.</summary>
    /// <param name="marginHorizontal">The margin value for left and right sides.</param>
    /// <param name="marginVertical">The margin value for top and bottom sides.</param>
    /// <param name="paddingHorizontal">The padding value for left and right sides.</param>
    /// <param name="paddingVertical">The padding value for top and bottom sides.</param>
    /// <returns>A new <see cref="ArySpacing" /> instance with symmetric spacing.</returns>
    public static ArySpacing FromSymmetric(AryNumberValue marginHorizontal,
        AryNumberValue marginVertical,
        AryNumberValue paddingHorizontal,
        AryNumberValue paddingVertical)
        => new(
            marginVertical, marginHorizontal, marginVertical, marginHorizontal, paddingVertical, paddingHorizontal,
            paddingVertical, paddingHorizontal
        );

    /// <summary>
    /// Builds CSS declarations for margins and paddings using logical properties (inline/block where applicable).
    /// </summary>
    /// <param name="varPrefix">
    /// A prefix for CSS variables (if the underlying values resolve to variables), or an empty string for none.
    /// </param>
    /// <returns>
    /// A <see cref="string" /> containing CSS declarations for all margin and padding sides, each terminated with a semicolon.
    /// </returns>
    public string ToCss(string? varPrefix = "")
    {
        var builder = new StringBuilder();
        builder.ToCss(MarginTop, "margin-top", varPrefix);
        builder.ToCss(MarginEnd, "margin-inline-end", varPrefix);
        builder.ToCss(MarginBottom, "margin-bottom", varPrefix);
        builder.ToCss(MarginStart, "margin-inline-start", varPrefix);
        builder.ToCss(PaddingTop, "padding-top", varPrefix);
        builder.ToCss(PaddingEnd, "padding-inline-end", varPrefix);
        builder.ToCss(PaddingBottom, "padding-bottom", varPrefix);
        builder.ToCss(PaddingStart, "padding-inline-start", varPrefix);

        return builder.ToString();
    }
}
