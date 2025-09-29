namespace Allyaria.Theming.Contracts;

/// <summary>
/// Represents a base class for theming values in Allyaria. Provides comparison, equality, and formatting functionality for
/// strongly typed value wrappers.
/// </summary>
public abstract class ValueBase : IComparable<ValueBase>, IEquatable<ValueBase>
{
    /// <summary>Initializes a new instance of the <see cref="ValueBase" /> class.</summary>
    /// <param name="values">The underlying string value.</param>
    protected ValueBase(params string[] values) => Value = string.Join(',', values);

    /// <summary>Gets the raw string value represented by this instance.</summary>
    public virtual string Value { get; }

    /// <summary>Compares two <see cref="ValueBase" /> instances for ordering.</summary>
    /// <param name="left">The left-hand instance.</param>
    /// <param name="right">The right-hand instance.</param>
    /// <returns>
    /// A value less than zero if <paramref name="left" /> is less than <paramref name="right" />, zero if they are equal, or
    /// greater than zero if <paramref name="left" /> is greater than <paramref name="right" />.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when types of <paramref name="left" /> and <paramref name="right" /> differ.</exception>
    public static int Compare(ValueBase? left, ValueBase? right)
    {
        if (left is null && right is null)
        {
            return 0;
        }

        if (left is null)
        {
            return -1;
        }

        if (right is null)
        {
            return 1;
        }

        if (left.GetType() != right.GetType())
        {
            throw new ArgumentException("Cannot compare values of different types.");
        }

        return string.Compare(left.Value, right.Value, StringComparison.Ordinal);
    }

    /// <summary>Compares this instance to another <see cref="ValueBase" /> instance.</summary>
    /// <param name="other">The other value to compare against.</param>
    /// <returns>
    /// A value less than zero if this instance is less than <paramref name="other" />, zero if they are equal, or greater than
    /// zero if this instance is greater than <paramref name="other" />.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the type of <paramref name="other" /> differs from this instance.</exception>
    public int CompareTo(ValueBase? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        if (GetType() != other.GetType())
        {
            throw new ArgumentException("Cannot compare values of different types.");
        }

        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }

    /// <summary>Determines whether the specified object is equal to this instance.</summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns><c>true</c> if the specified object is equal to this instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj) => obj is ValueBase other && Equals(other);

    /// <summary>Determines whether the specified <see cref="ValueBase" /> is equal to this instance.</summary>
    /// <param name="other">The other value to compare with.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(ValueBase? other) => Compare(this, other) is 0;

    /// <summary>Determines whether two <see cref="ValueBase" /> instances are equal.</summary>
    /// <param name="left">The left-hand instance.</param>
    /// <param name="right">The right-hand instance.</param>
    /// <returns><c>true</c> if both instances are equal; otherwise, <c>false</c>.</returns>
    public static bool Equals(ValueBase? left, ValueBase? right) => left?.Equals(right) is true;

    /// <summary>Returns a hash code for this instance based on its <see cref="Value" />.</summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);

    /// <summary>Converts the current value into a CSS declaration string for the specified property.</summary>
    /// <param name="propertyName">The CSS property name. If <c>null</c> or whitespace, only the value is returned.</param>
    /// <returns>
    /// A CSS declaration in the format <c>"property: value;"</c> if <paramref name="propertyName" /> is provided; otherwise,
    /// just the <see cref="Value" />.
    /// </returns>
    public string ToCss(string propertyName)
        => string.IsNullOrWhiteSpace(propertyName)
            ? Value
            : $"{propertyName.ToLowerInvariant().Trim()}:{Value};";

    /// <summary>Returns the string representation of this instance.</summary>
    /// <returns>The raw string value.</returns>
    public override string ToString() => Value;

    /// <summary>Validates and normalizes a raw string input for use in theming value objects.</summary>
    /// <param name="value">The input string to validate. Must not be <c>null</c>, empty, or whitespace-only.</param>
    /// <returns>A trimmed version of <paramref name="value" /> with leading and trailing whitespace removed.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, empty, consists only of whitespace, or contains disallowed
    /// control characters (all Unicode control characters except tab, line feed, and carriage return).
    /// </exception>
    protected static string ValidateInput(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        var trimmed = value.Trim();

        return trimmed.Any(static c => char.IsControl(c))
            ? throw new ArgumentException("Value contains control characters.", nameof(value))
            : trimmed;
    }

    /// <summary>Determines whether two instances are equal.</summary>
    public static bool operator ==(ValueBase? left, ValueBase? right) => Compare(left, right) == 0;

    /// <summary>Determines whether one instance is greater than another.</summary>
    public static bool operator >(ValueBase left, ValueBase right) => left.CompareTo(right) > 0;

    /// <summary>Determines whether one instance is greater than or equal to another.</summary>
    public static bool operator >=(ValueBase left, ValueBase right) => left.CompareTo(right) >= 0;

    /// <summary>Determines whether two instances are not equal.</summary>
    public static bool operator !=(ValueBase? left, ValueBase? right) => !(left == right);

    /// <summary>Determines whether one instance is less than another.</summary>
    public static bool operator <(ValueBase left, ValueBase right) => left.CompareTo(right) < 0;

    /// <summary>Determines whether one instance is less than or equal to another.</summary>
    public static bool operator <=(ValueBase left, ValueBase right) => left.CompareTo(right) <= 0;
}
