using Allyaria.Abstractions.Constants;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Allyaria.Abstractions.Types;

/// <summary>Represents an immutable color value with red, green, blue, and alpha channels.</summary>
/// <remarks>
///     <para>
///     A <see cref="HexColor" /> can be constructed directly from component values (<see cref="HexByte" />) or parsed from
///     a wide range of color string formats, including:
///     </para>
///     <list type="bullet">
///         <item>
///             <description><b>Hexadecimal</b> — <c>#RGB</c>, <c>#RGBA</c>, <c>#RRGGBB</c>, or <c>#RRGGBBAA</c></description>
///         </item>
///         <item>
///             <description>
///             <b>Functional RGB(A)</b> — <c>rgb(...)</c> and <c>rgba(...)</c>, including modern CSS Color Level 4 syntax
///             (e.g., <c>rgb(255 0 0 / .5)</c>)
///             </description>
///         </item>
///         <item>
///             <description><b>Functional HSV(A)</b> — <c>hsv(...)</c> and <c>hsva(...)</c></description>
///         </item>
///         <item>
///             <description>
///             <b>Named Colors</b> — case-insensitive lookup against the global
///             <see cref="Allyaria.Abstractions.Constants.Colors" /> registry (e.g., <c>"Red500"</c>). The lookup uses the
///             comparer configured in that registry (typically <see cref="StringComparer.InvariantCultureIgnoreCase" />).
///             </description>
///         </item>
///     </list>
///     <para>
///     The type is a <see langword="readonly struct" />, ensuring immutability and thread safety. All channel values are
///     normalized at construction time, and derived HSV components (<see cref="H" />, <see cref="S" />, <see cref="V" />)
///     are computed automatically.
///     </para>
///     <para>
///     If an input string cannot be parsed as a supported format or a known color name, an
///     <see cref="AryArgumentException" /> is thrown.
///     </para>
/// </remarks>
public readonly struct HexColor : IComparable<HexColor>, IEquatable<HexColor>
{
    /// <summary>
    /// Regular-expression sub-pattern used to match an alpha (opacity) component in color functions. Accepts values <c>0</c>,
    /// <c>1</c>, <c>1.0</c>, <c>0.5</c>, or fractional forms such as <c>.5</c>.
    /// </summary>
    private const string AlphaPattern = @"(?<alpha>(?:0?\.\d+|0|1(?:\.0+)?|(?:100|[1-9]?\d)%))";

    /// <summary>Precompiled regex pattern for hexadecimal color strings.</summary>
    private static readonly Regex HexColorPattern = new(
        "^#([0-9A-F]{3}|[0-9A-F]{4}|[0-9A-F]{6}|[0-9A-F]{8})$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Compiled regular expression that matches an <c>hsva(h, s%, v%, a)</c> color function containing hue, saturation, value,
    /// and alpha components. Alpha values are validated against <see cref="AlphaPattern" />.
    /// </summary>
    private static readonly Regex HsvaPattern = new(
        @"^hsva\s*\(\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))\s*,\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))%?\s*,\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))%?\s*,\s*" +
        AlphaPattern + @"\s*\)$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Compiled regular expression that matches an <c>hsv(h, s%, v%)</c> color function containing hue, saturation, and value
    /// components. The pattern supports optional signs and decimal fractions, but does <b>not</b> allow an alpha component
    /// (see <see cref="HsvaPattern" /> for that).
    /// </summary>
    private static readonly Regex HsvPattern = new(
        @"^hsv\s*\(\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))\s*,\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))%?\s*,\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))%?\s*\)$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Compiled regular expression that matches modern CSS Color Level 4 syntax for <c>rgb(...)</c> or <c>rgba(...)</c> where
    /// channels are space-separated and the alpha component follows a slash—for example, <c>rgb(255 0 0 / .5)</c>. Falls back
    /// to the same channel validation as <see cref="RgbaPattern" />.
    /// </summary>
    private static readonly Regex RgbaCss4Pattern = new(
        @"^rgba?\s*\(\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+)%?)\s+([+-]?(?:\d+(?:\.\d+)?|\.\d+)%?)\s+([+-]?(?:\d+(?:\.\d+)?|\.\d+)%?)(?:\s*\/\s*"
        + AlphaPattern + @")?\s*\)$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Compiled regular expression that matches an <c>rgba(r, g, b, a)</c> color function with integer RGB channels and an
    /// explicit alpha component validated by <see cref="AlphaPattern" />. This corresponds to the traditional comma-separated
    /// CSS syntax.
    /// </summary>
    private static readonly Regex RgbaPattern = new(
        @"^rgba\s*\(\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*,\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*,\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*,\s*"
        + AlphaPattern + @"\s*\)$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Compiled regular expression that matches an <c>rgb(r, g, b)</c> color function consisting of three integer channel
    /// values in the 0–255 range. This pattern does not support alpha; use <see cref="RgbaPattern" /> or
    /// <see cref="RgbaCss4Pattern" /> for variants that do.
    /// </summary>
    private static readonly Regex RgbPattern = new(
        @"^rgb\s*\(\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*,\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*,\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*\)$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Initializes a new instance of the <see cref="HexColor" /> struct with all channels default-initialized to 0.
    /// </summary>
    public HexColor()
        : this(new HexByte(), new HexByte(), new HexByte(), new HexByte()) { }

    /// <summary>Initializes a new instance of the <see cref="HexColor" /> struct from individual channels.</summary>
    /// <param name="red">The red channel value.</param>
    /// <param name="green">The green channel value.</param>
    /// <param name="blue">The blue channel value.</param>
    /// <param name="alpha">The optional alpha channel value; if <see langword="null" />, defaults to 255 (opaque).</param>
    public HexColor(HexByte red, HexByte green, HexByte blue, HexByte? alpha = null)
    {
        R = red;
        G = green;
        B = blue;
        A = alpha ?? new HexByte(255);

        RgbToHsv(out var h, out var s, out var v);

        H = h;
        S = s;
        V = v;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HexColor" /> struct from a color string. Accepts the following formats:
    /// <list type="bullet">
    ///     <item>
    ///         <description>Hex: <c>#RGB</c>, <c>#RGBA</c>, <c>#RRGGBB</c>, <c>#RRGGBBAA</c></description>
    ///     </item>
    ///     <item>
    ///         <description>RGB(A): <c>rgb(...)</c>, <c>rgba(...)</c> (including CSS Color Level 4 slash-alpha syntax)</description>
    ///     </item>
    ///     <item>
    ///         <description>HSV(A): <c>hsv(...)</c>, <c>hsva(...)</c></description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         <b>Named colors</b>: a case-insensitive lookup against the global <c>Colors</c> registry (e.g., <c>"Red500"</c>
    ///         ). This is attempted only if the value does not start with <c>"#"</c>, <c>"rgb"</c>, or <c>"hsv"</c>.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <param name="value">The color string to parse. Leading/trailing whitespace is ignored.</param>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is null/empty or does not match any supported format or known color name.
    /// </exception>
    /// <remarks>
    /// Named-color comparison follows the <see cref="StringComparer" /> used by the <c>Colors</c> registry (e.g.,
    /// <c>InvariantCultureIgnoreCase</c>). If layering concerns matter, consider moving name resolution out of
    /// <see cref="HexColor" /> to avoid an upward dependency on theming constants.
    /// </remarks>
    public HexColor(string value)
    {
        AryArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

        HexByte red;
        HexByte green;
        HexByte blue;
        HexByte alpha;
        var trimmed = value.Trim();

        if (trimmed.StartsWith("hsv", StringComparison.OrdinalIgnoreCase))
        {
            ParseHsva(trimmed, out red, out green, out blue, out alpha);
        }
        else if (trimmed.StartsWith("rgb", StringComparison.OrdinalIgnoreCase))
        {
            ParseRgba(trimmed, out red, out green, out blue, out alpha);
        }
        else if (trimmed.StartsWith("#", StringComparison.OrdinalIgnoreCase))
        {
            ParseHex(trimmed, out red, out green, out blue, out alpha);
        }
        else if (!TryParseColorName(trimmed, out red, out green, out blue, out alpha))
        {
            throw new AryArgumentException($"Invalid color string: {value}.", nameof(value));
        }

        R = red;
        G = green;
        B = blue;
        A = alpha;

        RgbToHsv(out var h, out var s, out var v);

        H = h;
        S = s;
        V = v;
    }

    /// <summary>Gets the alpha (opacity) channel.</summary>
    public HexByte A { get; }

    /// <summary>Gets the blue channel.</summary>
    public HexByte B { get; }

    /// <summary>Gets the green channel.</summary>
    public HexByte G { get; }

    /// <summary>
    /// Gets the <b>Hue</b> component of the color, expressed in degrees within the range [0, 360). Represents the color’s
    /// dominant wavelength — <c>0</c> = red, <c>120</c> = green, <c>240</c> = blue.
    /// </summary>
    public double H { get; }

    /// <summary>Gets the red channel.</summary>
    public HexByte R { get; }

    /// <summary>
    /// Gets the <b>Saturation</b> component of the color, normalized to the range <c>[0, 1]</c>. A value of <c>0</c> indicates
    /// a fully desaturated color (gray), while <c>1</c> indicates full color intensity.
    /// </summary>
    public double S { get; }

    /// <summary>
    /// Gets the <b>Value</b> (brightness) component of the color, normalized to the range <c>[0, 1]</c>. A value of <c>0</c>
    /// represents black, and <c>1</c> represents the brightest possible version of the color.
    /// </summary>
    public double V { get; }

    /// <summary>
    /// Linearly interpolates the current <see cref="V" /> (value/brightness) toward a specified target value by a given blend
    /// factor within the closed interval <c>[0, 1]</c>.
    /// </summary>
    /// <param name="target">The target brightness value to blend toward (0–1).</param>
    /// <param name="factor">
    /// The interpolation factor clamped to <c>[0, 1]</c>. A value of <c>0</c> returns the current <see cref="V" />; a value of
    /// <c>1</c> returns the <paramref name="target" />.
    /// </param>
    /// <returns>The interpolated scalar value resulting from the linear blend.</returns>
    private double BlendValue(double target, double factor) => V + (target - V) * factor;

    /// <summary>Clamps a normalized channel (0–1) to a byte (0–255) using banker's rounding (ToEven).</summary>
    /// <param name="value">The normalized value.</param>
    /// <returns>The clamped 8-bit channel value.</returns>
    private static byte ClampToByte(double value)
        => (byte)Math.Clamp(Math.Round(value * 255.0, MidpointRounding.ToEven), 0, 255);

    /// <summary>Compares this instance with another <see cref="HexColor" /> to determine relative ordering.</summary>
    /// <param name="other">The other color to compare.</param>
    /// <returns>
    /// A value less than zero if this instance precedes <paramref name="other" />; zero if equal; greater than zero if this
    /// instance follows <paramref name="other" />.
    /// </returns>
    public int CompareTo(HexColor other)
        => (Red: R, Green: G, Blue: B, Alpha: A).CompareTo((other.R, other.G, other.B, other.A));

    /// <summary>
    /// Calculates the WCAG 2.2 contrast ratio between the current color (treated as the foreground) and a specified background
    /// color, based on their relative luminance in the sRGB color space.
    /// </summary>
    /// <param name="background">
    /// The background <see cref="HexColor" /> against which to measure contrast. Both the current color and
    /// <paramref name="background" /> are assumed to be fully opaque.
    /// </param>
    /// <exception cref="AryArgumentException">Thrown when the contrast ratio is NaN or Infinity.</exception>
    /// <returns>
    /// A <see cref="double" /> representing the contrast ratio, defined as <c>(Lighter + 0.05) / (Darker + 0.05)</c>, where
    /// <em>L</em> is the WCAG relative luminance. The ratio ranges from <c>1.0</c> (no contrast) to <em>21.0</em> (maximum
    /// contrast).
    /// </returns>
    /// <remarks>
    /// The returned ratio can be evaluated against WCAG 2.2 contrast thresholds:
    /// <list type="bullet">
    ///     <item>
    ///         <description>Normal text: ≥ 4.5 : 1</description>
    ///     </item>
    ///     <item>
    ///         <description>Large text (≥ 18 pt or 14 pt bold): ≥ 3 : 1</description>
    ///     </item>
    ///     <item>
    ///         <description>UI components and graphics: ≥ 3 : 1</description>
    ///     </item>
    /// </list>
    /// </remarks>
    public double ContrastRatio(HexColor background)
    {
        var foregroundL = ToRelativeLuminance();
        var backgroundL = background.ToRelativeLuminance();
        var lighter = Math.Max(foregroundL, backgroundL);
        var darker = Math.Min(foregroundL, backgroundL);
        var result = (lighter + 0.05) / (darker + 0.05);

        return double.IsNaN(result) || double.IsInfinity(result)
            ? throw new AryArgumentException("The specified colors are not valid colors.", nameof(background))
            : result;
    }

    /// <summary>
    /// Reduces the color’s saturation by a specified fraction and optionally blends its brightness toward a mid-tone to
    /// maintain perceptual balance.
    /// </summary>
    /// <param name="desaturateBy">
    /// The amount to decrease saturation, expressed as a normalized fraction in <c>[0,1]</c> (e.g., <c>0.6</c> = reduce by
    /// 60%). Values outside this range are clamped automatically.
    /// </param>
    /// <param name="valueBlendTowardMid">
    /// The blend factor toward a mid-tone brightness (V = 0.5), clamped to <c>[0, 1]</c>. Higher values yield a more neutral,
    /// evenly lit result.
    /// </param>
    /// <returns>A new <see cref="HexColor" /> instance representing the desaturated color.</returns>
    public HexColor Desaturate(double desaturateBy = 0.5, double valueBlendTowardMid = 0.15)
        => FromHsva(
            H, Math.Clamp(S - Math.Clamp(desaturateBy, 0.0, 1.0), 0.0, 1.0),
            BlendValue(0.5, Math.Clamp(valueBlendTowardMid, 0.0, 1.0)), A.ToNormalized()
        );

    /// <summary>
    /// Resolves a foreground color that meets (or best-approaches) a minimum contrast ratio over the background by preserving
    /// the foreground hue and saturation (HSV H/S) and adjusting only value (V). If that hue rail cannot reach the target
    /// (even at V = 0 or 1), the method mixes toward black and white and returns the closest solution that meets—or
    /// best-approaches—the target.
    /// </summary>
    /// <param name="background">Background color (opaque).</param>
    /// <param name="minimumRatio">Required minimum contrast ratio (1–21, e.g., <c>4.5</c> for body text).</param>
    /// <exception cref="AryArgumentException">Thrown when the minimum ratio is less than 1 or greater than 21.</exception>
    /// <returns>The resolved color.</returns>
    public HexColor EnsureMinimumContrast(HexColor background, double minimumRatio = 3.0)
    {
        AryArgumentException.ThrowIfOutOfRange<double>(minimumRatio, 1.0, 21.0, nameof(minimumRatio));

        var startRatio = ContrastRatio(background);

        if (startRatio >= minimumRatio)
        {
            return this;
        }

        var direction = ValueDirection(background);

        // 1) Preferred value-rail attempt
        var first = SearchValueRail(direction, background, minimumRatio);

        if (first.IsMinimumMet)
        {
            return first.ForegroundColor;
        }

        // 2) Poles
        var towardWhite = SearchTowardPole(Colors.White.SetAlpha(A.Value), background, minimumRatio);
        var towardBlack = SearchTowardPole(Colors.Black.SetAlpha(A.Value), background, minimumRatio);

        if (towardWhite.IsMinimumMet)
        {
            return towardWhite.ForegroundColor;
        }

        if (towardBlack.IsMinimumMet)
        {
            return towardBlack.ForegroundColor;
        }

        // 3) Best-effort fallback (no one met the target): pick highest contrast among ALL candidates tried
        var best = first;

        if (towardWhite.ContrastRatio > best.ContrastRatio)
        {
            best = towardWhite;
        }

        if (towardBlack.ContrastRatio > best.ContrastRatio)
        {
            best = towardBlack;
        }

        return best.ForegroundColor;
    }

    /// <summary>Indicates whether the current color is equal to another <see cref="HexColor" />.</summary>
    /// <param name="other">The color to compare with.</param>
    /// <returns><see langword="true" /> if the colors are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals(HexColor other)
        => R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A);

    /// <summary>Determines whether the specified object is equal to the current instance.</summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is HexColor other && Equals(other);

    /// <summary>Creates a <see cref="HexColor" /> from HSVA component values.</summary>
    /// <param name="hue">The hue component in degrees (nominally 0–360; values are normalized).</param>
    /// <param name="saturation">The saturation component in the 0–1 range.</param>
    /// <param name="value">The value (brightness) component in the 0–1 range.</param>
    /// <param name="alpha">The alpha component in the 0–1 range. Defaults to 1.0 (opaque).</param>
    /// <returns>A <see cref="HexColor" /> representing the specified HSVA.</returns>
    public static HexColor FromHsva(double hue, double saturation, double value, double alpha = 1.0)
    {
        var h = hue % 360.0;

        if (h < 0)
        {
            h += 360.0;
        }

        var s = Math.Clamp(saturation, 0.0, 1.0);
        var v = Math.Clamp(value, 0.0, 1.0);
        var a = HexByte.FromNormalized(Math.Clamp(alpha, 0.0, 1.0));

        return HsvaToRgba(h, s, v, a);
    }

    /// <summary>Returns a hash code for this instance.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => HashCode.Combine(R, G, B, A);

    /// <summary>Picks a strong high-contrast stroke for a given surface: black for light surfaces, white for dark.</summary>
    private static HexColor HighContrastStroke(HexColor surface)
        => surface.IsLight()
            ? Colors.Black
            : Colors.White;

    /// <summary>Converts HSVA values to an equivalent <see cref="HexColor" />.</summary>
    /// <param name="hue">Hue in degrees (can wrap), nominally 0–360.</param>
    /// <param name="saturation">Saturation in the 0–1 range.</param>
    /// <param name="value">Value (brightness) in the 0–1 range.</param>
    /// <param name="alpha">Alpha channel as a <see cref="HexByte" />.</param>
    /// <returns>A <see cref="HexColor" /> representing the HSVA input.</returns>
    private static HexColor HsvaToRgba(double hue, double saturation, double value, HexByte alpha)
    {
        hue %= 360.0;

        if (hue < 0)
        {
            hue += 360.0;
        }

        hue = (hue % 360.0 + 360.0) % 360.0;

        double red, green, blue;

        var chroma = value * saturation;
        var prime = hue / 60.0;
        var x = chroma * (1.0 - Math.Abs(prime % 2.0 - 1.0));
        var m = value - chroma;

        switch (prime)
        {
            case < 1:
                red = chroma;
                green = x;
                blue = 0;

                break;
            case < 2:
                red = x;
                green = chroma;
                blue = 0;

                break;
            case < 3:
                red = 0;
                green = chroma;
                blue = x;

                break;
            case < 4:
                red = 0;
                green = x;
                blue = chroma;

                break;
            case < 5:
                red = x;
                green = 0;
                blue = chroma;

                break;
            default:
                red = chroma;
                green = 0;
                blue = x;

                break;
        }

        return new HexColor(ClampToByte(red + m), ClampToByte(green + m), ClampToByte(blue + m), alpha);
    }

    /// <summary>
    /// Produces the photographic negative of the current color by inverting each RGB channel (i.e., <c>R' = 255 − R</c>,
    /// <c>G' = 255 − G</c>, <c>B' = 255 − B</c>). The alpha channel is preserved.
    /// </summary>
    /// <remarks>
    /// This matches the classic “negative image” operation performed in RGB space. The resulting hue/saturation/value may not
    /// equal a simple 180° hue rotation; it is the exact per-channel inversion of the underlying RGB values.
    /// </remarks>
    /// <returns>A new <see cref="HexColor" /> representing the RGB-inverted (negative) color.</returns>
    public HexColor Invert()
    {
        var r = (byte)(255 - R.Value);
        var g = (byte)(255 - G.Value);
        var b = (byte)(255 - B.Value);

        return new HexColor(r, g, b, A);
    }

    /// <summary>Determines whether the color is perceptually dark based on its relative luminance.</summary>
    /// <returns>
    /// <see langword="true" /> if <see cref="ToRelativeLuminance()" /> is less than <c>0.5</c>; otherwise,
    /// <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// This method provides a simple luminance-based classification that can be used for dynamic contrast adjustments, such as
    /// choosing a light foreground color for dark backgrounds.
    /// </remarks>
    public bool IsDark() => ToRelativeLuminance() < 0.5;

    /// <summary>Determines whether the color is perceptually light based on its relative luminance.</summary>
    /// <returns>
    /// <see langword="true" /> if <see cref="ToRelativeLuminance()" /> is greater than or equal to <c>0.5</c>; otherwise,
    /// <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// Complements <see cref="IsDark()" />. Useful for automatically selecting dark text or icons when the background color is
    /// light.
    /// </remarks>
    public bool IsLight() => ToRelativeLuminance() >= 0.5;

    /// <summary>Determines whether the current color is fully opaque.</summary>
    /// <returns>
    /// <see langword="true" /> if the alpha channel (<see cref="A" />) equals <c>255</c>; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// This check is useful for quickly identifying colors that require no alpha blending or compositing in rendering
    /// pipelines.
    /// </remarks>
    public bool IsOpaque() => A.Value is 255;

    /// <summary>Determines whether the current color is fully transparent.</summary>
    /// <returns>
    /// <see langword="true" /> if the alpha channel (<see cref="A" />) equals <c>0</c>; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>A fully transparent color contributes no visible effect when composited over other surfaces.</remarks>
    public bool IsTransparent() => A.Value is 0;

    /// <summary>Parses a color string into a new <see cref="HexColor" />.</summary>
    /// <param name="value">The color string to parse.</param>
    /// <returns>A new <see cref="HexColor" />.</returns>
    public static HexColor Parse(string value) => new(value);

    /// <summary>Parses an alpha value in the range 0–1 from a string into a <see cref="HexByte" />.</summary>
    /// <param name="value">The alpha string to parse.</param>
    /// <returns>A <see cref="HexByte" /> corresponding to the parsed alpha.</returns>
    /// <exception cref="AryArgumentException">Thrown when the value is invalid or out of range.</exception>
    private static HexByte ParseAlpha(string value)
    {
        var trimmed = value.Trim();

        _ = double.TryParse(trimmed, NumberStyles.Float, CultureInfo.InvariantCulture, out var alpha);

        return HexByte.FromNormalized(alpha);
    }

    /// <summary>Parses a decimal byte value (0–255) from a string into a <see cref="HexByte" />.</summary>
    /// <param name="value">The string containing the byte value.</param>
    /// <returns>A <see cref="HexByte" /> representing the parsed byte.</returns>
    /// <exception cref="AryArgumentException">Thrown when the value is not a valid byte.</exception>
    private static HexByte ParseByte(string value)
        => byte.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var byteValue)
            ? new HexByte(byteValue)
            : throw new AryArgumentException($"Byte value is out of range: {value}", nameof(value));

    /// <summary>
    /// Parses a single RGB channel value that may be expressed either as a byte (0–255) or as a percentage (0%–100%).
    /// </summary>
    /// <param name="value">
    /// The channel text to parse. May include a trailing percent sign (e.g., "128", "50%"). Whitespace around the value is
    /// ignored.
    /// </param>
    /// <returns>A <see cref="HexByte" /> representing the normalized channel intensity from 0.0 to 1.0.</returns>
    /// <remarks>
    /// This method supports both integer and percentage inputs per CSS Color specifications. Percentage values are clamped to
    /// the [0, 100] range before normalization to [0, 1].
    /// </remarks>
    private static HexByte ParseChannel(string value)
    {
        var trimmed = value.Trim();

        if (!trimmed.EndsWith('%'))
        {
            return ParseByte(trimmed);
        }

        var p = double.Parse(trimmed.TrimEnd('%'), NumberStyles.Float, CultureInfo.InvariantCulture);

        return HexByte.FromNormalized(Math.Clamp(p / 100.0, 0.0, 1.0));
    }

    /// <summary>
    /// Parses a hexadecimal color string and outputs channel components. Accepts <c>#RGB</c>, <c>#RGBA</c>, <c>#RRGGBB</c>,
    /// and <c>#RRGGBBAA</c>.
    /// </summary>
    /// <param name="value">The hex color string.</param>
    /// <param name="red">The resulting red component.</param>
    /// <param name="green">The resulting green component.</param>
    /// <param name="blue">The resulting blue component.</param>
    /// <param name="alpha">The resulting alpha component.</param>
    /// <exception cref="AryArgumentException">Thrown when the format is invalid or contains non-hex digits.</exception>
    private static void ParseHex(string value,
        out HexByte red,
        out HexByte green,
        out HexByte blue,
        out HexByte alpha)
    {
        var match = HexColorPattern.Match(value.Trim());

        if (!match.Success)
        {
            throw new AryArgumentException($"Invalid hex color format: {value}", nameof(value));
        }

        var hexValue = match.Groups[1].Value;

        switch (hexValue.Length)
        {
            case 3:
                Span<char> buf3 = stackalloc char[8];
                buf3[0] = hexValue[0];
                buf3[1] = hexValue[0];
                buf3[2] = hexValue[1];
                buf3[3] = hexValue[1];
                buf3[4] = hexValue[2];
                buf3[5] = hexValue[2];
                buf3[6] = 'F';
                buf3[7] = 'F';
                hexValue = new string(buf3);

                break;

            case 4:
                Span<char> buf4 = stackalloc char[8];
                buf4[0] = hexValue[0];
                buf4[1] = hexValue[0];
                buf4[2] = hexValue[1];
                buf4[3] = hexValue[1];
                buf4[4] = hexValue[2];
                buf4[5] = hexValue[2];
                buf4[6] = hexValue[3];
                buf4[7] = hexValue[3];
                hexValue = new string(buf4);

                break;

            case 6:
                hexValue += "FF";

                break;

            case 8:
                break;
        }

        var r = byte.Parse(hexValue[..2], NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        var g = byte.Parse(hexValue.AsSpan(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        var b = byte.Parse(hexValue.AsSpan(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        var a = byte.Parse(hexValue.AsSpan(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);

        red = new HexByte(r);
        green = new HexByte(g);
        blue = new HexByte(b);
        alpha = new HexByte(a);
    }

    /// <summary>Parses an HSVA string and outputs equivalent RGBA channel components.</summary>
    /// <param name="value">The HSVA string to parse.</param>
    /// <param name="red">The resulting red component.</param>
    /// <param name="green">The resulting green component.</param>
    /// <param name="blue">The resulting blue component.</param>
    /// <param name="alpha">The resulting alpha component.</param>
    /// <exception cref="AryArgumentException">Thrown when the string is invalid or values are out of range.</exception>
    private static void ParseHsva(string value,
        out HexByte red,
        out HexByte green,
        out HexByte blue,
        out HexByte alpha)
    {
        var match = HsvaPattern.Match(value);

        if (!match.Success)
        {
            match = HsvPattern.Match(value);

            if (!match.Success)
            {
                throw new AryArgumentException($"Invalid HSV(A) color: {value}", nameof(value));
            }
        }

        var h = ParseHue(match.Groups[1].Value);
        var s = ParsePercent(match.Groups[2].Value);
        var v = ParsePercent(match.Groups[3].Value);

        var a = match.Groups["alpha"].Success
            ? ParseAlpha(match.Groups["alpha"].Value)
            : new HexByte(255);

        var color = HsvaToRgba(h, s, v, a);

        red = color.R;
        green = color.G;
        blue = color.B;
        alpha = color.A;
    }

    /// <summary>Parses a hue value from a string.</summary>
    /// <param name="value">The string containing the hue value.</param>
    /// <returns>The parsed hue (not yet normalized).</returns>
    /// <exception cref="AryArgumentException">Thrown when the hue value is invalid.</exception>
    private static double ParseHue(string value)
        => !double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var hue) ||
            !double.IsFinite(hue)
                ? throw new AryArgumentException($"Invalid hue value: {value}", nameof(value))
                : hue;

    /// <summary>
    /// Parses a percentage or fraction string (e.g., <c>75%</c> or <c>0.75</c>) into a normalized value (0–1).
    /// </summary>
    /// <param name="value">The percentage/fraction string to parse.</param>
    /// <returns>The normalized value.</returns>
    /// <exception cref="AryArgumentException">Thrown when the percentage is invalid or out of 0–100 range.</exception>
    /// <remarks>
    /// Values with a '%' suffix are treated as percentages (0–100). Values without '%' are interpreted as normalized fractions
    /// (0–1). For example, "0.75" = 75% and "75" = 75%.
    /// </remarks>
    private static double ParsePercent(string value)
    {
        var trimmed = value.Trim();
        var hadPercent = trimmed.EndsWith("%", StringComparison.Ordinal);

        var numericText = hadPercent
            ? trimmed.TrimEnd('%').Trim()
            : trimmed;

        _ = double.TryParse(numericText, NumberStyles.Float, CultureInfo.InvariantCulture, out var number);

        var percent = hadPercent
            ? number
            : number <= 1.0
                ? number * 100.0
                : number;

        if (!double.IsFinite(percent) || percent is < 0.0 or > 100.0)
        {
            throw new AryArgumentException($"Percentage must be between 0 and 100: {value}", nameof(value));
        }

        return percent / 100.0;
    }

    /// <summary>Parses an RGBA functional string and outputs component channels.</summary>
    /// <param name="value">The RGBA string to parse.</param>
    /// <param name="red">The resulting red component.</param>
    /// <param name="green">The resulting green component.</param>
    /// <param name="blue">The resulting blue component.</param>
    /// <param name="alpha">The resulting alpha component.</param>
    /// <exception cref="AryArgumentException">Thrown when the input is invalid.</exception>
    private static void ParseRgba(string value,
        out HexByte red,
        out HexByte green,
        out HexByte blue,
        out HexByte alpha)
    {
        var match = RgbaPattern.Match(value);

        if (!match.Success)
        {
            match = RgbPattern.Match(value);

            if (!match.Success)
            {
                match = RgbaCss4Pattern.Match(value);

                if (!match.Success)
                {
                    throw new AryArgumentException($"Invalid RGB(A) color: {value}", nameof(value));
                }
            }
        }

        red = ParseChannel(match.Groups[1].Value);
        green = ParseChannel(match.Groups[2].Value);
        blue = ParseChannel(match.Groups[3].Value);

        alpha = match.Groups["alpha"].Success
            ? match.Groups["alpha"].Value.TrimEnd().EndsWith("%", StringComparison.Ordinal)
                ? HexByte.FromNormalized(
                    Math.Clamp(
                        double.Parse(
                            match.Groups["alpha"].Value.TrimEnd('%'),
                            NumberStyles.Float, CultureInfo.InvariantCulture
                        ) / 100.0, 0.0, 1.0
                    )
                )
                : ParseAlpha(match.Groups["alpha"].Value)
            : new HexByte(255);
    }

    /// <summary>
    /// Computes the “least intrusive” passing score (lower is better) for a candidate border against its two adjacencies.
    /// Returns <see cref="double.PositiveInfinity" /> when it does not pass against any side.
    /// </summary>
    /// <param name="passes">Whether the candidate passes against at least one adjacency.</param>
    /// <param name="contrastA">Contrast versus adjacency A (e.g., component fill).</param>
    /// <param name="contrastB">Contrast versus adjacency B (e.g., outer background).</param>
    /// <param name="minContrast">The threshold that defines a “pass”.</param>
    private static double PassingScore(bool passes, double contrastA, double contrastB, double minContrast)
    {
        if (!passes)
        {
            return double.PositiveInfinity;
        }

        var best = double.PositiveInfinity;

        if (contrastA >= minContrast)
        {
            best = Math.Min(best, contrastA);
        }

        if (contrastB >= minContrast)
        {
            best = Math.Min(best, contrastB);
        }

        return best;
    }

    /// <summary>
    /// Converts the current RGB color components (<see cref="R" />, <see cref="G" />, <see cref="B" />) into their
    /// corresponding HSV (Hue, Saturation, Value) representation.
    /// </summary>
    /// <param name="hue">
    /// The resulting hue component, expressed in degrees within the range [0, 360). A value of <c>0</c> represents red,
    /// <c>120</c> represents green, and <c>240</c> represents blue.
    /// </param>
    /// <param name="saturation">
    /// The resulting saturation component, a normalized value in the range [0, 1], where <c>0</c> indicates a shade of gray
    /// and <c>1</c> represents full color intensity.
    /// </param>
    /// <param name="value">
    /// The resulting value (brightness) component, a normalized value in the range [0, 1], where <c>0</c> represents black and
    /// <c>1</c> represents the brightest form of the color.
    /// </param>
    /// <remarks>
    /// This method performs a precise RGB → HSV conversion without floating-point equality comparisons. The dominant color
    /// channel is determined from the original byte values to prevent loss of precision. When all RGB components are equal (a
    /// shade of gray), the hue is set to <c>0</c> by convention.
    /// </remarks>
    private void RgbToHsv(out double hue, out double saturation, out double value)
    {
        // Determine max/min from bytes for sector & delta, then scale once:
        var red = R.Value;
        var green = G.Value;
        var blue = B.Value;

        var maxByte = Math.Max(red, Math.Max(green, blue));
        var minByte = Math.Min(red, Math.Min(green, blue));

        var max = maxByte / 255.0;
        var min = minByte / 255.0;
        var delta = max - min;

        var rN = red / 255.0;
        var gN = green / 255.0;
        var bN = blue / 255.0;

        // Value (V)
        value = max;

        // Saturation (S)
        saturation = max <= 0.0
            ? 0.0
            : delta / max;

        // Hue (H)
        if (delta is 0.0)
        {
            // Gray — hue undefined; choose 0 by convention
            hue = 0.0;

            return;
        }

        // Pick sector by the *byte* that was the maximum (exact compare, stable on ties).
        // Tie-breaking falls back to the first true branch in this order: R, then G, then B.
        if (red >= green && red >= blue)
        {
            hue = 60.0 * ((gN - bN) / delta);
        }
        else if (green >= red && green >= blue)
        {
            hue = 60.0 * ((bN - rN) / delta + 2.0);
        }
        else
        {
            hue = 60.0 * ((rN - gN) / delta + 4.0);
        }

        // Normalize hue to [0, 360)
        if (hue < 0.0)
        {
            hue += 360.0;
        }
    }

    /// <summary>
    /// Binary-search mixing of a starting foreground toward a pole (black or white) in sRGB, returning the closest solution
    /// that meets—or best-approaches—the target contrast ratio.
    /// </summary>
    /// <param name="pole">Target pole (typically Black or White).</param>
    /// <param name="background">Background color (opaque).</param>
    /// <param name="minimumRatio">Target contrast ratio.</param>
    /// <returns>The resolution result for this pole.</returns>
    private ContrastResult SearchTowardPole(HexColor pole, HexColor background, double minimumRatio)
    {
        const int iterations = 18;
        const double eps = 1e-4;

        var bestRatio = -1.0;
        var bestColor = this;

        var met = false;
        var low = 0.0;
        var high = 1.0;

        for (var i = 0; i < iterations; i++)
        {
            var mid = Math.Clamp(0.5 * (low + high), 0.0, 1.0);
            var candidate = ToLerpLinearPreserveAlpha(pole, mid);
            var ratio = candidate.ContrastRatio(background);

            if (ratio > bestRatio)
            {
                bestRatio = ratio;
                bestColor = candidate;
            }

            if (ratio >= minimumRatio)
            {
                met = true;
                high = mid;
            }
            else
            {
                low = mid;
            }

            if (high - low < eps)
            {
                break;
            }
        }

        var finalColor = met
            ? ToLerpLinearPreserveAlpha(pole, high)
            : bestColor;

        var finalRatio = finalColor.ContrastRatio(background);

        return new ContrastResult(finalColor, finalRatio, met);
    }

    /// <summary>
    /// Binary search along the HSV value rail (holding H and S constant) to find the minimum-change V that meets a required
    /// contrast ratio; returns the best-approaching candidate when unreachable.
    /// </summary>
    /// <param name="direction"><c>+1</c> to brighten; <c>-1</c> to darken.</param>
    /// <param name="background">Background color.</param>
    /// <param name="minimumRatio">Target contrast ratio.</param>
    /// <returns>Resolution result for this search branch.</returns>
    private ContrastResult SearchValueRail(int direction, HexColor background, double minimumRatio)
    {
        const int iterations = 18;
        const double eps = 1e-4;

        var bestRatio = -1.0;
        var bestColor = FromHsva(H, S, V, A.ToNormalized());

        double low, high;
        double? found = null;

        if (direction > 0)
        {
            low = V;
            high = 1.0;
        }
        else
        {
            low = 0.0;
            high = V;
        }

        for (var i = 0; i < iterations; i++)
        {
            var mid = Math.Clamp(0.5 * (low + high), 0.0, 1.0);
            var candidate = FromHsva(H, S, mid, A.ToNormalized());
            var ratio = candidate.ContrastRatio(background);

            if (ratio > bestRatio)
            {
                bestRatio = ratio;
                bestColor = candidate;
            }

            if (ratio >= minimumRatio)
            {
                found = mid;
                high = mid;
            }
            else
            {
                low = mid;
            }

            if (high - low < eps)
            {
                break;
            }
        }

        if (!found.HasValue)
        {
            return new ContrastResult(bestColor, bestRatio, false);
        }

        var finalColor = FromHsva(H, S, high, A.ToNormalized());
        var finalRatio = finalColor.ContrastRatio(background);

        return new ContrastResult(finalColor, finalRatio, true);
    }

    /// <summary>
    /// Creates a new <see cref="HexColor" /> instance using the current red, green, and blue components, but with the
    /// specified alpha (opacity) value.
    /// </summary>
    /// <param name="alpha">
    /// The new alpha component, expressed as a byte in the range <c>[0, 255]</c>, where <c>0</c> represents full transparency
    /// and <c>255</c> represents full opacity.
    /// </param>
    /// <returns>
    /// A new <see cref="HexColor" /> that is identical in color to the current instance but with the provided alpha value
    /// applied.
    /// </returns>
    /// <remarks>
    /// This method does not modify the current instance; it returns a new color structure with the updated transparency
    /// component.
    /// </remarks>
    public HexColor SetAlpha(byte alpha) => new(R, G, B, new HexByte(alpha));

    /// <summary>
    /// Adjusts the perceived lightness of the color by shifting its value (<see cref="V" />) upward or downward depending on
    /// whether the color is currently light or dark.
    /// </summary>
    /// <param name="delta">
    /// The magnitude of change to apply to <see cref="V" />, expressed as a fraction in <c>[0,1]</c>. Positive values lighten
    /// dark colors and darken light colors automatically.
    /// </param>
    /// <returns>A new <see cref="HexColor" /> instance with the adjusted brightness level.</returns>
    public HexColor ShiftLightness(double delta = 0.05)
    {
        var direction = V >= 0.5
            ? -1.0
            : 1.0;

        return FromHsva(H, S, Math.Clamp(V + delta * direction, 0.0, 1.0), A.ToNormalized());
    }

    /// <summary>
    /// Derives a component border color. If the component has its own fill (e.g., a button), pass it via
    /// <paramref name="componentFill" />. If the component is transparent (no fill), leave <paramref name="componentFill" />
    /// as <see langword="null" /> and this will behave as a divider on <paramref name="outerBackground" />. In high-contrast
    /// mode, returns <c>this</c> (the component's content FG) for a strong outline.
    /// </summary>
    /// <param name="outerBackground">The surrounding/page surface under and around the component.</param>
    /// <param name="componentFill">Optional component fill; when <see langword="null" />, treated as “no fill”.</param>
    /// <param name="minContrast">Minimum required non-text contrast (default 3.0).</param>
    /// <param name="highContrast">When true, returns a strong HC outline (this FG).</param>
    /// <returns>A subtle border that passes against at least one adjacency and preserves text hierarchy.</returns>
    public HexColor ToComponentBorderColor(HexColor outerBackground,
        HexColor? componentFill = null,
        double minContrast = 3.0,
        bool highContrast = false)
    {
        if (highContrast)
        {
            return this;
        }

        if (!componentFill.HasValue)
        {
            return ToDividerBorderColor(outerBackground, minContrast, highContrast);
        }

        var fill = componentFill.Value;

        // Candidate A: shade of the fill (contrast vs fill)
        var fromFill = fill.EnsureMinimumContrast(fill, minContrast);
        var aVsFill = fromFill.ContrastRatio(fill);
        var aVsOuter = fromFill.ContrastRatio(outerBackground);
        var aPasses = aVsFill >= minContrast || aVsOuter >= minContrast;

        // Candidate B: shade of the outer background (contrast vs outer)
        var fromOuter = outerBackground.EnsureMinimumContrast(outerBackground, minContrast);
        var bVsFill = fromOuter.ContrastRatio(fill);
        var bVsOuter = fromOuter.ContrastRatio(outerBackground);
        var bPasses = bVsFill >= minContrast || bVsOuter >= minContrast;

        var scoreA = PassingScore(aPasses, aVsFill, aVsOuter, minContrast);
        var scoreB = PassingScore(bPasses, bVsFill, bVsOuter, minContrast);

        var chosen = scoreA <= scoreB
            ? fromFill
            : fromOuter;

        // Preserve hierarchy against the component fill (where label lives)
        var fgVsFill = ContrastRatio(fill);
        var chosenVsFill = chosen.ContrastRatio(fill);

        if (chosenVsFill > fgVsFill)
        {
            chosen = chosen.ToLerpLinearPreserveAlpha(fill, 0.15);
        }

        // Optional: avoid border merging with FG at the edge
        var fgVsBorder = ContrastRatio(chosen);

        if (fgVsBorder < 1.5)
        {
            var edgePole = fill.IsLight()
                ? Colors.Black
                : Colors.White;

            chosen = chosen.ToLerpLinearPreserveAlpha(edgePole, 0.10);
        }

        return chosen;
    }

    /// <summary>
    /// Derives a divider/outline color for a single surface (e.g., a hairline on a card) that meets WCAG non-text contrast (≥
    /// <paramref name="minContrast" />; default 3:1) against <paramref name="surface" />. In high-contrast mode, returns a
    /// strong outline automatically.
    /// </summary>
    public HexColor ToDividerBorderColor(HexColor surface, double minContrast = 3.0, bool highContrast = false)
    {
        if (highContrast)
        {
            return HighContrastStroke(surface);
        }

        // Preserve hue: adjust only Value (V) to reach the target vs the *same surface*.
        var stroke = surface.EnsureMinimumContrast(surface, minContrast);

        // Keep divider quieter than this FG over the same surface
        var fgVsSurface = ContrastRatio(surface);
        var strokeVsSurface = stroke.ContrastRatio(surface);

        if (strokeVsSurface > fgVsSurface)
        {
            stroke = stroke.ToLerpLinearPreserveAlpha(surface, 0.15);
        }

        return stroke;
    }

    /// <summary>
    /// Gamma-correct (linear-light) interpolation between two colors, including alpha. Uses linearization via
    /// <see cref="HexByte.ToLerpLinearHexByte" /> for perceptually better blends than plain sRGB lerp.
    /// </summary>
    /// <param name="end">End color.</param>
    /// <param name="factor">Interpolation factor clamped to <c>[0, 1]</c>.</param>
    /// <returns>The interpolated color.</returns>
    public HexColor ToLerpLinear(HexColor end, double factor)
        => new(
            R.ToLerpLinearHexByte(end.R, factor),
            G.ToLerpLinearHexByte(end.G, factor),
            B.ToLerpLinearHexByte(end.B, factor),
            A.ToLerpHexByte(end.A, factor)
        );

    /// <summary>
    /// Gamma-correct (linear-light) interpolation between two colors, preserving alpha. Uses linearization via
    /// <see cref="HexByte.ToLerpLinearHexByte" /> for perceptually better blends than plain sRGB lerp.
    /// </summary>
    /// <param name="end">End color.</param>
    /// <param name="factor">Interpolation factor clamped to <c>[0, 1]</c>.</param>
    /// <returns>The interpolated color.</returns>
    public HexColor ToLerpLinearPreserveAlpha(HexColor end, double factor)
        => new(
            R.ToLerpLinearHexByte(end.R, factor),
            G.ToLerpLinearHexByte(end.G, factor),
            B.ToLerpLinearHexByte(end.B, factor),
            A
        );

    /// <summary>Computes WCAG relative luminance from an opaque sRGB color.</summary>
    /// <returns>Relative luminance in <c>[0, 1]</c>.</returns>
    /// <remarks>
    /// Relative luminance is computed using the linearized sRGB formula: <c>0.2126 × R + 0.7152 × G + 0.0722 × B</c>.
    /// </remarks>
    public double ToRelativeLuminance()
    {
        var redLuminance = R.ToSrgbLinearValue();
        var greenLuminance = G.ToSrgbLinearValue();
        var blueLuminance = B.ToSrgbLinearValue();

        return 0.2126 * redLuminance + 0.7152 * greenLuminance + 0.0722 * blueLuminance;
    }

    /// <summary>Returns a hexadecimal string representation of the color in the form <c>#RRGGBBAA</c>.</summary>
    /// <returns>The string representation.</returns>
    public override string ToString() => $"#{R}{G}{B}{A}";

    /// <summary>Attempts to parse the specified color string.</summary>
    /// <param name="value">The color string to parse.</param>
    /// <param name="result">When this method returns, contains the parsed color or default on failure.</param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out HexColor result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = default(HexColor);

            return false;
        }

        try
        {
            result = new HexColor(value);

            return true;
        }
        catch
        {
            result = default(HexColor);

            return false;
        }
    }

    /// <summary>
    /// Attempts to resolve a named color (e.g., <c>"Red500"</c>) from the global color registry and returns its RGBA channel
    /// components.
    /// </summary>
    /// <param name="value">
    /// The color name to look up. Comparison is case-insensitive (per the dictionary’s comparer). Leading/trailing whitespace
    /// is ignored.
    /// </param>
    /// <param name="red">On success, receives the resolved red channel; otherwise <c>0</c>.</param>
    /// <param name="green">On success, receives the resolved green channel; otherwise <c>0</c>.</param>
    /// <param name="blue">On success, receives the resolved blue channel; otherwise <c>0</c>.</param>
    /// <param name="alpha">On success, receives the resolved alpha channel; otherwise <c>0</c> (transparent).</param>
    /// <returns><see langword="true" /> if the name maps to a known color; otherwise <see langword="false" />.</returns>
    /// <remarks>
    /// On failure, all channel outputs are set to <c>0</c>. The lookup uses whatever <see cref="StringComparer" /> the
    /// <c>Colors</c> dictionary was constructed with (e.g., <c>InvariantCultureIgnoreCase</c> or <c>OrdinalIgnoreCase</c>).
    /// </remarks>
    private static bool TryParseColorName(string value,
        out HexByte red,
        out HexByte green,
        out HexByte blue,
        out HexByte alpha)
    {
        red = new HexByte();
        green = new HexByte();
        blue = new HexByte();
        alpha = new HexByte();

        if (!Colors.TryGet(value, out var color))
        {
            return false;
        }

        red = color.R;
        green = color.G;
        blue = color.B;
        alpha = color.A;

        return true;
    }

    /// <summary>
    /// Chooses the initial direction to adjust HSV Value (V) for the foreground in order to locally increase contrast against
    /// the background. Returns <c>+1</c> to brighten or <c>-1</c> to darken.
    /// </summary>
    /// <param name="background">Background color (opaque).</param>
    /// <returns><c>+1</c> if brightening increases contrast more; otherwise <c>-1</c>.</returns>
    private int ValueDirection(HexColor background)
    {
        const double step = 0.02; // 2% of the 0..1 range

        var up = FromHsva(H, S, Math.Clamp(V + step, 0.0, 1.0), A.ToNormalized());
        var down = FromHsva(H, S, Math.Clamp(V - step, 0.0, 1.0), A.ToNormalized());

        var ratioUp = up.ContrastRatio(background);
        var ratioDown = down.ContrastRatio(background);

        return ratioUp > ratioDown
            ? +1
            : -1;
    }

    /// <summary>Determines whether two <see cref="HexColor" /> values are equal.</summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><see langword="true" /> if the values are equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(HexColor left, HexColor right) => left.Equals(right);

    /// <summary>Determines whether one <see cref="HexColor" /> is greater than another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if <paramref name="left" /> is greater; otherwise, <see langword="false" />.</returns>
    public static bool operator >(HexColor left, HexColor right) => left.CompareTo(right) > 0;

    /// <summary>Determines whether one <see cref="HexColor" /> is greater than or equal to another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool operator >=(HexColor left, HexColor right) => left.CompareTo(right) >= 0;

    /// <summary>Implicitly converts a color string to a <see cref="HexColor" />.</summary>
    /// <param name="hex">The color string to convert.</param>
    /// <returns>The resulting <see cref="HexColor" />.</returns>
    public static implicit operator HexColor(string hex) => new(hex);

    /// <summary>Implicitly converts a <see cref="HexColor" /> to its string representation.</summary>
    /// <param name="value">The color value.</param>
    /// <returns>The string representation.</returns>
    public static implicit operator string(HexColor value) => value.ToString();

    /// <summary>Determines whether two <see cref="HexColor" /> values are not equal.</summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><see langword="true" /> if the values are not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(HexColor left, HexColor right) => !left.Equals(right);

    /// <summary>Determines whether one <see cref="HexColor" /> is less than another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool operator <(HexColor left, HexColor right) => left.CompareTo(right) < 0;

    /// <summary>Determines whether one <see cref="HexColor" /> is less than or equal to another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool operator <=(HexColor left, HexColor right) => left.CompareTo(right) <= 0;
}
