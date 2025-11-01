namespace Allyaria.Theming.Types;

public readonly record struct Brand
{
    public Brand(BrandFont? font = null, BrandTheme? lightTheme = null, BrandTheme? darkTheme = null)
    {
        Font = font ?? new BrandFont();
        Variant = new BrandVariant(lightTheme: lightTheme, darkTheme: darkTheme);
    }

    public BrandFont Font { get; }

    public BrandVariant Variant { get; }
}
