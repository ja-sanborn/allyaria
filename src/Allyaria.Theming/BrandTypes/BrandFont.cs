namespace Allyaria.Theming.BrandTypes;

/// <summary>
/// Represents the font configuration for a brand, providing distinct font families for sans-serif, serif, and monospace
/// text rendering.
/// </summary>
public sealed record BrandFont
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BrandFont" /> struct with optional sans-serif, serif, and monospace font
    /// family names.
    /// </summary>
    /// <param name="sansSerif">
    /// The sans-serif font family name to use. Defaults to <see cref="StyleDefaults.SansSerifFont" /> if null or whitespace.
    /// </param>
    /// <param name="serif">
    /// The serif font family name to use. Defaults to <see cref="StyleDefaults.SerifFont" /> if null or whitespace.
    /// </param>
    /// <param name="monospace">
    /// The monospace font family name to use. Defaults to <see cref="StyleDefaults.MonospaceFont" /> if null or whitespace.
    /// </param>
    public BrandFont(string? sansSerif = null, string? serif = null, string? monospace = null)
    {
        var setMonospace = string.IsNullOrWhiteSpace(value: monospace)
            ? StyleDefaults.MonospaceFont
            : monospace.Trim();

        var setSansSerif = string.IsNullOrWhiteSpace(value: sansSerif)
            ? StyleDefaults.SansSerifFont
            : sansSerif.Trim();

        var setSerif = string.IsNullOrWhiteSpace(value: serif)
            ? StyleDefaults.SerifFont
            : serif.Trim();

        Monospace = setMonospace;
        SansSerif = setSansSerif;
        Serif = setSerif;
    }

    /// <summary>Gets the monospace font family name used for fixed-width text.</summary>
    public string Monospace { get; }

    /// <summary>Gets the sans-serif font family name used for general text rendering.</summary>
    public string SansSerif { get; }

    /// <summary>Gets the serif font family name used for formal or decorative text rendering.</summary>
    public string Serif { get; }
}
