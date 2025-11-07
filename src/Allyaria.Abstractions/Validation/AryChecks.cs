namespace Allyaria.Abstractions.Validation;

/// <summary>
/// Provides a set of helper methods for argument validation, returning <see cref="AryArgumentException" /> instances when
/// conditions fail.
/// </summary>
internal static class AryChecks
{
    /// <summary>Validates that the specified enumeration value is defined within its type.</summary>
    /// <typeparam name="TEnum">The enumeration type.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="argName">The name of the argument being validated.</param>
    /// <returns>An <see cref="AryArgumentException" /> if the value is not defined; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? EnumDefined<TEnum>(TEnum value, string argName)
        where TEnum : struct, Enum
        => !Enum.IsDefined(value: value)
            ? new AryArgumentException(
                message: $"{argName} is not a valid value for this enum.", argName: argName, argValue: value
            )
            : null;

    /// <summary>Validates that two values are equal.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compare">The expected value to compare to.</param>
    /// <param name="argName">The name of the argument being validated.</param>
    /// <returns>An <see cref="AryArgumentException" /> if the values are not equal; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? EqualTo<T>(T value, T compare, string argName)
        where T : struct, IEquatable<T>
        => !value.Equals(other: compare)
            ? new AryArgumentException(
                message: $"{argName} must be equal to {compare}.", argName: argName, argValue: value
            )
            : null;

    /// <summary>Validates that a condition is <c>false</c>.</summary>
    /// <param name="condition">The boolean condition to evaluate.</param>
    /// <param name="argName">The name of the argument being validated.</param>
    /// <returns>An <see cref="AryArgumentException" /> if the condition is <c>true</c>; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? False(bool condition, string argName)
        => condition
            ? new AryArgumentException(message: $"{argName} must be false.", argName: argName)
            : null;

    /// <summary>Validates that a value is greater than a minimum exclusive value.</summary>
    /// <typeparam name="T">The comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="minExclusive">The minimum exclusive value.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <returns>An <see cref="AryArgumentException" /> if validation fails; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? GreaterThan<T>(T value, T minExclusive, string argName)
        where T : IComparable<T>
        => value.CompareTo(other: minExclusive) <= 0
            ? new AryArgumentException(
                message: $"{argName} must be greater than {minExclusive}.", argName: argName, argValue: value
            )
            : null;

    /// <summary>Validates that a value is greater than or equal to a specified minimum inclusive value.</summary>
    /// <typeparam name="T">The comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="minInclusive">The minimum inclusive value.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if validation fails; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? GreaterThanOrEqualTo<T>(T value, T minInclusive, string argName)
        where T : IComparable<T>
        => value.CompareTo(other: minInclusive) < 0
            ? new AryArgumentException(
                message: $"{argName} must be greater than or equal to {minInclusive}.", argName: argName,
                argValue: value
            )
            : null;

    /// <summary>Validates that a value lies within a specified inclusive range.</summary>
    /// <typeparam name="T">The comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value (inclusive).</param>
    /// <param name="max">The maximum value (inclusive).</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if out of range; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? InRange<T>(T value, T min, T max, string argName)
        where T : IComparable<T>
        => value.CompareTo(other: min) < 0 || value.CompareTo(other: max) > 0
            ? new AryArgumentException(
                message: $"{argName} must be between {min} and {max} (inclusive).", argName: argName, argValue: value
            )
            : null;

    /// <summary>Validates that an object is assignable to the specified target type.</summary>
    /// <typeparam name="TTarget">The target type.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if assignability fails; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? IsAssignableTo<TTarget>(object? value, string argName)
        => value is null || value is not TTarget
            ? new AryArgumentException(
                message: $"{argName} must be assignable to {typeof(TTarget).FullName}.", argName: argName,
                argValue: value
            )
            : null;

    /// <summary>Validates that a value is less than a maximum exclusive value.</summary>
    /// <typeparam name="T">The comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="maxExclusive">The maximum exclusive value.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if validation fails; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? LessThan<T>(T value, T maxExclusive, string argName)
        where T : IComparable<T>
        => value.CompareTo(other: maxExclusive) >= 0
            ? new AryArgumentException(
                message: $"{argName} must be less than {maxExclusive}.", argName: argName, argValue: value
            )
            : null;

