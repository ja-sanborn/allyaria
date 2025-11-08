# Allyaria.Abstractions.Extensions.AryValidationExtensions

`AryValidationExtensions` provides a fluent, expressive set of extension methods for `AryValidation<T>` that enable
chainable argument validation using the `AryChecks` utility methods. It promotes composable validation logic where
multiple argument checks can be applied inline before conditionally throwing combined exceptions.

This class bridges `AryChecks`’ return-based validation style with a fluent builder pattern for streamlined argument
safety checks.

## Constructors

`AryValidationExtensions` is a static class and cannot be instantiated.

## Properties

*None*

## Methods

| Name                                                                                | Returns                                      | Description                                                                                      |
|-------------------------------------------------------------------------------------|----------------------------------------------|--------------------------------------------------------------------------------------------------|
| `Between<T>(this AryValidation<T> validation, T min, T max)`                        | `AryValidation<T>`                           | Validates that the current value lies strictly between `min` and `max` (exclusive).              |
| `Check<T>(this AryValidation<T> validation, bool condition, string message)`        | `AryValidation<T>`                           | Adds a custom validation check with an associated message when `condition` evaluates to `false`. |
| `EnumDefined<T>(this AryValidation<T> validation)`                                  | `AryValidation<T>`                           | Validates that the current enumeration value is defined within its enum type.                    |
| `EqualTo<T>(this AryValidation<T> validation, T compare)`                           | `AryValidation<T>`                           | Ensures that the current value equals the specified `compare` value.                             |
| `False<T>(this AryValidation<T> validation, bool condition)`                        | `AryValidation<T>`                           | Ensures that the specified condition is `false`.                                                 |
| `GreaterThan<T>(this AryValidation<T> validation, T minExclusive)`                  | `AryValidation<T>`                           | Validates that the current value is greater than the specified minimum (exclusive).              |
| `GreaterThanOrEqualTo<T>(this AryValidation<T> validation, T minInclusive)`         | `AryValidation<T>`                           | Validates that the current value is greater than or equal to the specified minimum (inclusive).  |
| `InRange<T>(this AryValidation<T> validation, T min, T max)`                        | `AryValidation<T>`                           | Validates that the current value lies between the specified inclusive range.                     |
| `IsAssignableTo<T, TTarget>(this AryValidation<T> validation)`                      | `AryValidation<T>`                           | Ensures that the current value is assignable to the specified target type `TTarget`.             |
| `LessThan<T>(this AryValidation<T> validation, T maxExclusive)`                     | `AryValidation<T>`                           | Validates that the current value is less than the specified maximum (exclusive).                 |
| `LessThanOrEqualTo<T>(this AryValidation<T> validation, T maxInclusive)`            | `AryValidation<T>`                           | Validates that the current value is less than or equal to the specified maximum (inclusive).     |
| `NotDefault<T>(this AryValidation<T> validation)`                                   | `AryValidation<T>`                           | Ensures that the current value is not its type’s default value.                                  |
| `NotEqualTo<T>(this AryValidation<T> validation, T compare)`                        | `AryValidation<T>`                           | Ensures that the current value does not equal the specified comparison value.                    |
| `NotNull<T>(this AryValidation<T> validation)`                                      | `AryValidation<T>`                           | Ensures that the current value is not `null`.                                                    |
| `NotNullOrEmpty(this AryValidation<string?> validation)`                            | `AryValidation<string?>`                     | Ensures that the current string value is not `null` or empty.                                    |
| `NotNullOrEmpty<TItem>(this AryValidation<IReadOnlyCollection<TItem>?> validation)` | `AryValidation<IReadOnlyCollection<TItem>?>` | Ensures that the current collection is not `null` or empty.                                      |
| `NotNullOrWhiteSpace(this AryValidation<string?> validation)`                       | `AryValidation<string?>`                     | Ensures that the current string value is not `null`, empty, or whitespace.                       |
| `SameTypeAs<T, TOther>(this AryValidation<T> validation, TOther other)`             | `AryValidation<T>`                           | Validates that the current value and another specified value are of the same runtime type.       |
| `True<T>(this AryValidation<T> validation, bool condition)`                         | `AryValidation<T>`                           | Ensures that the specified condition is `true`.                                                  |

## Operators

*None*

## Events

*None*

## Exceptions

Each method only adds `AryArgumentException` instances to the provided validation context; it does not throw directly.

## Example

```csharp
using System;
using Allyaria.Abstractions.Validation;
using Allyaria.Abstractions.Extensions;

public class Example
{
    public void RegisterUser(string? username, int age)
    {
        new AryValidation<string?>(username, nameof(username))
            .NotNullOrWhiteSpace()
            .Check(username?.Length >= 3, "Username must be at least 3 characters long.")
            .Check(username?.Length <= 20, "Username cannot exceed 20 characters.")
            .ThrowIfInvalid();

        new AryValidation<int>(age, nameof(age))
            .GreaterThanOrEqualTo(18)
            .LessThanOrEqualTo(100)
            .ThrowIfInvalid();

        Console.WriteLine($"User '{username}' (age {age}) registered successfully.");
    }
}
```

---

*Revision Date: 2025-11-08*
