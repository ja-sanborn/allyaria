namespace Allyaria.Theming;

public readonly partial struct AllyariaColor
{
    /// <summary>Compares this instance to another color by the canonical uppercase <c>#RRGGBBAA</c> string.</summary>
    /// <param name="other">The other color.</param>
    /// <returns>
    /// A signed integer indicating the relative order. Less than zero if this instance is less; greater than zero if this
    /// instance is greater; zero if equal.
    /// </returns>
    public int CompareTo(AllyariaColor other) => string.Compare(HexRgba, other.HexRgba, StringComparison.Ordinal);

    /// <summary>Determines whether the specified object is equal to the current color.</summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns><c>true</c> if equal; otherwise <c>false</c>.</returns>
    public override bool Equals(object? obj) => obj is AllyariaColor c && Equals(c);

    /// <summary>Determines whether another color is equal to this instance.</summary>
    /// <param name="other">The other color.</param>
    /// <returns><c>true</c> if all channels (including alpha) are equal; otherwise <c>false</c>.</returns>
    public bool Equals(AllyariaColor other)
        => R == other.R && G == other.G && B == other.B && AlphaByte == other.AlphaByte;

    /// <summary>Returns a color from HSVA channels.</summary>
    /// <param name="h">Hue in degrees, clamped to [0..360].</param>
    /// <param name="s">Saturation in percent, clamped to [0..100].</param>
    /// <param name="v">Value (brightness) in percent, clamped to [0..100].</param>
    /// <param name="a">Alpha in [0..1], clamped.</param>
    /// <returns>The AllyariaColor from the HSVA channels.</returns>
    public static AllyariaColor FromHsva(double h, double s, double v, double a = 1.0) => new(h, s, v, a);

    /// <summary>Returns a color from RGBA channels.</summary>
    /// <param name="r">Red in [0..255].</param>
    /// <param name="g">Green in [0..255].</param>
    /// <param name="b">Blue in [0..255].</param>
    /// <param name="a">Alpha in [0..1], clamped.</param>
    /// <returns>The AllyariaColor from the RGBA channels.</returns>
    public static AllyariaColor FromRgba(byte r, byte g, byte b, double a = 1.0) => new(r, g, b, a);

    /// <summary>Returns a hash code suitable for hashing in collections. Uses RGBA (alpha rounded to a byte).</summary>
    public override int GetHashCode() => HashCode.Combine(R, G, B, AlphaByte);

    /// <summary>
    /// Produces a hover-friendly variant: if <see cref="V" /> &lt; 50, lightens by 20; otherwise darkens by 20. Alpha is
    /// preserved.
    /// </summary>
    public AllyariaColor HoverColor()
    {
        var delta = V < 50
            ? 20
            : -20;

        return ShiftColor(delta);
    }

    /// <summary>Adjusts <see cref="V" /> (value/brightness) by the specified percentage.</summary>
    /// <param name="percent">A value in [-100..100]. Positive to lighten; negative to darken.</param>
    /// <returns>A new color with adjusted brightness; alpha is preserved.</returns>
    public AllyariaColor ShiftColor(double percent)
    {
        percent = Clamp(percent, -100, 100);
        RgbToHsv(R, G, B, out var h, out var s, out var v);
        var v2 = Clamp(v + percent, 0, 100);
        HsvToRgb(h, s, v2, out var r2, out var g2, out var b2);

        return new AllyariaColor(r2, g2, b2, A);
    }

    /// <summary>Converts the color to a CSS declaration using the specified property name.</summary>
    /// <param name="name">
    /// The CSS property name (e.g., <c>color</c>, <c>background-color</c>). If <c>null</c> or whitespace, defaults to
    /// <c>color</c>.
    /// </param>
    /// <returns>A declaration string in the form <c>name: #RRGGBBAA;</c>.</returns>
    public string ToCss(string? name = null)
    {
        var prop = string.IsNullOrWhiteSpace(name)
            ? "color"
            : name.Trim();

        return $"{prop}: {HexRgba};";
    }

    /// <summary>Returns the canonical uppercase <c>#RRGGBBAA</c> string for this color.</summary>
    public override string ToString() => HexRgba;
}
