namespace Allyaria.Abstractions.Validation;

/// <summary>
/// Provides guard clauses for argument validation by invoking <see cref="AryChecks" /> and throwing
/// <see cref="AryArgumentException" /> when validation fails.
/// </summary>
public static class AryGuard
{
    /// <summary>Throws the specified <see cref="AryArgumentException" /> when it is not <c>null</c>.</summary>
    /// <param name="ex">The exception instance to evaluate and potentially throw.</param>
    /// <remarks>
    /// This helper centralizes the guard behavior for all methods in <see cref="AryGuard" /> so that any non-null validation
    /// error produced by <see cref="AryChecks" /> is immediately surfaced as an exception.
    /// </remarks>
    /// <exception cref="AryArgumentException">Thrown when <paramref name="ex" /> is not <c>null</c>.</exception>
    private static void CheckError(AryArgumentException? ex)
    {
        if (ex is not null)
        {
            throw ex;
        }
    }

    /// <summary>Ensures that the specified enumeration value is defined in its enum type.</summary>
    /// <typeparam name="TEnum">The enumeration type.</typeparam>
    /// <param name="value">The enumeration value to check.</param>
    /// <param name="argName">The name of the argument being validated.</param>
    /// <exception cref="AryArgumentException">Thrown when the value is not defined in the enum type.</exception>
    public static void EnumDefined<TEnum>(TEnum value, string argName)
        where TEnum : struct, Enum
        => CheckError(ex: AryChecks.EnumDefined(value: value, argName: argName));

    /// <summary>Ensures that the specified value equals the expected comparison value.</summary>
    /// <typeparam name="T">The type of the values being compared.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compare">The value to compare against.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the two values are not equal.</exception>
    public static void EqualTo<T>(T value, T compare, string argName)
        where T : struct, IEquatable<T>
        => CheckError(ex: AryChecks.EqualTo(value: value, compare: compare, argName: argName));

    /// <summary>Ensures that the specified boolean condition is <c>false</c>.</summary>
    /// <param name="condition">The condition to validate.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the condition is <c>true</c>.</exception>
    public static void False(bool condition, string argName)
        => CheckError(ex: AryChecks.False(condition: condition, argName: argName));

    /// <summary>Creates a validation context for a given value.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An <see cref="AryValidation{T}" /> for fluent validation chaining.</returns>
    public static AryValidation<T> For<T>(T value, string argName) => new(argValue: value, argName: argName);

    /// <summary>Ensures that the specified value is greater than the given minimum exclusive value.</summary>
    /// <typeparam name="T">The comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="minExclusive">The minimum exclusive bound.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the value is not greater than the minimum.</exception>
    public static void GreaterThan<T>(T value, T minExclusive, string argName)
        where T : IComparable<T>
        => CheckError(ex: AryChecks.GreaterThan(value: value, minExclusive: minExclusive, argName: argName));

    /// <summary>Ensures that the specified value is greater than or equal to the given minimum inclusive value.</summary>
    /// <typeparam name="T">The comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="minInclusive">The minimum inclusive bound.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the value is less than the minimum inclusive bound.</exception>
    public static void GreaterThanOrEqualTo<T>(T value, T minInclusive, string argName)
        where T : IComparable<T>
        => CheckError(ex: AryChecks.GreaterThanOrEqualTo(value: value, minInclusive: minInclusive, argName: argName));

    /// <summary>Ensures that the specified value is within the given inclusive range.</summary>
    /// <typeparam name="T">The comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The inclusive lower bound.</param>
    /// <param name="max">The inclusive upper bound.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the value is outside the specified range.</exception>
    public static void InRange<T>(T value, T min, T max, string argName)
        where T : IComparable<T>
        => CheckError(ex: AryChecks.InRange(value: value, min: min, max: max, argName: argName));

    /// <summary>Ensures that an object is assignable to the specified target type.</summary>
    /// <typeparam name="TTarget">The expected assignable type.</typeparam>
    /// <param name="value">The object to test.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the object is not assignable to the specified type.</exception>
    public static void IsAssignableTo<TTarget>(object? value, string argName)
        => CheckError(ex: AryChecks.IsAssignableTo<TTarget>(value: value, argName: argName));

