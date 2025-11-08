# Allyaria.Abstractions.Validation.AryValidation<T>

`AryValidation<T>` represents a validation context for a single argument, enabling multiple validation checks to be
accumulated and evaluated collectively before throwing a combined `AryArgumentException`. It provides a structured and
readable way to perform layered argument validation while maintaining clean, composable, and exception-safe control
flow.

This type is most often used in conjunction with `AryChecks` to collect multiple validation outcomes for a single input
parameter.

## Constructors

`AryValidation(T argValue, string argName)` Initializes a new instance of the `AryValidation<T>` class for the specified
argument value and name.

## Properties

| Name       | Type                                  | Description                                                                                             |
|------------|---------------------------------------|---------------------------------------------------------------------------------------------------------|
| `ArgName`  | `string`                              | Gets the name of the argument being validated.                                                          |
| `ArgValue` | `T`                                   | Gets the value of the argument being validated.                                                         |
| `Errors`   | `IReadOnlyList<AryArgumentException>` | Gets the collection of validation errors accumulated during validation.                                 |
| `IsValid`  | `bool`                                | Gets a value indicating whether the argument passed all validation checks (i.e., no errors were added). |

## Methods

| Name                            | Returns | Description                                                                                                                                          |
|---------------------------------|---------|------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Add(AryArgumentException? ex)` | `void`  | Adds a validation error to the internal collection if the provided exception is non-null.                                                            |
| `ThrowIfInvalid()`              | `void`  | Throws a combined `AryArgumentException` if one or more validation errors are present, merging their messages into a single, line-separated message. |

## Operators

*None*

## Events

*None*

## Exceptions

| Exception Type         | Description                                                                        |
|------------------------|------------------------------------------------------------------------------------|
| `AryArgumentException` | Thrown by `ThrowIfInvalid()` when one or more accumulated validation errors exist. |

## Example

```csharp
using System;
using Allyaria.Abstractions.Validation;
using Allyaria.Abstractions.Exceptions;

public class Example
{
    public void CreateUser(string? username, int age)
    {
        var validation = new AryValidation<object>(argValue: username ?? string.Empty, argName: nameof(username));

        validation.Add(AryChecks.NotNullOrWhiteSpace(username, nameof(username)));
        validation.Add(AryChecks.GreaterThanOrEqualTo(age, 18, nameof(age)));

        validation.ThrowIfInvalid();

        Console.WriteLine($"User '{username}' created successfully at age {age}.");
    }
}
```

---

*Revision Date: 2025-11-08*
