namespace Allyaria.Abstractions.Extensions;

/// <summary>
/// Provides a set of extension methods for <see cref="AryValidation{T}" /> that enable fluent-style argument validation
/// using <see cref="AryChecks" />.
/// </summary>
public static class AryValidationExtensions
{
    /// <summary>Validates that the current value is within the specified exclusive range.</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="min">The minimum exclusive bound.</param>
    /// <param name="max">The maximum exclusive bound.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> Between<T>(this AryValidation<T> validation, T min, T max)
        where T : IComparable<T>
    {
        validation.Add(
            ex: AryChecks.Between(value: validation.ArgValue, min: min, max: max, argName: validation.ArgName)
        );

        return validation;
    }

    /// <summary>Validates that the current argument value is a defined enumeration constant.</summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <returns>The same <see cref="AryValidation{T}" /> for chaining.</returns>
    public static AryValidation<T> EnumDefined<T>(this AryValidation<T> validation)
        where T : struct, Enum
    {
        validation.Add(ex: AryChecks.EnumDefined(value: validation.ArgValue, argName: validation.ArgName));

        return validation;
    }

    /// <summary>Validates that the current value equals a specified comparison value.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="compare">The value to compare against.</param>
    /// <returns>The same <see cref="AryValidation{T}" /> for chaining.</returns>
    public static AryValidation<T> EqualTo<T>(this AryValidation<T> validation, T compare)
        where T : IEquatable<T>
    {
        validation.Add(
            ex: AryChecks.EqualTo(value: validation.ArgValue, compare: compare, argName: validation.ArgName)
        );

        return validation;
    }

    /// <summary>Validates that the specified condition is <c>false</c>.</summary>
    /// <typeparam name="T">The type of the argument being validated.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="condition">The condition to test.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> False<T>(this AryValidation<T> validation, bool condition)
    {
        validation.Add(ex: AryChecks.False(condition: condition, argName: validation.ArgName));

        return validation;
    }

    /// <summary>Validates that the current value is greater than a specified minimum (exclusive).</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="minExclusive">The exclusive lower bound.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> GreaterThan<T>(this AryValidation<T> validation, T minExclusive)
        where T : IComparable<T>
    {
        validation.Add(
            ex: AryChecks.GreaterThan(
                value: validation.ArgValue, minExclusive: minExclusive, argName: validation.ArgName
            )
        );

        return validation;
    }

    /// <summary>Validates that the current value is greater than or equal to a specified minimum (inclusive).</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="minInclusive">The inclusive lower bound.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> GreaterThanOrEqualTo<T>(this AryValidation<T> validation, T minInclusive)
        where T : IComparable<T>
    {
        validation.Add(
            ex: AryChecks.GreaterThanOrEqualTo(
                value: validation.ArgValue, minInclusive: minInclusive, argName: validation.ArgName
            )
        );

        return validation;
    }

    /// <summary>Validates that the current value is within the specified inclusive range.</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="min">The minimum inclusive bound.</param>
    /// <param name="max">The maximum inclusive bound.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> InRange<T>(this AryValidation<T> validation, T min, T max)
        where T : IComparable<T>
    {
        validation.Add(
            ex: AryChecks.InRange(value: validation.ArgValue, min: min, max: max, argName: validation.ArgName)
        );

        return validation;
    }

    /// <summary>Validates that the current value is assignable to a target type.</summary>
    /// <typeparam name="T">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> IsAssignableTo<T, TTarget>(this AryValidation<T> validation)
    {
        validation.Add(ex: AryChecks.IsAssignableTo<TTarget>(value: validation.ArgValue, argName: validation.ArgName));

        return validation;
    }

    /// <summary>Validates that the current value is less than the specified maximum (exclusive).</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="maxExclusive">The exclusive upper bound.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> LessThan<T>(this AryValidation<T> validation, T maxExclusive)
        where T : IComparable<T>
    {
        validation.Add(
            ex: AryChecks.LessThan(value: validation.ArgValue, maxExclusive: maxExclusive, argName: validation.ArgName)
        );

        return validation;
    }

