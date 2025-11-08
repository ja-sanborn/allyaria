# Allyaria.Abstractions.Results.AryResult

`AryResult` represents the outcome of an operation that can either succeed or fail, encapsulating the success state, any
associated error details, and relevant metadata. It provides a structured, exception-safe way to represent operation
results, enabling clear handling of both expected and unexpected outcomes without requiring exceptions for control flow.

This type forms the non-generic base for result patterns within the Allyaria framework, supporting functional-style
success/failure composition.

## Constructors

The constructor is private to enforce controlled instance creation through the provided static factory methods.

## Properties

| Name        | Type         | Description                                                                                        |
|-------------|--------------|----------------------------------------------------------------------------------------------------|
| `Error`     | `Exception?` | Gets the `Exception` associated with the result, or `null` if the result represents success.       |
| `IsFailure` | `bool`       | Gets a value indicating whether the operation failed (`true` if failed, otherwise `false`).        |
| `IsSuccess` | `bool`       | Gets a value indicating whether the operation succeeded (`true` if successful, otherwise `false`). |

## Methods

| Name                        | Returns        | Description                                                                                                                                                  |
|-----------------------------|----------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Failure(Exception? error)` | `AryResult`    | Creates a failure `AryResult` with the specified error. If `error` is `null`, an `AryException` with the message `"Unknown error"` is used.                  |
| `Success()`                 | `AryResult`    | Creates a successful `AryResult` representing a completed operation with no errors.                                                                          |
| `ToFailure<T>()`            | `AryResult<T>` | Converts this instance into a failed `AryResult<T>` with the same error details. Throws an `AryInvalidOperationException` if this result represents success. |

## Operators

*None*

## Events

*None*

## Exceptions

| Exception Type                 | Description                                                  |
|--------------------------------|--------------------------------------------------------------|
| `AryInvalidOperationException` | Thrown when calling `ToFailure<T>()` on a successful result. |

## Example

```csharp
using System;
using Allyaria.Abstractions.Results;
using Allyaria.Abstractions.Exceptions;

public class Example
{
    public AryResult PerformOperation(bool shouldFail)
    {
        if (shouldFail)
            return AryResult.Failure(new AryException("Operation failed."));

        return AryResult.Success();
    }

    public void Run()
    {
        var result = PerformOperation(shouldFail: true);

        if (result.IsFailure)
        {
            Console.WriteLine($"Operation failed with error: {result.Error?.Message}");
        }
        else
        {
            Console.WriteLine("Operation succeeded!");
        }
    }
}
```

---

*Revision Date: 2025-11-08*
