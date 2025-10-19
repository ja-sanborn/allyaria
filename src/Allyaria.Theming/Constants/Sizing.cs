namespace Allyaria.Theming.Constants;

/// <summary>
/// Provides Material Design–compliant sizing constants based on a 4px/8px grid. These values can be used for consistent
/// margins, paddings, and component dimensions.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class Sizing
{
    /// <summary>0px — no size.</summary>
    public static readonly ThemeNumber Size0 = new("0px");

    /// <summary>4px — micro spacing, used sparingly for fine alignment.</summary>
    public static readonly ThemeNumber Size1 = new("4px");

    /// <summary>72px — extra large spacing step.</summary>
    public static readonly ThemeNumber Size10 = new("72px");

    /// <summary>80px — extra large spacing step, upper bound of the default scale.</summary>
    public static readonly ThemeNumber Size11 = new("80px");

    /// <summary>8px — base spacing unit (1 step on Material grid).</summary>
    public static readonly ThemeNumber Size2 = new("8px");

    /// <summary>16px — default internal padding for many components.</summary>
    public static readonly ThemeNumber Size3 = new("16px");

    /// <summary>24px — common margin/gutter size on larger layouts.</summary>
    public static readonly ThemeNumber Size4 = new("24px");

    /// <summary>32px — larger spacing for layout separation.</summary>
    public static readonly ThemeNumber Size5 = new("32px");

    /// <summary>40px — large step in the spacing scale.</summary>
    public static readonly ThemeNumber Size6 = new("40px");

    /// <summary>48px — minimum touch target size per accessibility guidance.</summary>
    public static readonly ThemeNumber Size7 = new("48px");

    /// <summary>56px — commonly used for component heights (e.g., toolbars).</summary>
    public static readonly ThemeNumber Size8 = new("56px");

    /// <summary>64px — extra large spacing step.</summary>
    public static readonly ThemeNumber Size9 = new("64px");

    /// <summary>2px — double pixel spacing, usually used for borders.</summary>
    public static readonly ThemeNumber Thick = new("2px");

    /// <summary>1px — single pixel spacing, usually used for borders.</summary>
    public static readonly ThemeNumber Thin = new("1px");
}
