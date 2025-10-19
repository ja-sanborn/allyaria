namespace Allyaria.Theming.Contracts;

/// <summary>
/// Base type for strongly typed theming theme wrappers in Allyaria. Provides ordinal string-based comparison, equality,
/// hashing, and CSS formatting helpers.
/// </summary>
/// <remarks>
///     <para>
///     All ordering and equality semantics are based on the underlying <see cref="Value" /> string using
///     <see cref="StringComparison.Ordinal" />. Instances of different concrete types are not comparable and will cause an
///     <see cref="AryArgumentException" /> when compared.
///     </para>
/// </remarks>
public abstract class ThemeBase : IComparable<ThemeBase>, IEquatable<ThemeBase>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeBase" /> class by joining one or more raw string values.
    /// </summary>
    /// <param name="values">
    /// One or more raw string segments that will be concatenated with a comma (<c>,</c>) into the final <see cref="Value" />.
    /// If no items are provided, <see cref="string.Join(string?, object?[])" /> yields an empty string.
    /// </param>
    protected ThemeBase(params string[] values) => Value = string.Join(',', values);

    /// <summary>Gets the raw string theme represented by this instance.</summary>
    public virtual string Value { get; }

    /// <summary>Compares two <see cref="ThemeBase" /> instances for ordering using ordinal string comparison.</summary>
    /// <param name="left">The left-hand instance; may be <c>null</c>.</param>
    /// <param name="right">The right-hand instance; may be <c>null</c>.</param>
    /// <returns>
    /// A theme less than zero if <paramref name="left" /> is less than <paramref name="right" />, zero if they are equal, or
    /// greater than zero if <paramref name="left" /> is greater than <paramref name="right" />. If exactly one operand is
    /// <c>null</c>, the <c>null</c> operand is considered less.
    /// </returns>
    /// <exception cref="AryArgumentException">Thrown when both operands are non-<c>null</c> but their runtime types differ.</exception>
    public static int Compare(ThemeBase? left, ThemeBase? right)
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
            throw new AryArgumentException("Cannot compare values of different types.");
        }

        return string.Compare(left.Value, right.Value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Compares this instance to another <see cref="ThemeBase" /> instance using ordinal string comparison.
    /// </summary>
    /// <param name="other">The other theme to compare against; may be <c>null</c>.</param>
    /// <returns>
    /// A theme less than zero if this instance is less than <paramref name="other" />, zero if they are equal, or greater than
    /// zero if this instance is greater than <paramref name="other" />. A non-<c>null</c> instance is considered greater than
    /// <c>null</c>.
    /// </returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="other" /> is non-<c>null</c> and its runtime type differs from this instance.
    /// </exception>
    public int CompareTo(ThemeBase? other)
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
            throw new AryArgumentException("Cannot compare values of different types.");
        }

        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }

    /// <summary>Determines whether the specified object is equal to this instance.</summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="obj" /> is a <see cref="ThemeBase" /> of the same runtime type and its
    /// <see cref="Value" /> is ordinal-equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj) => obj is ThemeBase other && Equals(other);

    /// <summary>Determines whether the specified <see cref="ThemeBase" /> is equal to this instance.</summary>
    /// <param name="other">The other theme to compare with; may be <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if both instances are the same reference or both are non-<c>null</c>, of the same runtime type, and their
    /// <see cref="Value" /> strings are ordinal-equal; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(ThemeBase? other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other is null)
        {
            return false;
        }

        if (GetType() != other.GetType())
        {
            return false;
        }

        return string.Equals(Value, other.Value, StringComparison.Ordinal);
    }

    /// <summary>Determines whether two <see cref="ThemeBase" /> instances are equal.</summary>
    /// <param name="left">The left-hand instance; may be <c>null</c>.</param>
    /// <param name="right">The right-hand instance; may be <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if both are <c>null</c> or both are non-<c>null</c> and equal per <see cref="Equals(ThemeBase?)" />;
    /// otherwise, <c>false</c>.
    /// </returns>
    public static bool Equals(ThemeBase? left, ThemeBase? right)
        => (left is null && right is null) || (left is not null && left.Equals(right));

    /// <summary>Returns a hash code for this instance based on its <see cref="Value" /> using ordinal semantics.</summary>
    /// <returns>An ordinal hash code for <see cref="Value" />.</returns>
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);

    /// <summary>Converts the current theme into a CSS declaration string for the specified property.</summary>
    /// <param name="propertyName">
    /// The CSS property name. When provided, it is trimmed and lowercased using invariant culture. If <c>null</c> or
    /// whitespace, only the <see cref="Value" /> is returned.
    /// </param>
    /// <returns>
    /// A CSS declaration in the form <c>"property:theme;"</c> when <paramref name="propertyName" /> is provided; otherwise,
    /// the raw <see cref="Value" />.
    /// </returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="propertyName" /> is <c>null</c>, empty, whitespace-only, or contains any Unicode control
    /// characters.
    /// </exception>
    public string ToCss(string propertyName)
    {
        AryArgumentException.ThrowIfNullOrWhiteSpace(propertyName, nameof(propertyName));

        var trimmed = propertyName.Trim();

        var name = trimmed.StartsWith("--", StringComparison.Ordinal)
            ? trimmed
            : trimmed.ToLowerInvariant();

        return $"{name}:{Value};";
    }

    /// <summary>Returns the string representation of this instance.</summary>
    /// <returns>The raw <see cref="Value" />.</returns>
    public override string ToString() => Value;

    /// <summary>Validates and normalizes a raw string input for use in theming theme objects.</summary>
    /// <param name="value">The input string to validate. Must not be <c>null</c>, empty, or whitespace-only.</param>
    /// <returns>A trimmed version of <paramref name="value" />.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, empty, whitespace-only, or contains any Unicode control
    /// characters.
    /// </exception>
    protected static string ValidateInput(string value)
    {
        AryArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        var trimmed = value.Trim();

        return trimmed.Any(static c => char.IsControl(c))
            ? throw new AryArgumentException("Value contains control characters.", nameof(value), value)
            : trimmed;
    }

    /// <summary>Determines whether two instances are equal (operator).</summary>
    /// <param name="left">The left-hand instance; may be <c>null</c>.</param>
    /// <param name="right">The right-hand instance; may be <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if both are equal per <see cref="Compare(ThemeBase?, ThemeBase?)" />; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(ThemeBase? left, ThemeBase? right) => Compare(left, right) == 0;

    /// <summary>Determines whether one instance is greater than another (operator).</summary>
    /// <param name="left">The left-hand instance (must not be <c>null</c>).</param>
    /// <param name="right">The right-hand instance (must not be <c>null</c>).</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="AryArgumentException">Thrown when the runtime types of the operands differ.</exception>
    public static bool operator >(ThemeBase left, ThemeBase right) => left.CompareTo(right) > 0;

    /// <summary>Determines whether one instance is greater than or equal to another (operator).</summary>
    /// <param name="left">The left-hand instance (must not be <c>null</c>).</param>
    /// <param name="right">The right-hand instance (must not be <c>null</c>).</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="AryArgumentException">Thrown when the runtime types of the operands differ.</exception>
    public static bool operator >=(ThemeBase left, ThemeBase right) => left.CompareTo(right) >= 0;

    /// <summary>Determines whether two instances are not equal (operator).</summary>
    /// <param name="left">The left-hand instance; may be <c>null</c>.</param>
    /// <param name="right">The right-hand instance; may be <c>null</c>.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ThemeBase? left, ThemeBase? right) => !(left == right);

    /// <summary>Determines whether one instance is less than another (operator).</summary>
    /// <param name="left">The left-hand instance (must not be <c>null</c>).</param>
    /// <param name="right">The right-hand instance (must not be <c>null</c>).</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="AryArgumentException">Thrown when the runtime types of the operands differ.</exception>
    public static bool operator <(ThemeBase left, ThemeBase right) => left.CompareTo(right) < 0;

    /// <summary>Determines whether one instance is less than or equal to another (operator).</summary>
    /// <param name="left">The left-hand instance (must not be <c>null</c>).</param>
    /// <param name="right">The right-hand instance (must not be <c>null</c>).</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="AryArgumentException">Thrown when the runtime types of the operands differ.</exception>
    public static bool operator <=(ThemeBase left, ThemeBase right) => left.CompareTo(right) <= 0;
}
