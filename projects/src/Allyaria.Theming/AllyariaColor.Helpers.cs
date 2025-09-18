using System.Globalization;

namespace Allyaria.Theming;

public readonly partial struct AllyariaColor
{
    /// <summary>Gets the alpha component rendered as a byte in the range <c>[0..255]</c>.</summary>
    /// <remarks>
    /// The underlying <see cref="A" /> value (unit interval) is multiplied by 255 and rounded using
    /// <see cref="MidpointRounding.AwayFromZero" />; the result is then clamped to <c>[0..255]</c>.
    /// </remarks>
    internal byte AlphaByte => (byte)Math.Clamp((int)Math.Round(A * 255.0, MidpointRounding.AwayFromZero), 0, 255);

    /// <summary>Clamps a value to the inclusive range <paramref name="min" /> … <paramref name="max" />.</summary>
    /// <param name="v">The input value.</param>
    /// <param name="min">The minimum allowed value (inclusive).</param>
    /// <param name="max">The maximum allowed value (inclusive).</param>
    /// <returns>The clamped value.</returns>
    internal static double Clamp(double v, double min, double max) => Math.Clamp(v, min, max);

    /// <summary>Clamps a value to the unit interval <c>[0..1]</c>.</summary>
    /// <param name="v">The input value.</param>
    /// <returns><paramref name="v" /> clamped to <c>[0..1]</c>.</returns>
    internal static double Clamp01(double v) => Math.Clamp(v, 0.0, 1.0);

    /// <summary>Clamps an integer to the byte range <c>[0..255]</c> and casts to <see cref="byte" />.</summary>
    /// <param name="v">The integer value to clamp.</param>
    /// <returns>The clamped byte.</returns>
    internal static byte ClampByte(int v) => (byte)Math.Clamp(v, 0, 255);

    /// <summary>Parses a hexadecimal CSS color literal.</summary>
    /// <param name="s">A hex color string of the form <c>#RGB</c>, <c>#RGBA</c>, <c>#RRGGBB</c>, or <c>#RRGGBBAA</c>.</param>
    /// <param name="r">Outputs the red channel (0–255).</param>
    /// <param name="g">Outputs the green channel (0–255).</param>
    /// <param name="b">Outputs the blue channel (0–255).</param>
    /// <param name="a">Outputs the alpha channel (0–1).</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="s" /> is not a supported hex format or does not begin
    /// with <c>#</c>.
    /// </exception>
    internal static void FromHex(string s, out byte r, out byte g, out byte b, out double a)
    {
        var hex = s.Trim();

        if (!hex.StartsWith("#", StringComparison.Ordinal))
        {
            throw new ArgumentException($"Invalid hex color: '{s}'.", nameof(s));
        }

        hex = hex[1..];

        if (hex.Length is 3 or 4)
        {
            // #RGB or #RGBA -> expand nibbles
            var r1 = ToHexNibble(hex[0]);
            var g1 = ToHexNibble(hex[1]);
            var b1 = ToHexNibble(hex[2]);
            r = (byte)(r1 * 17);
            g = (byte)(g1 * 17);
            b = (byte)(b1 * 17);

            a = hex.Length == 4
                ? Clamp01(ToHexNibble(hex[3]) * 17 / 255.0)
                : 1.0;

            return;
        }

        if (hex.Length is 6 or 8)
        {
            r = Convert.ToByte(hex.Substring(0, 2), 16);
            g = Convert.ToByte(hex.Substring(2, 2), 16);
            b = Convert.ToByte(hex.Substring(4, 2), 16);

            a = hex.Length == 8
                ? Convert.ToByte(hex.Substring(6, 2), 16) / 255.0
                : 1.0;

            return;
        }

        throw new ArgumentException($"Hex color must be #RGB, #RGBA, #RRGGBB, or #RRGGBBAA: '{s}'.", nameof(s));
    }

    /// <summary>Creates an <see cref="AllyariaColor" /> from a hex literal (helper used by color tables).</summary>
    /// <param name="hex">A hex color string of the form <c>#RRGGBB</c> or <c>#RRGGBBAA</c>.</param>
    /// <returns>A new <see cref="AllyariaColor" /> parsed from <paramref name="hex" />.</returns>
    internal static AllyariaColor FromHexInline(string hex)
    {
        FromHex(hex, out var r, out var g, out var b, out var a);

        return new AllyariaColor(r, g, b, a);
    }

    /// <summary>Parses an <c>hsv(H,S%,V%)</c> or <c>hsva(H,S%,V%,A)</c> CSS color function.</summary>
    /// <param name="s">The input string to parse.</param>
    /// <param name="r">Outputs the red channel (0–255).</param>
    /// <param name="g">Outputs the green channel (0–255).</param>
    /// <param name="b">Outputs the blue channel (0–255).</param>
    /// <param name="a">Outputs the alpha channel (0–1). Defaults to 1.0 when omitted.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the string is not in <c>hsv()</c>/<c>hsva()</c> form or contains
    /// out-of-range values.
    /// </exception>
    internal static void FromHsvFunction(string s, out byte r, out byte g, out byte b, out double a)
    {
        var m = RxHsv.Match(s);

        if (!m.Success)
        {
            throw new ArgumentException(
                $"Invalid hsv/hsva() format: '{s}'. Expected hsv(H,S%,V%) or hsva(H,S%,V%,A).", nameof(s)
            );
        }

        var h = Clamp(ParseDouble(m.Groups["h"].Value, "H"), 0, 360);
        var sp = Clamp(ParseDouble(m.Groups["s"].Value, "S%"), 0, 100);
        var vp = Clamp(ParseDouble(m.Groups["v"].Value, "V%"), 0, 100);

        a = m.Groups["a"].Success
            ? Clamp01(ParseDouble(m.Groups["a"].Value, "A"))
            : 1.0;

        HsvToRgb(h, sp, vp, out r, out g, out b);
    }

    /// <summary>Parses an <c>rgb(r,g,b)</c> or <c>rgba(r,g,b,a)</c> CSS color function.</summary>
    /// <param name="s">The input string to parse.</param>
    /// <param name="r">Outputs the red channel (0–255).</param>
    /// <param name="g">Outputs the green channel (0–255).</param>
    /// <param name="b">Outputs the blue channel (0–255).</param>
    /// <param name="a">Outputs the alpha channel (0–1). Defaults to 1.0 when omitted.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the string is not in <c>rgb()</c>/<c>rgba()</c> form or contains
    /// out-of-range values.
    /// </exception>
    internal static void FromRgbFunction(string s, out byte r, out byte g, out byte b, out double a)
    {
        var m = RxRgb.Match(s);

        if (!m.Success)
        {
            throw new ArgumentException(
                $"Invalid rgb/rgba() format: '{s}'. Expected rgb(r,g,b) or rgba(r,g,b,a).", nameof(s)
            );
        }

        r = ClampByte(ParseInt(m.Groups["r"].Value, "r", 0, 255));
        g = ClampByte(ParseInt(m.Groups["g"].Value, "g", 0, 255));
        b = ClampByte(ParseInt(m.Groups["b"].Value, "b", 0, 255));

        a = m.Groups["a"].Success
            ? Clamp01(ParseDouble(m.Groups["a"].Value, "a"))
            : 1.0;
    }

    /// <summary>Converts HSV to RGB bytes.</summary>
    /// <param name="h">Hue in degrees (<c>0..360</c>).</param>
    /// <param name="s">Saturation in percent (<c>0..100</c>).</param>
    /// <param name="v">Value (brightness) in percent (<c>0..100</c>).</param>
    /// <param name="r">Outputs the red channel (0–255).</param>
    /// <param name="g">Outputs the green channel (0–255).</param>
    /// <param name="b">Outputs the blue channel (0–255).</param>
    /// <remarks>
    /// This method normalizes <paramref name="h" /> to <c>[0,360)</c>, converts <paramref name="s" /> and
    /// <paramref name="v" /> from percent to unit, performs the sector-based conversion, and rounds the results to bytes.
    /// </remarks>
    internal static void HsvToRgb(double h,
        double s,
        double v,
        out byte r,
        out byte g,
        out byte b)
    {
        s = Clamp01(s / 100.0);
        v = Clamp01(v / 100.0);

        if (s <= 0.0)
        {
            var c = (byte)Math.Round(v * 255.0);
            r = g = b = c;

            return;
        }

        h = (h % 360 + 360) % 360; // normalize to [0,360]
        var hh = h / 60.0;
        var i = (int)Math.Floor(hh);
        var ff = hh - i;

        var p = v * (1.0 - s);
        var q = v * (1.0 - s * ff);
        var t = v * (1.0 - s * (1.0 - ff));

        double r1, g1, b1;

        switch (i)
        {
            case 0:
                r1 = v;
                g1 = t;
                b1 = p;

                break;
            case 1:
                r1 = q;
                g1 = v;
                b1 = p;

                break;
            case 2:
                r1 = p;
                g1 = v;
                b1 = t;

                break;
            case 3:
                r1 = p;
                g1 = q;
                b1 = v;

                break;
            case 4:
                r1 = t;
                g1 = p;
                b1 = v;

                break;
            default:
                r1 = v;
                g1 = p;
                b1 = q;

                break; // case 5
        }

        r = (byte)Math.Clamp((int)Math.Round(r1 * 255.0), 0, 255);
        g = (byte)Math.Clamp((int)Math.Round(g1 * 255.0), 0, 255);
        b = (byte)Math.Clamp((int)Math.Round(b1 * 255.0), 0, 255);
    }

    /// <summary>Normalizes a Material color key for lookup.</summary>
    /// <param name="input">An input such as <c>"Deep Purple 200"</c>, <c>"deep-purple-200"</c>, or <c>"red500"</c>.</param>
    /// <returns>A normalized, lower-case key without spaces, dashes, or underscores (e.g., <c>"deeppurple200"</c>).</returns>
    internal static string NormalizeMaterialKey(string input)
    {
        var s = input.Trim().ToLowerInvariant()
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .Replace("_", string.Empty);

        return s;
    }

    /// <summary>Parses a floating-point number using invariant culture.</summary>
    /// <param name="s">The source text.</param>
    /// <param name="param">A parameter name used in exception messages.</param>
    /// <returns>The parsed number.</returns>
    /// <exception cref="ArgumentException">Thrown when parsing fails.</exception>
    internal static double ParseDouble(string s, string param)
        => !double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var v)
            ? throw new ArgumentException($"Could not parse number {param}='{s}'.", param)
            : v;

    /// <summary>Parses an integer and validates it against the provided inclusive range.</summary>
    /// <param name="s">The source text.</param>
    /// <param name="param">A parameter name used in exception messages.</param>
    /// <param name="min">The minimum allowed value (inclusive).</param>
    /// <param name="max">The maximum allowed value (inclusive).</param>
    /// <returns>The parsed integer.</returns>
    /// <exception cref="ArgumentException">Thrown when parsing fails.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the parsed value is out of range.</exception>
    internal static int ParseInt(string s, string param, int min, int max)
    {
        if (!int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var v))
        {
            throw new ArgumentException($"Could not parse integer {param}='{s}'.", param);
        }

        if (v < min || v > max)
        {
            throw new ArgumentOutOfRangeException(param, v, $"Expected {param} in [{min}..{max}].");
        }

        return v;
    }

    /// <summary>Converts RGB bytes to HSV.</summary>
    /// <param name="r">Red (0–255).</param>
    /// <param name="g">Green (0–255).</param>
    /// <param name="b">Blue (0–255).</param>
    /// <param name="h">Outputs hue in degrees (<c>0..360</c>).</param>
    /// <param name="s">Outputs saturation in percent (<c>0..100</c>).</param>
    /// <param name="v">Outputs value (brightness) in percent (<c>0..100</c>).</param>
    /// <remarks>
    /// When <paramref name="r" />, <paramref name="g" />, and <paramref name="b" /> are equal, hue is defined as <c>0</c> and
    /// saturation as <c>0</c>.
    /// </remarks>
    internal static void RgbToHsv(byte r,
        byte g,
        byte b,
        out double h,
        out double s,
        out double v)
    {
        var rf = r / 255.0;
        var gf = g / 255.0;
        var bf = b / 255.0;

        var max = Math.Max(rf, Math.Max(gf, bf));
        var min = Math.Min(rf, Math.Min(gf, bf));
        var delta = max - min;

        // Hue
        if (delta < 1e-9)
        {
            h = 0.0;
        }
        else if (Math.Abs(max - rf) < 1e-9)
        {
            h = 60.0 * ((gf - bf) / delta % 6.0);
        }
        else if (Math.Abs(max - gf) < 1e-9)
        {
            h = 60.0 * ((bf - rf) / delta + 2.0);
        }
        else
        {
            h = 60.0 * ((rf - gf) / delta + 4.0);
        }

        if (h < 0)
        {
            h += 360.0;
        }

        // Saturation
        s = max <= 0
            ? 0
            : delta / max * 100.0;

        // Value
        v = max * 100.0;
    }

    /// <summary>Converts a single hexadecimal digit to its numeric nibble value.</summary>
    /// <param name="c">A character in the set <c>0–9</c>, <c>a–f</c>, or <c>A–F</c>.</param>
    /// <returns>The integer value <c>0..15</c> represented by <paramref name="c" />.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="c" /> is not a valid hex digit.</exception>
    internal static int ToHexNibble(char c)
        => c switch
        {
            >= '0' and <= '9' => c - '0',
            >= 'a' and <= 'f' => c - 'a' + 10,
            >= 'A' and <= 'F' => c - 'A' + 10,
            _ => throw new ArgumentException($"Invalid hex digit '{c}'.")
        };

    /// <summary>Attempts to parse a Material color name of the form <c>{Hue}{Tone}</c>.</summary>
    /// <param name="name">
    /// Examples include <c>"DeepPurple200"</c>, <c>"red500"</c>, or <c>"deep-purple a700"</c> (whitespace, dashes, and
    /// underscores are ignored).
    /// </param>
    /// <param name="color">When this method returns, contains the parsed color if successful; otherwise the default value.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise <c>false</c>.</returns>
    internal static bool TryFromMaterialName(string name, out AllyariaColor color)
    {
        var norm = NormalizeMaterialKey(name);

        if (MaterialMap.TryGetValue(norm, out var rgba))
        {
            color = rgba;

            return true;
        }

        color = default(AllyariaColor);

        return false;
    }

    /// <summary>Attempts to parse a CSS Web color name (case-insensitive).</summary>
    /// <param name="name">The color name (e.g., <c>"dodgerblue"</c>, <c>"white"</c>).</param>
    /// <param name="color">When this method returns, contains the parsed color if successful; otherwise the default value.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise <c>false</c>.</returns>
    internal static bool TryFromWebName(string name, out AllyariaColor color)
    {
        var key = name.Trim().ToLowerInvariant();

        if (WebNameMap.TryGetValue(key, out var rgba))
        {
            color = rgba;

            return true;
        }

        color = default(AllyariaColor);

        return false;
    }
}
