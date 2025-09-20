using System.Globalization;

namespace Allyaria.Theming.Styles;

public readonly partial struct AllyariaColor
{
    /// <summary>Gets the alpha channel as a unit value in the range [0..1].</summary>
    public double A { get; }

    /// <summary>Gets the blue channel in the range [0..255].</summary>
    public byte B { get; }

    /// <summary>Gets the green channel in the range [0..255].</summary>
    public byte G { get; }

    /// <summary>Gets the hue in degrees in the range [0..360].</summary>
    /// <remarks>The value is computed from the underlying RGB channels.</remarks>
    public double H
    {
        get
        {
            RgbToHsv(R, G, B, out var h, out _, out _);

            return h;
        }
    }

    /// <summary>Gets the uppercase <c>#RRGGBB</c> representation of the color (alpha omitted).</summary>
    public string HexRgb => $"#{R:X2}{G:X2}{B:X2}";

    /// <summary>Gets the uppercase <c>#RRGGBBAA</c> representation of the color (alpha included).</summary>
    public string HexRgba => $"#{R:X2}{G:X2}{B:X2}{AlphaByte:X2}";

    /// <summary>Gets the <c>hsv(H, S%, V%)</c> representation using invariant culture.</summary>
    public string Hsv
    {
        get
        {
            RgbToHsv(R, G, B, out var h, out var s, out var v);

            return string.Create(CultureInfo.InvariantCulture, $"hsv({h:0.##}, {s:0.##}%, {v:0.##}%)");
        }
    }

    /// <summary>Gets the <c>hsva(H, S%, V%, A)</c> representation using invariant culture.</summary>
    public string Hsva
    {
        get
        {
            RgbToHsv(R, G, B, out var h, out var s, out var v);

            return string.Create(CultureInfo.InvariantCulture, $"hsva({h:0.##}, {s:0.##}%, {v:0.##}%, {A:0.###})");
        }
    }

    /// <summary>Gets the red channel in the range [0..255].</summary>
    public byte R { get; }

    /// <summary>Gets the <c>rgb(r, g, b)</c> representation.</summary>
    public string Rgb => $"rgb({R}, {G}, {B})";

    /// <summary>
    /// Gets the <c>rgba(r, g, b, a)</c> representation using invariant culture, where <c>a</c> is shown in [0..1].
    /// </summary>
    public string Rgba => string.Create(CultureInfo.InvariantCulture, $"rgba({R}, {G}, {B}, {A:0.###})");

    /// <summary>Gets the saturation in percent in the range [0..100].</summary>
    /// <remarks>The value is computed from the underlying RGB channels.</remarks>
    public double S
    {
        get
        {
            RgbToHsv(R, G, B, out _, out var s, out _);

            return s;
        }
    }

    /// <summary>Gets the value (brightness) in percent in the range [0..100].</summary>
    /// <remarks>The value is computed from the underlying RGB channels.</remarks>
    public double V
    {
        get
        {
            RgbToHsv(R, G, B, out _, out _, out var v);

            return v;
        }
    }
}
