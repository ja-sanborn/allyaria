namespace Allyaria.Theming.BrandTypes;

public readonly record struct BrandFont
{
    public BrandFont(string? sansSerif = null,
        string? serif = null,
        string? monospace = null)
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

    public string Monospace { get; }

    public string SansSerif { get; }

    public string Serif { get; }
}
