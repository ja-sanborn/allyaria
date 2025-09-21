namespace Allyaria.Theming.Abstractions;

/// <summary>
/// Provides a base record type for style values used in theming. Encapsulates a string-based value and supports comparison
/// and ordering.
/// </summary>
public abstract record StyleValueBase : IComparable<StyleValueBase>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleValueBase" /> class. Trims whitespace from the provided value.
    /// </summary>
    /// <param name="value">
    /// The raw value to assign. If <c>null</c> or empty, an empty string will be used. Whitespace will be trimmed.
    /// </param>
    protected StyleValueBase(string value = "") => Value = value.Trim();

    /// <summary>Gets the style value represented as a string.</summary>
    public virtual string Value { get; }

    /// <summary>
    /// Compares the current instance to another <see cref="StyleValueBase" /> object to determine sort order.
    /// </summary>
    /// <param name="other">The other <see cref="StyleValueBase" /> instance to compare against.</param>
    /// <returns>
    /// A signed integer that indicates the relative order of the objects:
    /// <list type="bullet">
    ///     <item>
    ///         <description>Less than zero: this instance precedes <paramref name="other" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>Zero: this instance occurs in the same position as <paramref name="other" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>Greater than zero: this instance follows <paramref name="other" />.</description>
    ///     </item>
    /// </list>
    /// If <paramref name="other" /> is <c>null</c>, the current instance is considered greater.
    /// </returns>
    public int CompareTo(StyleValueBase? other)
        => other is null
            ? 1
            : string.Compare(Value, other.Value, StringComparison.Ordinal);

    /// <summary>Determines whether one <see cref="StyleValueBase" /> is greater than another.</summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator >(StyleValueBase left, StyleValueBase right) => left.CompareTo(right) > 0;

    /// <summary>Determines whether one <see cref="StyleValueBase" /> is greater than or equal to another.</summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator >=(StyleValueBase left, StyleValueBase right) => left.CompareTo(right) >= 0;

    /// <summary>Determines whether one <see cref="StyleValueBase" /> is less than another.</summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator <(StyleValueBase left, StyleValueBase right) => left.CompareTo(right) < 0;

    /// <summary>Determines whether one <see cref="StyleValueBase" /> is less than or equal to another.</summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator <=(StyleValueBase left, StyleValueBase right) => left.CompareTo(right) <= 0;
}