    /// <summary>Validates that a value is less than or equal to a maximum inclusive value.</summary>
    /// <typeparam name="T">The comparable type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="maxInclusive">The maximum inclusive value.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if validation fails; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? LessThanOrEqualTo<T>(T value, T maxInclusive, string argName)
        where T : IComparable<T>
        => value.CompareTo(other: maxInclusive) > 0
            ? new AryArgumentException(
                message: $"{argName} must be less than or equal to {maxInclusive}.", argName: argName, argValue: value
            )
            : null;

    /// <summary>Validates that a value is not its type’s default.</summary>
    /// <typeparam name="T">The struct type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if value equals default; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? NotDefault<T>(T value, string argName)
        where T : struct, IEquatable<T>
        => value.Equals(other: default(T))
            ? new AryArgumentException(message: $"{argName} cannot be the default value.", argName: argName)
            : null;

    /// <summary>Validates that two values are not equal.</summary>
    /// <typeparam name="T">The struct type.</typeparam>
    /// <param name="value">The first value.</param>
    /// <param name="compare">The second value.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if values are equal; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? NotEqualTo<T>(T value, T compare, string argName)
        where T : struct, IEquatable<T>
        => value.Equals(other: compare)
            ? new AryArgumentException(
                message: $"{argName} must not be equal to {compare}.", argName: argName, argValue: value
            )
            : null;

    /// <summary>Validates that a reference or nullable value is not <c>null</c>.</summary>
    /// <typeparam name="T">The type of value.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if null; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? NotNull<T>(T? value, string argName)
        => value is null
            ? new AryArgumentException(message: $"{argName} cannot be null.", argName: argName)
            : null;

    /// <summary>Validates that a string is not null or empty.</summary>
    /// <param name="value">The string to check.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if null or empty; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? NotNullOrEmpty(string? value, string argName)
        => string.IsNullOrEmpty(value: value)
            ? new AryArgumentException(message: $"{argName} cannot be null or empty.", argName: argName)
            : null;

    /// <summary>Validates that a collection is not null or empty.</summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if null or empty; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? NotNullOrEmpty<T>(IReadOnlyCollection<T>? collection, string argName)
        => collection is null || collection.Count == 0
            ? new AryArgumentException(message: $"{argName} cannot be null or an empty collection.", argName: argName)
            : null;

    /// <summary>Validates that a string is not null, empty, or whitespace.</summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if invalid; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? NotNullOrWhiteSpace(string? value, string argName)
        => string.IsNullOrWhiteSpace(value: value)
            ? new AryArgumentException(message: $"{argName} cannot be null, empty, or whitespace.", argName: argName)
            : null;

    /// <summary>Validates that two values are of the same type.</summary>
    /// <typeparam name="T1">The first value type.</typeparam>
    /// <typeparam name="T2">The second value type.</typeparam>
    /// <param name="value1">The first value.</param>
    /// <param name="value2">The second value.</param>
    /// <param name="argName">The argument name.</param>
    /// <returns>An exception if type mismatch occurs; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? SameType<T1, T2>(T1 value1, T2 value2, string argName)
    {
        if (value1 is null || value2 is null)
        {
            return new AryArgumentException(
                message: $"{argName} cannot be compared because one or both values are null.", argName: argName,
                argValue: value1 ?? (object?)value2
            );
        }

        var type1 = value1.GetType();
        var type2 = value2.GetType();

        return type1 != type2
            ? new AryArgumentException(
                message: $"{argName} type mismatch: {type1.FullName} cannot be compared with {type2.FullName}.",
                argName: argName, argValue: value1
            )
            : null;
    }

    /// <summary>Validates a condition and throws a custom message if it fails.</summary>
    /// <param name="condition">The condition to test.</param>
    /// <param name="argName">The argument name.</param>
    /// <param name="message">The failure message.</param>
    /// <returns>An exception if condition is false; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? That(bool condition, string argName, string message)
        => !condition
            ? new AryArgumentException(message: message, argName: argName)
            : null;

    /// <summary>Validates that a condition is <c>true</c>.</summary>
    /// <param name="condition">The boolean condition to evaluate.</param>
    /// <param name="argName">The name of the argument being validated.</param>
    /// <returns>An <see cref="AryArgumentException" /> if the condition is <c>false</c>; otherwise, <c>null</c>.</returns>
    public static AryArgumentException? True(bool condition, string argName)
        => !condition
            ? new AryArgumentException(message: $"{argName} must be true.", argName: argName)
            : null;
}
