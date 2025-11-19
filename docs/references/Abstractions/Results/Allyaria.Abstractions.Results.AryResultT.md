# Allyaria.Abstractions.Results.AryResult<T>

`AryResult<T>` represents the outcome of an operation that may return a value of type `T`. It encapsulates both success
and failure states, along with optional error details and contextual information.

This class provides a functional alternative to exception-based error handling, enabling developers to model operation
outcomes in a clear and predictable way.

## Constructors

The constructor is private to enforce the use of factory methods for result creation (`Success` and `Failure`).

## Properties

| Name        | Type         | Description                                                                                        |
|-------------|--------------|----------------------------------------------------------------------------------------------------|
| `Error`     | `Exception?` | Gets the `Exception` associated with the result, or `null` if the operation succeeded.             |
| `IsFailure` | `bool`       | Gets a value indicating whether the operation failed (`true` if failed, otherwise `false`).        |
| `IsSuccess` | `bool`       | Gets a value indicating whether the operation succeeded (`true` if successful, otherwise `false`). |
| `Value`     | `T?`         | Gets the result value if the operation succeeded, or `null` if it failed.                          |

## Methods

| Name                        | Returns        | Description                                                                                                                                             |
|-----------------------------|----------------|---------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Failure(Exception? error)` | `AryResult<T>` | Creates a failed `AryResult<T>` with the specified error. If `error` is `null`, an `AryException` with message `"Unknown error"` is used.               |
| `Success(T value)`          | `AryResult<T>` | Creates a successful `AryResult<T>` that encapsulates the specified value.                                                                              |
| `ToFailure()`               | `AryResult`    | Converts this instance to a failed `AryResult` with the same error details. Throws an `AryInvalidOperationException` if this result represents success. |

## Operators

*None*

## Events

*None*

## Exceptions

| Exception Type                 | Description                                               |
|--------------------------------|-----------------------------------------------------------|
| `AryInvalidOperationException` | Thrown when calling `ToFailure()` on a successful result. |

## Example

```csharp
using System;
using Allyaria.Abstractions.Results;
using Allyaria.Abstractions.Exceptions;

public class Example
{
    public AryResult<int> Divide(int numerator, int denominator)
    {
        if (denominator == 0)
            return AryResult<int>.Failure(new AryArgumentException("Denominator cannot be zero.", nameof(denominator), denominator));

        return AryResult<int>.Success(numerator / denominator);
    }

    public void Run()
    {
        var result = Divide(10, 0);

        if (result.IsFailure)
        {
            Console.WriteLine($"Error: {result.Error?.Message}");
        }
        else
        {
            Console.WriteLine($"Result: {result.Value}");
        }
    }
}
```

---

*Revision Date: 2025-11-08*
