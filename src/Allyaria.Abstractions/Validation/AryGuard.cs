using System.Runtime.CompilerServices;

namespace Allyaria.Abstractions.Validation;

/// <summary>
/// Provides guard clauses for argument validation by invoking <see cref="AryChecks" /> and throwing
/// <see cref="AryArgumentException" /> when validation fails.
/// </summary>
public static class AryGuard
{
    /// <summary>Ensures that the specified value lies within the given exclusive range.</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The exclusive lower bound.</param>
    /// <param name="max">The exclusive upper bound.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the value is outside the range.</exception>
    public static void Between<T>(T value,
        T min,
        T max,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        where T : IComparable<T>
        => CheckError(
            ex: AryChecks.Between(
                value: value, min: min, max: max, argName: argName.OrDefault(defaultValue: nameof(value))
            )
        );

    /// <summary>Throws the specified <see cref="AryArgumentException" /> if it is not null.</summary>
    /// <param name="ex">The exception to check and potentially throw.</param>
    private static void CheckError(AryArgumentException? ex)
    {
        if (ex is not null)
        {
            throw ex;
        }
    }

    /// <summary>Ensures that the specified enumeration value is defined within the enumeration type.</summary>
    /// <typeparam name="TEnum">The enumeration type.</typeparam>
    /// <param name="value">The enumeration value to validate.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the value is not defined in the enumeration.</exception>
    public static void EnumDefined<TEnum>(TEnum value,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        where TEnum : struct, Enum
        => CheckError(ex: AryChecks.EnumDefined(value: value, argName: argName.OrDefault(defaultValue: nameof(value))));

    /// <summary>Ensures that the specified value is equal to another value.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The first value to compare.</param>
    /// <param name="compare">The second value to compare.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the values are not equal.</exception>
    public static void EqualTo<T>(T value,
        T compare,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        where T : IEquatable<T>
        => CheckError(
            ex: AryChecks.EqualTo(
                value: value, compare: compare, argName: argName.OrDefault(defaultValue: nameof(value))
            )
        );

    /// <summary>Ensures that the specified condition is false.</summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the condition is true.</exception>
    public static void False(bool condition,
        [CallerArgumentExpression(parameterName: nameof(condition))]
        string? argName = null)
        => CheckError(
            ex: AryChecks.False(condition: condition, argName: argName.OrDefault(defaultValue: nameof(condition)))
        );

    /// <summary>Creates a new <see cref="AryValidation{T}" /> context for the specified value.</summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <returns>A new validation context.</returns>
    public static AryValidation<T> For<T>(T value,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        => new(argValue: value, argName: argName.OrDefault(defaultValue: nameof(value)));

    /// <summary>Ensures that the specified value is greater than the given minimum (exclusive).</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="minExclusive">The exclusive minimum bound.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the value is less than or equal to the minimum.</exception>
    public static void GreaterThan<T>(T value,
        T minExclusive,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        where T : IComparable<T>
        => CheckError(
            ex: AryChecks.GreaterThan(
                value: value, minExclusive: minExclusive, argName: argName.OrDefault(defaultValue: nameof(value))
            )
        );

    /// <summary>Ensures that the specified value is greater than or equal to the given minimum (inclusive).</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="minInclusive">The inclusive minimum bound.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the value is less than the minimum.</exception>
    public static void GreaterThanOrEqualTo<T>(T value,
        T minInclusive,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        where T : IComparable<T>
        => CheckError(
            ex: AryChecks.GreaterThanOrEqualTo(
                value: value, minInclusive: minInclusive, argName: argName.OrDefault(defaultValue: nameof(value))
            )
        );

    /// <summary>Ensures that the specified value lies within the given inclusive range.</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The inclusive lower bound.</param>
    /// <param name="max">The inclusive upper bound.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the value is outside the range.</exception>
    public static void InRange<T>(T value,
        T min,
        T max,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        where T : IComparable<T>
        => CheckError(
            ex: AryChecks.InRange(
                value: value, min: min, max: max, argName: argName.OrDefault(defaultValue: nameof(value))
            )
        );

    /// <summary>Ensures that the given value is assignable to the specified target type.</summary>
    /// <typeparam name="TTarget">The target type.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the value cannot be assigned to the target type.</exception>
    public static void IsAssignableTo<TTarget>(object? value,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        => CheckError(
            ex: AryChecks.IsAssignableTo<TTarget>(value: value, argName: argName.OrDefault(defaultValue: nameof(value)))
        );

    /// <summary>Ensures that the specified value is less than the given maximum (exclusive).</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="maxExclusive">The exclusive upper bound.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the value is greater than or equal to the maximum.</exception>
    public static void LessThan<T>(T value,
        T maxExclusive,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        where T : IComparable<T>
        => CheckError(
            ex: AryChecks.LessThan(
                value: value, maxExclusive: maxExclusive, argName: argName.OrDefault(defaultValue: nameof(value))
            )
        );

    /// <summary>Ensures that the specified value is less than or equal to the given maximum (inclusive).</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="maxInclusive">The inclusive upper bound.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the value is greater than the maximum.</exception>
    public static void LessThanOrEqualTo<T>(T value,
        T maxInclusive,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        where T : IComparable<T>
        => CheckError(
            ex: AryChecks.LessThanOrEqualTo(
                value: value, maxInclusive: maxInclusive, argName: argName.OrDefault(defaultValue: nameof(value))
            )
        );

    /// <summary>Ensures that the specified value is not equal to its default value.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the value equals its default.</exception>
    public static void NotDefault<T>(T value,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        where T : struct, IEquatable<T>
        => CheckError(ex: AryChecks.NotDefault(value: value, argName: argName.OrDefault(defaultValue: nameof(value))));

    /// <summary>Ensures that the specified value is not equal to another value.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The first value to compare.</param>
    /// <param name="compare">The value to compare against.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the values are equal.</exception>
    public static void NotEqualTo<T>(T value,
        T compare,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        where T : IEquatable<T>
        => CheckError(
            ex: AryChecks.NotEqualTo(
                value: value, compare: compare, argName: argName.OrDefault(defaultValue: nameof(value))
            )
        );

    /// <summary>Ensures that the specified value is not null.</summary>
    /// <typeparam name="T">The reference type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the value is null.</exception>
    public static void NotNull<T>(T? value,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        => CheckError(ex: AryChecks.NotNull(value: value, argName: argName.OrDefault(defaultValue: nameof(value))));

    /// <summary>Ensures that the specified string is not null or empty.</summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the string is null or empty.</exception>
    public static void NotNullOrEmpty(string? value,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        => CheckError(
            ex: AryChecks.NotNullOrEmpty(value: value, argName: argName.OrDefault(defaultValue: nameof(value)))
        );

    /// <summary>Ensures that the specified collection is not null or empty.</summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the collection is null or empty.</exception>
    public static void NotNullOrEmpty<T>(IReadOnlyCollection<T>? collection,
        [CallerArgumentExpression(parameterName: nameof(collection))]
        string? argName = null)
        => CheckError(
            ex: AryChecks.NotNullOrEmpty(
                collection: collection, argName: argName.OrDefault(defaultValue: nameof(collection))
            )
        );

    /// <summary>Ensures that the specified string is not null or whitespace.</summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the string is null, empty, or whitespace only.</exception>
    public static void NotNullOrWhiteSpace(string? value,
        [CallerArgumentExpression(parameterName: nameof(value))]
        string? argName = null)
        => CheckError(
            ex: AryChecks.NotNullOrWhiteSpace(value: value, argName: argName.OrDefault(defaultValue: nameof(value)))
        );

    /// <summary>Ensures that the two provided values are of the same type.</summary>
    /// <typeparam name="T1">The type of the first value.</typeparam>
    /// <typeparam name="T2">The type of the second value.</typeparam>
    /// <param name="value1">The first value to compare.</param>
    /// <param name="value2">The second value to compare.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the types differ.</exception>
    public static void SameType<T1, T2>(T1 value1,
        T2 value2,
        [CallerArgumentExpression(parameterName: nameof(value1))]
        string? argName = null)
        => CheckError(
            ex: AryChecks.SameType(
                value1: value1, value2: value2, argName: argName.OrDefault(defaultValue: nameof(value1))
            )
        );

    /// <summary>Ensures that the specified condition is true.</summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="argName">The argument name, automatically captured.</param>
    /// <exception cref="AryArgumentException">Thrown if the condition is false.</exception>
    public static void True(bool condition,
        [CallerArgumentExpression(parameterName: nameof(condition))]
        string? argName = null)
        => CheckError(
            ex: AryChecks.True(condition: condition, argName: argName.OrDefault(defaultValue: nameof(condition)))
        );

    /// <summary>Ensures that a condition is true; throws otherwise.</summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="argName">The argument name.</param>
    /// <param name="message">The message describing the violation.</param>
    /// <exception cref="AryArgumentException">Thrown if the condition is false.</exception>
    public static void When(bool condition, string argName, string message)
        => CheckError(ex: AryChecks.When(condition: condition, argName: argName, message: message));
}
