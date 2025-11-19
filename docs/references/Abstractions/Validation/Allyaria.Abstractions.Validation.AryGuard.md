# Allyaria.Abstractions.Validation.AryGuard

`AryGuard` provides a rich set of guard clauses for validating method arguments. It leverages the underlying `AryChecks`
validation helpers and automatically throws `AryArgumentException` instances when conditions fail. By using
`[CallerArgumentExpression]`, it captures argument names automatically, reducing boilerplate and making validation
expressive and safe.

This class is ideal for precondition checks at method entry points and constructors, ensuring robust and fail-fast
argument validation.

## Constructors

`AryGuard` is a static class and cannot be instantiated.

## Properties

*None*

## Methods

| Name                                                                            | Returns            | Description                                                                      |
|---------------------------------------------------------------------------------|--------------------|----------------------------------------------------------------------------------|
| `Between<T>(T value, T min, T max, string? argName = null)`                     | `void`             | Ensures that `value` lies strictly between `min` and `max` (exclusive).          |
| `Check(bool condition, string argName, string message)`                         | `void`             | Validates a custom condition and throws an exception if it evaluates to `false`. |
| `EnumDefined<TEnum>(TEnum value, string? argName = null)`                       | `void`             | Ensures that the specified enumeration value is defined within its type.         |
| `EqualTo<T>(T value, T compare, string? argName = null)`                        | `void`             | Ensures that the specified value equals another given value.                     |
| `False(bool condition, string? argName = null)`                                 | `void`             | Ensures that the specified condition is `false`.                                 |
| `For<T>(T value, string? argName = null)`                                       | `AryValidation<T>` | Creates a new `AryValidation<T>` context for fluent or aggregated validation.    |
| `GreaterThan<T>(T value, T minExclusive, string? argName = null)`               | `void`             | Ensures that `value` is greater than the given minimum (exclusive).              |
| `GreaterThanOrEqualTo<T>(T value, T minInclusive, string? argName = null)`      | `void`             | Ensures that `value` is greater than or equal to the given minimum (inclusive).  |
| `InRange<T>(T value, T min, T max, string? argName = null)`                     | `void`             | Ensures that `value` lies within the inclusive range between `min` and `max`.    |
| `IsAssignableTo<TTarget>(object? value, string? argName = null)`                | `void`             | Ensures that `value` is assignable to the specified target type.                 |
| `LessThan<T>(T value, T maxExclusive, string? argName = null)`                  | `void`             | Ensures that `value` is less than the specified maximum (exclusive).             |
| `LessThanOrEqualTo<T>(T value, T maxInclusive, string? argName = null)`         | `void`             | Ensures that `value` is less than or equal to the specified maximum (inclusive). |
| `NotDefault<T>(T value, string? argName = null)`                                | `void`             | Ensures that `value` is not equal to its type’s default value.                   |
| `NotEqualTo<T>(T value, T compare, string? argName = null)`                     | `void`             | Ensures that `value` does not equal another specified value.                     |
| `NotNull<T>(T? value, string? argName = null)`                                  | `void`             | Ensures that the specified value is not `null`.                                  |
| `NotNullOrEmpty(string? value, string? argName = null)`                         | `void`             | Ensures that a string is not `null` or empty.                                    |
| `NotNullOrEmpty<T>(IReadOnlyCollection<T>? collection, string? argName = null)` | `void`             | Ensures that a collection is not `null` or empty.                                |
| `NotNullOrWhiteSpace(string? value, string? argName = null)`                    | `void`             | Ensures that a string is not `null`, empty, or whitespace.                       |
| `SameType<T1, T2>(T1 value1, T2 value2, string? argName = null)`                | `void`             | Ensures that the two provided values have the same runtime type.                 |
| `True(bool condition, string? argName = null)`                                  | `void`             | Ensures that the specified condition is `true`.                                  |

## Operators

*None*

## Events

*None*

## Exceptions

| Exception Type         | Description                                                     |
|------------------------|-----------------------------------------------------------------|
| `AryArgumentException` | Thrown by all guard methods when the argument fails validation. |

## Example

```csharp
using System;
using Allyaria.Abstractions.Validation;

public class Example
{
    public void CreateUser(string? username, int age)
    {
        AryGuard.NotNullOrWhiteSpace(username);
        AryGuard.InRange(age, 18, 100);

        Console.WriteLine($"User '{username}' created successfully at age {age}.");
    }

    public void AssignRole(object role)
    {
        AryGuard.IsAssignableTo<string>(role);
        Console.WriteLine($"Assigned role: {role}");
    }

    public void CheckExample()
    {
        AryGuard.True(5 > 3);
        AryGuard.False(string.IsNullOrEmpty("Valid"));

        // Fluent validation
        AryGuard.For("Admin")
            .NotNullOrWhiteSpace()
            .Check(value: true, "Custom rule passed.")
            .ThrowIfInvalid();
    }
}
```

---

*Revision Date: 2025-11-08*
