namespace Allyaria.Theming.Constants.Styling;

/// <summary>
/// Provides Material Design–compliant sizing constants based on a 4px/8px grid. These values can be used for consistent
/// margins, paddings, and component dimensions.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssSize
{
    /// <summary>0px — no size.</summary>
    public static readonly StyleValueNumber Size0 = new(value: "0px");

    /// <summary>4px — micro spacing, used sparingly for fine alignment.</summary>
    public static readonly StyleValueNumber Size1 = new(value: "4px");

    /// <summary>72px — extra large spacing step.</summary>
    public static readonly StyleValueNumber Size10 = new(value: "72px");

    /// <summary>80px — extra large spacing step, upper bound of the default scale.</summary>
    public static readonly StyleValueNumber Size11 = new(value: "80px");

    /// <summary>8px — base spacing unit (1 step on Material grid).</summary>
    public static readonly StyleValueNumber Size2 = new(value: "8px");

    /// <summary>16px — default internal padding for many components.</summary>
    public static readonly StyleValueNumber Size3 = new(value: "16px");

    /// <summary>24px — common margin/gutter size on larger layouts.</summary>
    public static readonly StyleValueNumber Size4 = new(value: "24px");

    /// <summary>32px — larger spacing for layout separation.</summary>
    public static readonly StyleValueNumber Size5 = new(value: "32px");

    /// <summary>40px — large step in the spacing scale.</summary>
    public static readonly StyleValueNumber Size6 = new(value: "40px");

    /// <summary>48px — minimum touch target size per accessibility guidance.</summary>
    public static readonly StyleValueNumber Size7 = new(value: "48px");

    /// <summary>56px — commonly used for component heights (e.g., toolbars).</summary>
    public static readonly StyleValueNumber Size8 = new(value: "56px");

    /// <summary>64px — extra large spacing step.</summary>
    public static readonly StyleValueNumber Size9 = new(value: "64px");

    /// <summary>2px — double pixel spacing, usually used for borders.</summary>
    public static readonly StyleValueNumber Thick = new(value: "2px");

    /// <summary>1px — single pixel spacing, usually used for borders.</summary>
    public static readonly StyleValueNumber Thin = new(value: "1px");
}
