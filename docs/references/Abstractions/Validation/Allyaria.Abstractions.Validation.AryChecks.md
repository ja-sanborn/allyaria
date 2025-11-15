# Allyaria.Abstractions.Validation.AryChecks

`AryChecks` provides a comprehensive set of static helper methods for validating method arguments and returning
`AryArgumentException` instances when conditions fail. It supports consistent validation across Allyaria libraries and
encourages defensive programming without introducing direct dependencies on external validation frameworks.

Each method follows a lightweight pattern: it either returns an `AryArgumentException` describing the validation failure
or `null` when the argument is valid. Consumers can choose whether to throw the exception immediately or aggregate
multiple validation results.

## Constructors

`AryChecks` is a static class and cannot be instantiated.

## Properties

*None*

## Methods

| Name                                                                    | Returns                 | Description                                                                       |
|-------------------------------------------------------------------------|-------------------------|-----------------------------------------------------------------------------------|
| `Between<T>(T value, T min, T max, string argName)`                     | `AryArgumentException?` | Validates that `value` lies strictly between `min` and `max` (exclusive).         |
| `Check(bool condition, string argName, string message)`                 | `AryArgumentException?` | Validates a custom condition and returns an exception if it evaluates to `false`. |
| `EnumDefined<TEnum>(TEnum value, string argName)`                       | `AryArgumentException?` | Ensures that an enumeration `value` is defined within its type.                   |
| `EqualTo<T>(T value, T compare, string argName)`                        | `AryArgumentException?` | Validates that `value` equals `compare`.                                          |
| `False(bool condition, string argName)`                                 | `AryArgumentException?` | Ensures that `condition` is `false`.                                              |
| `GreaterThan<T>(T value, T minExclusive, string argName)`               | `AryArgumentException?` | Ensures that `value` is greater than `minExclusive` (exclusive).                  |
| `GreaterThanOrEqualTo<T>(T value, T minInclusive, string argName)`      | `AryArgumentException?` | Ensures that `value` is greater than or equal to `minInclusive`.                  |
| `InRange<T>(T value, T min, T max, string argName)`                     | `AryArgumentException?` | Validates that `value` lies between `min` and `max` (inclusive).                  |
| `IsAssignableTo<TTarget>(object? value, string argName)`                | `AryArgumentException?` | Ensures that `value` is assignable to `TTarget`.                                  |
| `LessThan<T>(T value, T maxExclusive, string argName)`                  | `AryArgumentException?` | Ensures that `value` is less than `maxExclusive` (exclusive).                     |
| `LessThanOrEqualTo<T>(T value, T maxInclusive, string argName)`         | `AryArgumentException?` | Ensures that `value` is less than or equal to `maxInclusive`.                     |
| `NotDefault<T>(T value, string argName)`                                | `AryArgumentException?` | Ensures that `value` is not its type’s default value.                             |
| `NotEqualTo<T>(T value, T compare, string argName)`                     | `AryArgumentException?` | Ensures that `value` does not equal `compare`.                                    |
| `NotNull<T>(T? value, string argName)`                                  | `AryArgumentException?` | Ensures that `value` is not `null`.                                               |
| `NotNullOrEmpty(string? value, string argName)`                         | `AryArgumentException?` | Ensures that a string is not `null` or empty.                                     |
| `NotNullOrEmpty<T>(IReadOnlyCollection<T>? collection, string argName)` | `AryArgumentException?` | Ensures that a collection is not `null` or empty.                                 |
| `NotNullOrWhiteSpace(string? value, string argName)`                    | `AryArgumentException?` | Ensures that a string is not `null`, empty, or whitespace.                        |
| `SameType<T1, T2>(T1 value1, T2 value2, string argName)`                | `AryArgumentException?` | Validates that `value1` and `value2` are of the same runtime type.                |
| `True(bool condition, string argName)`                                  | `AryArgumentException?` | Ensures that `condition` is `true`.                                               |

## Operators

*None*

## Events

*None*

## Exceptions

All methods return `AryArgumentException` instances; they do not throw directly.

## Example

```csharp
using System;
using Allyaria.Abstractions.Validation;
using Allyaria.Abstractions.Exceptions;

public class Example
{
    public void CreateUser(string? username, int age)
    {
        var ex1 = AryChecks.NotNullOrWhiteSpace(username, nameof(username));
        var ex2 = AryChecks.GreaterThanOrEqualTo(age, 18, nameof(age));

        if (ex1 is not null) throw ex1;
        if (ex2 is not null) throw ex2;

        Console.WriteLine($"User '{username}' created successfully at age {age}.");
    }
}
```

---

*Revision Date: 2025-11-08*
