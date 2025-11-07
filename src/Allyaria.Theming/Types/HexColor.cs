namespace Allyaria.Theming.Types;

/// <summary>Represents an immutable color theme with red, green, blue, and alpha channels.</summary>
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
///             <b>Named Colors</b> — case-insensitive lookup against the global <see cref="Colors" /> registry (e.g.,
///             <c>"Red500"</c>). The lookup uses the comparer configured in that registry (typically
///             <see cref="StringComparer.InvariantCultureIgnoreCase" />).
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
        pattern: "^#([0-9A-F]{3}|[0-9A-F]{4}|[0-9A-F]{6}|[0-9A-F]{8})$",
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Compiled regular expression that matches an <c>hsva(h, s%, v%, a)</c> color function containing hue, saturation, theme,
    /// and alpha components. Alpha values are validated against <see cref="AlphaPattern" />.
    /// </summary>
    private static readonly Regex HsvaPattern = new(
        pattern:
        @"^hsva\s*\(\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))\s*,\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))%?\s*,\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))%?\s*,\s*" +
        AlphaPattern + @"\s*\)$",
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Compiled regular expression that matches an <c>hsv(h, s%, v%)</c> color function containing hue, saturation, and theme
    /// components. The pattern supports optional signs and decimal fractions, but does <b>not</b> allow an alpha component
    /// (see <see cref="HsvaPattern" /> for that).
    /// </summary>
    private static readonly Regex HsvPattern = new(
        pattern:
        @"^hsv\s*\(\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))\s*,\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))%?\s*,\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+))%?\s*\)$",
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Compiled regular expression that matches modern CSS Color Level 4 syntax for <c>rgb(...)</c> or <c>rgba(...)</c> where
    /// channels are space-separated and the alpha component follows a slash—for example, <c>rgb(255 0 0 / .5)</c>. Falls back
    /// to the same channel validation as <see cref="RgbaPattern" />.
    /// </summary>
    private static readonly Regex RgbaCss4Pattern = new(
        pattern:
        @"^rgba?\s*\(\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+)%?)\s+([+-]?(?:\d+(?:\.\d+)?|\.\d+)%?)\s+([+-]?(?:\d+(?:\.\d+)?|\.\d+)%?)(?:\s*\/\s*"
        + AlphaPattern + @")?\s*\)$",
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Compiled regular expression that matches an <c>rgba(r, g, b, a)</c> color function with integer RGB channels and an
    /// explicit alpha component validated by <see cref="AlphaPattern" />. This corresponds to the traditional comma-separated
    /// CSS syntax.
    /// </summary>
    private static readonly Regex RgbaPattern = new(
        pattern:
        @"^rgba\s*\(\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*,\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*,\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*,\s*"
        + AlphaPattern + @"\s*\)$",
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Compiled regular expression that matches an <c>rgb(r, g, b)</c> color function consisting of three integer channel
    /// values in the 0–255 range. This pattern does not support alpha; use <see cref="RgbaPattern" /> or
    /// <see cref="RgbaCss4Pattern" /> for variants that do.
    /// </summary>
    private static readonly Regex RgbPattern = new(
        pattern:
        @"^rgb\s*\(\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*,\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*,\s*([+-]?(?:\d{1,3}(?:\.\d+)?%?))\s*\)$",
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Initializes a new instance of the <see cref="HexColor" /> struct with all channels default-initialized to 0.
    /// </summary>
    public HexColor()
        : this(red: new HexByte(), green: new HexByte(), blue: new HexByte(), alpha: new HexByte()) { }

    /// <summary>Initializes a new instance of the <see cref="HexColor" /> struct from individual channels.</summary>
    /// <param name="red">The red channel theme.</param>
    /// <param name="green">The green channel theme.</param>
    /// <param name="blue">The blue channel theme.</param>
    /// <param name="alpha">The optional alpha channel theme; if <see langword="null" />, defaults to 255 (opaque).</param>
    public HexColor(HexByte red, HexByte green, HexByte blue, HexByte? alpha = null)
    {
        R = red;
        G = green;
        B = blue;
        A = alpha ?? new HexByte(value: 255);

        RgbToHsv(hue: out var h, saturation: out var s, value: out var v);

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
    ///         ). This is attempted only if the theme does not start with <c>"#"</c>, <c>"rgb"</c>, or <c>"hsv"</c>.
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
        AryGuard.NotNullOrWhiteSpace(value: value, argName: nameof(value));

        HexByte red;
        HexByte green;
        HexByte blue;
        HexByte alpha;
        var trimmed = value.Trim();

        if (trimmed.StartsWith(value: "hsv", comparisonType: StringComparison.OrdinalIgnoreCase))
        {
            ParseHsva(value: trimmed, red: out red, green: out green, blue: out blue, alpha: out alpha);
        }
        else if (trimmed.StartsWith(value: "rgb", comparisonType: StringComparison.OrdinalIgnoreCase))
        {
            ParseRgba(value: trimmed, red: out red, green: out green, blue: out blue, alpha: out alpha);
        }
        else if (trimmed.StartsWith(value: "#", comparisonType: StringComparison.OrdinalIgnoreCase))
        {
            ParseHex(value: trimmed, red: out red, green: out green, blue: out blue, alpha: out alpha);
        }
        else if (!TryParseColorName(value: trimmed, red: out red, green: out green, blue: out blue, alpha: out alpha))
        {
            throw new AryArgumentException(message: $"Invalid color string: {value}.", argName: nameof(value));
        }

        R = red;
        G = green;
        B = blue;
        A = alpha;

        RgbToHsv(hue: out var h, saturation: out var s, value: out var v);

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
    /// Gets the <b>Saturation</b> component of the color, normalized to the range <c>[0, 1]</c>. A theme of <c>0</c> indicates
    /// a fully desaturated color (gray), while <c>1</c> indicates full color intensity.
    /// </summary>
    public double S { get; }

    /// <summary>
    /// Gets the <b>Value</b> (brightness) component of the color, normalized to the range <c>[0, 1]</c>. A theme of <c>0</c>
    /// represents black, and <c>1</c> represents the brightest possible version of the color.
    /// </summary>
    public double V { get; }

    /// <summary>
    /// Linearly interpolates the current <see cref="V" /> (theme/brightness) toward a specified target theme by a given blend
    /// factor within the closed interval <c>[0, 1]</c>.
    /// </summary>
    /// <param name="target">The target brightness theme to blend toward (0–1).</param>
    /// <param name="factor">
    /// The interpolation factor clamped to <c>[0, 1]</c>. A theme of <c>0</c> returns the current <see cref="V" />; a theme of
    /// <c>1</c> returns the <paramref name="target" />.
    /// </param>
    /// <returns>The interpolated scalar theme resulting from the linear blend.</returns>
    private double BlendValue(double target, double factor) => V + (target - V) * factor;

    /// <summary>Clamps a normalized channel (0–1) to a byte (0–255) using banker's rounding (ToEven).</summary>
    /// <param name="value">The normalized theme.</param>
    /// <returns>The clamped 8-bit channel theme.</returns>
    private static byte ClampToByte(double value)
        => (byte)Math.Clamp(value: Math.Round(value: value * 255.0, mode: MidpointRounding.ToEven), min: 0, max: 255);

    /// <summary>Compares this instance with another <see cref="HexColor" /> to determine relative ordering.</summary>
    /// <param name="other">The other color to compare.</param>
    /// <returns>
    /// A theme less than zero if this instance precedes <paramref name="other" />; zero if equal; greater than zero if this
    /// instance follows <paramref name="other" />.
    /// </returns>
    public int CompareTo(HexColor other)
        => (Red: R, Green: G, Blue: B, Alpha: A).CompareTo(other: (other.R, other.G, other.B, other.A));

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
        var lighter = Math.Max(val1: foregroundL, val2: backgroundL);
        var darker = Math.Min(val1: foregroundL, val2: backgroundL);
        var result = (lighter + 0.05) / (darker + 0.05);

        return double.IsNaN(d: result) || double.IsInfinity(d: result)
            ? throw new AryArgumentException(
                message: "The specified colors are not valid colors.", argName: nameof(background)
            )
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
            hue: H,
            saturation: Math.Clamp(value: S - Math.Clamp(value: desaturateBy, min: 0.0, max: 1.0), min: 0.0, max: 1.0),
            value: BlendValue(target: 0.5, factor: Math.Clamp(value: valueBlendTowardMid, min: 0.0, max: 1.0)),
            alpha: A.ToNormalized()
        );

    /// <summary>
    /// Resolves a foreground color that meets (or best-approaches) a minimum contrast ratio over the background by preserving
    /// the foreground hue and saturation (HSV H/S) and adjusting only theme (V). If that hue rail cannot reach the target
    /// (even at V = 0 or 1), the method mixes toward black and white and returns the closest solution that meets—or
    /// best-approaches—the target.
    /// </summary>
    /// <param name="background">Background color (opaque).</param>
    /// <param name="minimumRatio">Required minimum contrast ratio (1–21, e.g., <c>4.5</c> for body text).</param>
    /// <exception cref="AryArgumentException">Thrown when the minimum ratio is less than 1 or greater than 21.</exception>
    /// <returns>The resolved color.</returns>
    public HexColor EnsureContrast(HexColor background, double minimumRatio = 3.0)
    {
        AryGuard.InRange(value: minimumRatio, min: 1.0, max: 21.0, argName: nameof(minimumRatio));

        var startRatio = ContrastRatio(background: background);

        if (startRatio >= minimumRatio)
        {
            return this;
        }

        var direction = ValueDirection(background: background);

        // 1) Preferred theme-rail attempt
        var first = SearchValueRail(direction: direction, background: background, minimumRatio: minimumRatio);

        if (first.IsMinimumMet)
        {
            return first.ForegroundColor;
        }

        // 2) Poles
        var towardWhite = SearchTowardPole(
            pole: Colors.White.SetAlpha(alpha: A.Value), background: background, minimumRatio: minimumRatio
        );

        var towardBlack = SearchTowardPole(
            pole: Colors.Black.SetAlpha(alpha: A.Value), background: background, minimumRatio: minimumRatio
        );

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
        => R.Equals(other: other.R) && G.Equals(other: other.G) && B.Equals(other: other.B) && A.Equals(other: other.A);

    /// <summary>Determines whether the specified object is equal to the current instance.</summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is HexColor other && Equals(other: other);

    /// <summary>Creates a <see cref="HexColor" /> from HSVA component values.</summary>
    /// <param name="hue">The hue component in degrees (nominally 0–360; values are normalized).</param>
    /// <param name="saturation">The saturation component in the 0–1 range.</param>
    /// <param name="value">The theme (brightness) component in the 0–1 range.</param>
    /// <param name="alpha">The alpha component in the 0–1 range. Defaults to 1.0 (opaque).</param>
    /// <returns>A <see cref="HexColor" /> representing the specified HSVA.</returns>
    public static HexColor FromHsva(double hue, double saturation, double value, double alpha = 1.0)
    {
        var h = hue % 360.0;

        if (h < 0)
        {
            h += 360.0;
        }

        var s = Math.Clamp(value: saturation, min: 0.0, max: 1.0);
        var v = Math.Clamp(value: value, min: 0.0, max: 1.0);
        var a = HexByte.FromNormalized(value: Math.Clamp(value: alpha, min: 0.0, max: 1.0));

        return HsvaToRgba(hue: h, saturation: s, value: v, alpha: a);
    }

    /// <summary>Returns a hash code for this instance.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => HashCode.Combine(value1: R, value2: G, value3: B, value4: A);

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
        var x = chroma * (1.0 - Math.Abs(value: prime % 2.0 - 1.0));
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

        return new HexColor(
            red: ClampToByte(value: red + m), green: ClampToByte(value: green + m), blue: ClampToByte(value: blue + m),
            alpha: alpha
        );
    }

    /// <summary>
    /// Produces the photographic negative of the current color by inverting each RGB channel (i.e., <c>R' = 255 − R</c>,
    /// <c>G' = 255 − G</c>, <c>B' = 255 − B</c>). The alpha channel is preserved.
    /// </summary>
    /// <remarks>
    /// This matches the classic “negative image” operation performed in RGB space. The resulting hue/saturation/theme may not
    /// equal a simple 180° hue rotation; it is the exact per-channel inversion of the underlying RGB values.
    /// </remarks>
    /// <returns>A new <see cref="HexColor" /> representing the RGB-inverted (negative) color.</returns>
    public HexColor Invert()
    {
        var r = (byte)(255 - R.Value);
        var g = (byte)(255 - G.Value);
        var b = (byte)(255 - B.Value);

        return new HexColor(red: r, green: g, blue: b, alpha: A);
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
    public static HexColor Parse(string value) => new(value: value);

    /// <summary>
    /// Parses a normalized alpha component from a string value in the range [0, 1] and converts it to a <see cref="HexByte" />
    /// representation.
    /// </summary>
    /// <param name="value">
    /// The string containing the alpha value to parse. Leading and trailing whitespace are ignored. The value must represent a
    /// finite number within the inclusive range [0, 1].
    /// </param>
    /// <returns>A <see cref="HexByte" /> corresponding to the parsed and clamped alpha value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> cannot be parsed as a finite <see cref="double" /> or is outside the [0, 1]
    /// range.
    /// </exception>
    private static HexByte ParseAlpha(string value)
    {
        var trimmed = value.Trim();

        if (!double.TryParse(
                s: trimmed, style: NumberStyles.Float, provider: CultureInfo.InvariantCulture, result: out var alpha
            ) ||
            !double.IsFinite(d: alpha))
        {
            throw new AryArgumentException(message: $"Invalid alpha value: {value}", argName: nameof(value));
        }

        AryGuard.InRange(value: alpha, min: 0.0, max: 1.0, argName: nameof(value));

        return HexByte.FromNormalized(value: alpha);
    }

    /// <summary>
    /// Parses an integer channel value from a string and returns it as a <see cref="HexByte" />. The value is expected to
    /// represent an 8-bit channel in the range [0, 255].
    /// </summary>
    /// <param name="value">The string containing the channel value to parse.</param>
    /// <returns>A <see cref="HexByte" /> corresponding to the parsed channel value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> cannot be parsed as a byte or is outside the valid 0–255 range.
    /// </exception>
    private static HexByte ParseByte(string value)
        => byte.TryParse(
            s: value, style: NumberStyles.Integer, provider: CultureInfo.InvariantCulture, result: out var byteValue
        )
            ? new HexByte(value: byteValue)
            : throw new AryArgumentException(message: $"Byte theme is out of range: {value}", argName: nameof(value));

    /// <summary>
    /// Parses a color channel from a string that may be expressed either as an absolute value or as a percentage. Percentage
    /// values are normalized to the range [0, 1] before being converted to a <see cref="HexByte" />.
    /// </summary>
    /// <param name="value">
    /// The channel string to parse. If it ends with <c>'%'</c>, it is interpreted as a percentage in the range [0, 100];
    /// otherwise, it is parsed as an integer channel value in the range [0, 255].
    /// </param>
    /// <returns>A <see cref="HexByte" /> representing the parsed channel value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> cannot be parsed as a valid channel value or percentage, or when the numeric
    /// percentage is outside the 0–100 range.
    /// </exception>
    private static HexByte ParseChannel(string value)
    {
        var trimmed = value.Trim();

        if (!trimmed.EndsWith(value: '%'))
        {
            return ParseByte(value: trimmed);
        }

        var text = trimmed.TrimEnd(trimChar: '%').Trim();

        if (!double.TryParse(
                s: text, style: NumberStyles.Float, provider: CultureInfo.InvariantCulture, result: out var channel
            ) ||
            !double.IsFinite(d: channel))
        {
            throw new AryArgumentException(message: $"Invalid channel percentage: {value}", argName: nameof(value));
        }

        AryGuard.InRange(value: channel, min: 0.0, max: 100.0, argName: nameof(value));

        return HexByte.FromNormalized(value: channel);
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
        var match = HexColorPattern.Match(input: value.Trim());

        if (!match.Success)
        {
            throw new AryArgumentException(message: $"Invalid hex color format: {value}", argName: nameof(value));
        }

        var hexValue = match.Groups[groupnum: 1].Value;

        switch (hexValue.Length)
        {
            case 3:
                Span<char> buf3 = stackalloc char[8];
                buf3[index: 0] = hexValue[index: 0];
                buf3[index: 1] = hexValue[index: 0];
                buf3[index: 2] = hexValue[index: 1];
                buf3[index: 3] = hexValue[index: 1];
                buf3[index: 4] = hexValue[index: 2];
                buf3[index: 5] = hexValue[index: 2];
                buf3[index: 6] = 'F';
                buf3[index: 7] = 'F';
                hexValue = new string(value: buf3);

                break;

            case 4:
                Span<char> buf4 = stackalloc char[8];
                buf4[index: 0] = hexValue[index: 0];
                buf4[index: 1] = hexValue[index: 0];
                buf4[index: 2] = hexValue[index: 1];
                buf4[index: 3] = hexValue[index: 1];
                buf4[index: 4] = hexValue[index: 2];
                buf4[index: 5] = hexValue[index: 2];
                buf4[index: 6] = hexValue[index: 3];
                buf4[index: 7] = hexValue[index: 3];
                hexValue = new string(value: buf4);

                break;

            case 6:
                hexValue += "FF";

                break;

            case 8:
                break;
        }

        var r = byte.Parse(s: hexValue[..2], style: NumberStyles.HexNumber, provider: CultureInfo.InvariantCulture);

        var g = byte.Parse(
            s: hexValue.AsSpan(start: 2, length: 2), style: NumberStyles.HexNumber,
            provider: CultureInfo.InvariantCulture
        );

        var b = byte.Parse(
            s: hexValue.AsSpan(start: 4, length: 2), style: NumberStyles.HexNumber,
            provider: CultureInfo.InvariantCulture
        );

        var a = byte.Parse(
            s: hexValue.AsSpan(start: 6, length: 2), style: NumberStyles.HexNumber,
            provider: CultureInfo.InvariantCulture
        );

        red = new HexByte(value: r);
        green = new HexByte(value: g);
        blue = new HexByte(value: b);
        alpha = new HexByte(value: a);
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
        var match = HsvaPattern.Match(input: value);

        if (!match.Success)
        {
            match = HsvPattern.Match(input: value);

            if (!match.Success)
            {
                throw new AryArgumentException(message: $"Invalid HSV(A) color: {value}", argName: nameof(value));
            }
        }

        var h = ParseHue(value: match.Groups[groupnum: 1].Value);
        var s = ParsePercent(value: match.Groups[groupnum: 2].Value);
        var v = ParsePercent(value: match.Groups[groupnum: 3].Value);

        var a = match.Groups[groupname: "alpha"].Success
            ? ParseAlpha(value: match.Groups[groupname: "alpha"].Value)
            : new HexByte(value: 255);

        var color = HsvaToRgba(hue: h, saturation: s, value: v, alpha: a);

        red = color.R;
        green = color.G;
        blue = color.B;
        alpha = color.A;
    }

    /// <summary>Parses a hue theme from a string.</summary>
    /// <param name="value">The string containing the hue theme.</param>
    /// <returns>The parsed hue (not yet normalized).</returns>
    /// <exception cref="AryArgumentException">Thrown when the hue theme is invalid.</exception>
    private static double ParseHue(string value)
        => !double.TryParse(
                s: value, style: NumberStyles.Float, provider: CultureInfo.InvariantCulture, result: out var hue
            ) ||
            !double.IsFinite(d: hue)
                ? throw new AryArgumentException(message: $"Invalid hue theme: {value}", argName: nameof(value))
                : hue;

    /// <summary>
    /// Parses a percentage-like string into a fractional value in the range [0, 1]. Inputs may be given either as a raw
    /// fraction (e.g., <c>0.5</c>) or as a percentage (e.g., <c>50</c> or <c>50%</c>).
    /// </summary>
    /// <param name="value">
    /// The string containing the percentage. If it ends with <c>'%'</c>, it is treated as a percentage between 0 and 100;
    /// otherwise values ≤ 1 are interpreted as fractions and larger values as percentages.
    /// </param>
    /// <returns>The parsed percentage expressed as a normalized fraction in the range [0, 1].</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> cannot be parsed as a finite <see cref="double" /> or when the resulting
    /// percentage is outside the 0–100 range.
    /// </exception>
    private static double ParsePercent(string value)
    {
        var trimmed = value.Trim();
        var hadPercent = trimmed.EndsWith(value: "%", comparisonType: StringComparison.Ordinal);

        var numericText = hadPercent
            ? trimmed.TrimEnd(trimChar: '%').Trim()
            : trimmed;

        if (!double.TryParse(
                s: numericText, style: NumberStyles.Float, provider: CultureInfo.InvariantCulture,
                result: out var number
            ) ||
            !double.IsFinite(d: number))
        {
            throw new AryArgumentException(message: $"Invalid percentage value: {value}", argName: nameof(value));
        }

        var percent = hadPercent
            ? number
            : number <= 1.0
                ? number * 100.0
                : number;

        AryGuard.InRange(value: percent, min: 0.0, max: 100.0, argName: nameof(value));

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
        var match = RgbaPattern.Match(input: value);

        if (!match.Success)
        {
            match = RgbPattern.Match(input: value);

            if (!match.Success)
            {
                match = RgbaCss4Pattern.Match(input: value);

                if (!match.Success)
                {
                    throw new AryArgumentException(message: $"Invalid RGB(A) color: {value}", argName: nameof(value));
                }
            }
        }

        red = ParseChannel(value: match.Groups[groupnum: 1].Value);
        green = ParseChannel(value: match.Groups[groupnum: 2].Value);
        blue = ParseChannel(value: match.Groups[groupnum: 3].Value);

        alpha = match.Groups[groupname: "alpha"].Success
            ? match.Groups[groupname: "alpha"].Value.TrimEnd().EndsWith(
                value: "%", comparisonType: StringComparison.Ordinal
            )
                ? HexByte.FromNormalized(
                    value: Math.Clamp(
                        value: double.Parse(
                            s: match.Groups[groupname: "alpha"].Value.TrimEnd(trimChar: '%'),
                            style: NumberStyles.Float, provider: CultureInfo.InvariantCulture
                        ) / 100.0, min: 0.0, max: 1.0
                    )
                )
                : ParseAlpha(value: match.Groups[groupname: "alpha"].Value)
            : new HexByte(value: 255);
    }

    /// <summary>
    /// Converts the current RGB color components (<see cref="R" />, <see cref="G" />, <see cref="B" />) into their
    /// corresponding HSV (Hue, Saturation, Value) representation.
    /// </summary>
    /// <param name="hue">
    /// The resulting hue component, expressed in degrees within the range [0, 360). A theme of <c>0</c> represents red,
    /// <c>120</c> represents green, and <c>240</c> represents blue.
    /// </param>
    /// <param name="saturation">
    /// The resulting saturation component, a normalized theme in the range [0, 1], where <c>0</c> indicates a shade of gray
    /// and <c>1</c> represents full color intensity.
    /// </param>
    /// <param name="value">
    /// The resulting theme (brightness) component, a normalized theme in the range [0, 1], where <c>0</c> represents black and
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

        var maxByte = Math.Max(val1: red, val2: Math.Max(val1: green, val2: blue));
        var minByte = Math.Min(val1: red, val2: Math.Min(val1: green, val2: blue));

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
            var mid = Math.Clamp(value: 0.5 * (low + high), min: 0.0, max: 1.0);
            var candidate = ToLerpLinearPreserveAlpha(end: pole, factor: mid);
            var ratio = candidate.ContrastRatio(background: background);

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
            ? ToLerpLinearPreserveAlpha(end: pole, factor: high)
            : bestColor;

        var finalRatio = finalColor.ContrastRatio(background: background);

        return new ContrastResult(ForegroundColor: finalColor, ContrastRatio: finalRatio, IsMinimumMet: met);
    }

    /// <summary>
    /// Binary search along the HSV theme rail (holding H and S constant) to find the minimum-change V that meets a required
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
        var bestColor = FromHsva(hue: H, saturation: S, value: V, alpha: A.ToNormalized());

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
            var mid = Math.Clamp(value: 0.5 * (low + high), min: 0.0, max: 1.0);
            var candidate = FromHsva(hue: H, saturation: S, value: mid, alpha: A.ToNormalized());
            var ratio = candidate.ContrastRatio(background: background);

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
            return new ContrastResult(ForegroundColor: bestColor, ContrastRatio: bestRatio, IsMinimumMet: false);
        }

        var finalColor = FromHsva(hue: H, saturation: S, value: high, alpha: A.ToNormalized());
        var finalRatio = finalColor.ContrastRatio(background: background);

        return new ContrastResult(ForegroundColor: finalColor, ContrastRatio: finalRatio, IsMinimumMet: true);
    }

    /// <summary>
    /// Creates a new <see cref="HexColor" /> instance using the current red, green, and blue components, but with the
    /// specified alpha (opacity) theme.
    /// </summary>
    /// <param name="alpha">
    /// The new alpha component, expressed as a byte in the range <c>[0, 255]</c>, where <c>0</c> represents full transparency
    /// and <c>255</c> represents full opacity.
    /// </param>
    /// <returns>
    /// A new <see cref="HexColor" /> that is identical in color to the current instance but with the provided alpha theme
    /// applied.
    /// </returns>
    /// <remarks>
    /// This method does not modify the current instance; it returns a new color structure with the updated transparency
    /// component.
    /// </remarks>
    public HexColor SetAlpha(byte alpha) => new(red: R, green: G, blue: B, alpha: new HexByte(value: alpha));

    /// <summary>
    /// Adjusts the perceived lightness of the color by shifting its theme (<see cref="V" />) upward or downward depending on
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

        return FromHsva(
            hue: H, saturation: S, value: Math.Clamp(value: V + delta * direction, min: 0.0, max: 1.0),
            alpha: A.ToNormalized()
        );
    }

    /// <summary>
    /// Converts the current color into an accent variant suitable for emphasizing interactive or highlighted UI elements.
    /// Internally adjusts perceived lightness using <see cref="ShiftLightness(double)" /> with a stronger delta.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the accent variant of the current color.</returns>
    public HexColor ToAccent() => ShiftLightness(delta: 0.6);

    /// <summary>
    /// Produces a disabled-state variant of the current color by reducing saturation and slightly flattening perceived
    /// contrast. Intended for controls that are present but not interactive.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the disabled-state color.</returns>
    public HexColor ToDisabled() => Desaturate(desaturateBy: 0.6);

    /// <summary>
    /// Produces a dragged-state variant of the current color by adjusting lightness for improved visual feedback while a
    /// draggable element is being moved.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the dragged-state color.</returns>
    public HexColor ToDragged() => ShiftLightness(delta: 0.18);

    /// <summary>
    /// Produces a slightly elevated variant of the current color corresponding to the first elevation level in layered UI
    /// surfaces.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the elevation 1 color.</returns>
    public HexColor ToElevation1() => ShiftLightness(delta: 0.02);

    /// <summary>
    /// Produces an elevated variant of the current color corresponding to the second elevation level in layered UI surfaces.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the elevation 2 color.</returns>
    public HexColor ToElevation2() => ShiftLightness(delta: 0.04);

    /// <summary>
    /// Produces an elevated variant of the current color corresponding to the third elevation level in layered UI surfaces.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the elevation 3 color.</returns>
    public HexColor ToElevation3() => ShiftLightness(delta: 0.06);

    /// <summary>
    /// Produces an elevated variant of the current color corresponding to the fourth elevation level in layered UI surfaces.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the elevation 4 color.</returns>
    public HexColor ToElevation4() => ShiftLightness(delta: 0.08);

    /// <summary>
    /// Produces an elevated variant of the current color corresponding to the fifth elevation level in layered UI surfaces.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the elevation 5 color.</returns>
    public HexColor ToElevation5() => ShiftLightness(delta: 0.1);

    /// <summary>
    /// Produces a focused-state variant of the current color, typically used to render focus rings or outlines with enhanced
    /// prominence relative to the base color.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the focused-state color.</returns>
    public HexColor ToFocused() => ShiftLightness(delta: 0.1);

    /// <summary>
    /// Derives a high-contrast foreground variant from the current color by strongly adjusting lightness. Intended for text or
    /// iconography rendered on top of the base color.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> suitable for use as a foreground color.</returns>
    public HexColor ToForeground() => ShiftLightness(delta: 0.9);

    /// <summary>
    /// Produces a hover-state variant of the current color by slightly adjusting lightness to provide visual feedback when a
    /// pointer hovers over an interactive element.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the hovered-state color.</returns>
    public HexColor ToHovered() => ShiftLightness(delta: 0.06);

    /// <summary>
    /// Gamma-correct (linear-light) interpolation between two colors, including alpha. Uses linearization via
    /// <see cref="HexByte.ToLerpLinearHexByte" /> for perceptually better blends than plain sRGB lerp.
    /// </summary>
    /// <param name="end">InlineEnd color.</param>
    /// <param name="factor">Interpolation factor clamped to <c>[0, 1]</c>.</param>
    /// <returns>The interpolated color.</returns>
    public HexColor ToLerpLinear(HexColor end, double factor)
        => new(
            red: R.ToLerpLinearHexByte(end: end.R, factor: factor),
            green: G.ToLerpLinearHexByte(end: end.G, factor: factor),
            blue: B.ToLerpLinearHexByte(end: end.B, factor: factor),
            alpha: A.ToLerpHexByte(end: end.A, factor: factor)
        );

    /// <summary>
    /// Gamma-correct (linear-light) interpolation between two colors, preserving alpha. Uses linearization via
    /// <see cref="HexByte.ToLerpLinearHexByte" /> for perceptually better blends than plain sRGB lerp.
    /// </summary>
    /// <param name="end">InlineEnd color.</param>
    /// <param name="factor">Interpolation factor clamped to <c>[0, 1]</c>.</param>
    /// <returns>The interpolated color.</returns>
    public HexColor ToLerpLinearPreserveAlpha(HexColor end, double factor)
        => new(
            red: R.ToLerpLinearHexByte(end: end.R, factor: factor),
            green: G.ToLerpLinearHexByte(end: end.G, factor: factor),
            blue: B.ToLerpLinearHexByte(end: end.B, factor: factor),
            alpha: A
        );

    /// <summary>
    /// Produces a pressed-state variant of the current color by adjusting lightness to convey a deeper interaction state when
    /// a control is actively pressed or engaged.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the pressed-state color.</returns>
    public HexColor ToPressed() => ShiftLightness(delta: 0.14);

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

    /// <summary>
    /// Produces a visited-state variant of the current color by modestly reducing saturation while preserving general hue and
    /// brightness, making it suitable for indicating visited links or previously activated actions.
    /// </summary>
    /// <returns>A new <see cref="HexColor" /> representing the visited-state color.</returns>
    public HexColor ToVisited() => Desaturate(desaturateBy: 0.3);

    /// <summary>Attempts to parse the specified color string.</summary>
    /// <param name="value">The color string to parse.</param>
    /// <param name="result">When this method returns, contains the parsed color or default on failure.</param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out HexColor result)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            result = default(HexColor);

            return false;
        }

        try
        {
            result = new HexColor(value: value);

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

        if (!Colors.TryGet(name: value, value: out var color))
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

        var up = FromHsva(
            hue: H, saturation: S, value: Math.Clamp(value: V + step, min: 0.0, max: 1.0), alpha: A.ToNormalized()
        );

        var down = FromHsva(
            hue: H, saturation: S, value: Math.Clamp(value: V - step, min: 0.0, max: 1.0), alpha: A.ToNormalized()
        );

        var ratioUp = up.ContrastRatio(background: background);
        var ratioDown = down.ContrastRatio(background: background);

        return ratioUp > ratioDown
            ? +1
            : -1;
    }

    /// <summary>Determines whether two <see cref="HexColor" /> values are equal.</summary>
    /// <param name="left">The first theme to compare.</param>
    /// <param name="right">The second theme to compare.</param>
    /// <returns><see langword="true" /> if the values are equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(HexColor left, HexColor right) => left.Equals(other: right);

    /// <summary>Determines whether one <see cref="HexColor" /> is greater than another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if <paramref name="left" /> is greater; otherwise, <see langword="false" />.</returns>
    public static bool operator >(HexColor left, HexColor right) => left.CompareTo(other: right) > 0;

    /// <summary>Determines whether one <see cref="HexColor" /> is greater than or equal to another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool operator >=(HexColor left, HexColor right) => left.CompareTo(other: right) >= 0;

    /// <summary>Implicitly converts a color string to a <see cref="HexColor" />.</summary>
    /// <param name="hex">The color string to convert.</param>
    /// <returns>The resulting <see cref="HexColor" />.</returns>
    public static implicit operator HexColor(string hex) => new(value: hex);

    /// <summary>Implicitly converts a <see cref="HexColor" /> to its string representation.</summary>
    /// <param name="value">The color theme.</param>
    /// <returns>The string representation.</returns>
    public static implicit operator string(HexColor value) => value.ToString();

    /// <summary>Determines whether two <see cref="HexColor" /> values are not equal.</summary>
    /// <param name="left">The first theme to compare.</param>
    /// <param name="right">The second theme to compare.</param>
    /// <returns><see langword="true" /> if the values are not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(HexColor left, HexColor right) => !left.Equals(other: right);

    /// <summary>Determines whether one <see cref="HexColor" /> is less than another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool operator <(HexColor left, HexColor right) => left.CompareTo(other: right) < 0;

    /// <summary>Determines whether one <see cref="HexColor" /> is less than or equal to another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool operator <=(HexColor left, HexColor right) => left.CompareTo(other: right) <= 0;
}
