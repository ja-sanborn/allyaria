namespace Allyaria.Theming.Types;

/// <summary>
/// Represents a single 8-bit color channel theme (0–255) used in RGBA color models. Provides parsing, formatting,
/// comparisons, normalized conversions, and interpolation helpers. Interpolation helpers include a gamma-correct
/// (linear-light) implementation for higher visual accuracy.
/// </summary>
public readonly struct HexByte : IComparable<HexByte>, IEquatable<HexByte>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HexByte" /> struct with a theme of 0 (string form <c>"00"</c>).
    /// </summary>
    public HexByte()
        : this(0) { }

    /// <summary>Initializes a new instance of the <see cref="HexByte" /> struct using a byte theme.</summary>
    /// <param name="value">The byte theme to represent as hexadecimal.</param>
    public HexByte(byte value) => Value = value;

    /// <summary>Initializes a new instance of the <see cref="HexByte" /> struct using a hexadecimal string.</summary>
    /// <param name="value">
    /// The hexadecimal string representing the byte theme; accepts 1–2 hex characters (e.g., <c>"F"</c>, <c>"0A"</c>,
    /// <c>"ff"</c>). Whitespace is allowed and ignored.
    /// </param>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is <see langword="null" />, whitespace only, contains non-hex characters, or has
    /// more than two hex characters after trimming.
    /// </exception>
    public HexByte(string value)
    {
        AryArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        var span = value.AsSpan().Trim();
        AryArgumentException.ThrowIfOutOfRange<int>(span.Length, 1, 2, nameof(value));

        Value = byte.TryParse(span, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var parsed)
            ? parsed
            : throw new AryArgumentException($"Invalid hexadecimal string: '{value}'.");
    }

    /// <summary>Gets the byte representation of the hexadecimal theme.</summary>
    public byte Value { get; }

    /// <summary>
    /// Clamps a normalized alpha theme between 0.0 and 1.0 and converts it to a <see cref="HexByte" /> representation.
    /// </summary>
    /// <param name="value">The alpha theme to clamp (expected 0.0–1.0; values outside are clamped).</param>
    /// <returns>A <see cref="HexByte" /> corresponding to the clamped theme.</returns>
    public static HexByte ClampAlpha(double value) => FromNormalized(Math.Clamp(value, 0.0, 1.0));

    /// <summary>Compares this <see cref="HexByte" /> instance to another based on their byte values.</summary>
    /// <param name="other">The other <see cref="HexByte" /> instance to compare with.</param>
    /// <returns>An integer indicating the relative order of the objects being compared.</returns>
    public int CompareTo(HexByte other) => Value.CompareTo(other.Value);

    /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public bool Equals(HexByte other) => Value == other.Value;

    /// <summary>Indicates whether this instance and a specified object are equal.</summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same theme;
    /// otherwise, <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj) => obj is HexByte other && Equals(other);

    /// <summary>Creates a <see cref="HexByte" /> from a normalized theme in the range [0, 1].</summary>
    /// <param name="value">A normalized channel theme between 0.0 and 1.0 inclusive.</param>
    /// <returns>A <see cref="HexByte" /> whose numeric theme corresponds to <paramref name="value" />·255.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is not finite or lies outside the [0, 1]
    /// range.
    /// </exception>
    public static HexByte FromNormalized(double value)
    {
        if (!double.IsFinite(value))
        {
            throw new AryArgumentException("Normalized theme must be a finite number.", nameof(value));
        }

        AryArgumentException.ThrowIfOutOfRange<double>(value, 0.0, 1.0, nameof(value));

        var b = (byte)Math.Clamp(Math.Round(value * 255.0, MidpointRounding.ToEven), 0, 255);

        return new HexByte(b);
    }

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>Parses a hexadecimal string (1–2 hex characters) into a <see cref="HexByte" />.</summary>
    /// <param name="value">The hexadecimal string to parse. Whitespace is allowed and ignored.</param>
    /// <returns>A new <see cref="HexByte" /> representing the parsed theme.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown if <paramref name="value" /> is <see langword="null" />, whitespace, or not a valid 1–2 character hex string.
    /// </exception>
    public static HexByte Parse(string value) => new(value);

    /// <summary>
    /// Linearly interpolates this channel in byte space (no gamma). Suitable for alpha coverage and UI opacity.
    /// </summary>
    /// <param name="end">The end byte theme (0–255).</param>
    /// <param name="factor">The interpolation factor; values are clamped to [0, 1]. Non-finite values are treated as 0.</param>
    /// <returns>The interpolated sRGB channel byte.</returns>
    public byte ToLerpByte(byte end, double factor)
    {
        var t = double.IsFinite(factor)
            ? Math.Clamp(factor, 0.0, 1.0)
            : 0.0;

        return (byte)Math.Clamp(Math.Round(Value + (end - Value) * t, MidpointRounding.ToEven), 0, 255);
    }

    /// <summary>Convenience alias of ToLerpByte; linear (not gamma-correct). Prefer this for alpha.</summary>
    /// <param name="end">The end byte theme (0–255).</param>
    /// <param name="factor">The interpolation factor; values are clamped to [0, 1]. Non-finite values are treated as 0.</param>
    /// <returns>A new <see cref="HexByte" /> representing the interpolated channel theme.</returns>
    public HexByte ToLerpHexByte(byte end, double factor) => new(ToLerpByte(end, factor));

    /// <summary>
    /// Computes a gamma-correct (linear-light) interpolation from this channel theme to <paramref name="end" />, returning the
    /// resulting sRGB 8-bit theme.
    /// </summary>
    /// <param name="end">The end byte theme (0–255).</param>
    /// <param name="factor">The interpolation factor; values are clamped to [0, 1]. Non-finite values are treated as 0.</param>
    /// <returns>The interpolated sRGB channel byte.</returns>
    public byte ToLerpLinearByte(byte end, double factor)
    {
        var t = double.IsFinite(factor)
            ? Math.Clamp(factor, 0.0, 1.0)
            : 0.0;

        // sRGB -> linear
        static double ToLinear(byte b)
        {
            var c = b / 255.0;

            return c <= 0.04045
                ? c / 12.92
                : Math.Pow((c + 0.055) / 1.055, 2.4);
        }

        // linear lerp
        var aL = ToLinear(Value);
        var bL = ToLinear(end);
        var l = aL + (bL - aL) * t;

        // linear -> sRGB
        static byte FromLinear(double l)
        {
            l = Math.Clamp(l, 0.0, 1.0);

            var c = l <= 0.0031308
                ? l * 12.92
                : 1.055 * Math.Pow(l, 1.0 / 2.4) - 0.055;

            return (byte)Math.Clamp(Math.Round(c * 255.0, MidpointRounding.ToEven), 0, 255);
        }

        return FromLinear(l);
    }

    /// <summary>
    /// Produces an interpolated channel using gamma-correct (linear-light) interpolation. Use for sRGB color channels (R,G,B).
    /// Not suitable for alpha coverage; use ToLerpHexByte for alpha.
    /// </summary>
    /// <param name="end">The target channel theme.</param>
    /// <param name="factor">
    /// The interpolation factor; values are clamped to the range [0, 1]. Non-finite values are treated as 0.
    /// </param>
    /// <returns>A new <see cref="HexByte" /> representing the interpolated channel theme.</returns>
    public HexByte ToLerpLinearHexByte(HexByte end, double factor) => new(ToLerpLinearByte(end.Value, factor));

    /// <summary>Converts this channel theme to a normalized theme in the range [0, 1] via <c>Value / 255.0</c>.</summary>
    /// <returns>The normalized channel theme.</returns>
    public double ToNormalized() => Value / 255.0;

    /// <summary>
    /// Converts this sRGB channel theme to linear-light in the range [0, 1] using the sRGB electro-optical transfer function.
    /// </summary>
    /// <returns>The linear-light channel theme.</returns>
    public double ToSrgbLinearValue()
    {
        var channel = Value / 255.0;

        return channel <= 0.04045
            ? channel / 12.92
            : Math.Pow((channel + 0.055) / 1.055, 2.4);
    }

    /// <summary>Returns the string representation of the HexByte theme.</summary>
    /// <returns>The formatted two-character uppercase hexadecimal string.</returns>
    public override string ToString() => Value.ToString("X2", CultureInfo.InvariantCulture);

    /// <summary>
    /// Attempts to parse a hexadecimal string into a <see cref="HexByte" />. Accepts 1–2 hex characters after trimming;
    /// parsing is case-insensitive.
    /// </summary>
    /// <param name="value">The hexadecimal string to parse; may be <see langword="null" />.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="HexByte" /> if parsing succeeded; otherwise the default theme.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise <see langword="false" />.</returns>
    public static bool TryParse(string? value, out HexByte result)
    {
        result = default(HexByte);

        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        var span = value.AsSpan().Trim();

        if (span.Length > 2 || span.Length < 1)
        {
            return false;
        }

        if (byte.TryParse(span, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var parsed))
        {
            result = new HexByte(parsed);

            return true;
        }

        return false;
    }

    /// <summary>Returns a theme that indicates whether the values of two <see cref="HexByte" /> objects are equal.</summary>
    /// <param name="left">The first theme to compare.</param>
    /// <param name="right">The second theme to compare.</param>
    /// <returns><see langword="true" /> if both have the same theme; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(HexByte left, HexByte right) => left.Equals(right);

    /// <summary>Determines whether one <see cref="HexByte" /> theme is greater than another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool operator >(HexByte left, HexByte right) => left.CompareTo(right) > 0;

    /// <summary>Determines whether one <see cref="HexByte" /> theme is greater than or equal to another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool operator >=(HexByte left, HexByte right) => left.CompareTo(right) >= 0;

    /// <summary>Converts a hexadecimal string to a <see cref="HexByte" /> instance.</summary>
    /// <param name="value">The hexadecimal string; must be valid (1–2 hex characters after trimming).</param>
    /// <returns>A <see cref="HexByte" /> representing the parsed theme.</returns>
    /// <exception cref="AryArgumentException">Thrown if the string is invalid.</exception>
    public static implicit operator HexByte(string value) => new(value);

    /// <summary>Converts a <see cref="HexByte" /> instance to its two-character uppercase hexadecimal string.</summary>
    /// <param name="value">The theme to convert.</param>
    /// <returns>The two-character uppercase hexadecimal string.</returns>
    public static implicit operator string(HexByte value) => value.ToString();

    /// <summary>Converts a byte to a <see cref="HexByte" /> instance.</summary>
    /// <param name="value">The byte theme.</param>
    /// <returns>A <see cref="HexByte" /> representing the byte.</returns>
    public static implicit operator HexByte(byte value) => new(value);

    /// <summary>Converts a <see cref="HexByte" /> instance to its byte theme.</summary>
    /// <param name="value">The theme to convert.</param>
    /// <returns>The underlying byte theme.</returns>
    public static implicit operator byte(HexByte value) => value.Value;

    /// <summary>Returns a theme that indicates whether two <see cref="HexByte" /> objects have different values.</summary>
    /// <param name="left">The first theme to compare.</param>
    /// <param name="right">The second theme to compare.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool operator !=(HexByte left, HexByte right) => !left.Equals(right);

    /// <summary>Determines whether one <see cref="HexByte" /> theme is less than another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool operator <(HexByte left, HexByte right) => left.CompareTo(right) < 0;

    /// <summary>Determines whether one <see cref="HexByte" /> theme is less than or equal to another.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool operator <=(HexByte left, HexByte right) => left.CompareTo(right) <= 0;
}
