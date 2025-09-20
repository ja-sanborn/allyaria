using Allyaria.Theming.Styles;

namespace Allyaria.Theming.Typography;

/// <summary>
/// Immutable, validated typography (non-color) settings that can render to CSS. All members are nullable and omitted from
/// CSS when null or whitespace. This struct now composes the strongly typed style structs under
/// <see cref="Allyaria.Theming.Styles" />.
/// </summary>
public readonly record struct AllyariaTypoItem
{
    /// <summary>
    /// Creates a validated, immutable instance. All parameters are optional (nullable). Invalid inputs throw
    /// <see cref="ArgumentException" />. When a parameter is <c>null</c> or whitespace, the corresponding style struct is not
    /// created.
    /// </summary>
    /// <param name="fontFamily">Font family string list.</param>
    /// <param name="fontSize">Font size string.</param>
    /// <param name="fontStyle">Font style string.</param>
    /// <param name="fontWeight">Font weight string.</param>
    /// <param name="letterSpacing">Letter spacing string.</param>
    /// <param name="lineHeight">Line height string.</param>
    /// <param name="textAlign">Text alignment string.</param>
    /// <param name="textDecoration">Text decoration string.</param>
    /// <param name="textTransform">Text transform string.</param>
    /// <param name="verticalAlign">Vertical alignment string.</param>
    /// <param name="wordSpacing">Word spacing string.</param>
    public AllyariaTypoItem(string[]? fontFamily = null,
        string? fontSize = null,
        string? fontStyle = null,
        string? fontWeight = null,
        string? letterSpacing = null,
        string? lineHeight = null,
        string? textAlign = null,
        string? textDecoration = null,
        string? textTransform = null,
        string? verticalAlign = null,
        string? wordSpacing = null)
    {
        if (fontFamily?.Length > 0)
        {
            FontFamily = new AllyariaFontFamily(fontFamily);
        }

        if (FontFamily?.Families.Length is 0)
        {
            FontFamily = null;
        }

        FontSize = !string.IsNullOrWhiteSpace(fontSize)
            ? new AllyariaFontSize(fontSize)
            : null!;

        FontStyle = !string.IsNullOrWhiteSpace(fontStyle)
            ? new AllyariaFontStyle(fontStyle)
            : null!;

        FontWeight = !string.IsNullOrWhiteSpace(fontWeight)
            ? new AllyariaFontWeight(fontWeight)
            : null!;

        LetterSpacing = !string.IsNullOrWhiteSpace(letterSpacing)
            ? new AllyariaLetterSpacing(letterSpacing)
            : null!;

        LineHeight = !string.IsNullOrWhiteSpace(lineHeight)
            ? new AllyariaLineHeight(lineHeight)
            : null!;

        TextAlign = !string.IsNullOrWhiteSpace(textAlign)
            ? new AllyariaTextAlign(textAlign)
            : null!;

        TextDecoration = !string.IsNullOrWhiteSpace(textDecoration)
            ? new AllyariaTextDecoration(textDecoration)
            : null!;

        TextTransform = !string.IsNullOrWhiteSpace(textTransform)
            ? new AllyariaTextTransform(textTransform)
            : null!;

        VerticalAlign = !string.IsNullOrWhiteSpace(verticalAlign)
            ? new AllyariaVerticalAlign(verticalAlign)
            : null!;

        WordSpacing = !string.IsNullOrWhiteSpace(wordSpacing)
            ? new AllyariaWordSpacing(wordSpacing)
            : null!;
    }

    /// <summary>Optional strongly typed font-family style.</summary>
    public AllyariaFontFamily? FontFamily { get; }

    /// <summary>Optional strongly typed font-size style.</summary>
    public AllyariaFontSize? FontSize { get; }

    /// <summary>Optional strongly typed font-style.</summary>
    public AllyariaFontStyle? FontStyle { get; }

    /// <summary>Optional strongly typed font-weight.</summary>
    public AllyariaFontWeight? FontWeight { get; }

    /// <summary>Optional strongly typed letter-spacing style.</summary>
    public AllyariaLetterSpacing? LetterSpacing { get; }

    /// <summary>Optional strongly typed line-height style.</summary>
    public AllyariaLineHeight? LineHeight { get; }

    /// <summary>Optional strongly typed text-align style.</summary>
    public AllyariaTextAlign? TextAlign { get; }

    /// <summary>Optional strongly typed text-decoration style.</summary>
    public AllyariaTextDecoration? TextDecoration { get; init; }

    /// <summary>Optional strongly typed text-transform style.</summary>
    public AllyariaTextTransform? TextTransform { get; }

    /// <summary>Optional strongly typed vertical-align style.</summary>
    public AllyariaVerticalAlign? VerticalAlign { get; }

    /// <summary>Optional strongly typed word-spacing style.</summary>
    public AllyariaWordSpacing? WordSpacing { get; }

    /// <summary>
    /// Produces a single-line CSS declaration string in fixed order, skipping null/whitespace properties. Format:
    /// <c>prop:value;prop:value;</c> (no spaces).
    /// </summary>
    /// <remarks>
    /// Order: font-family, font-size, font-weight, line-height, font-style, text-align, letter-spacing, word-spacing,
    /// text-transform, text-decoration, vertical-align.
    /// </remarks>
    /// <returns>CSS declarations string.</returns>
    public string ToCss()
    {
        var parts = new List<string>(11);

        if (FontFamily is
            { } ff)
        {
            parts.Add(ff.ToCss());
        }

        if (FontSize is
            { } fs)
        {
            parts.Add(fs.ToCss());
        }

        if (FontWeight is
            { } fw)
        {
            parts.Add(fw.ToCss());
        }

        if (LineHeight is
            { } lh)
        {
            parts.Add(lh.ToCss());
        }

        if (FontStyle is
            { } fst)
        {
            parts.Add(fst.ToCss());
        }

        if (TextAlign is
            { } ta)
        {
            parts.Add(ta.ToCss());
        }

        if (LetterSpacing is
            { } ls)
        {
            parts.Add(ls.ToCss());
        }

        if (WordSpacing is
            { } ws)
        {
            parts.Add(ws.ToCss());
        }

        if (TextTransform is
            { } tt)
        {
            parts.Add(tt.ToCss());
        }

        if (TextDecoration is
            { } td)
        {
            parts.Add(td.ToCss());
        }

        if (VerticalAlign is
            { } va)
        {
            parts.Add(va.ToCss());
        }

        return string.Concat(parts);
    }

    /// <summary>Renders the current typography state as a CSS declaration string.</summary>
    /// <returns>CSS declarations suitable for inclusion in a <c>style</c> attribute or block.</returns>
    public override string ToString() => ToCss();
}