    /// <summary>Ensures that a value is less than the specified maximum exclusive value.</summary>
    /// <typeparam name="T">The comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="maxExclusive">The exclusive upper bound.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the value is not less than the maximum.</exception>
    public static void LessThan<T>(T value, T maxExclusive, string argName)
        where T : IComparable<T>
        => CheckError(ex: AryChecks.LessThan(value: value, maxExclusive: maxExclusive, argName: argName));

    /// <summary>Ensures that a value is less than or equal to the specified maximum inclusive value.</summary>
    /// <typeparam name="T">The comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="maxInclusive">The inclusive upper bound.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the value exceeds the maximum inclusive bound.</exception>
    public static void LessThanOrEqualTo<T>(T value, T maxInclusive, string argName)
        where T : IComparable<T>
        => CheckError(ex: AryChecks.LessThanOrEqualTo(value: value, maxInclusive: maxInclusive, argName: argName));

    /// <summary>Ensures that a value is not its type’s default value.</summary>
    /// <typeparam name="T">The struct type.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the value equals its type’s default.</exception>
    public static void NotDefault<T>(T value, string argName)
        where T : struct, IEquatable<T>
        => CheckError(ex: AryChecks.NotDefault(value: value, argName: argName));

    /// <summary>Ensures that two values are not equal.</summary>
    /// <typeparam name="T">The struct type.</typeparam>
    /// <param name="value">The first value.</param>
    /// <param name="compare">The value to compare against.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the values are equal.</exception>
    public static void NotEqualTo<T>(T value, T compare, string argName)
        where T : struct, IEquatable<T>
        => CheckError(ex: AryChecks.NotEqualTo(value: value, compare: compare, argName: argName));

    /// <summary>Ensures that a value is not <c>null</c>.</summary>
    /// <typeparam name="T">The type of value.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the value is <c>null</c>.</exception>
    public static void NotNull<T>(T? value, string argName)
        => CheckError(ex: AryChecks.NotNull(value: value, argName: argName));

    /// <summary>Ensures that a string value is not null or empty.</summary>
    /// <param name="value">The string to test.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the string is null or empty.</exception>
    public static void NotNullOrEmpty(string? value, string argName)
        => CheckError(ex: AryChecks.NotNullOrEmpty(value: value, argName: argName));

    /// <summary>Ensures that a collection is not null or empty.</summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    /// <param name="collection">The collection to test.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the collection is null or empty.</exception>
    public static void NotNullOrEmpty<T>(IReadOnlyCollection<T>? collection, string argName)
        => CheckError(ex: AryChecks.NotNullOrEmpty(collection: collection, argName: argName));

    /// <summary>Ensures that a string is not null, empty, or whitespace.</summary>
    /// <param name="value">The string to test.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the string is null, empty, or whitespace.</exception>
    public static void NotNullOrWhiteSpace(string? value, string argName)
        => CheckError(ex: AryChecks.NotNullOrWhiteSpace(value: value, argName: argName));

    /// <summary>Ensures that two values are of the same runtime type.</summary>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <param name="value1">The first value.</param>
    /// <param name="value2">The second value.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the values differ in runtime type or one is null.</exception>
    public static void SameType<T1, T2>(T1 value1, T2 value2, string argName)
        => CheckError(ex: AryChecks.SameType(value1: value1, value2: value2, argName: argName));

    /// <summary>Ensures that a specified condition evaluates to <c>true</c>; throws a custom message if not.</summary>
    /// <param name="condition">The condition to test.</param>
    /// <param name="argName">The argument name.</param>
    /// <param name="message">The message for the thrown exception.</param>
    /// <exception cref="AryArgumentException">Thrown when the condition evaluates to <c>false</c>.</exception>
    public static void That(bool condition, string argName, string message)
        => CheckError(ex: AryChecks.That(condition: condition, argName: argName, message: message));

    /// <summary>Ensures that the specified condition is <c>true</c>.</summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="argName">The argument name.</param>
    /// <exception cref="AryArgumentException">Thrown when the condition is <c>false</c>.</exception>
    public static void True(bool condition, string argName)
        => CheckError(ex: AryChecks.True(condition: condition, argName: argName));
}
