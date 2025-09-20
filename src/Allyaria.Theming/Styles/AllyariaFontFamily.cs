using Allyaria.Theming.Helpers;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>font-family</c> CSS value.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> implements value-based equality and accepts a sequence of
/// font family names. Each name is normalized by trimming whitespace, applying quotes when necessary, and deduplicating
/// while preserving order.
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns <c>font-family:value1,value2,...;</c> (no spaces after commas).
/// <see cref="ToString" /> calls <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct AllyariaFontFamily : IEquatable<AllyariaFontFamily>
{
    /// <summary>Backing field that stores the normalized font families.</summary>
    private readonly string[] _families;

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaFontFamily" /> struct from an array of raw family names.
    /// </summary>
    /// <param name="families">An array of font family names.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="families" /> is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when no valid family names remain after normalization.</exception>
    public AllyariaFontFamily(string[] families)
    {
        if (families is null || families.Length == 0)
        {
            throw new ArgumentNullException(nameof(families), "font-family array cannot be null or empty.");
        }

        var normalized = StyleHelpers.NormalizeFontFamily(families);

        if (normalized is null || normalized.Length == 0)
        {
            throw new ArgumentException(
                "font-family array produced no valid names after normalization.", nameof(families)
            );
        }

        _families = normalized;
    }

    /// <summary>Gets the normalized font family array.</summary>
    public string[] Families => _families;

    /// <summary>Determines whether the specified object is equal to the current instance (value equality).</summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is AllyariaFontFamily other && Equals(other);

    /// <summary>
    /// Determines whether the specified <see cref="AllyariaFontFamily" /> is equal to the current instance (value equality).
    /// </summary>
    /// <param name="other">The other instance to compare.</param>
    /// <returns>
    /// <see langword="true" /> if both contain the same normalized sequence; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(AllyariaFontFamily other)
    {
        if (_families.Length != other._families.Length)
        {
            return false;
        }

        return !_families.Where((t, i) => !string.Equals(t, other._families[i], StringComparison.Ordinal)).Any();
    }

    /// <summary>Returns a hash code for this instance based on all normalized family names.</summary>
    /// <returns>A 32-bit signed hash code.</returns>
    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var f in _families)
        {
            hash.Add(f, StringComparer.Ordinal);
        }

        return hash.ToHashCode();
    }

    /// <summary>
    /// Generates a CSS declaration in the form <c>font-family:value1,value2,...;</c> (no spaces after commas).
    /// </summary>
    /// <returns>The CSS declaration for the font families.</returns>
    public string ToCss() => $"font-family:{string.Join(",", _families)};";

    /// <summary>Returns the same output as <see cref="ToCss" />.</summary>
    /// <returns>The CSS declaration string.</returns>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaFontFamily" /> using value equality.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(AllyariaFontFamily left, AllyariaFontFamily right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaFontFamily" />.</summary>
    /// <param name="families">The raw font family array to convert.</param>
    /// <returns>An <see cref="AllyariaFontFamily" /> created from <paramref name="families" />.</returns>
    public static implicit operator AllyariaFontFamily(string[] families) => new(families);

    /// <summary>Implicit conversion from <see cref="AllyariaFontFamily" /> to <see cref="string" />.</summary>
    /// <param name="fontFamily">The <see cref="AllyariaFontFamily" /> instance.</param>
    /// <returns>The normalized font family array.</returns>
    public static implicit operator string[](AllyariaFontFamily fontFamily) => fontFamily._families;

    /// <summary>Inequality operator for <see cref="AllyariaFontFamily" /> using value equality.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(AllyariaFontFamily left, AllyariaFontFamily right) => !left.Equals(right);
}
