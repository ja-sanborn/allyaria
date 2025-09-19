namespace Allyaria.Theming.Palette;

public readonly partial struct AllyariaColor
{
    /// <summary>Returns <c>true</c> if the two colors are equal.</summary>
    public static bool operator ==(AllyariaColor left, AllyariaColor right) => left.Equals(right);

    /// <summary>
    /// Returns <c>true</c> if <paramref name="left" /> is greater than <paramref name="right" /> (by <c>#RRGGBBAA</c>
    /// ordering).
    /// </summary>
    public static bool operator >(AllyariaColor left, AllyariaColor right) => left.CompareTo(right) > 0;

    /// <summary>
    /// Returns <c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />.
    /// </summary>
    public static bool operator >=(AllyariaColor left, AllyariaColor right) => left.CompareTo(right) >= 0;

    /// <summary>Implicit conversion from <see cref="string" /> by parsing.</summary>
    /// <param name="value">A supported color string.</param>
    public static implicit operator AllyariaColor(string value) => new(value);

    /// <summary>Implicit conversion to <see cref="string" /> using <see cref="ToString" /> (i.e., <c>#RRGGBBAA</c>).</summary>
    public static implicit operator string(AllyariaColor value) => value.ToString();

    /// <summary>Returns <c>true</c> if the two colors are not equal.</summary>
    public static bool operator !=(AllyariaColor left, AllyariaColor right) => !left.Equals(right);

    /// <summary>
    /// Returns <c>true</c> if <paramref name="left" /> is less than <paramref name="right" /> (by <c>#RRGGBBAA</c> ordering).
    /// </summary>
    public static bool operator <(AllyariaColor left, AllyariaColor right) => left.CompareTo(right) < 0;

    /// <summary>Returns <c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />.</summary>
    public static bool operator <=(AllyariaColor left, AllyariaColor right) => left.CompareTo(right) <= 0;
}
