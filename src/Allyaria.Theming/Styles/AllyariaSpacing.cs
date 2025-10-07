using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using System.Text;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents logical spacing (margins and paddings) for a themable element, providing strongly typed values and helpers
/// to compute CSS declarations.
/// </summary>
/// <remarks>
/// Defaults are taken from <see cref="StyleDefaults" /> when constructor arguments are not provided. This type is
/// immutable (readonly record struct) and safe for value semantics.
/// </remarks>
public readonly record struct AllyariaSpacing
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaSpacing" /> struct using all default margin and padding values
    /// from <see cref="StyleDefaults" />.
    /// </summary>
    public AllyariaSpacing()
        : this(null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaSpacing" /> struct, applying provided values or falling back to
    /// <see cref="StyleDefaults.Margin" /> and <see cref="StyleDefaults.Padding" /> when a value is not supplied.
    /// </summary>
    /// <param name="marginTop">The margin on the top side, or <c>null</c> to use the default margin.</param>
    /// <param name="marginRight">The margin on the right side, or <c>null</c> to use the default margin.</param>
    /// <param name="marginBottom">The margin on the bottom side, or <c>null</c> to use the default margin.</param>
    /// <param name="marginLeft">The margin on the left side, or <c>null</c> to use the default margin.</param>
    /// <param name="paddingTop">The padding on the top side, or <c>null</c> to use the default padding.</param>
    /// <param name="paddingRight">The padding on the right side, or <c>null</c> to use the default padding.</param>
    /// <param name="paddingBottom">The padding on the bottom side, or <c>null</c> to use the default padding.</param>
    /// <param name="paddingLeft">The padding on the left side, or <c>null</c> to use the default padding.</param>
    public AllyariaSpacing(AllyariaNumberValue? marginTop = null,
        AllyariaNumberValue? marginRight = null,
        AllyariaNumberValue? marginBottom = null,
        AllyariaNumberValue? marginLeft = null,
        AllyariaNumberValue? paddingTop = null,
        AllyariaNumberValue? paddingRight = null,
        AllyariaNumberValue? paddingBottom = null,
        AllyariaNumberValue? paddingLeft = null)
    {
        MarginTop = marginTop ?? StyleDefaults.Margin;
        MarginRight = marginRight ?? StyleDefaults.Margin;
        MarginBottom = marginBottom ?? StyleDefaults.Margin;
        MarginLeft = marginLeft ?? StyleDefaults.Margin;
        PaddingTop = paddingTop ?? StyleDefaults.Padding;
        PaddingRight = paddingRight ?? StyleDefaults.Padding;
        PaddingBottom = paddingBottom ?? StyleDefaults.Padding;
        PaddingLeft = paddingLeft ?? StyleDefaults.Padding;
    }

    /// <summary>Gets or initializes the margin on the bottom side.</summary>
    public AllyariaNumberValue MarginBottom { get; init; }

    /// <summary>Gets or initializes the margin on the left side.</summary>
    public AllyariaNumberValue MarginLeft { get; init; }

    /// <summary>Gets or initializes the margin on the right side.</summary>
    public AllyariaNumberValue MarginRight { get; init; }

    /// <summary>Gets or initializes the margin on the top side.</summary>
    public AllyariaNumberValue MarginTop { get; init; }

    /// <summary>Gets or initializes the padding on the bottom side.</summary>
    public AllyariaNumberValue PaddingBottom { get; init; }

    /// <summary>Gets or initializes the padding on the left side.</summary>
    public AllyariaNumberValue PaddingLeft { get; init; }

    /// <summary>Gets or initializes the padding on the right side.</summary>
    public AllyariaNumberValue PaddingRight { get; init; }

    /// <summary>Gets or initializes the padding on the top side.</summary>
    public AllyariaNumberValue PaddingTop { get; init; }

    /// <summary>
    /// Returns a copy of this instance with the provided values overriding existing ones. Any argument left as <c>null</c>
    /// preserves the corresponding current value.
    /// </summary>
    /// <param name="marginTop">Optional new top margin.</param>
    /// <param name="marginRight">Optional new right margin.</param>
    /// <param name="marginBottom">Optional new bottom margin.</param>
    /// <param name="marginLeft">Optional new left margin.</param>
    /// <param name="paddingTop">Optional new top padding.</param>
    /// <param name="paddingRight">Optional new right padding.</param>
    /// <param name="paddingBottom">Optional new bottom padding.</param>
    /// <param name="paddingLeft">Optional new left padding.</param>
    /// <returns>A new <see cref="AllyariaSpacing" /> with supplied values applied and unspecified values preserved.</returns>
    public AllyariaSpacing Cascade(AllyariaNumberValue? marginTop = null,
        AllyariaNumberValue? marginRight = null,
        AllyariaNumberValue? marginBottom = null,
        AllyariaNumberValue? marginLeft = null,
        AllyariaNumberValue? paddingTop = null,
        AllyariaNumberValue? paddingRight = null,
        AllyariaNumberValue? paddingBottom = null,
        AllyariaNumberValue? paddingLeft = null)
        => this with
        {
            MarginTop = marginTop ?? MarginTop,
            MarginRight = marginRight ?? MarginRight,
            MarginBottom = marginBottom ?? MarginBottom,
            MarginLeft = marginLeft ?? MarginLeft,
            PaddingTop = paddingTop ?? PaddingTop,
            PaddingRight = paddingRight ?? PaddingRight,
            PaddingBottom = paddingBottom ?? PaddingBottom,
            PaddingLeft = paddingLeft ?? PaddingLeft
        };

    /// <summary>
    /// Creates an <see cref="AllyariaSpacing" /> where all margins use the same value and all paddings use the same value.
    /// </summary>
    /// <param name="margin">The margin value to apply to all sides.</param>
    /// <param name="padding">The padding value to apply to all sides.</param>
    /// <returns>A new <see cref="AllyariaSpacing" /> instance with uniform margins and paddings.</returns>
    public static AllyariaSpacing FromSingle(AllyariaNumberValue margin, AllyariaNumberValue padding)
        => new(margin, margin, margin, margin, padding, padding, padding, padding);

    /// <summary>
    /// Creates an <see cref="AllyariaSpacing" /> with symmetric horizontal and vertical margins and paddings.
    /// </summary>
    /// <param name="marginHorizontal">The margin value for left and right sides.</param>
    /// <param name="marginVertical">The margin value for top and bottom sides.</param>
    /// <param name="paddingHorizontal">The padding value for left and right sides.</param>
    /// <param name="paddingVertical">The padding value for top and bottom sides.</param>
    /// <returns>A new <see cref="AllyariaSpacing" /> instance with symmetric spacing.</returns>
    public static AllyariaSpacing FromSymmetric(AllyariaNumberValue marginHorizontal,
        AllyariaNumberValue marginVertical,
        AllyariaNumberValue paddingHorizontal,
        AllyariaNumberValue paddingVertical)
        => new(
            marginVertical, marginHorizontal, marginVertical, marginHorizontal, paddingVertical, paddingHorizontal,
            paddingVertical, paddingHorizontal
        );

    /// <summary>Builds CSS declarations for margins and paddings using physical properties.</summary>
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
        builder.ToCss(MarginRight, "margin-right", varPrefix);
        builder.ToCss(MarginBottom, "margin-bottom", varPrefix);
        builder.ToCss(MarginLeft, "margin-left", varPrefix);
        builder.ToCss(PaddingTop, "padding-top", varPrefix);
        builder.ToCss(PaddingRight, "padding-right", varPrefix);
        builder.ToCss(PaddingBottom, "padding-bottom", varPrefix);
        builder.ToCss(PaddingLeft, "padding-left", varPrefix);

        return builder.ToString();
    }
}