    /// <summary>Validates that the current value is less than or equal to the specified maximum (inclusive).</summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="maxInclusive">The inclusive upper bound.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> LessThanOrEqualTo<T>(this AryValidation<T> validation, T maxInclusive)
        where T : IComparable<T>
    {
        validation.Add(
            ex: AryChecks.LessThanOrEqualTo(
                value: validation.ArgValue, maxInclusive: maxInclusive, argName: validation.ArgName
            )
        );

        return validation;
    }

    /// <summary>Validates that the current value is not its type’s default value.</summary>
    /// <typeparam name="T">A struct type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> NotDefault<T>(this AryValidation<T> validation)
        where T : struct, IEquatable<T>
    {
        validation.Add(ex: AryChecks.NotDefault(value: validation.ArgValue, argName: validation.ArgName));

        return validation;
    }

    /// <summary>Validates that the current value is not equal to the specified comparison value.</summary>
    /// <typeparam name="T">A struct type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="compare">The value to compare against.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> NotEqualTo<T>(this AryValidation<T> validation, T compare)
        where T : IEquatable<T>
    {
        validation.Add(
            ex: AryChecks.NotEqualTo(value: validation.ArgValue, compare: compare, argName: validation.ArgName)
        );

        return validation;
    }

    /// <summary>Validates that the current value is not <c>null</c>.</summary>
    /// <typeparam name="T">The type of the argument being validated.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> NotNull<T>(this AryValidation<T> validation)
    {
        validation.Add(ex: AryChecks.NotNull(value: validation.ArgValue, argName: validation.ArgName));

        return validation;
    }

    /// <summary>Validates that the current string value is not null or empty.</summary>
    /// <param name="validation">The current validation context.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<string?> NotNullOrEmpty(this AryValidation<string?> validation)
    {
        validation.Add(ex: AryChecks.NotNullOrEmpty(value: validation.ArgValue, argName: validation.ArgName));

        return validation;
    }

    /// <summary>Validates that the current collection is not null or empty.</summary>
    /// <typeparam name="TItem">The type of elements in the collection.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<IReadOnlyCollection<TItem>?> NotNullOrEmpty<TItem>(
        this AryValidation<IReadOnlyCollection<TItem>?> validation)
    {
        validation.Add(ex: AryChecks.NotNullOrEmpty(collection: validation.ArgValue, argName: validation.ArgName));

        return validation;
    }

    /// <summary>Validates that the current string value is not null, empty, or whitespace.</summary>
    /// <param name="validation">The current validation context.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<string?> NotNullOrWhiteSpace(this AryValidation<string?> validation)
    {
        validation.Add(ex: AryChecks.NotNullOrWhiteSpace(value: validation.ArgValue, argName: validation.ArgName));

        return validation;
    }

    /// <summary>Validates that the current value is of the same runtime type as another specified value.</summary>
    /// <typeparam name="T">The current argument type.</typeparam>
    /// <typeparam name="TOther">The comparison type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="other">The value to compare against.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> SameTypeAs<T, TOther>(this AryValidation<T> validation, TOther other)
    {
        validation.Add(ex: AryChecks.SameType(value1: validation.ArgValue, value2: other, argName: validation.ArgName));

        return validation;
    }

    /// <summary>Validates that a specified condition is <c>true</c>.</summary>
    /// <typeparam name="T">The argument type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="condition">The boolean condition to evaluate.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> True<T>(this AryValidation<T> validation, bool condition)
    {
        validation.Add(ex: AryChecks.True(condition: condition, argName: validation.ArgName));

        return validation;
    }

    /// <summary>Validates a custom condition and associates a custom message when it fails.</summary>
    /// <typeparam name="T">The argument type.</typeparam>
    /// <param name="validation">The current validation context.</param>
    /// <param name="condition">The boolean condition to evaluate.</param>
    /// <param name="message">The error message to associate with the failure.</param>
    /// <returns>The same validation context.</returns>
    public static AryValidation<T> When<T>(this AryValidation<T> validation, bool condition, string message)
    {
        validation.Add(ex: AryChecks.When(condition: condition, argName: validation.ArgName, message: message));

        return validation;
    }
}
