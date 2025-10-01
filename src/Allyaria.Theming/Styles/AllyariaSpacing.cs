using Allyaria.Theming.Values;
using System.Text;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Strongly typed spacing definition (margins and paddings) for Allyaria theming. Each side is represented by an optional
/// <see cref="AllyariaNumberValue" /> (e.g., <c>16px</c>, <c>1rem</c>, <c>8%</c>). When a side is <see langword="null" />,
/// it is considered unset and will not be emitted by <see cref="ToCss" /> or <see cref="ToCssVars(string)" />. Supports
/// non-destructive overrides via <see cref="Cascade" />.
/// </summary>
public readonly record struct AllyariaSpacing
{
    /// <summary>
    /// Initializes a new <see cref="AllyariaSpacing" /> with optional per-side margins and paddings in TRBL order. Pass
    /// <see langword="null" /> for any side to leave it unset (it will not be emitted by <see cref="ToCss" /> or
    /// <see cref="ToCssVars(string)" />).
    /// </summary>
    /// <param name="marginTop">Optional margin-top.</param>
    /// <param name="marginRight">Optional margin-right.</param>
    /// <param name="marginBottom">Optional margin-bottom.</param>
    /// <param name="marginLeft">Optional margin-left.</param>
    /// <param name="paddingTop">Optional padding-top.</param>
    /// <param name="paddingRight">Optional padding-right.</param>
    /// <param name="paddingBottom">Optional padding-bottom.</param>
    /// <param name="paddingLeft">Optional padding-left.</param>
    public AllyariaSpacing(AllyariaNumberValue? marginTop = null,
        AllyariaNumberValue? marginRight = null,
        AllyariaNumberValue? marginBottom = null,
        AllyariaNumberValue? marginLeft = null,
        AllyariaNumberValue? paddingTop = null,
        AllyariaNumberValue? paddingRight = null,
        AllyariaNumberValue? paddingBottom = null,
        AllyariaNumberValue? paddingLeft = null)
    {
        MarginTop = marginTop;
        MarginRight = marginRight;
        MarginBottom = marginBottom;
        MarginLeft = marginLeft;
        PaddingTop = paddingTop;
        PaddingRight = paddingRight;
        PaddingBottom = paddingBottom;
        PaddingLeft = paddingLeft;
    }

    /// <summary>Gets or initializes the margin on the bottom side, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? MarginBottom { get; init; }

    /// <summary>Gets or initializes the margin on the left side, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? MarginLeft { get; init; }

    /// <summary>Gets or initializes the margin on the right side, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? MarginRight { get; init; }

    /// <summary>Gets or initializes the margin on the top side, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? MarginTop { get; init; }

    /// <summary>Gets or initializes the padding on the bottom side, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? PaddingBottom { get; init; }

    /// <summary>Gets or initializes the padding on the left side, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? PaddingLeft { get; init; }

    /// <summary>Gets or initializes the padding on the right side, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? PaddingRight { get; init; }

    /// <summary>Gets or initializes the padding on the top side, or <see langword="null" /> when unset.</summary>
    public AllyariaNumberValue? PaddingTop { get; init; }

    /// <summary>Appends a CSS declaration to the builder when the provided value is non-null.</summary>
    /// <param name="builder">The destination <see cref="StringBuilder" />.</param>
    /// <param name="value">The optional <see cref="AllyariaNumberValue" /> to emit.</param>
    /// <param name="propertyName">The CSS property name (e.g., <c>margin-top</c>).</param>
    private static void AppendIfNotNull(StringBuilder builder, AllyariaNumberValue? value, string propertyName)
    {
        if (value is not null)
        {
            builder.Append(value.ToCss(propertyName));
        }
    }

    /// <summary>
    /// Returns a new spacing definition by applying per-side overrides in TRBL order for both margin and padding. Each
    /// parameter is optional; when <see langword="null" />, the existing value for that side is kept (which may also be
    /// <see langword="null" />).
    /// </summary>
    /// <param name="marginTop">Optional new margin-top.</param>
    /// <param name="marginRight">Optional new margin-right.</param>
    /// <param name="marginBottom">Optional new margin-bottom.</param>
    /// <param name="marginLeft">Optional new margin-left.</param>
    /// <param name="paddingTop">Optional new padding-top.</param>
    /// <param name="paddingRight">Optional new padding-right.</param>
    /// <param name="paddingBottom">Optional new padding-bottom.</param>
    /// <param name="paddingLeft">Optional new padding-left.</param>
    /// <returns>A new <see cref="AllyariaSpacing" /> with the applied overrides.</returns>
    public AllyariaSpacing Cascade(AllyariaNumberValue? marginTop = null,
        AllyariaNumberValue? marginRight = null,
        AllyariaNumberValue? marginBottom = null,
        AllyariaNumberValue? marginLeft = null,
        AllyariaNumberValue? paddingTop = null,
        AllyariaNumberValue? paddingRight = null,
        AllyariaNumberValue? paddingBottom = null,
        AllyariaNumberValue? paddingLeft = null)
        => new()
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
    /// Builds a CSS style string (e.g., <c>margin-top:8px;padding-right:12px;</c>). Only non-<see langword="null" /> sides are
    /// emitted.
    /// </summary>
    /// <returns>A concatenated CSS declaration string suitable for an inline <c>style</c> attribute.</returns>
    public string ToCss()
    {
        var sb = new StringBuilder();

        AppendIfNotNull(sb, MarginTop, "margin-top");
        AppendIfNotNull(sb, MarginRight, "margin-right");
        AppendIfNotNull(sb, MarginBottom, "margin-bottom");
        AppendIfNotNull(sb, MarginLeft, "margin-left");
        AppendIfNotNull(sb, PaddingTop, "padding-top");
        AppendIfNotNull(sb, PaddingRight, "padding-right");
        AppendIfNotNull(sb, PaddingBottom, "padding-bottom");
        AppendIfNotNull(sb, PaddingLeft, "padding-left");

        return sb.ToString();
    }

    /// <summary>
    /// Builds a CSS custom-properties string for this spacing. The optional <paramref name="prefix" /> is normalized by
    /// collapsing runs of whitespace/dashes, trimming leading/trailing dashes, and lowercasing. If empty, the default
    /// <c>--aa-</c> prefix is used; otherwise variables emit as <c>--{prefix}-margin-top</c>, etc. Only non-null sides are
    /// emitted.
    /// </summary>
    /// <param name="prefix">Optional namespace for variables (e.g., <c>editor</c>).</param>
    /// <returns>A CSS variables string containing only non-null sides.</returns>
    public string ToCssVars(string prefix = "")
    {
        var basePrefix = Regex.Replace(prefix, @"[\s-]+", "-").Trim('-').ToLowerInvariant();

        basePrefix = string.IsNullOrWhiteSpace(basePrefix)
            ? "--aa-"
            : $"--{basePrefix}-";

        var sb = new StringBuilder();

        AppendIfNotNull(sb, MarginTop, $"{basePrefix}margin-top");
        AppendIfNotNull(sb, MarginRight, $"{basePrefix}margin-right");
        AppendIfNotNull(sb, MarginBottom, $"{basePrefix}margin-bottom");
        AppendIfNotNull(sb, MarginLeft, $"{basePrefix}margin-left");
        AppendIfNotNull(sb, PaddingTop, $"{basePrefix}padding-top");
        AppendIfNotNull(sb, PaddingRight, $"{basePrefix}padding-right");
        AppendIfNotNull(sb, PaddingBottom, $"{basePrefix}padding-bottom");
        AppendIfNotNull(sb, PaddingLeft, $"{basePrefix}padding-left");

        return sb.ToString();
    }
}
