using System.Globalization;

namespace Allyaria.Theming;

/// <summary>
/// Represents a framework-agnostic color value with CSS-oriented parsing and formatting, immutable value semantics, and
/// total ordering by the uppercase <c>#RRGGBBAA</c> form.
/// </summary>
/// <remarks>
/// This type is a small, immutable value type (readonly struct). It supports:
/// <list type="bullet">
///     <item>
///         <description>
///         Parsing from <c>#RGB</c>, <c>#RGBA</c>, <c>#RRGGBB</c>, <c>#RRGGBBAA</c>, <c>rgb()</c>, <c>rgba()</c>,
///         <c>hsv()</c>, <c>hsva()</c>, CSS Web color names, and Material color names.
///         </description>
///     </item>
///     <item>
///         <description>Conversions between RGBA and HSVA (H: 0–360°, S/V: 0–100%, A: 0–1).</description>
///     </item>
///     <item>
///         <description>Formatting to multiple string forms and CSS declarations.</description>
///     </item>
///     <item>
///         <description>Value equality and ordering by the canonical <c>#RRGGBBAA</c> string.</description>
///     </item>
/// </list>
/// All numeric parsing/formatting uses <see cref="CultureInfo.InvariantCulture" />.
/// </remarks>
public readonly partial struct AllyariaColor : IComparable<AllyariaColor>, IEquatable<AllyariaColor>
{
    /// <summary>Initializes a color from HSVA channels.</summary>
    /// <param name="h">Hue in degrees, clamped to [0..360].</param>
    /// <param name="s">Saturation in percent, clamped to [0..100].</param>
    /// <param name="v">Value (brightness) in percent, clamped to [0..100].</param>
    /// <param name="a">Alpha in [0..1], clamped.</param>
    private AllyariaColor(double h, double s, double v, double a = 1.0)
    {
        HsvToRgb(Clamp(h, 0, 360), Clamp(s, 0, 100), Clamp(v, 0, 100), out var r, out var g, out var b);
        R = r;
        G = g;
        B = b;
        A = Clamp01(a);
    }

    /// <summary>Initializes a color from RGBA channels.</summary>
    /// <param name="r">Red in [0..255].</param>
    /// <param name="g">Green in [0..255].</param>
    /// <param name="b">Blue in [0..255].</param>
    /// <param name="a">Alpha in [0..1], clamped.</param>
    private AllyariaColor(byte r, byte g, byte b, double a = 1.0)
    {
        R = r;
        G = g;
        B = b;
        A = Clamp01(a);
    }

    /// <summary>
    /// Initializes a color by parsing a CSS-like string: <c>#RGB</c>, <c>#RGBA</c>, <c>#RRGGBB</c>, <c>#RRGGBBAA</c>,
    /// <c>rgb()</c>, <c>rgba()</c>, <c>hsv(H,S%,V%)</c>, <c>hsva(H,S%,V%,A)</c>, Web color names, or Material color names.
    /// </summary>
    /// <param name="value">The input string to parse.</param>
    /// <exception cref="ArgumentException">Thrown when the value is not a recognized color format or name.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <c>null</c>.</exception>
    public AllyariaColor(string value)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(value);

            var s = value.Trim();

            // Hex forms
            if (s.StartsWith("#", StringComparison.Ordinal))
            {
                FromHexString(s, out var r, out var g, out var b, out var a);
                R = r;
                G = g;
                B = b;
                A = a;

                return;
            }

            // rgb()/rgba()
            if (s.StartsWith("rgb", StringComparison.OrdinalIgnoreCase))
            {
                FromRgbString(s, out var r, out var g, out var b, out var a);
                R = r;
                G = g;
                B = b;
                A = a;

                return;
            }

            // hsv()/hsva()
            if (s.StartsWith("hsv", StringComparison.OrdinalIgnoreCase))
            {
                FromHsvString(s, out var r, out var g, out var b, out var a);
                R = r;
                G = g;
                B = b;
                A = a;

                return;
            }

            // Named palettes
            if (TryFromWebName(s, out var web))
            {
                R = web.R;
                G = web.G;
                B = web.B;
                A = web.A;

                return;
            }

            if (TryFromMaterialName(s, out var mat))
            {
                R = mat.R;
                G = mat.G;
                B = mat.B;
                A = mat.A;

                return;
            }

            throw new ArgumentException("Color not found.", nameof(value));
        }

        catch (Exception exception)
        {
            throw new ArgumentException(
                $"Unrecognized color: '{value}'. Expected #RRGGBB, #RRGGBBAA, rgb(), rgba(), hsv(), hsva(), a CSS Web color name, or a Material color name.",
                nameof(value), exception
            );
        }
    }
}
