namespace Allyaria.Theming.Enumerations;

/// <summary>Defines units of length measurement for styling and layout.</summary>
public enum LengthUnits
{
    /// <summary>Character width unit (<c>ch</c>), based on the width of the "0" glyph.</summary>
    [Description(description: "ch")]
    Character,

    /// <summary>Centimeters (<c>cm</c>).</summary>
    [Description(description: "cm")]
    Centimeter,

    /// <summary>Container block size (<c>cqb</c>), relative to container’s block dimension.</summary>
    [Description(description: "cqb")]
    ContainerBlock,

    /// <summary>Container height (<c>cqh</c>), relative to the container’s height (block size).</summary>
    [Description(description: "cqh")]
    ContainerHeight,

    /// <summary>Container inline size (<c>cqi</c>).</summary>
    [Description(description: "cqi")]
    ContainerInline,

    /// <summary>Container width (<c>cqw</c>).</summary>
    [Description(description: "cqw")]
    ContainerWidth,

    /// <summary>Maximum container size (<c>cqmax</c>).</summary>
    [Description(description: "cqmax")]
    ContainerMax,

    /// <summary>Minimum container size (<c>cqmin</c>).</summary>
    [Description(description: "cqmin")]
    ContainerMin,

    /// <summary>Em size (<c>em</c>), relative to the font size of the element.</summary>
    [Description(description: "em")]
    Em,

    /// <summary>Ex size (<c>ex</c>), relative to the x-height of the font.</summary>
    [Description(description: "ex")]
    Ex,

    /// <summary>Fractional unit (<c>fr</c>), used in grid layouts.</summary>
    [Description(description: "fr")]
    Fraction,

    /// <summary>Inches (<c>in</c>).</summary>
    [Description(description: "in")]
    Inch,

    /// <summary>Line height (<c>lh</c>) of the element.</summary>
    [Description(description: "lh")]
    LineHeight,

    /// <summary>Millimeters (<c>mm</c>).</summary>
    [Description(description: "mm")]
    Millimeter,

    /// <summary>Picas (<c>pc</c>).</summary>
    [Description(description: "pc")]
    Pica,

    /// <summary>Points (<c>pt</c>).</summary>
    [Description(description: "pt")]
    Point,

    /// <summary>Percentage (<c>%</c>), relative to parent element or context.</summary>
    [Description(description: "%")]
    Percent,

    /// <summary>Pixels (<c>px</c>).</summary>
    [Description(description: "px")]
    Pixel,

    /// <summary>Quarter-millimeters (<c>Q</c>).</summary>
    [Description(description: "Q")]
    QuarterMillimeter,

    /// <summary>Root em (<c>rem</c>), relative to the font size of the root element.</summary>
    [Description(description: "rem")]
    RootEm,

    /// <summary>Root line height (<c>rlh</c>), relative to the root element’s line height.</summary>
    [Description(description: "rlh")]
    RootLineHeight,

    /// <summary>Viewport block size (<c>vb</c>).</summary>
    [Description(description: "vb")]
    ViewportBlock,

    /// <summary>Viewport height (<c>vh</c>), relative to 1% of the viewport height.</summary>
    [Description(description: "vh")]
    ViewportHeight,

    /// <summary>Viewport inline size (<c>vi</c>).</summary>
    [Description(description: "vi")]
    ViewportInline,

    /// <summary>Larger of viewport height or width (<c>vmax</c>).</summary>
    [Description(description: "vmax")]
    ViewportMax,

    /// <summary>Smaller of viewport height or width (<c>vmin</c>).</summary>
    [Description(description: "vmin")]
    ViewportMin,

    /// <summary>Viewport width (<c>vw</c>), relative to 1% of the viewport width.</summary>
    [Description(description: "vw")]
    ViewportWidth
}
