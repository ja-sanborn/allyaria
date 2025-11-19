namespace Allyaria.Theming.Enumerations;

/// <summary>Defines logical groups of related CSS style properties used by the Allyaria theming engine.</summary>
/// <remarks>
/// Each group represents a set of semantically related CSS properties applied to a specific style aspect such as borders,
/// margins, or padding. These groupings support logical styling operations using Blazor’s CSS isolation and theme
/// application mechanisms.
/// </remarks>
public enum StyleGroupType
{
    /// <summary>
    /// Represents the logical group of all border color properties for block and inline directions.
    /// <para>
    /// Includes: <c>border-block-end-color</c>, <c>border-block-start-color</c>, <c>border-inline-end-color</c>, and
    /// <c>border-inline-start-color</c>.
    /// </para>
    /// </summary>
    [Description(
        description: "border-block-end-color,border-block-start-color,border-inline-end-color,border-inline-start-color"
    )]
    BorderColor,

    /// <summary>
    /// Represents the logical group of all border radius properties for block and inline corners.
    /// <para>
    /// Includes: <c>border-end-end-radius</c>, <c>border-end-start-radius</c>, <c>border-start-end-radius</c>, and
    /// <c>border-start-start-radius</c>.
    /// </para>
    /// </summary>
    [Description(
        description: "border-end-end-radius,border-end-start-radius,border-start-end-radius,border-start-start-radius"
    )]
    BorderRadius,

    /// <summary>
    /// Represents the logical group of all border style properties for block and inline sides.
    /// <para>
    /// Includes: <c>border-block-end-style</c>, <c>border-block-start-style</c>, <c>border-inline-end-style</c>, and
    /// <c>border-inline-start-style</c>.
    /// </para>
    /// </summary>
    [Description(
        description: "border-block-end-style,border-block-start-style,border-inline-end-style,border-inline-start-style"
    )]
    BorderStyle,

    /// <summary>
    /// Represents the logical group of all border width properties for block and inline sides.
    /// <para>
    /// Includes: <c>border-block-end-width</c>, <c>border-block-start-width</c>, <c>border-inline-end-width</c>, and
    /// <c>border-inline-start-width</c>.
    /// </para>
    /// </summary>
    [Description(
        description: "border-block-end-width,border-block-start-width,border-inline-end-width,border-inline-start-width"
    )]
    BorderWidth,

    /// <summary>
    /// Represents the logical group of all margin properties in both block and inline directions.
    /// <para>
    /// Includes: <c>margin-block-end</c>, <c>margin-block-start</c>, <c>margin-inline-end</c>, and <c>margin-inline-start</c>.
    /// </para>
    /// </summary>
    [Description(description: "margin-block-end,margin-block-start,margin-inline-end,margin-inline-start")]
    Margin,

    /// <summary>
    /// Represents the logical group of all padding properties in both block and inline directions.
    /// <para>
    /// Includes: <c>padding-block-end</c>, <c>padding-block-start</c>, <c>padding-inline-end</c>, and
    /// <c>padding-inline-start</c>.
    /// </para>
    /// </summary>
    [Description(description: "padding-block-end,padding-block-start,padding-inline-end,padding-inline-start")]
    Padding
}
