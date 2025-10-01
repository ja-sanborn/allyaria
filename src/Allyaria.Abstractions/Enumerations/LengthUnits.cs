using System.ComponentModel;

namespace Allyaria.Abstractions.Enumerations;

/// <summary>Defines units of length measurement for styling and layout.</summary>
public enum LengthUnits
{
    /// <summary>Character width unit (<c>ch</c>), based on the width of the "0" glyph.</summary>
    [Description("ch")]
    Character,

    /// <summary>Centimeters (<c>cm</c>).</summary>
    [Description("cm")]
    Centimeter,

    /// <summary>Container block size (<c>cqb</c>), relative to container’s block dimension.</summary>
    [Description("cqb")]
    ContainerBlock,

    /// <summary>Container height (<c>cqh</c>), relative to the container’s height (block size).</summary>
    [Description("cqh")]
    ContainerHeight,

    /// <summary>Container inline size (<c>cqi</c>).</summary>
    [Description("cqi")]
    ContainerInline,

    /// <summary>Container width (<c>cqw</c>).</summary>
    [Description("cqw")]
    ContainerWidth,

    /// <summary>Maximum container size (<c>cqmax</c>).</summary>
    [Description("cqmax")]
    ContainerMax,

    /// <summary>Minimum container size (<c>cqmin</c>).</summary>
    [Description("cqmin")]
    ContainerMin,

    /// <summary>Em size (<c>em</c>), relative to the font size of the element.</summary>
    [Description("em")]
    Em,

    /// <summary>Ex size (<c>ex</c>), relative to the x-height of the font.</summary>
    [Description("ex")]
    Ex,

    /// <summary>Fractional unit (<c>fr</c>), used in grid layouts.</summary>
    [Description("fr")]
    Fraction,

    /// <summary>Inches (<c>in</c>).</summary>
    [Description("in")]
    Inch,

    /// <summary>Line height (<c>lh</c>) of the element.</summary>
    [Description("lh")]
    LineHeight,

    /// <summary>Millimeters (<c>mm</c>).</summary>
    [Description("mm")]
    Millimeter,

    /// <summary>Picas (<c>pc</c>).</summary>
    [Description("pc")]
    Pica,

    /// <summary>Points (<c>pt</c>).</summary>
    [Description("pt")]
    Point,

    /// <summary>Percentage (<c>%</c>), relative to parent element or context.</summary>
    [Description("%")]
    Percent,

    /// <summary>Pixels (<c>px</c>).</summary>
    [Description("px")]
    Pixel,

    /// <summary>Quarter-millimeters (<c>Q</c>).</summary>
    [Description("Q")]
    QuarterMillimeter,

    /// <summary>Root em (<c>rem</c>), relative to the font size of the root element.</summary>
    [Description("rem")]
    RootEm,

    /// <summary>Root line height (<c>rlh</c>), relative to the root element’s line height.</summary>
    [Description("rlh")]
    RootLineHeight,

    /// <summary>Viewport block size (<c>vb</c>).</summary>
    [Description("vb")]
    ViewportBlock,

    /// <summary>Viewport height (<c>vh</c>), relative to 1% of the viewport height.</summary>
    [Description("vh")]
    ViewportHeight,

    /// <summary>Viewport inline size (<c>vi</c>).</summary>
    [Description("vi")]
    ViewportInline,

    /// <summary>Larger of viewport height or width (<c>vmax</c>).</summary>
    [Description("vmax")]
    ViewportMax,

    /// <summary>Smaller of viewport height or width (<c>vmin</c>).</summary>
    [Description("vmin")]
    ViewportMin,

    /// <summary>Viewport width (<c>vw</c>), relative to 1% of the viewport width.</summary>
    [Description("vw")]
    ViewportWidth
}
